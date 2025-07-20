// <copyright file="IPNetworkV6ParseIpIpTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// ParseIpIp.
/// </summary>
[TestClass]
public class IPNetworkV6ParseIpIpTests
{
    /// <summary>
    /// Test ParseString with IP.
    /// </summary>
    [TestMethod]
    public void ParseIpIp1()
    {
        string ipaddress = "2001:0db8::";
        string netmask = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:fff0";
        var ip = IPAddress.Parse(ipaddress);
        var netm = IPAddress.Parse(netmask);
        var ipnetwork = IPNetwork2.Parse(ip, netm);
        Assert.AreEqual("2001:db8::/124", ipnetwork.ToString(), "network");
    }

    /// <summary>
    /// Test ParseString with IP.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ParseIpIp3()
    {
        string ipaddress = "2001:0db8::";
        var ip = IPAddress.Parse(ipaddress);
        IPAddress netm = null;
        IPNetwork2.Parse(ip, netm);
    }
}
