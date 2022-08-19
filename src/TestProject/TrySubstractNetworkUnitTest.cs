
// <copyright file="TrySubstractNetworkUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// TrySubstractNetworkUnitTest test every single method
    /// </summary>
    [TestClass]
    public class TrySubstractNetworkUnitTest
    {


        [TestMethod]
        public void TrySubstractNetwork1() {
            string[] ips = new[] { "178.82.0.0/16" };
            string substract = "178.82.131.209/32";

            List<IPNetwork> ipns = new List<IPNetwork>();
            Array.ForEach<string>(ips, new Action<string>(
                delegate(string ip)
                {
                    IPNetwork ipn;
                    if (IPNetwork.TryParse(ip, out ipn)) {
                        ipns.Add(ipn);
                    }
                }
            ));

            var nsubstract = IPNetwork.Parse(substract);

            IEnumerable<IPNetwork> result;
            bool substracted = IPNetwork.TrySubstractNetwork(ipns.ToArray(), nsubstract, out result);
            Assert.AreEqual(true, substracted, "substracted");

        }

        [TestMethod]
        public void TrySubstractNetwork2() {
            string[] ips = new[] { "0.0.0.0/0" };
            string substract = "1.1.1.1/32";

            List<IPNetwork> ipns = new List<IPNetwork>();
            Array.ForEach<string>(ips, new Action<string>(
                delegate(string ip)
                {
                    IPNetwork ipn;
                    if (IPNetwork.TryParse(ip, out ipn)) {
                        ipns.Add(ipn);
                    }
                }
            ));

            var nsubstract = IPNetwork.Parse(substract);

            IEnumerable<IPNetwork> result;
            bool substracted = IPNetwork.TrySubstractNetwork(ipns.ToArray(), nsubstract, out result);
            Assert.AreEqual(true, substracted, "substracted");
        }

    }
}
