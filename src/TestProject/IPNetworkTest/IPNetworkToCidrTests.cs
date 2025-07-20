// <copyright file="IPNetworkToCidrTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class IPNetworkToCidrTests
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToCidrAne()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            IPNetwork2.ToCidr(null);
        });
    }

    /// <summary>
    ///     Tests To Cidr functionality with To Cidr AE.
    /// </summary>
    [TestMethod]
    public void TestToCidrAe()
    {
            byte cidr = IPNetwork2.ToCidr(IPAddress.IPv6Any);
            Assert.AreEqual(0, cidr, "cidr");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToCidrAe2()
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
        {
            IPNetwork2.ToCidr(IPAddress.Parse("6.6.6.6"));
        });
    }

    /// <summary>
    ///     Tests To Cidr functionality with To Cidr32.
    /// </summary>
    [TestMethod]
    public void TestToCidr32()
    {
            var mask = IPAddress.Parse("255.255.255.255");
            byte cidr = 32;
            int result = IPNetwork2.ToCidr(mask);

            Assert.AreEqual(cidr, result, "cidr");
        }

    /// <summary>
    ///     Tests To Cidr functionality with To Cidr24.
    /// </summary>
    [TestMethod]
    public void TestToCidr24()
    {
            var mask = IPAddress.Parse("255.255.255.0");
            byte cidr = 24;
            int result = IPNetwork2.ToCidr(mask);

            Assert.AreEqual(cidr, result, "cidr");
        }

    /// <summary>
    ///     Tests To Cidr functionality with To Cidr16.
    /// </summary>
    [TestMethod]
    public void TestToCidr16()
    {
            var mask = IPAddress.Parse("255.255.0.0");
            byte cidr = 16;
            int result = IPNetwork2.ToCidr(mask);

            Assert.AreEqual(cidr, result, "cidr");
        }

    /// <summary>
    ///     Tests To Cidr functionality with To Cidr8.
    /// </summary>
    [TestMethod]
    public void TestToCidr8()
    {
            var mask = IPAddress.Parse("255.0.0.0");
            byte cidr = 8;
            int result = IPNetwork2.ToCidr(mask);

            Assert.AreEqual(cidr, result, "cidr");
        }

    /// <summary>
    ///     Tests To Cidr functionality with To Cidr0.
    /// </summary>
    [TestMethod]
    public void TestToCidr0()
    {
            var mask = IPAddress.Parse("0.0.0.0");
            byte cidr = 0;
            int result = IPNetwork2.ToCidr(mask);

            Assert.AreEqual(cidr, result, "cidr");
        }
}