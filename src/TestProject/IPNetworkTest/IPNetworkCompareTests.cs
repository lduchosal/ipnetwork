// <copyright file="IPNetworkCompareTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test compare methods.
/// </summary>
[TestClass]
public class IPNetworkCompareTests
{
    /// <summary>
    ///     Tests Compare functionality with Compare To1.
    /// </summary>
    [TestMethod]
    public void TestCompareTo1()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
        var ipn2 = IPNetwork2.Parse("10.0.0.2/16");

        int comparison = ipn1.CompareTo(ipn2);

        Assert.AreEqual(0, comparison, "compare");
    }

    /// <summary>
    ///     Tests Compare functionality with Compare To2.
    /// </summary>
    [TestMethod]
    public void TestCompareTo2()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
        object ipn2 = IPNetwork2.Parse("10.0.0.2/16");

        int comparison = ipn1.CompareTo(ipn2);

        Assert.AreEqual(0, comparison, "compare");
    }

    /// <summary>
    ///     Tests Compare functionality with Compare To3.
    /// </summary>
    [TestMethod]
    public void TestCompareTo3()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
        object ipn2 = null;

        int comparison = ipn1.CompareTo(ipn2);

        Assert.AreEqual(1, comparison, "compare");
    }

    /// <summary>
    ///     Tests Compare functionality with Compare To4.
    /// </summary>
    [TestMethod]
    public void TestCompareTo4()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
        IPNetwork2 ipn2 = null;

        int comparison = ipn1.CompareTo(ipn2);

        Assert.AreEqual(1, comparison, "compare");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestCompareTo5()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
        string ipn2 = string.Empty;

        ipn1.CompareTo(ipn2);
    }

    /// <summary>
    ///     Tests Compare functionality with Compare To6.
    /// </summary>
    [TestMethod]
    public void TestCompareTo6()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
        int comparison = ipn1.CompareTo(ipn1);

        Assert.AreEqual(0, comparison, "compare");
    }

    /// <summary>
    ///     Tests Compare functionality with Compare1.
    /// </summary>
    [TestMethod]
    public void TestCompare1()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
        int comparison = IPNetwork2.Compare(null, ipn1);

        Assert.AreEqual(-1, comparison, "compare");
    }

    /// <summary>
    ///     Tests Compare functionality with Compare2.
    /// </summary>
    [TestMethod]
    public void TestCompare2()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.1/16");
        var ipn2 = IPNetwork2.Parse("20.0.0.1/16");
        int comparison = IPNetwork2.Compare(ipn1, ipn2);

        Assert.AreEqual(-1, comparison, "compare");
    }
}