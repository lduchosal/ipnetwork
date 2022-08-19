
// <copyright file="CountUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// CountUnitTest test every single method
    /// </summary>
    [TestClass]
    public class CountUnitTest
    {

        [TestMethod]
        public void TestTotal32()
        {
            var network = IPNetwork.Parse("0.0.0.0/32");
            var total = 1;
            Assert.AreEqual(total, network.Total, "Total");
        }

        [TestMethod]
        public void TestTotal31()
        {
            var network = IPNetwork.Parse("0.0.0.0/31");
            var total = 2;
            Assert.AreEqual(total, network.Total, "Total");
        }

        [TestMethod]
        public void TestTotal30()
        {
            var network = IPNetwork.Parse("0.0.0.0/30");
            var total = 4;
            Assert.AreEqual(total, network.Total, "Total");
        }

        [TestMethod]
        public void TestTotal24()
        {
            var network = IPNetwork.Parse("0.0.0.0/24");
            var total = 256;
            Assert.AreEqual(total, network.Total, "Total");
        }

        [TestMethod]
        public void TestTotal16()
        {
            var network = IPNetwork.Parse("0.0.0.0/16");
            var total = 65536;
            Assert.AreEqual(total, network.Total, "Total");
        }

        [TestMethod]
        public void TestTotal8()
        {
            var network = IPNetwork.Parse("0.0.0.0/8");
            var total = 16777216;
            Assert.AreEqual(total, network.Total, "Total");
        }

        [TestMethod]
        public void TestTotal0()
        {
            var network = IPNetwork.Parse("0.0.0.0/0");
            var total = 4294967296;
            Assert.AreEqual(total, network.Total, "Total");
        }

    }
}
