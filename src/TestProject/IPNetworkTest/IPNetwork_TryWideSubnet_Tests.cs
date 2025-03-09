// <copyright file="IPNetwork_TryWideSubnet_Tests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// IPNetworkUnitTest test every single method.
/// </summary>
[TestClass]
public class IPNetwork_TryWideSubnet_Tests
{

    [TestMethod]
    public void TryWideSubnet1()
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

            IPNetwork2 ipnetwork = null;
            bool wide = IPNetwork2.TryWideSubnet(ipns.ToArray(), out ipnetwork);
            Assert.AreEqual(true, wide, "wide");
            Assert.AreEqual("0.0.0.0/0", ipnetwork.ToString(), "ipnetwork");
        }

    [TestMethod]
    public void TryWideSubnet2()
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

            IPNetwork2 ipnetwork = null;
            bool wide = IPNetwork2.TryWideSubnet(ipns.ToArray(), out ipnetwork);
            Assert.AreEqual(true, wide, "wide");
            Assert.AreEqual("0.0.0.0/4", ipnetwork.ToString(), "ipnetwork");
        }

    [TestMethod]
    public void TryWideSubnet3()
    {
            string[] ips = new[] { "a", "b", "c", "d" };
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

    [TestMethod]
    public void TryWideSubnet4()
    {
            string[] ips = new[] { "a", "b", "1.1.1.1", "d" };
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

    [TestMethod]
    public void TryWideSubnetNull()
    {
            IPNetwork2 ipnetwork = null;
            bool wide = IPNetwork2.TryWideSubnet(null, out ipnetwork);
            Assert.AreEqual(false, wide, "wide");
        }

    }