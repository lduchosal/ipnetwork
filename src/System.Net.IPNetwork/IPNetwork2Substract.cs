// <copyright file="IPNetwork2Supernet.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;

namespace System.Net;

/// <summary>
/// Substract.
/// </summary>
public sealed partial class IPNetwork2
{
    /// <summary>
    /// Implementation for IP network symmetric difference (subtraction)
    /// 0.0.0.0/0 - 10.0.0.1/32 = [
    ///     0.0.0.0/5, 8.0.0.0/7, 10.0.0.0/32, 10.0.0.2/31, 10.0.0.4/30, 10.0.0.8/29,
    ///     10.0.0.16/28, 10.0.0.32/27, 10.0.0.64/26, 10.0.0.128/25, 10.0.1.0/24, 10.0.2.0/23,
    ///     10.0.4.0/22, 10.0.8.0/21, 10.0.16.0/20, 10.0.32.0/19, 10.0.64.0/18, 10.0.128.0/17,
    ///     10.1.0.0/16, 10.2.0.0/15, 10.4.0.0/14, 10.8.0.0/13, 10.16.0.0/12, 10.32.0.0/11,
    ///     10.64.0.0/10, 10.128.0.0/9, 11.0.0.0/8, 12.0.0.0/6, 16.0.0.0/4, 32.0.0.0/3,
    ///     64.0.0.0/2, 128.0.0.0/1
    /// ].
    /// </summary>
    /// <param name="network2">The network to subtract.</param>
    /// <returns>A list of IP Network.</returns>
    public List<IPNetwork2> Subtract(IPNetwork2 network2)
    {
        InternalSubtract(false, this, network2, out List<IPNetwork2> result);
        return result;
    }

    /// <summary>
    /// Implementation for IP network symmetric difference (subtraction)
    /// 0.0.0.0/0 - 10.0.0.1/32 = [
    ///     0.0.0.0/5, 8.0.0.0/7, 10.0.0.0/32, 10.0.0.2/31, 10.0.0.4/30, 10.0.0.8/29,
    ///     10.0.0.16/28, 10.0.0.32/27, 10.0.0.64/26, 10.0.0.128/25, 10.0.1.0/24, 10.0.2.0/23,
    ///     10.0.4.0/22, 10.0.8.0/21, 10.0.16.0/20, 10.0.32.0/19, 10.0.64.0/18, 10.0.128.0/17,
    ///     10.1.0.0/16, 10.2.0.0/15, 10.4.0.0/14, 10.8.0.0/13, 10.16.0.0/12, 10.32.0.0/11,
    ///     10.64.0.0/10, 10.128.0.0/9, 11.0.0.0/8, 12.0.0.0/6, 16.0.0.0/4, 32.0.0.0/3,
    ///     64.0.0.0/2, 128.0.0.0/1
    /// ].
    /// </summary>
    /// <param name="network2">The network to supernet with.</param>
    /// <param name="result">The resulting IPNetwork.</param>
    /// <returns>true if network2 was subtracted successfully; otherwise, false.</returns>
    public bool TrySubtract(IPNetwork2 network2, out List<IPNetwork2> result)
    {
        InternalSubtract(true, this, network2, out List<IPNetwork2> outResult);
        bool parsed = outResult != null;
        result = outResult;
        return parsed;
    }

    /// <summary>
    /// Attempts to merge two adjacent IP networks with equal CIDR values into a single supernet.
    /// </summary>
    /// <param name="trySubtract">If true, suppresses exceptions on failure; otherwise, throws.</param>
    /// <param name="network1">The first IP network.</param>
    /// <param name="network2">The second IP network.</param>
    /// <param name="result">The resulting subtracted.</param>
    internal static void InternalSubtract(
        bool trySubtract,
        IPNetwork2 network1,
        IPNetwork2 network2,
        out List<IPNetwork2> result)
    {
        if (network1 == null)
        {
            if (trySubtract == false)
            {
                throw new ArgumentNullException(nameof(network1));
            }

            result = null;
            return;
        }

        if (network2 == null)
        {
            if (trySubtract == false)
            {
                throw new ArgumentNullException(nameof(network2));
            }

            result = null;
            return;
        }

        // If network2 completely contains network1, return empty
        if (network2.Contains(network1))
        {
            result = [];
            return;
        }

        // If networks don't overlap, return original network
        if (!network1.Contains(network2))
        {
            var copy = new IPNetwork2(network1.InternalNetwork, network1.family, network1.Cidr);
            result = [copy];
            return;
        }

        // If network1 completely contains network2, we need to split
        result = network1.SplitAroundSubnet(network2);
        return;
    }
    
    private List<IPNetwork2> SplitAroundSubnet(IPNetwork2 network2)
    {
        var result = new List<IPNetwork2>();
        var queue = new Queue<IPNetwork2>();
        queue.Enqueue(this);
        
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            
            // If current doesn't overlap with hole, add it to result
            if (!current.Overlap(network2))
            {
                result.Add(current);
                continue;
            }
            
            // If hole completely contains current, skip it
            if (network2.Contains(current))
            {
                continue;
            }
            
            // If current is same size or smaller than hole and overlaps, we need to split
            var split = current.Subnet((byte)(current.Cidr + 1));
            var leftHalf = split[0];
            var rightHalf = split[1];
            
            queue.Enqueue(leftHalf);
            queue.Enqueue(rightHalf);
        }
        
        return result.OrderBy(n => n.ipaddress).ToList();
    }
}