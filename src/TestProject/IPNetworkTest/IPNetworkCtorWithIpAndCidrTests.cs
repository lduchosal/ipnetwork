// <copyright file="IPNetworkCtorWithIpAndCidrTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test Ctor.
/// </summary>
[TestClass]
public class IPNetworkCtorWithIpAndCidrTests
{
    /// <summary>
    ///     Tests Ctor With Ip And Cidr functionality with a /1 network.
    /// </summary>
    [TestMethod]
    public void CtorWithIpAndCidr1()
    {
        string ipaddress = "192.168.168.100";
        var ip = IPAddress.Parse(ipaddress);
        var ipnetwork = new IPNetwork2(ip, 24);
        Assert.AreEqual("192.168.168.0/24", ipnetwork.ToString(), "network");
    }

    /// <summary>
    ///     Tests Ctor With null ip.
    /// </summary>
    [TestMethod]
    public void CtorWithIpAndCidr2()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            IPAddress ip = null;
            var ipnetwork = new IPNetwork2(ip, 24);
        });
    }

    /// <summary>
    ///     Tests Ctor With too big cidr.
    /// </summary>
    [TestMethod]
    public void CtorWithIpAndCidr3()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
        {
            string ipaddress = "192.168.168.100";
            var ip = IPAddress.Parse(ipaddress);
            var ipnetwork = new IPNetwork2(ip, 33);
        });
    }
}