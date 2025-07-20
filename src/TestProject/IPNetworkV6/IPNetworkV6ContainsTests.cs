// <copyright file="IPNetworkV6ContainsTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// Contains.
/// </summary>
[TestClass]
public class IPNetworkV6ContainsTests
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestContains1()
    {
        var ipnetwork = IPNetwork2.Parse("2001:0db8::/64");
        var ipaddress = IPAddress.Parse("2001:0db8::1");

        bool result = ipnetwork.Contains(ipaddress);

        Assert.IsTrue(result, "contains");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestContains2()
    {
        var ipnetwork = IPNetwork2.Parse("2001:0db8::/64");
        var ipaddress = IPAddress.Parse("2001:0db8:0:1::");

        bool result = ipnetwork.Contains(ipaddress);

        Assert.IsFalse(result, "contains");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestContains3()
    {
        var ipnetwork = IPNetwork2.Parse("2001:0db8::/64");
        var ipnetwork2 = IPNetwork2.Parse("2001:0db8::/64");

        bool result = ipnetwork.Contains(ipnetwork2);

        Assert.IsTrue(result, "contains");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestContains4()
    {
        var ipnetwork = IPNetwork2.Parse("2001:0db8::/64");
        var ipnetwork2 = IPNetwork2.Parse("2001:0db8::/65");

        bool result = ipnetwork.Contains(ipnetwork2);

        Assert.IsTrue(result, "contains");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestContains5()
    {
        var ipnetwork = IPNetwork2.Parse("2001:0db8::/64");
        var ipnetwork2 = IPNetwork2.Parse("2001:0db8:1::/65");

        bool result = ipnetwork.Contains(ipnetwork2);

        Assert.IsFalse(result, "contains");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestContains6()
    {
        var ipnetwork = IPNetwork2.Parse("2001:0db8::/64");
        var ipnetwork2 = IPNetwork2.Parse("2001:0db8::/63");

        bool result = ipnetwork.Contains(ipnetwork2);

        Assert.IsFalse(result, "contains");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestContains10()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            var ipnetwork = IPNetwork2.Parse("::/0");
            IPAddress ipaddress = null;

            ipnetwork.Contains(ipaddress);
        });
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestContains11_different_address_family_returns_false()
    {
        var ipnetwork = IPNetwork2.Parse("::1"); // IPv6
        var ipaddress = IPAddress.Parse("127.0.0.1"); // IPv4

        bool result = ipnetwork.Contains(ipaddress);
        Assert.IsFalse(result, "contains");
    }
}
