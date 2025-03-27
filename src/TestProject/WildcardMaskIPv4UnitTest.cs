// <copyright file="WildcardMaskIPv4UnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject
{
    [TestClass]
    public class WildcardMaskIPv4UnitTest
    {
        [DataTestMethod]
        [DataRow(0, "0.0.0.0", "255.255.255.255")]
        [DataRow(32, "255.255.255.255", "0.0.0.0")]
        public void Test_WildcardMask_ipv4_mask(int cidr, string thenNetmask, string andWildcardmask)
        {
            var ipnetwork = IPNetwork2.Parse($"1.1.1.1/{cidr}");

            string netmask = ipnetwork.Netmask.ToString();
            string wildcardmask = ipnetwork.WildcardMask.ToString();

            Assert.AreEqual(thenNetmask, netmask, "netmask");
            Assert.AreEqual(andWildcardmask, wildcardmask, "wildcardmask");
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
            var ipnetwork = IPNetwork2.Parse($"1.1.1.1/{netmask}");

            string netmask2 = ipnetwork.Netmask.ToString();
            string wildcardmask = ipnetwork.WildcardMask.ToString();

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
            var ipnetwork = IPNetwork2.Parse($"1.1.1.1/{cidr}");

            int cidr2 = ipnetwork.Cidr;
            string wildcardmask = ipnetwork.WildcardMask.ToString();

            Assert.AreEqual(cidr, cidr2, "netmask");
            Assert.AreEqual(expected, wildcardmask, "wildcardmask");
        }
    }
}