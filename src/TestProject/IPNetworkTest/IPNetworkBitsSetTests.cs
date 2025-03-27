// <copyright file="IPNetworkBitsSetTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest
{
    /// <summary>
    ///     Tests for bit set functionality in IPNetwork2.
    /// </summary>
    [TestClass]
    public class IPNetworkBitsSetTests
    {
        /// <summary>
        ///     Tests that BitsSet returns 32 for a 255.255.255.255 address.
        /// </summary>
        [TestMethod]
        public void TestBitsSet32()
    {
            var ip = IPAddress.Parse("255.255.255.255");
            uint bits = 32;
            uint result = IPNetwork2.BitsSet(ip);

            Assert.AreEqual(bits, result, "BitsSet");
        }

        /// <summary>
        ///     Tests that BitsSet returns 24 for a 255.255.255.0 address.
        /// </summary>
        [TestMethod]
        public void TestBitsSet24()
    {
            var ip = IPAddress.Parse("255.255.255.0");
            uint bits = 24;
            uint result = IPNetwork2.BitsSet(ip);

            Assert.AreEqual(bits, result, "BitsSet");
        }

        /// <summary>
        ///     Tests that BitsSet returns 16 for a 255.255.0.0 address.
        /// </summary>
        [TestMethod]
        public void TestBitsSet16()
    {
            var ip = IPAddress.Parse("255.255.0.0");
            uint bits = 16;
            uint result = IPNetwork2.BitsSet(ip);

            Assert.AreEqual(bits, result, "BitsSet");
        }

        /// <summary>
        ///     Tests that BitsSet returns 4 for a 128.128.128.128 address.
        /// </summary>
        [TestMethod]
        public void TestBitsSet4()
    {
            var ip = IPAddress.Parse("128.128.128.128");
            uint bits = 4;
            uint result = IPNetwork2.BitsSet(ip);

            Assert.AreEqual(bits, result, "BitsSet");
        }
    }
}