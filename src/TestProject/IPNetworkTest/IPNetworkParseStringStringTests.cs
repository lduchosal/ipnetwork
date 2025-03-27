// <copyright file="IPNetworkParseStringStringTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest
{
    [TestClass]
    public class IPNetworkParseStringStringTests
    {
        /// <summary>
        ///     Tests Parse String String functionality with Parse String String1.
        /// </summary>
        [TestMethod]
        public void TestParseStringString1()
    {
            string ipaddress = "192.168.168.100";
            string netmask = "255.255.255.0";

            var ipnetwork = IPNetwork2.Parse(ipaddress, netmask);
            Assert.AreEqual("192.168.168.0/24", ipnetwork.ToString(), "network");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseStringString2()
    {
            string ipaddress = null;
            string netmask = null;

            var ipnetwork = IPNetwork2.Parse(ipaddress, netmask);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseStringString3()
    {
            string ipaddress = "192.168.168.100";
            string netmask = null;

            var ipnetwork = IPNetwork2.Parse(ipaddress, netmask);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseStringString4()
    {
            string ipaddress = string.Empty;
            string netmask = string.Empty;

            var ipnetwork = IPNetwork2.Parse(ipaddress, netmask);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseStringString5()
    {
            string ipaddress = "192.168.168.100";
            string netmask = string.Empty;

            var ipnetwork = IPNetwork2.Parse(ipaddress, netmask);
        }
    }
}