// <copyright file="IPNetwork2Print.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.IO;
public sealed partial class IPNetwork2
{
    /// <summary>
    /// Print an ipnetwork in a clear representation string.
    /// </summary>
    /// <returns>Dump an IPNetwork representation as string.</returns>
    public string Print()
    {
        using (var sw = new StringWriter())
        {
            sw.WriteLine("IPNetwork   : {0}", this.ToString());
            sw.WriteLine("Network     : {0}", this.Network);
            sw.WriteLine("Netmask     : {0}", this.Netmask);
            sw.WriteLine("Cidr        : {0}", this.Cidr);
            sw.WriteLine("Broadcast   : {0}", this.Broadcast);
            sw.WriteLine("FirstUsable : {0}", this.FirstUsable);
            sw.WriteLine("LastUsable  : {0}", this.LastUsable);
            sw.WriteLine("Usable      : {0}", this.Usable);

            return sw.ToString();
        }
    }

    /// <summary>
    /// Print an ipnetwork in a clear representation string.
    /// </summary>
    /// <param name="ipnetwork">The ipnetwork.</param>
    /// <returns>Dump an IPNetwork representation as string.</returns>
    /// <exception cref="ArgumentNullException">When arg is null.</exception>
    [Obsolete("static Print is deprecated, please use instance Print.")]
    public static string Print(IPNetwork2 ipnetwork)
    {
        if (ipnetwork == null)
        {
            throw new ArgumentNullException("ipnetwork");
        }

        return ipnetwork.Print();
    }
}