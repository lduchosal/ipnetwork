// <copyright file="IPNetwork2ToNetmask.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Diagnostics.CodeAnalysis;
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
    /// <returns>An IPAddress representing cidr.</returns>
    public static IPAddress ToNetmask(byte cidr, AddressFamily family)
    {
        if (!InternalToNetmask(false, cidr, family, out IPAddress? netmask))
        {
            throw new ArgumentException(nameof(cidr));
        }

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
    public static bool TryToNetmask(byte cidr, AddressFamily family, [NotNullWhen(true)] out IPAddress? netmask)
    {
        return InternalToNetmask(true, cidr, family, out netmask);
    }

    /// <summary>
    /// Converts a CIDR value to its corresponding IPAddress netmask.
    /// </summary>
    /// <param name="tryParse">If true, handles errors silently; otherwise, throws exceptions.</param>
    /// <param name="cidr">The CIDR value to convert.</param>
    /// <param name="family">The address family (IPv4 or IPv6).</param>
    /// <param name="netmask">The resulting IPAddress netmask.</param>
    internal static bool InternalToNetmask(bool tryParse, byte cidr, AddressFamily family, [NotNullWhen(true)] out IPAddress? netmask)
    {
        if (family != AddressFamily.InterNetwork
            && family != AddressFamily.InterNetworkV6)
        {
            if (!tryParse)
            {
                throw new ArgumentException(nameof(family));
            }

            netmask = null;
            return false;
        }

        int maxCidr = family == AddressFamily.InterNetwork ? 32 : 128;
        if (cidr > maxCidr)
        {
            if (!tryParse)
            {
                throw new ArgumentOutOfRangeException(nameof(cidr));
            }

            netmask = null;
            return false;
        }

        BigInteger mask = ToUint(cidr, family);
        netmask = ToIPAddress(mask, family);
        return true;
    }
}