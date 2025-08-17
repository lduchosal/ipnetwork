// <copyright file="IPNetwork2ToUint.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Net.Sockets;
using System.Numerics;

/// <summary>
/// ToUint.
/// </summary>
public sealed partial class IPNetwork2
{
    /// <summary>
    /// Convert an IPAddress to decimal
    /// 0.0.0.0 -> 0
    /// 0.0.1.0 -> 256.
    /// </summary>
    /// <param name="ipaddress">A string containing an ip address to convert.</param>
    /// <returns>A number representing the ipaddress.</returns>
    public static BigInteger ToBigInteger(IPAddress ipaddress)
    {
        bool parsed = InternalToBigInteger(false, ipaddress, out BigInteger uintIpAddress);
        if (!parsed)
        {
            throw new ArgumentOutOfRangeException(nameof(ipaddress));
        }
        return uintIpAddress;
    }

    /// <summary>
    /// Convert an IPAddress to decimal
    /// 0.0.0.0 -> 0
    /// 0.0.1.0 -> 256.
    /// </summary>
    /// <param name="ipaddress">A string containing an ip address to convert.</param>
    /// <param name="uintIpAddress">A number representing the IPAddress.</param>
    /// <returns>true if ipaddress was converted successfully; otherwise, false.</returns>
    public static bool TryToBigInteger(IPAddress ipaddress, out BigInteger uintIpAddress)
    {
        bool parsed = InternalToBigInteger(true, ipaddress, out uintIpAddress);
        return parsed;
    }

    /// <summary>
    /// Convert a cidr to BigInteger netmask.
    /// </summary>
    /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
    /// <param name="family">Either IPv4 or IPv6.</param>
    /// <returns>A number representing the netmask in CIDR form.</returns>
    public static BigInteger ToUint(byte cidr, AddressFamily family)
    {
        bool parsed = InternalToBigInteger(false, cidr, family, out BigInteger uintNetmask);
        if (!parsed)
        {
            throw new ArgumentException(nameof(cidr));
        }
        return uintNetmask;
    }

    /// <summary>
    /// Convert a cidr to uint netmask.
    /// </summary>
    /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
    /// <param name="family">Either IPv4 or IPv6.</param>
    /// <param name="uintNetmask">A number representing the netmask.</param>
    /// <returns>true if cidr was converted successfully; otherwise, false.</returns>
    public static bool TryToUint(byte cidr, AddressFamily family, out BigInteger uintNetmask)
    {
        bool parsed = InternalToBigInteger(true, cidr, family, out uintNetmask);
        return parsed;
    }

    /// <summary>
    /// Convert a cidr to uint netmask.
    /// </summary>
    /// <param name="tryParse">Whether to throw exception or not during conversion.</param>
    /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
    /// <param name="family">Either IPv4 or IPv6.</param>
    /// <param name="uintNetmask">A number representing the netmask.</param>
    internal static bool InternalToBigInteger(bool tryParse, byte cidr, AddressFamily family, out BigInteger uintNetmask)
    {
        if ((family == AddressFamily.InterNetwork && cidr > 32)
            || (family == AddressFamily.InterNetworkV6 && cidr > 128))
        {
            if (!tryParse)
            {
                throw new ArgumentOutOfRangeException(nameof(cidr));
            }

            uintNetmask = 0;
            return false;
        }

        if (family != AddressFamily.InterNetwork
            && family != AddressFamily.InterNetworkV6)
        {
            if (!tryParse)
            {
                throw new NotSupportedException(family.ToString());
            }

            uintNetmask = 0;
            return false;
        }

        if (family == AddressFamily.InterNetwork)
        {
            uintNetmask = cidr == 0 ? 0 : 0xffffffff << (32 - cidr);
            return true;
        }

        var mask = new BigInteger([
            0xff, 0xff, 0xff, 0xff,
            0xff, 0xff, 0xff, 0xff,
            0xff, 0xff, 0xff, 0xff,
            0xff, 0xff, 0xff, 0xff,
            0x00
        ]);

        BigInteger masked = cidr == 0 ? 0 : mask << (128 - cidr);
        byte[] m = masked.ToByteArray();
        byte[] bmask = new byte[17];
        int copy = m.Length > 16 ? 16 : m.Length;
        Array.Copy(m, 0, bmask, 0, copy);
        uintNetmask = new BigInteger(bmask);
        return true;
    }

    /// <summary>
    /// Converts an IPAddress to a nullable BigInteger representation.
    /// </summary>
    /// <param name="tryParse">Indicates whether the method should handle errors silently (true) or throw exceptions (false).</param>
    /// <param name="ipaddress">The IPAddress to convert.</param>
    /// <param name="uintIpAddress">The resulting BigInteger representation of the IP address, or null if conversion fails and tryParse is true.</param>
    internal static bool InternalToBigInteger(bool tryParse, IPAddress ipaddress, out BigInteger uintIpAddress)
    {
        if (ipaddress == null)
        {
            if (!tryParse)
            {
                throw new ArgumentNullException(nameof(ipaddress));
            }

            uintIpAddress = default;
            return false;
        }

#if NETSTANDARD2_1
        byte[] bytes = ipaddress.AddressFamily == AddressFamily.InterNetwork ? new byte[4] : new byte[16];
        Span<byte> span = bytes.AsSpan();
        if (!ipaddress.TryWriteBytes(span, out _))
        {
            if (!tryParse)
            {
                throw new ArgumentException(nameof(ipaddress));
            }

            uintIpAddress = default;
            return false;
        }

        uintIpAddress = new BigInteger(span, isUnsigned: true, isBigEndian: true);
#elif NETSTANDARD20
        byte[] bytes = ipaddress.GetAddressBytes();
        bytes.AsSpan().Reverse();

        // add trailing 0 to make unsigned
        byte[] unsigned = new byte[bytes.Length + 1];
        Buffer.BlockCopy(bytes, 0, unsigned, 0, bytes.Length);
        uintIpAddress = new BigInteger(unsigned);
#else
        byte[] bytes = ipaddress.GetAddressBytes();
        Array.Reverse(bytes);

        // add trailing 0 to make unsigned
        byte[] unsigned = new byte[bytes.Length + 1];
        Buffer.BlockCopy(bytes, 0, unsigned, 0, bytes.Length);
        uintIpAddress = new BigInteger(unsigned);
#endif
        return true;
    }
}