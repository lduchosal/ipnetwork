// <copyright file="IPNetworkParseIpIpTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test parse ip.
/// </summary>
[TestClass]
public class IPNetworkParseIpIpTests
{
    /// <summary>
    ///     Tests Parse Ip Ip functionality with a /1 network.
    /// </summary>
    [TestMethod]
    public void ParseIpIp1()
    {
            string ipaddress = "192.168.168.100";
            string netmask = "255.255.255.0";
            var ip = IPAddress.Parse(ipaddress);
            var netm = IPAddress.Parse(netmask);
            var ipnetwork = IPNetwork2.Parse(ip, netm);
            Assert.AreEqual("192.168.168.0/24", ipnetwork.ToString(), "network");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void ParseIpIp2()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            IPAddress ip = null;
            IPAddress netm = null;
            IPNetwork2.Parse(ip, netm);
        });
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void ParseIpIp3()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            string ipaddress = "192.168.168.100";
            var ip = IPAddress.Parse(ipaddress);
            IPAddress netm = null;
            IPNetwork2.Parse(ip, netm);
        });
    }
}