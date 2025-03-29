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
    /// Convert an ipadress to decimal
    /// 0.0.0.0 -> 0
    /// 0.0.1.0 -> 256.
    /// </summary>
    /// <param name="ipaddress">A string containing an ip address to convert.</param>
    /// <returns>A number representing the ipaddress.</returns>
    public static BigInteger ToBigInteger(IPAddress ipaddress)
    {
        IPNetwork2.InternalToBigInteger(false, ipaddress, out BigInteger? uintIpAddress);

        return (BigInteger)uintIpAddress;
    }

    /// <summary>
    /// Convert an ipadress to decimal
    /// 0.0.0.0 -> 0
    /// 0.0.1.0 -> 256.
    /// </summary>
    /// <param name="ipaddress">A string containing an ip address to convert.</param>
    /// <param name="uintIpAddress">A number representing the IPAdress.</param>
    /// <returns>true if ipaddress was converted successfully; otherwise, false.</returns>
    public static bool TryToBigInteger(IPAddress ipaddress, out BigInteger? uintIpAddress)
    {
        IPNetwork2.InternalToBigInteger(true, ipaddress, out BigInteger? uintIpAddress2);
        bool parsed = uintIpAddress2 != null;
        uintIpAddress = uintIpAddress2;

        return parsed;
    }

    /// <summary>
    /// Converts an IPAddress to a nullable BigInteger representation.
    /// </summary>
    /// <param name="tryParse">Indicates whether the method should handle errors silently (true) or throw exceptions (false).</param>
    /// <param name="ipaddress">The IPAddress to convert.</param>
    /// <param name="uintIpAddress">The resulting BigInteger representation of the IP address, or null if conversion fails and tryParse is true.</param>
    internal static void InternalToBigInteger(bool tryParse, IPAddress ipaddress, out BigInteger? uintIpAddress)
    {
        if (ipaddress == null)
        {
            if (tryParse == false)
            {
                throw new ArgumentNullException("ipaddress");
            }

            uintIpAddress = null;
            return;
        }

#if NETSTANDARD2_1
        byte[] bytes = ipaddress.AddressFamily == AddressFamily.InterNetwork ? new byte[4] : new byte[16];
        Span<byte> span = bytes.AsSpan();
        if (!ipaddress.TryWriteBytes(span, out _))
        {
            if (tryParse == false)
            {
                throw new ArgumentException("ipaddress");
            }

            uintIpAddress = null;
            return;
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
    }

    /// <summary>
    /// Convert a cidr to BigInteger netmask.
    /// </summary>
    /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
    /// <param name="family">Either IPv4 or IPv6.</param>
    /// <returns>A number representing the netmask exprimed in CIDR.</returns>
    public static BigInteger ToUint(byte cidr, AddressFamily family)
    {
        IPNetwork2.InternalToBigInteger(false, cidr, family, out BigInteger? uintNetmask);

        return (BigInteger)uintNetmask;
    }

    /// <summary>
    /// Convert a cidr to uint netmask.
    /// </summary>
    /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
    /// <param name="family">Either IPv4 or IPv6.</param>
    /// <param name="uintNetmask">A number representing the netmask.</param>
    /// <returns>true if cidr was converted successfully; otherwise, false.</returns>
    public static bool TryToUint(byte cidr, AddressFamily family, out BigInteger? uintNetmask)
    {
        IPNetwork2.InternalToBigInteger(true, cidr, family, out BigInteger? uintNetmask2);
        bool parsed = uintNetmask2 != null;
        uintNetmask = uintNetmask2;

        return parsed;
    }

    /// <summary>
    /// Convert a cidr to uint netmask.
    /// </summary>
    /// <param name="tryParse">Whether to throw exception or not during conversion.</param>
    /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
    /// <param name="family">Either IPv4 or IPv6.</param>
    /// <param name="uintNetmask">A number representing the netmask.</param>
    internal static void InternalToBigInteger(bool tryParse, byte cidr, AddressFamily family, out BigInteger? uintNetmask)
    {
        if (family == AddressFamily.InterNetwork && cidr > 32)
        {
            if (tryParse == false)
            {
                throw new ArgumentOutOfRangeException("cidr");
            }

            uintNetmask = null;
            return;
        }

        if (family == AddressFamily.InterNetworkV6 && cidr > 128)
        {
            if (tryParse == false)
            {
                throw new ArgumentOutOfRangeException("cidr");
            }

            uintNetmask = null;
            return;
        }

        if (family != AddressFamily.InterNetwork
            && family != AddressFamily.InterNetworkV6)
        {
            if (tryParse == false)
            {
                throw new NotSupportedException(family.ToString());
            }

            uintNetmask = null;
            return;
        }

        if (family == AddressFamily.InterNetwork)
        {
            uintNetmask = cidr == 0 ? 0 : 0xffffffff << (32 - cidr);
            return;
        }

        var mask = new BigInteger(new byte[]
        {
            0xff, 0xff, 0xff, 0xff,
            0xff, 0xff, 0xff, 0xff,
            0xff, 0xff, 0xff, 0xff,
            0xff, 0xff, 0xff, 0xff,
            0x00,
        });

        BigInteger masked = cidr == 0 ? 0 : mask << (128 - cidr);
        byte[] m = masked.ToByteArray();
        byte[] bmask = new byte[17];
        int copy = m.Length > 16 ? 16 : m.Length;
        Array.Copy(m, 0, bmask, 0, copy);
        uintNetmask = new BigInteger(bmask);
    }
}