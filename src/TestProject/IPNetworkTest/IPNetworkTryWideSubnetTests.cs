// <copyright file="IPNetworkTryWideSubnetTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class IPNetworkTryWideSubnetTests
{
    /// <summary>
    ///     Tests Try Wide Subnet functionality with a /1 network.
    /// </summary>
    [TestMethod]
    public void TryWideSubnet1()
    {
        string[] ips = ["1.1.1.1", "255.255.255.255", "2.2.2.2", "0.0.0.0"];
        var ipns = new List<IPNetwork2>();
        foreach (string ip in ips)
        {
            if (IPNetwork2.TryParse(ip, 32, out IPNetwork2 ipn))
            {
                ipns.Add(ipn);
            }
        }

        bool wide = IPNetwork2.TryWideSubnet(ipns.ToArray(), out IPNetwork2 ipnetwork);
        Assert.IsTrue(wide, "wide");
        Assert.AreEqual("0.0.0.0/0", ipnetwork.ToString(), "ipnetwork");
    }

    /// <summary>
    ///     Tests Try Wide Subnet functionality with a /2 network.
    /// </summary>
    [TestMethod]
    public void TryWideSubnet2()
    {
        string[] ips = ["1.1.1.1", "10.0.0.0", "2.2.2.2", "0.0.0.0"];
        var ipns = new List<IPNetwork2>();
        foreach (string ip in ips)
        {
            if (IPNetwork2.TryParse(ip, 32, out IPNetwork2 ipn))
            {
                ipns.Add(ipn);
            }
        }

        bool wide = IPNetwork2.TryWideSubnet(ipns.ToArray(), out IPNetwork2 ipnetwork);
        Assert.IsTrue(wide, "wide");
        Assert.AreEqual("0.0.0.0/4", ipnetwork.ToString(), "ipnetwork");
    }

    /// <summary>
    ///     Tests Try Wide Subnet functionality with a /3 network.
    /// </summary>
    [TestMethod]
    public void TryWideSubnet3()
    {
        string[] ips = ["a", "b", "c", "d"];
        var ipns = new List<IPNetwork2>();
        foreach (string ip in ips)
        {
            if (IPNetwork2.TryParse(ip, 32, out IPNetwork2 ipn))
            {
                ipns.Add(ipn);
            }
        }

        bool wide = IPNetwork2.TryWideSubnet(ipns.ToArray(), out IPNetwork2 _);
        Assert.IsFalse(wide, "wide");
    }

    /// <summary>
    ///     Tests Try Wide Subnet functionality with a /4 network.
    /// </summary>
    [TestMethod]
    public void TryWideSubnet4()
    {
        string[] ips = ["a", "b", "1.1.1.1", "d"];
        var ipns = new List<IPNetwork2>();
        foreach (string ip in ips)
        {
            if (IPNetwork2.TryParse(ip, 32, out IPNetwork2 ipn))
            {
                ipns.Add(ipn);
            }
        }

        bool wide = IPNetwork2.TryWideSubnet(ipns.ToArray(), out IPNetwork2 ipnetwork);
        Assert.IsTrue(wide, "wide");
        Assert.AreEqual("1.1.1.1/32", ipnetwork.ToString(), "ipnetwork");
    }

    /// <summary>
    ///     Tests Try Wide Subnet with null input to ensure proper null handling.
    /// </summary>
    [TestMethod]
    public void TryWideSubnetNull()
    {
        bool wide = IPNetwork2.TryWideSubnet(null, out IPNetwork2 _);
        Assert.IsFalse(wide, "wide");
    }
}