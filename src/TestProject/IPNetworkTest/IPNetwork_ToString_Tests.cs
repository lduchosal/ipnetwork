// <copyright file="IPNetwork_ToString_Tests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// IPNetworkUnitTest test every single method.
/// </summary>
[TestClass]
public class IPNetwork_ToString_Tests
{

    [TestMethod]
    public void TestToString()
    {
        var ipnetwork = IPNetwork2.Parse("192.168.15.1/8");
        string expected = "192.0.0.0/8";
        string result = ipnetwork.ToString();

        Assert.AreEqual(expected, result, "ToString");
    }

    [TestMethod]
    public void TestToString1()
    {
        var ipnetwork = IPNetwork2.Parse("192.168.15.1/9");
        string expected = "192.128.0.0/9";
        string result = ipnetwork.ToString();

        Assert.AreEqual(expected, result, "ToString");
    }

    [TestMethod]
    public void TestToString2()
    {
        var ipnetwork = IPNetwork2.Parse("192.168.15.1/10");
        string expected = "192.128.0.0/10";
        string result = ipnetwork.ToString();

        Assert.AreEqual(expected, result, "ToString");
    }

    [TestMethod]
    public void TestToString3()
    {
        var ipnetwork = IPNetwork2.Parse("192.168.15.1/11");
        string expected = "192.160.0.0/11";
        string result = ipnetwork.ToString();

        Assert.AreEqual(expected, result, "ToString");
    }

    [TestMethod]
    public void TestToString4()
    {
        var ipnetwork = IPNetwork2.Parse("192.168.15.1/12");
        string expected = "192.160.0.0/12";
        string result = ipnetwork.ToString();

        Assert.AreEqual(expected, result, "ToString");
    }

    [TestMethod]
    public void TestToString5()
    {
        var ipnetwork = IPNetwork2.Parse("192.168.15.1/13");
        string expected = "192.168.0.0/13";
        string result = ipnetwork.ToString();

        Assert.AreEqual(expected, result, "ToString");
    }

    [TestMethod]
    public void TestToString6()
    {
        var ipnetwork = IPNetwork2.Parse("192.168.15.1/14");
        string expected = "192.168.0.0/14";
        string result = ipnetwork.ToString();

        Assert.AreEqual(expected, result, "ToString");
    }

    [TestMethod]
    public void TestToString7()
    {
        var ipnetwork = IPNetwork2.Parse("192.168.15.1/15");
        string expected = "192.168.0.0/15";
        string result = ipnetwork.ToString();

        Assert.AreEqual(expected, result, "ToString");
    }

    [TestMethod]
    public void TestToString8()
    {
        var ipnetwork = IPNetwork2.Parse("192.168.15.1/16");
        string expected = "192.168.0.0/16";
        string result = ipnetwork.ToString();

        Assert.AreEqual(expected, result, "ToString");
    }

    }