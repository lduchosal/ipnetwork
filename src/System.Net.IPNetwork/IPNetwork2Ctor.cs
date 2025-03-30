// <copyright file="IPNetwork2Ctor.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Net.Sockets;
using System.Numerics;

/// <summary>
/// The constructors of IPNetwork2.
/// </summary>
public partial class IPNetwork2
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IPNetwork2"/> class with the specified IP address, address family, and CIDR.
    /// </summary>
    /// <param name="ipaddress">The IP address of the network.</param>
    /// <param name="family">The address family of the network.</param>
    /// <param name="cidr">The CIDR (Classless Inter-Domain Routing) notation of the network.</param>
#if TRAVISCI
    public
#else
    internal
#endif
        IPNetwork2(BigInteger ipaddress, AddressFamily family, byte cidr)
    {
        this.Init(ipaddress, family, cidr);
        this.hashCode = this.ComputeHashCode();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IPNetwork2"/> class.
    /// Creates a new IPNetwork.
    /// </summary>
    /// <param name="ipaddress">An ipaddress.</param>
    /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
    /// <exception cref="ArgumentNullException">ipaddress is null.</exception>
    public IPNetwork2(IPAddress ipaddress, byte cidr)
    {
        if (ipaddress == null)
        {
            throw new ArgumentNullException(nameof(ipaddress));
        }

        BigInteger uintIpAddress = ToBigInteger(ipaddress);

        this.Init(uintIpAddress, ipaddress.AddressFamily, cidr);
        this.hashCode = this.ComputeHashCode();
    }

    private void Init(BigInteger ipaddress1, AddressFamily family1, byte cidr1)
    {
        int maxCidr = family1 == AddressFamily.InterNetwork ? 32 : 128;
        if (cidr1 > maxCidr)
        {
            throw new ArgumentOutOfRangeException(nameof(cidr1));
        }

        this.ipaddress = ipaddress1;
        this.family = family1;
        this.cidr = cidr1;
    }
}