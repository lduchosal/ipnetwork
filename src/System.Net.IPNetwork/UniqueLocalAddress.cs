// <copyright file="UniqueLocalAddress.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace System.Net;

/// <summary>
/// Utility class for IPv6 Unique Local Address (ULA) generation and validation.
/// Implements RFC 4193 for generating ULA prefixes in the fd00::/8 range.
/// 
/// A locally-assigned ULA always looks like this (128 bits total):
/// | 8 bits  | 40 bits        | 16 bits    | 64 bits           |
/// +---------+----------------+-------------+-------------------+
/// | fd (8)  | Global ID      | Subnet ID   | Interface ID      |
///
/// •	fd = the fixed 8-bit prefix (fd00::/8 for locally assigned).
/// •	Global ID = 40 random bits, chosen once to make your ULA unique.
/// •	Subnet ID = 16 bits, chosen by you inside your site.
/// •	Interface ID = 64 bits, assigned to each host within the subnet (same rule as all IPv6).
/// 
/// The /48 prefix is the site prefix (fdXX:XXXX:XXXX::/48).
/// </summary>
public static class UniqueLocalAddress
{
    /// <summary>
    /// ULA prefix for locally assigned addresses (fd00::/8).
    /// </summary>
    public static readonly IPNetwork2 UlaLocallyAssigned = IPNetwork2.Parse("fd00::/8");

    /// <summary>
    /// ULA prefix for centrally assigned addresses (fc00::/8) - currently undefined.
    /// </summary>
    public static readonly IPNetwork2 UlaCentrallyAssigned = IPNetwork2.Parse("fc00::/8");

    /// <summary>
    /// Full ULA range (fc00::/7).
    /// </summary>
    public static readonly IPNetwork2 UlaRange = IPNetwork2.Parse("fc00::/7");

    /// <summary>
    /// Generates a random ULA /48 prefix using the algorithm from RFC 4193.
    /// </summary>
    /// <returns>A randomly generated ULA /48 network.</returns>
    public static IPNetwork2 GenerateUlaPrefix()
    {
        byte[] globalId = GenerateRandomGlobalId();
        return CreateUlaPrefix(globalId);
    }

    /// <summary>
    /// Generates a random ULA /48 prefix using a specific MAC address for entropy.
    /// </summary>
    /// <param name="macAddress">MAC address to use for entropy generation.</param>
    /// <returns>A ULA /48 network generated using the provided MAC address.</returns>
    public static IPNetwork2 GenerateUlaPrefix(byte[] macAddress)
    {
        if (macAddress == null)
        {
            throw new ArgumentNullException(nameof(macAddress));
        }

        if (macAddress.Length != 6)
        {
            throw new ArgumentException("MAC address must be 6 bytes long", nameof(macAddress));
        }

        byte[] globalId = GenerateGlobalIdFromMac(macAddress);
        return CreateUlaPrefix(globalId);
    }

    /// <summary>
    /// Generates a random ULA /48 prefix using a seed for deterministic generation.
    /// </summary>
    /// <param name="seed">Seed value for deterministic generation.</param>
    /// <returns>A ULA /48 network generated using the provided seed.</returns>
    public static IPNetwork2 GenerateUlaPrefix(string seed)
    {
        if (string.IsNullOrEmpty(seed))
        {
            throw new ArgumentNullException("Seed cannot be null or empty", nameof(seed));
        }

        byte[] globalId = GenerateGlobalIdFromSeed(seed);
        return CreateUlaPrefix(globalId);
    }

    /// <summary>
    /// Creates a ULA subnet within a ULA /48 prefix.
    /// </summary>
    /// <param name="ulaPrefix">The ULA /48 prefix.</param>
    /// <param name="subnetId">16-bit subnet identifier.</param>
    /// <returns>A ULA /64 subnet.</returns>
    public static IPNetwork2 CreateUlaSubnet(IPNetwork2 ulaPrefix, int subnetId)
    {
        if (!IsUlaPrefix(ulaPrefix))
        {
            throw new ArgumentException("Network must be a valid ULA prefix", nameof(ulaPrefix));
        }

        if (ulaPrefix.Cidr != 48)
        {
            throw new ArgumentException("ULA prefix must be /48", nameof(ulaPrefix));
        }
        
        if (subnetId < 0 || subnetId > 65535)
        {
            throw new ArgumentOutOfRangeException(nameof(ulaPrefix));
        }

        byte[] networkBytes = ulaPrefix.Network.GetAddressBytes();
            
        // Set subnet ID in bytes 6-7 (positions after the /48 prefix)
        networkBytes[6] = (byte)(subnetId >> 8);
        networkBytes[7] = (byte)(subnetId & 0xFF);

        var subnetAddress = new IPAddress(networkBytes);
        return IPNetwork2.Parse($"{subnetAddress}/64");
    }

