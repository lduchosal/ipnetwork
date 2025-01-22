// <copyright file="TryParseUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net.TestProject
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// IPNetworkUnitTest test every single method.
    /// </summary>
    [TestClass]
    public class TryParseUnitTest
    {
        #region TryParse IPV4

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE2()
        {
            IPAddress ip = null;
            bool parsed = IPNetwork2.TryParse(ip, ip, out IPNetwork2 ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE3()
        {
            bool parsed = IPNetwork2.TryParse(string.Empty, 0, out IPNetwork2 ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE4()
        {
            bool parsed = IPNetwork2.TryParse(null, 0, out IPNetwork2 ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE5()
        {
            string n = null;

            bool parsed = IPNetwork2.TryParse(n, n, out IPNetwork2 ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE6()
        {
            bool parsed = IPNetwork2.TryParse(IPAddress.Parse("10.10.10.10"), null, out IPNetwork2 ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE7()
        {
            bool parsed = IPNetwork2.TryParse("0.0.0.0", netmask: null, out IPNetwork2 ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE8()
        {
            bool parsed = IPNetwork2.TryParse("x.x.x.x", "x.x.x.x", out IPNetwork2 ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE9()
        {
            bool parsed = IPNetwork2.TryParse("0.0.0.0", "x.x.x.x", out IPNetwork2 ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE10()
        {
            bool parsed = IPNetwork2.TryParse("x.x.x.x", 0, out IPNetwork2 ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE11()
        {
            bool parsed = IPNetwork2.TryParse("0.0.0.0", 33, out IPNetwork2 ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmask()
        {
            string ipaddress = "192.168.168.100";
            string netmask = "255.255.255.0";

            string network = "192.168.168.0";
            string broadcast = "192.168.168.255";
            string firstUsable = "192.168.168.1";
            string lastUsable = "192.168.168.254";
            byte cidr = 24;
            uint usable = 254;

            bool parsed = IPNetwork2.TryParse(ipaddress, netmask, out IPNetwork2 ipnetwork);

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
            string ipaddress = "192.168.168.100 255.255.255.0";

            string network = "192.168.168.0";
            string netmask = "255.255.255.0";
            string broadcast = "192.168.168.255";
            string firstUsable = "192.168.168.1";
            string lastUsable = "192.168.168.254";
            byte cidr = 24;
            uint usable = 254;

            bool parsed = IPNetwork2.TryParse(ipaddress, out IPNetwork2 ipnetwork);

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
            string ipaddress = "192.168.168.100/24";

            string network = "192.168.168.0";
            string netmask = "255.255.255.0";
            string broadcast = "192.168.168.255";
            string firstUsable = "192.168.168.1";
            string lastUsable = "192.168.168.254";
            byte cidr = 24;
            uint usable = 254;

            bool parsed = IPNetwork2.TryParse(ipaddress, out IPNetwork2 ipnetwork);

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
            string ipaddress = "0.0.0.0/0";

            string network = "0.0.0.0";
            string netmask = "0.0.0.0";
            string broadcast = "255.255.255.255";
            string firstUsable = "0.0.0.1";
            string lastUsable = "255.255.255.254";
            byte cidr = 0;
            uint usable = 4294967294;

            bool parsed = IPNetwork2.TryParse(ipaddress, out IPNetwork2 ipnetwork);

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
            string ipaddress = "0.0.0.0/32";

            string network = "0.0.0.0";
            string netmask = "255.255.255.255";
            string broadcast = "0.0.0.0";
            string firstUsable = "0.0.0.0";
            string lastUsable = "0.0.0.0";
            byte cidr = 32;
            uint usable = 0;

            bool parsed = IPNetwork2.TryParse(ipaddress, out IPNetwork2 ipnetwork);

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
            string ipaddress = "255.255.255.255/32";

            string network = "255.255.255.255";
            string netmask = "255.255.255.255";
            string broadcast = "255.255.255.255";
            string firstUsable = "255.255.255.255";
            string lastUsable = "255.255.255.255";
            byte cidr = 32;
            uint usable = 0;

            bool parsed = IPNetwork2.TryParse(ipaddress, out IPNetwork2 ipnetwork);

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
            bool parsed = IPNetwork2.TryParse(ipaddress, out IPNetwork2 ipnetwork);

            Assert.AreEqual(false, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryParseStringAE2()
        {
            string ipaddress = "0.0.0.0 0.0.1.0";
            bool parsed = IPNetwork2.TryParse(ipaddress, out IPNetwork2 ipnetwork);

            Assert.AreEqual(false, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryParseStringANE1()
        {
            string ipaddress = null;
            bool parsed = IPNetwork2.TryParse(ipaddress, out IPNetwork2 ipnetwork);

            Assert.AreEqual(false, parsed, "parsed");
        }
        #endregion

        #region TryParse
        [TestMethod]
        public void Test_TryParse_InvalidIpv6_return_valid_ipv6network()
        {
            bool parsed1 = IPNetwork2.TryParse("g001:02b8::/64", out IPNetwork2 ipnetwork1);
            bool parsed2 = IPNetwork2.TryParse("1:2b8::/64", out IPNetwork2 ipnetwork2);

            Assert.AreEqual(true, parsed1, "parsed1");
            Assert.AreEqual(true, parsed2, "parsed2");
            Assert.AreEqual(ipnetwork1, ipnetwork2, "ipnetwork1 == ipnetwork2");
        }

        [DataTestMethod]
        [DataRow("1.1.1.1/1", true, true)]
        [DataRow("1.1.1.1/1", false, true)]
        [DataRow("::/0", true, true)]
        [DataRow("::/0", false, true)]
        [DataRow("g001:02b8::/64", true, true)]
        [DataRow("g001:02b8::/64", false, false)]
        [DataRow("    001:02b8::/64", false, false)]
        [DataRow("    001:02b8::/64", true, true)]
        [DataRow("001:02b8::    /    64", true, true)]
        [DataRow("001:02b8::    /    64", false, false)]
        [DataRow("001:02b8::/64", true, true)]
        [DataRow("001:02b8::/64", false, true)]
        public void Test_TryParse(string ipnetwork, bool sanitanize, bool parsed)
        {
            bool result = IPNetwork2.TryParse(ipnetwork, sanitanize, out IPNetwork2 ipnetwork1);

            Assert.AreEqual(parsed, result, "parsed1");
        }

        /// <summary>
        /// Test IPNetwork2.TryParse method with ip addresses or networks using classfull CIDR guessing.
        /// </summary>
        /// <param name="ipnetwork">A string containing an ip address to convert.</param>
        /// <param name="sanitanize">Whether to sanitize network or not.</param>
        /// <param name="parsed">The expected parse result.</param>
        [DataTestMethod]
        [DataRow("1.1.1.1/1", true, true)]
        [DataRow("1.1.1.1/1", false, true)]
        [DataRow("::/0", true, true)]
        [DataRow("::/0", false, true)]
        [DataRow("g001:02b8::/64", true, true)]
        [DataRow("g001:02b8::/64", false, false)]
        [DataRow("    001:02b8::/64", false, false)]
        [DataRow("    001:02b8::/64", true, true)]
        [DataRow("001:02b8::    /    64", true, true)]
        [DataRow("001:02b8::    /    64", false, false)]
        [DataRow("001:02b8::/64", true, true)]
        [DataRow("001:02b8::/64", false, true)]
        public void Test_TryParse_ClassFull(string ipnetwork, bool sanitanize, bool parsed)
        {
            bool result = IPNetwork2.TryParse(ipnetwork, sanitanize, out IPNetwork2 ipnetwork1);
            bool classfullResult = IPNetwork2.TryParse(ipnetwork, CidrGuess.ClassFull, sanitanize, out IPNetwork2 ipnetwork2);

            Assert.AreEqual(parsed, result, "parsed - class unspecified");
            Assert.AreEqual(parsed, classfullResult, "parsed - classfull");
            if (parsed)
            {
                Assert.AreEqual(ipnetwork1.Cidr, ipnetwork2.Cidr, "cidr");
            }
        }

        /// <summary>
        /// Test IPNetwork2.TryParse method with plain ip addresses using classless CIDR guessing.
        /// </summary>
        /// <param name="ipaddress">A string containing an ip address to convert.</param>
        /// <param name="cidr">The expected CIDR netmask notation, 32 for IPv4 and 128 for IPv6.</param>
        [DataTestMethod]
        [DataRow("10.0.0.0", 32)]
        [DataRow("::", 128)]
        [DataRow("2001:0db8::", 128)]
        public void Test_TryParse_ClassLess(string ipaddress, int cidr)
        {
            bool parsed = IPNetwork2.TryParse(ipaddress, CidrGuess.ClassLess, out IPNetwork2 ipnetwork2);

            Assert.IsTrue(parsed, "parsed");
            Assert.AreEqual(cidr, ipnetwork2.Cidr, "cidr");
        }

        [DataTestMethod]
        [DataRow("1.1.1.1", true)]
        [DataRow("::", true)]
        [DataRow("001:02b8::", true)]
        [DataRow("g001:02b8::", false)]
        [DataRow(" 001:02b8::", false)]
        [DataRow(" 001:02b8:: ", false)]
        [DataRow("001:02b8:: ", false)]
        public void Test_IPAddress_TryParse(string ipaddress, bool parsed)
        {
            bool result = IPAddress.TryParse(ipaddress, out IPAddress ipaddress1);

            Assert.AreEqual(parsed, result, "parsed1");
        }

        #endregion
        
        #region Issue294
        
        [TestMethod]
        public void Test_IPNetwork_TryParse_Issue294()
        {
            bool result = IPNetwork2.TryParse("*", out IPNetwork2 ipaddress1);
            Assert.AreEqual(false, result, "parsed1");
        }
        #endregion
    }
}
