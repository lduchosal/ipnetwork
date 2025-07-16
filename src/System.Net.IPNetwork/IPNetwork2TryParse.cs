// <copyright file="IPNetwork2TryParse.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

/// <summary>
/// Regroup the tryParse functionalities.
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
    /// <param name="ipaddress">A string containing an ip address to convert.</param>
    /// <param name="netmask">A string containing a netmask to convert (255.255.255.0).</param>
    /// <param name="ipnetwork">When this method returns, contains the IPNetwork value equivalent of the IPAddress contained in ipaddress with the netmask corresponding to cidr, if the conversion succeeded, or null if the conversion failed. The conversion fails if the s parameter is null or Empty, is not of the correct format, or represents an invalid ip address. This parameter is passed uninitialized; any value originally supplied in result will be overwritten.</param>
    /// <returns>true if ipaddress/netmask was converted successfully; otherwise, false.</returns>
    public static bool TryParse(string ipaddress, string netmask, out IPNetwork2 ipnetwork)
    {
        InternalParse(true, ipaddress, netmask, out IPNetwork2 ipnetwork2);
        bool parsed = ipnetwork2 != null;
        ipnetwork = ipnetwork2;

        return parsed;
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
    /// <param name="ipaddress">A string containing an ip address to convert.</param>
    /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
    /// <param name="ipnetwork">When this method returns, contains the IPNetwork value equivalent of the IPAddress contained in ipaddress with the netmask corresponding to cidr, if the conversion succeeded, or null if the conversion failed. The conversion fails if the s parameter is null or Empty, is not of the correct format, or represents an invalid ip address. This parameter is passed uninitialized; any value originally supplied in result will be overwritten.</param>
    /// <returns>true if ipaddress/cidr was converted successfully; otherwise, false.</returns>
    public static bool TryParse(string ipaddress, byte cidr, out IPNetwork2 ipnetwork)
    {
        InternalParse(true, ipaddress, cidr, out IPNetwork2 ipnetwork2);
        bool parsed = ipnetwork2 != null;
        ipnetwork = ipnetwork2;

        return parsed;
    }

    /// <summary>
    /// 192.168.0.1/24
    /// 192.168.0.1 255.255.255.0
    ///
    /// Network   : 192.168.0.0
    /// Netmask   : 255.255.255.0
    /// Cidr      : 24
    /// Start     : 192.168.0.1
    /// End       : 192.168.0.254
    /// Broadcast : 192.168.0.255.
    /// </summary>
    /// <param name="network">A string containing an ip network to convert.</param>
    /// <param name="ipnetwork">When this method returns, contains the IPNetwork value equivalent of the IPAddress contained in ipaddress with the netmask corresponding to cidr, if the conversion succeeded, or null if the conversion failed. The conversion fails if the s parameter is null or Empty, is not of the correct format, or represents an invalid ip address. This parameter is passed uninitialized; any value originally supplied in result will be overwritten.</param>
    /// <returns>true if network was converted successfully; otherwise, false.</returns>
    public static bool TryParse(string network, out IPNetwork2 ipnetwork)
    {
        InternalParse(true, network, CidrGuess.ClassFull, sanitanize: true, out IPNetwork2 ipnetwork2);
        bool parsed = ipnetwork2 != null;
        ipnetwork = ipnetwork2;

        return parsed;
    }

    /// <summary>
    /// 192.168.0.1/24
    /// 192.168.0.1 255.255.255.0
    ///
    /// Network   : 192.168.0.0
    /// Netmask   : 255.255.255.0
    /// Cidr      : 24
    /// Start     : 192.168.0.1
    /// End       : 192.168.0.254
    /// Broadcast : 192.168.0.255.
    /// </summary>
    /// <param name="network">A string containing an ip network to convert.</param>
    /// <param name="sanitanize">Whether to sanitize network or not.</param>
    /// <param name="ipnetwork">When this method returns, contains the IPNetwork value equivalent of the IPAddress contained in ipaddress with the netmask corresponding to cidr, if the conversion succeeded, or null if the conversion failed. The conversion fails if the s parameter is null or Empty, is not of the correct format, or represents an invalid ip address. This parameter is passed uninitialized; any value originally supplied in result will be overwritten.</param>
    /// <returns>true if network was converted successfully; otherwise, false.</returns>
    public static bool TryParse(string network, bool sanitanize, out IPNetwork2 ipnetwork)
    {
        InternalParse(true, network, CidrGuess.ClassFull, sanitanize, out IPNetwork2 ipnetwork2);
        bool parsed = ipnetwork2 != null;
        ipnetwork = ipnetwork2;

        return parsed;
    }

    /// <summary>
    /// 192.168.0.1/24
    /// 192.168.0.1 255.255.255.0
    ///
    /// Network   : 192.168.0.0
    /// Netmask   : 255.255.255.0
    /// Cidr      : 24
    /// Start     : 192.168.0.1
    /// End       : 192.168.0.254
    /// Broadcast : 192.168.0.255.
    /// </summary>
    /// <param name="ipaddress">An IPAddress to convert.</param>
    /// <param name="netmask">An IPAddress to be used as netmask to convert.</param>
    /// <param name="ipnetwork">When this method returns, contains the IPNetwork value equivalent of the IPAddress contained in ipaddress with the netmask corresponding to cidr, if the conversion succeeded, or null if the conversion failed. The conversion fails if the s parameter is null or Empty, is not of the correct format, or represents an invalid ip address. This parameter is passed uninitialized; any value originally supplied in result will be overwritten.</param>
    /// <returns>true if network was converted successfully; otherwise, false.</returns>
    public static bool TryParse(IPAddress ipaddress, IPAddress netmask, out IPNetwork2 ipnetwork)
    {
        InternalParse(true, ipaddress, netmask, out IPNetwork2 ipnetwork2);
        bool parsed = ipnetwork2 != null;
        ipnetwork = ipnetwork2;

        return parsed;
    }

    /// <summary>
    /// 192.168.0.1/24
    /// 192.168.0.1 255.255.255.0
    ///
    /// Network   : 192.168.0.0
    /// Netmask   : 255.255.255.0
    /// Cidr      : 24
    /// Start     : 192.168.0.1
    /// End       : 192.168.0.254
    /// Broadcast : 192.168.0.255.
    /// </summary>
    /// <param name="network">A string containing an ip network to convert.</param>
    /// <param name="cidrGuess">A ICidrGuess implementation that will be used to guess CIDR during conversion.</param>
    /// <param name="ipnetwork">When this method returns, contains the IPNetwork value equivalent of the IPAddress contained in ipaddress with the netmask corresponding to cidr, if the conversion succeeded, or null if the conversion failed. The conversion fails if the s parameter is null or Empty, is not of the correct format, or represents an invalid ip address. This parameter is passed uninitialized; any value originally supplied in result will be overwritten.</param>
    /// <returns>true if network was converted successfully; otherwise, false.</returns>
    public static bool TryParse(string network, ICidrGuess cidrGuess, out IPNetwork2 ipnetwork)
    {
        InternalParse(true, network, cidrGuess, true, out IPNetwork2 ipnetwork2);
        bool parsed = ipnetwork2 != null;
        ipnetwork = ipnetwork2;

        return parsed;
    }

    /// <summary>
    /// 192.168.0.1/24
    /// 192.168.0.1 255.255.255.0
    ///
    /// Network   : 192.168.0.0
    /// Netmask   : 255.255.255.0
    /// Cidr      : 24
    /// Start     : 192.168.0.1
    /// End       : 192.168.0.254
    /// Broadcast : 192.168.0.255.
    /// </summary>
    /// <param name="network">A string containing an ip network to convert.</param>
    /// <param name="cidrGuess">A ICidrGuess implementation that will be used to guess CIDR during conversion.</param>
    /// <param name="sanitanize">Whether to sanitize network or not.</param>
    /// <param name="ipnetwork">When this method returns, contains the IPNetwork value equivalent of the IPAddress contained in ipaddress with the netmask corresponding to cidr, if the conversion succeeded, or null if the conversion failed. The conversion fails if the s parameter is null or Empty, is not of the correct format, or represents an invalid ip address. This parameter is passed uninitialized; any value originally supplied in result will be overwritten.</param>
    /// <returns>true if network was converted successfully; otherwise, false.</returns>
    public static bool TryParse(string network, ICidrGuess cidrGuess, bool sanitanize, out IPNetwork2 ipnetwork)
    {
        InternalParse(true, network, cidrGuess, sanitanize, out IPNetwork2 ipnetwork2);
        bool parsed = ipnetwork2 != null;
        ipnetwork = ipnetwork2;

        return parsed;
    }
}