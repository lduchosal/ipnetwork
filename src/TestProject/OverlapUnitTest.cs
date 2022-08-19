
// <copyright file="OverlapUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// OverlapUnitTest test every single method
    /// </summary>
    [TestClass]
    public class OverlapUnitTest
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestOverlap1()
        {
            IPNetwork network1 = null;
            IPNetwork network2 = null;

#pragma warning disable 0618
            bool result = IPNetwork.Overlap(network1, network2);
#pragma warning restore 0618
        }

        [TestMethod]
        public void TestOverlapStatic2()
        {
            IPNetwork network1 = IPNetwork.IANA_ABLK_RESERVED1;
            IPNetwork network2 = IPNetwork.IANA_ABLK_RESERVED1;

#pragma warning disable 0618
            bool result = IPNetwork.Overlap(network1, network2);
#pragma warning restore 0618

            Assert.IsTrue(result, "result");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestOverlap2()
        {
            IPNetwork network1 = IPNetwork.Parse("10.0.0.0/0");
            IPNetwork network2 = null;
            network1.Overlap(network2);
        }

        [TestMethod]
        public void TestOverlap3()
        {
            IPNetwork network1 = IPNetwork.Parse("10.0.0.0/0");
            IPNetwork network2 = IPNetwork.Parse("10.0.0.0/0");
            bool result = network1.Overlap(network2);
            bool expected = true;

            Assert.AreEqual(expected, result, "overlap");
        }

        [TestMethod]
        public void TestOverlap4()
        {
            IPNetwork network1 = IPNetwork.Parse("10.10.0.0/16");
            IPNetwork network2 = IPNetwork.Parse("10.10.1.0/24");
            bool result = network1.Overlap(network2);
            bool expected = true;

            Assert.AreEqual(expected, result, "overlap");
        }

        [TestMethod]
        public void TestOverlap5()
        {
            IPNetwork network1 = IPNetwork.Parse("10.10.0.0/24");
            IPNetwork network2 = IPNetwork.Parse("10.10.1.0/24");
            bool result = network1.Overlap(network2);
            bool expected = false;

            Assert.AreEqual(expected, result, "overlap");
        }

        [TestMethod]
        public void TestOverlap6()
        {
            IPNetwork network1 = IPNetwork.Parse("10.10.1.0/24");
            IPNetwork network2 = IPNetwork.Parse("10.10.0.0/16");
            bool result = network1.Overlap(network2);
            bool expected = true;

            Assert.AreEqual(expected, result, "overlap");
        }

    }
}
