// <copyright file="IPNetwork2Parse.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Net.Sockets;
using System.Numerics;

namespace System.Net;

/// <summary>
/// the parse methods.
/// </summary>
public partial class IPNetwork2
{
    /// <summary>
    /// 192.168.1.45 - 192.168.1.65
    /// 
    /// ```
    /// 192.168.1.45/32 (covers: 192.168.1.45)
    /// 192.168.1.46/31 (covers: 192.168.1.46 - 192.168.1.47)
    /// 192.168.1.48/28 (covers: 192.168.1.48 - 192.168.1.63)
    /// 192.168.1.64/31 (covers: 192.168.1.64 - 192.168.1.65)
    /// ```
    /// 
    /// </summary>
    /// <param name="range">A string containing an ip range to convert (192.168.1.45 - 192.168.1.65).</param>
    /// <param name="ipnetworks">An IPNetwork List equivalent to the network contained in the range</param>
    /// <returns>true if parse was successful, false if the parse failed.</returns>
    public static bool TryParseRange(string range, out IEnumerable<IPNetwork2> ipnetworks)
    {
        return InternalParseRange(true, range, out ipnetworks);
    }
    
    /// <summary>
    /// 192.168.1.45 - 192.168.1.65
    /// 
    /// ```
    /// 192.168.1.45/32 (covers: 192.168.1.45)
    /// 192.168.1.46/31 (covers: 192.168.1.46 - 192.168.1.47)
    /// 192.168.1.48/28 (covers: 192.168.1.48 - 192.168.1.63)
    /// 192.168.1.64/31 (covers: 192.168.1.64 - 192.168.1.65)
    /// ```
    /// 
    /// </summary>
    /// <param name="start">A string containing an ip range start (**192.168.1.45** - 192.168.1.65).</param>
    /// <param name="end">A string containing an ip range end (192.168.1.45 - **192.168.1.65**).</param>
    /// <param name="ipnetworks">An IPNetwork List equivalent to the network contained in the range</param>
    /// <returns>true if parse was successful, false if the parse failed.</returns>
    public static bool TryParseRange(string start, string end, out IEnumerable<IPNetwork2> ipnetworks)
    {
        return InternalParseRange(true, start, end, out ipnetworks);
    }

    /// <summary>
    /// 192.168.1.45 - 192.168.1.65
    ///
    /// ```
    /// 192.168.1.45/32 (covers: 192.168.1.45)
    /// 192.168.1.46/31 (covers: 192.168.1.46 - 192.168.1.47)
    /// 192.168.1.48/28 (covers: 192.168.1.48 - 192.168.1.63)
    /// 192.168.1.64/31 (covers: 192.168.1.64 - 192.168.1.65)
    /// ```
    ///
    /// </summary>
    /// <param name="range">A string containing an ip range to convert (192.168.1.45 - 192.168.1.65).</param>
    /// <returns>An IPNetwork List equivalent to the network contained in the range.</returns>
    public static IEnumerable<IPNetwork2> ParseRange(string range)
    {
        InternalParseRange(false, range, out IEnumerable<IPNetwork2> ipnetworks);
        return ipnetworks;
    }

    /// <summary>
    /// 192.168.1.45, 192.168.1.65
    ///
    /// ```
    /// 192.168.1.45/32 (covers: 192.168.1.45)
    /// 192.168.1.46/31 (covers: 192.168.1.46 - 192.168.1.47)
    /// 192.168.1.48/28 (covers: 192.168.1.48 - 192.168.1.63)
    /// 192.168.1.64/31 (covers: 192.168.1.64 - 192.168.1.65)
    /// ```
    /// </summary>
    /// <param name="start">A string containing a start range ip address.</param>
    /// <param name="end">A string containing a end range ip address.</param>
    /// <returns>An IPNetwork List equivalent to the network contained in the range.</returns>
    public static IEnumerable<IPNetwork2> ParseRange(string start, string end)
    {
        InternalParseRange(false, start, end, out IEnumerable<IPNetwork2> ipnetworks);
        return ipnetworks;
    }
    
    /// <summary>
    /// 192.168.1.45 - 192.168.1.65
    ///
    /// ```
    /// 192.168.1.45/32 (covers: 192.168.1.45)
    /// 192.168.1.46/31 (covers: 192.168.1.46 - 192.168.1.47)
    /// 192.168.1.48/28 (covers: 192.168.1.48 - 192.168.1.63)
    /// 192.168.1.64/31 (covers: 192.168.1.64 - 192.168.1.65)
    /// ```
    /// </summary>
    /// <param name="tryParse">Whether to throw exception or not during conversion.</param>
    /// <param name="start">A string containing a start range ip address.</param>
    /// <param name="end">A string containing a end range ip address.</param>
    /// <param name="ipnetworks">The resulting IPNetworks.</param>
    internal static bool InternalParseRange(bool tryParse, string start, string end, out IEnumerable<IPNetwork2> ipnetworks)
    {
        bool startParsed = IPAddress.TryParse(start, out IPAddress startIp);
        if (!startParsed)
        {
            if (!tryParse)
            {
                throw new ArgumentException("Invalid start IPAddress", nameof(start));
            }

            ipnetworks = null;
            return false;
        }

        bool endParsed = IPAddress.TryParse(end, out IPAddress endIp);
        if (!endParsed)
        {
            if (!tryParse)
            {
                throw new ArgumentException("Invalid end IPAddress", nameof(end));
            }

            ipnetworks = null;
            return false;
        }

        bool parsed = InternalParseRange(tryParse, startIp, endIp, out ipnetworks);
        return parsed;
    }

