// <copyright file="HashSetUnitTestHashCodeAdd3.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

[TestClass]
public class HashSetUnitTestHashCodeAdd3
{
    [TestMethod]
    public void TestGetHashCode_HashSet_Add3()
    {
            var ipnetwork1 = IPNetwork2.Parse("0.0.0.0/32");
            var ipnetwork2 = IPNetwork2.Parse("0.0.0.1/32");

            var hashset = new HashSet<IPNetwork2>();
            bool add1 = hashset.Add(ipnetwork1);
            bool add2 = hashset.Add(ipnetwork2);

            Assert.IsTrue(add1, "add1");
            Assert.IsTrue(add2, "add2");
        }
}