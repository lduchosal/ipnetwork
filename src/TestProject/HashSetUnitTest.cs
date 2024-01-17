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
            var ipnetwork1 = IPNetwork2.Parse("0.0.1.1/0");
            var ipnetwork2 = IPNetwork2.Parse("1.1.1.1/0");

            var hashset = new HashSet<IPNetwork2>();
            bool add1 = hashset.Add(ipnetwork1);
            bool add2 = hashset.Add(ipnetwork2);

            Assert.IsTrue(add1, "add1");
            Assert.IsFalse(add2, "add2");
        }

        [TestMethod]
        public void TestGetHashCode_HashSet_Add2()
        {
            var ipnetwork1 = IPNetwork2.Parse("0.0.0.0/1");
            var ipnetwork2 = IPNetwork2.Parse("1.0.0.0/1");

            var hashset = new HashSet<IPNetwork2>();
            bool add1 = hashset.Add(ipnetwork1);
            bool add2 = hashset.Add(ipnetwork2);

            Assert.IsTrue(add1, "add1");
            Assert.IsFalse(add2, "add2");
        }

        [TestMethod]
        public void TestGetHashCode_HashSet_Add3()
        {
            var ipnetwork1 = IPNetwork2.Parse("0.0.0.0/32");
            var ipnetwork2 = IPNetwork2.Parse("0.0.0.1/32");

            var hashset = new HashSet<IPNetwork2>();
            bool add1 = hashset.Add(ipnetwork1);
            bool add2 = hashset.Add(ipnetwork2);

            Assert.IsTrue(add1, "add1");
            Assert.IsTrue(add2, "add2");
        }

        #region Equals IPv6 vs IPv4

        [TestMethod]
        public void TestHashSet_Add_ipv6_ipv4_0()
        {
            var ipnetwork1 = IPNetwork2.Parse("::/32");
            var ipnetwork2 = IPNetwork2.Parse("0.0.0.0/32");

            var hashset = new HashSet<IPNetwork2>();
            bool add1 = hashset.Add(ipnetwork1);
            bool add2 = hashset.Add(ipnetwork2);

            Assert.IsTrue(add1, "add1");
            Assert.IsTrue(add2, "add2");
        }

        #endregion
    }
}
