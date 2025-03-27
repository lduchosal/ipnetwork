// <copyright file="IPNetworkTryToCidrTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest
{
    [TestClass]
    public class IPNetworkTryToCidrTests
    {
        /// <summary>
        ///     Tests Try To Cidr functionality with Try To Cidr ANE.
        /// </summary>
        [TestMethod]
        public void TestTryToCidrAne()
        {
            byte? cidr = null;
            bool parsed = IPNetwork2.TryToCidr(null, out cidr);
            Assert.AreEqual(false, parsed, "parsed");
        }

        /// <summary>
        ///     Tests Try To Cidr functionality with Try To Cidr AE.
        /// </summary>
        [TestMethod]
        public void TestTryToCidrAe()
        {
            byte? cidr = null;
            bool parsed = IPNetwork2.TryToCidr(IPAddress.IPv6Any, out cidr);
            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual((byte)0, cidr, "cidr");
        }

        /// <summary>
        ///     Tests Try To Cidr functionality with Try To Cidr AE2.
        /// </summary>
        [TestMethod]
        public void TestTryToCidrAe2()
        {
            byte? cidr = null;
            bool parsed = IPNetwork2.TryToCidr(IPAddress.Parse("6.6.6.6"), out cidr);
            Assert.AreEqual(false, parsed, "parsed");
        }

        /// <summary>
        ///     Tests Try To Cidr functionality with Try To Cidr32.
        /// </summary>
        [TestMethod]
        public void TestTryToCidr32()
        {
            byte? cidr = null;
            var mask = IPAddress.Parse("255.255.255.255");
            byte result = 32;
            bool parsed = IPNetwork2.TryToCidr(mask, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(cidr, result, "cidr");
        }

        /// <summary>
        ///     Tests Try To Cidr functionality with Try To Cidr24.
        /// </summary>
        [TestMethod]
        public void TestTryToCidr24()
        {
            byte? cidr = null;
            var mask = IPAddress.Parse("255.255.255.0");
            byte result = 24;
            bool parsed = IPNetwork2.TryToCidr(mask, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(cidr, result, "cidr");
        }

        /// <summary>
        ///     Tests Try To Cidr functionality with Try To Cidr16.
        /// </summary>
        [TestMethod]
        public void TestTryToCidr16()
        {
            byte? cidr = null;
            var mask = IPAddress.Parse("255.255.0.0");
            byte result = 16;
            bool parsed = IPNetwork2.TryToCidr(mask, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(cidr, result, "cidr");
        }

        /// <summary>
        ///     Tests Try To Cidr functionality with Try To Cidr8.
        /// </summary>
        [TestMethod]
        public void TestTryToCidr8()
        {
            byte? cidr = null;
            var mask = IPAddress.Parse("255.0.0.0");
            byte result = 8;
            bool parsed = IPNetwork2.TryToCidr(mask, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(cidr, result, "cidr");
        }

        /// <summary>
        ///     Tests Try To Cidr functionality with Try To Cidr0.
        /// </summary>
        [TestMethod]
        public void TestTryToCidr0()
        {
            byte? cidr = null;
            var mask = IPAddress.Parse("0.0.0.0");
            byte result = 0;
            bool parsed = IPNetwork2.TryToCidr(mask, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(cidr, result, "cidr");
        }
    }
}