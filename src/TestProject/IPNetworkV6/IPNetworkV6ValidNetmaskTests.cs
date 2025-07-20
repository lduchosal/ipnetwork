// <copyright file="IPNetworkV6ValidNetmaskTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// ValidNetmask.
/// </summary>
[TestClass]
public class IPNetworkV6ValidNetmaskTests
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestValidNetmask0()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff");
        bool result = IPNetwork2.ValidNetmask(mask);

        Assert.IsTrue(result, "ValidNetmask");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestValidNetmask1()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:fff0");
        bool result = IPNetwork2.ValidNetmask(mask);

        Assert.IsTrue(result, "ValidNetmask");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestValidNetmask2()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:0000");
        bool result = IPNetwork2.ValidNetmask(mask);

        Assert.IsTrue(result, "ValidNetmask");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestValidNetmaskEae1()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:0000:ffff");
        bool result = IPNetwork2.ValidNetmask(mask);

        Assert.IsFalse(result, "ValidNetmask");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestValidNetmaskEae3()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:0000:0001");
        bool result = IPNetwork2.ValidNetmask(mask);

        Assert.IsFalse(result, "ValidNetmask");
    }
}
