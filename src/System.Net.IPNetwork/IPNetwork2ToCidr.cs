// <copyright file="IPNetwork2ToCidr.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Net.Sockets;
using System.Numerics;

/// <summary>
/// ToCidr.
/// </summary>
public sealed partial class IPNetwork2
{
    /// <summary>
    /// Convert netmask to CIDR
    ///  255.255.255.0 -> 24
    ///  255.255.0.0   -> 16
    ///  255.0.0.0     -> 8.
    /// </summary>
    /// <param name="netmask">An IP Address representing the CIDR to convert.</param>
    /// <returns>A byte representing the CIDR converted from the netmask.</returns>
    public static byte ToCidr(IPAddress netmask)
    {
        bool cidrified = InternalToCidr(false, netmask, out byte cidr);
        if (!cidrified)
        {
            throw new ArgumentException(nameof(netmask));
        }
        return cidr;
    }

    /// <summary>
    /// Convert netmask to CIDR
    ///  255.255.255.0 -> 24
    ///  255.255.0.0   -> 16
    ///  255.0.0.0     -> 8.
    /// </summary>
    /// <param name="netmask">An IPAddress representing the CIDR to convert.</param>
    /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
    /// <returns>true if netmask was converted successfully; otherwise, false.</returns>
    public static bool TryToCidr(IPAddress netmask, out byte cidr)
    {
        return InternalToCidr(true, netmask, out cidr);
    }

    private static bool InternalToCidr(bool tryParse, IPAddress netmask, out byte cidr)
    {
        if (netmask == null)
        {
            if (!tryParse)
            {
                throw new ArgumentNullException(nameof(netmask));
            }

            cidr = 0;
            return false;
        }

        TryToBigInteger(netmask, out BigInteger uintNetmask2);
        bool cidrified = InternalToCidr(tryParse, uintNetmask2, netmask.AddressFamily, out cidr);
        return cidrified;
    }

    /// <summary>
    /// Convert netmask to CIDR
    ///  255.255.255.0 -> 24
    ///  255.255.0.0   -> 16
    ///  255.0.0.0     -> 8.
    /// </summary>
    /// <param name="tryParse">Whether to throw exception or not during conversion.</param>
    /// <param name="netmask">A number representing the netmask to convert.</param>
    /// <param name="family">Either IPv4 or IPv6.</param>
    /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
    private static bool InternalToCidr(bool tryParse, BigInteger netmask, AddressFamily family, out byte cidr)
    {
        if (!InternalValidNetmask(netmask, family))
        {
            if (!tryParse)
            {
                throw new ArgumentException(nameof(netmask));
            }

            cidr = 0;
            return false;
        }

        byte cidr2 = BitsSet(netmask);
        cidr = cidr2;
        return true;
    }
}