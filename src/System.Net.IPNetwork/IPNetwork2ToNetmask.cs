// <copyright file="IPNetwork2ToNetmask.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Net.Sockets;
using System.Numerics;

/// <summary>
/// ToNetmask.
/// </summary>
public sealed partial class IPNetwork2
{
    /// <summary>
    /// Convert CIDR to netmask
    ///  24 -> 255.255.255.0
    ///  16 -> 255.255.0.0
    ///  8 -> 255.0.0.0.
    /// </summary>
    /// <see href="http://snipplr.com/view/15557/cidr-class-for-ipv4/"/>
    /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
    /// <param name="family">Either IPv4 or IPv6.</param>
    /// <returns>An IPAdress representing cidr.</returns>
    public static IPAddress ToNetmask(byte cidr, AddressFamily family)
    {
        IPNetwork2.InternalToNetmask(false, cidr, family, out IPAddress netmask);

        return netmask;
    }

    /// <summary>
    /// Convert CIDR to netmask
    ///  24 -> 255.255.255.0
    ///  16 -> 255.255.0.0
    ///  8 -> 255.0.0.0.
    /// </summary>
    /// <see href="http://snipplr.com/view/15557/cidr-class-for-ipv4/"/>
    /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
    /// <param name="family">Either IPv4 or IPv6.</param>
    /// <param name="netmask">The resulting netmask.</param>
    /// <returns>true if cidr was converted successfully; otherwise, false.</returns>
    public static bool TryToNetmask(byte cidr, AddressFamily family, out IPAddress netmask)
    {
        IPNetwork2.InternalToNetmask(true, cidr, family, out IPAddress netmask2);
        bool parsed = netmask2 != null;
        netmask = netmask2;

        return parsed;
    }

    /// <summary>
    /// Converts a CIDR value to its corresponding IPAddress netmask.
    /// </summary>
    /// <param name="tryParse">If true, handles errors silently; otherwise, throws exceptions.</param>
    /// <param name="cidr">The CIDR value to convert.</param>
    /// <param name="family">The address family (IPv4 or IPv6).</param>
    /// <param name="netmask">The resulting IPAddress netmask.</param>
    internal static void InternalToNetmask(bool tryParse, byte cidr, AddressFamily family, out IPAddress netmask)
    {
        if (family != AddressFamily.InterNetwork
            && family != AddressFamily.InterNetworkV6)
        {
            if (tryParse == false)
            {
                throw new ArgumentException("family");
            }

            netmask = null;
            return;
        }

        int maxCidr = family == Sockets.AddressFamily.InterNetwork ? 32 : 128;
        if (cidr > maxCidr)
        {
            if (tryParse == false)
            {
                throw new ArgumentOutOfRangeException("cidr");
            }

            netmask = null;
            return;
        }

        BigInteger mask = IPNetwork2.ToUint(cidr, family);
        var netmask2 = IPNetwork2.ToIPAddress(mask, family);
        netmask = netmask2;

        return;
    }
}