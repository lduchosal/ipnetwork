
// <copyright file="TryToUintUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// TryToUintUnitTest test every single method
    /// </summary>
    [TestClass]
    public class TryToUintUnitTest
    {

        [TestMethod]
        public void TestTryToUint1()
        {
            BigInteger? result = null;
            bool parsed = IPNetwork.TryToUint(32, Sockets.AddressFamily.InterNetwork, out result);

            Assert.IsNotNull(result, "uint");
            Assert.AreEqual(true, parsed, "parsed");
        }

    }
}
