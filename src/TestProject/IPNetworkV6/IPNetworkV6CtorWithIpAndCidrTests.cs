// <copyright file="IPNetworkV6CtorWithIpAndCidrTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// CtorWithIpAndCidr.
/// </summary>
[TestClass]
public class IPNetworkV6CtorWithIpAndCidrTests
{
    /// <summary>
    /// Test CtorWithIpAndCidr1.
    /// </summary>
    [TestMethod]
    public void CtorWithIpAndCidr1()
    {
        string ipaddress = "2001:0db8::";
        var ip = IPAddress.Parse(ipaddress);
        var ipnetwork = new IPNetwork2(ip, 124);
        Assert.AreEqual("2001:db8::/124", ipnetwork.ToString(), "network");
    }

    /// <summary>
    /// Test CtorWithIpAndCidr2.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void CtorWithIpAndCidr2()
    {
        string ipaddress = "2001:db8::";
        var ip = IPAddress.Parse(ipaddress);
        var ipn = new IPNetwork2(ip, 129);
    }
}
