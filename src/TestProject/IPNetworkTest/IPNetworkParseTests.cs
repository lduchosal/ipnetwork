// <copyright file="IPNetworkParseTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest
{
    [TestClass]
    public class IPNetworkParseTests
    {
        [TestMethod]
        [TestCategory("Parse")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseIPAddressNetmaskAne2()
        {
            IPAddress ip = null;
            IPNetwork2.Parse(ip, ip);
        }

        [TestMethod]
        [TestCategory("Parse")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseIPAddressNetmaskAne3()
        {
            IPNetwork2.Parse(string.Empty, 0);
        }

        [TestMethod]
        [TestCategory("Parse")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseIPAddressNetmaskAne4()
        {
            IPNetwork2.Parse(null, 0);
        }

        [TestMethod]
        [TestCategory("Parse")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseIPAddressNetmaskAne5()
        {
            string n = null;
            IPNetwork2.Parse(n, n);
        }

        [TestMethod]
        [TestCategory("Parse")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseIPAddressNetmaskAne8()
        {
            var ipnet = IPNetwork2.Parse("x.x.x.x", "x.x.x.x");
        }

        [TestMethod]
        [TestCategory("Parse")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseIPAddressNetmaskAne9()
        {
            var ipnet = IPNetwork2.Parse("0.0.0.0", "x.x.x.x");
        }

        [TestMethod]
        [TestCategory("Parse")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseIPAddressNetmaskAne10()
        {
            var ipnet = IPNetwork2.Parse("x.x.x.x", 0);
        }

        [TestMethod]
        [TestCategory("Parse")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseIPAddressNetmaskAne11()
        {
            var ipnet = IPNetwork2.Parse("0.0.0.0", 33);
        }

        [TestCategory("Parse")]
        /// <summary>
        /// Tests Parse functionality with Parse IPAddress Netmask.
        /// </summary>
        [TestMethod]
        public void TestParseIPAddressNetmask()
        {
            string ipaddress = "192.168.168.100";
            string netmask = "255.255.255.0";

            string network = "192.168.168.0";
            string broadcast = "192.168.168.255";
            string firstUsable = "192.168.168.1";
            string lastUsable = "192.168.168.254";
            byte cidr = 24;
            uint usable = 254;

            var ipnetwork = IPNetwork2.Parse(ipaddress, netmask);
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        [TestCategory("Parse")]
        /// <summary>
        /// Tests Parse functionality with Parse String1.
        /// </summary>
        [TestMethod]
        public void TestParseString1()
        {
            string ipaddress = "192.168.168.100 255.255.255.0";

            string network = "192.168.168.0";
            string netmask = "255.255.255.0";
            string broadcast = "192.168.168.255";
            string firstUsable = "192.168.168.1";
            string lastUsable = "192.168.168.254";
            byte cidr = 24;
            uint usable = 254;

            var ipnetwork = IPNetwork2.Parse(ipaddress);
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        [TestCategory("Parse")]
        /// <summary>
        /// Tests Parse functionality with Parse String2.
        /// </summary>
        [TestMethod]
        public void TestParseString2()
        {
            string ipaddress = "192.168.168.100/24";

            string network = "192.168.168.0";
            string netmask = "255.255.255.0";
            string broadcast = "192.168.168.255";
            string firstUsable = "192.168.168.1";
            string lastUsable = "192.168.168.254";
            byte cidr = 24;
            uint usable = 254;

            var ipnetwork = IPNetwork2.Parse(ipaddress);
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        [TestCategory("Parse")]
        /// <summary>
        /// Tests Parse functionality with Parse String3.
        /// </summary>
        [TestMethod]
        public void TestParseString3()
        {
            string ipaddress = "0.0.0.0/0";

            string network = "0.0.0.0";
            string netmask = "0.0.0.0";
            string broadcast = "255.255.255.255";
            string firstUsable = "0.0.0.1";
            string lastUsable = "255.255.255.254";
            byte cidr = 0;
            uint usable = 4294967294;

            var ipnetwork = IPNetwork2.Parse(ipaddress);
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        [TestCategory("Parse")]
        /// <summary>
        /// Tests Parse functionality with Parse String4.
        /// </summary>
        [TestMethod]
        public void TestParseString4()
        {
            string ipaddress = "0.0.0.0/32";

            string network = "0.0.0.0";
            string netmask = "255.255.255.255";
            string broadcast = "0.0.0.0";
            string firstUsable = "0.0.0.0";
            string lastUsable = "0.0.0.0";
            byte cidr = 32;
            uint usable = 0;

            var ipnetwork = IPNetwork2.Parse(ipaddress);
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        [TestCategory("Parse")]
        /// <summary>
        /// Tests Parse functionality with Parse String5.
        /// </summary>
        [TestMethod]
        public void TestParseString5()
        {
            string ipaddress = "255.255.255.255/32";

            string network = "255.255.255.255";
            string netmask = "255.255.255.255";
            string broadcast = "255.255.255.255";
            string firstUsable = "255.255.255.255";
            string lastUsable = "255.255.255.255";
            byte cidr = 32;
            uint usable = 0;

            var ipnetwork = IPNetwork2.Parse(ipaddress);
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        [TestCategory("Parse")]
        /// <summary>
        /// Tests Parse functionality with Parse IPAddress No Netmask1.
        /// </summary>
        [TestMethod]
        public void TestParseIPAddressNoNetmask1()
        {
            string ipaddress = "10.0.0.0";

            string network = "10.0.0.0";
            string netmask = "255.0.0.0";
            string broadcast = "10.255.255.255";
            string firstUsable = "10.0.0.1";
            string lastUsable = "10.255.255.254";
            byte cidr = 8;
            uint usable = 16777214;

            var ipnetwork = IPNetwork2.Parse(ipaddress);
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        /// <summary>
        ///     Tests Parse functionality.
        /// </summary>
        [TestMethod]
        public void When_Parse_254_128_0_0_Should_Succeed()
        {
            string ipaddress = "254.128.0.0";

            string network = "254.128.0.0";
            string netmask = "255.255.255.0";
            string broadcast = "254.128.0.255";
            string firstUsable = "254.128.0.1";
            string lastUsable = "254.128.0.254";
            byte cidr = 24;
            uint usable = 254;

            var ipnetwork = IPNetwork2.Parse(ipaddress);
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        /// <summary>
        ///     Tests Parse functionality with Parse IPAddress No Netmask2.
        /// </summary>
        [TestMethod]
        public void TestParseIPAddressNoNetmask2()
        {
            string ipaddress = "172.0.0.0";

            string network = "172.0.0.0";
            string netmask = "255.255.0.0";
            string broadcast = "172.0.255.255";
            string firstUsable = "172.0.0.1";
            string lastUsable = "172.0.255.254";
            byte cidr = 16;
            uint usable = 65534;

            var ipnetwork = IPNetwork2.Parse(ipaddress);
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        /// <summary>
        ///     Tests Parse functionality with Parse IPAddress No Netmask3.
        /// </summary>
        [TestMethod]
        public void TestParseIPAddressNoNetmask3()
        {
            string ipaddress = "192.0.0.0";

            string network = "192.0.0.0";
            string netmask = "255.255.255.0";
            string broadcast = "192.0.0.255";
            string firstUsable = "192.0.0.1";
            string lastUsable = "192.0.0.254";
            byte cidr = 24;
            uint usable = 254;

            var ipnetwork = IPNetwork2.Parse(ipaddress);
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        /// <summary>
        ///     Tests Parse functionality with Parse IPAddress No Netmask1_Class Full.
        /// </summary>
        [TestMethod]
        public void TestParseIPAddressNoNetmask1_ClassFull()
        {
            string ipaddress = "10.0.0.0";
            ICidrGuess guessCidr = CidrGuess.ClassFull;

            var ipnetwork = IPNetwork2.Parse(ipaddress, guessCidr);

            string network = "10.0.0.0";
            string netmask = "255.0.0.0";
            string broadcast = "10.255.255.255";
            string firstUsable = "10.0.0.1";
            string lastUsable = "10.255.255.254";
            byte cidr = 8;
            uint usable = 16777214;

            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        /// <summary>
        ///     Tests Parse functionality with Parse IPAddress No Netmask2_Class Full.
        /// </summary>
        [TestMethod]
        public void TestParseIPAddressNoNetmask2_ClassFull()
        {
            string ipaddress = "172.0.0.0";
            ICidrGuess guessCidr = CidrGuess.ClassFull;

            var ipnetwork = IPNetwork2.Parse(ipaddress, guessCidr);

            string network = "172.0.0.0";
            string netmask = "255.255.0.0";
            string broadcast = "172.0.255.255";
            string firstUsable = "172.0.0.1";
            string lastUsable = "172.0.255.254";
            byte cidr = 16;
            uint usable = 65534;

            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        /// <summary>
        ///     Tests Parse functionality with Parse IPAddress No Netmask3_Class Full.
        /// </summary>
        [TestMethod]
        public void TestParseIPAddressNoNetmask3_ClassFull()
        {
            string ipaddress = "192.0.0.0";
            ICidrGuess guessCidr = CidrGuess.ClassFull;

            var ipnetwork = IPNetwork2.Parse(ipaddress, guessCidr);

            string network = "192.0.0.0";
            string netmask = "255.255.255.0";
            string broadcast = "192.0.0.255";
            string firstUsable = "192.0.0.1";
            string lastUsable = "192.0.0.254";
            byte cidr = 24;
            uint usable = 254;

            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        /// <summary>
        ///     Tests Parse functionality with Parse IPAddress No Netmask1_Class Less.
        /// </summary>
        [TestMethod]
        public void TestParseIPAddressNoNetmask1_ClassLess()
        {
            string ipaddress = "10.0.0.0";
            ICidrGuess guessCidr = CidrGuess.ClassLess;

            var ipnetwork = IPNetwork2.Parse(ipaddress, guessCidr);

            string network = "10.0.0.0";
            string netmask = "255.255.255.255";
            string broadcast = "10.0.0.0";
            string firstUsable = "10.0.0.0";
            string lastUsable = "10.0.0.0";
            byte cidr = 32;
            uint usable = 0;

            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        /// <summary>
        ///     Tests Parse functionality with Parse IPAddress No Netmask2_Class Less.
        /// </summary>
        [TestMethod]
        public void TestParseIPAddressNoNetmask2_ClassLess()
        {
            string ipaddress = "172.0.0.0";
            ICidrGuess guessCidr = CidrGuess.ClassLess;

            var ipnetwork = IPNetwork2.Parse(ipaddress, guessCidr);

            string network = "172.0.0.0";
            string netmask = "255.255.255.255";
            string broadcast = "172.0.0.0";
            string firstUsable = "172.0.0.0";
            string lastUsable = "172.0.0.0";
            byte cidr = 32;
            uint usable = 0;

            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        /// <summary>
        ///     Tests Parse functionality with Parse IPAddress No Netmask3_Class Less.
        /// </summary>
        [TestMethod]
        public void TestParseIPAddressNoNetmask3_ClassLess()
        {
            string ipaddress = "192.0.0.0";
            ICidrGuess guessCidr = CidrGuess.ClassLess;

            var ipnetwork = IPNetwork2.Parse(ipaddress, guessCidr);

            string network = "192.0.0.0";
            string netmask = "255.255.255.255";
            string broadcast = "192.0.0.0";
            string firstUsable = "192.0.0.0";
            string lastUsable = "192.0.0.0";
            byte cidr = 32;
            uint usable = 0;

            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        /// <summary>
        ///     Tests Parse functionality with Parse IPAddress No Netmask4.
        /// </summary>
        [TestMethod]
        public void TestParseIPAddressNoNetmask4()
        {
            string ipaddress = "224.0.0.0";
            var ipnetwork = IPNetwork2.Parse(ipaddress);

            Assert.AreEqual("224.0.0.0/24", ipnetwork.ToString(), "Network");
        }

        /// <summary>
        ///     Tests Parse functionality with Parse IPAddress No Netmask5.
        /// </summary>
        [TestMethod]
        public void TestParseIPAddressNoNetmask5()
        {
            string ipaddress = "240.0.0.0";
            var ipnetwork = IPNetwork2.Parse(ipaddress);
            Assert.AreEqual("240.0.0.0/24", ipnetwork.ToString(), "Network");
        }

        /// <summary>
        ///     Tests Parse functionality with Parse IPAddress No Netmask127001.
        /// </summary>
        [TestMethod]
        public void TestParseIPAddressNoNetmask127001()
        {
            string ipaddress = "127.0.0.1";
            IPNetwork2 result = null;
            IPNetwork2.TryParse(ipaddress, out result);
            Assert.AreEqual(result.Cidr, 8);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseStringAe1()
        {
            string ipaddress = "garbage";
            var ipnetwork = IPNetwork2.Parse(ipaddress);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseStringAe2()
        {
            string ipaddress = "0.0.0.0 0.0.1.0";
            var ipnetwork = IPNetwork2.Parse(ipaddress);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseStringAne1()
        {
            string ipaddress = null;
            var ipnetwork = IPNetwork2.Parse(ipaddress);
        }
    }
}