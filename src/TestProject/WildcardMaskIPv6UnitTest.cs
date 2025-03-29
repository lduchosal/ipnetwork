// <copyright file="WildcardMaskIPv6UnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class WildcardMaskIPv6UnitTest
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_WildcardMask_ipv6_mask_0()
    {
            var ipnetwork = IPNetwork2.Parse("::/0");

            string netmask = ipnetwork.Netmask.ToString();
            string wildcardmask = ipnetwork.WildcardMask.ToString();

            Assert.AreEqual("::", netmask, "netmask");
            Assert.AreEqual("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff", wildcardmask, "wildcardmask");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_WildcardMask_ipv6_mask_128()
    {
            var ipnetwork = IPNetwork2.Parse("::/128");

            string netmask = ipnetwork.Netmask.ToString();
            string wildcardmask = ipnetwork.WildcardMask.ToString();

            Assert.AreEqual("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff", netmask, "netmask");
            Assert.AreEqual("::", wildcardmask, "wildcardmask");
        }

    /// <summary>
    /// Test.
    /// </summary>
    /// <param name="netmask">The netmask.</param>
    /// <param name="expected">The resulting netmask.</param>
    [DataTestMethod]
    [DataRow("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff", "::")]
    [DataRow("ffff:ffff:ffff:ffff::", "::ffff:ffff:ffff:ffff")]
    [DataRow("::", "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff")]
    public void Test_WildcardMask_ipv6(string netmask, string expected)
    {
            var ipnetwork = IPNetwork2.Parse($"::/{netmask}");

            string netmask2 = ipnetwork.Netmask.ToString();
            string wildcardmask = ipnetwork.WildcardMask.ToString();

            Assert.AreEqual(netmask, netmask2, "netmask");
            Assert.AreEqual(expected, wildcardmask, "wildcardmask");
        }

    /// <summary>
    /// Test.
    /// </summary>
    /// <param name="cidr">The cidr.</param>
    /// <param name="expected">The resulting netmask.</param>
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
            var ipnetwork = IPNetwork2.Parse($"::/{cidr}");

            int cidr2 = ipnetwork.Cidr;
            var expectedipv6 = IPAddress.Parse(expected);
            var expectedcidr = IPNetwork2.ToBigInteger(expectedipv6);
            IPAddress wildcardmask = ipnetwork.WildcardMask;
            var wildcardcidr = IPNetwork2.ToBigInteger(wildcardmask);

            Assert.AreEqual(cidr, cidr2, "netmask");
            Assert.AreEqual(expectedcidr, wildcardcidr, "wildcardcidr");
        }
}