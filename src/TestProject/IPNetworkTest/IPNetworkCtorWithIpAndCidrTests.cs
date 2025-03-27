// <copyright file="IPNetworkCtorWithIpAndCidrTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest
{
    [TestClass]
    public class IPNetworkCtorWithIpAndCidrTests
    {
        /// <summary>
        ///     Tests Ctor With Ip And Cidr functionality with a /1 network.
        /// </summary>
        [TestMethod]
        public void CtorWithIpAndCidr1()
        {
            string ipaddress = "192.168.168.100";
            var ip = IPAddress.Parse(ipaddress);
            var ipnetwork = new IPNetwork2(ip, 24);
            Assert.AreEqual("192.168.168.0/24", ipnetwork.ToString(), "network");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CtorWithIpAndCidr2()
        {
            IPAddress ip = null;
            var ipnetwork = new IPNetwork2(ip, 24);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CtorWithIpAndCidr3()
        {
            string ipaddress = "192.168.168.100";
            var ip = IPAddress.Parse(ipaddress);
            var ipnetwork = new IPNetwork2(ip, 33);
        }
    }
}