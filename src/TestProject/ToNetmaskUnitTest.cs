
// <copyright file="ToNetmaskUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// ToNetmaskUnitTest test every single method
    /// </summary>
    [TestClass]
    public class ToNetmaskUnitTest
    {

        [TestMethod]
        public void ToNetmask32()
        {
            byte cidr = 32;
            string netmask = "255.255.255.255";
            string result = IPNetwork.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToNetmaskNonInet()
        {
            byte cidr = 0;
            string result = IPNetwork.ToNetmask(cidr, Sockets.AddressFamily.AppleTalk).ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToNetmaskNegative()
        {
            byte cidr = 0;
            cidr--;
            string result = IPNetwork.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();
        }

        [TestMethod]
        public void ToNetmaskInternal1()
        {
            IPAddress result;
            IPNetwork.InternalToNetmask(true, 0, Sockets.AddressFamily.AppleTalk, out result);
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void ToNetmask31()
        {
            byte cidr = 31;
            string netmask = "255.255.255.254";
            string result = IPNetwork.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        [TestMethod]
        public void ToNetmask30()
        {
            byte cidr = 30;
            string netmask = "255.255.255.252";
            string result = IPNetwork.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        [TestMethod]
        public void ToNetmask29()
        {
            byte cidr = 29;
            string netmask = "255.255.255.248";
            string result = IPNetwork.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        [TestMethod]
        public void ToNetmask1()
        {
            byte cidr = 1;
            string netmask = "128.0.0.0";
            string result = IPNetwork.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        [TestMethod]
        public void ToNetmask0()
        {
            byte cidr = 0;
            string netmask = "0.0.0.0";
            string result = IPNetwork.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToNetmaskOORE1()
        {
            byte cidr = 33;
            string result = IPNetwork.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();
        }

    }
}
