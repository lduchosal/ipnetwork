// <copyright file="IPNetworkV6UsableTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// Usable.
/// </summary>
[TestClass]
public class IPNetworkV6UsableTests
{
    /// <summary>
    /// Test Usable32.
    /// </summary>
    [TestMethod]
    public void Usable32()
    {
        var network = IPNetwork2.Parse("::/128");
        uint usable = 1;
        Assert.AreEqual(usable, network.Usable, "Usable");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Usable31()
    {
        var network = IPNetwork2.Parse("::/127");
        uint usable = 2;
        Assert.AreEqual(usable, network.Usable, "Usable");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Usable30()
    {
        var network = IPNetwork2.Parse("::/126");
        uint usable = 4;
        Assert.AreEqual(usable, network.Usable, "Usable");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Usable24()
    {
        var network = IPNetwork2.Parse("::/120");
        uint usable = 256;
        Assert.AreEqual(usable, network.Usable, "Usable");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Usable16()
    {
        var network = IPNetwork2.Parse("::/112");
        uint usable = 65536;
        Assert.AreEqual(usable, network.Usable, "Usable");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Usable8()
    {
        var network = IPNetwork2.Parse("::/104");
        uint usable = 16777216;
        Assert.AreEqual(usable, network.Usable, "Usable");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Usable0()
    {
        var network = IPNetwork2.Parse("::/0");
        var usable = BigInteger.Pow(2, 128);
        Assert.AreEqual(usable, network.Usable, "Usable");
    }
}
