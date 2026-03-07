// <copyright file="IPNetworkIanaBlocksExtendedTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Tests for extended IANA reserved blocks (IPv4 and IPv6).
/// </summary>
[TestClass]
public class IPNetworkIanaBlocksExtendedTests
{
    /// <summary>
    /// IPv4 addresses that should be IANA reserved.
    /// </summary>
    [TestMethod]
    [DataRow("0.0.0.1", DisplayName = "This network (0.0.0.0/8)")]
    [DataRow("0.255.255.255", DisplayName = "This network last")]
    [DataRow("127.0.0.1", DisplayName = "Loopback (127.0.0.0/8)")]
    [DataRow("127.255.255.255", DisplayName = "Loopback last")]
    [DataRow("10.0.0.1", DisplayName = "RFC1918 Class A (10.0.0.0/8)")]
    [DataRow("169.254.1.1", DisplayName = "Link-local (169.254.0.0/16)")]
    [DataRow("172.16.0.1", DisplayName = "RFC1918 Class B (172.16.0.0/12)")]
    [DataRow("172.31.255.255", DisplayName = "RFC1918 Class B last")]
    [DataRow("192.0.0.1", DisplayName = "IETF Protocol (192.0.0.0/24)")]
    [DataRow("192.0.2.1", DisplayName = "TEST-NET-1 (192.0.2.0/24)")]
    [DataRow("192.168.0.1", DisplayName = "RFC1918 Class C (192.168.0.0/16)")]
    [DataRow("198.18.0.1", DisplayName = "Benchmarking (198.18.0.0/15)")]
    [DataRow("198.19.255.255", DisplayName = "Benchmarking last")]
    [DataRow("198.51.100.1", DisplayName = "TEST-NET-2 (198.51.100.0/24)")]
    [DataRow("203.0.113.1", DisplayName = "TEST-NET-3 (203.0.113.0/24)")]
    [DataRow("224.0.0.1", DisplayName = "Multicast (224.0.0.0/4)")]
    [DataRow("239.255.255.255", DisplayName = "Multicast last")]
    [DataRow("240.0.0.1", DisplayName = "Reserved (240.0.0.0/4)")]
    [DataRow("255.255.255.255", DisplayName = "Broadcast")]
    public void IsIANAReserved_IPv4_ReservedAddress_ReturnsTrue(string ipaddress)
    {
        var ip = IPAddress.Parse(ipaddress);
        Assert.IsTrue(IPNetwork2.IsIANAReserved(ip), $"{ipaddress} should be IANA reserved");
    }

    /// <summary>
    /// IPv4 addresses that should NOT be IANA reserved.
    /// </summary>
    [TestMethod]
    [DataRow("1.0.0.1", DisplayName = "Public 1.0.0.1")]
    [DataRow("8.8.8.8", DisplayName = "Google DNS")]
    [DataRow("100.64.0.1", DisplayName = "Shared address space")]
    [DataRow("185.199.108.153", DisplayName = "GitHub Pages")]
    public void IsIANAReserved_IPv4_PublicAddress_ReturnsFalse(string ipaddress)
    {
        var ip = IPAddress.Parse(ipaddress);
        Assert.IsFalse(IPNetwork2.IsIANAReserved(ip), $"{ipaddress} should not be IANA reserved");
    }

    /// <summary>
    /// IPv4 networks that should be IANA reserved.
    /// </summary>
    [TestMethod]
    [DataRow("127.0.0.0/8", DisplayName = "Loopback /8")]
    [DataRow("10.10.0.0/16", DisplayName = "RFC1918 subnet")]
    [DataRow("192.168.1.0/24", DisplayName = "RFC1918 Class C subnet")]
    [DataRow("224.0.0.0/24", DisplayName = "Multicast subnet")]
    [DataRow("169.254.0.0/24", DisplayName = "Link-local subnet")]
    [DataRow("192.0.2.0/24", DisplayName = "TEST-NET-1")]
    [DataRow("198.51.100.0/24", DisplayName = "TEST-NET-2")]
    [DataRow("203.0.113.0/24", DisplayName = "TEST-NET-3")]
    public void IsIANAReserved_IPv4_ReservedNetwork_ReturnsTrue(string network)
    {
        var ipnetwork = IPNetwork2.Parse(network);
        Assert.IsTrue(ipnetwork.IsIANAReserved(), $"{network} should be IANA reserved");
    }

    /// <summary>
    /// IPv4 networks that should NOT be IANA reserved.
    /// </summary>
    [TestMethod]
    [DataRow("8.8.8.0/24", DisplayName = "Google DNS /24")]
    [DataRow("1.0.0.0/8", DisplayName = "Public /8")]
    public void IsIANAReserved_IPv4_PublicNetwork_ReturnsFalse(string network)
    {
        var ipnetwork = IPNetwork2.Parse(network);
        Assert.IsFalse(ipnetwork.IsIANAReserved(), $"{network} should not be IANA reserved");
    }

