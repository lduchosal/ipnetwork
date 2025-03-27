// <copyright file="IPNetworkUsableTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest
{
    [TestClass]
    public class IPNetworkUsableTests
    {
        /// <summary>
        ///     Tests Usable functionality with a /32 network.
        /// </summary>
        [TestMethod]
        public void Usable32()
        {
            var network = IPNetwork2.Parse("0.0.0.0/32");
            uint usable = 0;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        /// <summary>
        ///     Tests Usable functionality with a /31 network.
        /// </summary>
        [TestMethod]
        public void Usable31()
        {
            var network = IPNetwork2.Parse("0.0.0.0/31");
            uint usable = 0;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        /// <summary>
        ///     Tests Usable functionality with a /30 network.
        /// </summary>
        [TestMethod]
        public void Usable30()
        {
            var network = IPNetwork2.Parse("0.0.0.0/30");
            uint usable = 2;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        /// <summary>
        ///     Tests Usable functionality with a /24 network.
        /// </summary>
        [TestMethod]
        public void Usable24()
        {
            var network = IPNetwork2.Parse("0.0.0.0/24");
            uint usable = 254;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        /// <summary>
        ///     Tests Usable functionality with a /16 network.
        /// </summary>
        [TestMethod]
        public void Usable16()
        {
            var network = IPNetwork2.Parse("0.0.0.0/16");
            uint usable = 65534;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        /// <summary>
        ///     Tests Usable functionality with a /8 network.
        /// </summary>
        [TestMethod]
        public void Usable8()
        {
            var network = IPNetwork2.Parse("0.0.0.0/8");
            uint usable = 16777214;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        /// <summary>
        ///     Tests Usable functionality with a /0 network.
        /// </summary>
        [TestMethod]
        public void Usable0()
        {
            var network = IPNetwork2.Parse("0.0.0.0/0");
            uint usable = 4294967294;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }
    }
}