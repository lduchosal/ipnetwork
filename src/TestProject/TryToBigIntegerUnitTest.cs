
// <copyright file="TryToBigIntegerUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// TryToBigIntegerUnitTest test every single method
    /// </summary>
    [TestClass]
    public class TryToBigIntegerUnitTest
    {

        [TestMethod]
        public void TestTryToBigInteger32()
        {
            IPAddress mask = IPAddress.Parse("255.255.255.255");
            uint uintMask = 0xffffffff;
            BigInteger? result = null;
            bool parsed = IPNetwork.TryToBigInteger(mask, out result);

            Assert.AreEqual(uintMask, result, "uint");
            Assert.AreEqual(true, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryToBigInteger24()
        {
            IPAddress mask = IPAddress.Parse("255.255.255.0");
            uint uintMask = 0xffffff00;
            BigInteger? result = null;
            bool parsed = IPNetwork.TryToBigInteger(mask, out result);

            Assert.AreEqual(uintMask, result, "uint");
            Assert.AreEqual(true, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryToBigInteger16()
        {
            IPAddress mask = IPAddress.Parse("255.255.0.0");
            uint uintMask = 0xffff0000;
            BigInteger? result = null;
            bool parsed = IPNetwork.TryToBigInteger(mask, out result);

            Assert.AreEqual(uintMask, result, "uint");
            Assert.AreEqual(true, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryToBigInteger8()
        {
            IPAddress mask = IPAddress.Parse("255.0.0.0");
            uint uintMask = 0xff000000;

            BigInteger? result = null;
            bool parsed = IPNetwork.TryToBigInteger(mask, out result);

            Assert.AreEqual(uintMask, result, "uint");
            Assert.AreEqual(true, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryToBigInteger0()
        {
            IPAddress mask = IPAddress.Parse("0.0.0.0");
            uint uintMask = 0x00000000;
            BigInteger? result = null;
            bool parsed = IPNetwork.TryToBigInteger(mask, out result);

            Assert.AreEqual(uintMask, result, "uint");
            Assert.AreEqual(true, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryToBigIntegerANE()
        {
            BigInteger? result = null;
            bool parsed = IPNetwork.TryToBigInteger(null, out result);

            Assert.AreEqual(null, result, "uint");
            Assert.AreEqual(false, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryToBigIntegerANE3()
        {
            IPAddress ip = null;
            BigInteger? result = null;
            bool parsed = IPNetwork.TryToBigInteger(ip, out result);

            Assert.AreEqual(null, result, "uint");
            Assert.AreEqual(false, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryToBigIntegerANE2()
        {
            BigInteger? result = null;
            bool parsed = IPNetwork.TryToBigInteger(IPAddress.IPv6Any, out result);

            Assert.AreEqual(0, result, "result");
            Assert.AreEqual(true, parsed, "parsed");
        }

    }
}
