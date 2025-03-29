// <copyright file="IPNetwork2WideSubnet.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Net.Sockets;
using System.Numerics;

/// <summary>
/// WideSubnet.
/// </summary>
public sealed partial class IPNetwork2
{
    /// <summary>
    /// Finds the widest subnet that can contain both the start and end IP addresses.
    /// </summary>
    /// <param name="start">The starting IP address.</param>
    /// <param name="end">The ending IP address.</param>
    /// <returns>The widest subnet that contains both the start and end IP addresses.</returns>
    /// <exception cref="ArgumentNullException">Thrown when either the start or end IP address is null or empty.</exception>
    /// <exception cref="ArgumentException">Thrown when the start or end IP addresses are not valid.</exception>
    /// <exception cref="NotSupportedException">Thrown when the start and end IP addresses have different address families.</exception>
    public static IPNetwork2 WideSubnet(string start, string end)
    {
        if (string.IsNullOrEmpty(start))
        {
            throw new ArgumentNullException("start");
        }

        if (string.IsNullOrEmpty(end))
        {
            throw new ArgumentNullException("end");
        }

        if (!IPAddress.TryParse(start, out IPAddress startIP))
        {
            throw new ArgumentException("start");
        }

        if (!IPAddress.TryParse(end, out IPAddress endIP))
        {
            throw new ArgumentException("end");
        }

        if (startIP.AddressFamily != endIP.AddressFamily)
        {
            throw new NotSupportedException("MixedAddressFamily");
        }

        var ipnetwork = new IPNetwork2(0, startIP.AddressFamily, 0);
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        for (byte cidr = 32; cidr >= 0; cidr--)
        {
            var wideSubnet = IPNetwork2.Parse(start, cidr);
            if (wideSubnet.Contains(endIP))
            {
                ipnetwork = wideSubnet;
                break;
            }
        }

        return ipnetwork;
    }

    /// <summary>
    /// Attempts to find the widest subnet that contains both the start and end IP addresses. objects.
    /// </summary>
    /// <param name="ipnetworks">An array of IPNetwork2 objects to wide subnet.</param>
    /// <param name="ipnetwork">When this method returns, contains the wide subnet of the IPNetwork2 objects, if wide subnet was successful; otherwise, null.</param>
    /// <returns>true if wide subnet was successful; otherwise, false.</returns>
    public static bool TryWideSubnet(IPNetwork2[] ipnetworks, out IPNetwork2 ipnetwork)
    {
        IPNetwork2.InternalWideSubnet(true, ipnetworks, out IPNetwork2 ipn);
        if (ipn == null)
        {
            ipnetwork = null;
            return false;
        }

        ipnetwork = ipn;

        return true;
    }

    /// <summary>
    /// Finds the widest subnet from an array of IP networks. </summary> <param name="ipnetworks">An array of IPNetwork2 objects representing the IP networks.</param> <returns>The widest subnet as an IPNetwork2 object.</returns>
    /// /
    public static IPNetwork2 WideSubnet(IPNetwork2[] ipnetworks)
    {
        IPNetwork2.InternalWideSubnet(false, ipnetworks, out IPNetwork2 ipn);
        return ipn;
    }

    /// <summary>
    /// Attempts to find the widest subnet that includes all given IPNetwork2 instances.
    /// </summary>
    /// <param name="tryWide">If true, suppresses exceptions on invalid input; otherwise, throws.</param>
    /// <param name="ipnetworks">The array of IPNetwork2 instances to encompass within the widest subnet.</param>
    /// <param name="ipnetwork">The resulting widest IPNetwork2 subnet, or null if unsuccessful and tryWide is true.</param>
    internal static void InternalWideSubnet(bool tryWide, IPNetwork2[] ipnetworks, out IPNetwork2 ipnetwork)
    {
        if (ipnetworks == null)
        {
            if (tryWide == false)
            {
                throw new ArgumentNullException("ipnetworks");
            }

            ipnetwork = null;
            return;
        }

        IPNetwork2[] nnin = Array.FindAll<IPNetwork2>(ipnetworks, new Predicate<IPNetwork2>(
            delegate(IPNetwork2 ipnet)
            {
                return ipnet != null;
            }));

        if (nnin.Length <= 0)
        {
            if (tryWide == false)
            {
                throw new ArgumentException("ipnetworks");
            }

            ipnetwork = null;
            return;
        }

        if (nnin.Length == 1)
        {
            IPNetwork2 ipn0 = nnin[0];
            ipnetwork = ipn0;
            return;
        }

        Array.Sort<IPNetwork2>(nnin);
        IPNetwork2 nnin0 = nnin[0];
        BigInteger uintNnin0 = nnin0.ipaddress;

        IPNetwork2 nninX = nnin[nnin.Length - 1];
        IPAddress ipaddressX = nninX.Broadcast;

        AddressFamily family = ipnetworks[0].family;
        foreach (IPNetwork2 ipnx in ipnetworks)
        {
            if (ipnx.family != family)
            {
                throw new ArgumentException("MixedAddressFamily");
            }
        }

        var ipn = new IPNetwork2(0, family, 0);
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        for (byte cidr = nnin0.cidr; cidr >= 0; cidr--)
        {
            var wideSubnet = new IPNetwork2(uintNnin0, family, cidr);
            if (wideSubnet.Contains(ipaddressX))
            {
                ipn = wideSubnet;
                break;
            }
        }

        ipnetwork = ipn;
        return;
    }
}