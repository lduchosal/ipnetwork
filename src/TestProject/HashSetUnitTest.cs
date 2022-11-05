// <copyright file="HashSetUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net.TestProject
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HashSetUnitTest
    {
        [TestMethod]
        public void TestGetHashCode_HashSet_Add1()
        {
            IPNetwork ipnetwork1 = IPNetwork.Parse("0.0.1.1/0");
            IPNetwork ipnetwork2 = IPNetwork.Parse("1.1.1.1/0");

            var hashset = new HashSet<IPNetwork>();
            bool add1 = hashset.Add(ipnetwork1);
            bool add2 = hashset.Add(ipnetwork2);

            Assert.IsTrue(add1, "add1");
            Assert.IsFalse(add2, "add2");
        }

        [TestMethod]
        public void TestGetHashCode_HashSet_Add2()
        {
            IPNetwork ipnetwork1 = IPNetwork.Parse("0.0.0.0/1");
            IPNetwork ipnetwork2 = IPNetwork.Parse("1.0.0.0/1");

            var hashset = new HashSet<IPNetwork>();
            bool add1 = hashset.Add(ipnetwork1);
            bool add2 = hashset.Add(ipnetwork2);

            Assert.IsTrue(add1, "add1");
            Assert.IsFalse(add2, "add2");
        }

        [TestMethod]
        public void TestGetHashCode_HashSet_Add3()
        {
            IPNetwork ipnetwork1 = IPNetwork.Parse("0.0.0.0/32");
            IPNetwork ipnetwork2 = IPNetwork.Parse("0.0.0.1/32");

            var hashset = new HashSet<IPNetwork>();
            bool add1 = hashset.Add(ipnetwork1);
            bool add2 = hashset.Add(ipnetwork2);

            Assert.IsTrue(add1, "add1");
            Assert.IsTrue(add2, "add2");
        }

        #region Equals IPv6 vs IPv4

        [TestMethod]
        public void TestHashSet_Add_ipv6_ipv4_0()
        {
            IPNetwork ipnetwork1 = IPNetwork.Parse("::/32");
            IPNetwork ipnetwork2 = IPNetwork.Parse("0.0.0.0/32");

            var hashset = new HashSet<IPNetwork>();
            bool add1 = hashset.Add(ipnetwork1);
            bool add2 = hashset.Add(ipnetwork2);

            Assert.IsTrue(add1, "add1");
            Assert.IsTrue(add2, "add2");
        }

        #endregion
    }
}
