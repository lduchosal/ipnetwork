// <copyright file="BigIntegerToUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using System.Numerics;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IPNetwork2.TestProject
{
    [TestClass]
    public class BigIntegerToUnitTest
    {
        [TestMethod]
        public void TestToOctalString1()
        {
            byte[] bytes = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x00 };
            var convertme = new BigInteger(bytes);
            string result = convertme.ToOctalString();

            Assert.AreEqual("037777777777", result);
        }

        [TestMethod]
        public void TestToOctalString3()
        {
            var bigi = BigInteger.Parse("1048576");
            bigi++;
            string result = bigi.ToOctalString();

            Assert.AreEqual("04000001", result);
        }

        [TestMethod]
        public void TestToOctalString01()
        {
            BigInteger bigi = BigInteger.Zero;
            bigi++;
            string result = bigi.ToOctalString();

            Assert.AreEqual("01", result);
        }

        [TestMethod]
        public void TestToOctalString02()
        {
            BigInteger bigi = BigInteger.Zero;
            bigi--;
            string result = bigi.ToOctalString();

            Assert.AreEqual("377", result);
        }

        [TestMethod]
        public void TestToOctalString03()
        {
            BigInteger bigi = BigInteger.Zero;
            bigi--;
            bigi--;
            bigi--;
            bigi--;
            bigi--;
            bigi--;
            bigi--;
            string result = bigi.ToOctalString();

            Assert.AreEqual("371", result);
        }

        [TestMethod]
        public void TestToHexadecimalString1()
        {
            byte[] bytes = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x00 };
            var convertme = new BigInteger(bytes);
            string result = convertme.ToHexadecimalString();

            Assert.AreEqual("0FFFFFFFF", result);
        }

        [TestMethod]
        public void TestToBinaryString1()
        {
            byte[] bytes = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x00 };
            var convertme = new BigInteger(bytes);
            string result = convertme.ToBinaryString();

            Assert.AreEqual("011111111111111111111111111111111", result);
        }

        [TestMethod]
        public void TestToBinaryString01()
        {
            BigInteger bigi = BigInteger.Zero;
            bigi++;
            string result = bigi.ToBinaryString();

            Assert.AreEqual("01", result);
        }

        [TestMethod]
        public void TestToBinaryString2()
        {
            var convertme = new BigInteger(-1);
            string result = convertme.ToBinaryString();

            Assert.AreEqual("11111111", result);
        }

        [TestMethod]
        public void TestToBinaryString3()
        {
            byte[] bytes = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            var convertme = new BigInteger(bytes);
            string result = convertme.ToBinaryString();

            Assert.AreEqual("11111111", result);
        }
    }
}
