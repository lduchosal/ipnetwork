
// <copyright file="IANAblocksUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// IANAblocksUnitTest test every single method
    /// </summary>
    [TestClass]
    public class IANAblocksUnitTest
    {
        [TestMethod]
        public void TestIANA1()
        {
            IPAddress ipaddress = IPAddress.Parse("192.168.66.66");
            bool expected = true;
            bool result = IPNetwork.IsIANAReserved(ipaddress);

            Assert.AreEqual(expected, result, "IANA");
        }

        [TestMethod]
        public void TestIANA2()
        {
            IPAddress ipaddress = IPAddress.Parse("10.0.0.0");
            bool expected = true;
            bool result = IPNetwork.IsIANAReserved(ipaddress);

            Assert.AreEqual(expected, result, "IANA");
        }

        [TestMethod]
        public void TestIANA3()
        {
            IPAddress ipaddress = IPAddress.Parse("172.17.10.10");
            bool expected = true;
            bool result = IPNetwork.IsIANAReserved(ipaddress);

            Assert.AreEqual(expected, result, "IANA");
        }

        [TestMethod]
        public void TestIANA4()
        {
            IPNetwork ipnetwork = IPNetwork.Parse("192.168.66.66/24");
            bool expected = true;
            bool result = ipnetwork.IsIANAReserved();

            Assert.AreEqual(expected, result, "IANA");
        }

        [TestMethod]
        public void TestIANA5()
        {
            IPNetwork ipnetwork = IPNetwork.Parse("10.10.10/18");
            bool expected = true;
            bool result = ipnetwork.IsIANAReserved();

            Assert.AreEqual(expected, result, "IANA");
        }

        [TestMethod]
        public void TestIANA6()
        {
            IPNetwork ipnetwork = IPNetwork.Parse("172.31.10.10/24");
            bool expected = true;
            bool result = ipnetwork.IsIANAReserved();

            Assert.AreEqual(expected, result, "IANA");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIANA7()
        {
            IPAddress ipaddress = null;
            IPNetwork.IsIANAReserved(ipaddress);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIANA8()
        {
            IPNetwork ipnetwork = null;
#pragma warning disable 0618
            bool result = IPNetwork.IsIANAReserved(ipnetwork);
#pragma warning restore 0618
        }

        [TestMethod]
        public void TestIANABlk1()
        {
            IPNetwork ipnetwork = IPNetwork.IANA_ABLK_RESERVED1;
#pragma warning disable 0618
            bool result = IPNetwork.IsIANAReserved(ipnetwork);
#pragma warning restore 0618
            Assert.IsTrue(result, "result");
        }

        [TestMethod]
        public void TestIANA9()
        {
            IPAddress ipaddress = IPAddress.Parse("1.2.3.4");
            bool expected = false;
            bool result = IPNetwork.IsIANAReserved(ipaddress);

            Assert.AreEqual(expected, result, "IANA");
        }

        [TestMethod]
        public void TestIANA10()
        {
            IPNetwork ipnetwork = IPNetwork.Parse("172.16.0.0/8");
            bool expected = false;
            bool result = ipnetwork.IsIANAReserved();

            Assert.AreEqual(expected, result, "IANA");
        }

        [TestMethod]
        public void TestIANA11()
        {
            IPNetwork ipnetwork = IPNetwork.Parse("192.168.15.1/8");
            bool expected = false;
            bool result = ipnetwork.IsIANAReserved();

            Assert.AreEqual(expected, result, "IANA");
        }
    }
}
