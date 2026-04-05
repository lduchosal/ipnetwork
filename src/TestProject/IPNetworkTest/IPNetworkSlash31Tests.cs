// <copyright file="IPNetworkSlash31Tests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Tests for /31 networks (point-to-point links) as per RFC 3021.
/// See GitHub issue #369: https://github.com/lduchosal/ipnetwork/issues/369
/// </summary>
[TestClass]
public class IPNetworkSlash31Tests
{
    /// <summary>
    /// Tests that FirstUsable and LastUsable are correct for /31 networks.
    /// According to RFC 3021, /31 networks have 2 usable addresses with no
    /// network or broadcast addresses reserved.
    ///
    /// Bug: In version 2.6, FirstUsable and LastUsable are inverted for /31 networks.
    /// Example: 167.92.212.82/31
    ///   Current (Wrong): FirstUsable = 167.92.212.83, LastUsable = 167.92.212.82
    ///   Expected: FirstUsable = 167.92.212.82, LastUsable = 167.92.212.83
    /// </summary>
    [TestMethod]
    [TestCategory("Parse")]
    [TestCategory("RFC3021")]
    public void TestSlash31_FirstUsable_LastUsable_Issue369()
    {
        // Arrange - using the exact example from issue #369
        string ipaddress = "167.92.212.82/31";

        // Expected values per RFC 3021
        string expectedNetwork = "167.92.212.82";
        string expectedNetmask = "255.255.255.254";
        string expectedBroadcast = "167.92.212.83";
        string expectedFirst = "167.92.212.82";
        string expectedLast = "167.92.212.83";
        string expectedFirstUsable = "167.92.212.82";
        string expectedLastUsable = "167.92.212.83";
        byte expectedCidr = 31;
        uint expectedUsable = 2;

        // Act
        var ipnetwork = IPNetwork2.Parse(ipaddress);

        // Assert
        Assert.AreEqual(expectedNetwork, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(expectedNetmask, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.AreEqual(expectedBroadcast, ipnetwork!.Broadcast!.ToString(), "Broadcast");
        Assert.AreEqual(expectedFirst, ipnetwork.First.ToString(), "First");
        Assert.AreEqual(expectedLast, ipnetwork.Last.ToString(), "Last");
        Assert.AreEqual(expectedCidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(expectedUsable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(expectedFirstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(expectedLastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");

        // Critical assertion: FirstUsable must be <= LastUsable
        Assert.IsTrue(
            ipnetwork.FirstUsable.ToString().CompareTo(ipnetwork.LastUsable.ToString()) <= 0 ||
            ipnetwork.FirstUsable.Equals(ipnetwork.LastUsable),
            "FirstUsable must be less than or equal to LastUsable");
    }

    /// <summary>
    /// Tests another /31 network to ensure the fix works for any /31 subnet.
    /// </summary>
    [TestMethod]
    [TestCategory("Parse")]
    [TestCategory("RFC3021")]
    public void TestSlash31_AnotherNetwork()
    {
        // Arrange
        string ipaddress = "10.0.0.0/31";

        string expectedFirstUsable = "10.0.0.0";
        string expectedLastUsable = "10.0.0.1";
        uint expectedUsable = 2;

        // Act
        var ipnetwork = IPNetwork2.Parse(ipaddress);

        // Assert
        Assert.AreEqual(expectedUsable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(expectedFirstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(expectedLastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Tests that /32 networks still work correctly (host route - single address).
    /// </summary>
    [TestMethod]
    [TestCategory("Parse")]
    public void TestSlash32_StillWorksCorrectly()
    {
        // Arrange
        string ipaddress = "192.168.1.1/32";

        string expectedFirstUsable = "192.168.1.1";
        string expectedLastUsable = "192.168.1.1";
        uint expectedUsable = 1;

        // Act
        var ipnetwork = IPNetwork2.Parse(ipaddress);

        // Assert
        Assert.AreEqual(expectedUsable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(expectedFirstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(expectedLastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Tests that /30 networks still work correctly (traditional smallest subnet with usable hosts).
    /// </summary>
    [TestMethod]
    [TestCategory("Parse")]
    public void TestSlash30_StillWorksCorrectly()
    {
        // Arrange
        string ipaddress = "192.168.1.0/30";

        string expectedNetwork = "192.168.1.0";
        string expectedBroadcast = "192.168.1.3";
        string expectedFirstUsable = "192.168.1.1";
        string expectedLastUsable = "192.168.1.2";
        uint expectedUsable = 2;

        // Act
        var ipnetwork = IPNetwork2.Parse(ipaddress);

        // Assert
        Assert.AreEqual(expectedNetwork, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(expectedBroadcast, ipnetwork!.Broadcast!.ToString(), "Broadcast");
        Assert.AreEqual(expectedUsable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(expectedFirstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(expectedLastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }
}
