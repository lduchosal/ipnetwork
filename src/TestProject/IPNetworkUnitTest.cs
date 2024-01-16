﻿// <copyright file="IPNetworkUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net.TestProject
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// IPNetworkUnitTest test every single method.
    /// </summary>
    [TestClass]
    public class IPNetworkUnitTest
    {
        #region ctor

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor1()
        {
            new IPNetwork2(BigInteger.Zero, Sockets.AddressFamily.InterNetwork, 33);
        }
        #endregion

        #region Parse

        [TestMethod]
        [TestCategory("Parse")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseIPAddressNetmaskANE2()
        {
            IPAddress ip = null;
            IPNetwork2.Parse(ip, ip);
        }

        [TestMethod]
        [TestCategory("Parse")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseIPAddressNetmaskANE3()
        {
            IPNetwork2.Parse(string.Empty, 0);
        }

        [TestMethod]
        [TestCategory("Parse")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseIPAddressNetmaskANE4()
        {
            IPNetwork2.Parse((string)null, 0);
        }

        [TestMethod]
        [TestCategory("Parse")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseIPAddressNetmaskANE5()
        {
            string n = null;
            IPNetwork2.Parse(n, n);
        }

        [TestMethod]
        [TestCategory("Parse")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseIPAddressNetmaskANE8()
        {
            var ipnet = IPNetwork2.Parse("x.x.x.x", "x.x.x.x");
        }

        [TestMethod]
        [TestCategory("Parse")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseIPAddressNetmaskANE9()
        {
            var ipnet = IPNetwork2.Parse("0.0.0.0", "x.x.x.x");
        }

        [TestMethod]
        [TestCategory("Parse")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseIPAddressNetmaskANE10()
        {
            var ipnet = IPNetwork2.Parse("x.x.x.x", 0);
        }

        [TestMethod]
        [TestCategory("Parse")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseIPAddressNetmaskANE11()
        {
            var ipnet = IPNetwork2.Parse("0.0.0.0", 33);
        }

        [TestCategory("Parse")]
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

        [TestMethod]
        public void TestParseIPAddressNoNetmask4()
        {
            string ipaddress = "224.0.0.0";
            var ipnetwork = IPNetwork2.Parse(ipaddress);

            Assert.AreEqual("224.0.0.0/24", ipnetwork.ToString(), "Network");
        }

        [TestMethod]
        public void TestParseIPAddressNoNetmask5()
        {
            string ipaddress = "240.0.0.0";
            var ipnetwork = IPNetwork2.Parse(ipaddress);
            Assert.AreEqual("240.0.0.0/24", ipnetwork.ToString(), "Network");
        }

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
        public void TestParseStringAE1()
        {
            string ipaddress = "garbage";
            var ipnetwork = IPNetwork2.Parse(ipaddress);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseStringAE2()
        {
            string ipaddress = "0.0.0.0 0.0.1.0";
            var ipnetwork = IPNetwork2.Parse(ipaddress);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseStringANE1()
        {
            string ipaddress = null;
            var ipnetwork = IPNetwork2.Parse(ipaddress);
        }

        #endregion

        #region TryParse

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE2()
        {
            IPNetwork2 ipnet = null;
            IPAddress ip = null;
            bool parsed = IPNetwork2.TryParse(ip, ip, out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE3()
        {
            IPNetwork2 ipnet = null;
            bool parsed = IPNetwork2.TryParse(string.Empty, 0, out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE4()
        {
            IPNetwork2 ipnet = null;
            bool parsed = IPNetwork2.TryParse(null, 0, out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE5()
        {
            string n = null;
            IPNetwork2 ipnet = null;
            bool parsed = IPNetwork2.TryParse(n, n, out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE6()
        {
            IPNetwork2 ipnet = null;
            bool parsed = IPNetwork2.TryParse(IPAddress.Parse("10.10.10.10"), null, out ipnet);
            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE7()
        {
            IPNetwork2 ipnet = null;
            bool parsed = IPNetwork2.TryParse("0.0.0.0", null, out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE8()
        {
            IPNetwork2 ipnet = null;
            bool parsed = IPNetwork2.TryParse("x.x.x.x", "x.x.x.x", out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE9()
        {
            IPNetwork2 ipnet = null;
            bool parsed = IPNetwork2.TryParse("0.0.0.0", "x.x.x.x", out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE10()
        {
            IPNetwork2 ipnet = null;
            bool parsed = IPNetwork2.TryParse("x.x.x.x", 0, out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE11()
        {
            IPNetwork2 ipnet = null;
            bool parsed = IPNetwork2.TryParse("0.0.0.0", 33, out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmask()
        {
            IPNetwork2 ipnetwork = null;
            string ipaddress = "192.168.168.100";
            string netmask = "255.255.255.0";

            string network = "192.168.168.0";
            string broadcast = "192.168.168.255";
            string firstUsable = "192.168.168.1";
            string lastUsable = "192.168.168.254";
            byte cidr = 24;
            uint usable = 254;

            bool parsed = IPNetwork2.TryParse(ipaddress, netmask, out ipnetwork);
            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        [TestMethod]
        public void TestTryParseString1()
        {
            IPNetwork2 ipnetwork = null;
            string ipaddress = "192.168.168.100 255.255.255.0";

            string network = "192.168.168.0";
            string netmask = "255.255.255.0";
            string broadcast = "192.168.168.255";
            string firstUsable = "192.168.168.1";
            string lastUsable = "192.168.168.254";
            byte cidr = 24;
            uint usable = 254;

            bool parsed = IPNetwork2.TryParse(ipaddress, out ipnetwork);
            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        [TestMethod]
        public void TestTryParseString2()
        {
            IPNetwork2 ipnetwork = null;
            string ipaddress = "192.168.168.100/24";

            string network = "192.168.168.0";
            string netmask = "255.255.255.0";
            string broadcast = "192.168.168.255";
            string firstUsable = "192.168.168.1";
            string lastUsable = "192.168.168.254";
            byte cidr = 24;
            uint usable = 254;

            bool parsed = IPNetwork2.TryParse(ipaddress, out ipnetwork);
            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        [TestMethod]
        public void TestTryParseString3()
        {
            IPNetwork2 ipnetwork = null;
            string ipaddress = "0.0.0.0/0";

            string network = "0.0.0.0";
            string netmask = "0.0.0.0";
            string broadcast = "255.255.255.255";
            string firstUsable = "0.0.0.1";
            string lastUsable = "255.255.255.254";
            byte cidr = 0;
            uint usable = 4294967294;

            bool parsed = IPNetwork2.TryParse(ipaddress, out ipnetwork);
            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        [TestMethod]
        public void TestTryParseString4()
        {
            IPNetwork2 ipnetwork = null;
            string ipaddress = "0.0.0.0/32";

            string network = "0.0.0.0";
            string netmask = "255.255.255.255";
            string broadcast = "0.0.0.0";
            string firstUsable = "0.0.0.0";
            string lastUsable = "0.0.0.0";
            byte cidr = 32;
            uint usable = 0;

            bool parsed = IPNetwork2.TryParse(ipaddress, out ipnetwork);
            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        [TestMethod]
        public void TestTryParseString5()
        {
            IPNetwork2 ipnetwork = null;
            string ipaddress = "255.255.255.255/32";

            string network = "255.255.255.255";
            string netmask = "255.255.255.255";
            string broadcast = "255.255.255.255";
            string firstUsable = "255.255.255.255";
            string lastUsable = "255.255.255.255";
            byte cidr = 32;
            uint usable = 0;

            bool parsed = IPNetwork2.TryParse(ipaddress, out ipnetwork);
            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
            Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
            Assert.AreEqual(broadcast, ipnetwork.Broadcast.ToString(), "Broadcast");
            Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
            Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
            Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
            Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
        }

        [TestMethod]
        public void TestTryParseStringAE1()
        {
            string ipaddress = "garbage";
            IPNetwork2 ipnetwork = null;
            bool parsed = IPNetwork2.TryParse(ipaddress, out ipnetwork);
            Assert.AreEqual(false, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryParseStringAE2()
        {
            string ipaddress = "0.0.0.0 0.0.1.0";
            IPNetwork2 ipnetwork = null;
            bool parsed = IPNetwork2.TryParse(ipaddress, out ipnetwork);
            Assert.AreEqual(false, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryParseStringANE1()
        {
            string ipaddress = null;
            IPNetwork2 ipnetwork = null;
            bool parsed = IPNetwork2.TryParse(ipaddress, out ipnetwork);
            Assert.AreEqual(false, parsed, "parsed");
        }

        #endregion

        #region ParseStringString

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

        #endregion

        #region ParseIpIp

        [TestMethod]
        public void ParseIpIp1()
        {
            string ipaddress = "192.168.168.100";
            string netmask = "255.255.255.0";
            var ip = IPAddress.Parse(ipaddress);
            var netm = IPAddress.Parse(netmask);
            var ipnetwork = IPNetwork2.Parse(ip, netm);
            Assert.AreEqual("192.168.168.0/24", ipnetwork.ToString(), "network");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseIpIp2()
        {
            IPAddress ip = null;
            IPAddress netm = null;
            var ipnetwork = IPNetwork2.Parse(ip, netm);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseIpIp3()
        {
            string ipaddress = "192.168.168.100";
            var ip = IPAddress.Parse(ipaddress);
            IPAddress netm = null;
            var ipnetwork = IPNetwork2.Parse(ip, netm);
        }

        #endregion

        #region CtorWithIpAndCidr

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

        #endregion

        #region ToCidr

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestToCidrANE()
        {
            IPNetwork2.ToCidr(null);
        }

        [TestMethod]
        public void TestToCidrAE()
        {
            byte cidr = IPNetwork2.ToCidr(IPAddress.IPv6Any);
            Assert.AreEqual(0, cidr, "cidr");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestToCidrAE2()
        {
            IPNetwork2.ToCidr(IPAddress.Parse("6.6.6.6"));
        }

        [TestMethod]
        public void TestToCidr32()
        {
            var mask = IPAddress.Parse("255.255.255.255");
            byte cidr = 32;
            int result = IPNetwork2.ToCidr(mask);

            Assert.AreEqual(cidr, result, "cidr");
        }

        [TestMethod]
        public void TestToCidr24()
        {
            var mask = IPAddress.Parse("255.255.255.0");
            byte cidr = 24;
            int result = IPNetwork2.ToCidr(mask);

            Assert.AreEqual(cidr, result, "cidr");
        }

        [TestMethod]
        public void TestToCidr16()
        {
            var mask = IPAddress.Parse("255.255.0.0");
            byte cidr = 16;
            int result = IPNetwork2.ToCidr(mask);

            Assert.AreEqual(cidr, result, "cidr");
        }

        [TestMethod]
        public void TestToCidr8()
        {
            var mask = IPAddress.Parse("255.0.0.0");
            byte cidr = 8;
            int result = IPNetwork2.ToCidr(mask);

            Assert.AreEqual(cidr, result, "cidr");
        }

        [TestMethod]
        public void TestToCidr0()
        {
            var mask = IPAddress.Parse("0.0.0.0");
            byte cidr = 0;
            int result = IPNetwork2.ToCidr(mask);

            Assert.AreEqual(cidr, result, "cidr");
        }

        #endregion

        #region TryToCidr

        [TestMethod]
        public void TestTryToCidrANE()
        {
            byte? cidr = null;
            bool parsed = IPNetwork2.TryToCidr(null, out cidr);
            Assert.AreEqual(false, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryToCidrAE()
        {
            byte? cidr = null;
            bool parsed = IPNetwork2.TryToCidr(IPAddress.IPv6Any, out cidr);
            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual((byte)0, cidr, "cidr");
        }

        [TestMethod]
        public void TestTryToCidrAE2()
        {
            byte? cidr = null;
            bool parsed = IPNetwork2.TryToCidr(IPAddress.Parse("6.6.6.6"), out cidr);
            Assert.AreEqual(false, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryToCidr32()
        {
            byte? cidr = null;
            var mask = IPAddress.Parse("255.255.255.255");
            byte result = 32;
            bool parsed = IPNetwork2.TryToCidr(mask, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(cidr, result, "cidr");
        }

        [TestMethod]
        public void TestTryToCidr24()
        {
            byte? cidr = null;
            var mask = IPAddress.Parse("255.255.255.0");
            byte result = 24;
            bool parsed = IPNetwork2.TryToCidr(mask, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(cidr, result, "cidr");
        }

        [TestMethod]
        public void TestTryToCidr16()
        {
            byte? cidr = null;
            var mask = IPAddress.Parse("255.255.0.0");
            byte result = 16;
            bool parsed = IPNetwork2.TryToCidr(mask, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(cidr, result, "cidr");
        }

        [TestMethod]
        public void TestTryToCidr8()
        {
            byte? cidr = null;
            var mask = IPAddress.Parse("255.0.0.0");
            byte result = 8;
            bool parsed = IPNetwork2.TryToCidr(mask, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(cidr, result, "cidr");
        }

        [TestMethod]
        public void TestTryToCidr0()
        {
            byte? cidr = null;
            var mask = IPAddress.Parse("0.0.0.0");
            byte result = 0;
            bool parsed = IPNetwork2.TryToCidr(mask, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(cidr, result, "cidr");
        }

        #endregion

        #region ToBigInteger

        [TestMethod]
        public void TestToBigInteger32()
        {
            var mask = IPAddress.Parse("255.255.255.255");
            uint uintMask = 0xffffffff;
            var result = IPNetwork2.ToBigInteger(mask);

            Assert.AreEqual(uintMask, result, "uint");
        }

        [TestMethod]
        public void TestToBigInteger24()
        {
            var mask = IPAddress.Parse("255.255.255.0");
            uint uintMask = 0xffffff00;
            BigInteger? result = IPNetwork2.ToBigInteger(mask);

            Assert.AreEqual(uintMask, result, "uint");
        }

        [TestMethod]
        public void TestToBigInteger16()
        {
            var mask = IPAddress.Parse("255.255.0.0");
            uint uintMask = 0xffff0000;
            BigInteger? result = IPNetwork2.ToBigInteger(mask);

            Assert.AreEqual(uintMask, result, "uint");
        }

        [TestMethod]
        public void TestToBigInteger8()
        {
            var mask = IPAddress.Parse("255.0.0.0");
            uint uintMask = 0xff000000;
            BigInteger? result = IPNetwork2.ToBigInteger(mask);

            Assert.AreEqual(uintMask, result, "uint");
        }

        [TestMethod]
        public void TestToBigInteger0()
        {
            var mask = IPAddress.Parse("0.0.0.0");
            uint uintMask = 0x00000000;
            BigInteger? result = IPNetwork2.ToBigInteger(mask);

            Assert.AreEqual(uintMask, result, "uint");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestToBigIntegerANE()
        {
            BigInteger? result = IPNetwork2.ToBigInteger(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestToBigIntegerANE3()
        {
            IPAddress ip = null;
            BigInteger? result = IPNetwork2.ToBigInteger(ip);
        }

        [TestMethod]
        public void TestToBigIntegerANE2()
        {
            BigInteger? result = IPNetwork2.ToBigInteger(IPAddress.IPv6Any);
            uint expected = 0;
            Assert.AreEqual(expected, result, "result");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestToBigIntegerByte()
        {
            BigInteger result = IPNetwork2.ToUint(33, Sockets.AddressFamily.InterNetwork);
        }

        [TestMethod]
        public void TestToBigIntegerByte2()
        {
            BigInteger result = IPNetwork2.ToUint(32, Sockets.AddressFamily.InterNetwork);
            uint expected = 4294967295;
            Assert.AreEqual(expected, result, "result");
        }

        [TestMethod]
        public void TestToBigIntegerByte3()
        {
            BigInteger result = IPNetwork2.ToUint(0, Sockets.AddressFamily.InterNetwork);
            uint expected = 0;
            Assert.AreEqual(expected, result, "result");
        }

        [TestMethod]
        public void TestToBigIntegerInternal1()
        {
            BigInteger? result = null;
            IPNetwork2.InternalToBigInteger(true, 33, Sockets.AddressFamily.InterNetwork, out result);
            Assert.AreEqual(null, result, "result");
        }

        [TestMethod]
        public void TestToBigIntegerInternal2()
        {
            BigInteger? result = null;
            IPNetwork2.InternalToBigInteger(true, 129, Sockets.AddressFamily.InterNetworkV6, out result);
            Assert.AreEqual(null, result, "result");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestToBigIntegerInternal3()
        {
            BigInteger? result = null;
            IPNetwork2.InternalToBigInteger(false, 129, Sockets.AddressFamily.InterNetworkV6, out result);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestToBigIntegerInternal4()
        {
            BigInteger? result = null;
            IPNetwork2.InternalToBigInteger(false, 32, Sockets.AddressFamily.AppleTalk, out result);
        }

        [TestMethod]
        public void TestToBigIntegerInternal5()
        {
            BigInteger? result = null;
            IPNetwork2.InternalToBigInteger(true, 32, Sockets.AddressFamily.AppleTalk, out result);
            Assert.AreEqual(null, result, "result");
        }

        #endregion

        #region TryToUint

        [TestMethod]
        public void TestTryToUint1()
        {
            BigInteger? result = null;
            bool parsed = IPNetwork2.TryToUint(32, Sockets.AddressFamily.InterNetwork, out result);

            Assert.IsNotNull(result, "uint");
            Assert.AreEqual(true, parsed, "parsed");
        }

        #endregion

        #region TryToBigInteger

        [TestMethod]
        public void TestTryToBigInteger32()
        {
            var mask = IPAddress.Parse("255.255.255.255");
            uint uintMask = 0xffffffff;
            BigInteger? result = null;
            bool parsed = IPNetwork2.TryToBigInteger(mask, out result);

            Assert.AreEqual(uintMask, result, "uint");
            Assert.AreEqual(true, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryToBigInteger24()
        {
            var mask = IPAddress.Parse("255.255.255.0");
            uint uintMask = 0xffffff00;
            BigInteger? result = null;
            bool parsed = IPNetwork2.TryToBigInteger(mask, out result);

            Assert.AreEqual(uintMask, result, "uint");
            Assert.AreEqual(true, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryToBigInteger16()
        {
            var mask = IPAddress.Parse("255.255.0.0");
            uint uintMask = 0xffff0000;
            BigInteger? result = null;
            bool parsed = IPNetwork2.TryToBigInteger(mask, out result);

            Assert.AreEqual(uintMask, result, "uint");
            Assert.AreEqual(true, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryToBigInteger8()
        {
            var mask = IPAddress.Parse("255.0.0.0");
            uint uintMask = 0xff000000;

            BigInteger? result = null;
            bool parsed = IPNetwork2.TryToBigInteger(mask, out result);

            Assert.AreEqual(uintMask, result, "uint");
            Assert.AreEqual(true, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryToBigInteger0()
        {
            var mask = IPAddress.Parse("0.0.0.0");
            uint uintMask = 0x00000000;
            BigInteger? result = null;
            bool parsed = IPNetwork2.TryToBigInteger(mask, out result);

            Assert.AreEqual(uintMask, result, "uint");
            Assert.AreEqual(true, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryToBigIntegerANE()
        {
            BigInteger? result = null;
            bool parsed = IPNetwork2.TryToBigInteger(null, out result);

            Assert.AreEqual(null, result, "uint");
            Assert.AreEqual(false, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryToBigIntegerANE3()
        {
            IPAddress ip = null;
            BigInteger? result = null;
            bool parsed = IPNetwork2.TryToBigInteger(ip, out result);

            Assert.AreEqual(null, result, "uint");
            Assert.AreEqual(false, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryToBigIntegerANE2()
        {
            BigInteger? result = null;
            bool parsed = IPNetwork2.TryToBigInteger(IPAddress.IPv6Any, out result);

            Assert.AreEqual(0, result, "result");
            Assert.AreEqual(true, parsed, "parsed");
        }

        #endregion

        #region TryToNetmask
        [TestMethod]
        public void TryToNetmask1()
        {
            IPAddress result = null;
            bool parsed = IPNetwork2.TryToNetmask(0, Sockets.AddressFamily.InterNetwork, out result);
            var expected = IPAddress.Parse("0.0.0.0");

            Assert.AreEqual(expected, result, "Netmask");
            Assert.AreEqual(true, parsed, "parsed");
        }

        [TestMethod]
        public void TryToNetmask2()
        {
            IPAddress result = null;
            bool parsed = IPNetwork2.TryToNetmask(33, Sockets.AddressFamily.InterNetwork, out result);
            IPAddress expected = null;

            Assert.AreEqual(expected, result, "Netmask");
            Assert.AreEqual(false, parsed, "parsed");
        }

        #endregion

        #region ToNetmask

        [TestMethod]
        public void ToNetmask32()
        {
            byte cidr = 32;
            string netmask = "255.255.255.255";
            string result = IPNetwork2.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToNetmaskNonInet()
        {
            byte cidr = 0;
            string result = IPNetwork2.ToNetmask(cidr, Sockets.AddressFamily.AppleTalk).ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToNetmaskNegative()
        {
            byte cidr = 0;
            cidr--;
            string result = IPNetwork2.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();
        }

        [TestMethod]
        public void ToNetmaskInternal1()
        {
            IPAddress result;
            IPNetwork2.InternalToNetmask(true, 0, Sockets.AddressFamily.AppleTalk, out result);
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void ToNetmask31()
        {
            byte cidr = 31;
            string netmask = "255.255.255.254";
            string result = IPNetwork2.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        [TestMethod]
        public void ToNetmask30()
        {
            byte cidr = 30;
            string netmask = "255.255.255.252";
            string result = IPNetwork2.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        [TestMethod]
        public void ToNetmask29()
        {
            byte cidr = 29;
            string netmask = "255.255.255.248";
            string result = IPNetwork2.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        [TestMethod]
        public void ToNetmask1()
        {
            byte cidr = 1;
            string netmask = "128.0.0.0";
            string result = IPNetwork2.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        [TestMethod]
        public void ToNetmask0()
        {
            byte cidr = 0;
            string netmask = "0.0.0.0";
            string result = IPNetwork2.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToNetmaskOORE1()
        {
            byte cidr = 33;
            string result = IPNetwork2.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();
        }

        #endregion

        #region ToIPAddress

        [TestMethod]
        public void TestToIPAddress()
        {
            var ip = new BigInteger(0);
            var result = IPNetwork2.ToIPAddress(ip, Sockets.AddressFamily.InterNetwork);
            Assert.AreEqual(IPAddress.Any, result, "ToIPAddress");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestToIPAddress2()
        {
            var ip = new BigInteger(0);
            var result = IPNetwork2.ToIPAddress(ip, Sockets.AddressFamily.AppleTalk);
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
            var result = IPNetwork2.ToIPAddress(ip, Sockets.AddressFamily.AppleTalk);
        }
        #endregion

        #region ValidNetmask

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestValidNetmaskInvalid1()
        {
            bool resut = IPNetwork2.InternalValidNetmask(BigInteger.Zero, Sockets.AddressFamily.AppleTalk);
        }

        [TestMethod]
        public void TestValidNetmask0()
        {
            var mask = IPAddress.Parse("255.255.255.255");
            bool expected = true;
            bool result = IPNetwork2.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

        [TestMethod]
        public void TestValidNetmask1()
        {
            var mask = IPAddress.Parse("255.255.255.0");
            bool expected = true;
            bool result = IPNetwork2.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

        [TestMethod]
        public void TestValidNetmask2()
        {
            var mask = IPAddress.Parse("255.255.0.0");
            bool expected = true;
            bool result = IPNetwork2.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

        [TestMethod]
        public void TestValidNetmaskEAE1()
        {
            var mask = IPAddress.Parse("0.255.0.0");
            bool expected = false;
            bool result = IPNetwork2.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestValidNetmaskEAE2()
        {
            IPAddress mask = null;
            bool expected = true;
            bool result = IPNetwork2.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

        [TestMethod]
        public void TestValidNetmaskEAE3()
        {
            var mask = IPAddress.Parse("255.255.0.1");
            bool expected = false;
            bool result = IPNetwork2.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

        #endregion

        #region BitsSet

        [TestMethod]
        public void TestBitsSet32()
        {
            var ip = IPAddress.Parse("255.255.255.255");
            uint bits = 32;
            uint result = IPNetwork2.BitsSet(ip);

            Assert.AreEqual(bits, result, "BitsSet");
        }

        [TestMethod]
        public void TestBitsSet24()
        {
            var ip = IPAddress.Parse("255.255.255.0");
            uint bits = 24;
            uint result = IPNetwork2.BitsSet(ip);

            Assert.AreEqual(bits, result, "BitsSet");
        }

        [TestMethod]
        public void TestBitsSet16()
        {
            var ip = IPAddress.Parse("255.255.0.0");
            uint bits = 16;
            uint result = IPNetwork2.BitsSet(ip);

            Assert.AreEqual(bits, result, "BitsSet");
        }

        [TestMethod]
        public void TestBitsSet4()
        {
            var ip = IPAddress.Parse("128.128.128.128");
            uint bits = 4;
            uint result = IPNetwork2.BitsSet(ip);

            Assert.AreEqual(bits, result, "BitsSet");
        }

        #endregion

        #region Overlap

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

        [TestMethod]
        public void TestOverlap3()
        {
            var network1 = IPNetwork2.Parse("10.0.0.0/0");
            var network2 = IPNetwork2.Parse("10.0.0.0/0");
            bool result = network1.Overlap(network2);
            bool expected = true;

            Assert.AreEqual(expected, result, "overlap");
        }

        [TestMethod]
        public void TestOverlap4()
        {
            var network1 = IPNetwork2.Parse("10.10.0.0/16");
            var network2 = IPNetwork2.Parse("10.10.1.0/24");
            bool result = network1.Overlap(network2);
            bool expected = true;

            Assert.AreEqual(expected, result, "overlap");
        }

        [TestMethod]
        public void TestOverlap5()
        {
            var network1 = IPNetwork2.Parse("10.10.0.0/24");
            var network2 = IPNetwork2.Parse("10.10.1.0/24");
            bool result = network1.Overlap(network2);
            bool expected = false;

            Assert.AreEqual(expected, result, "overlap");
        }

        [TestMethod]
        public void TestOverlap6()
        {
            var network1 = IPNetwork2.Parse("10.10.1.0/24");
            var network2 = IPNetwork2.Parse("10.10.0.0/16");
            bool result = network1.Overlap(network2);
            bool expected = true;

            Assert.AreEqual(expected, result, "overlap");
        }

        #endregion

        #region Examples

        [TestMethod]
        public void Example1()
        {
            var ipnetwork = IPNetwork2.Parse("192.168.168.100/24");

            Console.WriteLine("Network : {0}", ipnetwork.Network);
            Console.WriteLine("Netmask : {0}", ipnetwork.Netmask);
            Console.WriteLine("Broadcast : {0}", ipnetwork.Broadcast);
            Console.WriteLine("FirstUsable : {0}", ipnetwork.FirstUsable);
            Console.WriteLine("LastUsable : {0}", ipnetwork.LastUsable);
            Console.WriteLine("Usable : {0}", ipnetwork.Usable);
            Console.WriteLine("Cidr : {0}", ipnetwork.Cidr);
        }

        [TestMethod]
        public void Example2()
        {
            var ipnetwork = IPNetwork2.Parse("192.168.0.0/24");
            var ipaddress = IPAddress.Parse("192.168.0.100");
            var ipaddress2 = IPAddress.Parse("192.168.1.100");

            var ipnetwork2 = IPNetwork2.Parse("192.168.0.128/25");
            var ipnetwork3 = IPNetwork2.Parse("192.168.1.1/24");

            bool contains1 = ipnetwork.Contains(ipaddress);
            bool contains2 = ipnetwork.Contains(ipaddress2);
            bool contains3 = ipnetwork.Contains(ipnetwork2);
            bool contains4 = ipnetwork.Contains(ipnetwork3);

            bool overlap1 = ipnetwork.Overlap(ipnetwork2);
            bool overlap2 = ipnetwork.Overlap(ipnetwork3);

            Console.WriteLine("{0} contains {1} : {2}", ipnetwork, ipaddress, contains1);
            Console.WriteLine("{0} contains {1} : {2}", ipnetwork, ipaddress2, contains2);
            Console.WriteLine("{0} contains {1} : {2}", ipnetwork, ipnetwork2, contains3);
            Console.WriteLine("{0} contains {1} : {2}", ipnetwork, ipnetwork3, contains4);

            Console.WriteLine("{0} overlap {1} : {2}", ipnetwork, ipnetwork2, overlap1);
            Console.WriteLine("{0} overlap {1} : {2}", ipnetwork, ipnetwork3, overlap2);
        }

        [TestMethod]
        public void Example2b()
        {
            var ipnetwork1 = IPNetwork2.Parse("10.1.0.0/16");
            var ipnetwork2 = IPNetwork2.Parse("192.168.1.0/24");

            var ipaddress1 = IPAddress.Parse("192.168.1.1");
            var ipaddress2 = IPAddress.Parse("192.168.2.100");
            var ipaddress3 = IPAddress.Parse("10.1.2.3");
            var ipaddress4 = IPAddress.Parse("10.4.5.6");

            bool contains1 = ipnetwork2.Contains(ipaddress1);
            bool contains2 = ipnetwork2.Contains(ipaddress2);
            bool contains3 = ipnetwork1.Contains(ipaddress3);
            bool contains4 = ipnetwork1.Contains(ipaddress4);

            Console.WriteLine("{0} contains {1} : {2}", ipnetwork1, ipaddress1, contains1);
            Console.WriteLine("{0} contains {1} : {2}", ipnetwork1, ipaddress2, contains2);
            Console.WriteLine("{0} contains {1} : {2}", ipnetwork2, ipaddress3, contains3);
            Console.WriteLine("{0} contains {1} : {2}", ipnetwork2, ipaddress4, contains4);
        }

        [TestMethod]
        public void Example3()
        {
            IPNetwork2 iana_a_block = IPNetwork2.IANA_ABLK_RESERVED1;
            IPNetwork2 iana_b_block = IPNetwork2.IANA_BBLK_RESERVED1;
            IPNetwork2 iana_c_block = IPNetwork2.IANA_CBLK_RESERVED1;

            Console.WriteLine("IANA_ABLK_RESERVED1 is {0}", iana_a_block);
            Console.WriteLine("IANA_BBLK_RESERVED1 is {0}", iana_b_block);
            Console.WriteLine("IANA_CBLK_RESERVED1 is {0}", iana_c_block);
        }

        [TestMethod]
        public void Example4()
        {
            var wholeInternet = IPNetwork2.Parse("0.0.0.0/0");
            byte newCidr = 2;
            IPNetworkCollection subneted = wholeInternet.Subnet(newCidr);

            Console.WriteLine("{0} was subnetted into {1} subnets", wholeInternet, subneted.Count);
            Console.WriteLine("First: {0}", subneted[0]);
            Console.WriteLine("Last : {0}", subneted[subneted.Count - 1]);
            Console.WriteLine("All  :");

            foreach (IPNetwork2 ipnetwork in subneted)
            {
                Console.WriteLine("{0}", ipnetwork);
            }
        }

        [TestMethod]
        public void Example5()
        {
            var ipnetwork1 = IPNetwork2.Parse("192.168.0.0/24");
            var ipnetwork2 = IPNetwork2.Parse("192.168.1.0/24");
            IPNetwork2[] ipnetwork3 = IPNetwork2.Supernet(new[] { ipnetwork1, ipnetwork2 });

            Console.WriteLine("{0} + {1} = {2}", ipnetwork1, ipnetwork2, ipnetwork3[0]);
        }

        [TestMethod]
        public void Example7()
        {
            var ipnetwork = IPNetwork2.Parse("192.168.168.100/24");

            var ipaddress = IPAddress.Parse("192.168.168.200");
            var ipaddress2 = IPAddress.Parse("192.168.0.200");

            bool contains1 = ipnetwork.Contains(ipaddress);
            bool contains2 = ipnetwork.Contains(ipaddress2);

            Console.WriteLine("{0} contains {1} : {2}", ipnetwork, ipaddress, contains1);
            Console.WriteLine("{0} contains {1} : {2}", ipnetwork, ipaddress2, contains2);
        }

        [TestMethod]
        public void Example9()
        {
            var network = IPNetwork2.Parse("192.168.0.1");
            var network2 = IPNetwork2.Parse("192.168.0.254");

            IPNetwork2 ipnetwork = network.Supernet(network2);

            Console.WriteLine("Network : {0}", ipnetwork.Network);
            Console.WriteLine("Netmask : {0}", ipnetwork.Netmask);
            Console.WriteLine("Broadcast : {0}", ipnetwork.Broadcast);
            Console.WriteLine("FirstUsable : {0}", ipnetwork.FirstUsable);
            Console.WriteLine("LastUsable : {0}", ipnetwork.LastUsable);
            Console.WriteLine("Usable : {0}", ipnetwork.Usable);
            Console.WriteLine("Cidr : {0}", ipnetwork.Cidr);
        }

        [TestMethod]
        public void When_TrySupernet_192_168_0_0_cidr24_add_192_168_10_0_cidr24_Then_Should_Invalid()
        {
            var network = IPNetwork2.Parse("192.168.0.0/24");
            var network2 = IPNetwork2.Parse("192.168.10.0/24");

            bool supernetted = network.TrySupernet(network2, out IPNetwork2 ipnetwork);
            Assert.AreEqual(false, supernetted);
        }

        [TestMethod]
        public void When_TryWideSubnet_192_168_0_0_cidr24_add_192_168_10_0_cidr24_Then_Should_Invalid()
        {
            var network = IPNetwork2.Parse("192.168.0.0/24");
            var network2 = IPNetwork2.Parse("192.168.10.0/24");

            bool wideSubnetted = IPNetwork2.TryWideSubnet(new[] { network, network2 }, out IPNetwork2 ipnetwork);
            Assert.AreEqual(true, wideSubnetted);
            Assert.AreEqual("192.168.0.0/20", ipnetwork.ToString());

            Console.WriteLine("Network : {0}", ipnetwork.Network);
            Console.WriteLine("Netmask : {0}", ipnetwork.Netmask);
            Console.WriteLine("Broadcast : {0}", ipnetwork.Broadcast);
            Console.WriteLine("FirstUsable : {0}", ipnetwork.FirstUsable);
            Console.WriteLine("LastUsable : {0}", ipnetwork.LastUsable);
            Console.WriteLine("Usable : {0}", ipnetwork.Usable);
            Console.WriteLine("Cidr : {0}", ipnetwork.Cidr);
        }

        [TestMethod]
        public void Example10()
        {
            var ipnetwork = IPNetwork2.Parse("192.168.0.1/25");

            Console.WriteLine("Network : {0}", ipnetwork.Network);
            Console.WriteLine("Netmask : {0}", ipnetwork.Netmask);
            Console.WriteLine("Broadcast : {0}", ipnetwork.Broadcast);
            Console.WriteLine("FirstUsable : {0}", ipnetwork.FirstUsable);
            Console.WriteLine("LastUsable : {0}", ipnetwork.LastUsable);
            Console.WriteLine("Usable : {0}", ipnetwork.Usable);
            Console.WriteLine("Cidr : {0}", ipnetwork.Cidr);
        }

        [TestMethod]
        public void Example11()
        {
            var defaultParse = IPNetwork2.Parse("192.168.0.0"); // default to ClassFull
            var classFullParse = IPNetwork2.Parse("192.168.0.0", CidrGuess.ClassFull);
            var classLessParse = IPNetwork2.Parse("192.168.0.0", CidrGuess.ClassLess);

            Console.WriteLine("IPV4 Default Parse : {0}", defaultParse);
            Console.WriteLine("IPV4 ClassFull Parse : {0}", classFullParse);
            Console.WriteLine("IPV4 ClassLess Parse : {0}", classLessParse);
        }

        #endregion

        #region IANA blocks
        [TestMethod]
        public void TestIANA1()
        {
            var ipaddress = IPAddress.Parse("192.168.66.66");
            bool expected = true;
            bool result = IPNetwork2.IsIANAReserved(ipaddress);

            Assert.AreEqual(expected, result, "IANA");
        }

        [TestMethod]
        public void TestIANA2()
        {
            var ipaddress = IPAddress.Parse("10.0.0.0");
            bool expected = true;
            bool result = IPNetwork2.IsIANAReserved(ipaddress);

            Assert.AreEqual(expected, result, "IANA");
        }

        [TestMethod]
        public void TestIANA3()
        {
            var ipaddress = IPAddress.Parse("172.17.10.10");
            bool expected = true;
            bool result = IPNetwork2.IsIANAReserved(ipaddress);

            Assert.AreEqual(expected, result, "IANA");
        }

        [TestMethod]
        public void TestIANA4()
        {
            var ipnetwork = IPNetwork2.Parse("192.168.66.66/24");
            bool expected = true;
            bool result = ipnetwork.IsIANAReserved();

            Assert.AreEqual(expected, result, "IANA");
        }

        [TestMethod]
        public void TestIANA5()
        {
            var ipnetwork = IPNetwork2.Parse("10.10.10/18");
            bool expected = true;
            bool result = ipnetwork.IsIANAReserved();

            Assert.AreEqual(expected, result, "IANA");
        }

        [TestMethod]
        public void TestIANA6()
        {
            var ipnetwork = IPNetwork2.Parse("172.31.10.10/24");
            bool expected = true;
            bool result = ipnetwork.IsIANAReserved();

            Assert.AreEqual(expected, result, "IANA");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIANA7()
        {
            IPAddress ipaddress = null;
            IPNetwork2.IsIANAReserved(ipaddress);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIANA8()
        {
            IPNetwork2 ipnetwork = null;
#pragma warning disable 0618
            bool result = IPNetwork2.IsIANAReserved(ipnetwork);
#pragma warning restore 0618
        }

        [TestMethod]
        public void TestIANABlk1()
        {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_ABLK_RESERVED1;
#pragma warning disable 0618
            bool result = IPNetwork2.IsIANAReserved(ipnetwork);
#pragma warning restore 0618
            Assert.IsTrue(result, "result");
        }

        [TestMethod]
        public void TestIANA9()
        {
            var ipaddress = IPAddress.Parse("1.2.3.4");
            bool expected = false;
            bool result = IPNetwork2.IsIANAReserved(ipaddress);

            Assert.AreEqual(expected, result, "IANA");
        }

        [TestMethod]
        public void TestIANA10()
        {
            var ipnetwork = IPNetwork2.Parse("172.16.0.0/8");
            bool expected = false;
            bool result = ipnetwork.IsIANAReserved();

            Assert.AreEqual(expected, result, "IANA");
        }

        [TestMethod]
        public void TestIANA11()
        {
            var ipnetwork = IPNetwork2.Parse("192.168.15.1/8");
            bool expected = false;
            bool result = ipnetwork.IsIANAReserved();

            Assert.AreEqual(expected, result, "IANA");
        }
        #endregion

        #region ToString

        [TestMethod]
        public void TestToString()
        {
            var ipnetwork = IPNetwork2.Parse("192.168.15.1/8");
            string expected = "192.0.0.0/8";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString1()
        {
            var ipnetwork = IPNetwork2.Parse("192.168.15.1/9");
            string expected = "192.128.0.0/9";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString2()
        {
            var ipnetwork = IPNetwork2.Parse("192.168.15.1/10");
            string expected = "192.128.0.0/10";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString3()
        {
            var ipnetwork = IPNetwork2.Parse("192.168.15.1/11");
            string expected = "192.160.0.0/11";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString4()
        {
            var ipnetwork = IPNetwork2.Parse("192.168.15.1/12");
            string expected = "192.160.0.0/12";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString5()
        {
            var ipnetwork = IPNetwork2.Parse("192.168.15.1/13");
            string expected = "192.168.0.0/13";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString6()
        {
            var ipnetwork = IPNetwork2.Parse("192.168.15.1/14");
            string expected = "192.168.0.0/14";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString7()
        {
            var ipnetwork = IPNetwork2.Parse("192.168.15.1/15");
            string expected = "192.168.0.0/15";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString8()
        {
            var ipnetwork = IPNetwork2.Parse("192.168.15.1/16");
            string expected = "192.168.0.0/16";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        #endregion

        #region Subnet

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSubnet1()
        {
            IPNetwork2 ipnetwork = null;
            byte cidr = 9;

#pragma warning disable 0618
            IPNetworkCollection result = IPNetwork2.Subnet(ipnetwork, cidr);
#pragma warning restore 0618
        }

        [TestMethod]
        public void TestSubnetStatic1()
        {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_ABLK_RESERVED1;
            byte cidr = 9;

#pragma warning disable 0618
            IPNetworkCollection result = IPNetwork2.Subnet(ipnetwork, cidr);
#pragma warning restore 0618
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestSubnet3()
        {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_ABLK_RESERVED1;
            byte cidr = 55;

            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSubnet4()
        {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_ABLK_RESERVED1;
            byte cidr = 1;

            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
        }

        [TestMethod]
        public void TestSubnet5()
        {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_ABLK_RESERVED1;
            byte cidr = 9;

            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            Assert.AreEqual(2, subnets.Count, "count");
            Assert.AreEqual("10.0.0.0/9", subnets[0].ToString(), "subnet1");
            Assert.AreEqual("10.128.0.0/9", subnets[1].ToString(), "subnet2");
        }

        [TestMethod]
        public void TestSubnet6()
        {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_CBLK_RESERVED1;
            byte cidr = 20;

            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            Assert.AreEqual(16, subnets.Count, "count");
            Assert.AreEqual("192.168.0.0/20", subnets[0].ToString(), "subnet1");
            Assert.AreEqual("192.168.16.0/20", subnets[1].ToString(), "subnet2");
            Assert.AreEqual("192.168.32.0/20", subnets[2].ToString(), "subnet3");
            Assert.AreEqual("192.168.48.0/20", subnets[3].ToString(), "subnet4");
            Assert.AreEqual("192.168.64.0/20", subnets[4].ToString(), "subnet5");
            Assert.AreEqual("192.168.80.0/20", subnets[5].ToString(), "subnet6");
            Assert.AreEqual("192.168.96.0/20", subnets[6].ToString(), "subnet7");
            Assert.AreEqual("192.168.112.0/20", subnets[7].ToString(), "subnet8");
            Assert.AreEqual("192.168.128.0/20", subnets[8].ToString(), "subnet9");
            Assert.AreEqual("192.168.144.0/20", subnets[9].ToString(), "subnet10");
            Assert.AreEqual("192.168.160.0/20", subnets[10].ToString(), "subnet11");
            Assert.AreEqual("192.168.176.0/20", subnets[11].ToString(), "subnet12");
            Assert.AreEqual("192.168.192.0/20", subnets[12].ToString(), "subnet13");
            Assert.AreEqual("192.168.208.0/20", subnets[13].ToString(), "subnet14");
            Assert.AreEqual("192.168.224.0/20", subnets[14].ToString(), "subnet15");
            Assert.AreEqual("192.168.240.0/20", subnets[15].ToString(), "subnet16");
        }

        [TestMethod]
        public void TestSubnet7()
        {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_CBLK_RESERVED1;
            byte cidr = 24;

            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            Assert.AreEqual(256, subnets.Count, "count");
            Assert.AreEqual("192.168.0.0/24", subnets[0].ToString(), "subnet1");
            Assert.AreEqual("192.168.255.0/24", subnets[255].ToString(), "subnet16");
        }

        [TestMethod]
        public void TestSubnet8()
        {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_CBLK_RESERVED1;
            byte cidr = 24;

            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            Assert.AreEqual(256, subnets.Count, "count");
            Assert.AreEqual("192.168.0.0/24", subnets[0].ToString(), "subnet1");
            Assert.AreEqual("192.168.255.0/24", subnets[255].ToString(), "subnet256");
        }

        [TestMethod]
        public void TestSubnet9()
        {
            var ipnetwork = IPNetwork2.Parse("192.168.0.0/24");
            byte cidr = 32;

            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            Assert.AreEqual(256, subnets.Count, "count");
            Assert.AreEqual("192.168.0.0/32", subnets[0].ToString(), "subnet1");
            Assert.AreEqual("192.168.0.255/32", subnets[255].ToString(), "subnet256");
        }

        [TestMethod]
        public void TestSubnet_Example8()
        {
            var wholeInternet = IPNetwork2.Parse("0.0.0.0/0");
            byte newCidr = 2;
            IPNetworkCollection subneted = wholeInternet.Subnet(newCidr);

            Console.WriteLine("{0} was subnetted into {1} subnets", wholeInternet, subneted.Count);
            Console.WriteLine("First: {0}", subneted[0]);
            Console.WriteLine("Last : {0}", subneted[subneted.Count - 1]);
            Console.WriteLine("All  :");

            foreach (IPNetwork2 ipnetwork in subneted)
            {
                Console.WriteLine("{0}", ipnetwork);
            }
        }

        [TestMethod]
        public void TestSubnet10()
        {
            var ipnetwork = IPNetwork2.Parse("0.0.0.0/0");
            byte cidr = 32;

            // Here I spawm a OOM dragon ! beware of the beast !
            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            Assert.AreEqual(4294967296, subnets.Count, "count");
            Assert.AreEqual("0.0.0.0/32", subnets[0].ToString(), "subnet1");
            Assert.AreEqual("255.255.255.255/32", subnets[4294967295].ToString(), "subnet256");
        }

        [TestMethod]
        public void TestSubnet12()
        {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_CBLK_RESERVED1;
            byte cidr = 20;
            int i = -1;
            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            foreach (IPNetwork2 ipn in subnets)
            {
                i++;
                Assert.AreEqual(subnets[i], ipn, "subnet");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestSubnet13()
        {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_CBLK_RESERVED1;
            byte cidr = 20;
            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            IPNetwork2 error = subnets[1000];
        }

        #endregion

        #region TrySubnet

        [TestMethod]
        public void TestInternalSubnet1()
        {
            IPNetworkCollection subnets = null;
            IPNetwork2.InternalSubnet(true, null, 0, out subnets);
            Assert.AreEqual(null, subnets, "subnets");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInternalSubnet2()
        {
            IPNetworkCollection subnets = null;
            IPNetwork2.InternalSubnet(false, null, 0, out subnets);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestTrySubnet1()
        {
            IPNetwork2 ipnetwork = null;
            byte cidr = 9;

            IPNetworkCollection subnets = null;
#pragma warning disable 0618
            bool subnetted = IPNetwork2.TrySubnet(ipnetwork, cidr, out subnets);
#pragma warning restore 0618
        }

        [TestMethod]
        public void TestTrySubnetStatic1()
        {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_ABLK_RESERVED1;
            byte cidr = 9;

            IPNetworkCollection subnets = null;
#pragma warning disable 0618
            bool subnetted = IPNetwork2.TrySubnet(ipnetwork, cidr, out subnets);
#pragma warning restore 0618
        }

        [TestMethod]
        public void TestTrySubnet3()
        {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_ABLK_RESERVED1;
            byte cidr = 55;

            IPNetworkCollection subnets = null;
            bool subnetted = ipnetwork.TrySubnet(cidr, out subnets);

            Assert.AreEqual(false, subnetted, "subnetted");
        }

        [TestMethod]
        public void TestTrySubnet4()
        {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_ABLK_RESERVED1;
            byte cidr = 1;

            IPNetworkCollection subnets = null;
            bool subnetted = ipnetwork.TrySubnet(cidr, out subnets);

            Assert.AreEqual(false, subnetted, "subnetted");
        }

        [TestMethod]
        public void TestTrySubnet5()
        {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_ABLK_RESERVED1;
            byte cidr = 9;

            IPNetworkCollection subnets = null;
            bool subnetted = ipnetwork.TrySubnet(cidr, out subnets);

            Assert.AreEqual(true, subnetted, "subnetted");
            Assert.AreEqual(2, subnets.Count, "count");
            Assert.AreEqual("10.0.0.0/9", subnets[0].ToString(), "subnet1");
            Assert.AreEqual("10.128.0.0/9", subnets[1].ToString(), "subnet2");
        }

        [TestMethod]
        public void TestTrySubnet6()
        {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_CBLK_RESERVED1;
            byte cidr = 20;

            IPNetworkCollection subnets = null;
            bool subnetted = ipnetwork.TrySubnet(cidr, out subnets);

            Assert.AreEqual(true, subnetted, "subnetted");
            Assert.AreEqual(16, subnets.Count, "count");
            Assert.AreEqual("192.168.0.0/20", subnets[0].ToString(), "subnet1");
            Assert.AreEqual("192.168.16.0/20", subnets[1].ToString(), "subnet2");
            Assert.AreEqual("192.168.32.0/20", subnets[2].ToString(), "subnet3");
            Assert.AreEqual("192.168.48.0/20", subnets[3].ToString(), "subnet4");
            Assert.AreEqual("192.168.64.0/20", subnets[4].ToString(), "subnet5");
            Assert.AreEqual("192.168.80.0/20", subnets[5].ToString(), "subnet6");
            Assert.AreEqual("192.168.96.0/20", subnets[6].ToString(), "subnet7");
            Assert.AreEqual("192.168.112.0/20", subnets[7].ToString(), "subnet8");
            Assert.AreEqual("192.168.128.0/20", subnets[8].ToString(), "subnet9");
            Assert.AreEqual("192.168.144.0/20", subnets[9].ToString(), "subnet10");
            Assert.AreEqual("192.168.160.0/20", subnets[10].ToString(), "subnet11");
            Assert.AreEqual("192.168.176.0/20", subnets[11].ToString(), "subnet12");
            Assert.AreEqual("192.168.192.0/20", subnets[12].ToString(), "subnet13");
            Assert.AreEqual("192.168.208.0/20", subnets[13].ToString(), "subnet14");
            Assert.AreEqual("192.168.224.0/20", subnets[14].ToString(), "subnet15");
            Assert.AreEqual("192.168.240.0/20", subnets[15].ToString(), "subnet16");
        }

        #endregion

        #region Supernet

        [TestMethod]
        public void TestSupernetInternal1()
        {
            IPNetwork2 result;
            IPNetwork2.InternalSupernet(true, null, null, out result);

            Assert.AreEqual(null, result, "supernet");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSupernetInternal2()
        {
            IPNetwork2 result;
            IPNetwork2.InternalSupernet(false, null, null, out result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]

        public void Issue33__TestSupernet__Bug_or_default_behavior()
        {
            var network1 = IPNetwork2.Parse("192.168.0.0/24");
            var network2 = IPNetwork2.Parse("192.168.2.0/24");
            var expected = IPNetwork2.Parse("192.168.0.0/23");
            IPNetwork2 supernet = network1.Supernet(network2);

            Assert.AreEqual(expected, supernet, "supernet");
        }

        [TestMethod]
        public void Issue33__TestWideSubnet__Bug_or_default_behavior()
        {
            var network1 = IPNetwork2.Parse("192.168.0.0/24");
            var network2 = IPNetwork2.Parse("192.168.2.0/24");
            var expected = IPNetwork2.Parse("192.168.0.0/22");
            var widenetwork = IPNetwork2.WideSubnet(new[] { network1, network2 });

            Assert.AreEqual(expected, widenetwork, "widesubnet");
        }

        [TestMethod]
        public void Issue162__Test_IPrangeToCIDRnotation()
        {
            string network1 = "172.64.0.0";
            string network2 = "172.71.255.255";

            var final = IPNetwork2.WideSubnet(network1, network2);
            string result = final.ToString();

            string expected = "172.64.0.0/13";
            Assert.AreEqual(expected, result, "Supernet");
        }

        [TestMethod]
        public void TestSupernet1()
        {
            var network1 = IPNetwork2.Parse("192.168.0.1/24");
            var network2 = IPNetwork2.Parse("192.168.1.1/24");
            var expected = IPNetwork2.Parse("192.168.0.0/23");
            IPNetwork2 supernet = network1.Supernet(network2);

            Assert.AreEqual(expected, supernet, "supernet");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestSupernet2()
        {
            IPNetwork2 network1 = null;
            var network2 = IPNetwork2.Parse("192.168.1.1/24");
            IPNetwork2 supernet = network1.Supernet(network2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSupernet3()
        {
            var network1 = IPNetwork2.Parse("192.168.1.1/24");
            IPNetwork2 network2 = null;
            IPNetwork2 supernet = network1.Supernet(network2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSupernet4()
        {
            var network1 = IPNetwork2.Parse("192.168.0.1/24");
            var network2 = IPNetwork2.Parse("192.168.1.1/25");
            IPNetwork2 supernet = network1.Supernet(network2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestSupernet5()
        {
            var network1 = IPNetwork2.Parse("192.168.0.1/24");
            var network2 = IPNetwork2.Parse("192.168.5.1/24");
            IPNetwork2 supernet = network1.Supernet(network2);
        }

        [TestMethod]
        public void TestSupernet6()
        {
            var network1 = IPNetwork2.Parse("192.168.0.1/24");
            var network2 = IPNetwork2.Parse("192.168.0.1/25");
            var expected = IPNetwork2.Parse("192.168.0.0/24");
            IPNetwork2 supernet = network1.Supernet(network2);

            Assert.AreEqual(expected, supernet, "supernet");
        }

        [TestMethod]
        public void TestSupernet7()
        {
            var network1 = IPNetwork2.Parse("192.168.0.1/25");
            var network2 = IPNetwork2.Parse("192.168.0.1/24");
            var expected = IPNetwork2.Parse("192.168.0.0/24");
            IPNetwork2 supernet = network1.Supernet(network2);

            Assert.AreEqual(expected, supernet, "supernet");
        }

        [TestMethod]
        public void TestSupernetStatic1()
        {
            var network1 = IPNetwork2.Parse("192.168.0.1/25");
            var network2 = IPNetwork2.Parse("192.168.0.1/24");
            var expected = IPNetwork2.Parse("192.168.0.0/24");
#pragma warning disable CS0618 // Type or member is obsolete
            var supernet = IPNetwork2.Supernet(network1, network2);
#pragma warning restore CS0618 // Type or member is obsolete

            Assert.AreEqual(expected, supernet, "supernet");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSupernet8()
        {
            var network1 = IPNetwork2.Parse("192.168.1.1/24");
            var network2 = IPNetwork2.Parse("192.168.2.1/24");
            IPNetwork2 supernet = network1.Supernet(network2);
        }

        [TestMethod]
        public void TestSupernet9()
        {
            var ipnetwork1 = IPNetwork2.Parse("200.16.0.0/24");
            var ipnetwork2 = IPNetwork2.Parse("200.16.1.0/24");
            var ipnetwork3 = IPNetwork2.Parse("200.16.2.0/24");
            var ipnetwork4 = IPNetwork2.Parse("200.16.3.0/24");

            IPNetwork2 result = IPNetwork2.Supernet(new[] { ipnetwork1, ipnetwork2, ipnetwork3, ipnetwork4 })[0];
            var expected = IPNetwork2.Parse("200.16.0.0/22");

            Assert.AreEqual(expected, result, "supernet");
        }

        [TestMethod]
        public void TestSupernet10()
        {
            var ipnetwork1 = IPNetwork2.Parse("1.1.0.0/24");
            var ipnetwork2 = IPNetwork2.Parse("1.2.1.0/24");

            IPNetwork2 result = IPNetwork2.Supernet(new[] { ipnetwork1, ipnetwork2 })[0];
            var expected = IPNetwork2.Parse("1.1.0.0/24");

            Assert.AreEqual(expected, result, "supernet");
        }

        #endregion

        #region TrySupernet

        [TestMethod]
        public void TestTrySupernet1()
        {
            var network1 = IPNetwork2.Parse("192.168.0.1/24");
            var network2 = IPNetwork2.Parse("192.168.1.1/24");
            var supernetExpected = IPNetwork2.Parse("192.168.0.0/23");
            IPNetwork2 supernet;
            bool parsed = true;
            bool result = network1.TrySupernet(network2, out supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestTrySupernet2()
        {
            IPNetwork2 network1 = null;
            var network2 = IPNetwork2.Parse("192.168.1.1/24");
            IPNetwork2 supernet;

#pragma warning disable 0618
            bool result = IPNetwork2.TrySupernet(network1, network2, out supernet);
#pragma warning restore 0618
        }

        [TestMethod]
        public void TestTrySupernetStatic2()
        {
            IPNetwork2 network1 = IPNetwork2.IANA_ABLK_RESERVED1;
            var network2 = IPNetwork2.Parse("192.168.1.1/24");
            IPNetwork2 supernet;

#pragma warning disable 0618
            bool result = IPNetwork2.TrySupernet(network1, network2, out supernet);
#pragma warning restore 0618
        }

        [TestMethod]
        public void TestTrySupernet3()
        {
            var network1 = IPNetwork2.Parse("192.168.1.1/24");
            IPNetwork2 network2 = null;
            IPNetwork2 supernetExpected = null;
            IPNetwork2 supernet;
            bool parsed = false;
            bool result = network1.TrySupernet(network2, out supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

        [TestMethod]
        public void TestTrySupernet4()
        {
            var network1 = IPNetwork2.Parse("192.168.0.1/24");
            var network2 = IPNetwork2.Parse("192.168.1.1/25");
            IPNetwork2 supernetExpected = null;
            IPNetwork2 supernet;
            bool parsed = false;
            bool result = network1.TrySupernet(network2, out supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

        [TestMethod]
        public void TestTrySupernet5()
        {
            var network1 = IPNetwork2.Parse("192.168.0.1/24");
            var network2 = IPNetwork2.Parse("192.168.5.1/24");
            IPNetwork2 supernetExpected = null;
            IPNetwork2 supernet;
            bool parsed = false;
            bool result = network1.TrySupernet(network2, out supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

        [TestMethod]
        public void TestTrySupernet6()
        {
            var network1 = IPNetwork2.Parse("192.168.0.1/24");
            var network2 = IPNetwork2.Parse("192.168.0.1/25");
            var supernetExpected = IPNetwork2.Parse("192.168.0.0/24");
            IPNetwork2 supernet;
            bool parsed = true;
            bool result = network1.TrySupernet(network2, out supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

        [TestMethod]
        public void TestTrySupernet7()
        {
            var network1 = IPNetwork2.Parse("192.168.0.1/25");
            var network2 = IPNetwork2.Parse("192.168.0.1/24");
            var supernetExpected = IPNetwork2.Parse("192.168.0.0/24");
            IPNetwork2 supernet;
            bool parsed = true;
            bool result = network1.TrySupernet(network2, out supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

        [TestMethod]
        public void TestTrySupernet8()
        {
            var network1 = IPNetwork2.Parse("192.168.1.1/24");
            var network2 = IPNetwork2.Parse("192.168.2.1/24");
            IPNetwork2 supernetExpected = null;
            IPNetwork2 supernet;
            bool parsed = false;
            bool result = network1.TrySupernet(network2, out supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

        [TestMethod]
        public void TestTrySupernet9()
        {
            var network1 = IPNetwork2.Parse("192.168.1.1/24");
            var network2 = IPNetwork2.Parse("192.168.2.1/24");
            IPNetwork2[] network3 = new[] { network1, network2 };
            IPNetwork2[] supernetExpected = new[] { network1, network2 };
            IPNetwork2[] supernet;
            bool parsed = true;
            bool result = IPNetwork2.TrySupernet(network3, out supernet);

            Assert.AreEqual(supernetExpected[0], supernet[0], "supernet");
            Assert.AreEqual(supernetExpected[1], supernet[1], "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

        [TestMethod]
        public void TestTrySupernet10()
        {
            var network1 = IPNetwork2.Parse("192.168.0.1/24");
            var network2 = IPNetwork2.Parse("192.168.1.1/24");
            IPNetwork2[] network3 = new[] { network1, network2 };
            IPNetwork2[] supernetExpected = new[] { IPNetwork2.Parse("192.168.0.0/23") };
            IPNetwork2[] supernet;
            bool parsed = true;
            bool result = IPNetwork2.TrySupernet(network3, out supernet);

            Assert.AreEqual(supernetExpected[0], supernet[0], "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

        [TestMethod]
        public void TestTrySupernet11()
        {
            IPNetwork2[] network3 = null;
            IPNetwork2[] supernetExpected = new[] { IPNetwork2.Parse("192.168.0.0/23") };
            IPNetwork2[] supernet;
            bool parsed = false;
            bool result = IPNetwork2.TrySupernet(network3, out supernet);

            Assert.AreEqual(null, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

        #endregion

        #region SupernetArray

        [TestMethod]
        public void TestTrySupernetArray()
        {
            var ipnetwork1 = IPNetwork2.Parse("192.168.0.0/24");
            var ipnetwork2 = IPNetwork2.Parse("192.168.1.0/24");
            var ipnetwork3 = IPNetwork2.Parse("192.168.2.0/24");
            var ipnetwork4 = IPNetwork2.Parse("192.168.3.0/24");

            IPNetwork2[] ipnetworks = { ipnetwork1, ipnetwork2, ipnetwork3, ipnetwork4 };
            IPNetwork2[] expected = { IPNetwork2.Parse("192.168.0.0/22") };

            IPNetwork2[] result = IPNetwork2.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], expected[0], "suppernet");
        }

        [TestMethod]
        public void TestTrySupernetArray1()
        {
            IPNetwork2[] ipnetworks = { };
            IPNetwork2[] expected = { };

            IPNetwork2[] result = IPNetwork2.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestTrySupernetArray2()
        {
            IPNetwork2[] ipnetworks = null;
            IPNetwork2[] result = IPNetwork2.Supernet(ipnetworks);
        }

        [TestMethod]
        public void TestTrySupernetArray3()
        {
            IPNetwork2 ipnetwork1 = null;
            IPNetwork2 ipnetwork2 = null;
            IPNetwork2 ipnetwork3 = null;
            IPNetwork2 ipnetwork4 = null;

            IPNetwork2[] ipnetworks = { ipnetwork1, ipnetwork2, ipnetwork3, ipnetwork4 };
            IPNetwork2[] expected = { };

            IPNetwork2[] result = IPNetwork2.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
        }

        [TestMethod]
        public void TestTrySupernetArray4()
        {
            var ipnetwork1 = IPNetwork2.Parse("192.168.0.0/24");
            IPNetworkCollection subnetted = ipnetwork1.Subnet(32);
            IPNetwork2[] ipnetworks = subnetted.ToArray();
            Assert.AreEqual(256, ipnetworks.Length, "subnet");

            IPNetwork2[] expected = { IPNetwork2.Parse("192.168.0.0/24") };

            IPNetwork2[] result = IPNetwork2.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], ipnetwork1, "suppernet");
        }

        [TestMethod]
        public void TestTrySupernetArray5()
        {
            var ipnetwork1 = IPNetwork2.Parse("192.168.0.0/16");
            IPNetworkCollection subnetted = ipnetwork1.Subnet(24);
            IPNetwork2[] ipnetworks = subnetted.ToArray();
            Assert.AreEqual(256, ipnetworks.Length, "subnet");

            IPNetwork2[] expected = { IPNetwork2.Parse("192.168.0.0/16") };

            IPNetwork2[] result = IPNetwork2.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], ipnetwork1, "suppernet");
        }

        [TestMethod]
        public void TestTrySupernetArray6()
        {
            var ipnetwork1 = IPNetwork2.Parse("192.168.0.0/8");
            IPNetworkCollection subnetted = ipnetwork1.Subnet(24);
            IPNetwork2[] ipnetworks = subnetted.ToArray();
            Assert.AreEqual(65536, ipnetworks.Length, "subnet");

            IPNetwork2[] expected = { IPNetwork2.Parse("192.0.0.0/8") };

            IPNetwork2[] result = IPNetwork2.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], ipnetwork1, "suppernet");
        }

        [TestMethod]
        public void TestTrySupernetArray7()
        {
            IPNetwork2[] ipnetworks =
            {
                IPNetwork2.Parse("10.0.2.2/24"),
                IPNetwork2.Parse("192.168.0.0/24"),
                IPNetwork2.Parse("192.168.1.0/24"),
                IPNetwork2.Parse("192.168.2.0/24"),
                IPNetwork2.Parse("10.0.1.1/24"),
                IPNetwork2.Parse("192.168.3.0/24"),
            };

            IPNetwork2[] expected =
            {
                IPNetwork2.Parse("10.0.1.0/24"),
                IPNetwork2.Parse("10.0.2.0/24"),
                IPNetwork2.Parse("192.168.0/22"),
            };

            IPNetwork2[] result = IPNetwork2.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], result[0], "suppernet");
            Assert.AreEqual(expected[1], result[1], "suppernet1");
            Assert.AreEqual(expected[2], result[2], "suppernet2");
        }

        [TestMethod]
        public void TestTrySupernetArray8()
        {
            IPNetwork2[] ipnetworks =
            {
                IPNetwork2.Parse("10.0.2.2/24"),
                IPNetwork2.Parse("192.168.0.0/24"),
                IPNetwork2.Parse("192.168.1.0/24"),
                IPNetwork2.Parse("192.168.2.0/24"),
                IPNetwork2.Parse("10.0.1.1/24"),
                IPNetwork2.Parse("192.168.3.0/24"),
                IPNetwork2.Parse("10.6.6.6/8"),
            };

            IPNetwork2[] expected =
            {
                IPNetwork2.Parse("10.0.0.0/8"),
                IPNetwork2.Parse("192.168.0/22"),
            };

            IPNetwork2[] result = IPNetwork2.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], result[0], "suppernet");
            Assert.AreEqual(expected[1], result[1], "suppernet1");
        }

        [TestMethod]
        public void TestTrySupernetArray9()
        {
            IPNetwork2[] ipnetworks =
            {
                IPNetwork2.Parse("10.0.2.2/24"),
                IPNetwork2.Parse("192.168.0.0/24"),
                IPNetwork2.Parse("192.168.1.0/24"),
                IPNetwork2.Parse("192.168.2.0/24"),
                IPNetwork2.Parse("10.0.1.1/24"),
                IPNetwork2.Parse("192.168.3.0/24"),
                IPNetwork2.Parse("10.6.6.6/8"),
                IPNetwork2.Parse("11.6.6.6/8"),
                IPNetwork2.Parse("12.6.6.6/8"),
            };

            IPNetwork2[] expected =
            {
                IPNetwork2.Parse("10.0.0.0/7"),
                IPNetwork2.Parse("12.0.0.0/8"),
                IPNetwork2.Parse("192.168.0/22"),
            };

            IPNetwork2[] result = IPNetwork2.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], result[0], "suppernet");
            Assert.AreEqual(expected[1], result[1], "suppernet1");
            Assert.AreEqual(expected[2], result[2], "suppernet2");
        }

        [TestMethod]
        public void TestTrySupernetArray10()
        {
            IPNetwork2[] ipnetworks =
            {
                IPNetwork2.Parse("10.0.2.2/24"),
                IPNetwork2.Parse("10.0.2.2/23"),
            };

            IPNetwork2[] expected =
            {
                IPNetwork2.Parse("10.0.2.2/23"),
            };

            IPNetwork2[] result = IPNetwork2.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], result[0], "suppernet");
        }

        #endregion

        #region WideSubnet

        [TestMethod]
        public void TestWideSubnet1()
        {
            string start = "192.168.168.0";
            string end = "192.168.168.255";
            var expected = IPNetwork2.Parse("192.168.168.0/24");

            var wideSubnet = IPNetwork2.WideSubnet(start, end);
            Assert.AreEqual(expected, wideSubnet, "wideSubnet");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestWideSubnet2()
        {
            string start = null;
            string end = "192.168.168.255";

            var wideSubnet = IPNetwork2.WideSubnet(start, end);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestWideSubnet3()
        {
            string start = "192.168.168.255";
            string end = null;

            var wideSubnet = IPNetwork2.WideSubnet(start, end);
        }

        [TestMethod]
        public void TestWideSubnet4()
        {
            string start = "192.168.168.255";
            string end = "192.168.168.0";

            var expected = IPNetwork2.Parse("192.168.168.0/24");

            var wideSubnet = IPNetwork2.WideSubnet(start, end);
            Assert.AreEqual(expected, wideSubnet, "wideSubnet");
        }

        [TestMethod]
        public void TestWideSubnet7()
        {
            string start = "0.0.0.0";
            string end = "0.255.255.255";

            var expected = IPNetwork2.Parse("0.0.0.0/8");

            var wideSubnet = IPNetwork2.WideSubnet(start, end);
            Assert.AreEqual(expected, wideSubnet, "wideSubnet");
        }

        [TestMethod]
        public void TestWideSubnet8()
        {
            string start = "1.2.3.4";
            string end = "5.6.7.8";

            var expected = IPNetwork2.Parse("0.0.0.0/5");

            var wideSubnet = IPNetwork2.WideSubnet(start, end);
            Assert.AreEqual(expected, wideSubnet, "wideSubnet");
        }

        [TestMethod]
        public void TestWideSubnet9()
        {
            string start = "200.16.0.0/24";
            string end = "200.16.3.0/24";
            string firt = IPNetwork2.Parse(start).FirstUsable.ToString();
            string last = IPNetwork2.Parse(end).LastUsable.ToString();

            var expected = IPNetwork2.Parse("200.16.0.0/22");

            var wideSubnet = IPNetwork2.WideSubnet(firt, last);
            Assert.AreEqual(expected, wideSubnet, "wideSubnet");
        }

        [TestMethod]
        public void TestWideSubnet10()
        {
            string start = "200.16.0.0";
            string end = "200.16.3.255";

            var expected = IPNetwork2.Parse("200.16.0.0/22");

            var wideSubnet = IPNetwork2.WideSubnet(start, end);
            Assert.AreEqual(expected, wideSubnet, "wideSubnet");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWideSubnetInvalid1()
        {
            string start = "invalid";
            string end = "5.6.7.8";

            var wideSubnet = IPNetwork2.WideSubnet(start, end);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWideSubnetInvalid2()
        {
            string start = "1.2.3.4";
            string end = "invalid";

            var wideSubnet = IPNetwork2.WideSubnet(start, end);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestWideSubnetMixed1()
        {
            string start = "1.2.3.4";
            string end = "2001:0db8::";

            var wideSubnet = IPNetwork2.WideSubnet(start, end);
        }

        #endregion

        #region TryGuessCidr

        [TestMethod]
        public void TestTryGuessCidrNull()
        {
            byte cidr;
            bool parsed = IPNetwork2.TryGuessCidr(null, out cidr);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(0, cidr, "cidr");
        }

        [TestMethod]
        public void TestTryGuessCidrA()
        {
            byte cidr;
            bool parsed = IPNetwork2.TryGuessCidr("10.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(8, cidr, "cidr");
        }

        [TestMethod]
        public void TestTryGuessCidrB()
        {
            byte cidr;
            bool parsed = IPNetwork2.TryGuessCidr("172.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(16, cidr, "cidr");
        }

        [TestMethod]
        public void TestTryGuessCidrC()
        {
            byte cidr;
            bool parsed = IPNetwork2.TryGuessCidr("192.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(24, cidr, "cidr");
        }

        [TestMethod]
        public void TestTryGuessCidrD()
        {
            byte cidr;
            bool parsed = IPNetwork2.TryGuessCidr("224.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(24, cidr, "cidr");
        }

        [TestMethod]
        public void TestTryGuessCidrE()
        {
            byte cidr;
            bool parsed = IPNetwork2.TryGuessCidr("240.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(24, cidr, "cidr");
        }

        #endregion

        #region Print

        [TestMethod]
        public void Print()
        {
            var ipn = IPNetwork2.Parse("0.0.0.0/0");
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
            IPNetwork2 ipn = null;
#pragma warning disable 0618
            string print = IPNetwork2.Print(ipn);
#pragma warning restore 0618
        }

        [TestMethod]
        public void PrintStatic()
        {
            IPNetwork2 ipn = IPNetwork2.IANA_ABLK_RESERVED1;
#pragma warning disable 0618
            string print = IPNetwork2.Print(ipn);
#pragma warning restore 0618
        }

        #endregion

        #region Sort

        [TestMethod]
        public void TestSort1()
        {
            string[] ips = new[] { "1.1.1.1", "255.255.255.255", "2.2.2.2", "0.0.0.0" };
            var ipns = new List<IPNetwork2>();
            foreach (string ip in ips)
            {
                IPNetwork2 ipn;
                if (IPNetwork2.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            ipns.Sort();
            Assert.AreEqual("0.0.0.0/32", ipns[0].ToString(), "0");
            Assert.AreEqual("1.1.1.1/32", ipns[1].ToString(), "1");
            Assert.AreEqual("2.2.2.2/32", ipns[2].ToString(), "2");
            Assert.AreEqual("255.255.255.255/32", ipns[3].ToString(), "3");
        }

        [TestMethod]
        public void TestSort2()
        {
            string[] ips = new[] { "0.0.0.100/32", "0.0.0.0/24" };
            var ipns = new List<IPNetwork2>();
            foreach (string ip in ips)
            {
                IPNetwork2 ipn;
                if (IPNetwork2.TryParse(ip, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            ipns.Sort();
            Assert.AreEqual("0.0.0.0/24", ipns[0].ToString(), "0");
            Assert.AreEqual("0.0.0.100/32", ipns[1].ToString(), "1");
        }

        #endregion

        #region TryWideSubnet

        [TestMethod]
        public void TryWideSubnet1()
        {
            string[] ips = new[] { "1.1.1.1", "255.255.255.255", "2.2.2.2", "0.0.0.0" };
            var ipns = new List<IPNetwork2>();
            foreach (string ip in ips)
            {
                IPNetwork2 ipn;
                if (IPNetwork2.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            IPNetwork2 ipnetwork = null;
            bool wide = IPNetwork2.TryWideSubnet(ipns.ToArray(), out ipnetwork);
            Assert.AreEqual(true, wide, "wide");
            Assert.AreEqual("0.0.0.0/0", ipnetwork.ToString(), "ipnetwork");
        }

        [TestMethod]
        public void TryWideSubnet2()
        {
            string[] ips = new[] { "1.1.1.1", "10.0.0.0", "2.2.2.2", "0.0.0.0" };
            var ipns = new List<IPNetwork2>();
            foreach (string ip in ips)
            {
                IPNetwork2 ipn;
                if (IPNetwork2.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            IPNetwork2 ipnetwork = null;
            bool wide = IPNetwork2.TryWideSubnet(ipns.ToArray(), out ipnetwork);
            Assert.AreEqual(true, wide, "wide");
            Assert.AreEqual("0.0.0.0/4", ipnetwork.ToString(), "ipnetwork");
        }

        [TestMethod]
        public void TryWideSubnet3()
        {
            string[] ips = new[] { "a", "b", "c", "d" };
            var ipns = new List<IPNetwork2>();
            foreach (string ip in ips)
            {
                IPNetwork2 ipn;
                if (IPNetwork2.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            IPNetwork2 ipnetwork = null;
            bool wide = IPNetwork2.TryWideSubnet(ipns.ToArray(), out ipnetwork);
            Assert.AreEqual(false, wide, "wide");
        }

        [TestMethod]
        public void TryWideSubnet4()
        {
            string[] ips = new[] { "a", "b", "1.1.1.1", "d" };
            var ipns = new List<IPNetwork2>();
            foreach (string ip in ips)
            {
                IPNetwork2 ipn;
                if (IPNetwork2.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            IPNetwork2 ipnetwork = null;
            bool wide = IPNetwork2.TryWideSubnet(ipns.ToArray(), out ipnetwork);
            Assert.AreEqual(true, wide, "wide");
            Assert.AreEqual("1.1.1.1/32", ipnetwork.ToString(), "ipnetwork");
        }

        [TestMethod]
        public void TryWideSubnetNull()
        {
            IPNetwork2 ipnetwork = null;
            bool wide = IPNetwork2.TryWideSubnet(null, out ipnetwork);
            Assert.AreEqual(false, wide, "wide");
        }

        #endregion

        #region WideSubnet

        [TestMethod]
        public void WideSubnet1()
        {
            string[] ips = new[] { "1.1.1.1", "255.255.255.255", "2.2.2.2", "0.0.0.0" };
            var ipns = new List<IPNetwork2>();
            foreach (string ip in ips)
            {
                IPNetwork2 ipn;
                if (IPNetwork2.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            var ipnetwork = IPNetwork2.WideSubnet(ipns.ToArray());
            Assert.AreEqual("0.0.0.0/0", ipnetwork.ToString(), "ipnetwork");
        }

        [TestMethod]
        public void WideSubnet2()
        {
            string[] ips = new[] { "1.1.1.1", "10.0.0.0", "2.2.2.2", "0.0.0.0" };
            var ipns = new List<IPNetwork2>();
            foreach (string ip in ips)
            {
                IPNetwork2 ipn;
                if (IPNetwork2.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            var ipnetwork = IPNetwork2.WideSubnet(ipns.ToArray());
            Assert.AreEqual("0.0.0.0/4", ipnetwork.ToString(), "ipnetwork");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WideSubnetNull()
        {
            var ipnetwork = IPNetwork2.WideSubnet(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WideSubnetNull2()
        {
            string[] ips = new[] { "a", "b", "e", "d" };
            var ipns = new List<IPNetwork2>();
            foreach (string ip in ips)
            {
                IPNetwork2 ipn;
                if (IPNetwork2.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            var ipnetwork = IPNetwork2.WideSubnet(ipns.ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WideSubnetMixed()
        {
            var ipns = new List<IPNetwork2>
            {
                IPNetwork2.IANA_ABLK_RESERVED1,
                IPNetwork2.Parse("2001:0db8::/64"),
            };
            var ipnetwork = IPNetwork2.WideSubnet(ipns.ToArray());
        }

        #endregion

        #region resize

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestResize1()
        {
            byte[] resut = IPNetwork2.Resize(new byte[33], Sockets.AddressFamily.InterNetwork);
        }

        #endregion

        #region Count
        [TestMethod]
        public void TestTotal32()
        {
            var network = IPNetwork2.Parse("0.0.0.0/32");
            int total = 1;
            Assert.AreEqual(total, network.Total, "Total");
        }

        [TestMethod]
        public void TestTotal31()
        {
            var network = IPNetwork2.Parse("0.0.0.0/31");
            int total = 2;
            Assert.AreEqual(total, network.Total, "Total");
        }

        [TestMethod]
        public void TestTotal30()
        {
            var network = IPNetwork2.Parse("0.0.0.0/30");
            int total = 4;
            Assert.AreEqual(total, network.Total, "Total");
        }

        [TestMethod]
        public void TestTotal24()
        {
            var network = IPNetwork2.Parse("0.0.0.0/24");
            int total = 256;
            Assert.AreEqual(total, network.Total, "Total");
        }

        [TestMethod]
        public void TestTotal16()
        {
            var network = IPNetwork2.Parse("0.0.0.0/16");
            int total = 65536;
            Assert.AreEqual(total, network.Total, "Total");
        }

        [TestMethod]
        public void TestTotal8()
        {
            var network = IPNetwork2.Parse("0.0.0.0/8");
            int total = 16777216;
            Assert.AreEqual(total, network.Total, "Total");
        }

        [TestMethod]
        public void TestTotal0()
        {
            var network = IPNetwork2.Parse("0.0.0.0/0");
            long total = 4294967296;
            Assert.AreEqual(total, network.Total, "Total");
        }

        #endregion

        #region Usable

        [TestMethod]
        public void Usable32()
        {
            var network = IPNetwork2.Parse("0.0.0.0/32");
            uint usable = 0;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        [TestMethod]
        public void Usable31()
        {
            var network = IPNetwork2.Parse("0.0.0.0/31");
            uint usable = 0;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        [TestMethod]
        public void Usable30()
        {
            var network = IPNetwork2.Parse("0.0.0.0/30");
            uint usable = 2;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        [TestMethod]
        public void Usable24()
        {
            var network = IPNetwork2.Parse("0.0.0.0/24");
            uint usable = 254;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        [TestMethod]
        public void Usable16()
        {
            var network = IPNetwork2.Parse("0.0.0.0/16");
            uint usable = 65534;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        [TestMethod]
        public void Usable8()
        {
            var network = IPNetwork2.Parse("0.0.0.0/8");
            uint usable = 16777214;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        [TestMethod]
        public void Usable0()
        {
            var network = IPNetwork2.Parse("0.0.0.0/0");
            uint usable = 4294967294;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        #endregion

        #region TryParseCidr

        [TestMethod]
        public void TryParseCidr1()
        {
            string sidr = "0";
            byte? cidr;
            byte? result = 0;
            bool parsed = IPNetwork2.TryParseCidr(sidr, Sockets.AddressFamily.InterNetwork, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(result, cidr, "cidr");
        }

        [TestMethod]
        public void TryParseCidr2()
        {
            string sidr = "sadsd";
            byte? cidr;
            byte? result = null;

            bool parsed = IPNetwork2.TryParseCidr(sidr, Sockets.AddressFamily.InterNetwork, out cidr);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(result, cidr, "cidr");
        }

        [TestMethod]
        public void TryParseCidr3()
        {
            string sidr = "33";
            byte? cidr;
            byte? result = null;

            bool parsed = IPNetwork2.TryParseCidr(sidr, Sockets.AddressFamily.InterNetwork, out cidr);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(result, cidr, "cidr");
        }

        #endregion

        #region Compare

        [TestMethod]
        public void TestCompareTo1()
        {
            var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
            var ipn2 = IPNetwork2.Parse("10.0.0.2/16");

            int comparison = ipn1.CompareTo(ipn2);

            Assert.AreEqual(0, comparison, "compare");
        }

        [TestMethod]
        public void TestCompareTo2()
        {
            var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
            object ipn2 = (object)IPNetwork2.Parse("10.0.0.2/16");

            int comparison = ipn1.CompareTo(ipn2);

            Assert.AreEqual(0, comparison, "compare");
        }

        [TestMethod]
        public void TestCompareTo3()
        {
            var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
            object ipn2 = null;

            int comparison = ipn1.CompareTo(ipn2);

            Assert.AreEqual(1, comparison, "compare");
        }

        [TestMethod]
        public void TestCompareTo4()
        {
            var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
            IPNetwork2 ipn2 = null;

            int comparison = ipn1.CompareTo(ipn2);

            Assert.AreEqual(1, comparison, "compare");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCompareTo5()
        {
            var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
            string ipn2 = string.Empty;

            int comparison = ipn1.CompareTo(ipn2);
        }

        [TestMethod]
        public void TestCompareTo6()
        {
            var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
            int comparison = ipn1.CompareTo(ipn1);

            Assert.AreEqual(0, comparison, "compare");
        }

        [TestMethod]
        public void TestCompare1()
        {
            var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
            int comparison = IPNetwork2.Compare(null, ipn1);

            Assert.AreEqual(-1, comparison, "compare");
        }

        [TestMethod]
        public void TestCompare2()
        {
            var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
            var ipn2 = IPNetwork2.Parse("20.0.0.1/16");
            int comparison = IPNetwork2.Compare(ipn1, ipn2);

            Assert.AreEqual(-1, comparison, "compare");
        }

        #endregion

        #region Operator

        [TestMethod]
        public void TestOperatorGreater1()
        {
            var ipn1 = IPNetwork2.Parse("10.0.0.1/32");
            var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

            bool greater = ipn1 > ipn2;

            Assert.AreEqual(false, greater, "greater");
        }

        [TestMethod]
        public void TestOperatorGreater2()
        {
            var ipn1 = IPNetwork2.Parse("10.0.0.100/32");
            var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

            bool greater = ipn1 > ipn2;

            Assert.AreEqual(true, greater, "greater");
        }

        [TestMethod]
        public void TestOperatorLower1()
        {
            var ipn1 = IPNetwork2.Parse("10.0.0.1/32");
            var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

            bool lower = ipn1 < ipn2;

            Assert.AreEqual(true, lower, "lower");
        }

        [TestMethod]
        public void TestOperatorLower2()
        {
            var ipn1 = IPNetwork2.Parse("10.0.0.100/32");
            var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

            bool lower = ipn1 < ipn2;

            Assert.AreEqual(false, lower, "lower");
        }

        [TestMethod]
        public void TestOperatorDifferent1()
        {
            var ipn1 = IPNetwork2.Parse("10.0.0.100/32");
            var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

            bool different = ipn1 != ipn2;

            Assert.AreEqual(true, different, "different");
        }

        [TestMethod]
        public void TestOperatorDifferent2()
        {
            var ipn1 = IPNetwork2.Parse("10.0.0.1/32");
            var ipn2 = IPNetwork2.Parse("10.0.0.1/32");

            bool different = ipn1 != ipn2;

            Assert.AreEqual(false, different, "different");
        }

        [TestMethod]
        public void TestOperatorEqual1()
        {
            var ipn1 = IPNetwork2.Parse("10.0.0.100/32");
            var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

            bool eq = ipn1 == ipn2;

            Assert.AreEqual(false, eq, "eq");
        }

        [TestMethod]
        public void TestOperatorEqual2()
        {
            var ipn1 = IPNetwork2.Parse("10.0.0.1/32");
            var ipn2 = IPNetwork2.Parse("10.0.0.1/32");

            bool eq = ipn1 == ipn2;

            Assert.AreEqual(true, eq, "eq");
        }

        #endregion
    }
}
