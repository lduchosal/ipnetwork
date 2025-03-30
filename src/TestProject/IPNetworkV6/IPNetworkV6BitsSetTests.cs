// <copyright file="IPNetworkV6BitsSetTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// BitsSet.
/// </summary>
public class IPNetworkV6BitsSetTests
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestBitsSet128()
    {
        var ip = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff");
        uint bits = 128;
        uint result = IPNetwork2.BitsSet(ip);

        Assert.AreEqual(bits, result, "BitsSet");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestBitsSet120()
    {
        var ip = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:fff0");
        uint bits = 124;
        uint result = IPNetwork2.BitsSet(ip);

        Assert.AreEqual(bits, result, "BitsSet");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestBitsSet16()
    {
        var ip = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:0000");
        uint bits = 112;
        uint result = IPNetwork2.BitsSet(ip);

        Assert.AreEqual(bits, result, "BitsSet");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestBitsSet4()
    {
        var ip = IPAddress.Parse("f0f0:f0f0:f0f0:f0f0:f0f0:f0f0:f0f0:f0f0");
        uint bits = 64;
        uint result = IPNetwork2.BitsSet(ip);

        Assert.AreEqual(bits, result, "BitsSet");
    }
}
