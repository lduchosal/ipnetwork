// <copyright file="GetHashCodeUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>


namespace TestProject;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class GetHashCodeUnitTest
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestGetHashCode_SameNetwork_DifferentIpAddress1()
    {
            var ipnetwork1 = IPNetwork2.Parse("0.0.1.1/0");
            var ipnetwork2 = IPNetwork2.Parse("1.1.1.1/0");
            int hashCode1 = ipnetwork1.GetHashCode();
            int hashCode2 = ipnetwork2.GetHashCode();
            Assert.AreEqual(hashCode1, hashCode2, "hashcode");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestGetHashCode_SameNetwork_DifferentIpAddress2()
    {
            var ipnetwork1 = IPNetwork2.Parse("0.0.0.0/1");
            var ipnetwork2 = IPNetwork2.Parse("1.0.0.0/1");
            int hashCode1 = ipnetwork1.GetHashCode();
            int hashCode2 = ipnetwork2.GetHashCode();
            Assert.AreEqual(hashCode1, hashCode2, "hashcode");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestGetHashCode_Refactor__to_not_reference_mutable_fields()
    {
            var ipnetwork = IPNetwork2.Parse("1.0.0.0/1");
            int hashCode1 = ipnetwork.GetHashCode();
            ipnetwork.Value = "255.255.255.255/32";
            int hashCode2 = ipnetwork.GetHashCode();

            Assert.AreEqual(hashCode1, hashCode2, "hashcode");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestGetHashCode_Dictionary_failed()
    {
            var ipnetwork = IPNetwork2.Parse("1.0.0.0/1");
            var ipnetwork2 = IPNetwork2.Parse("1.0.0.0/1");
            var dic = new Dictionary<IPNetwork2, int>();
            bool contains1 = dic.ContainsKey(ipnetwork);
            bool contains2 = dic.ContainsKey(ipnetwork2);

            dic.Add(ipnetwork, 0);
            bool contains3 = dic.ContainsKey(ipnetwork);
            bool contains4 = dic.ContainsKey(ipnetwork2);

            ipnetwork.Value = "255.255.255.255/32";
            bool contains5 = dic.ContainsKey(ipnetwork);
            bool contains6 = dic.ContainsKey(ipnetwork2);

            Assert.AreEqual(false, contains1, "contains1");
            Assert.AreEqual(false, contains2, "contains2");
            Assert.AreEqual(true, contains3, "contains3");
            Assert.AreEqual(true, contains4, "contains4");
            Assert.AreEqual(true, contains5, "contains5");
            Assert.AreEqual(false, contains6, "contains6");
        }

    #region Equals IPv6 vs IPv4

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestGetHashCode_ipv6_ipv4_0()
    {
            var network1 = IPNetwork2.Parse("::/32");
            var network2 = IPNetwork2.Parse("0.0.0.0/32");

            int hashCode1 = network1.GetHashCode();
            int hashCode2 = network2.GetHashCode();

            Assert.AreNotEqual(hashCode1, hashCode2, "hashcode");
        }

    #endregion

    #region GetHashCode

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestGetHashCode1()
    {
            var ipnetwork1 = IPNetwork2.Parse("0.0.1.1/0");
            var ipnetwork2 = IPNetwork2.Parse("0.0.1.1/0");
            int hashCode1 = ipnetwork1.GetHashCode();
            int hashCode2 = ipnetwork2.GetHashCode();
            Assert.AreEqual(hashCode1, hashCode2, "hashcode");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestGetHashCode2()
    {
            var ipnetwork1 = IPNetwork2.Parse("0.0.0.0/1");
            var ipnetwork2 = IPNetwork2.Parse("0.0.0.0/1");
            int hashCode1 = ipnetwork1.GetHashCode();
            int hashCode2 = ipnetwork2.GetHashCode();
            Assert.AreEqual(hashCode1, hashCode2, "hashcode");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestGetHashCode3()
    {
            var ipnetwork1 = IPNetwork2.Parse("0.0.0.0/32");
            var ipnetwork2 = IPNetwork2.Parse("0.0.0.0/32");
            int hashCode1 = ipnetwork1.GetHashCode();
            int hashCode2 = ipnetwork2.GetHashCode();
            Assert.AreEqual(hashCode1, hashCode2, "hashcode");
        }

    #endregion
}