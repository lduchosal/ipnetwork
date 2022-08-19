
// <copyright file="WideSubnetUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// WideSubnetUnitTest test every single method
    /// </summary>
    [TestClass]
    public class WideSubnetUnitTest
    {

        [TestMethod]
        public void WideSubnet1()
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

            IPNetwork ipnetwork = IPNetwork.WideSubnet(ipns.ToArray());
            Assert.AreEqual("0.0.0.0/0", ipnetwork.ToString(), "ipnetwork");
        }

        [TestMethod]
        public void WideSubnet2()
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

            IPNetwork ipnetwork = IPNetwork.WideSubnet(ipns.ToArray());
            Assert.AreEqual("0.0.0.0/4", ipnetwork.ToString(), "ipnetwork");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WideSubnetNull()
        {
            IPNetwork ipnetwork = IPNetwork.WideSubnet(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WideSubnetNull2()
        {
            string[] ips = new[] { "a", "b", "e", "d" };
            List<IPNetwork> ipns = new List<IPNetwork>();
            foreach (string ip in ips)
            {
                IPNetwork ipn;
                if (IPNetwork.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            IPNetwork ipnetwork = IPNetwork.WideSubnet(ipns.ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WideSubnetMixed()
        {
            var ipns = new List<IPNetwork>
            {
                IPNetwork.IANA_ABLK_RESERVED1,
                IPNetwork.Parse("2001:0db8::/64"),
            };
            IPNetwork ipnetwork = IPNetwork.WideSubnet(ipns.ToArray());
        }

    }
}
