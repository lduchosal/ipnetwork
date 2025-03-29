// <copyright file="IPNetworkTryParseCidrTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class IPNetworkTryParseCidrTests
{
    /// <summary>
    ///     Tests Try Parse Cidr functionality with a /1 network.
    /// </summary>
    [TestMethod]
    public void TryParseCidr1()
    {
            string sidr = "0";
            byte? result = 0;
            bool parsed = IPNetwork2.TryParseCidr(sidr, AddressFamily.InterNetwork, out byte? cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(result, cidr, "cidr");
        }

    /// <summary>
    ///     Tests Try Parse Cidr functionality with a /2 network.
    /// </summary>
    [TestMethod]
    public void TryParseCidr2()
    {
            string sidr = "sadsd";
            byte? result = null;

            bool parsed = IPNetwork2.TryParseCidr(sidr, AddressFamily.InterNetwork, out byte? cidr);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(result, cidr, "cidr");
        }

    /// <summary>
    ///     Tests Try Parse Cidr functionality with a /3 network.
    /// </summary>
    [TestMethod]
    public void TryParseCidr3()
    {
            string sidr = "33";
            byte? result = null;

            bool parsed = IPNetwork2.TryParseCidr(sidr, AddressFamily.InterNetwork, out byte? cidr);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(result, cidr, "cidr");
        }
}