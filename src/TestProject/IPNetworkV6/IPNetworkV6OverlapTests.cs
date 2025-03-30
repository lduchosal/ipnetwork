// <copyright file="IPNetworkV6OverlapTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// Overlap.
/// </summary>
public class IPNetworkV6OverlapTests
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestOverlap2()
    {
        var network1 = IPNetwork2.Parse("2001:0db8::/0");
        IPNetwork2 network2 = null;
        network1.Overlap(network2);
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestOverlap3()
    {
        var network1 = IPNetwork2.Parse("2001:0db8::/64");
        var network2 = IPNetwork2.Parse("2001:0db8::/64");
        bool result = network1.Overlap(network2);

        Assert.IsTrue(result, "overlap");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestOverlap4()
    {
        var network1 = IPNetwork2.Parse("2001:0db8::/64");
        var network2 = IPNetwork2.Parse("2001:0db8:0:0:1::/65");
        bool result = network1.Overlap(network2);

        Assert.IsTrue(result, "overlap");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestOverlap5()
    {
        var network1 = IPNetwork2.Parse("2001:0db8:0:1::/68");
        var network2 = IPNetwork2.Parse("2001:0db8:0:2::/68");
        bool result = network1.Overlap(network2);

        Assert.IsFalse(result, "overlap");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestOverlap6()
    {
        var network1 = IPNetwork2.Parse("2001:0db8:0:1::/68");
        var network2 = IPNetwork2.Parse("2001:0db8:0:2::/62");
        bool result = network1.Overlap(network2);

        Assert.IsTrue(result, "overlap");
    }
}
