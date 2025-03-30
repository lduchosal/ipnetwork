// <copyright file="IPNetwork2BitsSet.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Net.Sockets;
using System.Numerics;

/// <summary>
/// BitSet.
/// </summary>
public sealed partial class IPNetwork2
{
    /// <summary>
    /// Count bits set to 1 in netmask.
    /// </summary>
    /// <param name="netmask">A number representing the netmask to count bits from.</param>
    /// <returns>The number of bytes set to 1.</returns>
    [CLSCompliant(false)]
    public static uint BitsSet(IPAddress netmask)
    {
        var uintNetmask = ToBigInteger(netmask);
        uint bits = BitsSet(uintNetmask, netmask.AddressFamily);

        return bits;
    }

    /// <summary>
    /// Count bits set to 1 in netmask.
    /// </summary>
    /// <see href="http://stackoverflow.com/questions/109023/best-algorithm-to-count-the-number-of-set-bits-in-a-32-bit-integer"/>
    /// <param name="netmask">A number representing the netmask to count bits from.</param>
    /// <param name="family">Either IPv4 or IPv6.</param>
    /// <returns>The number of bytes set to 1.</returns>
    private static byte BitsSet(BigInteger netmask, AddressFamily family)
    {
        string s = netmask.ToBinaryString();

        return (byte)s.Replace("0", string.Empty)
            .ToCharArray()
            .Length;
    }
}
