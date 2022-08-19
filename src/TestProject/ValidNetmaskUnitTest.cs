
// <copyright file="ValidNetmaskUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// ValidNetmaskUnitTest test every single method
    /// </summary>
    [TestClass]
    public class ValidNetmaskUnitTest
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestValidNetmaskInvalid1()
        {
            var resut = IPNetwork.InternalValidNetmask(BigInteger.Zero, Sockets.AddressFamily.AppleTalk);
        }

        [TestMethod]
        public void TestValidNetmask0()
        {
            IPAddress mask = IPAddress.Parse("255.255.255.255");
            bool expected = true;
            bool result = IPNetwork.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

        [TestMethod]
        public void TestValidNetmask1()
        {
            IPAddress mask = IPAddress.Parse("255.255.255.0");
            bool expected = true;
            bool result = IPNetwork.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

        [TestMethod]
        public void TestValidNetmask2()
        {
            IPAddress mask = IPAddress.Parse("255.255.0.0");
            bool expected = true;
            bool result = IPNetwork.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

        [TestMethod]
        public void TestValidNetmaskEAE1()
        {
            IPAddress mask = IPAddress.Parse("0.255.0.0");
            bool expected = false;
            bool result = IPNetwork.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestValidNetmaskEAE2()
        {
            IPAddress mask = null;
            bool expected = true;
            bool result = IPNetwork.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

        [TestMethod]
        public void TestValidNetmaskEAE3()
        {
            IPAddress mask = IPAddress.Parse("255.255.0.1");
            bool expected = false;
            bool result = IPNetwork.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

    }
}
