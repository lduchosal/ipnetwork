// <copyright file="IPNetwork2IANAblock.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Net.Sockets;

/// <summary>
/// IANA Blocks.
/// </summary>
public sealed partial class IPNetwork2
{
    // IPv4 RFC 1918 Private Address Blocks
    private static readonly Lazy<IPNetwork2> IanaAblockReserved = new (() => Parse("10.0.0.0/8"));
    private static readonly Lazy<IPNetwork2> IanaBblockReserved = new (() => Parse("172.16.0.0/12"));
    private static readonly Lazy<IPNetwork2> IanaCblockReserved = new (() => Parse("192.168.0.0/16"));

    // IPv4 Additional IANA Reserved Blocks
    private static readonly Lazy<IPNetwork2> IanaThisNetwork = new (() => Parse("0.0.0.0/8"));
    private static readonly Lazy<IPNetwork2> IanaLoopback = new (() => Parse("127.0.0.0/8"));
    private static readonly Lazy<IPNetwork2> IanaLinkLocal = new (() => Parse("169.254.0.0/16"));
    private static readonly Lazy<IPNetwork2> IanaIetfProtocol = new (() => Parse("192.0.0.0/24"));
    private static readonly Lazy<IPNetwork2> IanaTestNet1 = new (() => Parse("192.0.2.0/24"));
    private static readonly Lazy<IPNetwork2> IanaBenchmark = new (() => Parse("198.18.0.0/15"));
    private static readonly Lazy<IPNetwork2> IanaTestNet2 = new (() => Parse("198.51.100.0/24"));
    private static readonly Lazy<IPNetwork2> IanaTestNet3 = new (() => Parse("203.0.113.0/24"));
    private static readonly Lazy<IPNetwork2> IanaMulticast = new (() => Parse("224.0.0.0/4"));
    private static readonly Lazy<IPNetwork2> IanaReserved = new (() => Parse("240.0.0.0/4"));
    private static readonly Lazy<IPNetwork2> IanaBroadcast = new (() => Parse("255.255.255.255/32"));

    // IPv6 IANA Reserved Blocks
    private static readonly Lazy<IPNetwork2> Iana6Unspecified = new (() => Parse("::/128"));
    private static readonly Lazy<IPNetwork2> Iana6Loopback = new (() => Parse("::1/128"));
    private static readonly Lazy<IPNetwork2> Iana6Ipv4Mapped = new (() => Parse("::ffff:0:0/96"));
    private static readonly Lazy<IPNetwork2> Iana6Ipv4Translation = new (() => Parse("64:ff9b::/96"));
    private static readonly Lazy<IPNetwork2> Iana6Teredo = new (() => Parse("2001::/32"));
    private static readonly Lazy<IPNetwork2> Iana6Documentation = new (() => Parse("2001:db8::/32"));
    private static readonly Lazy<IPNetwork2> Iana6UniqueLocal = new (() => Parse("fc00::/7"));
    private static readonly Lazy<IPNetwork2> Iana6LinkLocal = new (() => Parse("fe80::/10"));
    private static readonly Lazy<IPNetwork2> Iana6Multicast = new (() => Parse("ff00::/8"));

    /// <summary>
    /// Gets 10.0.0.0/8.
    /// </summary>
    /// <returns>The IANA reserved IPNetwork 10.0.0.0/8.</returns>
    public static IPNetwork2 IANA_ABLK_RESERVED1 => IanaAblockReserved.Value;

    /// <summary>
    /// Gets 172.16.0.0/12.
    /// </summary>
    /// <returns>The IANA reserved IPNetwork 172.16.0.0/12.</returns>
    public static IPNetwork2 IANA_BBLK_RESERVED1 => IanaBblockReserved.Value;

    /// <summary>
    /// Gets 192.168.0.0/16.
    /// </summary>
    /// <returns>The IANA reserved IPNetwork 192.168.0.0/16.</returns>
    public static IPNetwork2 IANA_CBLK_RESERVED1 => IanaCblockReserved.Value;

    /// <summary>
    /// Gets 0.0.0.0/8 (This network).
    /// </summary>
    public static IPNetwork2 IANA_THIS_NETWORK => IanaThisNetwork.Value;

    /// <summary>
    /// Gets 127.0.0.0/8 (Loopback).
    /// </summary>
    public static IPNetwork2 IANA_LOOPBACK => IanaLoopback.Value;

