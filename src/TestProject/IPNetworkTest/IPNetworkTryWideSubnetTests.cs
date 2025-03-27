// <copyright file="IPNetworkTryWideSubnetTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

[TestClass]
public class IPNetworkTryWideSubnetTests
{
    /// <summary>
    ///     Tests Try Wide Subnet functionality with a /1 network.
    /// </summary>
    [TestMethod]
    public void TryWideSubnet1()
    {
        string[] ips = { "1.1.1.1", "255.255.255.255", "2.2.2.2", "0.0.0.0" };
        var ipns = new List<IPNetwork2>();
        foreach (string ip in ips)
        {
            IPNetwork2 ipn;
            if (IPNetwork2.TryParse(ip, 32, out ipn))
            {
                ipns.Add(ipn);
            }
        }

        IPNetwork2 ipnetwork = null;
        bool wide = IPNetwork2.TryWideSubnet(ipns.ToArray(), out ipnetwork);
        Assert.AreEqual(true, wide, "wide");
        Assert.AreEqual("0.0.0.0/0", ipnetwork.ToString(), "ipnetwork");
    }

    /// <summary>
    ///     Tests Try Wide Subnet functionality with a /2 network.
    /// </summary>
    [TestMethod]
    public void TryWideSubnet2()
    {
        string[] ips = { "1.1.1.1", "10.0.0.0", "2.2.2.2", "0.0.0.0" };
        var ipns = new List<IPNetwork2>();
        foreach (string ip in ips)
        {
            IPNetwork2 ipn;
            if (IPNetwork2.TryParse(ip, 32, out ipn))
            {
                ipns.Add(ipn);
            }
        }

        IPNetwork2 ipnetwork = null;
        bool wide = IPNetwork2.TryWideSubnet(ipns.ToArray(), out ipnetwork);
        Assert.AreEqual(true, wide, "wide");
        Assert.AreEqual("0.0.0.0/4", ipnetwork.ToString(), "ipnetwork");
    }

    /// <summary>
    ///     Tests Try Wide Subnet functionality with a /3 network.
    /// </summary>
    [TestMethod]
    public void TryWideSubnet3()
    {
        string[] ips = { "a", "b", "c", "d" };
        var ipns = new List<IPNetwork2>();
        foreach (string ip in ips)
        {
            IPNetwork2 ipn;
            if (IPNetwork2.TryParse(ip, 32, out ipn))
            {
                ipns.Add(ipn);
            }
        }

        IPNetwork2 ipnetwork = null;
        bool wide = IPNetwork2.TryWideSubnet(ipns.ToArray(), out ipnetwork);
        Assert.AreEqual(false, wide, "wide");
    }

    /// <summary>
    ///     Tests Try Wide Subnet functionality with a /4 network.
    /// </summary>
    [TestMethod]
    public void TryWideSubnet4()
    {
        string[] ips = { "a", "b", "1.1.1.1", "d" };
        var ipns = new List<IPNetwork2>();
        foreach (string ip in ips)
        {
            IPNetwork2 ipn;
            if (IPNetwork2.TryParse(ip, 32, out ipn))
            {
                ipns.Add(ipn);
            }
        }

        IPNetwork2 ipnetwork = null;
        bool wide = IPNetwork2.TryWideSubnet(ipns.ToArray(), out ipnetwork);
        Assert.AreEqual(true, wide, "wide");
        Assert.AreEqual("1.1.1.1/32", ipnetwork.ToString(), "ipnetwork");
    }

    /// <summary>
    ///     Tests Try Wide Subnet with null input to ensure proper null handling.
    /// </summary>
    [TestMethod]
    public void TryWideSubnetNull()
    {
        IPNetwork2 ipnetwork = null;
        bool wide = IPNetwork2.TryWideSubnet(null, out ipnetwork);
        Assert.AreEqual(false, wide, "wide");
    }
}