// <copyright file="CidrClassLess.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net
{
    using System.Net.Sockets;

    /// <summary>
    /// Try to guess a CIDR in a ClassLess way ie. ipv4 = 32, ipv6 = 128.
    /// </summary>
    public sealed class CidrClassLess : ICidrGuess
    {
        /// <summary>
        ///
        /// IPV4 : 32
        /// IPV6 : 128.
        ///
        /// </summary>
        /// <param name="ip">A string representing an ipadress that will be used to guess CIDR.</param>
        /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
        /// <returns>true if ip was converted successfully; otherwise, false.</returns>
        public bool TryGuessCidr(string ip, out byte cidr)
        {
            IPAddress ipaddress = null;
            bool parsed = IPAddress.TryParse(string.Format("{0}", ip), out ipaddress);
            if (parsed == false)
            {
                cidr = 0;
                return false;
            }

            if (ipaddress.AddressFamily == AddressFamily.InterNetworkV6)
            {
                cidr = 128;
                return true;
            }

            cidr = 32;
            return true;
        }
    }
}
