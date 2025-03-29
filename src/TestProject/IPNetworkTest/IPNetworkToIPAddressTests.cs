// <copyright file="IPNetworkToIPAddressTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

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

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestToIPAddress2()
    {
            var ip = new BigInteger(0);
            var result = IPNetwork2.ToIPAddress(ip, AddressFamily.AppleTalk);
        }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestToIPAddress3()
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
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF
            });
            var result = IPNetwork2.ToIPAddress(ip, AddressFamily.AppleTalk);
        }
}