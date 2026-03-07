// <copyright file="IPNetworkOverlapTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test overlap.
/// </summary>
[TestClass]
public class IPNetworkOverlapTests
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestOverlap2()
    {
        var network1 = IPNetwork2.Parse("10.0.0.0/0");
        IPNetwork2 network2 = null;
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            network1.Overlap(network2);
        });
    }

    /// <summary>
    ///     Tests Overlap functionality with Overlap3.
    /// </summary>
    [TestMethod]
    public void TestOverlap3()
    {
            var network1 = IPNetwork2.Parse("10.0.0.0/0");
            var network2 = IPNetwork2.Parse("10.0.0.0/0");
            bool result = network1.Overlap(network2);

            Assert.IsTrue(result, "overlap");
        }

    /// <summary>
    ///     Tests Overlap functionality with Overlap4.
    /// </summary>
    [TestMethod]
    public void TestOverlap4()
    {
            var network1 = IPNetwork2.Parse("10.10.0.0/16");
            var network2 = IPNetwork2.Parse("10.10.1.0/24");
            bool result = network1.Overlap(network2);

            Assert.IsTrue(result, "overlap");
        }

    /// <summary>
    ///     Tests Overlap functionality with Overlap5.
    /// </summary>
    [TestMethod]
    public void TestOverlap5()
    {
            var network1 = IPNetwork2.Parse("10.10.0.0/24");
            var network2 = IPNetwork2.Parse("10.10.1.0/24");
            bool result = network1.Overlap(network2);

            Assert.IsFalse(result, "overlap");
        }

    /// <summary>
    ///     Tests Overlap functionality with Overlap6.
    /// </summary>
    [TestMethod]
    public void TestOverlap6()
    {
            var network1 = IPNetwork2.Parse("10.10.1.0/24");
            var network2 = IPNetwork2.Parse("10.10.0.0/16");
            bool result = network1.Overlap(network2);

            Assert.IsTrue(result, "overlap");
        }
}