// <copyright file="HashSetUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class HashSetUnitTest
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestGetHashCode_HashSet_Add1()
    {
            var ipnetwork1 = IPNetwork2.Parse("0.0.1.1/0");
            var ipnetwork2 = IPNetwork2.Parse("1.1.1.1/0");

            var hashset = new HashSet<IPNetwork2>();
            bool add1 = hashset.Add(ipnetwork1);
            bool add2 = hashset.Add(ipnetwork2);

            Assert.IsTrue(add1, "add1");
            Assert.IsFalse(add2, "add2");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestGetHashCode_HashSet_Add2()
    {
            var ipnetwork1 = IPNetwork2.Parse("0.0.0.0/1");
            var ipnetwork2 = IPNetwork2.Parse("1.0.0.0/1");

            var hashset = new HashSet<IPNetwork2>();
            bool add1 = hashset.Add(ipnetwork1);
            bool add2 = hashset.Add(ipnetwork2);

            Assert.IsTrue(add1, "add1");
            Assert.IsFalse(add2, "add2");
        }
}