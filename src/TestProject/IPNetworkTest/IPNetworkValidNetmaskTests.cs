// <copyright file="IPNetworkValidNetmaskTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest
{
    [TestClass]
    public class IPNetworkValidNetmaskTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestValidNetmaskInvalid1()
    {
            bool resut = IPNetwork2.InternalValidNetmask(BigInteger.Zero, AddressFamily.AppleTalk);
        }

        /// <summary>
        ///     Tests Valid Netmask functionality with Valid Netmask0.
        /// </summary>
        [TestMethod]
        public void TestValidNetmask0()
    {
            var mask = IPAddress.Parse("255.255.255.255");
            bool expected = true;
            bool result = IPNetwork2.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

        /// <summary>
        ///     Tests Valid Netmask functionality with Valid Netmask1.
        /// </summary>
        [TestMethod]
        public void TestValidNetmask1()
    {
            var mask = IPAddress.Parse("255.255.255.0");
            bool expected = true;
            bool result = IPNetwork2.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

        /// <summary>
        ///     Tests Valid Netmask functionality with Valid Netmask2.
        /// </summary>
        [TestMethod]
        public void TestValidNetmask2()
    {
            var mask = IPAddress.Parse("255.255.0.0");
            bool expected = true;
            bool result = IPNetwork2.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

        /// <summary>
        ///     Tests Valid Netmask functionality with Valid Netmask EAE1.
        /// </summary>
        [TestMethod]
        public void TestValidNetmaskEae1()
    {
            var mask = IPAddress.Parse("0.255.0.0");
            bool expected = false;
            bool result = IPNetwork2.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestValidNetmaskEae2()
    {
            IPAddress mask = null;
            bool expected = true;
            bool result = IPNetwork2.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

        /// <summary>
        ///     Tests Valid Netmask functionality with Valid Netmask EAE3.
        /// </summary>
        [TestMethod]
        public void TestValidNetmaskEae3()
    {
            var mask = IPAddress.Parse("255.255.0.1");
            bool expected = false;
            bool result = IPNetwork2.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }
    }
}