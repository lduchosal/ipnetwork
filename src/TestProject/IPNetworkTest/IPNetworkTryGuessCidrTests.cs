// <copyright file="IPNetworkTryGuessCidrTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class IPNetworkTryGuessCidrTests
{
    /// <summary>
    ///     Tests Try Guess Cidr functionality with Try Guess Cidr Null.
    /// </summary>
    [TestMethod]
    public void TestTryGuessCidrNull()
    {
        bool parsed = IPNetwork2.TryGuessCidr(null, out byte cidr);

        Assert.AreEqual(false, parsed, "parsed");
        Assert.AreEqual(0, cidr, "cidr");
    }

    /// <summary>
    ///     Tests Try Guess Cidr functionality with Try Guess Cidr A.
    /// </summary>
    [TestMethod]
    public void TestTryGuessCidrA()
    {
        bool parsed = IPNetwork2.TryGuessCidr("10.0.0.0", out byte cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(8, cidr, "cidr");
    }

    /// <summary>
    ///     Tests Try Guess Cidr functionality with Try Guess Cidr B.
    /// </summary>
    [TestMethod]
    public void TestTryGuessCidrB()
    {
        bool parsed = IPNetwork2.TryGuessCidr("172.0.0.0", out byte cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(16, cidr, "cidr");
    }

    /// <summary>
    ///     Tests Try Guess Cidr functionality with Try Guess Cidr C.
    /// </summary>
    [TestMethod]
    public void TestTryGuessCidrC()
    {
        bool parsed = IPNetwork2.TryGuessCidr("192.0.0.0", out byte cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(24, cidr, "cidr");
    }

    /// <summary>
    ///     Tests Try Guess Cidr functionality with Try Guess Cidr D.
    /// </summary>
    [TestMethod]
    public void TestTryGuessCidrD()
    {
        bool parsed = IPNetwork2.TryGuessCidr("224.0.0.0", out byte cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(24, cidr, "cidr");
    }

    /// <summary>
    ///     Tests Try Guess Cidr functionality with Try Guess Cidr E.
    /// </summary>
    [TestMethod]
    public void TestTryGuessCidrE()
    {
        bool parsed = IPNetwork2.TryGuessCidr("240.0.0.0", out byte cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(24, cidr, "cidr");
    }
}