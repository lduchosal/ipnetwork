// <copyright file="IPNetwork2overlap.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Numerics;

public sealed partial class IPNetwork2
{
    /// <summary>
    /// return true is network2 overlap network.
    /// </summary>
    /// <param name="network2">The network to test.</param>
    /// <returns>true if network2 overlaps into the IP Network; otherwise, false.</returns>
    public bool Overlap(IPNetwork2 network2)
    {
        if (network2 == null)
        {
            throw new ArgumentNullException("network2");
        }

        BigInteger uintNetwork = this.InternalNetwork;
        BigInteger uintBroadcast = this.InternalBroadcast;

        BigInteger uintFirst = network2.InternalNetwork;
        BigInteger uintLast = network2.InternalBroadcast;

        bool overlap =
            (uintFirst >= uintNetwork && uintFirst <= uintBroadcast)
            || (uintLast >= uintNetwork && uintLast <= uintBroadcast)
            || (uintFirst <= uintNetwork && uintLast >= uintBroadcast)
            || (uintFirst >= uintNetwork && uintLast <= uintBroadcast);

        return overlap;
    }

    /// <summary>
    /// Determines if two IPNetwork2 objects overlap each other.
    /// </summary>
    /// <param name="network">The first IPNetwork2 object.</param>
    /// <param name="network2">The second IPNetwork2 object.</param>
    /// <returns>Returns true if the two IPNetwork2 objects overlap, otherwise false.</returns>
    [Obsolete("static Overlap is deprecated, please use instance Overlap.")]
    public static bool Overlap(IPNetwork2 network, IPNetwork2 network2)
    {
        if (network == null)
        {
            throw new ArgumentNullException("network");
        }

        return network.Overlap(network2);
    }
}