using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;

namespace System.Net.TestProject
{
    [TestClass]
    public class GetHashCodeUnitTest
    {
        #region GetHashCode

        [TestMethod]
        public void TestGetHashCode1()
        {
            IPNetwork ipnetwork1 = IPNetwork.Parse("0.0.1.1/0");
            IPNetwork ipnetwork2 = IPNetwork.Parse("0.0.1.1/0");
            int hashCode1 = ipnetwork1.GetHashCode();
            int hashCode2 = ipnetwork2.GetHashCode();
            Assert.AreEqual(hashCode1, hashCode2, "hashcode");
        }

        [TestMethod]
        public void TestGetHashCode2()
        {
            IPNetwork ipnetwork1 = IPNetwork.Parse("0.0.0.0/1");
            IPNetwork ipnetwork2 = IPNetwork.Parse("0.0.0.0/1");
            int hashCode1 = ipnetwork1.GetHashCode();
            int hashCode2 = ipnetwork2.GetHashCode();
            Assert.AreEqual(hashCode1, hashCode2, "hashcode");
        }

        [TestMethod]
        public void TestGetHashCode3()
        {
            IPNetwork ipnetwork1 = IPNetwork.Parse("0.0.0.0/32");
            IPNetwork ipnetwork2 = IPNetwork.Parse("0.0.0.0/32");
            int hashCode1 = ipnetwork1.GetHashCode();
            int hashCode2 = ipnetwork2.GetHashCode();
            Assert.AreEqual(hashCode1, hashCode2, "hashcode");
        }

        #endregion

        [TestMethod]
        public void TestGetHashCode_SameNetwork_DifferentIpAdress1()
        {
            IPNetwork ipnetwork1 = IPNetwork.Parse("0.0.1.1/0");
            IPNetwork ipnetwork2 = IPNetwork.Parse("1.1.1.1/0");
            int hashCode1 = ipnetwork1.GetHashCode();
            int hashCode2 = ipnetwork2.GetHashCode();
            Assert.AreEqual(hashCode1, hashCode2, "hashcode");
        }

        [TestMethod]
        public void TestGetHashCode_SameNetwork_DifferentIpAdress2()
        {
            IPNetwork ipnetwork1 = IPNetwork.Parse("0.0.0.0/1");
            IPNetwork ipnetwork2 = IPNetwork.Parse("1.0.0.0/1");
            int hashCode1 = ipnetwork1.GetHashCode();
            int hashCode2 = ipnetwork2.GetHashCode();
            Assert.AreEqual(hashCode1, hashCode2, "hashcode");
        }

        #region Equals IPv6 vs IPv4

        [TestMethod]
        public void TestGetHashCode_ipv6_ipv4_0()
        {
            IPNetwork network1 = IPNetwork.Parse("::/32");
            IPNetwork network2 = IPNetwork.Parse("0.0.0.0/32");

            int hashCode1 = network1.GetHashCode();
            int hashCode2 = network2.GetHashCode();

            Assert.AreNotEqual(hashCode1, hashCode2, "hashcode");
        }

        #endregion
    }
}
