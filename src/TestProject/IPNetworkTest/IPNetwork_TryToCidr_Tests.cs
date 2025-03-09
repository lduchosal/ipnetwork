// <copyright file="IPNetwork_TryToCidr_Tests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// IPNetworkUnitTest test every single method.
/// </summary>
[TestClass]
public class IPNetwork_TryToCidr_Tests
{

    [TestMethod]
    public void TestTryToCidrANE()
    {
        byte? cidr = null;
        bool parsed = IPNetwork2.TryToCidr(null, out cidr);
        Assert.AreEqual(false, parsed, "parsed");
    }

    [TestMethod]
    public void TestTryToCidrAE()
    {
        byte? cidr = null;
        bool parsed = IPNetwork2.TryToCidr(IPAddress.IPv6Any, out cidr);
        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual((byte)0, cidr, "cidr");
    }

    [TestMethod]
    public void TestTryToCidrAE2()
    {
        byte? cidr = null;
        bool parsed = IPNetwork2.TryToCidr(IPAddress.Parse("6.6.6.6"), out cidr);
        Assert.AreEqual(false, parsed, "parsed");
    }

    [TestMethod]
    public void TestTryToCidr32()
    {
        byte? cidr = null;
        var mask = IPAddress.Parse("255.255.255.255");
        byte result = 32;
        bool parsed = IPNetwork2.TryToCidr(mask, out cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    [TestMethod]
    public void TestTryToCidr24()
    {
        byte? cidr = null;
        var mask = IPAddress.Parse("255.255.255.0");
        byte result = 24;
        bool parsed = IPNetwork2.TryToCidr(mask, out cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    [TestMethod]
    public void TestTryToCidr16()
    {
        byte? cidr = null;
        var mask = IPAddress.Parse("255.255.0.0");
        byte result = 16;
        bool parsed = IPNetwork2.TryToCidr(mask, out cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    [TestMethod]
    public void TestTryToCidr8()
    {
        byte? cidr = null;
        var mask = IPAddress.Parse("255.0.0.0");
        byte result = 8;
        bool parsed = IPNetwork2.TryToCidr(mask, out cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    [TestMethod]
    public void TestTryToCidr0()
    {
        byte? cidr = null;
        var mask = IPAddress.Parse("0.0.0.0");
        byte result = 0;
        bool parsed = IPNetwork2.TryToCidr(mask, out cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    }