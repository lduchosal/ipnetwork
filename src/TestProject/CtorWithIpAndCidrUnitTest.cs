
// <copyright file="CtorWithIpAndCidrUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// CtorWithIpAndCidrUnitTest test every single method
    /// </summary>
    [TestClass]
    public class CtorWithIpAndCidrUnitTest
    {

        [TestMethod]
        public void CtorWithIpAndCidr1()
        {
            string ipaddress = "192.168.168.100";
            IPAddress ip = IPAddress.Parse(ipaddress);
            IPNetwork ipnetwork = new IPNetwork(ip, 24);
            Assert.AreEqual("192.168.168.0/24", ipnetwork.ToString(), "network");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CtorWithIpAndCidr2()
        {
            IPAddress ip = null;
            IPNetwork ipnetwork = new IPNetwork(ip, 24);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CtorWithIpAndCidr3()
        {
            string ipaddress = "192.168.168.100";
            IPAddress ip = IPAddress.Parse(ipaddress);
            IPNetwork ipnetwork = new IPNetwork(ip, 33);
        }

    }
}
