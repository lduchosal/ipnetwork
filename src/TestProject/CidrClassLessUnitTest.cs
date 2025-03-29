// <copyright file="CidrClassLessUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class CidrClassLessUnitTest
{
    #region IPV4

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryGuessCidrNull()
    {
        var cidrguess = new CidrClassLess();

        bool parsed = cidrguess.TryGuessCidr(null, out byte cidr);

        Assert.AreEqual(false, parsed, "parsed");
        Assert.AreEqual(0, cidr, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryGuessCidrA()
    {
        var cidrguess = new CidrClassLess();

        bool parsed = cidrguess.TryGuessCidr("10.0.0.0", out byte cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(32, cidr, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryGuessCidrB()
    {
        var cidrguess = new CidrClassLess();

        bool parsed = cidrguess.TryGuessCidr("172.0.0.0", out byte cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(32, cidr, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryGuessCidrC()
    {
        var cidrguess = new CidrClassLess();

        bool parsed = cidrguess.TryGuessCidr("192.0.0.0", out byte cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(32, cidr, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryGuessCidrD()
    {
        var cidrguess = new CidrClassLess();

        bool parsed = cidrguess.TryGuessCidr("224.0.0.0", out byte cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(32, cidr, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryGuessCidrE()
    {
        var cidrguess = new CidrClassLess();

        bool parsed = cidrguess.TryGuessCidr("240.0.0.0", out byte cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(32, cidr, "cidr");
    }

    #endregion

    #region IPV6

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestIpV6TryGuessCidrNull()
    {
        var cidrguess = new CidrClassLess();

        bool parsed = cidrguess.TryGuessCidr(null, out byte cidr);

        Assert.AreEqual(false, parsed, "parsed");
        Assert.AreEqual(0, cidr, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestIpV6TryGuessCidr1()
    {
        var cidrguess = new CidrClassLess();

        bool parsed = cidrguess.TryGuessCidr("::", out byte cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(128, cidr, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestIpV6TryGuessCidr2()
    {
        var cidrguess = new CidrClassLess();

        bool parsed = cidrguess.TryGuessCidr("2001:0db8::", out byte cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(128, cidr, "cidr");
    }

    #endregion
}