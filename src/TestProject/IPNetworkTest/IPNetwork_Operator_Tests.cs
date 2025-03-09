// <copyright file="IPNetwork_Operator_Tests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// IPNetworkUnitTest test every single method.
/// </summary>
[TestClass]
public class IPNetwork_Operator_Tests
{

    [TestMethod]
    public void TestOperatorGreater1()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.1/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

        bool greater = ipn1 > ipn2;

        Assert.AreEqual(false, greater, "greater");
    }

    [TestMethod]
    public void TestOperatorGreater2()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.100/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

        bool greater = ipn1 > ipn2;

        Assert.AreEqual(true, greater, "greater");
    }

    [TestMethod]
    public void TestOperatorLower1()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.1/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

        bool lower = ipn1 < ipn2;

        Assert.AreEqual(true, lower, "lower");
    }

    [TestMethod]
    public void TestOperatorLower2()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.100/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

        bool lower = ipn1 < ipn2;

        Assert.AreEqual(false, lower, "lower");
    }

    [TestMethod]
    public void TestOperatorDifferent1()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.100/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

        bool different = ipn1 != ipn2;

        Assert.AreEqual(true, different, "different");
    }

    [TestMethod]
    public void TestOperatorDifferent2()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.1/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.1/32");

        bool different = ipn1 != ipn2;

        Assert.AreEqual(false, different, "different");
    }

    [TestMethod]
    public void TestOperatorEqual1()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.100/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

        bool eq = ipn1 == ipn2;

        Assert.AreEqual(false, eq, "eq");
    }

    [TestMethod]
    public void TestOperatorEqual2()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.1/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.1/32");

        bool eq = ipn1 == ipn2;

        Assert.AreEqual(true, eq, "eq");
    }

    }