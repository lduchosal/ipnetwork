// <copyright file="IPNetwork_ValidNetmask_Tests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using System;
using System.Numerics;

namespace TestProject.IPNetworkTest;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// IPNetworkUnitTest test every single method.
/// </summary>
[TestClass]
public class IPNetwork_ValidNetmask_Tests
{

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestValidNetmaskInvalid1()
    {
            bool resut = IPNetwork2.InternalValidNetmask(BigInteger.Zero, System.Net.Sockets.AddressFamily.AppleTalk);
        }

    [TestMethod]
    public void TestValidNetmask0()
    {
            var mask = IPAddress.Parse("255.255.255.255");
            bool expected = true;
            bool result = IPNetwork2.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

    [TestMethod]
    public void TestValidNetmask1()
    {
            var mask = IPAddress.Parse("255.255.255.0");
            bool expected = true;
            bool result = IPNetwork2.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

    [TestMethod]
    public void TestValidNetmask2()
    {
            var mask = IPAddress.Parse("255.255.0.0");
            bool expected = true;
            bool result = IPNetwork2.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

    [TestMethod]
    public void TestValidNetmaskEAE1()
    {
            var mask = IPAddress.Parse("0.255.0.0");
            bool expected = false;
            bool result = IPNetwork2.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestValidNetmaskEAE2()
    {
            IPAddress mask = null;
            bool expected = true;
            bool result = IPNetwork2.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

    [TestMethod]
    public void TestValidNetmaskEAE3()
    {
            var mask = IPAddress.Parse("255.255.0.1");
            bool expected = false;
            bool result = IPNetwork2.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

    }