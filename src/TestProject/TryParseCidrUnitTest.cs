
// <copyright file="TryParseCidrUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// TryParseCidrUnitTest test every single method
    /// </summary>
    [TestClass]
    public class TryParseCidrUnitTest
    {

        [TestMethod]
        public void TryParseCidr1()
        {
            string sidr = "0";
            byte? cidr;
            byte? result = 0;
            bool parsed = IPNetwork.TryParseCidr(sidr, Sockets.AddressFamily.InterNetwork, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(result, cidr, "cidr");
        }

        [TestMethod]
        public void TryParseCidr2()
        {
            string sidr = "sadsd";
            byte? cidr;
            byte? result = null;

            bool parsed = IPNetwork.TryParseCidr(sidr, Sockets.AddressFamily.InterNetwork, out cidr);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(result, cidr, "cidr");
        }

        [TestMethod]
        public void TryParseCidr3()
        {
            string sidr = "33";
            byte? cidr;
            byte? result = null;

            bool parsed = IPNetwork.TryParseCidr(sidr, Sockets.AddressFamily.InterNetwork, out cidr);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(result, cidr, "cidr");
        }

    }
}
