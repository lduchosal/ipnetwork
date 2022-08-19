
// <copyright file="CtorUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// CtorUnitTest test every single method
    /// </summary>
    [TestClass]
    public class CtorUnitTest
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor1()
        {
            new IPNetwork(BigInteger.Zero, Sockets.AddressFamily.InterNetwork, 33);
        }
    }
}
