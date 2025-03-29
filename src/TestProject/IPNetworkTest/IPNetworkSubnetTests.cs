// <copyright file="IPNetworkSubnetTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class IPNetworkSubnetTests
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestSubnet1()
    {
            IPNetwork2 ipnetwork = null;
            byte cidr = 9;

#pragma warning disable 0618
            IPNetworkCollection result = IPNetwork2.Subnet(ipnetwork, cidr);
#pragma warning restore 0618
        }

    /// <summary>
    ///     Tests Subnet functionality with Subnet Static1.
    /// </summary>
    [TestMethod]
    public void TestSubnetStatic1()
    {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_ABLK_RESERVED1;
            byte cidr = 9;

#pragma warning disable 0618
            IPNetworkCollection result = IPNetwork2.Subnet(ipnetwork, cidr);
#pragma warning restore 0618
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestSubnet3()
    {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_ABLK_RESERVED1;
            byte cidr = 55;

            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestSubnet4()
    {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_ABLK_RESERVED1;
            byte cidr = 1;

            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
        }

    /// <summary>
    ///     Tests Subnet functionality with Subnet5.
    /// </summary>
    [TestMethod]
    public void TestSubnet5()
    {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_ABLK_RESERVED1;
            byte cidr = 9;

            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            Assert.AreEqual(2, subnets.Count, "count");
            Assert.AreEqual("10.0.0.0/9", subnets[0].ToString(), "subnet1");
            Assert.AreEqual("10.128.0.0/9", subnets[1].ToString(), "subnet2");
        }

    /// <summary>
    ///     Tests Subnet functionality with Subnet6.
    /// </summary>
    [TestMethod]
    public void TestSubnet6()
    {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_CBLK_RESERVED1;
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

    /// <summary>
    ///     Tests Subnet functionality with Subnet7.
    /// </summary>
    [TestMethod]
    public void TestSubnet7()
    {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_CBLK_RESERVED1;
            byte cidr = 24;

            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            Assert.AreEqual(256, subnets.Count, "count");
            Assert.AreEqual("192.168.0.0/24", subnets[0].ToString(), "subnet1");
            Assert.AreEqual("192.168.255.0/24", subnets[255].ToString(), "subnet16");
        }

    /// <summary>
    ///     Tests Subnet functionality with Subnet8.
    /// </summary>
    [TestMethod]
    public void TestSubnet8()
    {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_CBLK_RESERVED1;
            byte cidr = 24;

            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            Assert.AreEqual(256, subnets.Count, "count");
            Assert.AreEqual("192.168.0.0/24", subnets[0].ToString(), "subnet1");
            Assert.AreEqual("192.168.255.0/24", subnets[255].ToString(), "subnet256");
        }

    /// <summary>
    ///     Tests Subnet functionality with Subnet9.
    /// </summary>
    [TestMethod]
    public void TestSubnet9()
    {
            var ipnetwork = IPNetwork2.Parse("192.168.0.0/24");
            byte cidr = 32;

            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            Assert.AreEqual(256, subnets.Count, "count");
            Assert.AreEqual("192.168.0.0/32", subnets[0].ToString(), "subnet1");
            Assert.AreEqual("192.168.0.255/32", subnets[255].ToString(), "subnet256");
        }

    /// <summary>
    ///     Tests Subnet functionality with Subnet_Example8.
    /// </summary>
    [TestMethod]
    public void TestSubnet_Example8()
    {
            var wholeInternet = IPNetwork2.Parse("0.0.0.0/0");
            byte newCidr = 2;
            IPNetworkCollection subneted = wholeInternet.Subnet(newCidr);

            Console.WriteLine("{0} was subnetted into {1} subnets", wholeInternet, subneted.Count);
            Console.WriteLine("First: {0}", subneted[0]);
            Console.WriteLine("Last : {0}", subneted[subneted.Count - 1]);
            Console.WriteLine("All  :");

            foreach (IPNetwork2 ipnetwork in subneted)
            {
                Console.WriteLine("{0}", ipnetwork);
            }
        }

    /// <summary>
    ///     Tests Subnet functionality with Subnet10.
    /// </summary>
    [TestMethod]
    public void TestSubnet10()
    {
            var ipnetwork = IPNetwork2.Parse("0.0.0.0/0");
            byte cidr = 32;

            // Here I spawm a OOM dragon ! beware of the beast !
            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            Assert.AreEqual(4294967296, subnets.Count, "count");
            Assert.AreEqual("0.0.0.0/32", subnets[0].ToString(), "subnet1");
            Assert.AreEqual("255.255.255.255/32", subnets[4294967295].ToString(), "subnet256");
        }

    /// <summary>
    ///     Tests Subnet functionality with Subnet12.
    /// </summary>
    [TestMethod]
    public void TestSubnet12()
    {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_CBLK_RESERVED1;
            byte cidr = 20;
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
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestSubnet13()
    {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_CBLK_RESERVED1;
            byte cidr = 20;
            IPNetworkCollection subnets = ipnetwork.Subnet(cidr);
            IPNetwork2 error = subnets[1000];
        }
}