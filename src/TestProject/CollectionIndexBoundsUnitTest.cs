// <copyright file="CollectionIndexBoundsUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

using System.Threading;

/// <summary>
///     Both collection indexers guard the upper bound with <c>if (i &gt;= this.Count) throw</c> and never
///     check the lower one, so a negative index is accepted and silently resolves to an element located
///     before the network.
///     <para>
///     The cursor of both collections starts at -1, so this is reachable without ever computing an index:
///     reading <c>Current</c> before the first <c>MoveNext()</c>, or having another thread call
///     <c>Reset()</c> mid-enumeration, both yield an address outside the network with no exception.
///     For a library used to decide whether an address belongs to a range, handing back an out of range
///     element instead of throwing is the dangerous failure mode.
///     </para>
/// </summary>
[TestClass]
public class CollectionIndexBoundsUnitTest
{
    private const string Network = "192.168.1.0/24";

    /// <summary>
    ///     A negative index must be rejected the same way an index past the end already is.
    /// </summary>
    [TestMethod]
    public void TestNetworkCollectionRejectsNegativeIndex()
    {
        IPNetworkCollection subnets = IPNetwork2.Parse(Network).Subnet(26);

        Assert.ThrowsExactly<ArgumentOutOfRangeException>(
            () => _ = subnets[subnets.Count],
            "The upper bound is guarded, for reference.");
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(
            () => _ = subnets[-1],
            "subnets[-1] returned a subnet located before the network instead of throwing.");
    }

    /// <summary>
    ///     Same asymmetry on <see cref="IPAddressCollection"/>.
    /// </summary>
    [TestMethod]
    public void TestAddressCollectionRejectsNegativeIndex()
    {
        IPAddressCollection addresses = IPNetwork2.Parse(Network).ListIPAddress();

        Assert.ThrowsExactly<ArgumentOutOfRangeException>(
            () => _ = addresses[addresses.Count],
            "The upper bound is guarded, for reference.");
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(
            () => _ = addresses[-1],
            "addresses[-1] returned an address located before the network instead of throwing.");
    }

    /// <summary>
    ///     The cursor starts at -1, so reading <c>Current</c> before the first <c>MoveNext()</c> goes
    ///     straight through the missing lower bound check and returns an element outside the network.
    /// </summary>
    [TestMethod]
    public void TestCurrentBeforeMoveNextDoesNotReturnAnElementOutsideTheNetwork()
    {
        var network = IPNetwork2.Parse(Network);
        IPNetworkCollection subnets = network.Subnet(26);
        IPAddressCollection addresses = network.ListIPAddress();

        IPNetwork2? subnetBeforeStart = null;
        IPAddress? addressBeforeStart = null;

        try
        {
            subnetBeforeStart = subnets.Current;
        }
        catch (ArgumentOutOfRangeException)
        {
            // Throwing is the acceptable behaviour.
        }

        try
        {
            addressBeforeStart = addresses.Current;
        }
        catch (ArgumentOutOfRangeException)
        {
            // Throwing is the acceptable behaviour.
        }

        Assert.IsNull(
            subnetBeforeStart,
            $"IPNetworkCollection.Current returned {subnetBeforeStart} before the first MoveNext(), which "
            + $"is outside {Network}.");
        Assert.IsNull(
            addressBeforeStart,
            $"IPAddressCollection.Current returned {addressBeforeStart} before the first MoveNext(), which "
            + $"is outside {Network}.");
    }

    /// <summary>
    ///     Consequence under concurrency: a <c>Reset()</c> issued by another thread puts the shared cursor
    ///     back to -1 while a reader sits between <c>MoveNext()</c> and <c>Current</c>. The missing lower
    ///     bound check turns that into an element outside the network rather than an exception.
    /// </summary>
    [TestMethod]
    public void TestConcurrentResetNeverYieldsAnElementOutsideTheNetwork()
    {
        var network = IPNetwork2.Parse(Network);
        IPNetworkCollection subnets = network.Subnet(32);

        int stop = 0;
        long reads = 0;
        string? escaped = null;

        var resetter = new Thread(() =>
        {
            while (Volatile.Read(ref stop) == 0)
            {
                subnets.Reset();
            }
        });

        var reader = new Thread(() =>
        {
            while (Volatile.Read(ref stop) == 0)
            {
                try
                {
                    subnets.MoveNext();
                    IPNetwork2 current = subnets.Current;
                    Interlocked.Increment(ref reads);

                    if (!network.Contains(current))
                    {
                        Interlocked.CompareExchange(ref escaped, current.ToString(), null);
                        Volatile.Write(ref stop, 1);
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    // Throwing is the acceptable behaviour, keep looking for a silent escape.
                }
            }
        });

        resetter.IsBackground = true;
        reader.IsBackground = true;
        resetter.Start();
        reader.Start();

        for (int i = 0; i < 200 && Volatile.Read(ref escaped) == null; i++)
        {
            Thread.Sleep(10);
        }

        Volatile.Write(ref stop, 1);
        resetter.Join(TimeSpan.FromSeconds(30));
        reader.Join(TimeSpan.FromSeconds(30));

        Assert.IsNull(
            escaped,
            $"After {reads} reads a concurrent Reset() made the enumeration yield {escaped}, which is "
            + $"outside {Network}: the cursor went back to -1 and the indexer accepted it.");
    }
}
