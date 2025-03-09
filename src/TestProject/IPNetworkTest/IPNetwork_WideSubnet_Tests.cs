// <copyright file="IPNetwork_WideSubnet_Tests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// IPNetworkUnitTest test every single method.
/// </summary>
[TestClass]
public class IPNetwork_WideSubnet_Tests
{

    [TestMethod]
    public void WideSubnet1()
    {
            string[] ips = new[] { "1.1.1.1", "255.255.255.255", "2.2.2.2", "0.0.0.0" };
            var ipns = new List<IPNetwork2>();
            foreach (string ip in ips)
            {
                IPNetwork2 ipn;
                if (IPNetwork2.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            var ipnetwork = IPNetwork2.WideSubnet(ipns.ToArray());
            Assert.AreEqual("0.0.0.0/0", ipnetwork.ToString(), "ipnetwork");
        }

    [TestMethod]
    public void WideSubnet2()
    {
            string[] ips = new[] { "1.1.1.1", "10.0.0.0", "2.2.2.2", "0.0.0.0" };
            var ipns = new List<IPNetwork2>();
            foreach (string ip in ips)
            {
                IPNetwork2 ipn;
                if (IPNetwork2.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            var ipnetwork = IPNetwork2.WideSubnet(ipns.ToArray());
            Assert.AreEqual("0.0.0.0/4", ipnetwork.ToString(), "ipnetwork");
        }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void WideSubnetNull()
    {
            var ipnetwork = IPNetwork2.WideSubnet(null);
        }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void WideSubnetNull2()
    {
            string[] ips = new[] { "a", "b", "e", "d" };
            var ipns = new List<IPNetwork2>();
            foreach (string ip in ips)
            {
                IPNetwork2 ipn;
                if (IPNetwork2.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            var ipnetwork = IPNetwork2.WideSubnet(ipns.ToArray());
        }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void WideSubnetMixed()
    {
            var ipns = new List<IPNetwork2>
            {
                IPNetwork2.IANA_ABLK_RESERVED1,
                IPNetwork2.Parse("2001:0db8::/64"),
            };
            var ipnetwork = IPNetwork2.WideSubnet(ipns.ToArray());
        }

    }