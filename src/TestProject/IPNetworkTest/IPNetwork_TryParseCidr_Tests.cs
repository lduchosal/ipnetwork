// <copyright file="IPNetwork_TryParseCidr_Tests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// IPNetworkUnitTest test every single method.
/// </summary>
[TestClass]
public class IPNetwork_TryParseCidr_Tests
{

    [TestMethod]
    public void TryParseCidr1()
    {
        string sidr = "0";
        byte? cidr;
        byte? result = 0;
        bool parsed = IPNetwork2.TryParseCidr(sidr, System.Net.Sockets.AddressFamily.InterNetwork, out cidr);

        Assert.AreEqual(true, parsed, "parsed");
        Assert.AreEqual(result, cidr, "cidr");
    }

    [TestMethod]
    public void TryParseCidr2()
    {
        string sidr = "sadsd";
        byte? cidr;
        byte? result = null;

        bool parsed = IPNetwork2.TryParseCidr(sidr, System.Net.Sockets.AddressFamily.InterNetwork, out cidr);

        Assert.AreEqual(false, parsed, "parsed");
        Assert.AreEqual(result, cidr, "cidr");
    }

    [TestMethod]
    public void TryParseCidr3()
    {
        string sidr = "33";
        byte? cidr;
        byte? result = null;

        bool parsed = IPNetwork2.TryParseCidr(sidr, System.Net.Sockets.AddressFamily.InterNetwork, out cidr);

        Assert.AreEqual(false, parsed, "parsed");
        Assert.AreEqual(result, cidr, "cidr");
    }

    }