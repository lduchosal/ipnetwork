// <copyright file="IPNetworkV6ParseStringStringTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// ParseStringString.
/// </summary>
[TestClass]
public class IPNetworkV6ParseStringStringTests
{
    /// <summary>
    /// Test ParseString 1.
    /// </summary>
    [TestMethod]
    public void TestParseStringString1()
    {
        string ipaddress = "2001:0db8::";
        string netmask = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:fff0";

        var ipnetwork = IPNetwork2.Parse(ipaddress, netmask);
        Assert.AreEqual("2001:db8::/124", ipnetwork.ToString(), "network");
    }

    /// <summary>
    /// Test ParseString 3.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestParseStringString3()
    {
        string ipaddress = "2001:0db8::";
        string netmask = null;

        IPNetwork2.Parse(ipaddress, netmask);
    }

    /// <summary>
    /// Test ParseString with string string.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestParseStringString5()
    {
        string ipaddress = "2001:0db8::";
        string netmask = string.Empty;

        IPNetwork2.Parse(ipaddress, netmask);
    }
}
