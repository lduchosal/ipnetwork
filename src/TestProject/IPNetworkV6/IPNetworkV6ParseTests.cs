// <copyright file="IPNetworkV6ParseTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// Parse.
/// </summary>
[TestClass]
public class IPNetworkV6ParseTests
{
    /// <summary>
    /// Test parse of IPv6 networks.
    /// </summary>
    /// <param name="withFirst">First parameter.</param>
    /// <param name="andSecond">Second parameter.</param>
    [DataTestMethod]
    [DataRow("xxxx::", "xxxx::")]
    [DataRow("::", "xxxx::")]
    [ExpectedException(typeof(ArgumentException))]
    public void TestParseIPAddressNetmaskAne8(string withFirst, string andSecond)
    {
        IPNetwork2.Parse(withFirst, andSecond);
    }

    /// <summary>
    /// Test parse of IPv6 networks.
    /// </summary>
    /// <param name="withFirst">First parameter.</param>
    /// <param name="andSecond">Second parameter.</param>
    [DataTestMethod]
    [DataRow("xxxx::", "0")]
    [DataRow("::", "129")]
    public void TestParseIPAddressNetmaskAne10(string withFirst, string andSecond)
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
        {
            IPNetwork2.Parse(withFirst, andSecond);
        });
    }

    /// <summary>
    /// Test parse of IPv6 networks with 128 cidr.
    /// </summary>
    [TestMethod]
    public void TestParsev6_128()
    {
        string ipaddress = "2001:db8::";
        string netmask = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";

        string network = "2001:db8::";
        string netmask2 = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";

        // string broadcast = "2001:db8::";
        string firstUsable = "2001:db8::";
        string lastUsable = "2001:db8::";
        byte cidr = 128;
        BigInteger usable = 1;

        bool parsed = IPNetwork2.TryParse(ipaddress, netmask, out IPNetwork2 ipnetwork);
        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask2, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.IsNull(ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test parse of IPv6 networks with 127 cidr.
    /// </summary>
    [TestMethod]
    public void TestParsev6_127()
    {
        string ipaddress = "2001:db8::";
        string netmask = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:fffe";

        string network = "2001:db8::";
        string netmask2 = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:fffe";

        // string broadcast = "2001:db8::1";
        string firstUsable = "2001:db8::";
        string lastUsable = "2001:db8::1";
        byte cidr = 127;
        BigInteger usable = 2;

        bool parsed = IPNetwork2.TryParse(ipaddress, netmask, out IPNetwork2 ipnetwork);
        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask2, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.IsNull(ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test parse of IPv6 networks with 126 cidr.
    /// </summary>
    [TestMethod]
    public void TestParsev6_126()
    {
        string ipaddress = "2001:db8::";
        string netmask = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:fffc";

        string network = "2001:db8::";
        string netmask2 = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:fffc";

        // string broadcast = "2001:db8::3";
        string firstUsable = "2001:db8::";
        string lastUsable = "2001:db8::3";
        byte cidr = 126;
        BigInteger usable = 4;

        bool parsed = IPNetwork2.TryParse(ipaddress, netmask, out IPNetwork2 ipnetwork);
        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask2, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.IsNull(ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test parse of IPv6 networks with 125 cidr.
    /// </summary>
    [TestMethod]
    public void TestParsev6_125()
    {
        string ipaddress = "2001:db8::";
        string netmask = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:fff8";

        string network = "2001:db8::";
        string netmask2 = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:fff8";

        // string broadcast = "2001:db8::7";
        string firstUsable = "2001:db8::";
        string lastUsable = "2001:db8::7";
        byte cidr = 125;
        BigInteger usable = 8;

        bool parsed = IPNetwork2.TryParse(ipaddress, netmask, out IPNetwork2 ipnetwork);
        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask2, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.IsNull(ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test parse of IPv6 networks with 124 cidr.
    /// </summary>
    [TestMethod]
    public void TestParsev6_124()
    {
        string ipaddress = "2001:db8::";
        string netmask = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:fff0";

        string network = "2001:db8::";
        string netmask2 = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:fff0";

        // string broadcast = "2001:db8::f";
        string firstUsable = "2001:db8::";
        string lastUsable = "2001:db8::f";
        byte cidr = 124;
        BigInteger usable = 16;

        bool parsed = IPNetwork2.TryParse(ipaddress, netmask, out IPNetwork2 ipnetwork);
        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask2, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.IsNull(ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test parse of IPv6 networks with 123 cidr.
    /// </summary>
    [TestMethod]
    public void TestParsev6_123()
    {
        string ipaddress = "2001:0db8::";
        string netmask = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffe0";

        string network = "2001:db8::";
        string netmask2 = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffe0";

        // string broadcast = "2001:db8::1f";
        string firstUsable = "2001:db8::";
        string lastUsable = "2001:db8::1f";
        byte cidr = 123;
        BigInteger usable = 32;

        bool parsed = IPNetwork2.TryParse(ipaddress, netmask, out IPNetwork2 ipnetwork);
        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask2, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.IsNull(ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test parse of IPv6 networks with 112 cidr.
    /// </summary>
    [TestMethod]
    public void TestParsev6_112()
    {
        string ipaddress = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        string netmask = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:0000";

        string network = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:0";
        string netmask2 = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:0";

        // string broadcast = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        string firstUsable = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:0";
        string lastUsable = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        byte cidr = 112;
        uint usable = 65536;

        bool parsed = IPNetwork2.TryParse(ipaddress, netmask, out IPNetwork2 ipnetwork);
        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask2, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.IsNull(ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test parse of IPv6 networks with 64 cidr.
    /// </summary>
    [TestMethod]
    public void TestParsev6_64()
    {
        string ipaddress = "ffff:ffff:ffff:ffff:1234:1234:1234:1234";
        string netmask = "ffff:ffff:ffff:ffff:0000:0000:0000:0000";

        string network = "ffff:ffff:ffff:ffff::";
        string netmask2 = "ffff:ffff:ffff:ffff::";

        // string broadcast = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        string firstUsable = "ffff:ffff:ffff:ffff::";
        string lastUsable = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        byte cidr = 64;
        var usable = BigInteger.Pow(2, 128 - cidr);

        bool parsed = IPNetwork2.TryParse(ipaddress, netmask, out IPNetwork2 ipnetwork);
        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask2, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.IsNull(ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test parse of IPv6 networks with 16 cidr.
    /// </summary>
    [TestMethod]
    public void TestParsev6_16()
    {
        string ipaddress = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        string netmask = "ffff:0000:0000:0000:0000:0000:0000:0000";

        string network = "ffff::";
        string netmask2 = "ffff::";

        // string broadcast = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        string firstUsable = "ffff::";
        string lastUsable = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        byte cidr = 16;
        var usable = BigInteger.Pow(2, 128 - cidr);

        bool parsed = IPNetwork2.TryParse(ipaddress, netmask, out IPNetwork2 ipnetwork);
        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask2, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.IsNull(ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test parse of IPv6 networks with edge cases.
    /// </summary>
    [TestMethod]
    public void TestParsev6_EDGE()
    {
        string ipaddress = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        string netmask = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";

        string network = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        string firstUsable = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        string lastUsable = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        byte cidr = 128;
        uint usable = 1;

        bool parsed = IPNetwork2.TryParse(ipaddress, netmask, out IPNetwork2 ipnetwork);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.IsNull(ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }
}
