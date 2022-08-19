// <copyright file="BigIntegerExtensions.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace System.Net
{
    using System;
    using System.Net.Sockets;
    using System.Numerics;
    using System.Text;

    /// <summary>
    /// Extension methods to convert <see cref="System.Numerics.BigInteger"/>
    /// instances to hexadecimal, octal, and binary strings.
    /// </summary>
    public static class BigIntegerExtensions
    {
        /// <summary>
        /// Converts a <see cref="BigInteger"/> to a binary string.
        /// </summary>
        /// <param name="bigint">A <see cref="BigInteger"/>.</param>
        /// <returns>
        /// A <see cref="string"/> containing a binary
        /// representation of the supplied <see cref="BigInteger"/>.
        /// </returns>
        public static string ToBinaryString(this BigInteger bigint)
        {
            var bytes = bigint.ToByteArray();
            var idx = bytes.Length - 1;

            // Create a StringBuilder having appropriate capacity.
            var base2 = new StringBuilder(bytes.Length * 8);

            // Convert first byte to binary.
            var binary = Convert.ToString(bytes[idx], 2);

            // Ensure leading zero exists if value is positive.
            if (binary[0] != '0' && bigint.Sign == 1)
            {
                base2.Append('0');
            }

            // Append binary string to StringBuilder.
            base2.Append(binary);

            // Convert remaining bytes adding leading zeros.
            for (idx--; idx >= 0; idx--)
            {
                base2.Append(Convert.ToString(bytes[idx], 2).PadLeft(8, '0'));
            }

            return base2.ToString();
        }

        /// <summary>
        /// Converts a <see cref="BigInteger"/> to a hexadecimal string.
        /// </summary>
        /// <param name="bigint">A <see cref="BigInteger"/>.</param>
        /// <returns>
        /// A <see cref="string"/> containing a hexadecimal
        /// representation of the supplied <see cref="BigInteger"/>.
        /// </returns>
        public static string ToHexadecimalString(this BigInteger bigint)
        {
            return bigint.ToString("X");
        }

        /// <summary>
        /// Converts a <see cref="BigInteger"/> to a octal string.
        /// </summary>
        /// <param name="bigint">A <see cref="BigInteger"/>.</param>
        /// <returns>
        /// A <see cref="string"/> containing an octal
        /// representation of the supplied <see cref="BigInteger"/>.
        /// </returns>
        public static string ToOctalString(this BigInteger bigint)
        {
            var bytes = bigint.ToByteArray();
            var idx = bytes.Length - 1;

            // Create a StringBuilder having appropriate capacity.
            var base8 = new StringBuilder(((bytes.Length / 3) + 1) * 8);

            // Calculate how many bytes are extra when byte array is split
            // into three-byte (24-bit) chunks.
            var extra = bytes.Length % 3;

            // If no bytes are extra, use three bytes for first chunk.
            if (extra == 0)
            {
                extra = 3;
            }

            // Convert first chunk (24-bits) to integer value.
            int int24 = 0;
            for (; extra != 0; extra--)
            {
                int24 <<= 8;
                int24 += bytes[idx--];
            }

            // Convert 24-bit integer to octal without adding leading zeros.
            var octal = Convert.ToString(int24, 8);

            // Ensure leading zero exists if value is positive.
            if (octal[0] != '0')
            {
                if (bigint.Sign == 1)
                {
                    base8.Append('0');
                }
            }

            // Append first converted chunk to StringBuilder.
            base8.Append(octal);

            // Convert remaining 24-bit chunks, adding leading zeros.
            for (; idx >= 0; idx -= 3)
            {
                int24 = (bytes[idx] << 16) + (bytes[idx - 1] << 8) + bytes[idx - 2];
                base8.Append(Convert.ToString(int24, 8).PadLeft(8, '0'));
            }

            return base8.ToString();
        }

        /// <summary>
        ///
        /// Reverse a Positive BigInteger ONLY
        /// Bitwise ~ operator
        ///
        /// Input  : FF FF FF FF
        /// Width  : 4
        /// Result : 00 00 00 00
        ///
        ///
        /// Input  : 00 00 00 00
        /// Width  : 4
        /// Result : FF FF FF FF
        ///
        /// Input  : FF FF FF FF
        /// Width  : 8
        /// Result : FF FF FF FF 00 00 00 00
        ///
        ///
        /// Input  : 00 00 00 00
        /// Width  : 8
        /// Result : FF FF FF FF FF FF FF FF
        ///
        /// </summary>
        /// <param name="input"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static BigInteger PositiveReverse(this BigInteger input, int width)
        {
            var bytes = input.ToByteArray();
            var length = width + 1;

            // if the byte array is same size as output, we'll perform the operations in place
            var output = bytes.Length != length ? new byte[length] : bytes;

            // invert all of the source bytes
            for (var i = 0; i < bytes.Length - 1; i++)
            {
                output[i] = (byte)~bytes[i];
            }

            // invert the remainder of the output buffer
            for (var i = bytes.Length - 1; i < output.Length - 1; i++)
            {
                output[i] = byte.MaxValue;
            }

            // ensure output value is positive and return
            output[output.Length - 1] = 0;
            return new BigInteger(output);
        }

        #region ToIPAddress

        /// <summary>
        /// Transform a uint ipaddress into IPAddress object
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="family"></param>
        /// <returns></returns>
        public static IPAddress ToIPAddress(this BigInteger ipaddress, AddressFamily family)
        {
            int width = family == AddressFamily.InterNetwork ? 4 : 16;
            byte[] bytes = ipaddress.ToByteArray();
            byte[] bytes2 = new byte[width];
            int copy = bytes.Length > width ? width : bytes.Length;
            Array.Copy(bytes, 0, bytes2, 0, copy);
            Array.Reverse(bytes2);

            byte[] sized = Resize(bytes2, family);
            IPAddress ip = new IPAddress(sized);
            return ip;
        }

#if TRAVISCI
        public
#else
        internal
#endif
            static byte[] Resize(byte[] bytes, AddressFamily family)
        {
            if (family != AddressFamily.InterNetwork
                && family != AddressFamily.InterNetworkV6)
            {
                throw new ArgumentException("family");
            }

            int width = family == AddressFamily.InterNetwork ? 4 : 16;

            if (bytes.Length > width)
            {
                throw new ArgumentException("bytes");
            }

            byte[] result = new byte[width];
            Array.Copy(bytes, 0, result, 0, bytes.Length);

            return result;
        }

        #endregion

        #region BitsSet

        /// <summary>
        /// Count bits set to 1 in netmask
        /// </summary>
        /// <see href="http://stackoverflow.com/questions/109023/best-algorithm-to-count-the-number-of-set-bits-in-a-32-bit-integer"/>
        /// <param name="netmask"></param>
        /// <param name="family"></param>
        /// <returns></returns>
        public static byte BitsSet(this BigInteger netmask, AddressFamily family)
        {
            string s = netmask.ToBinaryString();

            return (byte)s.Replace("0", string.Empty)
                .ToCharArray()
                .Length;
        }
        #endregion
    }
}
