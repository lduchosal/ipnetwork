
// <copyright file="TryGuessCidrUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// TryGuessCidrUnitTest test every single method
    /// </summary>
    [TestClass]
    public class TryGuessCidrUnitTest
    {

        [TestMethod]
        public void TestTryGuessCidrNull()
        {
            byte cidr;
            bool parsed = IPNetwork.TryGuessCidr(null, out cidr);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(0, cidr, "cidr");
        }

        [TestMethod]
        public void TestTryGuessCidrA()
        {
            byte cidr;
            bool parsed = IPNetwork.TryGuessCidr("10.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(8, cidr, "cidr");
        }

        [TestMethod]
        public void TestTryGuessCidrB()
        {
            byte cidr;
            bool parsed = IPNetwork.TryGuessCidr("172.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(16, cidr, "cidr");
        }

        [TestMethod]
        public void TestTryGuessCidrC()
        {
            byte cidr;
            bool parsed = IPNetwork.TryGuessCidr("192.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(24, cidr, "cidr");
        }

        [TestMethod]
        public void TestTryGuessCidrD()
        {
            byte cidr;
            bool parsed = IPNetwork.TryGuessCidr("224.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(24, cidr, "cidr");
        }

        [TestMethod]
        public void TestTryGuessCidrE()
        {
            byte cidr;
            bool parsed = IPNetwork.TryGuessCidr("240.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(24, cidr, "cidr");
        }

    }
}
