// <copyright file="ContainsUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using System;
using System.Net;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IPNetwork2.TestProject
{
    /// <summary>
    /// ContainsUnitTest test every Contiains method.
    /// </summary>
    [TestClass]
    public class ContainsUnitTest
    {
        [TestMethod]
        public void TestContains1()
        {
            var ipnetwork = IPNetwork.Parse("192.168.0.1/24");
            var ipaddress = IPAddress.Parse("192.168.0.100");

            bool result = ipnetwork.Contains(ipaddress);
            bool expected = true;

            Assert.AreEqual(expected, result, "contains");
        }

        [TestMethod]
        public void TestContains2()
        {
            var ipnetwork = IPNetwork.Parse("192.168.0.1/24");
            var ipaddress = IPAddress.Parse("10.10.10.10");

            bool result = ipnetwork.Contains(ipaddress);
            bool expected = false;

            Assert.AreEqual(expected, result, "contains");
        }

        [TestMethod]
        public void TestContains3()
        {
            var ipnetwork = IPNetwork.Parse("192.168.0.1/24");
            var ipnetwork2 = IPNetwork.Parse("192.168.0.1/24");

            bool result = ipnetwork.Contains(ipnetwork2);
            bool expected = true;

            Assert.AreEqual(expected, result, "contains");
        }

        [TestMethod]
        public void TestContains4()
        {
            var ipnetwork = IPNetwork.Parse("192.168.0.1/16");
            var ipnetwork2 = IPNetwork.Parse("192.168.1.1/24");

            bool result = ipnetwork.Contains(ipnetwork2);
            bool expected = true;

            Assert.AreEqual(expected, result, "contains");
        }

        [TestMethod]
        public void TestContains5()
        {
            var ipnetwork = IPNetwork.Parse("192.168.0.1/16");
            var ipnetwork2 = IPNetwork.Parse("10.10.10.0/24");

            bool result = ipnetwork.Contains(ipnetwork2);
            bool expected = false;

            Assert.AreEqual(expected, result, "contains");
        }

        [TestMethod]
        public void TestContains6()
        {
            var ipnetwork = IPNetwork.Parse("192.168.1.1/24");
            var ipnetwork2 = IPNetwork.Parse("192.168.0.0/16");

            bool result = ipnetwork.Contains(ipnetwork2);
            bool expected = false;

            Assert.AreEqual(expected, result, "contains");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsStatic3()
        {
            IPNetwork ipnetwork = null;
            IPNetwork ipnetwork2 = null;

#pragma warning disable 0618
            bool result = IPNetwork.Contains(ipnetwork, ipnetwork2);
#pragma warning restore 0618
        }

        [TestMethod]
        public void TestContainsStatic4()
        {
            var ipnetwork = IPNetwork.IANA_CBLK_RESERVED1;
            var ipnetwork2 = IPNetwork.IANA_CBLK_RESERVED1;

#pragma warning disable 0618
            bool result = IPNetwork.Contains(ipnetwork, ipnetwork2);
#pragma warning restore 0618

            Assert.IsTrue(result, "result");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContains8()
        {
            var ipnetwork = IPNetwork.Parse("0.0.0.0/0");
            IPNetwork ipnetwork2 = null;

            bool result = ipnetwork.Contains(ipnetwork2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsStatic1()
        {
            IPNetwork ipnetwork = null;
            IPAddress ipaddress = null;

#pragma warning disable 0618
            bool result = IPNetwork.Contains(ipnetwork, ipaddress);
#pragma warning restore 0618
        }

        [TestMethod]
        public void TestContainsStatic2()
        {
            var ipnetwork = IPNetwork.IANA_ABLK_RESERVED1;
            var ipaddress = IPAddress.Parse("10.0.0.1");

#pragma warning disable 0618
            bool result = IPNetwork.Contains(ipnetwork, ipaddress);
#pragma warning restore 0618
            Assert.IsTrue(result, "result");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContains10()
        {
            var ipnetwork = IPNetwork.Parse("0.0.0.0/0");
            IPAddress ipaddress = null;

            bool result = ipnetwork.Contains(ipaddress);
        }

        [DataTestMethod]
        [DataRow("1.1.1.0/8", "1.1.1.1", true)]
        [DataRow("1.1.1.0/8", "2.1.1.1", false)]
        [DataRow("192.168.0.1/24", "192.168.0.100", true)]
        [DataRow("192.168.0.1/24", "10.10.10.10", false)]
        [DataRow("192.168.0.1/24", "192.168.0.1", true)]
        [DataRow("192.168.0.1/16", "192.168.1.1", true)]
        [DataRow("192.168.0.1/16", "10.10.10.0", false)]
        [DataRow("192.168.1.1/24", "192.168.0.0", false)]
        public void Test_Contains2_IPAddress_Should_Match_Contains(string contains1, string contains2, bool expected)
        {
            var ipnetwork = IPNetwork.Parse(contains1);
            var ipaddress = IPAddress.Parse(contains2);

            bool result1 = ipnetwork.Contains(ipaddress);

            Assert.AreEqual(expected, result1, "contains1");
        }

        [DataTestMethod]
        [DataRow("0.0.0.0/0", "255.255.255.255", true)]
        [DataRow("1.1.1.0/8", "1.1.1.1", true)]
        [DataRow("1.1.1.0/8", "2.1.1.1", false)]
        [DataRow("192.168.0.1/24", "192.168.0.100/32", true)]
        [DataRow("192.168.0.1/24", "10.10.10.10/32", false)]
        [DataRow("192.168.0.1/24", "192.168.0.1/24", true)]
        [DataRow("192.168.0.1/16", "192.168.1.1/24", true)]
        [DataRow("192.168.0.1/16", "10.10.10.0/24", false)]
        [DataRow("192.168.1.1/24", "192.168.0.0/16", false)]
        public void Test_Contains2_IPNetwork_Should_Match_Contains(string contains1, string contains2, bool expected)
        {
            var ipnetwork = IPNetwork.Parse(contains1);
            var ipaddress = IPNetwork.Parse(contains2);

            bool result1 = ipnetwork.Contains(ipaddress);

            Assert.AreEqual(expected, result1, "contains1");
        }
    }
}
