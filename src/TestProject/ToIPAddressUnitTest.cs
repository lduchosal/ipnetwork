
// <copyright file="ToIPAddressUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// ToIPAddressUnitTest test every single method
    /// </summary>
    [TestClass]
    public class ToIPAddressUnitTest
    {

        [TestMethod]
        public void TestToIPAddress()
        {
            var ip = new BigInteger(0);
            IPAddress result = IPNetwork.ToIPAddress(ip, Sockets.AddressFamily.InterNetwork);
            Assert.AreEqual(IPAddress.Any, result, "ToIPAddress");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestToIPAddress2()
        {
            var ip = new BigInteger(0);
            IPAddress result = IPNetwork.ToIPAddress(ip, Sockets.AddressFamily.AppleTalk);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestToIPAddress3()
        {
            var ip = new BigInteger(new byte[]
            {
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            });
            IPAddress result = IPNetwork.ToIPAddress(ip, Sockets.AddressFamily.AppleTalk);
        }
    }
}
