﻿// <copyright file="EqualsUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class EqualsUnitTest
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestEquals_ipv6_ipv4_0()
    {
        var network1 = IPNetwork2.Parse("::/32");
        var network2 = IPNetwork2.Parse("0.0.0.0/32");
        bool result = network1.Equals(network2);

        Assert.IsFalse(result, "equals");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestEquals_ipv4_1()
    {
        var network1 = IPNetwork2.Parse("192.168.0.1/24");
        var network2 = IPNetwork2.Parse("192.168.0.1/24");
        bool result = network1.Equals(network2);

        Assert.IsTrue(result, "equals");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestEquals_ipv4_2()
    {
        var network1 = IPNetwork2.Parse("192.168.0.1/24");
        IPNetwork2 network2 = null;
        bool result = network1.Equals(network2);

        Assert.IsFalse(result, "equals");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestEquals_ipv4_3()
    {
        var network1 = IPNetwork2.Parse("192.168.0.1/24");
        object network2 = string.Empty;
        bool result = network1.Equals(network2);

        Assert.IsFalse(result, "equals");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestEquals_ipv4_4()
    {
        var network1 = IPNetwork2.Parse("192.168.0.1/24");
        var network2 = IPNetwork2.Parse("192.168.0.1/25");
        bool result = network1.Equals(network2);

        Assert.IsFalse(result, "equals");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestEquals_ipv4_5()
    {
        var network1 = IPNetwork2.Parse("192.168.0.1/24");
        var network2 = IPNetwork2.Parse("192.168.1.1/24");
        bool result = network1.Equals(network2);

        Assert.IsFalse(result, "equals");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestEquals_ipv6_1()
    {
        var network1 = IPNetwork2.Parse("::1/128");
        var network2 = IPNetwork2.Parse("::1/128");
        bool result = network1.Equals(network2);

        Assert.IsTrue(result, "equals");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestEquals_ipv6_2()
    {
        var network1 = IPNetwork2.Parse("::1/128");
        IPNetwork2 network2 = null;
        bool result = network1.Equals(network2);

        Assert.IsFalse(result, "equals");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestEquals_ipv6_3()
    {
        var network1 = IPNetwork2.Parse("::1/128");
        object network2 = string.Empty;
        bool result = network1.Equals(network2);

        Assert.IsFalse(result, "equals");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestEquals_ipv6_4()
    {
        var network1 = IPNetwork2.Parse("::1/128");
        var network2 = IPNetwork2.Parse("::1/127");
        bool result = network1.Equals(network2);

        Assert.IsFalse(result, "equals");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestEquals_ipv6_5()
    {
        var network1 = IPNetwork2.Parse("::1/128");
        var network2 = IPNetwork2.Parse("::10/128");
        bool result = network1.Equals(network2);

        Assert.IsFalse(result, "equals");
    }
}