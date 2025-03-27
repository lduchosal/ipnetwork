// <copyright file="IPNetworkCountTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest
{
    /// <summary>
    ///     Tests for the Count property in IPNetwork2.
    /// </summary>
    [TestClass]
    public class IPNetworkCountTests
    {
        /// <summary>
        ///     Tests Count functionality with Total32.
        /// </summary>
        [TestMethod]
        public void TestTotal32()
        {
            var network = IPNetwork2.Parse("0.0.0.0/32");
            int total = 1;
            Assert.AreEqual(total, network.Total, "Total");
        }

        /// <summary>
        ///     Tests Count functionality with Total31.
        /// </summary>
        [TestMethod]
        public void TestTotal31()
        {
            var network = IPNetwork2.Parse("0.0.0.0/31");
            int total = 2;
            Assert.AreEqual(total, network.Total, "Total");
        }

        /// <summary>
        ///     Tests Count functionality with Total30.
        /// </summary>
        [TestMethod]
        public void TestTotal30()
        {
            var network = IPNetwork2.Parse("0.0.0.0/30");
            int total = 4;
            Assert.AreEqual(total, network.Total, "Total");
        }

        /// <summary>
        ///     Tests Count functionality with Total24.
        /// </summary>
        [TestMethod]
        public void TestTotal24()
        {
            var network = IPNetwork2.Parse("0.0.0.0/24");
            int total = 256;
            Assert.AreEqual(total, network.Total, "Total");
        }

        /// <summary>
        ///     Tests Count functionality with Total16.
        /// </summary>
        [TestMethod]
        public void TestTotal16()
        {
            var network = IPNetwork2.Parse("0.0.0.0/16");
            int total = 65536;
            Assert.AreEqual(total, network.Total, "Total");
        }

        /// <summary>
        ///     Tests Count functionality with Total8.
        /// </summary>
        [TestMethod]
        public void TestTotal8()
        {
            var network = IPNetwork2.Parse("0.0.0.0/8");
            int total = 16777216;
            Assert.AreEqual(total, network.Total, "Total");
        }

        /// <summary>
        ///     Tests Count functionality with Total0.
        /// </summary>
        [TestMethod]
        public void TestTotal0()
        {
            var network = IPNetwork2.Parse("0.0.0.0/0");
            long total = 4294967296;
            Assert.AreEqual(total, network.Total, "Total");
        }
    }
}