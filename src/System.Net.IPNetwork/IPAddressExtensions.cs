// <copyright file="IPAddressExtensions.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using System.Net.Sockets;
using System.Numerics;

namespace System.Net
{
    public static class IPAddressExtensions
    {
        /// <summary>
        /// Convert an ipadress to decimal
        /// 0.0.0.0 -> 0
        /// 0.0.1.0 -> 256
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <returns></returns>
        public static BigInteger ToBigInteger(this IPAddress ipaddress)
        {
            InternalToBigInteger(false, ipaddress, out var uintIpAddress);

            return (BigInteger)uintIpAddress;
        }

        /// <summary>
        /// Convert an ipadress to decimal
        /// 0.0.0.0 -> 0
        /// 0.0.1.0 -> 256
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="uintIpAddress"></param>
        /// <returns></returns>
        public static bool TryToBigInteger(this IPAddress ipaddress, out BigInteger? uintIpAddress)
        {
            InternalToBigInteger(true, ipaddress, out var uintIpAddress2);
            bool parsed = uintIpAddress2 != null;
            uintIpAddress = uintIpAddress2;

            return parsed;
        }

#if TRAVISCI
        public
#else
        internal
#endif
            static void InternalToBigInteger(bool tryParse, IPAddress ipaddress, out BigInteger? uintIpAddress)
        {
            if (ipaddress == null)
            {
                if (tryParse == false)
                {
                    throw new ArgumentNullException("ipaddress");
                }

                uintIpAddress = null;
                return;
            }

#if NET5_0 || NETSTANDARD2_1
            byte[] bytes = ipaddress.AddressFamily == AddressFamily.InterNetwork ? new byte[4] : new byte[16];
            var span = bytes.AsSpan();
            if (!ipaddress.TryWriteBytes(span, out _))
            {
                if (tryParse == false)
                {
                    throw new ArgumentException("ipaddress");
                }

                uintIpAddress = null;
                return;
            }

            uintIpAddress = new BigInteger(span, isUnsigned: true, isBigEndian: true);
#elif NET45 || NET46 || NET47 || NETSTANDARD20
            byte[] bytes = ipaddress.GetAddressBytes();
            bytes.AsSpan().Reverse();

            // add trailing 0 to make unsigned
            var unsigned = new byte[bytes.Length + 1];
            Buffer.BlockCopy(bytes, 0, unsigned, 0, bytes.Length);
            uintIpAddress = new BigInteger(unsigned);
#else
            byte[] bytes = ipaddress.GetAddressBytes();
            Array.Reverse(bytes);

            // add trailing 0 to make unsigned
            var unsigned = new byte[bytes.Length + 1];
            Buffer.BlockCopy(bytes, 0, unsigned, 0, bytes.Length);
            uintIpAddress = new BigInteger(unsigned);
#endif
        }

        #region ToCidr

        /// <summary>
        /// Convert netmask to CIDR
        ///  255.255.255.0 -> 24
        ///  255.255.0.0   -> 16
        ///  255.0.0.0     -> 8
        /// </summary>
        /// <param name="tryParse"></param>
        /// <param name="netmask"></param>
        /// <param name="family"></param>
        /// <param name="cidr"></param>
        /// <returns></returns>
        private static void InternalToCidr(bool tryParse, BigInteger netmask, AddressFamily family, out byte? cidr)
        {
            if (!IPNetwork.InternalValidNetmask(netmask, family))
            {
                if (tryParse == false)
                {
                    throw new ArgumentException("netmask");
                }

                cidr = null;
                return;
            }

            byte cidr2 = netmask.BitsSet(family);
            cidr = cidr2;

            return;
        }

        /// <summary>
        /// Convert netmask to CIDR
        ///  255.255.255.0 -> 24
        ///  255.255.0.0   -> 16
        ///  255.0.0.0     -> 8
        /// </summary>
        /// <param name="netmask"></param>
        /// <returns></returns>
        public static byte ToCidr(this IPAddress netmask)
        {
            InternalToCidr(false, netmask, out var cidr);
            return (byte)cidr;
        }

        /// <summary>
        /// Convert netmask to CIDR
        ///  255.255.255.0 -> 24
        ///  255.255.0.0   -> 16
        ///  255.0.0.0     -> 8
        /// </summary>
        /// <param name="netmask"></param>
        /// <param name="cidr"></param>
        /// <returns></returns>
        public static bool TryToCidr(this IPAddress netmask, out byte? cidr)
        {
            InternalToCidr(true, netmask, out var cidr2);
            bool parsed = cidr2 != null;
            cidr = cidr2;
            return parsed;
        }

        private static void InternalToCidr(bool tryParse, IPAddress netmask, out byte? cidr)
        {
            if (netmask == null)
            {
                if (tryParse == false)
                {
                    throw new ArgumentNullException("netmask");
                }

                cidr = null;
                return;
            }

            bool parsed = netmask.TryToBigInteger(out var uintNetmask2);

            // 20180217 lduchosal
            // impossible to reach code.
            // if (parsed == false) {
            //     if (tryParse == false) {
            //         throw new ArgumentException("netmask");
            //     }
            //     cidr = null;
            //     return;
            // }
            BigInteger uintNetmask = (BigInteger)uintNetmask2;

            InternalToCidr(tryParse, uintNetmask, netmask.AddressFamily, out var cidr2);
            cidr = cidr2;

            return;
        }

        #endregion

        /// <summary>
        /// Count bits set to 1 in netmask
        /// </summary>
        /// <param name="netmask"></param>
        /// <returns></returns>
        public static uint BitsSet(this IPAddress netmask)
        {
            BigInteger uintNetmask = netmask.ToBigInteger();
            uint bits = uintNetmask.BitsSet(netmask.AddressFamily);

            return bits;
        }
    }
}