    /// <summary>
    /// Validates whether an IP address is within the ULA range.
    /// </summary>
    /// <param name="address">IP address to validate.</param>
    /// <returns>True if the address is a ULA, false otherwise.</returns>
    public static bool IsUla(IPAddress address)
    {
        return address?.AddressFamily == AddressFamily.InterNetworkV6 && UlaRange.Contains(address);
    }

    /// <summary>
    /// Validates whether a network is within the ULA range.
    /// </summary>
    /// <param name="network">Network to validate.</param>
    /// <returns>True if the network is a ULA, false otherwise.</returns>
    public static bool IsUlaPrefix(IPNetwork2 network)
    {
        if (network?.AddressFamily != AddressFamily.InterNetworkV6)
        {
            return false;
        }

        return UlaRange.Contains(network.Network);
    }

    /// <summary>
    /// Validates whether a network is a locally assigned ULA (fd00::/8).
    /// </summary>
    /// <param name="network">Network to validate.</param>
    /// <returns>True if the network is locally assigned ULA, false otherwise.</returns>
    public static bool IsLocallyAssignedUla(IPNetwork2 network)
    {
        if (network?.AddressFamily != AddressFamily.InterNetworkV6)
        {
            return false;
        }

        return UlaLocallyAssigned.Contains(network.Network);
    }

    /// <summary>
    /// Generates a 40-bit random Global ID according to RFC 4193 algorithm.
    /// </summary>
    /// <returns>40-bit Global ID as a byte array.</returns>
    private static byte[] GenerateRandomGlobalId()
    {
        using var rng = RandomNumberGenerator.Create();
        byte[] globalId = new byte[5]; // 40 bits = 5 bytes
        rng.GetBytes(globalId);
        return globalId;
    }

    /// <summary>
    /// Generates a Global ID using MAC address and timestamp as suggested in RFC 4193.
    /// </summary>
    /// <param name="macAddress">6-byte MAC address.</param>
    /// <returns>40-bit Global ID as a byte array.</returns>
    private static byte[] GenerateGlobalIdFromMac(byte[] macAddress)
    {
        using var sha2 = SHA256.Create();
        byte[] input = new byte[macAddress.Length + 8];
        Array.Copy(macAddress, 0, input, 0, macAddress.Length);
                
        // Add current timestamp
        byte[] timestamp = BitConverter.GetBytes(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        Array.Copy(timestamp, 0, input, macAddress.Length, timestamp.Length);

        byte[] hash = sha2.ComputeHash(input);
        byte[] globalId = new byte[5];
        Array.Copy(hash, 0, globalId, 0, 5);
        return globalId;
    }

    /// <summary>
    /// Generates a Global ID from a seed string for deterministic generation.
    /// </summary>
    /// <param name="seed">Seed string.</param>
    /// <returns>40-bit Global ID as a byte array.</returns>
    private static byte[] GenerateGlobalIdFromSeed(string seed)
    {
        using var sha2 = SHA256.Create();
        byte[] seedBytes = Encoding.UTF8.GetBytes(seed);
        byte[] hash = sha2.ComputeHash(seedBytes);
        byte[] globalId = new byte[5];
        Array.Copy(hash, 0, globalId, 0, 5);
        return globalId;
    }

    /// <summary>
    /// Creates a ULA /48 prefix from a 40-bit Global ID.
    /// </summary>
    /// <param name="globalId">5-byte Global ID.</param>
    /// <returns>ULA /48 network.</returns>
    private static IPNetwork2 CreateUlaPrefix(byte[] globalId)
    {
        byte[] addressBytes = new byte[16];
            
        // Set ULA locally assigned prefix (fd)
        addressBytes[0] = 0xfd;
            
        // Set the 40-bit Global ID (5 bytes)
        Array.Copy(globalId, 0, addressBytes, 1, 5);
            
        // Remaining bytes are zero for /48 prefix
            
        var address = new IPAddress(addressBytes);
        return IPNetwork2.Parse($"{address}/48");
    }
}