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
    private static void InternalParse(bool tryParse, string ipaddress, string netmask, out IPNetwork2 ipnetwork)
    {
        if (string.IsNullOrEmpty(ipaddress))
        {
            if (tryParse == false)
            {
                throw new ArgumentNullException(nameof(ipaddress));
            }

            ipnetwork = null;
            return;
        }

        if (string.IsNullOrEmpty(netmask))
        {
            if (tryParse == false)
            {
                throw new ArgumentNullException(nameof(netmask));
            }

            ipnetwork = null;
            return;
        }

        bool ipaddressParsed = IPAddress.TryParse(ipaddress, out IPAddress ip);
        if (ipaddressParsed == false)
        {
            if (tryParse == false)
            {
                throw new ArgumentException("ipaddress");
            }

            ipnetwork = null;
            return;
        }

        bool netmaskParsed = IPAddress.TryParse(netmask, out IPAddress mask);
        if (netmaskParsed == false)
        {
            if (tryParse == false)
            {
                throw new ArgumentException("netmask");
            }

            ipnetwork = null;
            return;
        }

        InternalParse(tryParse, ip, mask, out ipnetwork);
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
    private static void InternalParse(bool tryParse, string network, ICidrGuess cidrGuess, bool sanitize, out IPNetwork2 ipnetwork)
    {
        if (string.IsNullOrEmpty(network))
        {
            if (tryParse == false)
            {
                throw new ArgumentNullException(nameof(network));
            }

            ipnetwork = null;
            return;
        }

        if (sanitize)
        {
            network = Regex.Replace(network, @"[^0-9a-fA-F\.\/\s\:]+", string.Empty, RegexOptions.None, TimeSpan.FromMilliseconds(100));
            network = Regex.Replace(network, @"\s{2,}", " ", RegexOptions.None, TimeSpan.FromMilliseconds(100));
            network = network.Trim();
        }

        StringSplitOptions splitOptions = sanitize ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
        string[] args = network.Split([' ', '/'], splitOptions);

        if (args.Length == 0)
        {
            if (tryParse == false)
            {
                throw new ArgumentNullException(nameof(network));
            }

            ipnetwork = null;
            return;
        }
        
        if (args.Length == 1)
        {
            string cidrlessNetwork = args[0];
            if (cidrGuess.TryGuessCidr(cidrlessNetwork, out byte cidr))
            {
                InternalParse(tryParse, cidrlessNetwork, cidr, out ipnetwork);
                return;
            }

            if (tryParse == false)
            {
                throw new ArgumentException("network");
            }

            ipnetwork = null;
            return;
        }
        
        if (args.Length == 2)
        {
            if (byte.TryParse(args[1], out byte cidr1))
            {
                InternalParse(tryParse, args[0], cidr1, out ipnetwork);
                return;
            }

            InternalParse(tryParse, args[0], args[1], out ipnetwork);
        }
        else
        {
            if (tryParse == false)
            {
                throw new ArgumentNullException(nameof(network));
            }
            ipnetwork = null;
            return;
        }
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
    private static void InternalParse(bool tryParse, IPAddress ipaddress, IPAddress netmask, out IPNetwork2 ipnetwork)
    {
        if (ipaddress == null)
        {
            if (tryParse == false)
            {
                throw new ArgumentNullException(nameof(ipaddress));
            }

            ipnetwork = null;
            return;
        }

        if (netmask == null)
        {
            if (tryParse == false)
            {
                throw new ArgumentNullException(nameof(netmask));
            }

            ipnetwork = null;
            return;
        }

        var uintIpAddress = ToBigInteger(ipaddress);
        bool parsed = TryToCidr(netmask, out byte? cidr2);
        if (parsed == false)
        {
            if (tryParse == false)
            {
                throw new ArgumentException("netmask");
            }

            ipnetwork = null;
            return;
        }

        byte cidr = (byte)cidr2!;

        var ipnet = new IPNetwork2(uintIpAddress, ipaddress.AddressFamily, cidr);
        ipnetwork = ipnet;
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
    private static void InternalParse(bool tryParse, string ipaddress, byte cidr, out IPNetwork2 ipnetwork)
    {
        if (string.IsNullOrEmpty(ipaddress))
        {
            if (tryParse == false)
            {
                throw new ArgumentNullException(nameof(ipaddress));
            }

            ipnetwork = null;
            return;
        }

        bool ipaddressParsed = IPAddress.TryParse(ipaddress, out IPAddress ip);
        if (ipaddressParsed == false)
        {
            if (tryParse == false)
            {
                throw new ArgumentException("ipaddress");
            }

            ipnetwork = null;
            return;
        }

        bool parsedNetmask = TryToNetmask(cidr, ip.AddressFamily, out IPAddress mask);
        if (parsedNetmask == false)
        {
            if (tryParse == false)
            {
                throw new ArgumentException("cidr");
            }

            ipnetwork = null;
            return;
        }

        InternalParse(tryParse, ip, mask, out ipnetwork);
    }
}