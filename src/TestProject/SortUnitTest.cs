
// <copyright file="SortUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// SortUnitTest test every single method
    /// </summary>
    [TestClass]
    public class SortUnitTest
    {

        [TestMethod]
        public void TestSort1()
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

            ipns.Sort();
            Assert.AreEqual("0.0.0.0/32", ipns[0].ToString(), "0");
            Assert.AreEqual("1.1.1.1/32", ipns[1].ToString(), "1");
            Assert.AreEqual("2.2.2.2/32", ipns[2].ToString(), "2");
            Assert.AreEqual("255.255.255.255/32", ipns[3].ToString(), "3");
        }

        [TestMethod]
        public void TestSort2()
        {
            string[] ips = new[] { "0.0.0.100/32", "0.0.0.0/24" };
            List<IPNetwork> ipns = new List<IPNetwork>();
            foreach (string ip in ips)
            {
                IPNetwork ipn;
                if (IPNetwork.TryParse(ip, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            ipns.Sort();
            Assert.AreEqual("0.0.0.0/24", ipns[0].ToString(), "0");
            Assert.AreEqual("0.0.0.100/32", ipns[1].ToString(), "1");
        }

    }
}
