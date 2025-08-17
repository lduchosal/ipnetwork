// <copyright file="IPNetwork2InternalParse.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Text.RegularExpressions;

/// <summary>
/// The InternalParse methodes.
/// </summary>
public partial class IPNetwork2
{
    /// <summary>
    /// 192.168.168.100 - 255.255.255.0
    ///
    /// Network   : 192.168.168.0
    /// Netmask   : 255.255.255.0
    /// Cidr      : 24
    /// Start     : 192.168.168.1
    /// End       : 192.168.168.254
    /// Broadcast : 192.168.168.255.
    /// </summary>
    /// <param name="tryParse">Whether to throw exception or not during conversion.</param>
    /// <param name="ipaddress">A string containing an ip address to convert.</param>
    /// <param name="netmask">A string containing a netmask to convert (255.255.255.0).</param>
    /// <param name="ipnetwork">The resulting IPNetwork.</param>
    private static bool InternalParse(bool tryParse, string ipaddress, string netmask, out IPNetwork2 ipnetwork)
    {
        if (string.IsNullOrEmpty(ipaddress))
        {
            if (!tryParse)
            {
                throw new ArgumentNullException(nameof(ipaddress));
            }

            ipnetwork = null;
            return false;
        }

        if (string.IsNullOrEmpty(netmask))
        {
            if (!tryParse)
            {
                throw new ArgumentNullException(nameof(netmask));
            }

            ipnetwork = null;
            return false;
        }

        bool ipaddressParsed = IPAddress.TryParse(ipaddress, out IPAddress ip);
        if (!ipaddressParsed)
        {
            if (!tryParse)
            {
                throw new ArgumentException("ipaddress");
            }

            ipnetwork = null;
            return false;
        }

        bool netmaskParsed = IPAddress.TryParse(netmask, out IPAddress mask);
        if (!netmaskParsed)
        {
            if (!tryParse)
            {
                throw new ArgumentException("netmask");
            }

            ipnetwork = null;
            return false;
        }

        bool parsed = InternalParse(tryParse, ip, mask, out ipnetwork);
        return parsed;
    }

    /// <summary>
    /// Internal parse an IPNetwork2.
    /// </summary>
    /// <param name="tryParse">Prevent exception.</param>
    /// <param name="network">The network to parse.</param>
    /// <param name="cidrGuess">The way to guess CIDR.</param>
    /// <param name="sanitize">If true, removes invalid characters and normalizes whitespace from the network string, keeping only valid network address characters (0-9, a-f, A-F, ., /, :, and spaces).</param>
    /// <param name="ipnetwork">The resulting IPNetwork.</param>
    /// <exception cref="ArgumentNullException">When network is null.</exception>
    /// <exception cref="ArgumentException">When network is not valid.</exception>
    /// <returns>true if parsed, otherwise false</returns>
    private static bool InternalParse(bool tryParse, string network, ICidrGuess cidrGuess, bool sanitize, out IPNetwork2 ipnetwork)
    {
        if (string.IsNullOrEmpty(network))
        {
            if (!tryParse)
            {
                throw new ArgumentNullException(nameof(network));
            }

            ipnetwork = null;
            return false;
        }

        if (sanitize)
        {
            network = Regex.Replace(network, @"[^0-9a-fA-F\.\/\s\:]+", string.Empty, RegexOptions.None, TimeSpan.FromMilliseconds(100));
            network = Regex.Replace(network, @"\s{2,}", " ", RegexOptions.None, TimeSpan.FromMilliseconds(100));
            network = network.Trim();
        }

        StringSplitOptions splitOptions = sanitize ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
        string[] args = network.Split([' ', '/'], splitOptions);
        
        if (args.Length == 1)
        {
            string cidrlessNetwork = args[0];
            if (cidrGuess.TryGuessCidr(cidrlessNetwork, out byte cidr))
            {
                bool parsed = InternalParse(tryParse, cidrlessNetwork, cidr, out ipnetwork);
                return parsed;
            }

            if (!tryParse)
            {
                throw new ArgumentException("network");
            }

            ipnetwork = null;
            return false;
        }
        
        if (args.Length == 2)
        {
            if (byte.TryParse(args[1], out byte cidr1))
            {
                bool parsed2 = InternalParse(tryParse, args[0], cidr1, out ipnetwork);
                return parsed2;
            }

            bool parsed3 = InternalParse(tryParse, args[0], args[1], out ipnetwork);
            return parsed3;
        }
        
        if (!tryParse)
        {
            throw new ArgumentNullException(nameof(network));
        }

        ipnetwork = null;
        return false;
    }

