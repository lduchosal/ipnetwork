
// <copyright file="BitsSetUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// BitsSetUnitTest test every single method
    /// </summary>
    [TestClass]
    public class BitsSetUnitTest
    {

        [TestMethod]
        public void TestBitsSet32()
        {
            IPAddress ip = IPAddress.Parse("255.255.255.255");
            uint bits = 32;
            uint result = IPNetwork.BitsSet(ip);

            Assert.AreEqual(bits, result, "BitsSet");
        }

        [TestMethod]
        public void TestBitsSet24()
        {
            IPAddress ip = IPAddress.Parse("255.255.255.0");
            uint bits = 24;
            uint result = IPNetwork.BitsSet(ip);

            Assert.AreEqual(bits, result, "BitsSet");
        }

        [TestMethod]
        public void TestBitsSet16()
        {
            IPAddress ip = IPAddress.Parse("255.255.0.0");
            uint bits = 16;
            uint result = IPNetwork.BitsSet(ip);

            Assert.AreEqual(bits, result, "BitsSet");
        }

        [TestMethod]
        public void TestBitsSet4()
        {
            IPAddress ip = IPAddress.Parse("128.128.128.128");
            uint bits = 4;
            uint result = IPNetwork.BitsSet(ip);

            Assert.AreEqual(bits, result, "BitsSet");
        }

    }
}
