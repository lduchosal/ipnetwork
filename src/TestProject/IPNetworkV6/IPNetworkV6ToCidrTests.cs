// <copyright file="IPNetworkV6ToCidrTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// ToCidr.
/// </summary>
public class IPNetworkV6ToCidrTests
{
    /// <summary>
    /// Test ToCidr.
    /// </summary>
    [TestMethod]
    public void TestToCidrAe()
    {
        byte cidr = IPNetwork2.ToCidr(IPAddress.IPv6Any);
        Assert.AreEqual(0, cidr, "cidr");
    }

    /// <summary>
    /// Test ToCidr.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestToCidrAe2()
    {
        IPNetwork2.ToCidr(IPAddress.Parse("2001:db8:3:4:5:6:7:8"));
    }

    /// <summary>
    /// Test ToCidr.
    /// </summary>
    [TestMethod]
    public void TestToCidr128()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff");
        byte cidr = 128;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test ToCidr.
    /// </summary>
    [TestMethod]
    public void TestToCidr127()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:fffe");
        byte cidr = 127;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test ToCidr.
    /// </summary>
    [TestMethod]
    public void TestToCidr126()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:fffc");
        byte cidr = 126;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test ToCidr.
    /// </summary>
    [TestMethod]
    public void TestToCidr125()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:fff8");
        byte cidr = 125;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test ToCidr.
    /// </summary>
    [TestMethod]
    public void TestToCidr124()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:fff0");
        byte cidr = 124;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test ToCidr.
    /// </summary>
    [TestMethod]
    public void TestToCidr123()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffe0");
        byte cidr = 123;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test ToCidr.
    /// </summary>
    [TestMethod]
    public void TestToCidr122()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffc0");
        byte cidr = 122;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test ToCidr.
    /// </summary>
    [TestMethod]
    public void TestToCidr121()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ff80");
        byte cidr = 121;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test ToCidr.
    /// </summary>
    [TestMethod]
    public void TestToCidr120()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ff00");
        byte cidr = 120;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test ToCidr.
    /// </summary>
    [TestMethod]
    public void TestToCidr119()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:fe00");
        byte cidr = 119;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test ToCidr.
    /// </summary>
    [TestMethod]
    public void TestToCidr118()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:fc00");
        byte cidr = 118;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test ToCidr.
    /// </summary>
    [TestMethod]
    public void TestToCidr117()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:f800");
        byte cidr = 117;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test ToCidr.
    /// </summary>
    [TestMethod]
    public void TestToCidr116()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:f000");
        byte cidr = 116;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToCidr115()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:e000");
        byte cidr = 115;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToCidr114()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:c000");
        byte cidr = 114;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToCidr113()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:8000");
        byte cidr = 113;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToCidr112()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:ffff:0000");
        byte cidr = 112;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToCidr111()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:fffe:0");
        byte cidr = 111;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToCidr110()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:fffc:0");
        byte cidr = 110;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToCidr109()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:fff8:0");
        byte cidr = 109;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToCidr108()
    {
        var mask = IPAddress.Parse("ffff:ffff:ffff:ffff:ffff:ffff:fff0:0");
        byte cidr = 108;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToCidr001()
    {
        var mask = IPAddress.Parse("8000::");
        byte cidr = 1;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToCidr000()
    {
        var mask = IPAddress.Parse("::");
        byte cidr = 0;
        int result = IPNetwork2.ToCidr(mask);

        Assert.AreEqual(cidr, result, "cidr");
    }
}
