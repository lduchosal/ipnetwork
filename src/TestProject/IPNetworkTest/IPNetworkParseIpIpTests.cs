// <copyright file="IPNetworkParseIpIpTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

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

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ParseIpIp2()
    {
            IPAddress ip = null;
            IPAddress netm = null;
            var ipnetwork = IPNetwork2.Parse(ip, netm);
        }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ParseIpIp3()
    {
            string ipaddress = "192.168.168.100";
            var ip = IPAddress.Parse(ipaddress);
            IPAddress netm = null;
            var ipnetwork = IPNetwork2.Parse(ip, netm);
        }
}