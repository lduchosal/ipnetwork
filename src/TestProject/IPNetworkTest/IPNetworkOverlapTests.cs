// <copyright file="IPNetworkOverlapTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest
{
    [TestClass]
    public class IPNetworkOverlapTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestOverlap1()
    {
            IPNetwork2 network1 = null;
            IPNetwork2 network2 = null;

#pragma warning disable 0618
            bool result = IPNetwork2.Overlap(network1, network2);
#pragma warning restore 0618
        }

        /// <summary>
        ///     Tests Overlap functionality with Overlap Static2.
        /// </summary>
        [TestMethod]
        public void TestOverlapStatic2()
    {
            IPNetwork2 network1 = IPNetwork2.IANA_ABLK_RESERVED1;
            IPNetwork2 network2 = IPNetwork2.IANA_ABLK_RESERVED1;

#pragma warning disable 0618
            bool result = IPNetwork2.Overlap(network1, network2);
#pragma warning restore 0618

            Assert.IsTrue(result, "result");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestOverlap2()
    {
            var network1 = IPNetwork2.Parse("10.0.0.0/0");
            IPNetwork2 network2 = null;
            network1.Overlap(network2);
        }

        /// <summary>
        ///     Tests Overlap functionality with Overlap3.
        /// </summary>
        [TestMethod]
        public void TestOverlap3()
    {
            var network1 = IPNetwork2.Parse("10.0.0.0/0");
            var network2 = IPNetwork2.Parse("10.0.0.0/0");
            bool result = network1.Overlap(network2);
            bool expected = true;

            Assert.AreEqual(expected, result, "overlap");
        }

        /// <summary>
        ///     Tests Overlap functionality with Overlap4.
        /// </summary>
        [TestMethod]
        public void TestOverlap4()
    {
            var network1 = IPNetwork2.Parse("10.10.0.0/16");
            var network2 = IPNetwork2.Parse("10.10.1.0/24");
            bool result = network1.Overlap(network2);
            bool expected = true;

            Assert.AreEqual(expected, result, "overlap");
        }

        /// <summary>
        ///     Tests Overlap functionality with Overlap5.
        /// </summary>
        [TestMethod]
        public void TestOverlap5()
    {
            var network1 = IPNetwork2.Parse("10.10.0.0/24");
            var network2 = IPNetwork2.Parse("10.10.1.0/24");
            bool result = network1.Overlap(network2);
            bool expected = false;

            Assert.AreEqual(expected, result, "overlap");
        }

        /// <summary>
        ///     Tests Overlap functionality with Overlap6.
        /// </summary>
        [TestMethod]
        public void TestOverlap6()
    {
            var network1 = IPNetwork2.Parse("10.10.1.0/24");
            var network2 = IPNetwork2.Parse("10.10.0.0/16");
            bool result = network1.Overlap(network2);
            bool expected = true;

            Assert.AreEqual(expected, result, "overlap");
        }
    }
}