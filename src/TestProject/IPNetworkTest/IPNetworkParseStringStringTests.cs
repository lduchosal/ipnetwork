// <copyright file="IPNetworkParseStringStringTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test parse string.
/// </summary>
[TestClass]
public class IPNetworkParseStringStringTests
{
    /// <summary>
    ///     Tests Parse String String functionality with Parse String String1.
    /// </summary>
    [TestMethod]
    public void TestParseStringString1()
    {
            string ipaddress = "192.168.168.100";
            string netmask = "255.255.255.0";

            var ipnetwork = IPNetwork2.Parse(ipaddress, netmask);
            Assert.AreEqual("192.168.168.0/24", ipnetwork.ToString(), "network");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestParseStringString2()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            string ipaddress = null;
            string netmask = null;

            IPNetwork2.Parse(ipaddress, netmask);
        });
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestParseStringString3()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            string ipaddress = "192.168.168.100";
            string netmask = null;

            IPNetwork2.Parse(ipaddress, netmask);
        });
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestParseStringString4()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            string ipaddress = string.Empty;
            string netmask = string.Empty;

            IPNetwork2.Parse(ipaddress, netmask);
        });
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestParseStringString5()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            string ipaddress = "192.168.168.100";
            string netmask = string.Empty;

            IPNetwork2.Parse(ipaddress, netmask);
        });
    }
}