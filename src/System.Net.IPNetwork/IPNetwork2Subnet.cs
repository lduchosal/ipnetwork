// <copyright file="IPNetwork2Subnet.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

/// <summary>
/// Subnet.
/// </summary>
public sealed partial class IPNetwork2
{
    /// <summary>
    /// Subnet method is used to divide the given IP network into subnets with the specified CIDR.
    /// </summary>
    /// <param name="network">The IP network to be subnetted.</param>
    /// <param name="cidr">The CIDR (Classless Inter-Domain Routing) value used to subnet the network.</param>
    /// <returns>
    /// A collection of subnets created from the given network using the specified CIDR.
    /// </returns>
    [Obsolete("static Subnet is deprecated, please use instance Subnet.")]
    public static IPNetworkCollection Subnet(IPNetwork2 network, byte cidr)
    {
        if (network == null)
        {
            throw new ArgumentNullException(nameof(network));
        }

        return network.Subnet(cidr);
    }

    /// <summary>
    /// Subnet a network into multiple nets of cidr mask
    /// Subnet 192.168.0.0/24 into cidr 25 gives 192.168.0.0/25, 192.168.0.128/25
    /// Subnet 10.0.0.0/8 into cidr 9 gives 10.0.0.0/9, 10.128.0.0/9.
    /// </summary>
    /// <param name="network">The network.</param>
    /// <param name="cidr">A byte representing the CIDR to be used to subnet the current IPNetwork.</param>
    /// <param name="ipnetworkCollection">The resulting subnetted IPNetwork.</param>
    /// <returns>true if network was split successfully; otherwise, false.</returns>
    [Obsolete("static TrySubnet is deprecated, please use instance TrySubnet.")]
    public static bool TrySubnet(IPNetwork2 network, byte cidr, out IPNetworkCollection ipnetworkCollection)
    {
        if (network == null)
        {
            throw new ArgumentNullException(nameof(network));
        }

        return network.TrySubnet(cidr, out ipnetworkCollection);
    }

    /// <summary>
    /// Subnet a network into multiple nets of cidr mask
    /// Subnet 192.168.0.0/24 into cidr 25 gives 192.168.0.0/25, 192.168.0.128/25
    /// Subnet 10.0.0.0/8 into cidr 9 gives 10.0.0.0/9, 10.128.0.0/9.
    /// </summary>
    /// <param name="cidr1">A byte representing the CIDR to be used to subnet the current IPNetwork.</param>
    /// <param name="ipnetworkCollection">The resulting subnetted IPNetwork.</param>
    /// <returns>true if network was split successfully; otherwise, false.</returns>
    public bool TrySubnet(byte cidr1, out IPNetworkCollection ipnetworkCollection)
    {
        InternalSubnet(true, this, cidr1, out IPNetworkCollection inc);
        if (inc == null)
        {
            ipnetworkCollection = null;
            return false;
        }

        ipnetworkCollection = inc;
        return true;
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
        InternalSubnet(false, this, cidr1, out IPNetworkCollection ipnetworkCollection);

        return ipnetworkCollection;
    }

    /// <summary>
    /// Splits a given IP network into smaller subnets of the specified CIDR size.
    /// </summary>
    /// <param name="trySubnet">Indicates whether to throw exceptions or return null on failure.</param>
    /// <param name="network">The IP network to be subnetted.</param>
    /// <param name="cidr">The CIDR value used to define the new subnet size.</param>
    /// <param name="ipnetworkCollection">The resulting collection of subnets, or null if the operation fails and trySubnet is true.</param>
    internal static void InternalSubnet(bool trySubnet, IPNetwork2 network, byte cidr, out IPNetworkCollection ipnetworkCollection)
    {
        if (network == null)
        {
            if (!trySubnet)
            {
                throw new ArgumentNullException(nameof(network));
            }

            ipnetworkCollection = null;
            return;
        }

        int maxCidr = network.family == Sockets.AddressFamily.InterNetwork ? 32 : 128;
        if (cidr > maxCidr)
        {
            if (!trySubnet)
            {
                throw new ArgumentOutOfRangeException(nameof(cidr));
            }

            ipnetworkCollection = null;
            return;
        }

        if (cidr < network.Cidr)
        {
            if (!trySubnet)
            {
                throw new ArgumentException("cidr");
            }

            ipnetworkCollection = null;
            return;
        }

        ipnetworkCollection = new IPNetworkCollection(network, cidr);
    }
}