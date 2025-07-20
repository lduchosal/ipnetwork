// <copyright file="IPNetworkV6TrySupernetTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// TrySupernet.
/// </summary>
[TestClass]
public class IPNetworkV6TrySupernetTests
{
    /// <summary>
    /// Test try supernet.
    /// </summary>
    [TestMethod]
    public void TestTrySupernet1()
    {
        var network1 = IPNetwork2.Parse("2001:db8::/65");
        var network2 = IPNetwork2.Parse("2001:db8:0:0:8000::/65");
        var supernetExpected = IPNetwork2.Parse("2001:db8::/64");
        bool result = network1.TrySupernet(network2, out IPNetwork2 supernet);

        Assert.IsTrue(result, "supernetted");
        Assert.AreEqual(supernetExpected, supernet, "supernet");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestTrySupernet2()
    {
        IPNetwork2 network1 = null;
        var network2 = IPNetwork2.Parse("2001:db8::/64");
        IPNetwork2 supernet;
#pragma warning disable 0618
        IPNetwork2.TrySupernet(network1, network2, out supernet);
#pragma warning restore 0618
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTrySupernet3()
    {
        var network1 = IPNetwork2.Parse("2001:db8::/64");
        IPNetwork2 network2 = null;
        IPNetwork2 supernetExpected = null;
        bool result = network1.TrySupernet(network2, out IPNetwork2 supernet);

        Assert.AreEqual(supernetExpected, supernet, "supernet");
        Assert.IsFalse(result, "parsed");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTrySupernet4()
    {
        var network1 = IPNetwork2.Parse("2001:db8::/64");
        var network2 = IPNetwork2.Parse("2001:db9::/65");
        IPNetwork2 supernetExpected = null;
        bool result = network1.TrySupernet(network2, out IPNetwork2 supernet);

        Assert.AreEqual(supernetExpected, supernet, "supernet");
        Assert.IsFalse(result, "parsed");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTrySupernet5()
    {
        var network1 = IPNetwork2.Parse("2001:db8::/64");
        var network2 = IPNetwork2.Parse("2001:dba::/64");
        IPNetwork2 supernetExpected = null;
        bool result = network1.TrySupernet(network2, out IPNetwork2 supernet);

        Assert.AreEqual(supernetExpected, supernet, "supernet");
        Assert.IsFalse(result, "parsed");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTrySupernet6()
    {
        var network1 = IPNetwork2.Parse("2001:db8::/64");
        var network2 = IPNetwork2.Parse("2001:db8::1/65");
        var supernetExpected = IPNetwork2.Parse("2001:db8::/64");
        bool result = network1.TrySupernet(network2, out IPNetwork2 supernet);

        Assert.AreEqual(supernetExpected, supernet, "supernet");
        Assert.IsTrue(result, "parsed");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTrySupernet8()
    {
        var network1 = IPNetwork2.Parse("2001:db0::/64");
        var network2 = IPNetwork2.Parse("2001:dbf::/64");
        IPNetwork2 supernetExpected = null;
        bool result = network1.TrySupernet(network2, out IPNetwork2 supernet);

        Assert.AreEqual(supernetExpected, supernet, "supernet");
        Assert.IsFalse(result, "parsed");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTrySupernet9()
    {
        var network1 = IPNetwork2.Parse("192.168.1.1/24");
        var network2 = IPNetwork2.Parse("192.168.2.1/24");
        IPNetwork2[] network3 = [network1, network2];
        IPNetwork2[] supernetExpected = [network1, network2];
        bool result = IPNetwork2.TrySupernet(network3, out IPNetwork2[] supernet);

        Assert.AreEqual(supernetExpected[0], supernet[0], "supernet");
        Assert.AreEqual(supernetExpected[1], supernet[1], "supernet");
        Assert.IsTrue(result, "parsed");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTrySupernet10()
    {
        var network1 = IPNetwork2.Parse("2001:db8:0000::/65");
        var network2 = IPNetwork2.Parse("2001:db8:0:0:8000::/65");
        IPNetwork2[] network3 = [network1, network2];
        IPNetwork2[] supernetExpected = [IPNetwork2.Parse("2001:db8::/64")];
        bool result = IPNetwork2.TrySupernet(network3, out IPNetwork2[] supernet);

        Assert.AreEqual(supernetExpected[0], supernet[0], "supernet");
        Assert.IsTrue(result, "parsed");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTrySupernet11()
    {
        IPNetwork2[] network3 = null;
        _ = new[] { IPNetwork2.Parse("2001:db8::/64") };
        bool result = IPNetwork2.TrySupernet(network3, out IPNetwork2[] supernet);

        Assert.IsNull(supernet, "supernet");
        Assert.IsFalse(result, "parsed");
    }
}
