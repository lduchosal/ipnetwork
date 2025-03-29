// <copyright file="IPNetwork2IANAblock.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

public sealed partial class IPNetwork2
{
    private static readonly Lazy<IPNetwork2> IanaAblockReserved = new (() => IPNetwork2.Parse("10.0.0.0/8"));
    private static readonly Lazy<IPNetwork2> IanaBblockReserved = new (() => IPNetwork2.Parse("172.16.0.0/12"));
    private static readonly Lazy<IPNetwork2> IanaCblockReserved = new (() => IPNetwork2.Parse("192.168.0.0/16"));

    /// <summary>
    /// Gets 10.0.0.0/8.
    /// </summary>
    /// <returns>The IANA reserved IPNetwork 10.0.0.0/8.</returns>
    public static IPNetwork2 IANA_ABLK_RESERVED1
    {
        get
        {
            return IanaAblockReserved.Value;
        }
    }

    /// <summary>
    /// Gets 172.12.0.0/12.
    /// </summary>
    /// <returns>The IANA reserved IPNetwork 172.12.0.0/12.</returns>
    public static IPNetwork2 IANA_BBLK_RESERVED1
    {
        get
        {
            return IanaBblockReserved.Value;
        }
    }

    /// <summary>
    /// Gets 192.168.0.0/16.
    /// </summary>
    /// <returns>The IANA reserved IPNetwork 192.168.0.0/16.</returns>
    public static IPNetwork2 IANA_CBLK_RESERVED1
    {
        get
        {
            return IanaCblockReserved.Value;
        }
    }

    /// <summary>
    /// return true if ipaddress is contained in
    /// IANA_ABLK_RESERVED1, IANA_BBLK_RESERVED1, IANA_CBLK_RESERVED1.
    /// </summary>
    /// <param name="ipaddress">A string containing an ip address to convert.</param>
    /// <returns>true if ipaddress is a IANA reserverd IP Netowkr ; otherwise, false.</returns>
    public static bool IsIANAReserved(IPAddress ipaddress)
    {
        if (ipaddress == null)
        {
            throw new ArgumentNullException("ipaddress");
        }

        return IPNetwork2.IANA_ABLK_RESERVED1.Contains(ipaddress)
               || IPNetwork2.IANA_BBLK_RESERVED1.Contains(ipaddress)
               || IPNetwork2.IANA_CBLK_RESERVED1.Contains(ipaddress);
    }

    /// <summary>
    /// return true if ipnetwork is contained in
    /// IANA_ABLK_RESERVED1, IANA_BBLK_RESERVED1, IANA_CBLK_RESERVED1.
    /// </summary>
    /// <returns>true if the ipnetwork is a IANA reserverd IP Netowkr ; otherwise, false.</returns>
    public bool IsIANAReserved()
    {
        return IPNetwork2.IANA_ABLK_RESERVED1.Contains(this)
               || IPNetwork2.IANA_BBLK_RESERVED1.Contains(this)
               || IPNetwork2.IANA_CBLK_RESERVED1.Contains(this);
    }

    /// <summary>
    /// Determines whether the specified IP network is reserved according to the IANA Reserved ranges.
    /// </summary>
    /// <param name="ipnetwork">The IP network to check.</param>
    /// <returns>
    /// <c>true</c> if the specified IP network is reserved according to the IANA Reserved ranges; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method is obsolete and should not be used. Please use the instance method <see cref="IsIANAReserved"/> instead.
    /// </para>
    /// <para>
    /// Throws an <see cref="ArgumentNullException"/> if <paramref name="ipnetwork"/> is <c>null</c>.
    /// </para>
    /// </remarks>
    [Obsolete("static IsIANAReserved is deprecated, please use instance IsIANAReserved.")]
    public static bool IsIANAReserved(IPNetwork2 ipnetwork)
    {
        if (ipnetwork == null)
        {
            throw new ArgumentNullException("ipnetwork");
        }

        return ipnetwork.IsIANAReserved();
    }
}