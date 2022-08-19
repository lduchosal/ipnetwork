// <copyright file="UintExtensions.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using System.Numerics;
using System.Net.Sockets;

namespace System.Net
{
    public static class UintExtensions
    {
        #region ToUint

        /// <summary>
        /// Convert a cidr to BigInteger netmask
        /// </summary>
        /// <param name="cidr"></param>
        /// <param name="family"></param>
        /// <returns></returns>
        public static BigInteger ToUint(this byte cidr, AddressFamily family)
        {
            InternalToBigInteger(false, cidr, family, out var uintNetmask);

            return (BigInteger)uintNetmask;
        }

        /// <summary>
        /// Convert a cidr to uint netmask
        /// </summary>
        /// <param name="cidr"></param>
        /// <param name="family"></param>
        /// <param name="uintNetmask"></param>
        /// <returns></returns>
        public static bool TryToUint(this byte cidr, AddressFamily family, out BigInteger? uintNetmask)
        {
            InternalToBigInteger(true, cidr, family, out var uintNetmask2);
            bool parsed = uintNetmask2 != null;
            uintNetmask = uintNetmask2;

            return parsed;
        }

        /// <summary>
        /// Convert a cidr to uint netmask
        /// </summary>
        /// <param name="tryParse"></param>
        /// <param name="cidr"></param>
        /// <param name="family"></param>
        /// <param name="uintNetmask"></param>
        /// <returns></returns>
#if TRAVISCI
        public
#else
        internal
#endif
            static void InternalToBigInteger(bool tryParse, byte cidr, AddressFamily family, out BigInteger? uintNetmask)
        {
            if (family == AddressFamily.InterNetwork && cidr > 32)
            {
                if (tryParse == false)
                {
                    throw new ArgumentOutOfRangeException("cidr");
                }

                uintNetmask = null;
                return;
            }

            if (family == AddressFamily.InterNetworkV6 && cidr > 128)
            {
                if (tryParse == false)
                {
                    throw new ArgumentOutOfRangeException("cidr");
                }

                uintNetmask = null;
                return;
            }

            if (family != AddressFamily.InterNetwork
                && family != AddressFamily.InterNetworkV6)
            {
                if (tryParse == false)
                {
                    throw new NotSupportedException(family.ToString());
                }

                uintNetmask = null;
                return;
            }

            if (family == AddressFamily.InterNetwork)
            {
                uintNetmask = cidr == 0 ? 0 : 0xffffffff << (32 - cidr);
                return;
            }

            BigInteger mask = new BigInteger(new byte[]
            {
                0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff,
                0x00,
            });

            BigInteger masked = cidr == 0 ? 0 : mask << (128 - cidr);
            byte[] m = masked.ToByteArray();
            byte[] bmask = new byte[17];
            int copy = m.Length > 16 ? 16 : m.Length;
            Array.Copy(m, 0, bmask, 0, copy);
            uintNetmask = new BigInteger(bmask);
        }

        #endregion

        #region ToNetmask

        /// <summary>
        /// Convert CIDR to netmask
        ///  24 -> 255.255.255.0
        ///  16 -> 255.255.0.0
        ///  8 -> 255.0.0.0
        /// </summary>
        /// <see href="http://snipplr.com/view/15557/cidr-class-for-ipv4/"/>
        /// <param name="cidr"></param>
        /// <param name="family"></param>
        /// <returns></returns>
        public static IPAddress ToNetmask(this byte cidr, AddressFamily family)
        {
            InternalToNetmask(false, cidr, family, out var netmask);

            return netmask;
        }

        /// <summary>
        /// Convert CIDR to netmask
        ///  24 -> 255.255.255.0
        ///  16 -> 255.255.0.0
        ///  8 -> 255.0.0.0
        /// </summary>
        /// <see href="http://snipplr.com/view/15557/cidr-class-for-ipv4/"/>
        /// <param name="cidr"></param>
        /// <param name="family"></param>
        /// <param name="netmask"></param>
        /// <returns></returns>
        public static bool TryToNetmask(this byte cidr, AddressFamily family, out IPAddress netmask)
        {
            InternalToNetmask(true, cidr, family, out var netmask2);
            bool parsed = netmask2 != null;
            netmask = netmask2;

            return parsed;
        }

#if TRAVISCI
        public
#else
        internal
#endif
            static void InternalToNetmask(bool tryParse, byte cidr, AddressFamily family, out IPAddress netmask)
        {
            if (family != AddressFamily.InterNetwork
                && family != AddressFamily.InterNetworkV6)
            {
                if (tryParse == false)
                {
                    throw new ArgumentException("family");
                }

                netmask = null;
                return;
            }

            // 20180217 lduchosal
            // impossible to reach code, byte cannot be negative :
            //
            // if (cidr < 0) {
            //     if (tryParse == false) {
            //         throw new ArgumentOutOfRangeException("cidr");
            //     }
            //     netmask = null;
            //     return;
            // }
            int maxCidr = family == Sockets.AddressFamily.InterNetwork ? 32 : 128;
            if (cidr > maxCidr)
            {
                if (tryParse == false)
                {
                    throw new ArgumentOutOfRangeException("cidr");
                }

                netmask = null;
                return;
            }

            BigInteger mask = cidr.ToUint(family);
            IPAddress netmask2 = mask.ToIPAddress(family);
            netmask = netmask2;

            return;
        }

        #endregion
    }
}