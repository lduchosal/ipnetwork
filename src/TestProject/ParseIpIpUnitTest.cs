
// <copyright file="ParseIpIpUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// ParseIpIpUnitTest test every single method
    /// </summary>
    [TestClass]
    public class ParseIpIpUnitTest
    {

        [TestMethod]
        public void ParseIpIp1()
        {
            string ipaddress = "192.168.168.100";
            string netmask = "255.255.255.0";
            IPAddress ip = IPAddress.Parse(ipaddress);
            IPAddress netm = IPAddress.Parse(netmask);
            IPNetwork ipnetwork = IPNetwork.Parse(ip, netm);
            Assert.AreEqual("192.168.168.0/24", ipnetwork.ToString(), "network");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseIpIp2()
        {
            IPAddress ip = null;
            IPAddress netm = null;
            IPNetwork ipnetwork = IPNetwork.Parse(ip, netm);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseIpIp3()
        {
            string ipaddress = "192.168.168.100";
            IPAddress ip = IPAddress.Parse(ipaddress);
            IPAddress netm = null;
            IPNetwork ipnetwork = IPNetwork.Parse(ip, netm);
        }

    }
}
