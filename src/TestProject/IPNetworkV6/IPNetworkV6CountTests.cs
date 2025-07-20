// <copyright file="IPNetworkV6CountTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// Count.
/// </summary>
[TestClass]
public class IPNetworkV6CountTests
{
    /// <summary>
    /// Test Total32.
    /// </summary>
    [TestMethod]
    public void Total32()
    {
        var network = IPNetwork2.Parse("::/128");
        int total = 1;
        Assert.AreEqual(total, network.Total, "Total");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Total31()
    {
        var network = IPNetwork2.Parse("::/127");
        int total = 2;
        Assert.AreEqual(total, network.Total, "Total");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Total30()
    {
        var network = IPNetwork2.Parse("::/126");
        int total = 4;
        Assert.AreEqual(total, network.Total, "Total");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Total24()
    {
        var network = IPNetwork2.Parse("::/120");
        int total = 256;
        Assert.AreEqual(total, network.Total, "Total");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Total16()
    {
        var network = IPNetwork2.Parse("::/112");
        int total = 65536;
        Assert.AreEqual(total, network.Total, "Total");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Total8()
    {
        var network = IPNetwork2.Parse("::/104");
        int total = 16777216;
        Assert.AreEqual(total, network.Total, "Total");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Total0()
    {
        var network = IPNetwork2.Parse("::/0");
        var total = BigInteger.Pow(2, 128);
        Assert.AreEqual(total, network.Total, "Total");
    }
}
