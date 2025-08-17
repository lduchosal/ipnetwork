// <copyright file="IPNetworkValidNetmaskTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test parse netmask that are invalid.
/// </summary>
[TestClass]
public class IPNetworkValidNetmaskTests
{
    /// <summary>
    /// Test invalid AddressFamily.
    /// </summary>
    [TestMethod]
    public void TestValidNetmaskInvalid1()
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
        {
            IPNetwork2.InternalValidNetmask(BigInteger.Zero, AddressFamily.AppleTalk);
        });
    }

    /// <summary>
    ///     Tests Valid Netmask functionality with Valid Netmask0.
    /// </summary>
    [TestMethod]
    public void TestValidNetmask0()
    {
        var mask = IPAddress.Parse("255.255.255.255");
        bool result = IPNetwork2.ValidNetmask(mask);

        Assert.IsTrue(result, "ValidNetmask");
    }

    /// <summary>
    ///     Tests Valid Netmask functionality with Valid Netmask1.
    /// </summary>
    [TestMethod]
    public void TestValidNetmask1()
    {
        var mask = IPAddress.Parse("255.255.255.0");
        bool result = IPNetwork2.ValidNetmask(mask);

        Assert.IsTrue(result, "ValidNetmask");
    }

    /// <summary>
    ///     Tests Valid Netmask functionality with Valid Netmask2.
    /// </summary>
    [TestMethod]
    public void TestValidNetmask2()
    {
        var mask = IPAddress.Parse("255.255.0.0");
        bool result = IPNetwork2.ValidNetmask(mask);

        Assert.IsTrue(result, "ValidNetmask");
    }

    /// <summary>
    ///     Tests Valid Netmask functionality with Valid Netmask EAE1.
    /// </summary>
    [TestMethod]
    public void TestValidNetmaskEae1()
    {
        var mask = IPAddress.Parse("0.255.0.0");
        bool result = IPNetwork2.ValidNetmask(mask);

        Assert.IsFalse(result, "ValidNetmask");
    }

    /// <summary>
    /// Test null mask.
    /// </summary>
    [TestMethod]
    public void TestValidNetmaskEae2()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            IPAddress mask = null;
            bool result = IPNetwork2.ValidNetmask(mask);

            Assert.IsTrue(result, "ValidNetmask");
        });
    }

    /// <summary>
    ///     Tests Valid Netmask functionality with Valid Netmask EAE3.
    /// </summary>
    [TestMethod]
    public void TestValidNetmaskEae3()
    {
        var mask = IPAddress.Parse("255.255.0.1");
        bool result = IPNetwork2.ValidNetmask(mask);

        Assert.IsFalse(result, "ValidNetmask");
    }
}