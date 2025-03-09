// <copyright file="IPNetwork_TryToNetmask_Tests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// IPNetworkUnitTest test every single method.
/// </summary>
[TestClass]
public class IPNetwork_TryToNetmask_Tests
{
    [TestMethod]
    public void TryToNetmask1()
    {
        IPAddress result = null;
        bool parsed = IPNetwork2.TryToNetmask(0, System.Net.Sockets.AddressFamily.InterNetwork, out result);
        var expected = IPAddress.Parse("0.0.0.0");

        Assert.AreEqual(expected, result, "Netmask");
        Assert.AreEqual(true, parsed, "parsed");
    }

    [TestMethod]
    public void TryToNetmask2()
    {
        IPAddress result = null;
        bool parsed = IPNetwork2.TryToNetmask(33, System.Net.Sockets.AddressFamily.InterNetwork, out result);
        IPAddress expected = null;

        Assert.AreEqual(expected, result, "Netmask");
        Assert.AreEqual(false, parsed, "parsed");
    }

    }