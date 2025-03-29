// <copyright file="IPNetwork2contains.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Net.Sockets;
using System.Numerics;

public sealed partial class IPNetwork2
{
    /// <summary>
    /// return true if ipaddress is contained in network.
    /// </summary>
    /// <param name="contains">A string containing an ip address to convert.</param>
    /// <returns>true if ipaddress is contained into the IP Network; otherwise, false.</returns>
    public bool Contains(IPAddress contains)
    {
        if (contains == null)
        {
            throw new ArgumentNullException("contains");
        }

        if (this.AddressFamily != contains.AddressFamily)
        {
            return false;
        }

        BigInteger uintNetwork = this.InternalNetwork;
        BigInteger uintBroadcast = this.InternalBroadcast; // CreateBroadcast(ref uintNetwork, this._netmask, this._family);
        var uintAddress = IPNetwork2.ToBigInteger(contains);

        bool result = uintAddress >= uintNetwork
                      && uintAddress <= uintBroadcast;

        return result;
    }

    /// <summary>
    /// Determines whether the given IP address is part of the given IP network.
    /// </summary>
    /// <param name="network">The IP network.</param>
    /// <param name="ipaddress">The IP address.</param>
    /// <returns>
    /// <c>true</c> if the IP address is part of the IP network; otherwise, <c>false</c>.
    /// </returns>
    [Obsolete("static Contains is deprecated, please use instance Contains.")]
    public static bool Contains(IPNetwork2 network, IPAddress ipaddress)
    {
        if (network == null)
        {
            throw new ArgumentNullException("network");
        }

        return network.Contains(ipaddress);
    }

    /// <summary>
    /// return true is network2 is fully contained in network.
    /// </summary>
    /// <param name="contains">The network to test.</param>
    /// <returns>It returns the boolean value. If network2 is in IPNetwork then it returns True, otherwise returns False.</returns>
    public bool Contains(IPNetwork2 contains)
    {
        if (contains == null)
        {
            throw new ArgumentNullException("contains");
        }

        BigInteger uintNetwork = this.InternalNetwork;
        BigInteger uintBroadcast = this.InternalBroadcast; // CreateBroadcast(ref uintNetwork, this._netmask, this._family);

        BigInteger uintFirst = contains.InternalNetwork;
        BigInteger
            uintLast = contains.InternalBroadcast; // CreateBroadcast(ref uintFirst, network2._netmask, network2._family);

        bool result = uintFirst >= uintNetwork
                      && uintLast <= uintBroadcast;

        return result;
    }

    /// <summary>
    /// Determines if the given <paramref name="network"/> contains the specified <paramref name="network2"/>.
    /// </summary>
    /// <param name="network">The network to check for containment.</param>
    /// <param name="network2">The network to check if it is contained.</param>
    /// <returns>
    /// <c>true</c> if the <paramref name="network"/> contains the <paramref name="network2"/>; otherwise, <c>false</c>.
    /// </returns>
    [Obsolete("static Contains is deprecated, please use instance Contains.")]
    public static bool Contains(IPNetwork2 network, IPNetwork2 network2)
    {
        if (network == null)
        {
            throw new ArgumentNullException("network");
        }

        return network.Contains(network2);
    }

    private static BigInteger CreateBroadcast(ref BigInteger network, BigInteger netmask, AddressFamily family)
    {
        int width = family == AddressFamily.InterNetwork ? 4 : 16;
        BigInteger uintBroadcast = network + netmask.PositiveReverse(width);

        return uintBroadcast;
    }
}