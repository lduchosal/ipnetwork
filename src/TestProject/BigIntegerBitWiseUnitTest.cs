// <copyright file="BigIntegerBitWiseUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net.TestProject
{
    using System.Numerics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BigIntegerBitWiseUnitTest
    {
        [TestMethod]
        public void Test1()
        {
            byte[] bytes = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x00 };
            BigInteger reverseme = new BigInteger(bytes);
            BigInteger reversed = reverseme.PositiveReverse(4);

            Assert.AreEqual(0, (int)reversed);
        }

        [TestMethod]
        public void Test2()
        {
            byte[] bytes = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x00 };
            BigInteger reverseme = new BigInteger(bytes);
            BigInteger reversed = reverseme.PositiveReverse(8);
            var result = reversed.ToByteArray();

            Assert.AreEqual(0x0, result[0]);
            Assert.AreEqual(0x0, result[1]);
            Assert.AreEqual(0x0, result[2]);
            Assert.AreEqual(0x0, result[3]);
            Assert.AreEqual(0xFF, result[4]);
            Assert.AreEqual(0xFF, result[5]);
            Assert.AreEqual(0xFF, result[6]);
            Assert.AreEqual(0xFF, result[7]);
        }
    }
}