    /// <summary>
    /// Gets 169.254.0.0/16 (Link-local).
    /// </summary>
    public static IPNetwork2 IANA_LINK_LOCAL => IanaLinkLocal.Value;

    /// <summary>
    /// Gets 192.0.0.0/24 (IETF Protocol Assignments).
    /// </summary>
    public static IPNetwork2 IANA_IETF_PROTOCOL => IanaIetfProtocol.Value;

    /// <summary>
    /// Gets 192.0.2.0/24 (Documentation TEST-NET-1).
    /// </summary>
    public static IPNetwork2 IANA_TEST_NET1 => IanaTestNet1.Value;

    /// <summary>
    /// Gets 198.18.0.0/15 (Benchmarking).
    /// </summary>
    public static IPNetwork2 IANA_BENCHMARK => IanaBenchmark.Value;

    /// <summary>
    /// Gets 198.51.100.0/24 (Documentation TEST-NET-2).
    /// </summary>
    public static IPNetwork2 IANA_TEST_NET2 => IanaTestNet2.Value;

    /// <summary>
    /// Gets 203.0.113.0/24 (Documentation TEST-NET-3).
    /// </summary>
    public static IPNetwork2 IANA_TEST_NET3 => IanaTestNet3.Value;

    /// <summary>
    /// Gets 224.0.0.0/4 (Multicast).
    /// </summary>
    public static IPNetwork2 IANA_MULTICAST => IanaMulticast.Value;

    /// <summary>
    /// Gets 240.0.0.0/4 (Reserved).
    /// </summary>
    public static IPNetwork2 IANA_RESERVED => IanaReserved.Value;

    /// <summary>
    /// Gets 255.255.255.255/32 (Broadcast).
    /// </summary>
    public static IPNetwork2 IANA_BROADCAST => IanaBroadcast.Value;

    /// <summary>
    /// Gets ::/128 (Unspecified).
    /// </summary>
    public static IPNetwork2 IANA6_UNSPECIFIED => Iana6Unspecified.Value;

    /// <summary>
    /// Gets ::1/128 (Loopback).
    /// </summary>
    public static IPNetwork2 IANA6_LOOPBACK => Iana6Loopback.Value;

    /// <summary>
    /// Gets ::ffff:0:0/96 (IPv4-mapped).
    /// </summary>
    public static IPNetwork2 IANA6_IPV4_MAPPED => Iana6Ipv4Mapped.Value;

    /// <summary>
    /// Gets 64:ff9b::/96 (IPv4/IPv6 translation).
    /// </summary>
    public static IPNetwork2 IANA6_IPV4_TRANSLATION => Iana6Ipv4Translation.Value;

    /// <summary>
    /// Gets 2001::/32 (TEREDO).
    /// </summary>
    public static IPNetwork2 IANA6_TEREDO => Iana6Teredo.Value;

    /// <summary>
    /// Gets 2001:db8::/32 (Documentation).
    /// </summary>
    public static IPNetwork2 IANA6_DOCUMENTATION => Iana6Documentation.Value;

    /// <summary>
    /// Gets fc00::/7 (Unique local).
    /// </summary>
    public static IPNetwork2 IANA6_UNIQUE_LOCAL => Iana6UniqueLocal.Value;

    /// <summary>
    /// Gets fe80::/10 (Link-local).
    /// </summary>
    public static IPNetwork2 IANA6_LINK_LOCAL => Iana6LinkLocal.Value;

    /// <summary>
    /// Gets ff00::/8 (Multicast).
    /// </summary>
    public static IPNetwork2 IANA6_MULTICAST => Iana6Multicast.Value;

    /// <summary>
    /// return true if ipaddress is contained in
    /// any IANA reserved block (IPv4 or IPv6).
    /// </summary>
    /// <param name="ipaddress">An IP address to check.</param>
    /// <returns>true if ipaddress is in an IANA reserved block; otherwise, false.</returns>
    public static bool IsIANAReserved(IPAddress ipaddress)
    {
        if (ipaddress == null)
        {
            throw new ArgumentNullException(nameof(ipaddress));
        }

        if (ipaddress.AddressFamily == AddressFamily.InterNetworkV6)
        {
            return IsIANAReservedIPv6(ipaddress);
        }

        return IsIANAReservedIPv4(ipaddress);
    }

