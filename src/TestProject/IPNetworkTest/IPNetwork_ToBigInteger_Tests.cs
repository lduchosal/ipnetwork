// <copyright file="IPNetwork_ToBigInteger_Tests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using System;
using System.Numerics;

namespace TestProject.IPNetworkTest;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// IPNetworkUnitTest test every single method.
/// </summary>
[TestClass]
public class IPNetwork_ToBigInteger_Tests
{

    [TestMethod]
    public void TestToBigInteger32()
    {
        var mask = IPAddress.Parse("255.255.255.255");
        uint uintMask = 0xffffffff;
        var result = IPNetwork2.ToBigInteger(mask);

        Assert.AreEqual(uintMask, result, "uint");
    }

    [TestMethod]
    public void TestToBigInteger24()
    {
        var mask = IPAddress.Parse("255.255.255.0");
        uint uintMask = 0xffffff00;
        BigInteger? result = IPNetwork2.ToBigInteger(mask);

        Assert.AreEqual(uintMask, result, "uint");
    }

    [TestMethod]
    public void TestToBigInteger16()
    {
        var mask = IPAddress.Parse("255.255.0.0");
        uint uintMask = 0xffff0000;
        BigInteger? result = IPNetwork2.ToBigInteger(mask);

        Assert.AreEqual(uintMask, result, "uint");
    }

    [TestMethod]
    public void TestToBigInteger8()
    {
        var mask = IPAddress.Parse("255.0.0.0");
        uint uintMask = 0xff000000;
        BigInteger? result = IPNetwork2.ToBigInteger(mask);

        Assert.AreEqual(uintMask, result, "uint");
    }

    [TestMethod]
    public void TestToBigInteger0()
    {
        var mask = IPAddress.Parse("0.0.0.0");
        uint uintMask = 0x00000000;
        BigInteger? result = IPNetwork2.ToBigInteger(mask);

        Assert.AreEqual(uintMask, result, "uint");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestToBigIntegerANE()
    {
        BigInteger? result = IPNetwork2.ToBigInteger(null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestToBigIntegerANE3()
    {
        IPAddress ip = null;
        BigInteger? result = IPNetwork2.ToBigInteger(ip);
    }

    [TestMethod]
    public void TestToBigIntegerANE2()
    {
        BigInteger? result = IPNetwork2.ToBigInteger(IPAddress.IPv6Any);
        uint expected = 0;
        Assert.AreEqual(expected, result, "result");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestToBigIntegerByte()
    {
        BigInteger result = IPNetwork2.ToUint(33, System.Net.Sockets.AddressFamily.InterNetwork);
    }

    [TestMethod]
    public void TestToBigIntegerByte2()
    {
        BigInteger result = IPNetwork2.ToUint(32, System.Net.Sockets.AddressFamily.InterNetwork);
        uint expected = 4294967295;
        Assert.AreEqual(expected, result, "result");
    }

    [TestMethod]
    public void TestToBigIntegerByte3()
    {
        BigInteger result = IPNetwork2.ToUint(0, System.Net.Sockets.AddressFamily.InterNetwork);
        uint expected = 0;
        Assert.AreEqual(expected, result, "result");
    }

    [TestMethod]
    public void TestToBigIntegerInternal1()
    {
        BigInteger? result = null;
        IPNetwork2.InternalToBigInteger(true, 33, System.Net.Sockets.AddressFamily.InterNetwork, out result);
        Assert.AreEqual(null, result, "result");
    }

    [TestMethod]
    public void TestToBigIntegerInternal2()
    {
        BigInteger? result = null;
        IPNetwork2.InternalToBigInteger(true, 129, System.Net.Sockets.AddressFamily.InterNetworkV6, out result);
        Assert.AreEqual(null, result, "result");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestToBigIntegerInternal3()
    {
        BigInteger? result = null;
        IPNetwork2.InternalToBigInteger(false, 129, System.Net.Sockets.AddressFamily.InterNetworkV6, out result);
    }

    [TestMethod]
    [ExpectedException(typeof(NotSupportedException))]
    public void TestToBigIntegerInternal4()
    {
        BigInteger? result = null;
        IPNetwork2.InternalToBigInteger(false, 32, System.Net.Sockets.AddressFamily.AppleTalk, out result);
    }

    [TestMethod]
    public void TestToBigIntegerInternal5()
    {
        BigInteger? result = null;
        IPNetwork2.InternalToBigInteger(true, 32, System.Net.Sockets.AddressFamily.AppleTalk, out result);
        Assert.AreEqual(null, result, "result");
    }

    }