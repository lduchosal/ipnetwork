// <copyright file="IPNetworkV6SubnetTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// Subnet.
/// </summary>
[TestClass]
public class IPNetworkV6SubnetTests
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestSubnet3()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
        {
            var ipnetwork = IPNetwork2.Parse("::");
            byte cidr = 129;

            ipnetwork.Subnet(cidr);
        });
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestSubnet4()
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
        {
            var ipnetwork = IPNetwork2.Parse("::");
            byte cidr = 1;

            ipnetwork.Subnet(cidr);
        });
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestSubnet5()
    {
        var ipnetwork = IPNetwork2.Parse("1:1:1:1:1:1:1:1");
        byte cidr = 65;

        IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
        Assert.AreEqual(2, subnets.Count, "count");
        Assert.AreEqual("1:1:1:1::/65", subnets[0].ToString(), "subnet1");
        Assert.AreEqual("1:1:1:1:8000::/65", subnets[1].ToString(), "subnet2");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestSubnet6()
    {
        var ipnetwork = IPNetwork2.Parse("1:1:1:1:1:1:1:1");
        byte cidr = 68;

        IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
        Assert.AreEqual(16, subnets.Count, "count");
        Assert.AreEqual("1:1:1:1::/68", subnets[0].ToString(), "subnet1");
        Assert.AreEqual("1:1:1:1:1000::/68", subnets[1].ToString(), "subnet2");
        Assert.AreEqual("1:1:1:1:2000::/68", subnets[2].ToString(), "subnet3");
        Assert.AreEqual("1:1:1:1:3000::/68", subnets[3].ToString(), "subnet4");
        Assert.AreEqual("1:1:1:1:4000::/68", subnets[4].ToString(), "subnet5");
        Assert.AreEqual("1:1:1:1:5000::/68", subnets[5].ToString(), "subnet6");
        Assert.AreEqual("1:1:1:1:6000::/68", subnets[6].ToString(), "subnet7");
        Assert.AreEqual("1:1:1:1:7000::/68", subnets[7].ToString(), "subnet8");
        Assert.AreEqual("1:1:1:1:8000::/68", subnets[8].ToString(), "subnet9");
        Assert.AreEqual("1:1:1:1:9000::/68", subnets[9].ToString(), "subnet10");
        Assert.AreEqual("1:1:1:1:a000::/68", subnets[10].ToString(), "subnet11");
        Assert.AreEqual("1:1:1:1:b000::/68", subnets[11].ToString(), "subnet12");
        Assert.AreEqual("1:1:1:1:c000::/68", subnets[12].ToString(), "subnet13");
        Assert.AreEqual("1:1:1:1:d000::/68", subnets[13].ToString(), "subnet14");
        Assert.AreEqual("1:1:1:1:e000::/68", subnets[14].ToString(), "subnet15");
        Assert.AreEqual("1:1:1:1:f000::/68", subnets[15].ToString(), "subnet16");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestSubnet7()
    {
        var ipnetwork = IPNetwork2.Parse("1:1:1:1:1:1:1:1");
        byte cidr = 72;

        IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
        Assert.AreEqual(256, subnets.Count, "count");
        Assert.AreEqual("1:1:1:1::/72", subnets[0].ToString(), "subnet1");
        Assert.AreEqual("1:1:1:1:ff00::/72", subnets[255].ToString(), "subnet256");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestSubnet9()
    {
        var ipnetwork = IPNetwork2.Parse("2001:db08:1:1:1:1:1:1");
        byte cidr = 128;
        var count = BigInteger.Pow(2, ipnetwork.Cidr);
        IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
        Assert.AreEqual(count, subnets.Count, "count");
        Assert.AreEqual("2001:db08:1:1::/128", subnets[0].ToString(), "subnet1");
        Assert.AreEqual("2001:db08:1:1::ff/128", subnets[255].ToString(), "subnet256");
        Assert.AreEqual("2001:db08:1:1:ffff:ffff:ffff:ffff/128", subnets[count - 1].ToString(), "last");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestSubnet10()
    {
        var ipnetwork = IPNetwork2.Parse("2001:db08::/0");
        byte cidr = 128;
        var count = BigInteger.Pow(2, 128 - ipnetwork.Cidr);

        // Here I spawm a OOM dragon ! beware of the beast !
        IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
        Assert.AreEqual(count, subnets.Count, "count");
        Assert.AreEqual("::/128", subnets[0].ToString(), "subnet1");
        Assert.AreEqual("ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff/128", subnets[count - 1].ToString(), "last");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestSubnet12()
    {
        var ipnetwork = IPNetwork2.Parse("2001:db08::/64");
        byte cidr = 70;
        int i = -1;
        IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
        foreach (IPNetwork2 ipn in subnets)
        {
            i++;
            Assert.AreEqual(subnets[i], ipn, "subnet");
        }
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestSubnet13()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
        {
            var ipnetwork = IPNetwork2.Parse("2001:db08::/64");
            byte cidr = 70;
            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            IPNetwork2 _ = subnets[1000];
        });
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestSubnet14()
    {
        var network = IPNetwork2.Parse("15.0.0.0/8");
        IPNetworkCollection subnets = network.Subnet(12);
        Assert.AreEqual("15.0.0.0/12", subnets[0].ToString(), "subnets[0]");
        Assert.AreEqual("15.16.0.0/12", subnets[1].ToString(), "subnets[1]");
        Assert.AreEqual("15.32.0.0/12", subnets[2].ToString(), "subnets[2]");
        Assert.AreEqual("15.240.0.0/12", subnets[15].ToString(), "subnets[15]");

        foreach (IPNetwork2 ipn in subnets)
        {
            Console.WriteLine(ipn);
        }
    }
}
