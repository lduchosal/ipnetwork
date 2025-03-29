// <copyright file="IPNetwork2SupernetArray.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Collections.Generic;

/// <summary>
/// SupernetArray.
/// </summary>
public sealed partial class IPNetwork2
{
    /// <summary>
    /// Supernet a list of subnet
    /// 192.168.0.0/24 + 192.168.1.0/24 = 192.168.0.0/23
    /// 192.168.0.0/24 + 192.168.1.0/24 + 192.168.2.0/24 + 192.168.3.0/24 = 192.168.0.0/22.
    /// </summary>
    /// <param name="ipnetworks">A list of IPNetwork to merge into common supernets.</param>
    /// <returns>The result of IPNetwork if merges succeed, the first ipnetwork otherwise.</returns>
    public static IPNetwork2[] Supernet(IPNetwork2[] ipnetworks)
    {
        InternalSupernet(false, ipnetworks, out IPNetwork2[] supernet);
        return supernet;
    }

    /// <summary>
    /// Supernet a list of subnet
    /// 192.168.0.0/24 + 192.168.1.0/24 = 192.168.0.0/23
    /// 192.168.0.0/24 + 192.168.1.0/24 + 192.168.2.0/24 + 192.168.3.0/24 = 192.168.0.0/22.
    /// </summary>
    /// <param name="ipnetworks">A list of IPNetwork to merge into common supernets.</param>
    /// <param name="supernet">The result of IPNetwork merges.</param>
    /// <returns>true if ipnetworks was supernetted successfully; otherwise, false.</returns>
    public static bool TrySupernet(IPNetwork2[] ipnetworks, out IPNetwork2[] supernet)
    {
        bool supernetted = InternalSupernet(true, ipnetworks, out supernet);
        return supernetted;
    }

    /// <summary>
    /// Attempts to merge an array of adjacent IP networks with equal CIDR values into the smallest possible set of supernets.
    /// </summary>
    /// <param name="trySupernet">If true, suppresses exceptions on failure; otherwise, throws.</param>
    /// <param name="ipnetworks">The array of IP networks to attempt to merge.</param>
    /// <param name="supernet">The resulting array of merged supernets if successful; otherwise, the original input.</param>
    /// <returns><c>true</c> if supernetting was successful; otherwise, <c>false</c>.</returns>
    internal static bool InternalSupernet(bool trySupernet, IPNetwork2[] ipnetworks, out IPNetwork2[] supernet)
    {
        if (ipnetworks == null)
        {
            if (trySupernet == false)
            {
                throw new ArgumentNullException("ipnetworks");
            }

            supernet = null;
            return false;
        }

        if (ipnetworks.Length <= 0)
        {
            supernet = new IPNetwork2[0];
            return true;
        }

        var supernetted = new List<IPNetwork2>();
        List<IPNetwork2> ipns = IPNetwork2.Array2List(ipnetworks);
        Stack<IPNetwork2> current = IPNetwork2.List2Stack(ipns);
        int previousCount = 0;
        int currentCount = current.Count;

        while (previousCount != currentCount)
        {
            supernetted.Clear();
            while (current.Count > 1)
            {
                IPNetwork2 ipn1 = current.Pop();
                IPNetwork2 ipn2 = current.Peek();

                bool success = ipn1.TrySupernet(ipn2, out IPNetwork2 outNetwork);
                if (success)
                {
                    current.Pop();
                    current.Push(outNetwork);
                }
                else
                {
                    supernetted.Add(ipn1);
                }
            }

            if (current.Count == 1)
            {
                supernetted.Add(current.Pop());
            }

            previousCount = currentCount;
            currentCount = supernetted.Count;
            current = IPNetwork2.List2Stack(supernetted);
        }

        supernet = supernetted.ToArray();
        return true;
    }

    private static Stack<IPNetwork2> List2Stack(List<IPNetwork2> list)
    {
        var stack = new Stack<IPNetwork2>();
        list.ForEach(new Action<IPNetwork2>(
            delegate(IPNetwork2 ipn)
            {
                stack.Push(ipn);
            }));
        return stack;
    }

    private static List<IPNetwork2> Array2List(IPNetwork2[] array)
    {
        var ipns = new List<IPNetwork2>();
        ipns.AddRange(array);
        IPNetwork2.RemoveNull(ipns);
        ipns.Sort(new Comparison<IPNetwork2>(
            delegate(IPNetwork2 ipn1, IPNetwork2 ipn2)
            {
                int networkCompare = ipn1.InternalNetwork.CompareTo(ipn2.InternalNetwork);
                if (networkCompare == 0)
                {
                    int cidrCompare = ipn1.cidr.CompareTo(ipn2.cidr);
                    return cidrCompare;
                }

                return networkCompare;
            }));
        ipns.Reverse();

        return ipns;
    }

    private static void RemoveNull(List<IPNetwork2> ipns)
    {
        ipns.RemoveAll(new Predicate<IPNetwork2>(
            delegate(IPNetwork2 ipn)
            {
                if (ipn == null)
                {
                    return true;
                }

                return false;
            }));
    }
}