// <copyright file="IPNetworkV6TryToBigIntegerTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// TryToBigInteger.
/// </summary>
[TestClass]
public class IPNetworkV6TryToBigIntegerTests
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToBigInteger32()
    {
        var mask = IPAddress.Parse("::ffff:ffff");
        uint uintMask = 0xffffffff;
        bool parsed = IPNetwork2.TryToBigInteger(mask, out BigInteger? result);

        Assert.AreEqual(uintMask, result, "uint");
        Assert.IsTrue(parsed, "parsed");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToBigInteger24()
    {
        var mask = IPAddress.Parse("::ffff:ff00");
        uint uintMask = 0xffffff00;
        bool parsed = IPNetwork2.TryToBigInteger(mask, out BigInteger? result);

        Assert.AreEqual(uintMask, result, "uint");
        Assert.IsTrue(parsed, "parsed");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToBigInteger16()
    {
        var mask = IPAddress.Parse("::ffff:0");
        uint uintMask = 0xffff0000;
        bool parsed = IPNetwork2.TryToBigInteger(mask, out BigInteger? result);

        Assert.AreEqual(uintMask, result, "uint");
        Assert.IsTrue(parsed, "parsed");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToBigInteger8()
    {
        var mask = IPAddress.Parse("::ff00:0");
        uint uintMask = 0xff000000;

        bool parsed = IPNetwork2.TryToBigInteger(mask, out BigInteger? result);

        Assert.AreEqual(uintMask, result, "uint");
        Assert.IsTrue(parsed, "parsed");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryToBigInteger0()
    {
        var mask = IPAddress.Parse("::");
        uint uintMask = 0x00000000;
        bool parsed = IPNetwork2.TryToBigInteger(mask, out BigInteger? result);

        Assert.AreEqual(uintMask, result, "uint");
        Assert.IsTrue(parsed, "parsed");
    }
}
