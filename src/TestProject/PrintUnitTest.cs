
// <copyright file="PrintUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// PrintUnitTest test every single method
    /// </summary>
    [TestClass]
    public class PrintUnitTest
    {

        [TestMethod]
        public void Print()
        {
            IPNetwork ipn = IPNetwork.Parse("0.0.0.0/0");
            string print = ipn.Print().Replace("\r", string.Empty);
            string expected = @"IPNetwork   : 0.0.0.0/0
Network     : 0.0.0.0
Netmask     : 0.0.0.0
Cidr        : 0
Broadcast   : 255.255.255.255
FirstUsable : 0.0.0.1
LastUsable  : 255.255.255.254
Usable      : 4294967294
".Replace("\r", string.Empty);

            Assert.AreEqual(expected, print, "Print");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PrintNull()
        {
            IPNetwork ipn = null;
#pragma warning disable 0618
            string print = IPNetwork.Print(ipn);
#pragma warning restore 0618
        }

        [TestMethod]
        public void PrintStatic()
        {
            IPNetwork ipn = IPNetwork.IANA_ABLK_RESERVED1;
#pragma warning disable 0618
            string print = IPNetwork.Print(ipn);
#pragma warning restore 0618
        }

    }
}
