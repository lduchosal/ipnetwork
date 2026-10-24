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
    /// The hash code follows the network: once <see cref="IPNetwork2.Value"/> is rewritten, the
    /// instance hashes like any other instance of the new network, as the Equals/GetHashCode
    /// contract requires.
    /// </summary>
    [TestMethod]
    public void TestGetHashCode_FollowsValue()
    {
        var ipnetwork = IPNetwork2.Parse("1.0.0.0/1");
        int hashCode1 = ipnetwork.GetHashCode();
        ipnetwork.Value = "255.255.255.255/32";
        int hashCode2 = ipnetwork.GetHashCode();
        int expected = IPNetwork2.Parse("255.255.255.255/32").GetHashCode();

        Assert.AreNotEqual(hashCode1, hashCode2, "hashcode");
        Assert.AreEqual(expected, hashCode2, "hashcode");
    }

    /// <summary>
    /// Like any mutable key, a network rewritten after being added to a dictionary can no longer be
    /// found there, neither by itself nor by an instance equal to its former value.
    /// </summary>
    [TestMethod]
    public void TestGetHashCode_Dictionary_MutatedKeyIsLost()
    {
        var ipnetwork = IPNetwork2.Parse("1.0.0.0/1");
        var ipnetwork2 = IPNetwork2.Parse("1.0.0.0/1");
        var dic = new Dictionary<IPNetwork2, int>
        {
            { ipnetwork, 0 },
        };
        bool contains3 = dic.ContainsKey(ipnetwork);
        bool contains4 = dic.ContainsKey(ipnetwork2);

        ipnetwork.Value = "255.255.255.255/32";
        bool contains5 = dic.ContainsKey(ipnetwork);
        bool contains6 = dic.ContainsKey(ipnetwork2);

        Assert.IsTrue(contains3, "contains3");
        Assert.IsTrue(contains4, "contains4");
        Assert.IsFalse(contains5, "contains5");
        Assert.IsFalse(contains6, "contains6");
    }

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
}