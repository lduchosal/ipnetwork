
// <copyright file="ParseStringStringUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// ParseStringStringUnitTest test every single method
    /// </summary>
    [TestClass]
    public class ParseStringStringUnitTest
    {

        [TestMethod]
        public void TestParseStringString1()
        {
            string ipaddress = "192.168.168.100";
            string netmask = "255.255.255.0";

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress, netmask);
            Assert.AreEqual("192.168.168.0/24", ipnetwork.ToString(), "network");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseStringString2()
        {
            string ipaddress = null;
            string netmask = null;

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress, netmask);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseStringString3()
        {
            string ipaddress = "192.168.168.100";
            string netmask = null;

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress, netmask);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseStringString4()
        {
            string ipaddress = string.Empty;
            string netmask = string.Empty;

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress, netmask);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseStringString5()
        {
            string ipaddress = "192.168.168.100";
            string netmask = string.Empty;

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress, netmask);
        }

    }
}
