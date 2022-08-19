
// <copyright file="TryToCidrUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// TryToCidrUnitTest test every single method
    /// </summary>
    [TestClass]
    public class TryToCidrUnitTest
    {

        [TestMethod]
        public void TestTryToCidrANE()
        {
            byte? cidr = null;
            bool parsed = IPNetwork.TryToCidr(null, out cidr);
            Assert.AreEqual(false, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryToCidrAE()
        {
            byte? cidr = null;
            bool parsed = IPNetwork.TryToCidr(IPAddress.IPv6Any, out cidr);
            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual((byte)0, cidr, "cidr");
        }

        [TestMethod]
        public void TestTryToCidrAE2()
        {
            byte? cidr = null;
            bool parsed = IPNetwork.TryToCidr(IPAddress.Parse("6.6.6.6"), out cidr);
            Assert.AreEqual(false, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryToCidr32()
        {
            byte? cidr = null;
            IPAddress mask = IPAddress.Parse("255.255.255.255");
            byte result = 32;
            bool parsed = IPNetwork.TryToCidr(mask, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(cidr, result, "cidr");
        }

        [TestMethod]
        public void TestTryToCidr24()
        {
            byte? cidr = null;
            IPAddress mask = IPAddress.Parse("255.255.255.0");
            byte result = 24;
            bool parsed = IPNetwork.TryToCidr(mask, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(cidr, result, "cidr");
        }

        [TestMethod]
        public void TestTryToCidr16()
        {
            byte? cidr = null;
            IPAddress mask = IPAddress.Parse("255.255.0.0");
            byte result = 16;
            bool parsed = IPNetwork.TryToCidr(mask, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(cidr, result, "cidr");
        }

        [TestMethod]
        public void TestTryToCidr8()
        {
            byte? cidr = null;
            IPAddress mask = IPAddress.Parse("255.0.0.0");
            byte result = 8;
            bool parsed = IPNetwork.TryToCidr(mask, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(cidr, result, "cidr");
        }

        [TestMethod]
        public void TestTryToCidr0()
        {
            byte? cidr = null;
            IPAddress mask = IPAddress.Parse("0.0.0.0");
            byte result = 0;
            bool parsed = IPNetwork.TryToCidr(mask, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(cidr, result, "cidr");
        }

    }
}
