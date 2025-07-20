// <copyright file="IPNetworkTryToBigIntegerTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class IPNetworkTryToBigIntegerTests
{
    /// <summary>
    ///     Tests Try To Big Integer functionality with Try To Big Integer32.
    /// </summary>
    [TestMethod]
    public void TestTryToBigInteger32()
    {
        var mask = IPAddress.Parse("255.255.255.255");
        uint uintMask = 0xffffffff;
        bool parsed = IPNetwork2.TryToBigInteger(mask, out BigInteger? result);

        Assert.AreEqual(uintMask, result, "uint");
        Assert.IsTrue(parsed, "parsed");
    }

    /// <summary>
    ///     Tests Try To Big Integer functionality with Try To Big Integer24.
    /// </summary>
    [TestMethod]
    public void TestTryToBigInteger24()
    {
        var mask = IPAddress.Parse("255.255.255.0");
        uint uintMask = 0xffffff00;
        bool parsed = IPNetwork2.TryToBigInteger(mask, out BigInteger? result);

        Assert.AreEqual(uintMask, result, "uint");
        Assert.IsTrue(parsed, "parsed");
    }

    /// <summary>
    ///     Tests Try To Big Integer functionality with Try To Big Integer16.
    /// </summary>
    [TestMethod]
    public void TestTryToBigInteger16()
    {
        var mask = IPAddress.Parse("255.255.0.0");
        uint uintMask = 0xffff0000;
        bool parsed = IPNetwork2.TryToBigInteger(mask, out BigInteger? result);

        Assert.AreEqual(uintMask, result, "uint");
        Assert.IsTrue(parsed, "parsed");
    }

    /// <summary>
    ///     Tests Try To Big Integer functionality with Try To Big Integer8.
    /// </summary>
    [TestMethod]
    public void TestTryToBigInteger8()
    {
        var mask = IPAddress.Parse("255.0.0.0");
        uint uintMask = 0xff000000;

        bool parsed = IPNetwork2.TryToBigInteger(mask, out BigInteger? result);

        Assert.AreEqual(uintMask, result, "uint");
        Assert.IsTrue(parsed, "parsed");
    }

    /// <summary>
    ///     Tests Try To Big Integer functionality with Try To Big Integer0.
    /// </summary>
    [TestMethod]
    public void TestTryToBigInteger0()
    {
        var mask = IPAddress.Parse("0.0.0.0");
        uint uintMask = 0x00000000;
        bool parsed = IPNetwork2.TryToBigInteger(mask, out BigInteger? result);

        Assert.AreEqual(uintMask, result, "uint");
        Assert.IsTrue(parsed, "parsed");
    }

    /// <summary>
    ///     Tests Try To Big Integer functionality with Try To Big Integer ANE.
    /// </summary>
    [TestMethod]
    public void TestTryToBigIntegerAne()
    {
        bool parsed = IPNetwork2.TryToBigInteger(null, out BigInteger? result);

        Assert.IsNull(result, "uint");
        Assert.IsFalse(parsed, "parsed");
    }

    /// <summary>
    ///     Tests Try To Big Integer functionality with Try To Big Integer ANE3.
    /// </summary>
    [TestMethod]
    public void TestTryToBigIntegerAne3()
    {
        bool parsed = IPNetwork2.TryToBigInteger(null, out BigInteger? result);

        Assert.IsNull(result, "uint");
        Assert.IsFalse(parsed, "parsed");
    }

    /// <summary>
    ///     Tests Try To Big Integer functionality with Try To Big Integer ANE2.
    /// </summary>
    [TestMethod]
    public void TestTryToBigIntegerAne2()
    {
        bool parsed = IPNetwork2.TryToBigInteger(IPAddress.IPv6Any, out BigInteger? result);

        Assert.AreEqual(0, result, "result");
        Assert.IsTrue(parsed, "parsed");
    }
}