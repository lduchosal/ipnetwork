// <copyright file="BigIntegerExtensions.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net
{
    using System;
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
            byte[] bytes = bigint.ToByteArray();
            int idx = bytes.Length - 1;

            // Create a StringBuilder having appropriate capacity.
            var base2 = new StringBuilder(bytes.Length * 8);

            // Convert first byte to binary.
            string binary = Convert.ToString(bytes[idx], 2);

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
            byte[] bytes = bigint.ToByteArray();
            int idx = bytes.Length - 1;

            // Create a StringBuilder having appropriate capacity.
            var base8 = new StringBuilder(((bytes.Length / 3) + 1) * 8);

            // Calculate how many bytes are extra when byte array is split
            // into three-byte (24-bit) chunks.
            int extra = bytes.Length % 3;

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
            string octal = Convert.ToString(int24, 8);

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
        /// Result : FF FF FF FF FF FF FF FF.
        ///
        /// </summary>
        /// <param name="input">The positive number to bitwise reverse.</param>
        /// <param name="width">The width of the parameter.
        /// </param>
        /// <returns>A number representing the input bitwise reversed.</returns>
        public static BigInteger PositiveReverse(this BigInteger input, int width)
        {
            byte[] bytes = input.ToByteArray();
            int length = width + 1;

            // if the byte array is same size as output, we'll perform the operations in place
            byte[] output = bytes.Length != length ? new byte[length] : bytes;

            // invert all of the source bytes
            for (int i = 0; i < bytes.Length - 1; i++)
            {
                output[i] = (byte)~bytes[i];
            }

            // invert the remainder of the output buffer
            for (int i = bytes.Length - 1; i < output.Length - 1; i++)
            {
                output[i] = byte.MaxValue;
            }

            // ensure output value is positive and return
            output[output.Length - 1] = 0;
            return new BigInteger(output);
        }
    }
}
