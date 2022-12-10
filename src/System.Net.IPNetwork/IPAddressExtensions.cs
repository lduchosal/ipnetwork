// <copyright file="IPAddressExtensions.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net
{
    /// <summary>
    /// A collection of extension functions applied to an IPAddress value.
    /// </summary>
    public static class IPAddressExtensions
    {
        /// <summary>
        /// Convert an IPAddress value into a single-address IPNetwork for that address.
        /// </summary>
        /// <param name="addr">IPAddress to convert.</param>
        /// <returns>IPNetwork object covering that IPAddress only.</returns>
        public static IPNetwork AsIPNetwork(this IPAddress addr)
        {
            /* IPv4? */
            if (addr.AddressFamily == Sockets.AddressFamily.InterNetwork)
            {
                /* Return address as a /32 network, the size of an IPv4 address. */
                return new IPNetwork(addr, 32);
            }

            /* IPV6? */
            if (addr.AddressFamily == Sockets.AddressFamily.InterNetworkV6)
            {
                /* Return address as a /128 network, the size of an IPv6 address. */
                return new IPNetwork(addr, 128);
            }

            /* No other families are supported. */
            throw new ArgumentException(
                $"AsIPNetwork does not support addresses in the {addr.AddressFamily} family.");
        }
    }
}
