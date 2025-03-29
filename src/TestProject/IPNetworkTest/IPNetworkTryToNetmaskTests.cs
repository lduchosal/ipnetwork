// <copyright file="IPNetworkTryToNetmaskTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class IPNetworkTryToNetmaskTests
{
    /// <summary>
    ///     Tests Try To Netmask functionality with a /1 network.
    /// </summary>
    [TestMethod]
    public void TryToNetmask1()
    {
        bool parsed = IPNetwork2.TryToNetmask(0, AddressFamily.InterNetwork, out IPAddress result);
        var expected = IPAddress.Parse("0.0.0.0");

        Assert.AreEqual(expected, result, "Netmask");
        Assert.AreEqual(true, parsed, "parsed");
    }

    /// <summary>
    ///     Tests Try To Netmask functionality with a /2 network.
    /// </summary>
    [TestMethod]
    public void TryToNetmask2()
    {
        bool parsed = IPNetwork2.TryToNetmask(33, AddressFamily.InterNetwork, out IPAddress result);
        IPAddress expected = null;

        Assert.AreEqual(expected, result, "Netmask");
        Assert.AreEqual(false, parsed, "parsed");
    }
}