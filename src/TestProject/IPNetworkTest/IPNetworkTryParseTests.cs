// <copyright file="IPNetworkTryParseTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

[TestClass]
public class IPNetworkTryParseTests
{
    /// <summary>
    ///     Tests Try Parse functionality with Try Parse IPAddress Netmask ANE2.
    /// </summary>
    [TestMethod]
    public void TestTryParseIPAddressNetmaskAne2()
    {
            IPNetwork2 ipnet = null;
            IPAddress ip = null;
            bool parsed = IPNetwork2.TryParse(ip, ip, out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

    /// <summary>
    ///     Tests Try Parse functionality with Try Parse IPAddress Netmask ANE3.
    /// </summary>
    [TestMethod]
    public void TestTryParseIPAddressNetmaskAne3()
    {
            IPNetwork2 ipnet = null;
            bool parsed = IPNetwork2.TryParse(string.Empty, 0, out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

    /// <summary>
    ///     Tests Try Parse functionality with Try Parse IPAddress Netmask ANE4.
    /// </summary>
    [TestMethod]
    public void TestTryParseIPAddressNetmaskAne4()
    {
            IPNetwork2 ipnet = null;
            bool parsed = IPNetwork2.TryParse(null, 0, out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

    /// <summary>
    ///     Tests Try Parse functionality with Try Parse IPAddress Netmask ANE5.
    /// </summary>
    [TestMethod]
    public void TestTryParseIPAddressNetmaskAne5()
    {
            string n = null;
            IPNetwork2 ipnet = null;
            bool parsed = IPNetwork2.TryParse(n, n, out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

    /// <summary>
    ///     Tests Try Parse functionality with Try Parse IPAddress Netmask ANE6.
    /// </summary>
    [TestMethod]
    public void TestTryParseIPAddressNetmaskAne6()
    {
            IPNetwork2 ipnet = null;
            bool parsed = IPNetwork2.TryParse(IPAddress.Parse("10.10.10.10"), null, out ipnet);
            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

    /// <summary>
    ///     Tests Try Parse functionality with Try Parse IPAddress Netmask ANE7.
    /// </summary>
    [TestMethod]
    public void TestTryParseIPAddressNetmaskAne7()
    {
            IPNetwork2 ipnet = null;
            bool parsed = IPNetwork2.TryParse("0.0.0.0", netmask:null, out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

    /// <summary>
    ///     Tests Try Parse functionality with Try Parse IPAddress Netmask ANE8.
    /// </summary>
    [TestMethod]
    public void TestTryParseIPAddressNetmaskAne8()
    {
            IPNetwork2 ipnet = null;
            bool parsed = IPNetwork2.TryParse("x.x.x.x", "x.x.x.x", out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

    /// <summary>
    ///     Tests Try Parse functionality with Try Parse IPAddress Netmask ANE9.
    /// </summary>
    [TestMethod]
    public void TestTryParseIPAddressNetmaskAne9()
    {
            IPNetwork2 ipnet = null;
            bool parsed = IPNetwork2.TryParse("0.0.0.0", "x.x.x.x", out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

    /// <summary>
    ///     Tests Try Parse functionality with Try Parse IPAddress Netmask ANE10.
    /// </summary>
    [TestMethod]
    public void TestTryParseIPAddressNetmaskAne10()
    {
            IPNetwork2 ipnet = null;
            bool parsed = IPNetwork2.TryParse("x.x.x.x", 0, out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

    /// <summary>
    ///     Tests Try Parse functionality with Try Parse IPAddress Netmask ANE11.
    /// </summary>
    [TestMethod]
    public void TestTryParseIPAddressNetmaskAne11()
    {
            IPNetwork2 ipnet = null;
            bool parsed = IPNetwork2.TryParse("0.0.0.0", 33, out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

    /// <summary>
    ///     Tests Try Parse functionality with Try Parse IPAddress Netmask.
    /// </summary>
    [TestMethod]
    public void TestTryParseIPAddressNetmask()
    {
            IPNetwork2 ipnetwork = null;
            string ipaddress = "192.168.168.100";
            string netmask = "255.255.255.0";

            string network = "192.168.168.0";
            string broadcast = "192.168.168.255";
            string firstUsable = "192.168.168.1";
            string lastUsable = "192.168.168.254";
            byte cidr = 24;
            uint usable = 254;

            bool parsed = IPNetwork2.TryParse(ipaddress, netmask, out ipnetwork);
            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

    /// <summary>
    ///     Tests Try Parse functionality with Try Parse String1.
    /// </summary>
    [TestMethod]
    public void TestTryParseString1()
    {
            IPNetwork2 ipnetwork = null;
            string ipaddress = "192.168.168.100 255.255.255.0";

            string network = "192.168.168.0";
            string netmask = "255.255.255.0";
            string broadcast = "192.168.168.255";
            string firstUsable = "192.168.168.1";
            string lastUsable = "192.168.168.254";
            byte cidr = 24;
            uint usable = 254;

            bool parsed = IPNetwork2.TryParse(ipaddress, out ipnetwork);
            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

    /// <summary>
    ///     Tests Try Parse functionality with Try Parse String2.
    /// </summary>
    [TestMethod]
    public void TestTryParseString2()
    {
            IPNetwork2 ipnetwork = null;
            string ipaddress = "192.168.168.100/24";

            string network = "192.168.168.0";
            string netmask = "255.255.255.0";
            string broadcast = "192.168.168.255";
            string firstUsable = "192.168.168.1";
            string lastUsable = "192.168.168.254";
            byte cidr = 24;
            uint usable = 254;

            bool parsed = IPNetwork2.TryParse(ipaddress, out ipnetwork);
            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

    /// <summary>
    ///     Tests Try Parse functionality with Try Parse String3.
    /// </summary>
    [TestMethod]
    public void TestTryParseString3()
    {
            IPNetwork2 ipnetwork = null;
            string ipaddress = "0.0.0.0/0";

            string network = "0.0.0.0";
            string netmask = "0.0.0.0";
            string broadcast = "255.255.255.255";
            string firstUsable = "0.0.0.1";
            string lastUsable = "255.255.255.254";
            byte cidr = 0;
            uint usable = 4294967294;

            bool parsed = IPNetwork2.TryParse(ipaddress, out ipnetwork);
            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

    /// <summary>
    ///     Tests Try Parse functionality with Try Parse String4.
    /// </summary>
    [TestMethod]
    public void TestTryParseString4()
    {
            IPNetwork2 ipnetwork = null;
            string ipaddress = "0.0.0.0/32";

            string network = "0.0.0.0";
            string netmask = "255.255.255.255";
            string broadcast = "0.0.0.0";
            string firstUsable = "0.0.0.0";
            string lastUsable = "0.0.0.0";
            byte cidr = 32;
            uint usable = 0;

            bool parsed = IPNetwork2.TryParse(ipaddress, out ipnetwork);
            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

    /// <summary>
    ///     Tests Try Parse functionality with Try Parse String5.
    /// </summary>
    [TestMethod]
    public void TestTryParseString5()
    {
            IPNetwork2 ipnetwork = null;
            string ipaddress = "255.255.255.255/32";

            string network = "255.255.255.255";
            string netmask = "255.255.255.255";
            string broadcast = "255.255.255.255";
            string firstUsable = "255.255.255.255";
            string lastUsable = "255.255.255.255";
            byte cidr = 32;
            uint usable = 0;

            bool parsed = IPNetwork2.TryParse(ipaddress, out ipnetwork);
            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

    /// <summary>
    ///     Tests Try Parse functionality with Try Parse String AE1.
    /// </summary>
    [TestMethod]
    public void TestTryParseStringAe1()
    {
            string ipaddress = "garbage";
            IPNetwork2 ipnetwork = null;
            bool parsed = IPNetwork2.TryParse(ipaddress, out ipnetwork);
            Assert.AreEqual(false, parsed, "parsed");
        }

    /// <summary>
    ///     Tests Try Parse functionality with Try Parse String AE2.
    /// </summary>
    [TestMethod]
    public void TestTryParseStringAe2()
    {
            string ipaddress = "0.0.0.0 0.0.1.0";
            IPNetwork2 ipnetwork = null;
            bool parsed = IPNetwork2.TryParse(ipaddress, out ipnetwork);
            Assert.AreEqual(false, parsed, "parsed");
        }

    /// <summary>
    ///     Tests Try Parse functionality with Try Parse String ANE1.
    /// </summary>
    [TestMethod]
    public void TestTryParseStringAne1()
    {
            string ipaddress = null;
            IPNetwork2 ipnetwork = null;
            bool parsed = IPNetwork2.TryParse(ipaddress, out ipnetwork);
            Assert.AreEqual(false, parsed, "parsed");
        }
}