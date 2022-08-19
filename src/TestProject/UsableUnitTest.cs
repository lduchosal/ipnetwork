
// <copyright file="UsableUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// UsableUnitTest test every single method
    /// </summary>
    [TestClass]
    public class UsableUnitTest
    {

        [TestMethod]
        public void Usable32()
        {
            var network = IPNetwork.Parse("0.0.0.0/32");
            uint usable = 0;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        [TestMethod]
        public void Usable31()
        {
            var network = IPNetwork.Parse("0.0.0.0/31");
            uint usable = 0;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        [TestMethod]
        public void Usable30()
        {
            var network = IPNetwork.Parse("0.0.0.0/30");
            uint usable = 2;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        [TestMethod]
        public void Usable24()
        {
            var network = IPNetwork.Parse("0.0.0.0/24");
            uint usable = 254;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        [TestMethod]
        public void Usable16()
        {
            var network = IPNetwork.Parse("0.0.0.0/16");
            uint usable = 65534;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        [TestMethod]
        public void Usable8()
        {
            var network = IPNetwork.Parse("0.0.0.0/8");
            uint usable = 16777214;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        [TestMethod]
        public void Usable0()
        {
            var network = IPNetwork.Parse("0.0.0.0/0");
            uint usable = 4294967294;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

    }
}
