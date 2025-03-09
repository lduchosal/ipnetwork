// <copyright file="IPNetwork_TryGuessCidr_Tests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// IPNetworkUnitTest test every single method.
/// </summary>
[TestClass]
public class IPNetwork_TryGuessCidr_Tests
{

    [TestMethod]
    public void TestTryGuessCidrNull()
    {
        byte cidr;
        bool parsed = IPNetwork2.TryGuessCidr(null, out cidr);

        Assert.AreEqual(false, parsed, "parsed");
        Assert.AreEqual(0, cidr, "cidr");
    }

    [TestMethod]
    public void TestTryGuessCidrA()
    {
        byte cidr;
        bool parsed = IPNetwork2.TryGuessCidr("10.0.0.0", out cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(8, cidr, "cidr");
    }

    [TestMethod]
    public void TestTryGuessCidrB()
    {
        byte cidr;
        bool parsed = IPNetwork2.TryGuessCidr("172.0.0.0", out cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(16, cidr, "cidr");
    }

    [TestMethod]
    public void TestTryGuessCidrC()
    {
        byte cidr;
        bool parsed = IPNetwork2.TryGuessCidr("192.0.0.0", out cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(24, cidr, "cidr");
    }

    [TestMethod]
    public void TestTryGuessCidrD()
    {
        byte cidr;
        bool parsed = IPNetwork2.TryGuessCidr("224.0.0.0", out cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(24, cidr, "cidr");
    }

    [TestMethod]
    public void TestTryGuessCidrE()
    {
        byte cidr;
        bool parsed = IPNetwork2.TryGuessCidr("240.0.0.0", out cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(24, cidr, "cidr");
    }

    }