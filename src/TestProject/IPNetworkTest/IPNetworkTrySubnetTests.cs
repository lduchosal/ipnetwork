// <copyright file="IPNetworkTrySubnetTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest
{
    [TestClass]
    public class IPNetworkTrySubnetTests
    {
        /// <summary>
        ///     Tests Try Subnet functionality with Internal Subnet1.
        /// </summary>
        [TestMethod]
        public void TestInternalSubnet1()
    {
            IPNetworkCollection subnets = null;
            IPNetwork2.InternalSubnet(true, null, 0, out subnets);
            Assert.AreEqual(null, subnets, "subnets");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInternalSubnet2()
    {
            IPNetworkCollection subnets = null;
            IPNetwork2.InternalSubnet(false, null, 0, out subnets);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestTrySubnet1()
    {
            IPNetwork2 ipnetwork = null;
            byte cidr = 9;

            IPNetworkCollection subnets = null;
#pragma warning disable 0618
            bool subnetted = IPNetwork2.TrySubnet(ipnetwork, cidr, out subnets);
#pragma warning restore 0618
        }

        /// <summary>
        ///     Tests Try Subnet functionality with Try Subnet Static1.
        /// </summary>
        [TestMethod]
        public void TestTrySubnetStatic1()
    {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_ABLK_RESERVED1;
            byte cidr = 9;

            IPNetworkCollection subnets = null;
#pragma warning disable 0618
            bool subnetted = IPNetwork2.TrySubnet(ipnetwork, cidr, out subnets);
#pragma warning restore 0618
        }

        /// <summary>
        ///     Tests Try Subnet functionality with Try Subnet3.
        /// </summary>
        [TestMethod]
        public void TestTrySubnet3()
    {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_ABLK_RESERVED1;
            byte cidr = 55;

            IPNetworkCollection subnets = null;
            bool subnetted = ipnetwork.TrySubnet(cidr, out subnets);

            Assert.AreEqual(false, subnetted, "subnetted");
        }

        /// <summary>
        ///     Tests Try Subnet functionality with Try Subnet4.
        /// </summary>
        [TestMethod]
        public void TestTrySubnet4()
    {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_ABLK_RESERVED1;
            byte cidr = 1;

            IPNetworkCollection subnets = null;
            bool subnetted = ipnetwork.TrySubnet(cidr, out subnets);

            Assert.AreEqual(false, subnetted, "subnetted");
        }

        /// <summary>
        ///     Tests Try Subnet functionality with Try Subnet5.
        /// </summary>
        [TestMethod]
        public void TestTrySubnet5()
    {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_ABLK_RESERVED1;
            byte cidr = 9;

            IPNetworkCollection subnets = null;
            bool subnetted = ipnetwork.TrySubnet(cidr, out subnets);

            Assert.AreEqual(true, subnetted, "subnetted");
            Assert.AreEqual(2, subnets.Count, "count");
            Assert.AreEqual("10.0.0.0/9", subnets[0].ToString(), "subnet1");
            Assert.AreEqual("10.128.0.0/9", subnets[1].ToString(), "subnet2");
        }

        /// <summary>
        ///     Tests Try Subnet functionality with Try Subnet6.
        /// </summary>
        [TestMethod]
        public void TestTrySubnet6()
    {
            IPNetwork2 ipnetwork = IPNetwork2.IANA_CBLK_RESERVED1;
            byte cidr = 20;

            IPNetworkCollection subnets = null;
            bool subnetted = ipnetwork.TrySubnet(cidr, out subnets);

            Assert.AreEqual(true, subnetted, "subnetted");
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
    }
}