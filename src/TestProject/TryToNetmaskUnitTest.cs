
// <copyright file="TryToNetmaskUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// TryToNetmaskUnitTest test every single method
    /// </summary>
    [TestClass]
    public class TryToNetmaskUnitTest
    {
        [TestMethod]
        public void TryToNetmask1()
        {
            IPAddress result = null;
            bool parsed = IPNetwork.TryToNetmask(0, Sockets.AddressFamily.InterNetwork, out result);
            IPAddress expected = IPAddress.Parse("0.0.0.0");

            Assert.AreEqual(expected, result, "Netmask");
            Assert.AreEqual(true, parsed, "parsed");
        }

        [TestMethod]
        public void TryToNetmask2()
        {
            IPAddress result = null;
            bool parsed = IPNetwork.TryToNetmask(33, Sockets.AddressFamily.InterNetwork, out result);
            IPAddress expected = null;

            Assert.AreEqual(expected, result, "Netmask");
            Assert.AreEqual(false, parsed, "parsed");
        }

    }
}
