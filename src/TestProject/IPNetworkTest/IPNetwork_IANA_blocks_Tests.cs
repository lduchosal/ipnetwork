// <copyright file="IPNetwork_IANA_blocks_Tests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;
using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// IPNetworkUnitTest test every single method.
/// </summary>
[TestClass]
public class IPNetwork_IANA_blocks_Tests
{

    [TestMethod]
    public void TestIANA1()
    {
            var ipaddress = IPAddress.Parse("192.168.66.66");
            bool expected = true;
            bool result = IPNetwork2.IsIANAReserved(ipaddress);

            Assert.AreEqual(expected, result, "IANA");
        }

    [TestMethod]
    public void TestIANA2()
    {
            var ipaddress = IPAddress.Parse("10.0.0.0");
            bool expected = true;
            bool result = IPNetwork2.IsIANAReserved(ipaddress);

            Assert.AreEqual(expected, result, "IANA");
        }

    [TestMethod]
    public void TestIANA3()
    {
            var ipaddress = IPAddress.Parse("172.17.10.10");
            bool expected = true;
            bool result = IPNetwork2.IsIANAReserved(ipaddress);

            Assert.AreEqual(expected, result, "IANA");
        }

    [TestMethod]
    public void TestIANA4()
    {
            var ipnetwork = IPNetwork2.Parse("192.168.66.66/24");
            bool expected = true;
            bool result = ipnetwork.IsIANAReserved();

            Assert.AreEqual(expected, result, "IANA");
        }

    [TestMethod]
    public void TestIANA5()
    {
            var ipnetwork = IPNetwork2.Parse("10.10.10/18");
            bool expected = true;
            bool result = ipnetwork.IsIANAReserved();

            Assert.AreEqual(expected, result, "IANA");
        }

    [TestMethod]
    public void TestIANA6()
    {
            var ipnetwork = IPNetwork2.Parse("172.31.10.10/24");
            bool expected = true;
            bool result = ipnetwork.IsIANAReserved();

            Assert.AreEqual(expected, result, "IANA");
        }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestIANA7()
    {
            IPAddress ipaddress = null;
            IPNetwork2.IsIANAReserved(ipaddress);
        }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestIANA8()
    {
            IPNetwork2 ipnetwork = null;
#pragma warning disable 0618
            bool result = IPNetwork2.IsIANAReserved(ipnetwork);
#pragma warning restore 0618
        }

    [TestMethod]
    public void TestIANABlk1()
    {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_ABLK_RESERVED1;
#pragma warning disable 0618
            bool result = IPNetwork2.IsIANAReserved(ipnetwork);
#pragma warning restore 0618
            Assert.IsTrue(result, "result");
        }

    [TestMethod]
    public void TestIANA9()
    {
            var ipaddress = IPAddress.Parse("1.2.3.4");
            bool expected = false;
            bool result = IPNetwork2.IsIANAReserved(ipaddress);

            Assert.AreEqual(expected, result, "IANA");
        }

    [TestMethod]
    public void TestIANA10()
    {
            var ipnetwork = IPNetwork2.Parse("172.16.0.0/8");
            bool expected = false;
            bool result = ipnetwork.IsIANAReserved();

            Assert.AreEqual(expected, result, "IANA");
        }

    [TestMethod]
    public void TestIANA11()
    {
            var ipnetwork = IPNetwork2.Parse("192.168.15.1/8");
            bool expected = false;
            bool result = ipnetwork.IsIANAReserved();

            Assert.AreEqual(expected, result, "IANA");
        }
    }