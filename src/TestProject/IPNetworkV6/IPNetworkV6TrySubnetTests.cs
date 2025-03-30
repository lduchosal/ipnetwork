// <copyright file="IPNetworkV6TrySubnetTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// TrySubnet.
/// </summary>
public class IPNetworkV6TrySubnetTests
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTrySubnet3()
    {
        var ipnetwork = IPNetwork2.Parse("2001:db08::/64");
        byte cidr = 255;

        bool subnetted = ipnetwork.TrySubnet(cidr, out IPNetworkCollection _);

        Assert.IsFalse(subnetted, "subnetted");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTrySubnet4()
    {
        var ipnetwork = IPNetwork2.Parse("2001:db08::/64");
        byte cidr = 63;

        bool subnetted = ipnetwork.TrySubnet(cidr, out IPNetworkCollection _);

        Assert.IsFalse(subnetted, "subnetted");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTrySubnet5()
    {
        var ipnetwork = IPNetwork2.Parse("2001:db8::/64");
        byte cidr = 65;

        bool subnetted = ipnetwork.TrySubnet(cidr, out IPNetworkCollection subnets);

        Assert.IsTrue(subnetted, "subnetted");
        Assert.AreEqual(2, subnets.Count, "count");
        Assert.AreEqual("2001:db8::/65", subnets[0].ToString(), "subnet1");
        Assert.AreEqual("2001:db8:0:0:8000::/65", subnets[1].ToString(), "subnet2");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTrySubnet6()
    {
        var ipnetwork = IPNetwork2.Parse("2001:db8::/64");
        byte cidr = 68;

        bool subnetted = ipnetwork.TrySubnet(cidr, out IPNetworkCollection subnets);

        Assert.IsTrue(subnetted, "subnetted");
        Assert.AreEqual(16, subnets.Count, "count");
        Assert.AreEqual("2001:db8::/68", subnets[0].ToString(), "subnet1");
        Assert.AreEqual("2001:db8:0:0:1000::/68", subnets[1].ToString(), "subnet2");
        Assert.AreEqual("2001:db8:0:0:2000::/68", subnets[2].ToString(), "subnet3");
        Assert.AreEqual("2001:db8:0:0:3000::/68", subnets[3].ToString(), "subnet4");
        Assert.AreEqual("2001:db8:0:0:4000::/68", subnets[4].ToString(), "subnet5");
        Assert.AreEqual("2001:db8:0:0:5000::/68", subnets[5].ToString(), "subnet6");
        Assert.AreEqual("2001:db8:0:0:6000::/68", subnets[6].ToString(), "subnet7");
        Assert.AreEqual("2001:db8:0:0:7000::/68", subnets[7].ToString(), "subnet8");
        Assert.AreEqual("2001:db8:0:0:8000::/68", subnets[8].ToString(), "subnet9");
        Assert.AreEqual("2001:db8:0:0:9000::/68", subnets[9].ToString(), "subnet10");
        Assert.AreEqual("2001:db8:0:0:a000::/68", subnets[10].ToString(), "subnet11");
        Assert.AreEqual("2001:db8:0:0:b000::/68", subnets[11].ToString(), "subnet12");
        Assert.AreEqual("2001:db8:0:0:c000::/68", subnets[12].ToString(), "subnet13");
        Assert.AreEqual("2001:db8:0:0:d000::/68", subnets[13].ToString(), "subnet14");
        Assert.AreEqual("2001:db8:0:0:e000::/68", subnets[14].ToString(), "subnet15");
        Assert.AreEqual("2001:db8:0:0:f000::/68", subnets[15].ToString(), "subnet16");
    }
}
