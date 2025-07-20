// <copyright file="IPNetworkV6TryToCidrTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// TryToCidr.
/// </summary>
[TestClass]
public class IPNetworkV6TryToCidrTests
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr128()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff");
        byte cidr = 128;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr127()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:fffe");
        byte cidr = 127;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr126()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:fffc");
        byte cidr = 126;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr125()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:fff8");
        byte cidr = 125;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr124()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:fff0");
        byte cidr = 124;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr123()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffe0");
        byte cidr = 123;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr122()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffc0");
        byte cidr = 122;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr121()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ff80");
        byte cidr = 121;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr120()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ff00");
        byte cidr = 120;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr119()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:fe00");
        byte cidr = 119;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr118()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:fc00");
        byte cidr = 118;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr117()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:f800");
        byte cidr = 117;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr116()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:f000");
        byte cidr = 116;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr115()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:e000");
        byte cidr = 115;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr114()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:c000");
        byte cidr = 114;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr113()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:8000");
        byte cidr = 113;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr112()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:0000");
        byte cidr = 112;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr111()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:fffe:0");
        byte cidr = 111;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr110()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:fffc:0");
        byte cidr = 110;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr109()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:fff8:0");
        byte cidr = 109;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr108()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:fff0:0");
        byte cidr = 108;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr001()
    {
        var mask = IPAddress.Parse("8000::");
        byte cidr = 1;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToCidr000()
    {
        var mask = IPAddress.Parse("::");
        byte cidr = 0;
        bool parsed = IPNetwork2.TryToCidr(mask, out byte? result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(cidr, result, "cidr");
    }
}
