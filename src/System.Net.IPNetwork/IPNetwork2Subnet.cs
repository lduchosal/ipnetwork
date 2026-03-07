// <copyright file="IPNetwork2Subnet.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Subnet.
/// </summary>
public sealed partial class IPNetwork2
{
    /// <summary>
    /// Subnet a network into multiple nets of cidr mask
    /// Subnet 192.168.0.0/24 into cidr 25 gives 192.168.0.0/25, 192.168.0.128/25
    /// Subnet 10.0.0.0/8 into cidr 9 gives 10.0.0.0/9, 10.128.0.0/9.
    /// </summary>
    /// <param name="cidr1">A byte representing the CIDR to be used to subnet the current IPNetwork.</param>
    /// <param name="ipnetworkCollection">The resulting subnetted IPNetwork.</param>
    /// <returns>true if network was split successfully; otherwise, false.</returns>
    public bool TrySubnet(byte cidr1, out IPNetworkCollection? ipnetworkCollection)
    {
        return InternalSubnet(true, this, cidr1, out ipnetworkCollection);
    }

    /// <summary>
    /// Subnet a network into multiple nets of cidr mask
    /// Subnet 192.168.0.0/24 into cidr 25 gives 192.168.0.0/25, 192.168.0.128/25
    /// Subnet 10.0.0.0/8 into cidr 9 gives 10.0.0.0/9, 10.128.0.0/9.
    /// </summary>
    /// <param name="cidr1">A byte representing the CIDR to be used to subnet the current IPNetwork.</param>
    /// <returns>A IPNetworkCollection split by CIDR.</returns>
    public IPNetworkCollection Subnet(byte cidr1)
    {
        if (!InternalSubnet(false, this, cidr1, out IPNetworkCollection? ipnetworkCollection))
        {
            throw new ArgumentException("Invalid CIDR.", nameof(cidr1));
        }

        return ipnetworkCollection;
    }

    /// <summary>
    /// Splits a given IP network into smaller subnets of the specified CIDR size.
    /// </summary>
    /// <param name="trySubnet">Indicates whether to throw exceptions or return null on failure.</param>
    /// <param name="network">The IP network to be subnetted.</param>
    /// <param name="cidr">The CIDR value used to define the new subnet size.</param>
    /// <param name="ipnetworkCollection">The resulting collection of subnets, or null if the operation fails and trySubnet is true.</param>
    internal static bool InternalSubnet(bool trySubnet, IPNetwork2 network, byte cidr, [NotNullWhen(true)] out IPNetworkCollection? ipnetworkCollection)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (network == null)
        {
            if (!trySubnet)
            {
                throw new ArgumentNullException(nameof(network));
            }

            ipnetworkCollection = null;
            return false;
        }

        int maxCidr = network.family == Sockets.AddressFamily.InterNetwork ? 32 : 128;
        if (cidr > maxCidr)
        {
            if (!trySubnet)
            {
                throw new ArgumentOutOfRangeException(nameof(cidr));
            }

            ipnetworkCollection = null;
            return false;
        }

        if (cidr < network.Cidr)
        {
            if (!trySubnet)
            {
                throw new ArgumentException("CIDR is smaller than the network CIDR.", nameof(cidr));
            }

            ipnetworkCollection = null;
            return false;
        }

        ipnetworkCollection = new IPNetworkCollection(network, cidr);
        return true;
    }
}