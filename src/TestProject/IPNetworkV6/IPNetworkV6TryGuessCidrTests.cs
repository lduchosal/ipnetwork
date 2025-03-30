// <copyright file="IPNetworkV6TryGuessCidrTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// TryGuessCidr.
/// </summary>
public class IPNetworkV6TryGuessCidrTests
{
    /// <summary>
    /// Test TryGuessCidrNull.
    /// </summary>
    [TestMethod]
    public void TestTryGuessCidrNull()
    {
        bool parsed = IPNetwork2.TryGuessCidr(null, out byte cidr);

        Assert.IsFalse(parsed, "parsed");
        Assert.AreEqual(0, cidr, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryGuessCidr1()
    {
        bool parsed = IPNetwork2.TryGuessCidr("::", out byte cidr);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(64, cidr, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryGuessCidr2()
    {
        bool parsed = IPNetwork2.TryGuessCidr("2001:0db8::", out byte cidr);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(64, cidr, "cidr");
    }
}
