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
    internal IPNetwork2(BigInteger ipaddress, AddressFamily family, byte cidr)
    {
        this.state = CreateSnapshot(ipaddress, family, cidr);
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

        this.state = CreateSnapshot(uintIpAddress, ipaddress.AddressFamily, cidr);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IPNetwork2"/> class sharing the state of another one.
    /// </summary>
    /// <param name="state">The snapshot to share.</param>
    private IPNetwork2(Snapshot state)
    {
        this.state = state;
    }

    /// <summary>
    /// Returns a network equal to this one that no other caller holds a reference to.
    /// </summary>
    /// <remarks>
    /// The snapshot is immutable, so it is shared rather than copied: the returned instance answers
    /// exactly like this one, and rewriting its <see cref="Value"/> replaces its own snapshot without
    /// touching this instance. That is what lets the shared singletons be handed out safely.
    /// </remarks>
    /// <returns>An independent <see cref="IPNetwork2"/> holding the same network.</returns>
    internal IPNetwork2 Copy()
    {
        return new IPNetwork2(this.state);
    }

    private static Snapshot CreateSnapshot(BigInteger ipaddress, AddressFamily family, byte cidr)
    {
        int maxCidr = family == AddressFamily.InterNetwork ? 32 : 128;
        if (cidr > maxCidr)
        {
            throw new ArgumentOutOfRangeException(nameof(cidr));
        }

        return new Snapshot(ipaddress, family, cidr);
    }
}
