// <copyright file="IPNetworkV6ToNetmaskTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// ToNetmask.
/// </summary>
public class IPNetworkV6ToNetmaskTests
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void ToNetmask128()
    {
        byte cidr = 128;
        string netmask = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        string result = IPNetwork2.ToNetmask(cidr, AddressFamily.InterNetworkV6).ToString();

        Assert.AreEqual(netmask, result, "netmask");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void ToNetmask31()
    {
        byte cidr = 127;
        string netmask = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:fffe";
        string result = IPNetwork2.ToNetmask(cidr, AddressFamily.InterNetworkV6).ToString();

        Assert.AreEqual(netmask, result, "netmask");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void ToNetmask30()
    {
        byte cidr = 126;
        string netmask = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:fffc";
        string result = IPNetwork2.ToNetmask(cidr, AddressFamily.InterNetworkV6).ToString();

        Assert.AreEqual(netmask, result, "netmask");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void ToNetmask1()
    {
        byte cidr = 1;
        string netmask = "8000::";
        string result = IPNetwork2.ToNetmask(cidr, AddressFamily.InterNetworkV6).ToString();

        Assert.AreEqual(netmask, result, "netmask");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void ToNetmask0()
    {
        byte cidr = 0;
        string netmask = "::";
        string result = IPNetwork2.ToNetmask(cidr, AddressFamily.InterNetworkV6).ToString();

        Assert.AreEqual(netmask, result, "netmask");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void ToNetmaskOore1()
    {
        byte cidr = 129;
        IPNetwork2.ToNetmask(cidr, AddressFamily.InterNetworkV6);
    }
}
