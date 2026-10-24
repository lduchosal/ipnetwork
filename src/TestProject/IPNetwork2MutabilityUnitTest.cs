// <copyright file="IPNetwork2MutabilityUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

using System.Diagnostics;
using System.Reflection;
using System.Threading;

/// <summary>
///     <see cref="IPNetwork2"/> behaves like an immutable value everywhere: it is built through
///     <c>Parse</c>, compared with <c>Equals</c>, hashed, and handed out as process wide singletons such
///     as the IANA blocks. Yet its <see cref="IPNetwork2.Value"/> property exposes a public setter that
///     rewrites the instance in place.
///     <para>
///     These tests pin the consequences: the mutation was not atomic, the cached hash code was never
///     refreshed, and the shared singletons could be rewritten by any caller.
///     </para>
/// </summary>
[TestClass]
public class IPNetwork2MutabilityUnitTest
{
    /// <summary>How long the concurrency tests hunt for a torn observation before giving up.</summary>
    private static readonly TimeSpan Budget = TimeSpan.FromSeconds(2);

    private const string NetworkA = "10.0.0.0/8";
    private const string NetworkB = "192.168.1.0/24";

    private const string BroadcastA = "10.255.255.255";
    private const string BroadcastB = "192.168.1.255";

    /// <summary>
    ///     The setter used to rewrite <c>ipaddress</c> (a 16 byte BigInteger), <c>family</c> and
    ///     <c>cidr</c> as three separate unsynchronized writes, so a reader could pair the new address
    ///     with the old CIDR and observe a network that was never assigned.
    ///     <para>
    ///     The observation is a single call on purpose. One library call must report one coherent
    ///     network; stitching two properties together is the caller's own race, not the library's.
    ///     </para>
    /// </summary>
    [TestMethod]
    public void TestValueSetterIsObservedAtomically()
    {
        string? anomaly = RunMutationRace(network => network.ToString(), NetworkA, NetworkB);

        Assert.IsNull(
            anomaly,
            $"A thread observed {anomaly}, which is neither {NetworkA} nor {NetworkB}: the Value setter "
            + "does not replace the address, the family and the CIDR as one, so readers can see a mix of "
            + "the old and the new network.");
    }

    /// <summary>
    ///     Same race seen through the broadcast address, which is both derived from all three fields and
    ///     memoized. A cache populated from a half written network survives the mutation and keeps
    ///     answering for a network that never existed.
    /// </summary>
    [TestMethod]
    public void TestBroadcastFollowsTheValueSetterAtomically()
    {
        string? anomaly = RunMutationRace(
            network => network.Broadcast!.ToString(),
            BroadcastA,
            BroadcastB);

        Assert.IsNull(
            anomaly,
            $"A thread observed a broadcast of {anomaly}, which is neither {BroadcastA} nor {BroadcastB}: "
            + "the broadcast cache is not invalidated together with the network it describes.");
    }

    /// <summary>
    ///     Deterministic guard for the same defect. The race above only reproduces when the scheduler
    ///     cooperates, which is unlikely on a small or strongly ordered CI machine. This test instead
    ///     pins the property that makes the tear impossible: the whole mutable state of an instance is
    ///     one reference, so replacing the network is a single atomic store.
    /// </summary>
    [TestMethod]
    public void TestMutableStateIsPublishedByASingleStore()
    {
        FieldInfo[] fields = typeof(IPNetwork2)
            .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
            .ToArray();

        Assert.HasCount(
            1,
            fields,
            "IPNetwork2 carries " + fields.Length + " instance fields ("
            + string.Join(", ", fields.Select(field => field.Name))
            + "). Several fields cannot be replaced by one store, so a reader can observe a network "
            + "made of one assignment and the next. Keep the state in a single reference.");

        Assert.IsFalse(
            fields[0].FieldType.IsValueType,
            $"IPNetwork2.{fields[0].Name} is a value type ({fields[0].FieldType.Name}). A multi-word "
            + "struct cannot be written or read atomically, so the mutation stays observable half done.");
    }

    /// <summary>
    ///     The hash code used to be memoized in a readonly Lazy built in the constructor, which the
    ///     <see cref="IPNetwork2.Value"/> setter never invalidated. Once materialized, mutating the
    ///     instance left the stale value in place.
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
    ///     Consequence of the stale hash: two instances that compare equal ended up with different hash
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
    ///     <see cref="IPNetwork2.Value"/> is publicly settable, any caller could rewrite them for the
    ///     whole process and change what the library reports as reserved.
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

    /// <summary>
    ///     Same defect on the ULA prefixes, which used to be public static readonly fields and so were
    ///     shared with every caller that asked for one.
    /// </summary>
    [TestMethod]
    public void TestUlaSingletonIsNotRewritableByCallers()
    {
        UniqueLocalAddress.UlaRange.Value = "8.8.8.0/24";

        Assert.AreEqual(
            "fc00::/7",
            UniqueLocalAddress.UlaRange.ToString(),
            "A caller rewrote the UlaRange singleton for the entire process.");
        Assert.IsTrue(
            IPNetwork2.Parse("fd12:3456:789a::/48").IsUla(),
            "A ULA prefix is no longer recognised after a caller mutated the shared UlaRange singleton.");
    }

    /// <summary>
    ///     Releases a writer and several readers on one shared network, the writer flipping it between
    ///     two values while the readers observe it through a single call.
    /// </summary>
    /// <param name="observe">The single call under observation.</param>
    /// <param name="expected">The observations that correspond to a network that was actually assigned.</param>
    /// <returns>The first observation that matches no assignment, or null if none was seen.</returns>
    private static string? RunMutationRace(Func<IPNetwork2, string> observe, params string[] expected)
    {
        var shared = IPNetwork2.Parse(NetworkA);
        int stop = 0;
        string? anomaly = null;

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
                        observed = observe(shared);
                    }
                    catch (Exception ex)
                    {
                        observed = ex.GetType().Name + ": " + ex.Message;
                    }

                    if (Array.IndexOf(expected, observed) < 0)
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

        var writer = new Thread(() =>
        {
            bool flip = false;
            while (Volatile.Read(ref stop) == 0)
            {
                shared.Value = flip ? NetworkA : NetworkB;
                flip = !flip;
            }
        });

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

        return Volatile.Read(ref anomaly);
    }
}
