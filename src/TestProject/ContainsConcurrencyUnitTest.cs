// <copyright file="ContainsConcurrencyUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

using System.Diagnostics;
using System.Reflection;
using System.Threading;

/// <summary>
///     Regression tests for issue #429: the broadcast cache backing <see cref="IPNetwork2.Contains(IPAddress)"/>
///     used to be a <c>BigInteger?</c> field published outside of any synchronization on the read fast path.
///     <para>
///     A <c>Nullable&lt;BigInteger&gt;</c> is 24 bytes on a 64 bit runtime and holds three separate fields
///     (<c>hasValue</c>, <c>BigInteger._sign</c> and <c>BigInteger._bits</c>), so neither the write nor the
///     read is atomic. A reader taking the lock-free fast path could observe <c>hasValue == true</c> while
///     the magnitude was still half written, and get a broadcast address of 0 or 1 instead of the real one.
///     </para>
/// </summary>
[TestClass]
public class ContainsConcurrencyUnitTest
{
    /// <summary>The network under test: 128.0.0.0/1, whose broadcast exceeds int.MaxValue.</summary>
    private const string ExpectedBroadcast = "255.255.255.255";

    /// <summary>How long a race test is allowed to hunt for a torn read before giving up.</summary>
    private static readonly TimeSpan Budget = TimeSpan.FromSeconds(2);

    private static readonly IPAddress Seed = IPAddress.Parse("128.0.0.0");
    private static readonly IPAddress Probe = IPAddress.Parse("200.1.2.3");

    /// <summary>
    ///     Hammers <see cref="IPNetwork2.Contains(IPAddress)"/> from many threads against a freshly built
    ///     network, so that every round races the very first population of the broadcast cache.
    ///     <see cref="IPNetwork2.Contains(IPAddress)"/> must never report a wrong answer.
    /// </summary>
    [TestMethod]
    public void TestContainsIsCorrectUnderConcurrentFirstAccess()
    {
        string? anomaly = RunFirstAccessRace(ObserveContains);
        Assert.IsNull(anomaly, $"Contains() returned a wrong result under concurrency: {anomaly}");
    }

    /// <summary>
    ///     Same race, observing the cached value directly rather than through
    ///     <see cref="IPNetwork2.Contains(IPAddress)"/>, so a torn read is reported with the value actually seen.
    /// </summary>
    [TestMethod]
    public void TestBroadcastIsCorrectUnderConcurrentFirstAccess()
    {
        string? anomaly = RunFirstAccessRace(ObserveBroadcast);
        Assert.IsNull(anomaly, $"Broadcast was torn under concurrency: {anomaly}");
    }

    /// <summary>
    ///     Deterministic guard for the same defect. The race tests above only detect a tear when the
    ///     scheduler cooperates, which is unlikely on a small or strongly ordered CI machine. This test
    ///     instead pins the invariant that makes the tear impossible: the broadcast cache must be
    ///     publishable with a single atomic store, i.e. it must be a reference, not a multi-word struct.
    /// </summary>
    [TestMethod]
    public void TestBroadcastCacheIsAtomicallyPublishable()
    {
        FieldInfo[] fields = typeof(IPNetwork2)
            .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
            .Where(field => field.Name.IndexOf("broadcast", StringComparison.OrdinalIgnoreCase) >= 0)
            .ToArray();

        Assert.AreEqual(1, fields.Length, "Expected exactly one broadcast cache field on IPNetwork2.");

        FieldInfo cache = fields[0];
        Assert.IsFalse(
            cache.FieldType.IsValueType,
            $"IPNetwork2.{cache.Name} is a value type ({cache.FieldType.Name}). A multi-word struct cannot be "
            + "published atomically, so a reader on the lock-free fast path can observe it half written. "
            + "Store the cache behind a reference instead.");
    }

    private static string? ObserveContains(IPNetwork2 network)
    {
        return network.Contains(Probe)
            ? null
            : string.Format("Contains({0}) returned false for 128.0.0.0/1", Probe);
    }

    private static string? ObserveBroadcast(IPNetwork2 network)
    {
        IPAddress? broadcast = network.Broadcast;
        string actual = broadcast == null ? "<null>" : broadcast.ToString();

        return actual == ExpectedBroadcast
            ? null
            : string.Format("Broadcast was {0} instead of {1}", actual, ExpectedBroadcast);
    }

    /// <summary>
    ///     Releases every worker onto a brand new <see cref="IPNetwork2"/> at the same instant, round after
    ///     round, and staggers each worker by a rotating spin count so that the observations sweep across
    ///     the window during which the cache is being populated.
    /// </summary>
    /// <param name="observe">Returns null when the observation is correct, or a description of the anomaly.</param>
    /// <returns>The first anomaly observed, or null if none was seen within the budget.</returns>
    private static string? RunFirstAccessRace(Func<IPNetwork2, string?> observe)
    {
        int threadCount = Math.Max(4, Environment.ProcessorCount - 1);
        const int MaxSpin = 256;

        IPNetwork2? shared = null;
        int generation = 0;
        int arrived = 0;
        int stop = 0;
        string? anomaly = null;

        var workers = new Thread[threadCount];
        for (int i = 0; i < threadCount; i++)
        {
            int id = i;
            var worker = new Thread(() =>
            {
                int seen = 0;
                while (true)
                {
                    seen++;
                    while (Volatile.Read(ref generation) < seen)
                    {
                        if (Volatile.Read(ref stop) == 1)
                        {
                            return;
                        }
                    }

                    if (Volatile.Read(ref stop) == 1)
                    {
                        return;
                    }

                    IPNetwork2 network = Volatile.Read(ref shared) !;

                    // Stagger the observation so that, across rounds and threads, some read lands
                    // while the cache is being written rather than before or after it.
                    int delay = ((id * 37) + (seen * 13)) % MaxSpin;
                    if (delay > 0)
                    {
                        Thread.SpinWait(delay);
                    }

                    string? observed;
                    try
                    {
                        observed = observe(network);
                    }
                    catch (Exception ex)
                    {
                        observed = ex.GetType().Name + ": " + ex.Message;
                    }

                    if (observed != null)
                    {
                        Interlocked.CompareExchange(ref anomaly, observed, null);
                    }

                    Interlocked.Increment(ref arrived);
                }
            });

            worker.IsBackground = true;
            worker.Name = "race-" + id;
            workers[i] = worker;
            worker.Start();
        }

        var clock = Stopwatch.StartNew();
        try
        {
            while (Volatile.Read(ref anomaly) == null && clock.Elapsed < Budget)
            {
                Volatile.Write(ref shared, new IPNetwork2(Seed, 1));
                Volatile.Write(ref arrived, 0);
                Interlocked.Increment(ref generation);

                while (Volatile.Read(ref arrived) < threadCount)
                {
                    Thread.SpinWait(1);
                }
            }
        }
        finally
        {
            Volatile.Write(ref stop, 1);
            Interlocked.Increment(ref generation);
            foreach (Thread worker in workers)
            {
                worker.Join(TimeSpan.FromSeconds(5));
            }
        }

        return Volatile.Read(ref anomaly);
    }
}
