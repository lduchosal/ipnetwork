// <copyright file="IPNetworkV6ToStringTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// ToString.
/// </summary>
[TestClass]
public class IPNetworkV6ToStringTests
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToString()
    {
        var ipnetwork = IPNetwork2.Parse("2001:0db8:0000:0000:0000:0000:0000:0000/32");
        string expected = "2001:db8::/32";
        string result = ipnetwork.ToString();

        Assert.AreEqual(expected, result, "ToString");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToString1()
    {
        var ipnetwork = IPNetwork2.Parse("2001:0db8:1:2:3:4:5:6/32");
        string expected = "2001:db8::/32";
        string result = ipnetwork.ToString();

        Assert.AreEqual(expected, result, "ToString");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToString2()
    {
        var ipnetwork = IPNetwork2.Parse("2001:0db8:1:2:3:4:5:6/64");
        string expected = "2001:db8:1:2::/64";
        string result = ipnetwork.ToString();

        Assert.AreEqual(expected, result, "ToString");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToString3()
    {
        var ipnetwork = IPNetwork2.Parse("2001:0db8:1:2:3:4:5:6/100");
        string expected = "2001:db8:1:2:3:4::/100";
        string result = ipnetwork.ToString();

        Assert.AreEqual(expected, result, "ToString");
    }
}
