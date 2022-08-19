
// <copyright file="CompareUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// CompareUnitTest test every single method
    /// </summary>
    [TestClass]
    public class CompareUnitTest
    {

        [TestMethod]
        public void TestCompareTo1()
        {
            IPNetwork ipn1 = IPNetwork.Parse("10.0.0.1/16");
            IPNetwork ipn2 = IPNetwork.Parse("10.0.0.2/16");

            int comparison = ipn1.CompareTo(ipn2);

            Assert.AreEqual(0, comparison, "compare");
        }

        [TestMethod]
        public void TestCompareTo2()
        {
            IPNetwork ipn1 = IPNetwork.Parse("10.0.0.1/16");
            object ipn2 = (object)IPNetwork.Parse("10.0.0.2/16");

            int comparison = ipn1.CompareTo(ipn2);

            Assert.AreEqual(0, comparison, "compare");
        }

        [TestMethod]
        public void TestCompareTo3()
        {
            IPNetwork ipn1 = IPNetwork.Parse("10.0.0.1/16");
            object ipn2 = null;

            int comparison = ipn1.CompareTo(ipn2);

            Assert.AreEqual(1, comparison, "compare");
        }

        [TestMethod]
        public void TestCompareTo4()
        {
            IPNetwork ipn1 = IPNetwork.Parse("10.0.0.1/16");
            IPNetwork ipn2 = null;

            int comparison = ipn1.CompareTo(ipn2);

            Assert.AreEqual(1, comparison, "compare");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCompareTo5()
        {
            IPNetwork ipn1 = IPNetwork.Parse("10.0.0.1/16");
            string ipn2 = string.Empty;

            int comparison = ipn1.CompareTo(ipn2);
        }

        [TestMethod]
        public void TestCompareTo6()
        {
            IPNetwork ipn1 = IPNetwork.Parse("10.0.0.1/16");
            int comparison = ipn1.CompareTo(ipn1);

            Assert.AreEqual(0, comparison, "compare");
        }

        [TestMethod]
        public void TestCompare1()
        {
            IPNetwork ipn1 = IPNetwork.Parse("10.0.0.1/16");
            int comparison = IPNetwork.Compare(null, ipn1);

            Assert.AreEqual(-1, comparison, "compare");
        }

        [TestMethod]
        public void TestCompare2()
        {
            IPNetwork ipn1 = IPNetwork.Parse("10.0.0.1/16");
            IPNetwork ipn2 = IPNetwork.Parse("20.0.0.1/16");
            int comparison = IPNetwork.Compare(ipn1, ipn2);

            Assert.AreEqual(-1, comparison, "compare");
        }

    }
}
