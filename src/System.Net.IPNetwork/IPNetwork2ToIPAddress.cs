// <copyright file="IPNetwork2ToIPAddress.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Net.Sockets;
using System.Numerics;

/// <summary>
/// ToIPAddress.
/// </summary>
public sealed partial class IPNetwork2
{
    /// <summary>
    /// Transform a uint ipaddress into IPAddress object.
    /// </summary>
    /// <param name="ipaddress">A number representing an ip address to convert.</param>
    /// <param name="family">Either IPv4 or IPv6.</param>
    /// <returns>An IPAddress.</returns>
    public static IPAddress ToIPAddress(BigInteger ipaddress, AddressFamily family)
    {
        int width = family == AddressFamily.InterNetwork ? 4 : 16;
        byte[] bytes = ipaddress.ToByteArray();
        byte[] bytes2 = new byte[width];
        int copy = bytes.Length > width ? width : bytes.Length;
        Array.Copy(bytes, 0, bytes2, 0, copy);
        Array.Reverse(bytes2);

        byte[] sized = Resize(bytes2, family);
        var ip = new IPAddress(sized);
        return ip;
    }

    /// <summary>
    /// Resizes the given byte array to match the expected width for the specified address family (IPv4 or IPv6).
    /// Pads with zeros if the array is shorter than required.
    /// </summary>
    /// <param name="bytes">The byte array to resize.</param>
    /// <param name="family">The address family (IPv4 or IPv6).</param>
    /// <returns>A byte array resized to the appropriate length for the address family.</returns>
    internal static byte[] Resize(byte[] bytes, AddressFamily family)
    {
        if (family != AddressFamily.InterNetwork
            && family != AddressFamily.InterNetworkV6)
        {
            throw new ArgumentException(nameof(family));
        }

        int width = family == AddressFamily.InterNetwork ? 4 : 16;

        if (bytes.Length > width)
        {
            throw new ArgumentException(nameof(bytes));
        }

        byte[] result = new byte[width];
        Array.Copy(bytes, 0, result, 0, bytes.Length);

        return result;
    }
}