// <copyright file="EqualsUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace System.Net.TestProject
{
    [TestClass]
    public class EqualsUnitTest
    {
        #region Equals IPv4

        [TestMethod]
        public void TestEquals_ipv4_1()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.0.1/24");
            bool result = network1.Equals(network2);
            bool expected = true;

            Assert.AreEqual(expected, result, "equals");
        }

        [TestMethod]
        public void TestEquals_ipv4_2()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork network2 = null;
            bool result = network1.Equals(network2);
            bool expected = false;

            Assert.AreEqual(expected, result, "equals");
        }

        [TestMethod]
        public void TestEquals_ipv4_3()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            object network2 = string.Empty;
            bool result = network1.Equals(network2);
            bool expected = false;

            Assert.AreEqual(expected, result, "equals");
        }

        [TestMethod]
        public void TestEquals_ipv4_4()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.0.1/25");
            bool result = network1.Equals(network2);
            bool expected = false;

            Assert.AreEqual(expected, result, "equals");
        }

        [TestMethod]
        public void TestEquals_ipv4_5()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.1.1/24");
            bool result = network1.Equals(network2);
            bool expected = false;

            Assert.AreEqual(expected, result, "equals");
        }

        #endregion

        #region Equals IPv6

        [TestMethod]
        public void TestEquals_ipv6_1()
        {
            IPNetwork network1 = IPNetwork.Parse("::1/128");
            IPNetwork network2 = IPNetwork.Parse("::1/128");
            bool result = network1.Equals(network2);
            bool expected = true;

            Assert.AreEqual(expected, result, "equals");
        }

        [TestMethod]
        public void TestEquals_ipv6_2()
        {
            IPNetwork network1 = IPNetwork.Parse("::1/128");
            IPNetwork network2 = null;
            bool result = network1.Equals(network2);
            bool expected = false;

            Assert.AreEqual(expected, result, "equals");
        }

        [TestMethod]
        public void TestEquals_ipv6_3()
        {
            IPNetwork network1 = IPNetwork.Parse("::1/128");
            object network2 = string.Empty;
            bool result = network1.Equals(network2);
            bool expected = false;

            Assert.AreEqual(expected, result, "equals");
        }

        [TestMethod]
        public void TestEquals_ipv6_4()
        {
            IPNetwork network1 = IPNetwork.Parse("::1/128");
            IPNetwork network2 = IPNetwork.Parse("::1/127");
            bool result = network1.Equals(network2);
            bool expected = false;

            Assert.AreEqual(expected, result, "equals");
        }

        [TestMethod]
        public void TestEquals_ipv6_5()
        {
            IPNetwork network1 = IPNetwork.Parse("::1/128");
            IPNetwork network2 = IPNetwork.Parse("::10/128");
            bool result = network1.Equals(network2);
            bool expected = false;

            Assert.AreEqual(expected, result, "equals");
        }

        #endregion

        #region Equals IPv6 vs IPv4

        [TestMethod]
        public void TestEquals_ipv6_ipv4_0()
        {
            IPNetwork network1 = IPNetwork.Parse("::/32");
            IPNetwork network2 = IPNetwork.Parse("0.0.0.0/32");
            bool result = network1.Equals(network2);
            bool expected = false;

            Assert.AreEqual(expected, result, "equals");
        }

        #endregion
    }
}