    /// <summary>
    /// IPv6 addresses that should be IANA reserved.
    /// </summary>
    [TestMethod]
    [DataRow("::", DisplayName = "Unspecified ::/128")]
    [DataRow("::1", DisplayName = "Loopback ::1/128")]
    [DataRow("::ffff:192.168.1.1", DisplayName = "IPv4-mapped")]
    [DataRow("64:ff9b::1", DisplayName = "IPv4/IPv6 translation")]
    [DataRow("2001::1", DisplayName = "TEREDO")]
    [DataRow("2001:db8::1", DisplayName = "Documentation")]
    [DataRow("fd00::1", DisplayName = "Unique local (fd00::)")]
    [DataRow("fc00::1", DisplayName = "Unique local (fc00::)")]
    [DataRow("fe80::1", DisplayName = "Link-local")]
    [DataRow("ff02::1", DisplayName = "Multicast")]
    [DataRow("ff00::1", DisplayName = "Multicast (ff00::)")]
    public void IsIANAReserved_IPv6_ReservedAddress_ReturnsTrue(string ipaddress)
    {
        var ip = IPAddress.Parse(ipaddress);
        Assert.IsTrue(IPNetwork2.IsIANAReserved(ip), $"{ipaddress} should be IANA reserved");
    }

    /// <summary>
    /// IPv6 addresses that should NOT be IANA reserved.
    /// </summary>
    [TestMethod]
    [DataRow("2607:f8b0:4004:800::200e", DisplayName = "Google")]
    [DataRow("2606:4700:4700::1111", DisplayName = "Cloudflare DNS")]
    public void IsIANAReserved_IPv6_PublicAddress_ReturnsFalse(string ipaddress)
    {
        var ip = IPAddress.Parse(ipaddress);
        Assert.IsFalse(IPNetwork2.IsIANAReserved(ip), $"{ipaddress} should not be IANA reserved");
    }

    /// <summary>
    /// IPv6 networks that should be IANA reserved.
    /// </summary>
    [TestMethod]
    [DataRow("fd00::/48", DisplayName = "Unique local /48")]
    [DataRow("fe80::/64", DisplayName = "Link-local /64")]
    [DataRow("2001:db8::/48", DisplayName = "Documentation /48")]
    [DataRow("ff00::/12", DisplayName = "Multicast /12")]
    public void IsIANAReserved_IPv6_ReservedNetwork_ReturnsTrue(string network)
    {
        var ipnetwork = IPNetwork2.Parse(network);
        Assert.IsTrue(ipnetwork.IsIANAReserved(), $"{network} should be IANA reserved");
    }

    /// <summary>
    /// IPv6 networks that should NOT be IANA reserved.
    /// </summary>
    [TestMethod]
    [DataRow("2607:f8b0::/32", DisplayName = "Google /32")]
    [DataRow("2606:4700::/32", DisplayName = "Cloudflare /32")]
    public void IsIANAReserved_IPv6_PublicNetwork_ReturnsFalse(string network)
    {
        var ipnetwork = IPNetwork2.Parse(network);
        Assert.IsFalse(ipnetwork.IsIANAReserved(), $"{network} should not be IANA reserved");
    }

    /// <summary>
    /// Static properties return expected values.
    /// </summary>
    [TestMethod]
    [DataRow("0.0.0.0/8", DisplayName = "IANA_THIS_NETWORK")]
    [DataRow("127.0.0.0/8", DisplayName = "IANA_LOOPBACK")]
    [DataRow("169.254.0.0/16", DisplayName = "IANA_LINK_LOCAL")]
    [DataRow("192.0.0.0/24", DisplayName = "IANA_IETF_PROTOCOL")]
    [DataRow("192.0.2.0/24", DisplayName = "IANA_TEST_NET1")]
    [DataRow("198.18.0.0/15", DisplayName = "IANA_BENCHMARK")]
    [DataRow("198.51.100.0/24", DisplayName = "IANA_TEST_NET2")]
    [DataRow("203.0.113.0/24", DisplayName = "IANA_TEST_NET3")]
    [DataRow("224.0.0.0/4", DisplayName = "IANA_MULTICAST")]
    [DataRow("240.0.0.0/4", DisplayName = "IANA_RESERVED")]
    [DataRow("255.255.255.255/32", DisplayName = "IANA_BROADCAST")]
    public void StaticProperty_IPv4_ReturnsExpected(string expected)
    {
        var network = IPNetwork2.Parse(expected);
        Assert.IsTrue(network.IsIANAReserved(), $"{expected} should be IANA reserved");
    }

    /// <summary>
    /// Static IPv6 properties return expected values.
    /// </summary>
    [TestMethod]
    [DataRow("::/128", DisplayName = "IANA6_UNSPECIFIED")]
    [DataRow("::1/128", DisplayName = "IANA6_LOOPBACK")]
    [DataRow("::ffff:0:0/96", DisplayName = "IANA6_IPV4_MAPPED")]
    [DataRow("64:ff9b::/96", DisplayName = "IANA6_IPV4_TRANSLATION")]
    [DataRow("2001::/32", DisplayName = "IANA6_TEREDO")]
    [DataRow("2001:db8::/32", DisplayName = "IANA6_DOCUMENTATION")]
    [DataRow("fc00::/7", DisplayName = "IANA6_UNIQUE_LOCAL")]
    [DataRow("fe80::/10", DisplayName = "IANA6_LINK_LOCAL")]
    [DataRow("ff00::/8", DisplayName = "IANA6_MULTICAST")]
    public void StaticProperty_IPv6_ReturnsExpected(string expected)
    {
        var network = IPNetwork2.Parse(expected);
        Assert.IsTrue(network.IsIANAReserved(), $"{expected} should be IANA reserved");
    }
}
