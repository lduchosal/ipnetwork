// <copyright file="IPNetwork_Compare_Tests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// IPNetworkUnitTest test every single method.
/// </summary>
[TestClass]
public class IPNetwork_Compare_Tests
{

    [TestMethod]
    public void TestCompareTo1()
    {
            var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
            var ipn2 = IPNetwork2.Parse("10.0.0.2/16");

            int comparison = ipn1.CompareTo(ipn2);

            Assert.AreEqual(0, comparison, "compare");
        }

    [TestMethod]
    public void TestCompareTo2()
    {
            var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
            object ipn2 = (object)IPNetwork2.Parse("10.0.0.2/16");

            int comparison = ipn1.CompareTo(ipn2);

            Assert.AreEqual(0, comparison, "compare");
        }

    [TestMethod]
    public void TestCompareTo3()
    {
            var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
            object ipn2 = null;

            int comparison = ipn1.CompareTo(ipn2);

            Assert.AreEqual(1, comparison, "compare");
        }

    [TestMethod]
    public void TestCompareTo4()
    {
            var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
            IPNetwork2 ipn2 = null;

            int comparison = ipn1.CompareTo(ipn2);

            Assert.AreEqual(1, comparison, "compare");
        }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestCompareTo5()
    {
            var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
            string ipn2 = string.Empty;

            int comparison = ipn1.CompareTo(ipn2);
        }

    [TestMethod]
    public void TestCompareTo6()
    {
            var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
            int comparison = ipn1.CompareTo(ipn1);

            Assert.AreEqual(0, comparison, "compare");
        }

    [TestMethod]
    public void TestCompare1()
    {
            var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
            int comparison = IPNetwork2.Compare(null, ipn1);

            Assert.AreEqual(-1, comparison, "compare");
        }

    [TestMethod]
    public void TestCompare2()
    {
            var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
            var ipn2 = IPNetwork2.Parse("20.0.0.1/16");
            int comparison = IPNetwork2.Compare(ipn1, ipn2);

            Assert.AreEqual(-1, comparison, "compare");
        }

    }