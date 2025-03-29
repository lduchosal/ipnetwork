// <copyright file="IPNetwork2Parse.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

/// <summary>
/// the parse methods.
/// </summary>
public partial class IPNetwork2
{
    /// <summary>
    /// 192.168.168.100 - 255.255.255.0
    ///
    /// ```
    /// Network   : 192.168.168.0
    /// Netmask   : 255.255.255.0
    /// Cidr      : 24
    /// Start     : 192.168.168.1
    /// End       : 192.168.168.254
    /// Broadcast : 192.168.168.255
    /// ```.
    ///
    /// </summary>
    /// <param name="ipaddress">A string containing an ip address to convert.</param>
    /// <param name="netmask">A string representing a netmask in std format (255.255.255.0).</param>
    /// <returns>An IPNetwork equivalent to the network contained in ipaddress/netmask.</returns>
    public static IPNetwork2 Parse(string ipaddress, string netmask)
    {
        InternalParse(false, ipaddress, netmask, out IPNetwork2 ipnetwork);
        return ipnetwork;
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
    /// <returns>An IPNetwork equivalent to the network contained in ipaddress/cidr.</returns>
    public static IPNetwork2 Parse(string ipaddress, byte cidr)
    {
        IPNetwork2.InternalParse(false, ipaddress, cidr, out IPNetwork2 ipnetwork);
        return ipnetwork;
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
    /// <param name="ipaddress">A string containing an ip address to convert.</param>
    /// <param name="netmask">A netmask to be used to create the IPNetwork.</param>
    /// <returns>An IPNetwork equivalent to the network contained in ipaddress/netmask.</returns>
    public static IPNetwork2 Parse(IPAddress ipaddress, IPAddress netmask)
    {
        IPNetwork2.InternalParse(false, ipaddress, netmask, out IPNetwork2 ipnetwork);
        return ipnetwork;
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
    /// <returns>An IPNetwork equivalent to the network contained in string network.</returns>
    public static IPNetwork2 Parse(string network)
    {
        IPNetwork2.InternalParse(false, network, CidrGuess.ClassFull, true, out IPNetwork2 ipnetwork);
        return ipnetwork;
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
    /// <returns>An IPNetwork equivalent to the network contained in string network.</returns>
    public static IPNetwork2 Parse(string network, bool sanitanize)
    {
        IPNetwork2.InternalParse(false, network, CidrGuess.ClassFull, sanitanize, out IPNetwork2 ipnetwork);
        return ipnetwork;
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
    /// <param name="cidrGuess">A ICidrGuess implementation that will be used to guess CIDR during converion.</param>
    /// <returns>An IPNetwork equivalent to the network contained in string network.</returns>
    public static IPNetwork2 Parse(string network, ICidrGuess cidrGuess)
    {
        IPNetwork2.InternalParse(false, network, cidrGuess, true, out IPNetwork2 ipnetwork);
        return ipnetwork;
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
    /// <param name="cidrGuess">A ICidrGuess implementation that will be used to guess CIDR during converion.</param>
    /// <param name="sanitanize">Whether to sanitize network or not.</param>
    /// <returns>An IPNetwork equivalent to the network contained in string network.</returns>
    public static IPNetwork2 Parse(string network, ICidrGuess cidrGuess, bool sanitanize)
    {
        IPNetwork2.InternalParse(false, network, cidrGuess, sanitanize, out IPNetwork2 ipnetwork);
        return ipnetwork;
    }
}