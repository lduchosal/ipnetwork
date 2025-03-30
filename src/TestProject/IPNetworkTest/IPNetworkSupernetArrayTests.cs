// <copyright file="IPNetworkSupernetArrayTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class IPNetworkSupernetArrayTests
{
    /// <summary>
    ///     Tests Supernet Array functionality with Try Supernet Array.
    /// </summary>
    [TestMethod]
    public void TestTrySupernetArray()
    {
            var ipnetwork1 = IPNetwork2.Parse("192.168.0.0/24");
            var ipnetwork2 = IPNetwork2.Parse("192.168.1.0/24");
            var ipnetwork3 = IPNetwork2.Parse("192.168.2.0/24");
            var ipnetwork4 = IPNetwork2.Parse("192.168.3.0/24");

            IPNetwork2[] ipnetworks = [ipnetwork1, ipnetwork2, ipnetwork3, ipnetwork4];
            IPNetwork2[] expected = [IPNetwork2.Parse("192.168.0.0/22")];

            IPNetwork2[] result = IPNetwork2.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], expected[0], "suppernet");
        }

    /// <summary>
    ///     Tests Supernet Array functionality with Try Supernet Array1.
    /// </summary>
    [TestMethod]
    public void TestTrySupernetArray1()
    {
            IPNetwork2[] ipnetworks = [];
            IPNetwork2[] expected = [];

            IPNetwork2[] result = IPNetwork2.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestTrySupernetArray2()
    {
        IPNetwork2[] ipnetworks = null;
        IPNetwork2.Supernet(ipnetworks);
    }

    /// <summary>
    ///     Tests Supernet Array functionality with Try Supernet Array3.
    /// </summary>
    [TestMethod]
    public void TestTrySupernetArray3()
    {
            IPNetwork2 ipnetwork1 = null;
            IPNetwork2 ipnetwork2 = null;
            IPNetwork2 ipnetwork3 = null;
            IPNetwork2 ipnetwork4 = null;

            IPNetwork2[] ipnetworks = [ipnetwork1, ipnetwork2, ipnetwork3, ipnetwork4];
            IPNetwork2[] expected = [];

            IPNetwork2[] result = IPNetwork2.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
        }

    /// <summary>
    ///     Tests Supernet Array functionality with Try Supernet Array4.
    /// </summary>
    [TestMethod]
    public void TestTrySupernetArray4()
    {
            var ipnetwork1 = IPNetwork2.Parse("192.168.0.0/24");
            IPNetworkCollection subnetted = ipnetwork1.Subnet(32);
            IPNetwork2[] ipnetworks = subnetted.ToArray();
            Assert.AreEqual(256, ipnetworks.Length, "subnet");

            IPNetwork2[] expected = [IPNetwork2.Parse("192.168.0.0/24")];

            IPNetwork2[] result = IPNetwork2.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], ipnetwork1, "suppernet");
        }

    /// <summary>
    ///     Tests Supernet Array functionality with Try Supernet Array5.
    /// </summary>
    [TestMethod]
    public void TestTrySupernetArray5()
    {
            var ipnetwork1 = IPNetwork2.Parse("192.168.0.0/16");
            IPNetworkCollection subnetted = ipnetwork1.Subnet(24);
            IPNetwork2[] ipnetworks = subnetted.ToArray();
            Assert.AreEqual(256, ipnetworks.Length, "subnet");

            IPNetwork2[] expected = [IPNetwork2.Parse("192.168.0.0/16")];

            IPNetwork2[] result = IPNetwork2.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], ipnetwork1, "suppernet");
        }

    /// <summary>
    ///     Tests Supernet Array functionality with Try Supernet Array6.
    /// </summary>
    [TestMethod]
    public void TestTrySupernetArray6()
    {
            var ipnetwork1 = IPNetwork2.Parse("192.168.0.0/8");
            IPNetworkCollection subnetted = ipnetwork1.Subnet(24);
            IPNetwork2[] ipnetworks = subnetted.ToArray();
            Assert.AreEqual(65536, ipnetworks.Length, "subnet");

            IPNetwork2[] expected = [IPNetwork2.Parse("192.0.0.0/8")];

            IPNetwork2[] result = IPNetwork2.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], ipnetwork1, "suppernet");
        }

    /// <summary>
    ///     Tests Supernet Array functionality with Try Supernet Array7.
    /// </summary>
    [TestMethod]
    public void TestTrySupernetArray7()
    {
            IPNetwork2[] ipnetworks =
            [
                IPNetwork2.Parse("10.0.2.2/24"),
                IPNetwork2.Parse("192.168.0.0/24"),
                IPNetwork2.Parse("192.168.1.0/24"),
                IPNetwork2.Parse("192.168.2.0/24"),
                IPNetwork2.Parse("10.0.1.1/24"),
                IPNetwork2.Parse("192.168.3.0/24")
            ];

            IPNetwork2[] expected =
            [
                IPNetwork2.Parse("10.0.1.0/24"),
                IPNetwork2.Parse("10.0.2.0/24"),
                IPNetwork2.Parse("192.168.0/22")
            ];

            IPNetwork2[] result = IPNetwork2.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], result[0], "suppernet");
            Assert.AreEqual(expected[1], result[1], "suppernet1");
            Assert.AreEqual(expected[2], result[2], "suppernet2");
        }

    /// <summary>
    ///     Tests Supernet Array functionality with Try Supernet Array8.
    /// </summary>
    [TestMethod]
    public void TestTrySupernetArray8()
    {
            IPNetwork2[] ipnetworks =
            [
                IPNetwork2.Parse("10.0.2.2/24"),
                IPNetwork2.Parse("192.168.0.0/24"),
                IPNetwork2.Parse("192.168.1.0/24"),
                IPNetwork2.Parse("192.168.2.0/24"),
                IPNetwork2.Parse("10.0.1.1/24"),
                IPNetwork2.Parse("192.168.3.0/24"),
                IPNetwork2.Parse("10.6.6.6/8")
            ];

            IPNetwork2[] expected =
            [
                IPNetwork2.Parse("10.0.0.0/8"),
                IPNetwork2.Parse("192.168.0/22")
            ];

            IPNetwork2[] result = IPNetwork2.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], result[0], "suppernet");
            Assert.AreEqual(expected[1], result[1], "suppernet1");
        }

    /// <summary>
    ///     Tests Supernet Array functionality with Try Supernet Array9.
    /// </summary>
    [TestMethod]
    public void TestTrySupernetArray9()
    {
            IPNetwork2[] ipnetworks =
            [
                IPNetwork2.Parse("10.0.2.2/24"),
                IPNetwork2.Parse("192.168.0.0/24"),
                IPNetwork2.Parse("192.168.1.0/24"),
                IPNetwork2.Parse("192.168.2.0/24"),
                IPNetwork2.Parse("10.0.1.1/24"),
                IPNetwork2.Parse("192.168.3.0/24"),
                IPNetwork2.Parse("10.6.6.6/8"),
                IPNetwork2.Parse("11.6.6.6/8"),
                IPNetwork2.Parse("12.6.6.6/8")
            ];

            IPNetwork2[] expected =
            [
                IPNetwork2.Parse("10.0.0.0/7"),
                IPNetwork2.Parse("12.0.0.0/8"),
                IPNetwork2.Parse("192.168.0/22")
            ];

            IPNetwork2[] result = IPNetwork2.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], result[0], "suppernet");
            Assert.AreEqual(expected[1], result[1], "suppernet1");
            Assert.AreEqual(expected[2], result[2], "suppernet2");
        }

    /// <summary>
    ///     Tests Supernet Array functionality with Try Supernet Array10.
    /// </summary>
    [TestMethod]
    public void TestTrySupernetArray10()
    {
            IPNetwork2[] ipnetworks =
            [
                IPNetwork2.Parse("10.0.2.2/24"),
                IPNetwork2.Parse("10.0.2.2/23")
            ];

            IPNetwork2[] expected =
            [
                IPNetwork2.Parse("10.0.2.2/23")
            ];

            IPNetwork2[] result = IPNetwork2.Supernet(ipnetworks);

            Assert.AreEqual(expected.Length, result.Length, "supernetarray");
            Assert.AreEqual(expected[0], result[0], "suppernet");
        }
}