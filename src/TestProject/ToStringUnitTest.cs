
// <copyright file="ToStringUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// ToStringUnitTest test every single method
    /// </summary>
    [TestClass]
    public class ToStringUnitTest
    {

        [TestMethod]
        public void TestToString()
        {
            IPNetwork ipnetwork = IPNetwork.Parse("192.168.15.1/8");
            string expected = "192.0.0.0/8";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString1()
        {
            IPNetwork ipnetwork = IPNetwork.Parse("192.168.15.1/9");
            string expected = "192.128.0.0/9";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString2()
        {
            IPNetwork ipnetwork = IPNetwork.Parse("192.168.15.1/10");
            string expected = "192.128.0.0/10";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString3()
        {
            IPNetwork ipnetwork = IPNetwork.Parse("192.168.15.1/11");
            string expected = "192.160.0.0/11";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString4()
        {
            IPNetwork ipnetwork = IPNetwork.Parse("192.168.15.1/12");
            string expected = "192.160.0.0/12";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString5()
        {
            IPNetwork ipnetwork = IPNetwork.Parse("192.168.15.1/13");
            string expected = "192.168.0.0/13";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString6()
        {
            IPNetwork ipnetwork = IPNetwork.Parse("192.168.15.1/14");
            string expected = "192.168.0.0/14";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString7()
        {
            IPNetwork ipnetwork = IPNetwork.Parse("192.168.15.1/15");
            string expected = "192.168.0.0/15";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString8()
        {
            IPNetwork ipnetwork = IPNetwork.Parse("192.168.15.1/16");
            string expected = "192.168.0.0/16";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

    }
}
