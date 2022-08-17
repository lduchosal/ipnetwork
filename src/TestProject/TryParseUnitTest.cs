using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// IPNetworkUnitTest test every single method
    /// </summary>
    [TestClass]
    public class TryParseUnitTest
    {


        #region TryParse IPV4

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE2() {
            IPNetwork ipnet = null;
            IPAddress ip = null;
            bool parsed = IPNetwork.TryParse(ip, ip, out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE3() {
            IPNetwork ipnet = null;
            bool parsed = IPNetwork.TryParse("", 0, out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }
        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE4() {
            IPNetwork ipnet = null;
            bool parsed = IPNetwork.TryParse(null, 0, out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }
        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE5() {
            string n = null;
            IPNetwork ipnet = null;
            bool parsed = IPNetwork.TryParse(n, n, out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");

        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE6() {
            IPNetwork ipnet = null;
            bool parsed = IPNetwork.TryParse(IPAddress.Parse("10.10.10.10"), null, out ipnet);
            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");
        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE7() {
            IPNetwork ipnet = null;
            bool parsed = IPNetwork.TryParse("0.0.0.0", null, out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");

        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE8() {
            IPNetwork ipnet = null;
            bool parsed = IPNetwork.TryParse("x.x.x.x", "x.x.x.x", out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");

        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE9() {
            IPNetwork ipnet = null;
            bool parsed = IPNetwork.TryParse("0.0.0.0", "x.x.x.x", out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");

        }



        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE10() {
            IPNetwork ipnet = null;
            bool parsed = IPNetwork.TryParse("x.x.x.x", 0, out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");

        }

        [TestMethod]
        public void TestTryParseIPAddressNetmaskANE11() {
            IPNetwork ipnet = null;
            bool parsed = IPNetwork.TryParse("0.0.0.0", 33, out ipnet);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(null, ipnet, "ipnet");

        }


        [TestMethod]
        public void TestTryParseIPAddressNetmask() {

            IPNetwork ipnetwork = null;
            string ipaddress = "192.168.168.100";
            string netmask = "255.255.255.0";

            string network = "192.168.168.0";
            string broadcast = "192.168.168.255";
            string firstUsable = "192.168.168.1";
            string lastUsable = "192.168.168.254";
            byte cidr = 24;
            uint usable = 254;

            bool parsed = IPNetwork.TryParse(ipaddress, netmask, out ipnetwork);
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
        public void TestTryParseString1() {

            IPNetwork ipnetwork = null;
            string ipaddress = "192.168.168.100 255.255.255.0";

            string network = "192.168.168.0";
            string netmask = "255.255.255.0";
            string broadcast = "192.168.168.255";
            string firstUsable = "192.168.168.1";
            string lastUsable = "192.168.168.254";
            byte cidr = 24;
            uint usable = 254;

            bool parsed = IPNetwork.TryParse(ipaddress, out ipnetwork);
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
        public void TestTryParseString2() {

            IPNetwork ipnetwork = null;
            string ipaddress = "192.168.168.100/24";

            string network = "192.168.168.0";
            string netmask = "255.255.255.0";
            string broadcast = "192.168.168.255";
            string firstUsable = "192.168.168.1";
            string lastUsable = "192.168.168.254";
            byte cidr = 24;
            uint usable = 254;

            bool parsed = IPNetwork.TryParse(ipaddress, out ipnetwork);
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
        public void TestTryParseString3() {

            IPNetwork ipnetwork = null;
            string ipaddress = "0.0.0.0/0";

            string network = "0.0.0.0";
            string netmask = "0.0.0.0";
            string broadcast = "255.255.255.255";
            string firstUsable = "0.0.0.1";
            string lastUsable = "255.255.255.254";
            byte cidr = 0;
            uint usable = 4294967294;

            bool parsed = IPNetwork.TryParse(ipaddress, out ipnetwork);
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
        public void TestTryParseString4() {

            IPNetwork ipnetwork = null;
            string ipaddress = "0.0.0.0/32";

            string network = "0.0.0.0";
            string netmask = "255.255.255.255";
            string broadcast = "0.0.0.0";
            string firstUsable = "0.0.0.0";
            string lastUsable = "0.0.0.0";
            byte cidr = 32;
            uint usable = 0;

            bool parsed = IPNetwork.TryParse(ipaddress, out ipnetwork);
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
        public void TestTryParseString5() {

            IPNetwork ipnetwork = null;
            string ipaddress = "255.255.255.255/32";

            string network = "255.255.255.255";
            string netmask = "255.255.255.255";
            string broadcast = "255.255.255.255";
            string firstUsable = "255.255.255.255";
            string lastUsable = "255.255.255.255";
            byte cidr = 32;
            uint usable = 0;

            bool parsed = IPNetwork.TryParse(ipaddress, out ipnetwork);
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
        public void TestTryParseStringAE1() {
            string ipaddress = "garbage";
            IPNetwork ipnetwork = null;
            bool parsed = IPNetwork.TryParse(ipaddress, out ipnetwork);
            Assert.AreEqual(false, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryParseStringAE2() {
            string ipaddress = "0.0.0.0 0.0.1.0";
            IPNetwork ipnetwork = null;
            bool parsed = IPNetwork.TryParse(ipaddress, out ipnetwork);
            Assert.AreEqual(false, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryParseStringANE1() {
            string ipaddress = null;
            IPNetwork ipnetwork = null;
            bool parsed = IPNetwork.TryParse(ipaddress, out ipnetwork);
            Assert.AreEqual(false, parsed, "parsed");
        }



        #endregion


        #region TryParse

        [TestMethod]
        public void Test_TryParse_InvalidIpv6_return_valid_ipv6network()
        {
            var parsed1 = IPNetwork.TryParse("g001:02b8::/64", out var ipnetwork1);
            var parsed2 = IPNetwork.TryParse("1:2b8::/64", out var ipnetwork2);

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
            var result = IPNetwork.TryParse(ipnetwork, sanitanize, out var ipnetwork1);

            Assert.AreEqual(parsed, result, "parsed1");
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
            var result = IPAddress.TryParse(ipaddress, out var ipaddress1);

            Assert.AreEqual(parsed, result, "parsed1");
        }

        #endregion
    }
}
