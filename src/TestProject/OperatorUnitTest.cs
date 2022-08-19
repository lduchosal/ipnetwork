
// <copyright file="OperatorUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// OperatorUnitTest test every single method
    /// </summary>
    [TestClass]
    public class OperatorUnitTest
    {

        [TestMethod]
        public void TestOperatorGreater1()
        {
            IPNetwork ipn1 = IPNetwork.Parse("10.0.0.1/32");
            IPNetwork ipn2 = IPNetwork.Parse("10.0.0.2/32");

            bool greater = ipn1 > ipn2;

            Assert.AreEqual(false, greater, "greater");
        }

        [TestMethod]
        public void TestOperatorGreater2()
        {
            IPNetwork ipn1 = IPNetwork.Parse("10.0.0.100/32");
            IPNetwork ipn2 = IPNetwork.Parse("10.0.0.2/32");

            bool greater = ipn1 > ipn2;

            Assert.AreEqual(true, greater, "greater");
        }

        [TestMethod]
        public void TestOperatorLower1()
        {
            IPNetwork ipn1 = IPNetwork.Parse("10.0.0.1/32");
            IPNetwork ipn2 = IPNetwork.Parse("10.0.0.2/32");

            bool lower = ipn1 < ipn2;

            Assert.AreEqual(true, lower, "lower");
        }

        [TestMethod]
        public void TestOperatorLower2()
        {
            IPNetwork ipn1 = IPNetwork.Parse("10.0.0.100/32");
            IPNetwork ipn2 = IPNetwork.Parse("10.0.0.2/32");

            bool lower = ipn1 < ipn2;

            Assert.AreEqual(false, lower, "lower");
        }

        [TestMethod]
        public void TestOperatorDifferent1()
        {
            IPNetwork ipn1 = IPNetwork.Parse("10.0.0.100/32");
            IPNetwork ipn2 = IPNetwork.Parse("10.0.0.2/32");

            bool different = ipn1 != ipn2;

            Assert.AreEqual(true, different, "different");
        }

        [TestMethod]
        public void TestOperatorDifferent2()
        {
            IPNetwork ipn1 = IPNetwork.Parse("10.0.0.1/32");
            IPNetwork ipn2 = IPNetwork.Parse("10.0.0.1/32");

            bool different = ipn1 != ipn2;

            Assert.AreEqual(false, different, "different");
        }

        [TestMethod]
        public void TestOperatorEqual1()
        {
            IPNetwork ipn1 = IPNetwork.Parse("10.0.0.100/32");
            IPNetwork ipn2 = IPNetwork.Parse("10.0.0.2/32");

            bool eq = ipn1 == ipn2;

            Assert.AreEqual(false, eq, "eq");
        }

        [TestMethod]
        public void TestOperatorEqual2()
        {
            IPNetwork ipn1 = IPNetwork.Parse("10.0.0.1/32");
            IPNetwork ipn2 = IPNetwork.Parse("10.0.0.1/32");

            bool eq = ipn1 == ipn2;

            Assert.AreEqual(true, eq, "eq");
        }

    }
}
