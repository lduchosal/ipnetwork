// <copyright file="IPNetwork2TryGuessCidr.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Net.Sockets;

/// <summary>
/// TryGuessCidr.
/// </summary>
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
        if (!byte.TryParse(sidr, out byte b))
        {
            cidr = null;
            return false;
        }

        if (!TryToNetmask(b, family, out IPAddress _))
        {
            cidr = null;
            return false;
        }

        cidr = b;
        return true;
    }
}