// <copyright file="CidrClassFull.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net
{
    using System.Net.Sockets;

    /// <summary>
    /// Class <c>CidrClassFull</c> tries to guess CIDR in a ClassFull way.
    /// </summary>
    public sealed class CidrClassFull : ICidrGuess
    {
        /// <summary>
        ///
        /// IPV4 :
        ///
        /// Class              Leading bits    Default netmask
        ///     A (CIDR /8)        00           255.0.0.0
        ///     A (CIDR /8)        01           255.0.0.0
        ///     B (CIDR /16)       10           255.255.0.0
        ///     C (CIDR /24)       11           255.255.255.0
        ///
        /// IPV6 : 64.
        ///
        /// </summary>
        /// <param name="ip">A string representing the CIDR to convert.</param>
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
                cidr = 64;
                return true;
            }

            var uintIPAddress = IPNetwork.ToBigInteger(ipaddress);
            uintIPAddress = uintIPAddress >> 30;
            if (uintIPAddress <= 1)
            {
                cidr = 8;
                return true;
            }
            else if (uintIPAddress <= 2)
            {
                cidr = 16;
                return true;
            }
            else if (uintIPAddress <= 3)
            {
                cidr = 24;
                return true;
            }

            cidr = 0;
            return false;
        }
    }
}