    /// <summary>
    /// Internal parse an IPNetwork2.
    /// </summary>
    /// <param name="tryParse">Prevent exception.</param>
    /// <param name="range">The network range parse.</param>
    /// <param name="ipnetworks">The resulting IPNetworks.</param>
    /// <exception cref="ArgumentNullException">When network is null.</exception>
    /// <exception cref="ArgumentException">When network is not valid.</exception>
    /// <returns>true if parsed, otherwise false</returns>
    internal static bool InternalParseRange(bool tryParse, string range, out IEnumerable<IPNetwork2> ipnetworks)
    {
        if (string.IsNullOrEmpty(range))
        {
            if (!tryParse)
            {
                throw new ArgumentNullException(nameof(range));
            }

            ipnetworks = null;
            return false;
        }

        string[] args = range.Split([' ', '-'], StringSplitOptions.RemoveEmptyEntries);
        if (args.Length == 2)
        {
            bool parsed3 = InternalParseRange(tryParse, args[0], args[1], out ipnetworks);
            return parsed3;
        }
        
        if (!tryParse)
        {
            throw new ArgumentOutOfRangeException(nameof(range));
        }
        ipnetworks = null;
        return false;
    }

    /// <summary>
    /// 192.168.168.100 255.255.255.0
    ///
    /// Network   : 192.168.168.0
    /// Netmask   : 255.255.255.0
    /// Cidr      : 24
    /// Start     : 192.168.168.1
    /// End       : 192.168.168.254
    /// Broadcast : 192.168.168.255.
    /// </summary>
    /// <param name="tryParse">Whether to throw exception or not during conversion.</param>
    /// <param name="start">A start range ip address.</param>
    /// <param name="end">An end range ip address.</param>
    /// <param name="ipnetworks">The resulting IPNetworks.</param>
    internal static bool InternalParseRange(bool tryParse, IPAddress start, IPAddress end, out IEnumerable<IPNetwork2> ipnetworks)
    {
        if (start == null)
        {
            if (!tryParse)
            {
                throw new ArgumentNullException(nameof(start));
            }

            ipnetworks = null;
            return false;
        }

        if (end == null)
        {
            if (!tryParse)
            {
                throw new ArgumentNullException(nameof(end));
            }
            ipnetworks = null;
            return false;
        }

        if (end.AddressFamily != start.AddressFamily)
        {
            if (!tryParse)
            {
                throw new ArgumentException(nameof(AddressFamily));
            }
            ipnetworks = null;
            return false;
        }

        var result = new List<IPNetwork2>();
    
        var startValue = ToBigInteger(start);
        var endValue = ToBigInteger(end);

        if (startValue > endValue)
        {
            throw new ArgumentException("Start IP must be less than or equal to end IP", nameof(end));
        }

        var addressFamily = start.AddressFamily;
        byte addressBits = addressFamily == AddressFamily.InterNetworkV6 ? (byte)128 : (byte)32;

        var current = startValue;
        while (current <= endValue)
        {
            // Find the largest CIDR block that starts at current and doesn't exceed endValue
            byte prefixLength = FindOptimalPrefixLength(current, endValue, addressBits);

            var network = new IPNetwork2(current, addressFamily, prefixLength);
            result.Add(network);
        
            // Move to the next IP after this block
            uint blockSize = (uint)(1 << (addressBits - prefixLength));
            current += blockSize;
        }
        
        ipnetworks = result;
        return true;
    }

    private static byte FindOptimalPrefixLength(BigInteger startIp, BigInteger endIp, int addressBits)
    {
        BigInteger remainingIps = endIp - startIp + 1;
    
        // Find the number of trailing zeros in startIp (alignment)
        int alignment = startIp.IsZero ? addressBits : CountTrailingZeros(startIp);
    
        // Find the largest power of 2 that fits in the remaining range
        int maxBlockSizeBits = remainingIps.IsZero ? 0 : GetHighestBitPosition(remainingIps);
    
        // Take the minimum of alignment and what fits in range
        int blockSizeBits = Math.Min(alignment, maxBlockSizeBits);
    
        // Convert to prefix length
        return (byte)(addressBits - blockSizeBits);
    }

    private static int CountTrailingZeros(BigInteger value)
    {
        if (value.IsZero) return 0;
    
        int count = 0;
        while ((value & BigInteger.One) == 0)
        {
            value >>= 1;
            count++;
        }
        return count;
    }

    private static int GetHighestBitPosition(BigInteger value)
    {
        if (value.IsZero) return 0;
    
        int position = 0;
        while (value > 1)
        {
            value >>= 1;
            position++;
        }
        return position;
    }
}