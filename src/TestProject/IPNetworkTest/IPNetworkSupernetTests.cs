// <copyright file="IPNetworkSupernetTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Tests Supernet functionality.
/// </summary>
[TestClass]
public class IPNetworkSupernetTests
{
    /// <summary>
    ///     Tests Supernet functionality with Supernet Internal1.
    /// </summary>
    [TestMethod]
    public void TestSupernetInternal1()
    {
        IPNetwork2.InternalSupernet(true, null, null, out IPNetwork2 result);

        Assert.IsNull(result, "supernet");
    }

    /// <summary>
    /// Tests Supernet functionality with null.
    /// </summary>
    [TestMethod]
    public void TestSupernetInternal2()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            IPNetwork2.InternalSupernet(false, null, null, out var _);
        });
    }

    /// <summary>
    /// Tests Supernet functionality with Issue33__TestSupernet__Bug_or_default_behavior.
    /// </summary>
    [TestMethod]
    public void Issue33__TestSupernet__Bug_or_default_behavior()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
        {
            var network1 = IPNetwork2.Parse("192.168.1.0/24");
            var network2 = IPNetwork2.Parse("192.168.2.0/24");
            var expected = IPNetwork2.Parse("192.168.0.0/23");
            IPNetwork2 supernet = network1.Supernet(network2);

            Assert.AreEqual(expected, supernet, "supernet");
        });
    }

    /// <summary>
    ///     Tests Supernet functionality.
    /// </summary>
    [TestMethod]
    public void Issue33__TestWideSubnet__Bug_or_default_behavior()
    {
        var network1 = IPNetwork2.Parse("192.168.0.0/24");
        var network2 = IPNetwork2.Parse("192.168.2.0/24");
        var expected = IPNetwork2.Parse("192.168.0.0/22");
        var widenetwork = IPNetwork2.WideSubnet([network1, network2]);

        Assert.AreEqual(expected, widenetwork, "widesubnet");
    }

    /// <summary>
    ///     Tests Supernet functionality.
    /// </summary>
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

    /// <summary>
    ///     Tests Supernet functionality with Supernet1.
    /// </summary>
    [TestMethod]
    public void TestSupernet1()
    {
        var network1 = IPNetwork2.Parse("192.168.10.1/24");
        var network2 = IPNetwork2.Parse("192.168.11.1/24");
        var expected = IPNetwork2.Parse("192.168.10.0/23");
        IPNetwork2 supernet = network1.Supernet(network2);

        Assert.AreEqual(expected, supernet, "supernet");
    }

    /// <summary>
    /// Test to supernet a null network1.
    /// </summary>
    [TestMethod]
    public void TestSupernet2()
    {
        Assert.ThrowsExactly<NullReferenceException>(() =>
        {
            var network1 = IPNetwork2.Parse("192.168.1.1/24");
            network1.Supernet(null);
        });
    }

    /// <summary>
    /// Test to supernet a null network2.
    /// </summary>
    [TestMethod]
    public void TestSupernet3()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            var network1 = IPNetwork2.Parse("192.168.1.1/24");
            IPNetwork2 network2 = null;
            network1.Supernet(network2);
        });
    }

    /// <summary>
    /// Test to supernet overlapping networks.
    /// </summary>
    [TestMethod]
    public void TestSupernet4()
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
        {
            var network1 = IPNetwork2.Parse("192.168.0.1/24");
            var network2 = IPNetwork2.Parse("192.168.1.1/25");
            network1.Supernet(network2);
        });
    }

    /// <summary>
    /// Test to supernet non overlapping networks.
    /// </summary>
    [TestMethod]
    public void TestSupernet5()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
        {
            var network1 = IPNetwork2.Parse("192.168.0.1/24");
            var network2 = IPNetwork2.Parse("192.168.5.1/24");
            network1.Supernet(network2);
        });
    }

    /// <summary>
    ///     Tests Supernet functionality with Supernet6.
    /// </summary>
    [TestMethod]
    public void TestSupernet6()
    {
        var network1 = IPNetwork2.Parse("192.168.0.1/24");
        var network2 = IPNetwork2.Parse("192.168.0.1/25");
        var expected = IPNetwork2.Parse("192.168.0.0/24");
        IPNetwork2 supernet = network1.Supernet(network2);

        Assert.AreEqual(expected, supernet, "supernet");
    }

    /// <summary>
    ///     Tests Supernet functionality with Supernet7.
    /// </summary>
    [TestMethod]
    public void TestSupernet7()
    {
        var network1 = IPNetwork2.Parse("192.168.0.1/25");
        var network2 = IPNetwork2.Parse("192.168.0.1/24");
        var expected = IPNetwork2.Parse("192.168.0.0/24");
        IPNetwork2 supernet = network1.Supernet(network2);

        Assert.AreEqual(expected, supernet, "supernet");
    }

    /// <summary>
    ///     Tests Supernet functionality with Supernet Static1.
    /// </summary>
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

    /// <summary>
    /// Test to supernet continuous networks.
    /// </summary>
    [TestMethod]
    public void TestSupernet8()
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
        {
            var network1 = IPNetwork2.Parse("192.168.1.1/24");
            var network2 = IPNetwork2.Parse("192.168.2.1/24");
            network1.Supernet(network2);
        });
    }

    /// <summary>
    ///     Tests Supernet functionality with Supernet9.
    /// </summary>
    [TestMethod]
    public void TestSupernet9()
    {
        var ipnetwork1 = IPNetwork2.Parse("200.16.0.0/24");
        var ipnetwork2 = IPNetwork2.Parse("200.16.1.0/24");
        var ipnetwork3 = IPNetwork2.Parse("200.16.2.0/24");
        var ipnetwork4 = IPNetwork2.Parse("200.16.3.0/24");

        IPNetwork2 result = IPNetwork2.Supernet([ipnetwork1, ipnetwork2, ipnetwork3, ipnetwork4])[0];
        var expected = IPNetwork2.Parse("200.16.0.0/22");

        Assert.AreEqual(expected, result, "supernet");
    }

    /// <summary>
    ///     Tests Supernet functionality with Supernet10.
    /// </summary>
    [TestMethod]
    public void TestSupernet10()
    {
        var ipnetwork1 = IPNetwork2.Parse("1.1.0.0/24");
        var ipnetwork2 = IPNetwork2.Parse("1.2.1.0/24");

        IPNetwork2 result = IPNetwork2.Supernet([ipnetwork1, ipnetwork2])[0];
        var expected = IPNetwork2.Parse("1.1.0.0/24");

        Assert.AreEqual(expected, result, "supernet");
    }
    
    
    /// <summary>
    /// Tests WideSubnet with addresses in same range
    /// </summary>
    [TestMethod]
    public void TestSupernet11()
    {
        var ipnetwork1 = IPNetwork2.Parse("192.168.1.45/32");
        var ipnetwork2 = IPNetwork2.Parse("192.168.1.65/32");

        IPNetwork2 result = IPNetwork2.Supernet([ipnetwork1, ipnetwork2])[0];
        var expected = IPNetwork2.Parse("192.168.1.45/32");

        Assert.AreEqual(expected, result, "supernet");
    }
}