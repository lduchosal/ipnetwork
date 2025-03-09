// <copyright file="IPNetwork_ToIPAddress_Tests.cs" company="IPNetwork">
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
public class IPNetwork_ToIPAddress_Tests
{

    [TestMethod]
    public void TestToIPAddress()
    {
        var ip = new BigInteger(0);
        var result = IPNetwork2.ToIPAddress(ip, System.Net.Sockets.AddressFamily.InterNetwork);
        Assert.AreEqual(IPAddress.Any, result, "ToIPAddress");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestToIPAddress2()
    {
        var ip = new BigInteger(0);
        var result = IPNetwork2.ToIPAddress(ip, System.Net.Sockets.AddressFamily.AppleTalk);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestToIPAddress3()
    {
        var ip = new BigInteger(new byte[]
        {
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
        });
        var result = IPNetwork2.ToIPAddress(ip, System.Net.Sockets.AddressFamily.AppleTalk);
    }
    }