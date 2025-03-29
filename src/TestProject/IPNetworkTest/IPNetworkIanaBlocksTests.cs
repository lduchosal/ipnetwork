// <copyright file="IPNetworkIanaBlocksTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Tests with the IANA blocks.
/// </summary>
[TestClass]
public class IPNetworkIanaBlocksTests
{
    /// <summary>
    ///     Tests IANA_blocks functionality with IANA1.
    /// </summary>
    [TestMethod]
    public void TestIana1()
    {
        var ipaddress = IPAddress.Parse("192.168.66.66");
        bool expected = true;
        bool result = IPNetwork2.IsIANAReserved(ipaddress);

        Assert.AreEqual(expected, result, "IANA");
    }

    /// <summary>
    ///     Tests IANA_blocks functionality with IANA2.
    /// </summary>
    [TestMethod]
    public void TestIana2()
    {
        var ipaddress = IPAddress.Parse("10.0.0.0");
        bool expected = true;
        bool result = IPNetwork2.IsIANAReserved(ipaddress);

        Assert.AreEqual(expected, result, "IANA");
    }

    /// <summary>
    ///     Tests IANA_blocks functionality with IANA3.
    /// </summary>
    [TestMethod]
    public void TestIana3()
    {
        var ipaddress = IPAddress.Parse("172.17.10.10");
        bool expected = true;
        bool result = IPNetwork2.IsIANAReserved(ipaddress);

        Assert.AreEqual(expected, result, "IANA");
    }

    /// <summary>
    ///     Tests IANA_blocks functionality with IANA4.
    /// </summary>
    [TestMethod]
    public void TestIana4()
    {
        var ipnetwork = IPNetwork2.Parse("192.168.66.66/24");
        bool expected = true;
        bool result = ipnetwork.IsIANAReserved();

        Assert.AreEqual(expected, result, "IANA");
    }

    /// <summary>
    ///     Tests IANA_blocks functionality with IANA5.
    /// </summary>
    [TestMethod]
    public void TestIana5()
    {
        var ipnetwork = IPNetwork2.Parse("10.10.10/18");
        bool expected = true;
        bool result = ipnetwork.IsIANAReserved();

        Assert.AreEqual(expected, result, "IANA");
    }

    /// <summary>
    ///     Tests IANA_blocks functionality with IANA6.
    /// </summary>
    [TestMethod]
    public void TestIana6()
    {
        var ipnetwork = IPNetwork2.Parse("172.31.10.10/24");
        bool expected = true;
        bool result = ipnetwork.IsIANAReserved();

        Assert.AreEqual(expected, result, "IANA");
    }

    /// <summary>
    /// Test is a null ipaddress is in IANA block.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestIana7()
    {
        IPAddress ipaddress = null;
        IPNetwork2.IsIANAReserved(ipaddress);
    }

    /// <summary>
    /// Test is a null ipnetwork is in IANA block.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestIana8()
    {
        IPNetwork2 ipnetwork = null;
#pragma warning disable 0618
        bool result = IPNetwork2.IsIANAReserved(ipnetwork);
#pragma warning restore 0618
    }

    /// <summary>
    ///     Tests IANA_blocks functionality with IANABlk1.
    /// </summary>
    [TestMethod]
    public void TestIanaBlk1()
    {
        IPNetwork2 ipnetwork = IPNetwork2.IANA_ABLK_RESERVED1;
#pragma warning disable 0618
        bool result = IPNetwork2.IsIANAReserved(ipnetwork);
#pragma warning restore 0618
        Assert.IsTrue(result, "result");
    }

    /// <summary>
    ///     Tests IANA_blocks functionality with IANA9.
    /// </summary>
    [TestMethod]
    public void TestIana9()
    {
        var ipaddress = IPAddress.Parse("1.2.3.4");
        bool expected = false;
        bool result = IPNetwork2.IsIANAReserved(ipaddress);

        Assert.AreEqual(expected, result, "IANA");
    }

    /// <summary>
    ///     Tests IANA_blocks functionality with IANA10.
    /// </summary>
    [TestMethod]
    public void TestIana10()
    {
        var ipnetwork = IPNetwork2.Parse("172.16.0.0/8");
        bool expected = false;
        bool result = ipnetwork.IsIANAReserved();

        Assert.AreEqual(expected, result, "IANA");
    }

    /// <summary>
    ///     Tests IANA_blocks functionality with IANA11.
    /// </summary>
    [TestMethod]
    public void TestIana11()
    {
        var ipnetwork = IPNetwork2.Parse("192.168.15.1/8");
        bool expected = false;
        bool result = ipnetwork.IsIANAReserved();

        Assert.AreEqual(expected, result, "IANA");
    }
}