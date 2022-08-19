
// <copyright file="SupernetArrayUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// SupernetArrayUnitTest test every single method
    /// </summary>
    [TestClass]
    public class SupernetArrayUnitTest
    {

        [TestMethod]
        public void TestTrySupernetArray()
        {
            IPNetwork ipnetwork1 = IPNetwork.Parse("192.168.0.0/24");
            IPNetwork ipnetwork2 = IPNetwork.Parse("192.168.1.0/24");
            IPNetwork ipnetwork3 = IPNetwork.Parse("192.168.2.0/24");
            IPNetwork ipnetwork4 = IPNetwork.Parse("192.168.3.0/24");

            IPNetwork[] ipnetworks = { ipnetwork1, ipnetwork2, ipnetwork3, ipnetwork4 };
            IPNetwork[] expected = { IPNetwork.Parse("192.168.0.0/22") };

            IPNetwork[] result = IPNetwork.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], expected[0], "suppernet");
        }

        [TestMethod]
        public void TestTrySupernetArray1()
        {
            IPNetwork[] ipnetworks = { };
            IPNetwork[] expected = { };

            IPNetwork[] result = IPNetwork.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestTrySupernetArray2()
        {
            IPNetwork[] ipnetworks = null;
            IPNetwork[] result = IPNetwork.Supernet(ipnetworks);
        }

        [TestMethod]
        public void TestTrySupernetArray3()
        {
            IPNetwork ipnetwork1 = null;
            IPNetwork ipnetwork2 = null;
            IPNetwork ipnetwork3 = null;
            IPNetwork ipnetwork4 = null;

            IPNetwork[] ipnetworks = { ipnetwork1, ipnetwork2, ipnetwork3, ipnetwork4 };
            IPNetwork[] expected = { };

            IPNetwork[] result = IPNetwork.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
        }

        [TestMethod]
        public void TestTrySupernetArray4()
        {
            IPNetwork ipnetwork1 = IPNetwork.Parse("192.168.0.0/24");
            IPNetworkCollection subnetted = ipnetwork1.Subnet(32);
            IPNetwork[] ipnetworks = subnetted.ToArray();
            Assert.AreEqual(256, ipnetworks.Length, "subnet");

            IPNetwork[] expected = { IPNetwork.Parse("192.168.0.0/24") };

            IPNetwork[] result = IPNetwork.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], ipnetwork1, "suppernet");
        }

        [TestMethod]
        public void TestTrySupernetArray5()
        {
            IPNetwork ipnetwork1 = IPNetwork.Parse("192.168.0.0/16");
            IPNetworkCollection subnetted = ipnetwork1.Subnet(24);
            IPNetwork[] ipnetworks = subnetted.ToArray();
            Assert.AreEqual(256, ipnetworks.Length, "subnet");

            IPNetwork[] expected = { IPNetwork.Parse("192.168.0.0/16") };

            IPNetwork[] result = IPNetwork.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], ipnetwork1, "suppernet");
        }

        [TestMethod]
        public void TestTrySupernetArray6()
        {
            IPNetwork ipnetwork1 = IPNetwork.Parse("192.168.0.0/8");
            IPNetworkCollection subnetted = ipnetwork1.Subnet(24);
            IPNetwork[] ipnetworks = subnetted.ToArray();
            Assert.AreEqual(65536, ipnetworks.Length, "subnet");

            IPNetwork[] expected = { IPNetwork.Parse("192.0.0.0/8") };

            IPNetwork[] result = IPNetwork.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], ipnetwork1, "suppernet");
        }

        [TestMethod]
        public void TestTrySupernetArray7()
        {
            IPNetwork[] ipnetworks =
            {
                IPNetwork.Parse("10.0.2.2/24"),
                IPNetwork.Parse("192.168.0.0/24"),
                IPNetwork.Parse("192.168.1.0/24"),
                IPNetwork.Parse("192.168.2.0/24"),
                IPNetwork.Parse("10.0.1.1/24"),
                IPNetwork.Parse("192.168.3.0/24"),
            };

            IPNetwork[] expected =
            {
                IPNetwork.Parse("10.0.1.0/24"),
                IPNetwork.Parse("10.0.2.0/24"),
                IPNetwork.Parse("192.168.0/22"),
            };

            IPNetwork[] result = IPNetwork.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], result[0], "suppernet");
            Assert.AreEqual(expected[1], result[1], "suppernet1");
            Assert.AreEqual(expected[2], result[2], "suppernet2");
        }

        [TestMethod]
        public void TestTrySupernetArray8()
        {
            IPNetwork[] ipnetworks =
            {
                IPNetwork.Parse("10.0.2.2/24"),
                IPNetwork.Parse("192.168.0.0/24"),
                IPNetwork.Parse("192.168.1.0/24"),
                IPNetwork.Parse("192.168.2.0/24"),
                IPNetwork.Parse("10.0.1.1/24"),
                IPNetwork.Parse("192.168.3.0/24"),
                IPNetwork.Parse("10.6.6.6/8"),
            };

            IPNetwork[] expected =
            {
                IPNetwork.Parse("10.0.0.0/8"),
                IPNetwork.Parse("192.168.0/22"),
            };

            IPNetwork[] result = IPNetwork.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], result[0], "suppernet");
            Assert.AreEqual(expected[1], result[1], "suppernet1");
        }

        [TestMethod]
        public void TestTrySupernetArray9()
        {
            IPNetwork[] ipnetworks =
            {
                IPNetwork.Parse("10.0.2.2/24"),
                IPNetwork.Parse("192.168.0.0/24"),
                IPNetwork.Parse("192.168.1.0/24"),
                IPNetwork.Parse("192.168.2.0/24"),
                IPNetwork.Parse("10.0.1.1/24"),
                IPNetwork.Parse("192.168.3.0/24"),
                IPNetwork.Parse("10.6.6.6/8"),
                IPNetwork.Parse("11.6.6.6/8"),
                IPNetwork.Parse("12.6.6.6/8"),
            };

            IPNetwork[] expected =
            {
                IPNetwork.Parse("10.0.0.0/7"),
                IPNetwork.Parse("12.0.0.0/8"),
                IPNetwork.Parse("192.168.0/22"),
            };

            IPNetwork[] result = IPNetwork.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], result[0], "suppernet");
            Assert.AreEqual(expected[1], result[1], "suppernet1");
            Assert.AreEqual(expected[2], result[2], "suppernet2");
        }

        [TestMethod]
        public void TestTrySupernetArray10()
        {
            IPNetwork[] ipnetworks =
            {
                IPNetwork.Parse("10.0.2.2/24"),
                IPNetwork.Parse("10.0.2.2/23"),
            };

            IPNetwork[] expected =
            {
                IPNetwork.Parse("10.0.2.2/23"),
            };

            IPNetwork[] result = IPNetwork.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], result[0], "suppernet");
        }

    }
}
