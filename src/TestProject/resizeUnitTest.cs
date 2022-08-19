
// <copyright file="resizeUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// resizeUnitTest test every single method
    /// </summary>
    [TestClass]
    public class resizeUnitTest
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestResize1()
        {
            var resut = IPNetwork.Resize(new byte[33], Sockets.AddressFamily.InterNetwork);
        }

    }
}
