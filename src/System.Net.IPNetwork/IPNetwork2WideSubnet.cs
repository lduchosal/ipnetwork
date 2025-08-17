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
            throw new ArgumentNullException(nameof(start));
        }

        if (string.IsNullOrEmpty(end))
        {
            throw new ArgumentNullException(nameof(end));
        }

        if (!IPAddress.TryParse(start, out IPAddress startIP))
        {
            throw new ArgumentException(nameof(start));
        }

        if (!IPAddress.TryParse(end, out IPAddress endIP))
        {
            throw new ArgumentException(nameof(end));
        }

        if (startIP.AddressFamily != endIP.AddressFamily)
        {
            throw new NotSupportedException(nameof(AddressFamily));
        }

        IPNetwork2 ipnetwork;

        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        for (byte cidr = 32; ; cidr--)
        {
            var wideSubnet = Parse(start, cidr);
            if (wideSubnet.Contains(endIP))
            {
                ipnetwork = wideSubnet;
                break;
            }
        }

        return ipnetwork;
    }

    /// <summary>
    /// Finds the widest subnet from an array of IP networks. </summary> <param name="ipnetworks">An array of IPNetwork2 objects representing the IP networks.</param> <returns>The widest subnet as an IPNetwork2 object.</returns>
    /// /
    public static IPNetwork2 WideSubnet(IPNetwork2[] ipnetworks)
    {
        bool parsed = InternalWideSubnet(false, ipnetworks, out IPNetwork2 ipn);
        if (!parsed)
        {
            throw new ArgumentException(nameof(ipnetworks));
        }
        return ipn;
    }

    /// <summary>
    /// Attempts to find the widest subnet that contains both the start and end IP addresses. objects.
    /// </summary>
    /// <param name="ipnetworks">An array of IPNetwork2 objects to wide subnet.</param>
    /// <param name="ipnetwork">When this method returns, contains the wide subnet of the IPNetwork2 objects, if wide subnet was successful; otherwise, null.</param>
    /// <returns>true if wide subnet was successful; otherwise, false.</returns>
    public static bool TryWideSubnet(IPNetwork2[] ipnetworks, out IPNetwork2 ipnetwork)
    {
        bool parsed = InternalWideSubnet(true, ipnetworks, out IPNetwork2 ipn);
        if (!parsed)
        {
            ipnetwork = null;
            return false;
        }
        
        ipnetwork = ipn;
        return true;
    }

    /// <summary>
    /// Attempts to find the widest subnet that includes all given IPNetwork2 instances.
    /// </summary>
    /// <param name="tryWide">If true, suppresses exceptions on invalid input; otherwise, throws.</param>
    /// <param name="ipnetworks">The array of IPNetwork2 instances to encompass within the widest subnet.</param>
    /// <param name="ipnetwork">The resulting widest IPNetwork2 subnet, or null if unsuccessful and tryWide is true.</param>
    /// <returns>true if successful, otherwise false.</returns>
    internal static bool InternalWideSubnet(bool tryWide, IPNetwork2[] ipnetworks, out IPNetwork2 ipnetwork)
    {
        if (ipnetworks == null)
        {
            if (!tryWide)
            {
                throw new ArgumentNullException(nameof(ipnetworks));
            }

            ipnetwork = null;
            return false;
        }

        IPNetwork2[] nnin = Array.FindAll(ipnetworks, ipnet => ipnet != null);

        if (nnin.Length <= 0)
        {
            if (!tryWide)
            {
                throw new ArgumentException(nameof(ipnetworks));
            }

            ipnetwork = null;
            return false;
        }

        if (nnin.Length == 1)
        {
            IPNetwork2 ipn0 = nnin[0];
            ipnetwork = ipn0;
            return true;
        }

        Array.Sort(nnin);
        IPNetwork2 nnin0 = nnin[0];
        BigInteger uintNnin0 = nnin0.ipaddress;

        IPNetwork2 nninX = nnin[nnin.Length - 1];
        IPAddress ipaddressX = nninX.Broadcast;

        AddressFamily family = ipnetworks[0].family;
        foreach (IPNetwork2 ipnx in ipnetworks)
        {
            if (ipnx.family != family)
            {
                if (!tryWide)
                {
                     throw new ArgumentException(nameof(family));
                }
                ipnetwork = null;
                return false;
            }
        }

        IPNetwork2 ipn;

        for (byte cidr = nnin0.cidr; ; cidr--)
        {
            var wideSubnet = new IPNetwork2(uintNnin0, family, cidr);
            if (wideSubnet.Contains(ipaddressX))
            {
                ipn = wideSubnet;
                break;
            }
        }

        ipnetwork = ipn;
        return true;
    }
}