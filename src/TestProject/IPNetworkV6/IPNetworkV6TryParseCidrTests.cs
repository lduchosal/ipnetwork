// <copyright file="IPNetworkV6TryParseCidrTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// TryParseCidr.
/// </summary>
public class IPNetworkV6TryParseCidrTests
{
    /// <summary>
    /// Test TryParseCidr1.
    /// </summary>
    [TestMethod]
    public void TryParseCidr1()
    {
        string sidr = "0";
        byte? result = 0;
        bool parsed = IPNetwork2.TryParseCidr(sidr, AddressFamily.InterNetworkV6, out byte? cidr);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(result, cidr, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TryParseCidr2()
    {
        string sidr = "sadsd";

        bool parsed = IPNetwork2.TryParseCidr(sidr, AddressFamily.InterNetworkV6, out byte? cidr);

        Assert.IsFalse(parsed, "parsed");
        Assert.AreEqual(null, cidr, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TryParseCidr33()
    {
        string sidr = "33";
        byte result = 33;

        bool parsed = IPNetwork2.TryParseCidr(sidr, AddressFamily.InterNetworkV6, out byte? cidr);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(result, cidr, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TryParseCidr128()
    {
        string sidr = "128";
        byte result = 128;

        bool parsed = IPNetwork2.TryParseCidr(sidr, AddressFamily.InterNetworkV6, out byte? cidr);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreEqual(result, cidr, "cidr");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TryParseCidr129()
    {
        bool parsed = IPNetwork2.TryParseCidr("129", AddressFamily.InterNetworkV6, out byte? cidr);

        Assert.IsFalse(parsed, "parsed");
        Assert.AreEqual(null, cidr, "cidr");
    }
}
