// <copyright file="ContainsUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

/// <summary>
///     ContainsUnitTest test every Contiains method.
/// </summary>
[TestClass]
public class ContainsUnitTest
{
    /// <summary>
    /// Test.
    /// </summary>
    /// <param name="network1">The network.</param>
    /// <param name="networkOrAddress">The netmask.</param>
    /// <param name="expected">The result.</param>
    [DataTestMethod]
    [DataRow("192.168.0.1/24", "192.168.0.1/24", true)]
    [DataRow("192.168.0.1/16", "192.168.1.1/24", true)]
    [DataRow("192.168.0.1/16", "10.10.10.0/24", false)]
    [DataRow("192.168.1.1/24", "192.168.0.0/16", false)]
    public void TestContainsNetwork(string network1, string networkOrAddress, bool expected)
    {
            var ipnetwork = IPNetwork2.Parse(network1);

            var ipnetwork2 = IPNetwork2.Parse(networkOrAddress);
            bool result = ipnetwork.Contains(ipnetwork2);

            Assert.AreEqual(expected, result, "contains");
        }

    /// <summary>
    /// Test.
    /// </summary>
    /// <param name="network1">The network.</param>
    /// <param name="networkOrAddress">An address in the netmask.</param>
    /// <param name="expected">Contained.</param>
    [DataTestMethod]
    [DataRow("192.168.0.1/24", "192.168.0.100", true)]
    [DataRow("192.168.0.1/24", "10.10.10.10", false)]
    public void TestContainsAdrress(string network1, string networkOrAddress, bool expected)
    {
            var ipnetwork = IPNetwork2.Parse(network1);
            bool result;
            var ipaddress = IPAddress.Parse(networkOrAddress);
            result = ipnetwork.Contains(ipaddress);

            Assert.AreEqual(expected, result, "contains");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestContainsStatic3()
    {
            IPNetwork2 ipnetwork = null;
            IPNetwork2 ipnetwork2 = null;

#pragma warning disable 0618
            IPNetwork2.Contains(ipnetwork, ipnetwork2);
#pragma warning restore 0618
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestContainsStatic4()
    {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_CBLK_RESERVED1;
            IPNetwork2 ipnetwork2 = IPNetwork2.IANA_CBLK_RESERVED1;

#pragma warning disable 0618
            bool result = IPNetwork2.Contains(ipnetwork, ipnetwork2);
#pragma warning restore 0618

            Assert.IsTrue(result, "result");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestContains8()
    {
            var ipnetwork = IPNetwork2.Parse("0.0.0.0/0");
            IPNetwork2 ipnetwork2 = null;

            ipnetwork.Contains(ipnetwork2);
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestContainsStatic1()
    {
            IPNetwork2 ipnetwork = null;
            IPAddress ipaddress = null;

#pragma warning disable 0618
            IPNetwork2.Contains(ipnetwork, ipaddress);
#pragma warning restore 0618
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestContainsStatic2()
    {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_ABLK_RESERVED1;
            var ipaddress = IPAddress.Parse("10.0.0.1");

#pragma warning disable 0618
            bool result = IPNetwork2.Contains(ipnetwork, ipaddress);
#pragma warning restore 0618
            Assert.IsTrue(result, "result");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestContains10()
    {
            var ipnetwork = IPNetwork2.Parse("0.0.0.0/0");
            IPAddress ipaddress = null;

            ipnetwork.Contains(ipaddress);
        }

    /// <summary>
    /// Test.
    /// </summary>
    /// <param name="contains1">The network.</param>
    /// <param name="contains2">Contains the IPAddress.</param>
    /// <param name="expected">Result, true if contains.</param>
    [DataTestMethod]
    [DataRow("1.1.1.0/8", "1.1.1.1", true)]
    [DataRow("1.1.1.0/8", "2.1.1.1", false)]
    [DataRow("192.168.0.1/24", "192.168.0.100", true)]
    [DataRow("192.168.0.1/24", "10.10.10.10", false)]
    [DataRow("192.168.0.1/24", "192.168.0.1", true)]
    [DataRow("192.168.0.1/16", "192.168.1.1", true)]
    [DataRow("192.168.0.1/16", "10.10.10.0", false)]
    [DataRow("192.168.1.1/24", "192.168.0.0", false)]
    public void Test_Contains2_IPAddress_Should_Match_Contains(string contains1, string contains2, bool expected)
    {
            var ipnetwork = IPNetwork2.Parse(contains1);
            var ipaddress = IPAddress.Parse(contains2);

            bool result1 = ipnetwork.Contains(ipaddress);

            Assert.AreEqual(expected, result1, "contains1");
        }

    /// <summary>
    /// Test.
    /// </summary>
    /// <param name="contains1">The network.</param>
    /// <param name="contains2">Contains the IPAddress.</param>
    /// <param name="expected">Result, true if contains.</param>
    [DataTestMethod]
    [DataRow("0.0.0.0/0", "255.255.255.255", true)]
    [DataRow("1.1.1.0/8", "1.1.1.1", true)]
    [DataRow("1.1.1.0/8", "2.1.1.1", false)]
    [DataRow("192.168.0.1/24", "192.168.0.100/32", true)]
    [DataRow("192.168.0.1/24", "10.10.10.10/32", false)]
    [DataRow("192.168.0.1/24", "192.168.0.1/24", true)]
    [DataRow("192.168.0.1/16", "192.168.1.1/24", true)]
    [DataRow("192.168.0.1/16", "10.10.10.0/24", false)]
    [DataRow("192.168.1.1/24", "192.168.0.0/16", false)]
    public void Test_Contains2_IPNetwork_Should_Match_Contains(string contains1, string contains2, bool expected)
    {
            var ipnetwork = IPNetwork2.Parse(contains1);
            var ipaddress = IPNetwork2.Parse(contains2);

            bool result1 = ipnetwork.Contains(ipaddress);

            Assert.AreEqual(expected, result1, "contains1");
        }
}