// <copyright file="TrySupernetUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// TrySupernetUnitTest test every single method
    /// </summary>
    [TestClass]
    public class TrySupernetUnitTest
    {
        [TestMethod]
        public void TestTrySupernet1()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.1.1/24");
            IPNetwork supernetExpected = IPNetwork.Parse("192.168.0.0/23");
            IPNetwork supernet;
            bool parsed = true;
            bool result = network1.TrySupernet(network2, out supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestTrySupernet2()
        {
            IPNetwork network1 = null;
            IPNetwork network2 = IPNetwork.Parse("192.168.1.1/24");
            IPNetwork supernet;

#pragma warning disable 0618
            bool result = IPNetwork.TrySupernet(network1, network2, out supernet);
#pragma warning restore 0618
        }

        [TestMethod]
        public void TestTrySupernetStatic2()
        {
            IPNetwork network1 = IPNetwork.IANA_ABLK_RESERVED1;
            IPNetwork network2 = IPNetwork.Parse("192.168.1.1/24");
            IPNetwork supernet;

#pragma warning disable 0618
            bool result = IPNetwork.TrySupernet(network1, network2, out supernet);
#pragma warning restore 0618
        }

        [TestMethod]
        public void TestTrySupernet3()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.1.1/24");
            IPNetwork network2 = null;
            IPNetwork supernetExpected = null;
            IPNetwork supernet;
            bool parsed = false;
            bool result = network1.TrySupernet(network2, out supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

        [TestMethod]
        public void TestTrySupernet4()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.1.1/25");
            IPNetwork supernetExpected = null;
            IPNetwork supernet;
            bool parsed = false;
            bool result = network1.TrySupernet(network2, out supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

        [TestMethod]
        public void TestTrySupernet5()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.5.1/24");
            IPNetwork supernetExpected = null;
            IPNetwork supernet;
            bool parsed = false;
            bool result = network1.TrySupernet(network2, out supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

        [TestMethod]
        public void TestTrySupernet6()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.0.1/25");
            IPNetwork supernetExpected = IPNetwork.Parse("192.168.0.0/24");
            IPNetwork supernet;
            bool parsed = true;
            bool result = network1.TrySupernet(network2, out supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

        [TestMethod]
        public void TestTrySupernet7()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/25");
            IPNetwork network2 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork supernetExpected = IPNetwork.Parse("192.168.0.0/24");
            IPNetwork supernet;
            bool parsed = true;
            bool result = network1.TrySupernet(network2, out supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

        [TestMethod]
        public void TestTrySupernet8()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.1.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.2.1/24");
            IPNetwork supernetExpected = null;
            IPNetwork supernet;
            bool parsed = false;
            bool result = network1.TrySupernet(network2, out supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

        [TestMethod]
        public void TestTrySupernet9()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.1.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.2.1/24");
            IPNetwork[] network3 = new[] { network1, network2 };
            IPNetwork[] supernetExpected = new[] { network1, network2 };
            IPNetwork[] supernet;
            bool parsed = true;
            bool result = IPNetwork.TrySupernet(network3, out supernet);

            Assert.AreEqual(supernetExpected[0], supernet[0], "supernet");
            Assert.AreEqual(supernetExpected[1], supernet[1], "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

        [TestMethod]
        public void TestTrySupernet10()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.1.1/24");
            IPNetwork[] network3 = new[] { network1, network2 };
            IPNetwork[] supernetExpected = new[] { IPNetwork.Parse("192.168.0.0/23") };
            IPNetwork[] supernet;
            bool parsed = true;
            bool result = IPNetwork.TrySupernet(network3, out supernet);

            Assert.AreEqual(supernetExpected[0], supernet[0], "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

        [TestMethod]
        public void TestTrySupernet11()
        {
            IPNetwork[] network3 = null;
            IPNetwork[] supernetExpected = new[] { IPNetwork.Parse("192.168.0.0/23") };
            IPNetwork[] supernet;
            bool parsed = false;
            bool result = IPNetwork.TrySupernet(network3, out supernet);

            Assert.AreEqual(null, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }
    }
}
