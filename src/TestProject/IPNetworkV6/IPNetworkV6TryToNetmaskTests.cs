// <copyright file="IPNetworkV6TryToNetmaskTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// TryToNetmask.
/// </summary>
public class IPNetworkV6TryToNetmaskTests
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TryToNetmask1()
    {
        bool parsed = IPNetwork2.TryToNetmask(0, AddressFamily.InterNetworkV6, out IPAddress result);
        var expected = IPAddress.Parse("::");

        Assert.AreEqual(expected, result, "Netmask");
        Assert.IsTrue(parsed, "parsed");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TryToNetmask2()
    {
        bool parsed = IPNetwork2.TryToNetmask(33, AddressFamily.InterNetworkV6, out IPAddress result);
        var expected = IPAddress.Parse("ffff:ffff:8000::");

        Assert.AreEqual(expected, result, "Netmask");
        Assert.IsTrue(parsed, "parsed");
    }
}
