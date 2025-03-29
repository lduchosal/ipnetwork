// <copyright file="IPNetworkCollectionMoveNextTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class IPNetworkCollectionMoveNextTests
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void MoveNext1()
    {
            var ipn = IPNetwork2.Parse("192.168.1.0/30");
            using (IPNetworkCollection ipns = ipn.Subnet(32))
            {
                bool next = ipns.MoveNext();
                Assert.IsTrue(next, "next");
            }
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void MoveNext2()
    {
            var ipn = IPNetwork2.Parse("192.168.1.0/30");
            using (IPNetworkCollection ipns = ipn.Subnet(32))
            {
                ipns.MoveNext();
                ipns.MoveNext();
                ipns.MoveNext();
                ipns.MoveNext();
                bool next = ipns.MoveNext();

                Assert.IsFalse(next, "next");
            }
        }
}