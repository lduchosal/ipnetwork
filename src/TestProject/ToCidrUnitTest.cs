
// <copyright file="ToCidrUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// ToCidrUnitTest test every single method
    /// </summary>
    [TestClass]
    public class ToCidrUnitTest
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestToCidrANE()
        {
            IPNetwork.ToCidr(null);
        }

        [TestMethod]
        public void TestToCidrAE()
        {
            byte cidr = IPNetwork.ToCidr(IPAddress.IPv6Any);
            Assert.AreEqual(0, cidr, "cidr");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestToCidrAE2()
        {
            IPNetwork.ToCidr(IPAddress.Parse("6.6.6.6"));
        }

        [TestMethod]
        public void TestToCidr32()
        {
            IPAddress mask = IPAddress.Parse("255.255.255.255");
            byte cidr = 32;
            int result = IPNetwork.ToCidr(mask);

            Assert.AreEqual(cidr, result, "cidr");
        }

        [TestMethod]
        public void TestToCidr24()
        {
            IPAddress mask = IPAddress.Parse("255.255.255.0");
            byte cidr = 24;
            int result = IPNetwork.ToCidr(mask);

            Assert.AreEqual(cidr, result, "cidr");
        }

        [TestMethod]
        public void TestToCidr16()
        {
            IPAddress mask = IPAddress.Parse("255.255.0.0");
            byte cidr = 16;
            int result = IPNetwork.ToCidr(mask);

            Assert.AreEqual(cidr, result, "cidr");
        }

        [TestMethod]
        public void TestToCidr8()
        {
            IPAddress mask = IPAddress.Parse("255.0.0.0");
            byte cidr = 8;
            int result = IPNetwork.ToCidr(mask);

            Assert.AreEqual(cidr, result, "cidr");
        }

        [TestMethod]
        public void TestToCidr0()
        {
            IPAddress mask = IPAddress.Parse("0.0.0.0");
            byte cidr = 0;
            int result = IPNetwork.ToCidr(mask);

            Assert.AreEqual(cidr, result, "cidr");
        }

    }
}
