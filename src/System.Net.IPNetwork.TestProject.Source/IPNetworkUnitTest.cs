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
    public class IPNetworkUnitTest {

        #region Parse

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseIPAddressNetmaskANE2() {
            IPAddress ip = null;
            IPNetwork.Parse(ip, ip);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseIPAddressNetmaskANE3() {
            IPNetwork.Parse("", 0);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseIPAddressNetmaskANE4() {
            IPNetwork.Parse(null, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseIPAddressNetmaskANE5() {
            string n = null;
            IPNetwork.Parse(n, n);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseIPAddressNetmaskANE8() {
            IPNetwork ipnet = IPNetwork.Parse("x.x.x.x", "x.x.x.x");

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseIPAddressNetmaskANE9() {
            IPNetwork ipnet = IPNetwork.Parse("0.0.0.0", "x.x.x.x");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseIPAddressNetmaskANE10() {
            IPNetwork ipnet = IPNetwork.Parse("x.x.x.x", 0);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseIPAddressNetmaskANE11() {
            IPNetwork ipnet = IPNetwork.Parse("0.0.0.0", 33);

        }

        [TestMethod]
        public void TestParseIPAddressNetmask() {

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
        public void TestParseString1() {

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
        public void TestParseString2() {

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
        public void TestParseString3() {

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
        public void TestParseString4() {

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
        public void TestParseString5() {

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
        public void TestParseIPAddressNoNetmask1() {

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
        public void TestParseIPAddressNoNetmask2() {

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
        public void TestParseIPAddressNoNetmask3() {

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
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseIPAddressNoNetmask4() {

            string ipaddress = "224.0.0.0";
            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseIPAddressNoNetmask5() {

            string ipaddress = "240.0.0.0";
            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress);

        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseStringAE1() {
            string ipaddress = "garbage";
            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseStringAE2() {
            string ipaddress = "0.0.0.0 0.0.1.0";
            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseStringANE1() {
            string ipaddress = null;
            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress);
        }



        #endregion

        #region TryParse

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

        #region ParseStringString

        [TestMethod]
        public void TestParseStringString1() {

            string ipaddress = "192.168.168.100";
            string netmask = "255.255.255.0";

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress, netmask);
            Assert.AreEqual("192.168.168.0/24", ipnetwork.ToString(), "network");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseStringString2() {

            string ipaddress = null;
            string netmask = null;

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress, netmask);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseStringString3() {

            string ipaddress = "192.168.168.100";
            string netmask = null;

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress, netmask);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseStringString4() {

            string ipaddress = "";
            string netmask = "";

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress, netmask);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseStringString5() {

            string ipaddress = "192.168.168.100";
            string netmask = "";

            IPNetwork ipnetwork = IPNetwork.Parse(ipaddress, netmask);

        }

        #endregion

        #region ParseIpIp

        [TestMethod]
        public void ParseIpIp1() {

            string ipaddress = "192.168.168.100";
            string netmask = "255.255.255.0";
            IPAddress ip = IPAddress.Parse(ipaddress);
            IPAddress netm = IPAddress.Parse(netmask);
            IPNetwork ipnetwork = IPNetwork.Parse(ip, netm);
            Assert.AreEqual("192.168.168.0/24", ipnetwork.ToString(), "network");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseIpIp2() {

            IPAddress ip = null;
            IPAddress netm = null;
            IPNetwork ipnetwork = IPNetwork.Parse(ip, netm);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseIpIp3() {

            string ipaddress = "192.168.168.100";
            IPAddress ip = IPAddress.Parse(ipaddress);
            IPAddress netm = null;
            IPNetwork ipnetwork = IPNetwork.Parse(ip, netm);

        }

        #endregion

        #region ToCidr

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestToCidrANE() {
            IPNetwork.ToCidr(null);
        }

        [TestMethod]
        public void TestToCidrAE() {
            byte cidr = IPNetwork.ToCidr(IPAddress.IPv6Any);
            Assert.AreEqual(0, cidr, "cidr");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestToCidrAE2() {
            IPNetwork.ToCidr(IPAddress.Parse("6.6.6.6"));
        }


        [TestMethod]
        public void TestToCidr32() {

            IPAddress mask = IPAddress.Parse("255.255.255.255");
            byte cidr = 32;
            int result = IPNetwork.ToCidr(mask);

            Assert.AreEqual(cidr, result, "cidr");
        }
        [TestMethod]
        public void TestToCidr24() {

            IPAddress mask = IPAddress.Parse("255.255.255.0");
            byte cidr = 24;
            int result = IPNetwork.ToCidr(mask);

            Assert.AreEqual(cidr, result, "cidr");
        }
        [TestMethod]
        public void TestToCidr16() {

            IPAddress mask = IPAddress.Parse("255.255.0.0");
            byte cidr = 16;
            int result = IPNetwork.ToCidr(mask);

            Assert.AreEqual(cidr, result, "cidr");
        }
        [TestMethod]
        public void TestToCidr8() {

            IPAddress mask = IPAddress.Parse("255.0.0.0");
            byte cidr = 8;
            int result = IPNetwork.ToCidr(mask);

            Assert.AreEqual(cidr, result, "cidr");
        }
        [TestMethod]
        public void TestToCidr0() {

            IPAddress mask = IPAddress.Parse("0.0.0.0");
            byte cidr = 0;
            int result = IPNetwork.ToCidr(mask);

            Assert.AreEqual(cidr, result, "cidr");
        }

        #endregion

        #region TryToCidr

        [TestMethod]
        public void TestTryToCidrANE() {
            byte? cidr = null;
            bool parsed = IPNetwork.TryToCidr(null, out cidr);
            Assert.AreEqual(false, parsed, "parsed");
        }

        [TestMethod]
        public void TestTryToCidrAE() {
            byte? cidr = null;
            bool parsed = IPNetwork.TryToCidr(IPAddress.IPv6Any, out cidr);
            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual((byte)0, cidr, "cidr");
        }


        [TestMethod]
        public void TestTryToCidrAE2() {
            byte? cidr = null;
            bool parsed = IPNetwork.TryToCidr(IPAddress.Parse("6.6.6.6"), out cidr);
            Assert.AreEqual(false, parsed, "parsed");
        }


        [TestMethod]
        public void TestTryToCidr32() {
            byte? cidr = null;
            IPAddress mask = IPAddress.Parse("255.255.255.255");
            byte result = 32;
            bool parsed = IPNetwork.TryToCidr(mask, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(cidr, result, "cidr");
        }
        [TestMethod]
        public void TestTryToCidr24() {
            byte? cidr = null;
            IPAddress mask = IPAddress.Parse("255.255.255.0");
            byte result = 24;
            bool parsed = IPNetwork.TryToCidr(mask, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(cidr, result, "cidr");
            
        }
        [TestMethod]
        public void TestTryToCidr16() {

            byte? cidr = null;
            IPAddress mask = IPAddress.Parse("255.255.0.0");
            byte result = 16;
            bool parsed = IPNetwork.TryToCidr(mask, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(cidr, result, "cidr");
            
        }
        [TestMethod]
        public void TestTryToCidr8() {


            byte? cidr = null;
            IPAddress mask = IPAddress.Parse("255.0.0.0");
            byte result = 8;
            bool parsed = IPNetwork.TryToCidr(mask, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(cidr, result, "cidr");
            
        }
        [TestMethod]
        public void TestTryToCidr0() {

            byte? cidr = null;
            IPAddress mask = IPAddress.Parse("0.0.0.0");
            byte result = 0;
            bool parsed = IPNetwork.TryToCidr(mask, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(cidr, result, "cidr");
            
        }

        #endregion

        #region ToBigInteger

        [TestMethod]
        public void TestToBigInteger32() {

            IPAddress mask = IPAddress.Parse("255.255.255.255");
            uint uintMask = 0xffffffff;
            BigInteger result = IPNetwork.ToBigInteger(mask);

            Assert.AreEqual(uintMask, result, "uint");
        }
        [TestMethod]
        public void TestToBigInteger24() {

            IPAddress mask = IPAddress.Parse("255.255.255.0");
            uint uintMask = 0xffffff00;
            BigInteger? result = IPNetwork.ToBigInteger(mask);

            Assert.AreEqual(uintMask, result, "uint");
        }
        [TestMethod]
        public void TestToBigInteger16() {

            IPAddress mask = IPAddress.Parse("255.255.0.0");
            uint uintMask = 0xffff0000;
            BigInteger? result = IPNetwork.ToBigInteger(mask);

            Assert.AreEqual(uintMask, result, "uint");
        }
        [TestMethod]
        public void TestToBigInteger8() {

            IPAddress mask = IPAddress.Parse("255.0.0.0");
            uint uintMask = 0xff000000;
            BigInteger? result = IPNetwork.ToBigInteger(mask);

            Assert.AreEqual(uintMask, result, "uint");
        }
        [TestMethod]
        public void TestToBigInteger0() {

            IPAddress mask = IPAddress.Parse("0.0.0.0");
            uint uintMask = 0x00000000;
            BigInteger? result = IPNetwork.ToBigInteger(mask);

            Assert.AreEqual(uintMask, result, "uint");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestToBigIntegerANE() {
            BigInteger? result = IPNetwork.ToBigInteger(null);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestToBigIntegerANE3() {
            IPAddress ip = null;
            BigInteger? result = IPNetwork.ToBigInteger(ip);
        }
        [TestMethod]
        public void TestToBigIntegerANE2() {
            BigInteger? result = IPNetwork.ToBigInteger(IPAddress.IPv6Any);
            uint expected = 0;
            Assert.AreEqual(expected, result, "result");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestToBigIntegerByte() {
            BigInteger result = IPNetwork.ToUint(33, Sockets.AddressFamily.InterNetwork);
        }

        [TestMethod]
        public void TestToBigIntegerByte2() {
            BigInteger result = IPNetwork.ToUint(32, Sockets.AddressFamily.InterNetwork);
            uint expected = 4294967295;
            Assert.AreEqual(expected, result, "result");

        }


        [TestMethod]
        public void TestToBigIntegerByte3() {
            BigInteger result = IPNetwork.ToUint(0, Sockets.AddressFamily.InterNetwork);
            uint expected = 0;
            Assert.AreEqual(expected, result, "result");

        }



        #endregion

        #region TryToBigInteger

        [TestMethod]
        public void TestTryToBigInteger32() {

            IPAddress mask = IPAddress.Parse("255.255.255.255");
            uint uintMask = 0xffffffff;
            BigInteger? result = null;
            bool parsed = IPNetwork.TryToBigInteger(mask, out result);

            Assert.AreEqual(uintMask, result, "uint");
            Assert.AreEqual(true, parsed, "parsed");
        }
        [TestMethod]
        public void TestTryToBigInteger24() {

            IPAddress mask = IPAddress.Parse("255.255.255.0");
            uint uintMask = 0xffffff00;
            BigInteger? result = null;
            bool parsed = IPNetwork.TryToBigInteger(mask, out result);

            Assert.AreEqual(uintMask, result, "uint");
            Assert.AreEqual(true, parsed, "parsed");
        }
        [TestMethod]
        public void TestTryToBigInteger16() {

            IPAddress mask = IPAddress.Parse("255.255.0.0");
            uint uintMask = 0xffff0000;
            BigInteger? result = null;
            bool parsed = IPNetwork.TryToBigInteger(mask, out result);

            Assert.AreEqual(uintMask, result, "uint");
            Assert.AreEqual(true, parsed, "parsed");
        }
        [TestMethod]
        public void TestTryToBigInteger8() {

            IPAddress mask = IPAddress.Parse("255.0.0.0");
            uint uintMask = 0xff000000;

            BigInteger? result = null;
            bool parsed = IPNetwork.TryToBigInteger(mask, out result);

            Assert.AreEqual(uintMask, result, "uint");
            Assert.AreEqual(true, parsed, "parsed");
        }
        [TestMethod]
        public void TestTryToBigInteger0() {

            IPAddress mask = IPAddress.Parse("0.0.0.0");
            uint uintMask = 0x00000000;
            BigInteger? result = null;
            bool parsed = IPNetwork.TryToBigInteger(mask, out result);

            Assert.AreEqual(uintMask, result, "uint");
            Assert.AreEqual(true, parsed, "parsed");
        }
        [TestMethod]
        public void TestTryToBigIntegerANE() {

            BigInteger? result = null;
            bool parsed = IPNetwork.TryToBigInteger(null, out result);

            Assert.AreEqual(null, result, "uint");
            Assert.AreEqual(false, parsed, "parsed");

        }
        [TestMethod]
        public void TestTryToBigIntegerANE3() {
            IPAddress ip = null;
            BigInteger? result = null;
            bool parsed = IPNetwork.TryToBigInteger(ip, out result);

            Assert.AreEqual(null, result, "uint");
            Assert.AreEqual(false, parsed, "parsed");
        }
        [TestMethod]
        public void TestTryToBigIntegerANE2() {

            BigInteger? result = null;
            bool parsed = IPNetwork.TryToBigInteger(IPAddress.IPv6Any, out result);

            Assert.AreEqual(0, result, "result");
            Assert.AreEqual(true, parsed, "parsed");
        }



        #endregion

        #region TryToNetmask
        [TestMethod]
        public void TryToNetmask1() {
            IPAddress result = null;
            bool parsed = IPNetwork.TryToNetmask(0, Sockets.AddressFamily.InterNetwork, out result);
            IPAddress expected = IPAddress.Parse("0.0.0.0");

            Assert.AreEqual(expected, result, "Netmask");
            Assert.AreEqual(true, parsed, "parsed");

        }

        [TestMethod]
        public void TryToNetmask2() {
            IPAddress result = null;
            bool parsed = IPNetwork.TryToNetmask(33, Sockets.AddressFamily.InterNetwork, out result);
            IPAddress expected = null;

            Assert.AreEqual(expected, result, "Netmask");
            Assert.AreEqual(false, parsed, "parsed");

        }

        #endregion

        #region ToNetmask

        [TestMethod]
        public void ToNetmask32() {

            byte cidr = 32;
            string netmask = "255.255.255.255";
            string result = IPNetwork.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        [TestMethod]
        public void ToNetmask31() {

            byte cidr = 31;
            string netmask = "255.255.255.254";
            string result = IPNetwork.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        [TestMethod]
        public void ToNetmask30() {

            byte cidr = 30;
            string netmask = "255.255.255.252";
            string result = IPNetwork.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        [TestMethod]
        public void ToNetmask29() {

            byte cidr = 29;
            string netmask = "255.255.255.248";
            string result = IPNetwork.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        [TestMethod]
        public void ToNetmask1() {

            byte cidr = 1;
            string netmask = "128.0.0.0";
            string result = IPNetwork.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }


        [TestMethod]
        public void ToNetmask0() {

            byte cidr = 0;
            string netmask = "0.0.0.0";
            string result = IPNetwork.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();

            Assert.AreEqual(netmask, result, "netmask");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToNetmaskOORE1() {
            byte cidr = 33;
            string result = IPNetwork.ToNetmask(cidr, Sockets.AddressFamily.InterNetwork).ToString();
        }

        #endregion

        #region ValidNetmask

        [TestMethod]
        public void TestValidNetmask0() {

            IPAddress mask = IPAddress.Parse("255.255.255.255");
            bool expected = true;
            bool result = IPNetwork.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

        [TestMethod]
        public void TestValidNetmask1() {

            IPAddress mask = IPAddress.Parse("255.255.255.0");
            bool expected = true;
            bool result = IPNetwork.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

        [TestMethod]
        public void TestValidNetmask2() {

            IPAddress mask = IPAddress.Parse("255.255.0.0");
            bool expected = true;
            bool result = IPNetwork.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }


        [TestMethod]
        public void TestValidNetmaskEAE1() {

            IPAddress mask = IPAddress.Parse("0.255.0.0");
            bool expected = false;
            bool result = IPNetwork.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestValidNetmaskEAE2() {

            IPAddress mask = null;
            bool expected = true;
            bool result = IPNetwork.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }


        [TestMethod]
        public void TestValidNetmaskEAE3() {

            IPAddress mask = IPAddress.Parse("255.255.0.1");
            bool expected = false;
            bool result = IPNetwork.ValidNetmask(mask);

            Assert.AreEqual(expected, result, "ValidNetmask");
        }

        #endregion

        #region BitsSet

        [TestMethod]
        public void TestBitsSet32() {

            IPAddress ip = IPAddress.Parse("255.255.255.255");
            uint bits = 32;
            uint result = IPNetwork.BitsSet(ip);

            Assert.AreEqual(bits, result, "BitsSet");

        }
        [TestMethod]
        public void TestBitsSet24() {

            IPAddress ip = IPAddress.Parse("255.255.255.0");
            uint bits = 24;
            uint result = IPNetwork.BitsSet(ip);

            Assert.AreEqual(bits, result, "BitsSet");

        }
        [TestMethod]
        public void TestBitsSet16() {

            IPAddress ip = IPAddress.Parse("255.255.0.0");
            uint bits = 16;
            uint result = IPNetwork.BitsSet(ip);

            Assert.AreEqual(bits, result, "BitsSet");

        }
        [TestMethod]
        public void TestBitsSet4() {

            IPAddress ip = IPAddress.Parse("128.128.128.128");
            uint bits = 4;
            uint result = IPNetwork.BitsSet(ip);

            Assert.AreEqual(bits, result, "BitsSet");

        }

        #endregion

        #region Contains

        [TestMethod]
        public void TestContains1()
        {

            IPNetwork ipnetwork = IPNetwork.Parse("192.168.0.1/24");
            IPAddress ipaddress = IPAddress.Parse("192.168.0.100");

            bool result = ipnetwork.Contains(ipaddress);
            bool expected = true;

            Assert.AreEqual(expected, result, "contains");

        }

        [TestMethod]
        public void TestContains2()
        {

            IPNetwork ipnetwork = IPNetwork.Parse("192.168.0.1/24");
            IPAddress ipaddress = IPAddress.Parse("10.10.10.10");

            bool result = ipnetwork.Contains(ipaddress);
            bool expected = false;

            Assert.AreEqual(expected, result, "contains");

        }

        [TestMethod]
        public void TestContains3()
        {

            IPNetwork ipnetwork = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork ipnetwork2 = IPNetwork.Parse("192.168.0.1/24");

            bool result = ipnetwork.Contains(ipnetwork2);
            bool expected = true;

            Assert.AreEqual(expected, result, "contains");

        }

        [TestMethod]
        public void TestContains4()
        {

            IPNetwork ipnetwork = IPNetwork.Parse("192.168.0.1/16");
            IPNetwork ipnetwork2 = IPNetwork.Parse("192.168.1.1/24");

            bool result = ipnetwork.Contains(ipnetwork2);
            bool expected = true;

            Assert.AreEqual(expected, result, "contains");

        }

        [TestMethod]
        public void TestContains5()
        {

            IPNetwork ipnetwork = IPNetwork.Parse("192.168.0.1/16");
            IPNetwork ipnetwork2 = IPNetwork.Parse("10.10.10.0/24");

            bool result = ipnetwork.Contains(ipnetwork2);
            bool expected = false;

            Assert.AreEqual(expected, result, "contains");

        }


        [TestMethod]
        public void TestContains6()
        {

            IPNetwork ipnetwork = IPNetwork.Parse("192.168.1.1/24");
            IPNetwork ipnetwork2 = IPNetwork.Parse("192.168.0.0/16");

            bool result = ipnetwork.Contains(ipnetwork2);
            bool expected = false;

            Assert.AreEqual(expected, result, "contains");

        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContains7()
        {

            IPNetwork ipnetwork = null;
            IPNetwork ipnetwork2 = null;

            bool result = ipnetwork.Contains(ipnetwork2);


        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContains8()
        {

            IPNetwork ipnetwork = IPNetwork.Parse("0.0.0.0/0");
            IPNetwork ipnetwork2 = null;

            bool result = ipnetwork.Contains(ipnetwork2);

        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContains9()
        {

            IPNetwork ipnetwork = null;
            IPAddress ipaddress = null;

            bool result = ipnetwork.Contains(ipaddress);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContains10()
        {

            IPNetwork ipnetwork = IPNetwork.Parse("0.0.0.0/0");
            IPAddress ipaddress = null;

            bool result = ipnetwork.Contains(ipaddress);

        }

        #endregion

        #region Overlap

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestOverlap1()
        {
            IPNetwork network1 = null;
            IPNetwork network2 = null;
            network1.Overlap(network2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestOverlap2()
        {
            IPNetwork network1 = IPNetwork.Parse("10.0.0.0/0");
            IPNetwork network2 = null;
            network1.Overlap(network2);
        }

        [TestMethod]
        public void TestOverlap3()
        {
            IPNetwork network1 = IPNetwork.Parse("10.0.0.0/0");
            IPNetwork network2 = IPNetwork.Parse("10.0.0.0/0");
            bool result = network1.Overlap(network2);
            bool expected = true;

            Assert.AreEqual(expected, result, "overlap");
        }

        [TestMethod]
        public void TestOverlap4()
        {
            IPNetwork network1 = IPNetwork.Parse("10.10.0.0/16");
            IPNetwork network2 = IPNetwork.Parse("10.10.1.0/24");
            bool result = network1.Overlap(network2);
            bool expected = true;

            Assert.AreEqual(expected, result, "overlap");
        }

        [TestMethod]
        public void TestOverlap5()
        {
            IPNetwork network1 = IPNetwork.Parse("10.10.0.0/24");
            IPNetwork network2 = IPNetwork.Parse("10.10.1.0/24");
            bool result = network1.Overlap(network2);
            bool expected = false;

            Assert.AreEqual(expected, result, "overlap");
        }

        [TestMethod]
        public void TestOverlap6()
        {
            IPNetwork network1 = IPNetwork.Parse("10.10.1.0/24");
            IPNetwork network2 = IPNetwork.Parse("10.10.0.0/16");
            bool result = network1.Overlap(network2);
            bool expected = true;

            Assert.AreEqual(expected, result, "overlap");
        }

        #endregion

        #region Examples

        public void Example1()
        {
            IPNetwork ipnetwork = IPNetwork.Parse("192.168.168.100/24");

            Console.WriteLine("Network : {0}", ipnetwork.Network);
            Console.WriteLine("Netmask : {0}", ipnetwork.Netmask);
            Console.WriteLine("Broadcast : {0}", ipnetwork.Broadcast);
            Console.WriteLine("FirstUsable : {0}", ipnetwork.FirstUsable);
            Console.WriteLine("LastUsable : {0}", ipnetwork.LastUsable);
            Console.WriteLine("Usable : {0}", ipnetwork.Usable);
            Console.WriteLine("Cidr : {0}", ipnetwork.Cidr);

        }

        public void Example2() {

            IPNetwork ipnetwork = IPNetwork.Parse("192.168.0.0/24");
            IPAddress ipaddress = IPAddress.Parse("192.168.0.100");
            IPAddress ipaddress2 = IPAddress.Parse("192.168.1.100");

            IPNetwork ipnetwork2 = IPNetwork.Parse("192.168.0.128/25");
            IPNetwork ipnetwork3 = IPNetwork.Parse("192.168.1.1/24");

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

        public void Example2b() {

            IPNetwork ipnetwork1 = IPNetwork.Parse("10.1.0.0/16");
            IPNetwork ipnetwork2 = IPNetwork.Parse("192.168.1.0/24");

            IPAddress ipaddress1 = IPAddress.Parse("192.168.1.1");
            IPAddress ipaddress2 = IPAddress.Parse("192.168.2.100");
            IPAddress ipaddress3 = IPAddress.Parse("10.1.2.3");
            IPAddress ipaddress4 = IPAddress.Parse("10.4.5.6");


            bool contains1 = ipnetwork2.Contains(ipaddress1);
            bool contains2 = ipnetwork2.Contains(ipaddress2);
            bool contains3 = ipnetwork1.Contains(ipaddress3);
            bool contains4 = ipnetwork1.Contains(ipaddress4);


            Console.WriteLine("{0} contains {1} : {2}", ipnetwork1, ipaddress1, contains1);
            Console.WriteLine("{0} contains {1} : {2}", ipnetwork1, ipaddress2, contains2);
            Console.WriteLine("{0} contains {1} : {2}", ipnetwork2, ipaddress3, contains3);
            Console.WriteLine("{0} contains {1} : {2}", ipnetwork2, ipaddress4, contains4);


        }

        public void Example3()
        {

            IPNetwork iana_a_block = IPNetwork.IANA_ABLK_RESERVED1;
            IPNetwork iana_b_block = IPNetwork.IANA_BBLK_RESERVED1;
            IPNetwork iana_c_block = IPNetwork.IANA_CBLK_RESERVED1;

            Console.WriteLine("IANA_ABLK_RESERVED1 is {0}", iana_a_block);
            Console.WriteLine("IANA_BBLK_RESERVED1 is {0}", iana_b_block);
            Console.WriteLine("IANA_CBLK_RESERVED1 is {0}", iana_c_block);

        }


        public void Example4()
        {
            IPNetwork wholeInternet = IPNetwork.Parse("0.0.0.0/0");
            byte newCidr = 2;
            IPNetworkCollection subneted = wholeInternet.Subnet(newCidr);

            Console.WriteLine("{0} was subnetted into {1} subnets", wholeInternet, subneted.Count);
            Console.WriteLine("First: {0}", subneted[0]);
            Console.WriteLine("Last : {0}", subneted[subneted.Count - 1]);
            Console.WriteLine("All  :");

            foreach (IPNetwork ipnetwork in subneted)
            {
                Console.WriteLine("{0}", ipnetwork);
            }
        }

        public void Example5()
        {
            IPNetwork ipnetwork1 = IPNetwork.Parse("192.168.0.0/24");
            IPNetwork ipnetwork2 = IPNetwork.Parse("192.168.1.0/24");
            IPNetwork[] ipnetwork3 = IPNetwork.Supernet(new[]{ipnetwork1, ipnetwork2});

            Console.WriteLine("{0} + {1} = {2}", ipnetwork1, ipnetwork2, ipnetwork3[0]);
            
        }



        public void Example7() {

            IPNetwork ipnetwork = IPNetwork.Parse("192.168.168.100/24");

            IPAddress ipaddress = IPAddress.Parse("192.168.168.200");
            IPAddress ipaddress2 = IPAddress.Parse("192.168.0.200");

            bool contains1 = ipnetwork.Contains(ipaddress);
            bool contains2 = ipnetwork.Contains(ipaddress2);

            Console.WriteLine("{0} contains {1} : {2}", ipnetwork, ipaddress, contains1);
            Console.WriteLine("{0} contains {1} : {2}", ipnetwork, ipaddress2, contains2);

        }
        public void Example9() {

            IPNetwork network = IPNetwork.Parse("192.168.0.1");
            IPNetwork network2 = IPNetwork.Parse("192.168.0.254");

            IPNetwork ipnetwork = network.Supernet(network2);

            Console.WriteLine("Network : {0}", ipnetwork.Network);
            Console.WriteLine("Netmask : {0}", ipnetwork.Netmask);
            Console.WriteLine("Broadcast : {0}", ipnetwork.Broadcast);
            Console.WriteLine("FirstUsable : {0}", ipnetwork.FirstUsable);
            Console.WriteLine("LastUsable : {0}", ipnetwork.LastUsable);
            Console.WriteLine("Usable : {0}", ipnetwork.Usable);
            Console.WriteLine("Cidr : {0}", ipnetwork.Cidr);

        }


        public void Example10() {

            IPNetwork ipnetwork = IPNetwork.Parse("192.168.0.1/25");

            Console.WriteLine("Network : {0}", ipnetwork.Network);
            Console.WriteLine("Netmask : {0}", ipnetwork.Netmask);
            Console.WriteLine("Broadcast : {0}", ipnetwork.Broadcast);
            Console.WriteLine("FirstUsable : {0}", ipnetwork.FirstUsable);
            Console.WriteLine("LastUsable : {0}", ipnetwork.LastUsable);
            Console.WriteLine("Usable : {0}", ipnetwork.Usable);
            Console.WriteLine("Cidr : {0}", ipnetwork.Cidr);

        }

        #endregion

        #region IANA blocks
        [TestMethod]
        public void TestIANA1() {

            IPAddress ipaddress = IPAddress.Parse("192.168.66.66");
            bool expected = true;
            bool result = IPNetwork.IsIANAReserved(ipaddress);

            Assert.AreEqual(expected, result, "IANA");

        }

        [TestMethod]
        public void TestIANA2() {

            IPAddress ipaddress = IPAddress.Parse("10.0.0.0");
            bool expected = true;
            bool result = IPNetwork.IsIANAReserved(ipaddress);

            Assert.AreEqual(expected, result, "IANA");

        }

        [TestMethod]
        public void TestIANA3() {

            IPAddress ipaddress = IPAddress.Parse("172.17.10.10");
            bool expected = true;
            bool result = IPNetwork.IsIANAReserved(ipaddress);

            Assert.AreEqual(expected, result, "IANA");

        }

        [TestMethod]
        public void TestIANA4() {

            IPNetwork ipnetwork = IPNetwork.Parse("192.168.66.66/24");
            bool expected = true;
            bool result = ipnetwork.IsIANAReserved();

            Assert.AreEqual(expected, result, "IANA");

        }

        [TestMethod]
        public void TestIANA5() {

            IPNetwork ipnetwork = IPNetwork.Parse("10.10.10/18");
            bool expected = true;
            bool result = ipnetwork.IsIANAReserved();

            Assert.AreEqual(expected, result, "IANA");

        }

        [TestMethod]
        public void TestIANA6() {

            IPNetwork ipnetwork = IPNetwork.Parse("172.31.10.10/24");
            bool expected = true;
            bool result = ipnetwork.IsIANAReserved();

            Assert.AreEqual(expected, result, "IANA");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIANA7() {

            IPAddress ipaddress = null;
            IPNetwork.IsIANAReserved(ipaddress);


        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIANA8() {

            IPNetwork ipnetwork = null;
            ipnetwork.IsIANAReserved();


        }


        [TestMethod]
        public void TestIANA9() {

            IPAddress ipaddress = IPAddress.Parse("1.2.3.4");
            bool expected = false;
            bool result = IPNetwork.IsIANAReserved(ipaddress);

            Assert.AreEqual(expected, result, "IANA");

        }

        [TestMethod]
        public void TestIANA10() {

            IPNetwork ipnetwork = IPNetwork.Parse("172.16.0.0/8");
            bool expected = false;
            bool result = ipnetwork.IsIANAReserved();

            Assert.AreEqual(expected, result, "IANA");

        }


        [TestMethod]
        public void TestIANA11() {

            IPNetwork ipnetwork = IPNetwork.Parse("192.168.15.1/8");
            bool expected = false;
            bool result = ipnetwork.IsIANAReserved();

            Assert.AreEqual(expected, result, "IANA");

        }
        #endregion

        #region ToString


        [TestMethod]
        public void TestToString() {

            IPNetwork ipnetwork = IPNetwork.Parse("192.168.15.1/8");
            string expected = "192.0.0.0/8";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString1() {

            IPNetwork ipnetwork = IPNetwork.Parse("192.168.15.1/9");
            string expected = "192.128.0.0/9";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString2() {

            IPNetwork ipnetwork = IPNetwork.Parse("192.168.15.1/10");
            string expected = "192.128.0.0/10";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString3() {

            IPNetwork ipnetwork = IPNetwork.Parse("192.168.15.1/11");
            string expected = "192.160.0.0/11";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString4() {

            IPNetwork ipnetwork = IPNetwork.Parse("192.168.15.1/12");
            string expected = "192.160.0.0/12";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString5() {

            IPNetwork ipnetwork = IPNetwork.Parse("192.168.15.1/13");
            string expected = "192.168.0.0/13";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString6() {

            IPNetwork ipnetwork = IPNetwork.Parse("192.168.15.1/14");
            string expected = "192.168.0.0/14";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }
        [TestMethod]
        public void TestToString7() {

            IPNetwork ipnetwork = IPNetwork.Parse("192.168.15.1/15");
            string expected = "192.168.0.0/15";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }

        [TestMethod]
        public void TestToString8() {

            IPNetwork ipnetwork = IPNetwork.Parse("192.168.15.1/16");
            string expected = "192.168.0.0/16";
            string result = ipnetwork.ToString();

            Assert.AreEqual(expected, result, "ToString");
        }
        
        #endregion

        #region Subnet

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSubnet1() {

            IPNetwork ipnetwork = null;
            byte cidr = 9;

            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestSubnet3() {

            IPNetwork ipnetwork = IPNetwork.IANA_ABLK_RESERVED1;
            byte cidr = 55;

            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);

        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSubnet4() {

            IPNetwork ipnetwork = IPNetwork.IANA_ABLK_RESERVED1;
            byte cidr = 1;

            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);

        }


        [TestMethod]
        public void TestSubnet5() {

            IPNetwork ipnetwork = IPNetwork.IANA_ABLK_RESERVED1;
            byte cidr = 9;

            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            Assert.AreEqual(2, subnets.Count, "count");
            Assert.AreEqual("10.0.0.0/9", subnets[0].ToString(), "subnet1");
            Assert.AreEqual("10.128.0.0/9", subnets[1].ToString(), "subnet2");

        }


        [TestMethod]
        public void TestSubnet6() {

            IPNetwork ipnetwork = IPNetwork.IANA_CBLK_RESERVED1;
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
        public void TestSubnet7() {

            IPNetwork ipnetwork = IPNetwork.IANA_CBLK_RESERVED1;
            byte cidr = 24;

            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            Assert.AreEqual(256, subnets.Count, "count");
            Assert.AreEqual("192.168.0.0/24", subnets[0].ToString(), "subnet1");
            Assert.AreEqual("192.168.255.0/24", subnets[255].ToString(), "subnet16");

        }


        [TestMethod]
        public void TestSubnet8() {

            IPNetwork ipnetwork = IPNetwork.IANA_CBLK_RESERVED1;
            byte cidr = 24;

            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            Assert.AreEqual(256, subnets.Count, "count");
            Assert.AreEqual("192.168.0.0/24", subnets[0].ToString(), "subnet1");
            Assert.AreEqual("192.168.255.0/24", subnets[255].ToString(), "subnet256");

        }

        [TestMethod]
        public void TestSubnet9() {

            IPNetwork ipnetwork = IPNetwork.Parse("192.168.0.0/24");
            byte cidr = 32;

            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            Assert.AreEqual(256, subnets.Count, "count");
            Assert.AreEqual("192.168.0.0/32", subnets[0].ToString(), "subnet1");
            Assert.AreEqual("192.168.0.255/32", subnets[255].ToString(), "subnet256");

        }


        [TestMethod]
        public void TestSubnet10()
        {
            IPNetwork ipnetwork = IPNetwork.Parse("0.0.0.0/0");
            byte cidr = 32;

            // Here I spawm a OOM dragon ! beware of the beast !
            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            Assert.AreEqual(4294967296, subnets.Count, "count");
            Assert.AreEqual("0.0.0.0/32", subnets[0].ToString(), "subnet1");
            Assert.AreEqual("255.255.255.255/32", subnets[4294967295].ToString(), "subnet256");

        }


        [TestMethod]
        public void TestSubnet12() {

            IPNetwork ipnetwork = IPNetwork.IANA_CBLK_RESERVED1;
            byte cidr = 20;
            int i = -1;
            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            foreach (IPNetwork ipn in subnets) {
                i++;
                Assert.AreEqual(subnets[i], ipn, "subnet");
            }

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestSubnet13() {

            IPNetwork ipnetwork = IPNetwork.IANA_CBLK_RESERVED1;
            byte cidr = 20;
            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            IPNetwork error = subnets[1000];

        }

        #endregion

        #region TrySubnet


        [TestMethod]
        public void TestTrySubnet1() {

            IPNetwork ipnetwork = null;
            byte cidr = 9;

            IPNetworkCollection subnets = null;
            bool subnetted = IPNetwork.TrySubnet(ipnetwork, cidr, out subnets);

            Assert.AreEqual(false, subnetted, "subnetted");

        }

        [TestMethod]
        public void TestTrySubnet3() {

            IPNetwork ipnetwork = IPNetwork.IANA_ABLK_RESERVED1;
            byte cidr = 55;

            IPNetworkCollection subnets = null;
            bool subnetted = IPNetwork.TrySubnet(ipnetwork, cidr, out subnets);

            Assert.AreEqual(false, subnetted, "subnetted");
        }


        [TestMethod]
        public void TestTrySubnet4() {

            IPNetwork ipnetwork = IPNetwork.IANA_ABLK_RESERVED1;
            byte cidr = 1;

            IPNetworkCollection subnets = null;
            bool subnetted = IPNetwork.TrySubnet(ipnetwork, cidr, out subnets);

            Assert.AreEqual(false, subnetted, "subnetted");

        }


        [TestMethod]
        public void TestTrySubnet5() {

            IPNetwork ipnetwork = IPNetwork.IANA_ABLK_RESERVED1;
            byte cidr = 9;


            IPNetworkCollection subnets = null;
            bool subnetted = IPNetwork.TrySubnet(ipnetwork, cidr, out subnets);

            Assert.AreEqual(true, subnetted, "subnetted");
            Assert.AreEqual(2, subnets.Count, "count");
            Assert.AreEqual("10.0.0.0/9", subnets[0].ToString(), "subnet1");
            Assert.AreEqual("10.128.0.0/9", subnets[1].ToString(), "subnet2");

        }


        [TestMethod]
        public void TestTrySubnet6() {

            IPNetwork ipnetwork = IPNetwork.IANA_CBLK_RESERVED1;
            byte cidr = 20;

            IPNetworkCollection subnets = null;
            bool subnetted = IPNetwork.TrySubnet(ipnetwork, cidr, out subnets);

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
        public void TestSupernet1() {

            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.1.1/24");
            IPNetwork expected = IPNetwork.Parse("192.168.0.0/23");
            IPNetwork supernet = network1.Supernet(network2);

            Assert.AreEqual(expected, supernet, "supernet");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSupernet2() {

            IPNetwork network1 = null;
            IPNetwork network2 = IPNetwork.Parse("192.168.1.1/24");
            IPNetwork supernet = IPNetwork.Supernet(network1, network2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSupernet3() {

            IPNetwork network1 = IPNetwork.Parse("192.168.1.1/24");
            IPNetwork network2 = null;
            IPNetwork supernet = network1.Supernet(network2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSupernet4() {

            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.1.1/25");
            IPNetwork supernet = network1.Supernet(network2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestSupernet5() {

            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.5.1/24");
            IPNetwork supernet = network1.Supernet(network2);
        }

        [TestMethod]
        public void TestSupernet6() {

            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.0.1/25");
            IPNetwork expected = IPNetwork.Parse("192.168.0.0/24");
            IPNetwork supernet = network1.Supernet(network2);

            Assert.AreEqual(expected, supernet, "supernet");

        }

        [TestMethod]
        public void TestSupernet7() {

            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/25");
            IPNetwork network2 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork expected = IPNetwork.Parse("192.168.0.0/24");
            IPNetwork supernet = network1.Supernet(network2);

            Assert.AreEqual(expected, supernet, "supernet");

        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSupernet8() {

            IPNetwork network1 = IPNetwork.Parse("192.168.1.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.2.1/24");
            IPNetwork supernet = network1.Supernet(network2);


        }


        [TestMethod]
        public void TestSupernet9()
        {

            IPNetwork ipnetwork1 = IPNetwork.Parse("200.16.0.0/24");
            IPNetwork ipnetwork2 = IPNetwork.Parse("200.16.1.0/24");
            IPNetwork ipnetwork3 = IPNetwork.Parse("200.16.2.0/24");
            IPNetwork ipnetwork4 = IPNetwork.Parse("200.16.3.0/24");

            IPNetwork result = IPNetwork.Supernet(new[] { ipnetwork1, ipnetwork2, ipnetwork3, ipnetwork4 })[0];
            IPNetwork expected = IPNetwork.Parse("200.16.0.0/22");

            Assert.AreEqual(expected, result, "supernet");

        }

        #endregion

        #region TrySupernet


        [TestMethod]
        public void TestTrySupernet1() {

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
        public void TestTrySupernet2() {

            IPNetwork network1 = null;
            IPNetwork network2 = IPNetwork.Parse("192.168.1.1/24");
            IPNetwork supernetExpected = null;
            IPNetwork supernet;
            bool parsed = false;
            bool result = network1.TrySupernet(network2, out supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

        [TestMethod]
        public void TestTrySupernet3() {

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
        public void TestTrySupernet4() {

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
        public void TestTrySupernet5() {

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
        public void TestTrySupernet6() {

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
        public void TestTrySupernet7() {

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
        public void TestTrySupernet8() {

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
        public void TestTrySupernet9() {

            IPNetwork network1 = IPNetwork.Parse("192.168.1.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.2.1/24");
            IPNetwork[] network3 = new [] { network1, network2 };
            IPNetwork[] supernetExpected = new [] { network1, network2 };
            IPNetwork[] supernet;
            bool parsed = true;
            bool result = IPNetwork.TrySupernet(network3, out supernet);

            Assert.AreEqual(supernetExpected[0], supernet[0], "supernet");
            Assert.AreEqual(supernetExpected[1], supernet[1], "supernet");
            Assert.AreEqual(parsed, result, "parsed");

        }


        [TestMethod]
        public void TestTrySupernet10() {

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
        public void TestTrySupernet11() {

            IPNetwork[] network3 = null;
            IPNetwork[] supernetExpected = new[] { IPNetwork.Parse("192.168.0.0/23") };
            IPNetwork[] supernet;
            bool parsed = false;
            bool result = IPNetwork.TrySupernet(network3, out supernet);

            Assert.AreEqual(null, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");

        }


        #endregion

        #region SupernetArray

        [TestMethod]
        public void TestTrySupernetArray()
        {

            IPNetwork ipnetwork1 = IPNetwork.Parse("192.168.0.0/24");
            IPNetwork ipnetwork2 = IPNetwork.Parse("192.168.1.0/24");
            IPNetwork ipnetwork3 = IPNetwork.Parse("192.168.2.0/24");
            IPNetwork ipnetwork4 = IPNetwork.Parse("192.168.3.0/24");

            IPNetwork[] ipnetworks = { ipnetwork1, ipnetwork2, ipnetwork3, ipnetwork4 };
            IPNetwork[] expected = { IPNetwork.Parse("192.168.0.0/22") };

            IPNetwork[] result = IPNetwork.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], expected[0], "suppernet");

        }

        [TestMethod]
        public void TestTrySupernetArray1()
        {


            IPNetwork[] ipnetworks = { };
            IPNetwork[] expected = { };

            IPNetwork[] result = IPNetwork.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestTrySupernetArray2()
        {


            IPNetwork[] ipnetworks = null;
            IPNetwork[] result = IPNetwork.Supernet(ipnetworks);

        }


        [TestMethod]
        public void TestTrySupernetArray3()
        {

            IPNetwork ipnetwork1 = null;
            IPNetwork ipnetwork2 = null;
            IPNetwork ipnetwork3 = null;
            IPNetwork ipnetwork4 = null;

            IPNetwork[] ipnetworks = { ipnetwork1, ipnetwork2, ipnetwork3, ipnetwork4 };
            IPNetwork[] expected = { };

            IPNetwork[] result = IPNetwork.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");

        }

        [TestMethod]
        public void TestTrySupernetArray4()
        {

            IPNetwork ipnetwork1 = IPNetwork.Parse("192.168.0.0/24");
            IPNetworkCollection subnetted = ipnetwork1.Subnet(32);
            IPNetwork[] ipnetworks = subnetted.ToArray();
            Assert.AreEqual(256, ipnetworks.Length, "subnet");

            IPNetwork[] expected = { IPNetwork.Parse("192.168.0.0/24") };

            IPNetwork[] result = IPNetwork.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], ipnetwork1, "suppernet");
        }


        [TestMethod]
        public void TestTrySupernetArray5()
        {

            IPNetwork ipnetwork1 = IPNetwork.Parse("192.168.0.0/16");
            IPNetworkCollection subnetted = ipnetwork1.Subnet(32);
            IPNetwork[] ipnetworks = subnetted.ToArray();
            Assert.AreEqual(65536, ipnetworks.Length, "subnet");

            IPNetwork[] expected = { IPNetwork.Parse("192.168.0.0/16") };

            IPNetwork[] result = IPNetwork.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], ipnetwork1, "suppernet");
        }

        public void TestTrySupernetArray6()
        {

            IPNetwork ipnetwork1 = IPNetwork.Parse("192.168.0.0/8");
            IPNetworkCollection subnetted = ipnetwork1.Subnet(32);
            IPNetwork[] ipnetworks = subnetted.ToArray();
            Assert.AreEqual(16777216, ipnetworks.Length, "subnet");

            IPNetwork[] expected = { IPNetwork.Parse("192.0.0.0/8") };

            IPNetwork[] result = IPNetwork.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], ipnetwork1, "suppernet");
        }

        [TestMethod]
        public void TestTrySupernetArray7()
        {

            IPNetwork[] ipnetworks = { 
                IPNetwork.Parse("10.0.2.2/24"),
                IPNetwork.Parse("192.168.0.0/24"),
                IPNetwork.Parse("192.168.1.0/24"),
                IPNetwork.Parse("192.168.2.0/24"),
                IPNetwork.Parse("10.0.1.1/24"),
                IPNetwork.Parse("192.168.3.0/24")
            };

            IPNetwork[] expected = { 
                IPNetwork.Parse("10.0.1.0/24"),
                IPNetwork.Parse("10.0.2.0/24"),
                IPNetwork.Parse("192.168.0/22")
            };

            IPNetwork[] result = IPNetwork.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], result[0], "suppernet");
            Assert.AreEqual(expected[1], result[1], "suppernet1");
            Assert.AreEqual(expected[2], result[2], "suppernet2");
        }

        [TestMethod]
        public void TestTrySupernetArray8()
        {

            IPNetwork[] ipnetworks = { 
                IPNetwork.Parse("10.0.2.2/24"),
                IPNetwork.Parse("192.168.0.0/24"),
                IPNetwork.Parse("192.168.1.0/24"),
                IPNetwork.Parse("192.168.2.0/24"),
                IPNetwork.Parse("10.0.1.1/24"),
                IPNetwork.Parse("192.168.3.0/24"),
                IPNetwork.Parse("10.6.6.6/8")
                
            };

            IPNetwork[] expected = { 
                IPNetwork.Parse("10.0.0.0/8"),
                IPNetwork.Parse("192.168.0/22")
            };

            IPNetwork[] result = IPNetwork.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], result[0], "suppernet");
            Assert.AreEqual(expected[1], result[1], "suppernet1");
        }


        [TestMethod]
        public void TestTrySupernetArray9()
        {

            IPNetwork[] ipnetworks = { 
                IPNetwork.Parse("10.0.2.2/24"),
                IPNetwork.Parse("192.168.0.0/24"),
                IPNetwork.Parse("192.168.1.0/24"),
                IPNetwork.Parse("192.168.2.0/24"),
                IPNetwork.Parse("10.0.1.1/24"),
                IPNetwork.Parse("192.168.3.0/24"),
                IPNetwork.Parse("10.6.6.6/8"),
                IPNetwork.Parse("11.6.6.6/8"),
                IPNetwork.Parse("12.6.6.6/8"),
                
            };

            IPNetwork[] expected = { 
                IPNetwork.Parse("10.0.0.0/7"),
                IPNetwork.Parse("12.0.0.0/8"),
                IPNetwork.Parse("192.168.0/22")
            };

            IPNetwork[] result = IPNetwork.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], result[0], "suppernet");
            Assert.AreEqual(expected[1], result[1], "suppernet1");
            Assert.AreEqual(expected[2], result[2], "suppernet2");
        }


        #endregion

        #region Equals

        [TestMethod]
        public void TestEquals1() {

            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.0.1/24");
            bool result = network1.Equals(network2);
            bool expected = true;

            Assert.AreEqual(expected, result, "equals");

        }

        [TestMethod]
        public void TestEquals2() {

            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork network2 = null;
            bool result = network1.Equals(network2);
            bool expected = false;

            Assert.AreEqual(expected, result, "equals");

        }

        [TestMethod]
        public void TestEquals3() {

            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            object network2 = "";
            bool result = network1.Equals(network2);
            bool expected = false;

            Assert.AreEqual(expected, result, "equals");

        }

        [TestMethod]
        public void TestEquals4() {

            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.0.1/25");
            bool result = network1.Equals(network2);
            bool expected = false;

            Assert.AreEqual(expected, result, "equals");

        }


        [TestMethod]
        public void TestEquals5() {

            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.1.1/24");
            bool result = network1.Equals(network2);
            bool expected = false;

            Assert.AreEqual(expected, result, "equals");

        }

        #endregion

        #region WideSubnet

        [TestMethod]
        public void TestWideSubnet1() {

            string start = "192.168.168.0";
            string end = "192.168.168.255";
            IPNetwork expected = IPNetwork.Parse("192.168.168.0/24");

            IPNetwork wideSubnet = IPNetwork.WideSubnet(start, end);
            Assert.AreEqual(expected, wideSubnet, "wideSubnet");



        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestWideSubnet2() {

            string start = null;
            string end = "192.168.168.255";

            IPNetwork wideSubnet = IPNetwork.WideSubnet(start, end);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestWideSubnet3() {

            string start = "192.168.168.255";
            string end = null;

            IPNetwork wideSubnet = IPNetwork.WideSubnet(start, end);

        }

        [TestMethod]
        public void TestWideSubnet4() {

            string start = "192.168.168.255";
            string end = "192.168.168.0";


            IPNetwork expected = IPNetwork.Parse("192.168.168.0/24");

            IPNetwork wideSubnet = IPNetwork.WideSubnet(start, end);
            Assert.AreEqual(expected, wideSubnet, "wideSubnet");
        }


        [TestMethod]
        public void TestWideSubnet7() {

            string start = "0.0.0.0";
            string end = "0.255.255.255";

            IPNetwork expected = IPNetwork.Parse("0.0.0.0/8");

            IPNetwork wideSubnet = IPNetwork.WideSubnet(start, end);
            Assert.AreEqual(expected, wideSubnet, "wideSubnet");
        }



        [TestMethod]
        public void TestWideSubnet8() {

            string start = "1.2.3.4";
            string end = "5.6.7.8";

            IPNetwork expected = IPNetwork.Parse("0.0.0.0/5");

            IPNetwork wideSubnet = IPNetwork.WideSubnet(start, end);
            Assert.AreEqual(expected, wideSubnet, "wideSubnet");
        }


        [TestMethod]
        public void TestWideSubnet9()
        {

            string start = "200.16.0.0/24";
            string end = "200.16.3.0/24";
            var firt = IPNetwork.Parse(start).FirstUsable.ToString();
            var last = IPNetwork.Parse(end).LastUsable.ToString();

            IPNetwork expected = IPNetwork.Parse("200.16.0.0/22");

            IPNetwork wideSubnet = IPNetwork.WideSubnet(firt, last);
            Assert.AreEqual(expected, wideSubnet, "wideSubnet");



        }


        [TestMethod]
        public void TestWideSubnet10()
        {

            string start = "200.16.0.0";
            string end = "200.16.3.255";

            IPNetwork expected = IPNetwork.Parse("200.16.0.0/22");

            IPNetwork wideSubnet = IPNetwork.WideSubnet(start, end);
            Assert.AreEqual(expected, wideSubnet, "wideSubnet");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWideSubnetInvalid1() {

            string start = "invalid";
            string end = "5.6.7.8";

            IPNetwork wideSubnet = IPNetwork.WideSubnet(start, end);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWideSubnetInvalid2() {

            string start = "1.2.3.4";
            string end = "invalid";

            IPNetwork wideSubnet = IPNetwork.WideSubnet(start, end);
        }

        #endregion

        #region TryGuessCidr

        [TestMethod]
        public void TestTryGuessCidrNull() {

            byte cidr;
            bool parsed = IPNetwork.TryGuessCidr(null, out cidr);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(0, cidr, "cidr");
        }

        [TestMethod]
        public void TestTryGuessCidrA() {

            byte cidr;
            bool parsed = IPNetwork.TryGuessCidr("10.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(8, cidr, "cidr");
        }

        [TestMethod]
        public void TestTryGuessCidrB() {

            byte cidr;
            bool parsed = IPNetwork.TryGuessCidr("172.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(16, cidr, "cidr");
        }

        [TestMethod]
        public void TestTryGuessCidrC() {

            byte cidr;
            bool parsed = IPNetwork.TryGuessCidr("192.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(24, cidr, "cidr");
        }

        [TestMethod]
        public void TestTryGuessCidrD() {

            byte cidr;
            bool parsed = IPNetwork.TryGuessCidr("224.0.0.0", out cidr);

            Assert.AreEqual(false, parsed, "parsed");
        }
        [TestMethod]
        public void TestTryGuessCidrE() {

            byte cidr;
            bool parsed = IPNetwork.TryGuessCidr("240.0.0.0", out cidr);

            Assert.AreEqual(false, parsed, "parsed");
        }

        #endregion

        #region GetHashCode

        [TestMethod]
        public void TestGetHashCode1() {

            IPNetwork ipnetwork1 = IPNetwork.Parse("0.0.1.1/0");
            IPNetwork ipnetwork2 = IPNetwork.Parse("0.0.1.1/0");
            int hashCode1 = ipnetwork1.GetHashCode();
            int hashCode2 = ipnetwork2.GetHashCode();
            Assert.AreEqual(hashCode1, hashCode2, "hashcode");

        }

        [TestMethod]
        public void TestGetHashCode2() {

            IPNetwork ipnetwork1 = IPNetwork.Parse("0.0.0.0/1");
            IPNetwork ipnetwork2 = IPNetwork.Parse("0.0.0.0/1");
            int hashCode1 = ipnetwork1.GetHashCode();
            int hashCode2 = ipnetwork2.GetHashCode();
            Assert.AreEqual(hashCode1, hashCode2, "hashcode");

        }
        [TestMethod]
        public void TestGetHashCode3() {

            IPNetwork ipnetwork1 = IPNetwork.Parse("0.0.0.0/32");
            IPNetwork ipnetwork2 = IPNetwork.Parse("0.0.0.0/32");
            int hashCode1 = ipnetwork1.GetHashCode();
            int hashCode2 = ipnetwork2.GetHashCode();
            Assert.AreEqual(hashCode1, hashCode2, "hashcode");

        }

        #endregion

        #region Print

        [TestMethod]
        public void Print() {
            IPNetwork ipn = IPNetwork.Parse("0.0.0.0/0");
            string print = IPNetwork.Print(ipn).Replace("\r", "");
            string expected = @"IPNetwork   : 0.0.0.0/0
Network     : 0.0.0.0
Netmask     : 0.0.0.0
Cidr        : 0
Broadcast   : 255.255.255.255
FirstUsable : 0.0.0.1
LastUsable  : 255.255.255.254
Usable      : 4294967294
".Replace("\r", "");

            Assert.AreEqual(expected, print, "Print");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PrintNull() {
            IPNetwork ipn = null;
            string print = IPNetwork.Print(ipn);
        }

        #endregion

        #region Sort

        [TestMethod]
        public void TestSort1() {
            string[] ips = new[] { "1.1.1.1", "255.255.255.255", "2.2.2.2", "0.0.0.0" };
            List<IPNetwork> ipns = new List<IPNetwork>();
            foreach(string ip in ips) {
                IPNetwork ipn;
                if (IPNetwork.TryParse(ip, 32, out ipn)) {
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
        public void TestSort2() {
            string[] ips = new[] { "0.0.0.100/32", "0.0.0.0/24" };
            List<IPNetwork> ipns = new List<IPNetwork>();
            foreach (string ip in ips)
            {
                IPNetwork ipn;
                if (IPNetwork.TryParse(ip, out ipn)) {
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
        public void TryWideSubnet1() {
            string[] ips = new[] { "1.1.1.1", "255.255.255.255", "2.2.2.2", "0.0.0.0" };
            List<IPNetwork> ipns = new List<IPNetwork>();
            foreach (string ip in ips)
            {
                IPNetwork ipn;
                if (IPNetwork.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            IPNetwork ipnetwork = null;
            bool wide = IPNetwork.TryWideSubnet(ipns.ToArray(), out ipnetwork);
            Assert.AreEqual(true, wide, "wide");
            Assert.AreEqual("0.0.0.0/0", ipnetwork.ToString(), "ipnetwork");

        }

        [TestMethod]
        public void TryWideSubnet2() {
            string[] ips = new[] { "1.1.1.1", "10.0.0.0", "2.2.2.2", "0.0.0.0" };
            List<IPNetwork> ipns = new List<IPNetwork>();
            foreach (string ip in ips)
            {
                IPNetwork ipn;
                if (IPNetwork.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            IPNetwork ipnetwork = null;
            bool wide = IPNetwork.TryWideSubnet(ipns.ToArray(), out ipnetwork);
            Assert.AreEqual(true, wide, "wide");
            Assert.AreEqual("0.0.0.0/4", ipnetwork.ToString(), "ipnetwork");

        }


        [TestMethod]
        public void TryWideSubnet3() {
            string[] ips = new[] { "a", "b", "c", "d" };
            List<IPNetwork> ipns = new List<IPNetwork>();
            foreach (string ip in ips)
            {
                IPNetwork ipn;
                if (IPNetwork.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            IPNetwork ipnetwork = null;
            bool wide = IPNetwork.TryWideSubnet(ipns.ToArray(), out ipnetwork);
            Assert.AreEqual(false, wide, "wide");

        }

        [TestMethod]
        public void TryWideSubnet4() {
            string[] ips = new[] { "a", "b", "1.1.1.1", "d" };
            List<IPNetwork> ipns = new List<IPNetwork>();
            foreach (string ip in ips)
            {
                IPNetwork ipn;
                if (IPNetwork.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            IPNetwork ipnetwork = null;
            bool wide = IPNetwork.TryWideSubnet(ipns.ToArray(), out ipnetwork);
            Assert.AreEqual(true, wide, "wide");
            Assert.AreEqual("1.1.1.1/32", ipnetwork.ToString(), "ipnetwork");

        }

        [TestMethod]
        public void TryWideSubnetNull() {

            IPNetwork ipnetwork = null;
            bool wide = IPNetwork.TryWideSubnet(null, out ipnetwork);
            Assert.AreEqual(false, wide, "wide");

        }

        #endregion

        #region WideSubnet

        [TestMethod]
        public void WideSubnet1() {
            string[] ips = new[] { "1.1.1.1", "255.255.255.255", "2.2.2.2", "0.0.0.0" };
            List<IPNetwork> ipns = new List<IPNetwork>();
            foreach (string ip in ips)
            {
                IPNetwork ipn;
                if (IPNetwork.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            IPNetwork ipnetwork = IPNetwork.WideSubnet(ipns.ToArray());
            Assert.AreEqual("0.0.0.0/0", ipnetwork.ToString(), "ipnetwork");

        }

        [TestMethod]
        public void WideSubnet2() {
            string[] ips = new[] { "1.1.1.1", "10.0.0.0", "2.2.2.2", "0.0.0.0" };
            List<IPNetwork> ipns = new List<IPNetwork>();
            foreach (string ip in ips)
            {
                IPNetwork ipn;
                if (IPNetwork.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            IPNetwork ipnetwork = IPNetwork.WideSubnet(ipns.ToArray());
            Assert.AreEqual("0.0.0.0/4", ipnetwork.ToString(), "ipnetwork");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WideSubnetNull() {

            IPNetwork ipnetwork = IPNetwork.WideSubnet(null);


        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WideSubnetNull2() {

            string[] ips = new[] { "a", "b", "e", "d" };
            List<IPNetwork> ipns = new List<IPNetwork>();
            foreach (string ip in ips)
            {
                IPNetwork ipn;
                if (IPNetwork.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            IPNetwork ipnetwork = IPNetwork.WideSubnet(ipns.ToArray());


        }

        #endregion

        /**
         * 
        #region TrySubstractNetwork


        [TestMethod]
        public void TrySubstractNetwork1() {
            string[] ips = new[] { "178.82.0.0/16" };
            string substract = "178.82.131.209/32";

            List<IPNetwork> ipns = new List<IPNetwork>();
            Array.ForEach<string>(ips, new Action<string>(
                delegate(string ip)
                {
                    IPNetwork ipn;
                    if (IPNetwork.TryParse(ip, out ipn)) {
                        ipns.Add(ipn);
                    }
                }
            ));

            var nsubstract = IPNetwork.Parse(substract);

            IEnumerable<IPNetwork> result;
            bool substracted = IPNetwork.TrySubstractNetwork(ipns.ToArray(), nsubstract, out result);
            Assert.AreEqual(true, substracted, "substracted");

        }

        [TestMethod]
        public void TrySubstractNetwork2() {
            string[] ips = new[] { "0.0.0.0/0" };
            string substract = "1.1.1.1/32";

            List<IPNetwork> ipns = new List<IPNetwork>();
            Array.ForEach<string>(ips, new Action<string>(
                delegate(string ip)
                {
                    IPNetwork ipn;
                    if (IPNetwork.TryParse(ip, out ipn)) {
                        ipns.Add(ipn);
                    }
                }
            ));

            var nsubstract = IPNetwork.Parse(substract);

            IEnumerable<IPNetwork> result;
            bool substracted = IPNetwork.TrySubstractNetwork(ipns.ToArray(), nsubstract, out result);
            Assert.AreEqual(true, substracted, "substracted");

        }



        #endregion
        **/

        #region Count

        [TestMethod]
        public void Total32() {
            var network = IPNetwork.Parse("0.0.0.0/32");
            var total = 1;
            Assert.AreEqual(total, network.Total, "Total");
        }

        [TestMethod]
        public void Total31() {
            var network = IPNetwork.Parse("0.0.0.0/31");
            var total = 2;
            Assert.AreEqual(total, network.Total, "Total");
        }

        [TestMethod]
        public void Total30() {
            var network = IPNetwork.Parse("0.0.0.0/30");
            var total = 4;
            Assert.AreEqual(total, network.Total, "Total");
        }

        [TestMethod]
        public void Total24() {
            var network = IPNetwork.Parse("0.0.0.0/24");
            var total = 256;
            Assert.AreEqual(total, network.Total, "Total");
        }

        [TestMethod]
        public void Total16() {
            var network = IPNetwork.Parse("0.0.0.0/16");
            var total = 65536;
            Assert.AreEqual(total, network.Total, "Total");
        }

        [TestMethod]
        public void Total8() {
            var network = IPNetwork.Parse("0.0.0.0/8");
            var total = 16777216;
            Assert.AreEqual(total, network.Total, "Total");
        }

        [TestMethod]
        public void Total0() {
            var network = IPNetwork.Parse("0.0.0.0/0");
            var total = 4294967296;
            Assert.AreEqual(total, network.Total, "Total");
        }

        #endregion

        #region Usable

        [TestMethod]
        public void Usable32() {
            var network = IPNetwork.Parse("0.0.0.0/32");
            uint usable = 0;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        [TestMethod]
        public void Usable31() {
            var network = IPNetwork.Parse("0.0.0.0/31");
            uint usable = 0;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        [TestMethod]
        public void Usable30() {
            var network = IPNetwork.Parse("0.0.0.0/30");
            uint usable = 2;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        [TestMethod]
        public void Usable24() {
            var network = IPNetwork.Parse("0.0.0.0/24");
            uint usable = 254;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        [TestMethod]
        public void Usable16() {
            var network = IPNetwork.Parse("0.0.0.0/16");
            uint usable = 65534;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        [TestMethod]
        public void Usable8() {
            var network = IPNetwork.Parse("0.0.0.0/8");
            uint usable = 16777214;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        [TestMethod]
        public void Usable0() {
            var network = IPNetwork.Parse("0.0.0.0/0");
            uint usable = 4294967294;
            Assert.AreEqual(usable, network.Usable, "Usable");
        }

        #endregion

        #region TryParseCidr

        [TestMethod]
        public void TryParseCidr1() {

            string sidr = "0";
            byte? cidr;
            byte? result = 0;
            bool parsed = IPNetwork.TryParseCidr(sidr, Sockets.AddressFamily.InterNetwork, out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(result, cidr, "cidr");

        }

        [TestMethod]
        public void TryParseCidr2() {

            string sidr = "sadsd";
            byte? cidr;
            byte? result = null;

            bool parsed = IPNetwork.TryParseCidr(sidr, Sockets.AddressFamily.InterNetwork, out cidr);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(result, cidr, "cidr");

        }

        [TestMethod]
        public void TryParseCidr3() {

            string sidr = "33";
            byte? cidr;
            byte? result = null;

            bool parsed = IPNetwork.TryParseCidr(sidr, Sockets.AddressFamily.InterNetwork, out cidr);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(result, cidr, "cidr");

        }

        #endregion
    }
}
