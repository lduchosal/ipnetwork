// <copyright file="IPNetwork_BitsSet_Tests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// IPNetworkUnitTest test every single method.
/// </summary>
[TestClass]
public class IPNetwork_BitsSet_Tests
{

    [TestMethod]
    public void TestBitsSet32()
    {
        var ip = IPAddress.Parse("255.255.255.255");
        uint bits = 32;
        uint result = IPNetwork2.BitsSet(ip);

        Assert.AreEqual(bits, result, "BitsSet");
    }

    [TestMethod]
    public void TestBitsSet24()
    {
        var ip = IPAddress.Parse("255.255.255.0");
        uint bits = 24;
        uint result = IPNetwork2.BitsSet(ip);

        Assert.AreEqual(bits, result, "BitsSet");
    }

    [TestMethod]
    public void TestBitsSet16()
    {
        var ip = IPAddress.Parse("255.255.0.0");
        uint bits = 16;
        uint result = IPNetwork2.BitsSet(ip);

        Assert.AreEqual(bits, result, "BitsSet");
    }

    [TestMethod]
    public void TestBitsSet4()
    {
        var ip = IPAddress.Parse("128.128.128.128");
        uint bits = 4;
        uint result = IPNetwork2.BitsSet(ip);

        Assert.AreEqual(bits, result, "BitsSet");
    }

    }