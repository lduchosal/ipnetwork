// <copyright file="IPNetworkToNetmaskTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest
{
    [TestClass]
    public class IPNetworkToNetmaskTests
    {
        /// <summary>
        ///     Tests To Netmask functionality with a /32 network.
        /// </summary>
        [TestMethod]
        public void ToNetmask32()
        {
            byte cidr = 32;
            string netmask = "255.255.255.255";
            string result = IPNetwork2.ToNetmask(cidr, AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToNetmaskNonInet()
        {
            byte cidr = 0;
            string result = IPNetwork2.ToNetmask(cidr, AddressFamily.AppleTalk).ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToNetmaskNegative()
        {
            byte cidr = 0;
            cidr--;
            string result = IPNetwork2.ToNetmask(cidr, AddressFamily.InterNetwork).ToString();
        }

        /// <summary>
        ///     Tests To Netmask functionality with a /1 network.
        /// </summary>
        [TestMethod]
        public void ToNetmaskInternal1()
        {
            IPAddress result;
            IPNetwork2.InternalToNetmask(true, 0, AddressFamily.AppleTalk, out result);
            Assert.AreEqual(null, result);
        }

        /// <summary>
        ///     Tests To Netmask functionality with a /31 network.
        /// </summary>
        [TestMethod]
        public void ToNetmask31()
        {
            byte cidr = 31;
            string netmask = "255.255.255.254";
            string result = IPNetwork2.ToNetmask(cidr, AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        /// <summary>
        ///     Tests To Netmask functionality with a /30 network.
        /// </summary>
        [TestMethod]
        public void ToNetmask30()
        {
            byte cidr = 30;
            string netmask = "255.255.255.252";
            string result = IPNetwork2.ToNetmask(cidr, AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        /// <summary>
        ///     Tests To Netmask functionality with a /29 network.
        /// </summary>
        [TestMethod]
        public void ToNetmask29()
        {
            byte cidr = 29;
            string netmask = "255.255.255.248";
            string result = IPNetwork2.ToNetmask(cidr, AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        /// <summary>
        ///     Tests To Netmask functionality with a /1 network.
        /// </summary>
        [TestMethod]
        public void ToNetmask1()
        {
            byte cidr = 1;
            string netmask = "128.0.0.0";
            string result = IPNetwork2.ToNetmask(cidr, AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        /// <summary>
        ///     Tests To Netmask functionality with a /0 network.
        /// </summary>
        [TestMethod]
        public void ToNetmask0()
        {
            byte cidr = 0;
            string netmask = "0.0.0.0";
            string result = IPNetwork2.ToNetmask(cidr, AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToNetmaskOore1()
        {
            byte cidr = 33;
            string result = IPNetwork2.ToNetmask(cidr, AddressFamily.InterNetwork).ToString();
        }
    }
}