// <copyright file="IPNetwork2TryGuessCidr.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Net.Sockets;

public sealed partial class IPNetwork2
{
    /// <summary>
    /// Delegate to CidrGuess ClassFull guessing of cidr.
    /// </summary>
    /// <param name="ip">A string representing an IPAdress that will be used to guess the corresponding CIDR.</param>
    /// <param name="cidr">The resulting CIDR as byte.</param>
    /// <returns>true if cidr was guessed successfully; otherwise, false.</returns>
    public static bool TryGuessCidr(string ip, out byte cidr)
    {
        return CidrGuess.ClassFull.TryGuessCidr(ip, out cidr);
    }

    /// <summary>
    /// Try to parse cidr. Have to be >= 0 and &lt;= 32 or 128.
    /// </summary>
    /// <param name="sidr">A string representing a byte CIRD (/24).</param>
    /// <param name="family">Either IPv4 or IPv6.</param>
    /// <param name="cidr">The resulting CIDR as byte.</param>
    /// <returns>true if cidr was converted successfully; otherwise, false.</returns>
    public static bool TryParseCidr(string sidr, AddressFamily family, out byte? cidr)
    {
        byte b = 0;
        if (!byte.TryParse(sidr, out b))
        {
            cidr = null;
            return false;
        }

        if (!IPNetwork2.TryToNetmask(b, family, out IPAddress netmask))
        {
            cidr = null;
            return false;
        }

        cidr = b;
        return true;
    }
}