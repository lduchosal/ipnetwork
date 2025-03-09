// <copyright file="IPNetwork_ToCidr_Tests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using System;

namespace TestProject.IPNetworkTest;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// IPNetworkUnitTest test every single method.
/// </summary>
[TestClass]
public class IPNetwork_ToCidr_Tests
{

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestToCidrANE()
    {
        IPNetwork2.ToCidr(null);
    }

    [TestMethod]
    public void TestToCidrAE()
    {
        byte cidr = IPNetwork2.ToCidr(IPAddress.IPv6Any);
        Assert.AreEqual(0, cidr, "cidr");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestToCidrAE2()
    {
        IPNetwork2.ToCidr(IPAddress.Parse("6.6.6.6"));
    }

    [TestMethod]
    public void TestToCidr32()
    {
        var mask = IPAddress.Parse("255.255.255.255");
        byte cidr = 32;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    [TestMethod]
    public void TestToCidr24()
    {
        var mask = IPAddress.Parse("255.255.255.0");
        byte cidr = 24;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    [TestMethod]
    public void TestToCidr16()
    {
        var mask = IPAddress.Parse("255.255.0.0");
        byte cidr = 16;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    [TestMethod]
    public void TestToCidr8()
    {
        var mask = IPAddress.Parse("255.0.0.0");
        byte cidr = 8;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    [TestMethod]
    public void TestToCidr0()
    {
        var mask = IPAddress.Parse("0.0.0.0");
        byte cidr = 0;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    }