
// <copyright file="ParseUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// ParseUnitTest test every single method
    /// </summary>
    [TestClass]
    public class ParseUnitTest
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseIPAddressNetmaskANE2()
        {
            IPAddress ip = null;
            IPNetwork.Parse(ip, ip);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseIPAddressNetmaskANE3()
        {
            IPNetwork.Parse(string.Empty, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseIPAddressNetmaskANE4()
        {
            IPNetwork.Parse((string)null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseIPAddressNetmaskANE5()
        {
            string n = null;
            IPNetwork.Parse(n, n);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseIPAddressNetmaskANE8()
        {
            IPNetwork ipnet = IPNetwork.Parse("x.x.x.x", "x.x.x.x");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseIPAddressNetmaskANE9()
        {
            IPNetwork ipnet = IPNetwork.Parse("0.0.0.0", "x.x.x.x");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseIPAddressNetmaskANE10()
        {
            IPNetwork ipnet = IPNetwork.Parse("x.x.x.x", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseIPAddressNetmaskANE11()
        {
            IPNetwork ipnet = IPNetwork.Parse("0.0.0.0", 33);
        }

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

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress, netmask);
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

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

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress);
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

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

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress);
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

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

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress);
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

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

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress);
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

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

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress);
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

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

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress);
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

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

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress);
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

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

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress);
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

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

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress);
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        [TestMethod]
        public void TestParseIPAddressNoNetmask1_ClassFull()
        {
            string ipaddress = "10.0.0.0";
            var guessCidr = CidrGuess.ClassFull;

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress, guessCidr);

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

        [TestMethod]
        public void TestParseIPAddressNoNetmask2_ClassFull()
        {
            string ipaddress = "172.0.0.0";
            var guessCidr = CidrGuess.ClassFull;

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress, guessCidr);

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

        [TestMethod]
        public void TestParseIPAddressNoNetmask3_ClassFull()
        {
            string ipaddress = "192.0.0.0";
            var guessCidr = CidrGuess.ClassFull;

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress, guessCidr);

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

        [TestMethod]
        public void TestParseIPAddressNoNetmask1_ClassLess()
        {
            string ipaddress = "10.0.0.0";
            var guessCidr = CidrGuess.ClassLess;

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress, guessCidr);

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

        [TestMethod]
        public void TestParseIPAddressNoNetmask2_ClassLess()
        {
            string ipaddress = "172.0.0.0";
            var guessCidr = CidrGuess.ClassLess;

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress, guessCidr);

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

        [TestMethod]
        public void TestParseIPAddressNoNetmask3_ClassLess()
        {
            string ipaddress = "192.0.0.0";
            var guessCidr = CidrGuess.ClassLess;

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress, guessCidr);

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

        [TestMethod]
        public void TestParseIPAddressNoNetmask4()
        {
            string ipaddress = "224.0.0.0";
            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress);

            Assert.AreEqual("224.0.0.0/24", ipnetwork.ToString(), "Network");
        }

        [TestMethod]
        public void TestParseIPAddressNoNetmask5()
        {
            string ipaddress = "240.0.0.0";
            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress);
            Assert.AreEqual("240.0.0.0/24", ipnetwork.ToString(), "Network");
        }

        [TestMethod]
        public void TestParseIPAddressNoNetmask127001()
        {
            string ipaddress = "127.0.0.1";
            IPNetwork result = null;
            IPNetwork.TryParse(ipaddress, out result);
            Assert.AreEqual(result.Cidr, 8);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseStringAE1()
        {
            string ipaddress = "garbage";
            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseStringAE2()
        {
            string ipaddress = "0.0.0.0 0.0.1.0";
            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseStringANE1()
        {
            string ipaddress = null;
            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress);
        }

    }
}
