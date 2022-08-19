
// <copyright file="ToBigIntegerUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// ToBigIntegerUnitTest test every single method
    /// </summary>
    [TestClass]
    public class ToBigIntegerUnitTest
    {

        [TestMethod]
        public void TestToBigInteger32()
        {
            IPAddress mask = IPAddress.Parse("255.255.255.255");
            uint uintMask = 0xffffffff;
            BigInteger result = IPNetwork.ToBigInteger(mask);

            Assert.AreEqual(uintMask, result, "uint");
        }

        [TestMethod]
        public void TestToBigInteger24()
        {
            IPAddress mask = IPAddress.Parse("255.255.255.0");
            uint uintMask = 0xffffff00;
            BigInteger? result = IPNetwork.ToBigInteger(mask);

            Assert.AreEqual(uintMask, result, "uint");
        }

        [TestMethod]
        public void TestToBigInteger16()
        {
            IPAddress mask = IPAddress.Parse("255.255.0.0");
            uint uintMask = 0xffff0000;
            BigInteger? result = IPNetwork.ToBigInteger(mask);

            Assert.AreEqual(uintMask, result, "uint");
        }

        [TestMethod]
        public void TestToBigInteger8()
        {
            IPAddress mask = IPAddress.Parse("255.0.0.0");
            uint uintMask = 0xff000000;
            BigInteger? result = IPNetwork.ToBigInteger(mask);

            Assert.AreEqual(uintMask, result, "uint");
        }

        [TestMethod]
        public void TestToBigInteger0()
        {
            IPAddress mask = IPAddress.Parse("0.0.0.0");
            uint uintMask = 0x00000000;
            BigInteger? result = IPNetwork.ToBigInteger(mask);

            Assert.AreEqual(uintMask, result, "uint");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestToBigIntegerANE()
        {
            BigInteger? result = IPNetwork.ToBigInteger(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestToBigIntegerANE3()
        {
            IPAddress ip = null;
            BigInteger? result = IPNetwork.ToBigInteger(ip);
        }

        [TestMethod]
        public void TestToBigIntegerANE2()
        {
            BigInteger? result = IPNetwork.ToBigInteger(IPAddress.IPv6Any);
            uint expected = 0;
            Assert.AreEqual(expected, result, "result");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestToBigIntegerByte()
        {
            BigInteger result = IPNetwork.ToUint(33, Sockets.AddressFamily.InterNetwork);
        }

        [TestMethod]
        public void TestToBigIntegerByte2()
        {
            BigInteger result = IPNetwork.ToUint(32, Sockets.AddressFamily.InterNetwork);
            uint expected = 4294967295;
            Assert.AreEqual(expected, result, "result");
        }

        [TestMethod]
        public void TestToBigIntegerByte3()
        {
            BigInteger result = IPNetwork.ToUint(0, Sockets.AddressFamily.InterNetwork);
            uint expected = 0;
            Assert.AreEqual(expected, result, "result");
        }

        [TestMethod]
        public void TestToBigIntegerInternal1()
        {
            BigInteger? result = null;
            IPNetwork.InternalToBigInteger(true, 33, Sockets.AddressFamily.InterNetwork, out result);
            Assert.AreEqual(null, result, "result");
        }

        [TestMethod]
        public void TestToBigIntegerInternal2()
        {
            BigInteger? result = null;
            IPNetwork.InternalToBigInteger(true, 129, Sockets.AddressFamily.InterNetworkV6, out result);
            Assert.AreEqual(null, result, "result");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestToBigIntegerInternal3()
        {
            BigInteger? result = null;
            IPNetwork.InternalToBigInteger(false, 129, Sockets.AddressFamily.InterNetworkV6, out result);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestToBigIntegerInternal4()
        {
            BigInteger? result = null;
            IPNetwork.InternalToBigInteger(false, 32, Sockets.AddressFamily.AppleTalk, out result);
        }

        [TestMethod]
        public void TestToBigIntegerInternal5()
        {
            BigInteger? result = null;
            IPNetwork.InternalToBigInteger(true, 32, Sockets.AddressFamily.AppleTalk, out result);
            Assert.AreEqual(null, result, "result");
        }

    }
}
