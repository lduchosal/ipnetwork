// <copyright file="IPNetworkV6ToBigIntegerTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// ToBigInteger.
/// </summary>
[TestClass]
public class IPNetworkV6ToBigIntegerTests
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToBigInteger32()
    {
        var mask = IPAddress.Parse("::f");
        uint uintMask = 0xf;
        var result = IPNetwork2.ToBigInteger(mask);

        Assert.AreEqual(uintMask, result, "uint");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToBigInteger24()
    {
        var mask = IPAddress.Parse("::fff");
        uint uintMask = 0xfff;
        BigInteger? result = IPNetwork2.ToBigInteger(mask);

        Assert.AreEqual(uintMask, result, "uint");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToBigInteger16()
    {
        var mask = IPAddress.Parse("::ff");
        uint uintMask = 0xff;
        BigInteger? result = IPNetwork2.ToBigInteger(mask);

        Assert.AreEqual(uintMask, result, "uint");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToBigInteger8()
    {
        var mask = IPAddress.Parse("::ff00:0");
        uint uintMask = 0xff000000;
        BigInteger? result = IPNetwork2.ToBigInteger(mask);

        Assert.AreEqual(uintMask, result, "uint");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToBigInteger0()
    {
        var mask = IPAddress.Parse("::");
        uint uintMask = 0x00000000;
        BigInteger? result = IPNetwork2.ToBigInteger(mask);

        Assert.AreEqual(uintMask, result, "uint");
    }
}
