// <copyright file="IPNetworkCollectionSharedCursorUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

using System.Threading;

/// <summary>
///     <see cref="IPNetworkCollection"/> implements both <see cref="IEnumerable{T}"/> and
///     <see cref="IEnumerator{T}"/>, and its <c>GetEnumerator()</c> returns <c>this</c>. Every consumer
///     therefore shares one single cursor held in a field of the collection.
///     <para>
///     A collection is expected to hand out an independent cursor per enumeration, the way
///     <see cref="IPAddressCollection"/> already does by returning a dedicated enumerator instance.
///     Sharing it makes a second, a nested or a concurrent enumeration silently return the wrong
///     elements, with no exception raised.
///     </para>
/// </summary>
[TestClass]
public class IPNetworkCollectionSharedCursorUnitTest
{
    /// <summary>
    ///     Enumerating a collection twice must yield every element both times. With a shared cursor the
    ///     second loop starts where the first one stopped, so it yields nothing at all.
    /// </summary>
    [TestMethod]
    public void TestSecondEnumerationYieldsEveryElement()
    {
        var network = IPNetwork2.Parse("192.168.0.0/24");
        IPNetworkCollection subnets = network.Subnet(26);

        int first = CountByForeach(subnets);
        int second = CountByForeach(subnets);

        Assert.AreEqual(4, first, "The first enumeration is expected to yield the 4 subnets of 192.168.0.0/24.");
        Assert.AreEqual(
            4,
            second,
            $"Re-enumerating the same collection yielded {second} subnets instead of 4: the cursor is shared "
            + "between enumerations instead of being created per GetEnumerator() call.");
    }

    /// <summary>
    ///     Nested enumeration of the same collection must visit every pair. The inner loop consumes the
    ///     one and only cursor, so the outer loop is truncated as a side effect.
    /// </summary>
    [TestMethod]
    public void TestNestedEnumerationVisitsEveryPair()
    {
        var network = IPNetwork2.Parse("192.168.0.0/24");
        IPNetworkCollection subnets = network.Subnet(26);

        int pairs = 0;
        foreach (IPNetwork2 unusedOuter in subnets)
        {
            foreach (IPNetwork2 unusedInner in subnets)
            {
                pairs++;
            }
        }

        Assert.AreEqual(
            16,
            pairs,
            $"Nested enumeration visited {pairs} pairs instead of 16: the inner loop consumed the cursor "
            + "shared with the outer loop.");
    }

    /// <summary>
    ///     Two threads enumerating the same collection must each see the whole collection, exactly as two
    ///     sequential loops would. They share one cursor instead, so the elements get split between them.
    ///     <para>
    ///     This failure does not depend on the interleaving: whichever way the two threads are scheduled,
    ///     the single cursor is advanced once per element, so the observed total is about Count rather
    ///     than twice Count.
    ///     </para>
    /// </summary>
    [TestMethod]
    public void TestConcurrentEnumerationsAreIndependent()
    {
        var network = IPNetwork2.Parse("10.0.0.0/16");
        IPNetworkCollection subnets = network.Subnet(24);
        int expected = (int)subnets.Count;

        int total = 0;
        string? failure = null;

        var workers = new Thread[2];
        for (int i = 0; i < workers.Length; i++)
        {
            var worker = new Thread(() =>
            {
                try
                {
                    Interlocked.Add(ref total, CountByForeach(subnets));
                }
                catch (Exception ex)
                {
                    Interlocked.CompareExchange(ref failure, ex.GetType().Name + ": " + ex.Message, null);
                }
            });

            worker.IsBackground = true;
            workers[i] = worker;
            worker.Start();
        }

        foreach (Thread worker in workers)
        {
            worker.Join(TimeSpan.FromSeconds(30));
        }

        Assert.IsNull(failure, $"Concurrent enumeration threw: {failure}");
        Assert.AreEqual(
            expected * 2,
            total,
            $"Two concurrent enumerations of a {expected} element collection yielded {total} elements in "
            + $"total instead of {expected * 2}: both threads advanced the same shared cursor, so each of "
            + "them silently skipped the subnets consumed by the other.");
    }

    /// <summary>
    ///     Deterministic guard for the same defect, and for its side effect: because GetEnumerator()
    ///     returns the collection itself, the collection is also the IDisposable that foreach disposes at
    ///     the end of every loop. A collection must hand out an enumerator distinct from itself.
    /// </summary>
    [TestMethod]
    public void TestGetEnumeratorReturnsAnInstanceDistinctFromTheCollection()
    {
        var network = IPNetwork2.Parse("192.168.0.0/24");
        IPNetworkCollection subnets = network.Subnet(26);

        IEnumerator<IPNetwork2> first = ((IEnumerable<IPNetwork2>)subnets).GetEnumerator();
        IEnumerator<IPNetwork2> second = ((IEnumerable<IPNetwork2>)subnets).GetEnumerator();

        Assert.AreNotSame(
            subnets,
            first,
            "GetEnumerator() returned the collection itself, so every enumeration shares one cursor and "
            + "foreach disposes the collection when it ends.");
        Assert.AreNotSame(
            first,
            second,
            "Two calls to GetEnumerator() returned the same instance, so two enumerations cannot be "
            + "independent.");
    }

    private static int CountByForeach(IPNetworkCollection subnets)
    {
        int count = 0;
        foreach (IPNetwork2 unused in subnets)
        {
            count++;
        }

        return count;
    }
}