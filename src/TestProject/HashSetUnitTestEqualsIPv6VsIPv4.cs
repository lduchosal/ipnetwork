// <copyright file="HashSetUnitTestEqualsIPv6VsIPv4.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class HashSetUnitTestEqualsIPv6VsIPv4
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestHashSet_Add_ipv6_ipv4_0()
    {
            var ipnetwork1 = IPNetwork2.Parse("::/32");
            var ipnetwork2 = IPNetwork2.Parse("0.0.0.0/32");

            var hashset = new HashSet<IPNetwork2>();
            bool add1 = hashset.Add(ipnetwork1);
            bool add2 = hashset.Add(ipnetwork2);

            Assert.IsTrue(add1, "add1");
            Assert.IsTrue(add2, "add2");
        }
}