    /// <summary>
    /// return true if ipnetwork is contained in
    /// any IANA reserved block (IPv4 or IPv6).
    /// </summary>
    /// <param name="ipnetwork">The IPNetwork to test.</param>
    /// <returns>true if the ipnetwork is in an IANA reserved block; otherwise, false.</returns>
    [Obsolete("static IsIANAReserved(IPNetwork2) is deprecated, please use instance IsIANAReserved.")]
    public static bool IsIANAReserved(IPNetwork2 ipnetwork)
    {
        if (ipnetwork == null)
        {
            throw new ArgumentNullException(nameof(ipnetwork));
        }

        return ipnetwork.IsIANAReserved();
    }

    /// <summary>
    /// return true if this ipnetwork is contained in
    /// any IANA reserved block (IPv4 or IPv6).
    /// </summary>
    /// <returns>true if the ipnetwork is in an IANA reserved block; otherwise, false.</returns>
    public bool IsIANAReserved()
    {
        if (this.family == AddressFamily.InterNetworkV6)
        {
            return IsIANAReservedIPv6(this);
        }

        return IsIANAReservedIPv4(this);
    }

    private static bool IsIANAReservedIPv4(IPAddress ipaddress)
    {
        return IANA_THIS_NETWORK.Contains(ipaddress)
               || IANA_LOOPBACK.Contains(ipaddress)
               || IANA_ABLK_RESERVED1.Contains(ipaddress)
               || IANA_LINK_LOCAL.Contains(ipaddress)
               || IANA_BBLK_RESERVED1.Contains(ipaddress)
               || IANA_IETF_PROTOCOL.Contains(ipaddress)
               || IANA_TEST_NET1.Contains(ipaddress)
               || IANA_CBLK_RESERVED1.Contains(ipaddress)
               || IANA_BENCHMARK.Contains(ipaddress)
               || IANA_TEST_NET2.Contains(ipaddress)
               || IANA_TEST_NET3.Contains(ipaddress)
               || IANA_MULTICAST.Contains(ipaddress)
               || IANA_RESERVED.Contains(ipaddress)
               || IANA_BROADCAST.Contains(ipaddress);
    }

    private static bool IsIANAReservedIPv4(IPNetwork2 ipnetwork)
    {
        return IANA_THIS_NETWORK.Contains(ipnetwork)
               || IANA_LOOPBACK.Contains(ipnetwork)
               || IANA_ABLK_RESERVED1.Contains(ipnetwork)
               || IANA_LINK_LOCAL.Contains(ipnetwork)
               || IANA_BBLK_RESERVED1.Contains(ipnetwork)
               || IANA_IETF_PROTOCOL.Contains(ipnetwork)
               || IANA_TEST_NET1.Contains(ipnetwork)
               || IANA_CBLK_RESERVED1.Contains(ipnetwork)
               || IANA_BENCHMARK.Contains(ipnetwork)
               || IANA_TEST_NET2.Contains(ipnetwork)
               || IANA_TEST_NET3.Contains(ipnetwork)
               || IANA_MULTICAST.Contains(ipnetwork)
               || IANA_RESERVED.Contains(ipnetwork)
               || IANA_BROADCAST.Contains(ipnetwork);
    }

    private static bool IsIANAReservedIPv6(IPAddress ipaddress)
    {
        return IANA6_UNSPECIFIED.Contains(ipaddress)
               || IANA6_LOOPBACK.Contains(ipaddress)
               || IANA6_IPV4_MAPPED.Contains(ipaddress)
               || IANA6_IPV4_TRANSLATION.Contains(ipaddress)
               || IANA6_TEREDO.Contains(ipaddress)
               || IANA6_DOCUMENTATION.Contains(ipaddress)
               || IANA6_UNIQUE_LOCAL.Contains(ipaddress)
               || IANA6_LINK_LOCAL.Contains(ipaddress)
               || IANA6_MULTICAST.Contains(ipaddress);
    }

    private static bool IsIANAReservedIPv6(IPNetwork2 ipnetwork)
    {
        return IANA6_UNSPECIFIED.Contains(ipnetwork)
               || IANA6_LOOPBACK.Contains(ipnetwork)
               || IANA6_IPV4_MAPPED.Contains(ipnetwork)
               || IANA6_IPV4_TRANSLATION.Contains(ipnetwork)
               || IANA6_TEREDO.Contains(ipnetwork)
               || IANA6_DOCUMENTATION.Contains(ipnetwork)
               || IANA6_UNIQUE_LOCAL.Contains(ipnetwork)
               || IANA6_LINK_LOCAL.Contains(ipnetwork)
               || IANA6_MULTICAST.Contains(ipnetwork);
    }
}
