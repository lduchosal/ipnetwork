// <copyright file="IPNetwork_TrySupernet_Tests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using System;

namespace TestProject.IPNetworkTest;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// IPNetworkUnitTest test every single method.
/// </summary>
[TestClass]
public class IPNetwork_TrySupernet_Tests
{

    [TestMethod]
    public void TestTrySupernet1()
    {
        var network1 = IPNetwork2.Parse("192.168.0.1/24");
        var network2 = IPNetwork2.Parse("192.168.1.1/24");
        var supernetExpected = IPNetwork2.Parse("192.168.0.0/23");
        IPNetwork2 supernet;
        bool parsed = true;
        bool result = network1.TrySupernet(network2, out supernet);

        Assert.AreEqual(supernetExpected, supernet, "supernet");
        Assert.AreEqual(parsed, result, "parsed");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestTrySupernet2()
    {
        IPNetwork2 network1 = null;
        var network2 = IPNetwork2.Parse("192.168.1.1/24");
        IPNetwork2 supernet;

#pragma warning disable 0618
        bool result = IPNetwork2.TrySupernet(network1, network2, out supernet);
#pragma warning restore 0618
    }

    [TestMethod]
    public void TestTrySupernetStatic2()
    {
        IPNetwork2 network1 = IPNetwork2.IANA_ABLK_RESERVED1;
        var network2 = IPNetwork2.Parse("192.168.1.1/24");
        IPNetwork2 supernet;

#pragma warning disable 0618
        bool result = IPNetwork2.TrySupernet(network1, network2, out supernet);
#pragma warning restore 0618
    }

    [TestMethod]
    public void TestTrySupernet3()
    {
        var network1 = IPNetwork2.Parse("192.168.1.1/24");
        IPNetwork2 network2 = null;
        IPNetwork2 supernetExpected = null;
        IPNetwork2 supernet;
        bool parsed = false;
        bool result = network1.TrySupernet(network2, out supernet);

        Assert.AreEqual(supernetExpected, supernet, "supernet");
        Assert.AreEqual(parsed, result, "parsed");
    }

    [TestMethod]
    public void TestTrySupernet4()
    {
        var network1 = IPNetwork2.Parse("192.168.0.1/24");
        var network2 = IPNetwork2.Parse("192.168.1.1/25");
        IPNetwork2 supernetExpected = null;
        IPNetwork2 supernet;
        bool parsed = false;
        bool result = network1.TrySupernet(network2, out supernet);

        Assert.AreEqual(supernetExpected, supernet, "supernet");
        Assert.AreEqual(parsed, result, "parsed");
    }

    [TestMethod]
    public void TestTrySupernet5()
    {
        var network1 = IPNetwork2.Parse("192.168.0.1/24");
        var network2 = IPNetwork2.Parse("192.168.5.1/24");
        IPNetwork2 supernetExpected = null;
        IPNetwork2 supernet;
        bool parsed = false;
        bool result = network1.TrySupernet(network2, out supernet);

        Assert.AreEqual(supernetExpected, supernet, "supernet");
        Assert.AreEqual(parsed, result, "parsed");
    }

    [TestMethod]
    public void TestTrySupernet6()
    {
        var network1 = IPNetwork2.Parse("192.168.0.1/24");
        var network2 = IPNetwork2.Parse("192.168.0.1/25");
        var supernetExpected = IPNetwork2.Parse("192.168.0.0/24");
        IPNetwork2 supernet;
        bool parsed = true;
        bool result = network1.TrySupernet(network2, out supernet);

        Assert.AreEqual(supernetExpected, supernet, "supernet");
        Assert.AreEqual(parsed, result, "parsed");
    }

    [TestMethod]
    public void TestTrySupernet7()
    {
        var network1 = IPNetwork2.Parse("192.168.0.1/25");
        var network2 = IPNetwork2.Parse("192.168.0.1/24");
        var supernetExpected = IPNetwork2.Parse("192.168.0.0/24");
        IPNetwork2 supernet;
        bool parsed = true;
        bool result = network1.TrySupernet(network2, out supernet);

        Assert.AreEqual(supernetExpected, supernet, "supernet");
        Assert.AreEqual(parsed, result, "parsed");
    }

    [TestMethod]
    public void TestTrySupernet8()
    {
        var network1 = IPNetwork2.Parse("192.168.1.1/24");
        var network2 = IPNetwork2.Parse("192.168.2.1/24");
        IPNetwork2 supernetExpected = null;
        IPNetwork2 supernet;
        bool parsed = false;
        bool result = network1.TrySupernet(network2, out supernet);

        Assert.AreEqual(supernetExpected, supernet, "supernet");
        Assert.AreEqual(parsed, result, "parsed");
    }

    [TestMethod]
    public void TestTrySupernet9()
    {
        var network1 = IPNetwork2.Parse("192.168.1.1/24");
        var network2 = IPNetwork2.Parse("192.168.2.1/24");
        IPNetwork2[] network3 = new[] { network1, network2 };
        IPNetwork2[] supernetExpected = new[] { network1, network2 };
        IPNetwork2[] supernet;
        bool parsed = true;
        bool result = IPNetwork2.TrySupernet(network3, out supernet);

        Assert.AreEqual(supernetExpected[0], supernet[0], "supernet");
        Assert.AreEqual(supernetExpected[1], supernet[1], "supernet");
        Assert.AreEqual(parsed, result, "parsed");
    }

    [TestMethod]
    public void TestTrySupernet10()
    {
        var network1 = IPNetwork2.Parse("192.168.0.1/24");
        var network2 = IPNetwork2.Parse("192.168.1.1/24");
        IPNetwork2[] network3 = new[] { network1, network2 };
        IPNetwork2[] supernetExpected = new[] { IPNetwork2.Parse("192.168.0.0/23") };
        IPNetwork2[] supernet;
        bool parsed = true;
        bool result = IPNetwork2.TrySupernet(network3, out supernet);

        Assert.AreEqual(supernetExpected[0], supernet[0], "supernet");
        Assert.AreEqual(parsed, result, "parsed");
    }

    [TestMethod]
    public void TestTrySupernet11()
    {
        IPNetwork2[] network3 = null;
        IPNetwork2[] supernetExpected = new[] { IPNetwork2.Parse("192.168.0.0/23") };
        IPNetwork2[] supernet;
        bool parsed = false;
        bool result = IPNetwork2.TrySupernet(network3, out supernet);

        Assert.AreEqual(null, supernet, "supernet");
        Assert.AreEqual(parsed, result, "parsed");
    }

    }