// <copyright file="IPNetwork2Supernet.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Diagnostics.CodeAnalysis;
using System.Numerics;

/// <summary>
/// supernet.
/// </summary>
public sealed partial class IPNetwork2
{
    /// <summary>
    /// Supernet two consecutive cidr equal subnet into a single one
    /// 192.168.0.0/24 + 192.168.1.0/24 = 192.168.0.0/23
    /// 10.1.0.0/16 + 10.0.0.0/16 = 10.0.0.0/15
    /// 192.168.0.0/24 + 192.168.0.0/25 = 192.168.0.0/24.
    /// </summary>
    /// <param name="network2">The network to supernet with.</param>
    /// <returns>A super netted IP Network.</returns>
    public IPNetwork2 Supernet(IPNetwork2 network2)
    {
        if (!InternalSupernet(false, this, network2, out IPNetwork2? supernet))
        {
            throw new ArgumentException("Failed to supernet networks.", nameof(network2));
        }

        return supernet;
    }

    /// <summary>
    /// Try to supernet two consecutive cidr equal subnet into a single one
    /// 192.168.0.0/24 + 192.168.1.0/24 = 192.168.0.0/23
    /// 10.1.0.0/16 + 10.0.0.0/16 = 10.0.0.0/15
    /// 192.168.0.0/24 + 192.168.0.0/25 = 192.168.0.0/24.
    /// </summary>
    /// <param name="network2">The network to supernet with.</param>
    /// <param name="supernet">The resulting IPNetwork.</param>
    /// <returns>true if network2 was super netted successfully; otherwise, false.</returns>
    public bool TrySupernet(IPNetwork2 network2, [NotNullWhen(true)] out IPNetwork2? supernet)
    {
        return InternalSupernet(true, this, network2, out supernet);
    }

    /// <summary>
    /// Supernet two consecutive cidr equal subnet into a single one
    /// 192.168.0.0/24 + 192.168.1.0/24 = 192.168.0.0/23
    /// 10.1.0.0/16 + 10.0.0.0/16 = 10.0.0.0/15
    /// 192.168.0.0/24 + 192.168.0.0/25 = 192.168.0.0/24.
    /// </summary>
    /// <param name="network1">The first network.</param>
    /// <param name="network2">The second network.</param>
    /// <returns>A super netted IP Network.</returns>
    public static IPNetwork2 Supernet(IPNetwork2 network1, IPNetwork2 network2)
    {
        if (!InternalSupernet(false, network1, network2, out IPNetwork2? supernet))
        {
            throw new ArgumentException("Failed to supernet networks.", nameof(network2));
        }

        return supernet;
    }

    /// <summary>
    /// Try to supernet two consecutive cidr equal subnet into a single one
    /// 192.168.0.0/24 + 192.168.1.0/24 = 192.168.0.0/23
    /// 10.1.0.0/16 + 10.0.0.0/16 = 10.0.0.0/15
    /// 192.168.0.0/24 + 192.168.0.0/25 = 192.168.0.0/24.
    /// </summary>
    /// <param name="network1">The first network.</param>
    /// <param name="network2">The second network.</param>
    /// <param name="supernet">The resulting IPNetwork.</param>
    /// <returns>true if network1 and network2 were super netted successfully; otherwise, false.</returns>
    public static bool TrySupernet(IPNetwork2 network1, IPNetwork2 network2, [NotNullWhen(true)] out IPNetwork2? supernet)
    {
        if (network1 == null)
        {
            throw new ArgumentNullException(nameof(network1));
        }

        if (network2 == null)
        {
            throw new ArgumentNullException(nameof(network2));
        }

        return InternalSupernet(true, network1, network2, out supernet);
    }

    /// <summary>
    /// Attempts to merge two adjacent IP networks with equal CIDR values into a single supernet.
    /// </summary>
    /// <param name="trySupernet">If true, suppresses exceptions on failure; otherwise, throws.</param>
    /// <param name="network1">The first IP network.</param>
    /// <param name="network2">The second IP network.</param>
    /// <param name="supernet">The resulting supernet if the merge is successful; otherwise, null.</param>
    internal static bool InternalSupernet(
        bool trySupernet,
        IPNetwork2 network1,
        IPNetwork2 network2,
        [NotNullWhen(true)] out IPNetwork2? supernet)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (network1 == null)
        {
            if (!trySupernet)
            {
                throw new ArgumentNullException(nameof(network1));
            }

            supernet = null;
            return false;
        }

        if (network2 == null)
        {
            if (!trySupernet)
            {
                throw new ArgumentNullException(nameof(network2));
            }

            supernet = null;
            return false;
        }

        if (network1.Contains(network2))
        {
            supernet = new IPNetwork2(network1.InternalNetwork, network1.family, network1.Cidr);
            return true;
        }

        if (network2.Contains(network1))
        {
            supernet = new IPNetwork2(network2.InternalNetwork, network2.family, network2.Cidr);
            return true;
        }

        if (network1.cidr != network2.cidr)
        {
            if (!trySupernet)
            {
                throw new ArgumentException("Networks must have the same CIDR.", nameof(network1));
            }

            supernet = null;
            return false;
        }

        IPNetwork2 first = (network1.InternalNetwork < network2.InternalNetwork) ? network1 : network2;
        IPNetwork2 last = (network1.InternalNetwork > network2.InternalNetwork) ? network1 : network2;

        // Starting from here :
        // network1 and network2 have the same cidr,
        // network1 does not contain network2,
        // network2 does not contain network1,
        // first is the lower subnet
        // last is the higher subnet
        if ((first.InternalBroadcast + 1) != last.InternalNetwork)
        {
            if (!trySupernet)
            {
                throw new ArgumentOutOfRangeException(nameof(network1));
            }

            supernet = null;
            return false;
        }

        BigInteger uintSupernet = first.InternalNetwork;
        byte cidrSupernet = (byte)(first.cidr - 1);

        var networkSupernet = new IPNetwork2(uintSupernet, first.family, cidrSupernet);
        if (networkSupernet.InternalNetwork != first.InternalNetwork)
        {
            if (!trySupernet)
            {
                throw new ArgumentException("Networks are not contiguous.", nameof(network1));
            }

            supernet = null;
            return false;
        }

        supernet = networkSupernet;
        return true;
    }
}