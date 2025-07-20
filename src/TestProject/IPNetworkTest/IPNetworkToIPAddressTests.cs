// <copyright file="IPNetworkToIPAddressTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class IPNetworkToIPAddressTests
{
    /// <summary>
    ///     Tests To IPAddress functionality with To IPAddress.
    /// </summary>
    [TestMethod]
    public void TestToIPAddress()
    {
            var ip = new BigInteger(0);
            var result = IPNetwork2.ToIPAddress(ip, AddressFamily.InterNetwork);
            Assert.AreEqual(IPAddress.Any, result, "ToIPAddress");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToIPAddress2()
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
        {
            var ip = new BigInteger(0);
            IPNetwork2.ToIPAddress(ip, AddressFamily.AppleTalk);
        });
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestToIPAddress3()
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
        {
            var ip = new BigInteger(new byte[]
            {
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            });
            IPNetwork2.ToIPAddress(ip, AddressFamily.AppleTalk);
        });
    }
}