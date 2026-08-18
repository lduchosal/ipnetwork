// <copyright file="IPNetwork2MutabilityUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

using System.Diagnostics;
using System.Threading;

/// <summary>
///     <see cref="IPNetwork2"/> behaves like an immutable value everywhere: it is built through
///     <c>Parse</c>, compared with <c>Equals</c>, hashed, and handed out as process wide singletons such
///     as the IANA blocks. Yet its <see cref="IPNetwork2.Value"/> property exposes a public setter that
///     rewrites the instance in place.
///     <para>
///     These tests pin the consequences: the mutation is not atomic, the cached hash code is never
///     refreshed, and the shared singletons can be rewritten by any caller.
///     </para>
/// </summary>
[TestClass]
public class IPNetwork2MutabilityUnitTest
{
    /// <summary>How long the concurrency test hunts for a torn observation before giving up.</summary>
    private static readonly TimeSpan Budget = TimeSpan.FromSeconds(2);

    private const string NetworkA = "10.0.0.0/8";
    private const string NetworkB = "192.168.1.0/24";

    /// <summary>
    ///     The setter rewrites <c>ipaddress</c> (a 16 byte BigInteger), <c>family</c> and <c>cidr</c> as
    ///     three separate unsynchronized writes, so a concurrent reader can pair the new CIDR with the old
    ///     address and observe a network that was never assigned.
    /// </summary>
    [TestMethod]
    public void TestValueSetterIsObservedAtomically()
    {
        var shared = IPNetwork2.Parse(NetworkA);
        int stop = 0;
        long reads = 0;
        string? anomaly = null;

        var writer = new Thread(() =>
        {
            bool flip = false;
            while (Volatile.Read(ref stop) == 0)
            {
                shared.Value = flip ? NetworkA : NetworkB;
                flip = !flip;
            }
        });

        var readers = new Thread[Math.Max(2, Environment.ProcessorCount / 2)];
        for (int i = 0; i < readers.Length; i++)
        {
            var reader = new Thread(() =>
            {
                while (Volatile.Read(ref stop) == 0)
                {
                    string observed;
                    try
                    {
                        observed = shared.Network + "/" + shared.Cidr;
                    }
                    catch (Exception ex)
                    {
                        observed = ex.GetType().Name + ": " + ex.Message;
                    }

                    Interlocked.Increment(ref reads);

                    if (observed != NetworkA && observed != NetworkB)
                    {
                        Interlocked.CompareExchange(ref anomaly, observed, null);
                        Volatile.Write(ref stop, 1);
                    }
                }
            });

            reader.IsBackground = true;
            readers[i] = reader;
            reader.Start();
        }

        writer.IsBackground = true;
        writer.Start();

        var clock = Stopwatch.StartNew();
        while (clock.Elapsed < Budget && Volatile.Read(ref anomaly) == null)
        {
            Thread.Sleep(10);
        }

        Volatile.Write(ref stop, 1);
        writer.Join(TimeSpan.FromSeconds(30));
        foreach (Thread reader in readers)
        {
            reader.Join(TimeSpan.FromSeconds(30));
        }

        Assert.IsNull(
            anomaly,
            $"After {reads} reads, a thread observed {anomaly}, which is neither {NetworkA} nor {NetworkB}: "
            + "the Value setter rewrites the address, the family and the CIDR as separate writes, so readers "
            + "can see a mix of the old and the new network.");
    }

    /// <summary>
    ///     The hash code is memoized in a readonly Lazy built in the constructor, and the
    ///     <see cref="IPNetwork2.Value"/> setter never invalidates it. Once the hash has been materialized,
    ///     mutating the instance leaves the stale value in place.
    /// </summary>
    [TestMethod]
    public void TestHashCodeIsRefreshedAfterMutation()
    {
        var network = IPNetwork2.Parse(NetworkA);
        int before = network.GetHashCode();

        network.Value = NetworkB;

        var expected = IPNetwork2.Parse(NetworkB);
        Assert.AreEqual(
            expected.GetHashCode(),
            network.GetHashCode(),
            $"After mutating to {NetworkB} the instance still hashes to {before}, the hash of {NetworkA}: "
            + "the cached hash code is never invalidated by the Value setter.");
    }

    /// <summary>
    ///     Consequence of the stale hash: two instances that compare equal end up with different hash
    ///     codes, which breaks the Equals/GetHashCode contract and every hash based collection.
    /// </summary>
    [TestMethod]
    public void TestMutatedNetworkIsStillFoundInHashSet()
    {
        var network = IPNetwork2.Parse(NetworkA);
        _ = network.GetHashCode();
        network.Value = NetworkB;

        var expected = IPNetwork2.Parse(NetworkB);
        var set = new HashSet<IPNetwork2> { expected };

        Assert.IsTrue(network.Equals(expected), "The mutated network is expected to equal a freshly parsed one.");
        Assert.IsTrue(
            set.Contains(network),
            "A HashSet does not find the mutated network even though it equals the stored one: equal "
            + "instances carry different hash codes.");
    }

    /// <summary>
    ///     The IANA blocks are process wide singletons handed out by static properties. Because
    ///     <see cref="IPNetwork2.Value"/> is publicly settable, any caller can rewrite them for the whole
    ///     process and change what the library reports as reserved.
    /// </summary>
    [TestMethod]
    public void TestIanaSingletonIsNotRewritableByCallers()
    {
        string original = IPNetwork2.IANA_LOOPBACK.ToString();
        try
        {
            IPNetwork2.IANA_LOOPBACK.Value = "8.8.8.0/24";

            Assert.AreEqual(
                "127.0.0.0/8",
                IPNetwork2.IANA_LOOPBACK.ToString(),
                "A caller rewrote the IANA_LOOPBACK singleton for the entire process.");
            Assert.IsTrue(
                IPNetwork2.Parse("127.0.0.1/32").IsIANAReserved(),
                "127.0.0.1 is no longer reported as IANA reserved after a caller mutated the shared "
                + "IANA_LOOPBACK singleton.");
        }
        finally
        {
            IPNetwork2.IANA_LOOPBACK.Value = original;
        }
    }
}
