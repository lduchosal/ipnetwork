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
    /// <param name="tryParse">Whether to throw exception or not during conversion.</param>
    /// <param name="netmask">A number representing the netmask to convert.</param>
    /// <param name="family">Either IPv4 or IPv6.</param>
    /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
    private static void InternalToCidr(bool tryParse, BigInteger netmask, AddressFamily family, out byte? cidr)
    {
        if (!IPNetwork2.InternalValidNetmask(netmask, family))
        {
            if (tryParse == false)
            {
                throw new ArgumentException("netmask");
            }

            cidr = null;
            return;
        }

        byte cidr2 = IPNetwork2.BitsSet(netmask, family);
        cidr = cidr2;

        return;
    }

    /// <summary>
    /// Convert netmask to CIDR
    ///  255.255.255.0 -> 24
    ///  255.255.0.0   -> 16
    ///  255.0.0.0     -> 8.
    /// </summary>
    /// <param name="netmask">An IPAdress representing the CIDR to convert.</param>
    /// <returns>A byte representing the CIDR converted from the netmask.</returns>
    public static byte ToCidr(IPAddress netmask)
    {
        IPNetwork2.InternalToCidr(false, netmask, out byte? cidr);
        return (byte)cidr;
    }

    /// <summary>
    /// Convert netmask to CIDR
    ///  255.255.255.0 -> 24
    ///  255.255.0.0   -> 16
    ///  255.0.0.0     -> 8.
    /// </summary>
    /// <param name="netmask">An IPAdress representing the CIDR to convert.</param>
    /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
    /// <returns>true if netmask was converted successfully; otherwise, false.</returns>
    public static bool TryToCidr(IPAddress netmask, out byte? cidr)
    {
        IPNetwork2.InternalToCidr(true, netmask, out byte? cidr2);
        bool parsed = cidr2 != null;
        cidr = cidr2;
        return parsed;
    }

    private static void InternalToCidr(bool tryParse, IPAddress netmask, out byte? cidr)
    {
        if (netmask == null)
        {
            if (tryParse == false)
            {
                throw new ArgumentNullException("netmask");
            }

            cidr = null;
            return;
        }

        bool parsed = IPNetwork2.TryToBigInteger(netmask, out BigInteger? uintNetmask2);

        // 20180217 lduchosal
        // impossible to reach code.
        // if (parsed == false) {
        //     if (tryParse == false) {
        //         throw new ArgumentException("netmask");
        //     }
        //     cidr = null;
        //     return;
        // }
        var uintNetmask = (BigInteger)uintNetmask2;

        IPNetwork2.InternalToCidr(tryParse, uintNetmask, netmask.AddressFamily, out byte? cidr2);
        cidr = cidr2;

        return;
    }
}