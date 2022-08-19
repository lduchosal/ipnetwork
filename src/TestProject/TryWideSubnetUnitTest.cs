
// <copyright file="TryWideSubnetUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// TryWideSubnetUnitTest test every single method
    /// </summary>
    [TestClass]
    public class TryWideSubnetUnitTest
    {

        [TestMethod]
        public void TryWideSubnet1()
        {
            string[] ips = new[] { "1.1.1.1", "255.255.255.255", "2.2.2.2", "0.0.0.0" };
            List<IPNetwork> ipns = new List<IPNetwork>();
            foreach (string ip in ips)
            {
                IPNetwork ipn;
                if (IPNetwork.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            IPNetwork ipnetwork = null;
            bool wide = IPNetwork.TryWideSubnet(ipns.ToArray(), out ipnetwork);
            Assert.AreEqual(true, wide, "wide");
            Assert.AreEqual("0.0.0.0/0", ipnetwork.ToString(), "ipnetwork");
        }

        [TestMethod]
        public void TryWideSubnet2()
        {
            string[] ips = new[] { "1.1.1.1", "10.0.0.0", "2.2.2.2", "0.0.0.0" };
            List<IPNetwork> ipns = new List<IPNetwork>();
            foreach (string ip in ips)
            {
                IPNetwork ipn;
                if (IPNetwork.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            IPNetwork ipnetwork = null;
            bool wide = IPNetwork.TryWideSubnet(ipns.ToArray(), out ipnetwork);
            Assert.AreEqual(true, wide, "wide");
            Assert.AreEqual("0.0.0.0/4", ipnetwork.ToString(), "ipnetwork");
        }

        [TestMethod]
        public void TryWideSubnet3()
        {
            string[] ips = new[] { "a", "b", "c", "d" };
            List<IPNetwork> ipns = new List<IPNetwork>();
            foreach (string ip in ips)
            {
                IPNetwork ipn;
                if (IPNetwork.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            IPNetwork ipnetwork = null;
            bool wide = IPNetwork.TryWideSubnet(ipns.ToArray(), out ipnetwork);
            Assert.AreEqual(false, wide, "wide");
        }

        [TestMethod]
        public void TryWideSubnet4()
        {
            string[] ips = new[] { "a", "b", "1.1.1.1", "d" };
            List<IPNetwork> ipns = new List<IPNetwork>();
            foreach (string ip in ips)
            {
                IPNetwork ipn;
                if (IPNetwork.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            IPNetwork ipnetwork = null;
            bool wide = IPNetwork.TryWideSubnet(ipns.ToArray(), out ipnetwork);
            Assert.AreEqual(true, wide, "wide");
            Assert.AreEqual("1.1.1.1/32", ipnetwork.ToString(), "ipnetwork");
        }

        [TestMethod]
        public void TryWideSubnetNull()
        {
            IPNetwork ipnetwork = null;
            bool wide = IPNetwork.TryWideSubnet(null, out ipnetwork);
            Assert.AreEqual(false, wide, "wide");
        }

    }
}
