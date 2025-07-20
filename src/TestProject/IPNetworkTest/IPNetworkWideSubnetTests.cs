// <copyright file="IPNetworkWideSubnetTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
///     Tests for the WideSubnet method in IPNetwork2.
/// </summary>
[TestClass]
public class IPNetworkWideSubnetTests
{
    /// <summary>
    ///     Tests WideSubnet with a diverse set of addresses, expecting a /0 network.
    /// </summary>
    [TestMethod]
    public void WideSubnet1()
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

            var ipnetwork = IPNetwork2.WideSubnet(ipns.ToArray());
            Assert.AreEqual("0.0.0.0/0", ipnetwork.ToString(), "ipnetwork");
        }

    /// <summary>
    ///     Tests WideSubnet with addresses in same range, expecting a /4 network.
    /// </summary>
    [TestMethod]
    public void WideSubnet2()
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

            var ipnetwork = IPNetwork2.WideSubnet(ipns.ToArray());
            Assert.AreEqual("0.0.0.0/4", ipnetwork.ToString(), "ipnetwork");
        }

    /// <summary>
    ///     Tests WideSubnet with null input to ensure it throws ArgumentNullException.
    /// </summary>
    [TestMethod]
    public void WideSubnetNull()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            IPNetwork2.WideSubnet(null);
        });
    }

    /// <summary>
    ///     Tests WideSubnet with invalid IP addresses to ensure it throws ArgumentException.
    /// </summary>
    [TestMethod]
    public void WideSubnetNull2()
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
        {
            string[] ips = ["a", "b", "e", "d"];
            var ipns = new List<IPNetwork2>();
            foreach (string ip in ips)
            {
                if (IPNetwork2.TryParse(ip, 32, out IPNetwork2 ipn))
                {
                    ipns.Add(ipn);
                }
            }

            IPNetwork2.WideSubnet(ipns.ToArray());
        });
    }

    /// <summary>
    ///     Tests WideSubnet with mixed IPv4 and IPv6 addresses to ensure it throws ArgumentException.
    /// </summary>
    [TestMethod]
    public void WideSubnetMixed()
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
        {
            var ipns = new List<IPNetwork2>
            {
                IPNetwork2.IANA_ABLK_RESERVED1,
                IPNetwork2.Parse("2001:0db8::/64"),
            };
            IPNetwork2.WideSubnet(ipns.ToArray());
        });
    }
}