    /// <summary>
    /// 192.168.168.100 255.255.255.0
    ///
    /// Network   : 192.168.168.0
    /// Netmask   : 255.255.255.0
    /// Cidr      : 24
    /// Start     : 192.168.168.1
    /// End       : 192.168.168.254
    /// Broadcast : 192.168.168.255.
    /// </summary>
    /// <param name="tryParse">Whether to throw exception or not during conversion.</param>
    /// <param name="ipaddress">An ip address to convert.</param>
    /// <param name="netmask">A netmask to convert (255.255.255.0).</param>
    /// <param name="ipnetwork">The resulting IPNetwork.</param>
    private static bool InternalParse(bool tryParse, IPAddress ipaddress, IPAddress netmask, out IPNetwork2 ipnetwork)
    {
        if (ipaddress == null)
        {
            if (!tryParse)
            {
                throw new ArgumentNullException(nameof(ipaddress));
            }

            ipnetwork = null;
            return false;
        }

        if (netmask == null)
        {
            if (!tryParse)
            {
                throw new ArgumentNullException(nameof(netmask));
            }

            ipnetwork = null;
            return false;
        }

        var uintIpAddress = ToBigInteger(ipaddress);
        bool parsed = TryToCidr(netmask, out byte cidr2);
        if (!parsed)
        {
            if (!tryParse)
            {
                throw new ArgumentException("netmask");
            }

            ipnetwork = null;
            return false;
        }

        var ipnet = new IPNetwork2(uintIpAddress, ipaddress.AddressFamily, cidr2);
        ipnetwork = ipnet;
        return true;
    }

    /// <summary>
    /// 192.168.168.100/24
    ///
    /// Network   : 192.168.168.0
    /// Netmask   : 255.255.255.0
    /// Cidr      : 24
    /// Start     : 192.168.168.1
    /// End       : 192.168.168.254
    /// Broadcast : 192.168.168.255.
    /// </summary>
    /// <param name="tryParse">Whether to throw exception or not during conversion.</param>
    /// <param name="ipaddress">A string containing an ip address to convert.</param>
    /// <param name="cidr">A byte representing the CIDR to be used in conversion (/24).</param>
    /// <param name="ipnetwork">The resulting IPNetwork.</param>
    private static bool InternalParse(bool tryParse, string ipaddress, byte cidr, out IPNetwork2 ipnetwork)
    {
        if (string.IsNullOrEmpty(ipaddress))
        {
            if (!tryParse)
            {
                throw new ArgumentNullException(nameof(ipaddress));
            }

            ipnetwork = null;
            return false;
        }

        bool ipaddressParsed = IPAddress.TryParse(ipaddress, out IPAddress ip);
        if (!ipaddressParsed)
        {
            if (!tryParse)
            {
                throw new ArgumentException("ipaddress");
            }

            ipnetwork = null;
            return false;
        }

        bool parsedNetmask = TryToNetmask(cidr, ip.AddressFamily, out IPAddress mask);
        if (!parsedNetmask)
        {
            if (!tryParse)
            {
                throw new ArgumentException("cidr");
            }

            ipnetwork = null;
            return false;
        }

        bool parsed = InternalParse(tryParse, ip, mask, out ipnetwork);
        return parsed;
    }
}