// <copyright file="IPNetwork2ValidNetmask.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Net.Sockets;
using System.Numerics;

/// <summary>
/// ValidNetmask.
/// </summary>
public sealed partial class IPNetwork2
{
    /// <summary>
    /// return true if netmask is a valid netmask
    /// 255.255.255.0, 255.0.0.0, 255.255.240.0, ...
    /// </summary>
    /// <see href="http://www.actionsnip.com/snippets/tomo_atlacatl/calculate-if-a-netmask-is-valid--as2-"/>
    /// <param name="netmask">A number representing the netmask to validate.</param>
    /// <returns>true if netmask is a valid IP Netmask; otherwise, false.</returns>
    public static bool ValidNetmask(IPAddress netmask)
    {
        if (netmask == null)
        {
            throw new ArgumentNullException(nameof(netmask));
        }

        var uintNetmask = ToBigInteger(netmask);
        bool valid = InternalValidNetmask(uintNetmask, netmask.AddressFamily);

        return valid;
    }

    /// <summary>
    /// Determines whether a given BigInteger netmask is valid for the specified address family.
    /// </summary>
    /// <param name="netmask">The netmask represented as a BigInteger.</param>
    /// <param name="family">The address family (IPv4 or IPv6).</param>
    /// <returns>
    /// <c>true</c> if the netmask is valid; otherwise, <c>false</c>.
    /// </returns>
    internal static bool InternalValidNetmask(BigInteger netmask, AddressFamily family)
    {
        if (family != AddressFamily.InterNetwork
            && family != AddressFamily.InterNetworkV6)
        {
            throw new ArgumentException("family");
        }

        BigInteger mask = family == AddressFamily.InterNetwork
            ? new BigInteger(0x0ffffffff)
            : new BigInteger([
                0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff,
                0x00
            ]);

        BigInteger neg = (~netmask) & mask;
        bool isNetmask = ((neg + 1) & neg) == 0;

        return isNetmask;
    }
}