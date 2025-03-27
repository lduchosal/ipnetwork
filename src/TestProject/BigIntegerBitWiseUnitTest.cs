﻿// <copyright file="BigIntegerBitWiseUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject
{
    [TestClass]
    public class BigIntegerBitWiseUnitTest
    {
        [TestMethod]
        [DataRow(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x00 }, 4, new byte[] { 0x0 })]
        [DataRow(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x00 }, 8,
            new byte[] { 0x0, 0x0, 0x0, 0x0, 0xFF, 0xFF, 0xFF, 0xFF, 0x0 })]
        public void Test1(byte[] bytes, int reverseLength, byte[] expected)
        {
            var reverseme = new BigInteger(bytes);
            BigInteger reversed = reverseme.PositiveReverse(reverseLength);
            byte[] result = reversed.ToByteArray();
            Assert.AreEqual(expected.Length, result.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i], i.ToString());
            }
        }
    }
}