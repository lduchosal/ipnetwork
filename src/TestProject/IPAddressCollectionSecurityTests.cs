// <copyright file="IPAddressCollectionSecurityTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

using System.Diagnostics;

/// <summary>
/// Security tests for IPAddressCollection — issue #371.
/// Validates that accessing IP addresses in large networks
/// completes in reasonable time without memory exhaustion.
/// </summary>
[TestClass]
public class IPAddressCollectionSecurityTests
{
    private const long MaxMilliseconds = 1000;

    /// <summary>
    /// Accessing the first IP of a /0 IPv4 network by index must complete quickly.
    /// Before fix: this triggers Subnet(32) creating 2^32 subnets = timeout/OOM.
    /// </summary>
    [TestMethod]
    public void TestIndexer_LargeIPv4Network_CompletesQuickly()
    {
        var network = IPNetwork2.Parse("0.0.0.0/0");
        var collection = network.ListIPAddress();

        var sw = Stopwatch.StartNew();
        var first = collection[0];
        sw.Stop();

        Assert.AreEqual(IPAddress.Parse("0.0.0.0"), first);
        Assert.IsLessThan(MaxMilliseconds, sw.ElapsedMilliseconds);
    }

    /// <summary>
    /// Accessing the last IP of a /0 IPv4 network by index must complete quickly.
    /// </summary>
    [TestMethod]
    public void TestIndexer_LargeIPv4Network_LastAddress_CompletesQuickly()
    {
        var network = IPNetwork2.Parse("0.0.0.0/0");
        var collection = network.ListIPAddress();

        var sw = Stopwatch.StartNew();
        var last = collection[collection.Count - 1];
        sw.Stop();

        Assert.AreEqual(IPAddress.Parse("255.255.255.255"), last);
        Assert.IsLessThan(MaxMilliseconds, sw.ElapsedMilliseconds);
    }

    /// <summary>
    /// Accessing an IP in the middle of a /0 network must complete quickly.
    /// </summary>
    [TestMethod]
    public void TestIndexer_LargeIPv4Network_MiddleAddress_CompletesQuickly()
    {
        var network = IPNetwork2.Parse("0.0.0.0/0");
        var collection = network.ListIPAddress();

        var sw = Stopwatch.StartNew();
        var middle = collection[BigInteger.Pow(2, 31)];
        sw.Stop();

        Assert.AreEqual(IPAddress.Parse("128.0.0.0"), middle);
        Assert.IsLessThan(MaxMilliseconds, sw.ElapsedMilliseconds);
    }

    /// <summary>
    /// Accessing the first IP of a /0 IPv6 network by index must complete quickly.
    /// </summary>
    [TestMethod]
    public void TestIndexer_LargeIPv6Network_CompletesQuickly()
    {
        var network = IPNetwork2.Parse("::/0");
        var collection = network.ListIPAddress();

        var sw = Stopwatch.StartNew();
        var first = collection[0];
        sw.Stop();

        Assert.AreEqual(IPAddress.Parse("::"), first);
        Assert.IsLessThan(MaxMilliseconds, sw.ElapsedMilliseconds);
    }

    /// <summary>
    /// Accessing IPs in a small /24 network still works correctly after the fix.
    /// </summary>
    [TestMethod]
    public void TestIndexer_SmallNetwork_CorrectResults()
    {
        var network = IPNetwork2.Parse("192.168.1.0/24");
        var collection = network.ListIPAddress();

        Assert.AreEqual(IPAddress.Parse("192.168.1.0"), collection[0]);
        Assert.AreEqual(IPAddress.Parse("192.168.1.1"), collection[1]);
        Assert.AreEqual(IPAddress.Parse("192.168.1.254"), collection[254]);
        Assert.AreEqual(IPAddress.Parse("192.168.1.255"), collection[255]);
    }

    /// <summary>
    /// Usable filter still works correctly on a small network after the fix.
    /// </summary>
    [TestMethod]
    public void TestIndexer_SmallNetwork_UsableFilter_CorrectResults()
    {
        var network = IPNetwork2.Parse("192.168.1.0/24");
        var collection = network.ListIPAddress(Filter.Usable);

        // Usable skips network (192.168.1.0) and broadcast (192.168.1.255)
        Assert.AreEqual(IPAddress.Parse("192.168.1.1"), collection[0]);
        Assert.AreEqual(IPAddress.Parse("192.168.1.254"), collection[collection.Count - 1]);
    }

    /// <summary>
    /// Enumerating a /24 network via foreach completes correctly.
    /// </summary>
    [TestMethod]
    public void TestEnumerator_SmallNetwork_CorrectCount()
    {
        var network = IPNetwork2.Parse("192.168.1.0/24");
        var collection = network.ListIPAddress();

        int count = 0;
        foreach (var _ in collection)
        {
            count++;
        }

        Assert.AreEqual(256, count);
    }

    /// <summary>
    /// Accessing a /8 network by index must complete quickly (was slow before fix).
    /// </summary>
    [TestMethod]
    public void TestIndexer_ClassANetwork_CompletesQuickly()
    {
        var network = IPNetwork2.Parse("10.0.0.0/8");
        var collection = network.ListIPAddress();

        var sw = Stopwatch.StartNew();
        var ip = collection[256];
        sw.Stop();

        Assert.AreEqual(IPAddress.Parse("10.0.1.0"), ip);
        Assert.IsLessThan(MaxMilliseconds, sw.ElapsedMilliseconds);
    }
}
