using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Net.TestProject
{
    [TestClass]
    public class WildcardMaskUnitTest
    {
        #region ipv4
        [TestMethod]
        public void Test_WildcardMask_ipv4_mask_0()
        {
            IPNetwork ipnetwork = IPNetwork.Parse("1.1.1.1/0");

            var netmask = ipnetwork.Netmask.ToString();
            var wildcardmask = ipnetwork.WildcardMask.ToString();

            Assert.AreEqual("0.0.0.0", netmask, "netmask");
            Assert.AreEqual("255.255.255.255", wildcardmask, "wildcardmask");
        }

        [TestMethod]
        public void Test_WildcardMask_ipv4_mask_32()
        {
            IPNetwork ipnetwork = IPNetwork.Parse("1.1.1.1/32");

            var netmask = ipnetwork.Netmask.ToString();
            var wildcardmask = ipnetwork.WildcardMask.ToString();

            Assert.AreEqual("255.255.255.255", netmask, "netmask");
            Assert.AreEqual("0.0.0.0", wildcardmask, "wildcardmask");
        }

        [DataTestMethod]
        [DataRow("255.255.255.255", "0.0.0.0")]
        [DataRow("255.255.255.254", "0.0.0.1")]
        [DataRow("255.255.255.252", "0.0.0.3")]
        [DataRow("255.255.255.248", "0.0.0.7")]
        [DataRow("255.255.255.240", "0.0.0.15")]
        [DataRow("255.255.255.224", "0.0.0.31")]
        [DataRow("255.255.255.192", "0.0.0.63")]
        [DataRow("255.255.255.128", "0.0.0.127")]
        [DataRow("255.255.255.0", "0.0.0.255")]
        [DataRow("255.255.254.0", "0.0.1.255")]
        [DataRow("255.255.252.0", "0.0.3.255")]
        [DataRow("255.255.248.0", "0.0.7.255")]
        [DataRow("255.255.240.0", "0.0.15.255")]
        [DataRow("255.255.224.0", "0.0.31.255")]
        [DataRow("255.255.192.0", "0.0.63.255")]
        [DataRow("255.255.128.0", "0.0.127.255")]
        [DataRow("255.255.0.0", "0.0.255.255")]
        [DataRow("255.254.0.0", "0.1.255.255")]
        [DataRow("255.252.0.0", "0.3.255.255")]
        [DataRow("255.248.0.0", "0.7.255.255")]
        [DataRow("255.240.0.0", "0.15.255.255")]
        [DataRow("255.224.0.0", "0.31.255.255")]
        [DataRow("255.192.0.0", "0.63.255.255")]
        [DataRow("255.128.0.0", "0.127.255.255")]
        [DataRow("255.0.0.0", "0.255.255.255")]
        [DataRow("254.0.0.0", "1.255.255.255")]
        [DataRow("252.0.0.0", "3.255.255.255")]
        [DataRow("248.0.0.0", "7.255.255.255")]
        [DataRow("240.0.0.0", "15.255.255.255")]
        [DataRow("224.0.0.0", "31.255.255.255")]
        [DataRow("192.0.0.0", "63.255.255.255")]
        [DataRow("128.0.0.0", "127.255.255.255")]
        [DataRow("0.0.0.0", "255.255.255.255")]
        public void Test_WildcardMask_ipv4(string netmask, string expected)
        {
            IPNetwork ipnetwork = IPNetwork.Parse($"1.1.1.1/{netmask}");

            var netmask2 = ipnetwork.Netmask.ToString();
            var wildcardmask = ipnetwork.WildcardMask.ToString();

            Assert.AreEqual(netmask, netmask2, "netmask");
            Assert.AreEqual(expected, wildcardmask, "wildcardmask");
        }

        [DataTestMethod]
        [DataRow(32, "0.0.0.0")]
        [DataRow(31, "0.0.0.1")]
        [DataRow(30, "0.0.0.3")]
        [DataRow(29, "0.0.0.7")]
        [DataRow(28, "0.0.0.15")]
        [DataRow(27, "0.0.0.31")]
        [DataRow(26, "0.0.0.63")]
        [DataRow(25, "0.0.0.127")]
        [DataRow(24, "0.0.0.255")]
        [DataRow(23, "0.0.1.255")]
        [DataRow(22, "0.0.3.255")]
        [DataRow(21, "0.0.7.255")]
        [DataRow(20, "0.0.15.255")]
        [DataRow(19, "0.0.31.255")]
        [DataRow(18, "0.0.63.255")]
        [DataRow(17, "0.0.127.255")]
        [DataRow(16, "0.0.255.255")]
        [DataRow(15, "0.1.255.255")]
        [DataRow(14, "0.3.255.255")]
        [DataRow(13, "0.7.255.255")]
        [DataRow(12, "0.15.255.255")]
        [DataRow(11, "0.31.255.255")]
        [DataRow(10, "0.63.255.255")]
        [DataRow(9, "0.127.255.255")]
        [DataRow(8, "0.255.255.255")]
        [DataRow(7, "1.255.255.255")]
        [DataRow(6, "3.255.255.255")]
        [DataRow(5, "7.255.255.255")]
        [DataRow(4, "15.255.255.255")]
        [DataRow(3, "31.255.255.255")]
        [DataRow(2, "63.255.255.255")]
        [DataRow(1, "127.255.255.255")]
        [DataRow(0, "255.255.255.255")]
        public void Test_WildcardMask_ipv4_cidr(int cidr, string expected)
        {
            IPNetwork ipnetwork = IPNetwork.Parse($"1.1.1.1/{cidr}");

            var cidr2 = (int)ipnetwork.Cidr;
            var wildcardmask = ipnetwork.WildcardMask.ToString();

            Assert.AreEqual(cidr, cidr2, "netmask");
            Assert.AreEqual(expected, wildcardmask, "wildcardmask");
        }

        #endregion

        #region ipv6

        [TestMethod]
        public void Test_WildcardMask_ipv6_mask_0()
        {
            IPNetwork ipnetwork = IPNetwork.Parse("::/0");

            var netmask = ipnetwork.Netmask.ToString();
            var wildcardmask = ipnetwork.WildcardMask.ToString();

            Assert.AreEqual("::", netmask, "netmask");
            Assert.AreEqual("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff", wildcardmask, "wildcardmask");
        }

        [TestMethod]
        public void Test_WildcardMask_ipv6_mask_128()
        {
            IPNetwork ipnetwork = IPNetwork.Parse("::/128");

            var netmask = ipnetwork.Netmask.ToString();
            var wildcardmask = ipnetwork.WildcardMask.ToString();

            Assert.AreEqual("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff", netmask, "netmask");
            Assert.AreEqual("::", wildcardmask, "wildcardmask");
        }

        [DataTestMethod]
        [DataRow("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff", "::")]
        [DataRow("ffff:ffff:ffff:ffff::", "::ffff:ffff:ffff:ffff")]
        [DataRow("::", "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff")]
        public void Test_WildcardMask_ipv6(string netmask, string expected)
        {
            IPNetwork ipnetwork = IPNetwork.Parse($"::/{netmask}");

            var netmask2 = ipnetwork.Netmask.ToString();
            var wildcardmask = ipnetwork.WildcardMask.ToString();

            Assert.AreEqual(netmask, netmask2, "netmask");
            Assert.AreEqual(expected, wildcardmask, "wildcardmask");
        }

        [DataTestMethod]
        [DataRow(128, "::")]

        [DataRow(127, "::1")]
        [DataRow(126, "::3")]
        [DataRow(125, "::7")]
        [DataRow(124, "::f")]

        [DataRow(123, "::1f")]
        [DataRow(122, "::3f")]
        [DataRow(121, "::7f")]
        [DataRow(120, "::ff")]

        [DataRow(119, "::1ff")]
        [DataRow(118, "::3ff")]
        [DataRow(117, "::7ff")]
        [DataRow(116, "::fff")]

        [DataRow(115, "::1fff")]
        [DataRow(114, "::3fff")]
        [DataRow(113, "::7fff")]
        [DataRow(112, "::ffff")]

        [DataRow(111, "::1:ffff")]
        [DataRow(110, "::3:ffff")]
        [DataRow(109, "::7:ffff")]
        [DataRow(108, "::f:ffff")]

        [DataRow(107, "::1f:ffff")]
        [DataRow(106, "::3f:ffff")]
        [DataRow(105, "::7f:ffff")]
        [DataRow(104, "::ff:ffff")]

        [DataRow(103, "::1ff:ffff")]
        [DataRow(102, "::3ff:ffff")]
        [DataRow(101, "::7ff:ffff")]
        [DataRow(100, "::fff:ffff")]

        [DataRow(99, "::1fff:ffff")]
        [DataRow(98, "::3fff:ffff")]
        [DataRow(97, "::7fff:ffff")]
        [DataRow(96, "::ffff:ffff")]

        [DataRow(95, "::1:ffff:ffff")]
        [DataRow(94, "::3:ffff:ffff")]
        [DataRow(93, "::7:ffff:ffff")]
        [DataRow(92, "::f:ffff:ffff")]

        [DataRow(91, "::1f:ffff:ffff")]
        [DataRow(90, "::3f:ffff:ffff")]
        [DataRow(89, "::7f:ffff:ffff")]
        [DataRow(88, "::ff:ffff:ffff")]

        [DataRow(87, "::1ff:ffff:ffff")]
        [DataRow(86, "::3ff:ffff:ffff")]
        [DataRow(85, "::7ff:ffff:ffff")]
        [DataRow(84, "::fff:ffff:ffff")]

        [DataRow(83, "::1fff:ffff:ffff")]
        [DataRow(82, "::3fff:ffff:ffff")]
        [DataRow(81, "::7fff:ffff:ffff")]
        [DataRow(80, "::ffff:ffff:ffff")]

        [DataRow(79, "::1:ffff:ffff:ffff")]
        [DataRow(78, "::3:ffff:ffff:ffff")]
        [DataRow(77, "::7:ffff:ffff:ffff")]
        [DataRow(76, "::f:ffff:ffff:ffff")]

        [DataRow(75, "::1f:ffff:ffff:ffff")]
        [DataRow(74, "::3f:ffff:ffff:ffff")]
        [DataRow(73, "::7f:ffff:ffff:ffff")]
        [DataRow(72, "::ff:ffff:ffff:ffff")]

        [DataRow(71, "::1ff:ffff:ffff:ffff")]
        [DataRow(70, "::3ff:ffff:ffff:ffff")]
        [DataRow(69, "::7ff:ffff:ffff:ffff")]
        [DataRow(68, "::fff:ffff:ffff:ffff")]

        [DataRow(67, "::1fff:ffff:ffff:ffff")]
        [DataRow(66, "::3fff:ffff:ffff:ffff")]
        [DataRow(65, "::7fff:ffff:ffff:ffff")]
        [DataRow(64, "::ffff:ffff:ffff:ffff")]

        [DataRow(63, "::1:ffff:ffff:ffff:ffff")]
        [DataRow(62, "::3:ffff:ffff:ffff:ffff")]
        [DataRow(61, "::7:ffff:ffff:ffff:ffff")]
        [DataRow(60, "::f:ffff:ffff:ffff:ffff")]

        [DataRow(59, "::1f:ffff:ffff:ffff:ffff")]
        [DataRow(58, "::3f:ffff:ffff:ffff:ffff")]
        [DataRow(57, "::7f:ffff:ffff:ffff:ffff")]
        [DataRow(56, "::ff:ffff:ffff:ffff:ffff")]

        [DataRow(55, "::1ff:ffff:ffff:ffff:ffff")]
        [DataRow(54, "::3ff:ffff:ffff:ffff:ffff")]
        [DataRow(53, "::7ff:ffff:ffff:ffff:ffff")]
        [DataRow(52, "::fff:ffff:ffff:ffff:ffff")]

        [DataRow(51, "::1fff:ffff:ffff:ffff:ffff")]
        [DataRow(50, "::3fff:ffff:ffff:ffff:ffff")]
        [DataRow(49, "::7fff:ffff:ffff:ffff:ffff")]
        [DataRow(48, "::ffff:ffff:ffff:ffff:ffff")]

        [DataRow(47, "::1:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(46, "::3:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(45, "::7:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(44, "::f:ffff:ffff:ffff:ffff:ffff")]

        [DataRow(43, "::1f:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(42, "::3f:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(41, "::7f:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(40, "::ff:ffff:ffff:ffff:ffff:ffff")]

        [DataRow(39, "::1ff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(38, "::3ff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(37, "::7ff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(36, "::fff:ffff:ffff:ffff:ffff:ffff")]

        [DataRow(35, "::1fff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(34, "::3fff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(33, "::7fff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(32, "::ffff:ffff:ffff:ffff:ffff:ffff")]

        [DataRow(31, "0:1:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(30, "0:3:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(29, "0:7:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(28, "0:f:ffff:ffff:ffff:ffff:ffff:ffff")]

        [DataRow(27, "0:1f:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(26, "0:3f:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(25, "0:7f:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(24, "0:ff:ffff:ffff:ffff:ffff:ffff:ffff")]

        [DataRow(23, "0:1ff:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(22, "0:3ff:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(21, "0:7ff:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(20, "0:fff:ffff:ffff:ffff:ffff:ffff:ffff")]

        [DataRow(19, "0:1fff:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(18, "0:3fff:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(17, "0:7fff:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(16, "0:ffff:ffff:ffff:ffff:ffff:ffff:ffff")]

        [DataRow(15, "1:ffff:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(14, "3:ffff:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(13, "7:ffff:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(12, "f:ffff:ffff:ffff:ffff:ffff:ffff:ffff")]

        [DataRow(11, "1f:ffff:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(10, "3f:ffff:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(9, "7f:ffff:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(8, "ff:ffff:ffff:ffff:ffff:ffff:ffff:ffff")]

        [DataRow(7, "1ff:ffff:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(6, "3ff:ffff:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(5, "7ff:ffff:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(4, "fff:ffff:ffff:ffff:ffff:ffff:ffff:ffff")]

        [DataRow(3, "1fff:ffff:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(2, "3fff:ffff:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(1, "7fff:ffff:ffff:ffff:ffff:ffff:ffff:ffff")]
        [DataRow(0, "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff")]
        public void Test_WildcardMask_ipv6_cidr(int cidr, string expected)
        {
            IPNetwork ipnetwork = IPNetwork.Parse($"::/{cidr}");

            var cidr2 = (int)ipnetwork.Cidr;
            var expectedipv6 = IPAddress.Parse(expected);
            var expectedcidr = IPNetwork.ToBigInteger(expectedipv6);
            var wildcardmask = ipnetwork.WildcardMask;
            var wildcardcidr = IPNetwork.ToBigInteger(wildcardmask);

            Assert.AreEqual(cidr, cidr2, "netmask");
            Assert.AreEqual(expectedcidr, wildcardcidr, "wildcardcidr");
        }

        #endregion
    }
}
