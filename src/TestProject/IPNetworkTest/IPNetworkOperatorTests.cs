// <copyright file="IPNetworkOperatorTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Tests with operators.
/// </summary>
[TestClass]
public class IPNetworkOperatorTests
{
    /// <summary>
    ///     Tests Operator functionality with Operator Greater1.
    /// </summary>
    [TestMethod]
    public void TestOperatorGreater1()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.1/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

        bool greater = ipn1 > ipn2;

        Assert.IsFalse(greater, "greater");
    }

    /// <summary>
    ///     Tests Operator functionality with Operator Greater2.
    /// </summary>
    [TestMethod]
    public void TestOperatorGreater2()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.100/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

        bool greater = ipn1 > ipn2;

        Assert.IsTrue(greater, "greater");
    }

    /// <summary>
    ///     Tests Operator functionality with Operator Lower1.
    /// </summary>
    [TestMethod]
    public void TestOperatorLower1()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.1/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

        bool lower = ipn1 < ipn2;

        Assert.IsTrue(lower, "lower");
    }

    /// <summary>
    ///     Tests Operator functionality with Operator Lower2.
    /// </summary>
    [TestMethod]
    public void TestOperatorLower2()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.100/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

        bool lower = ipn1 < ipn2;

        Assert.IsFalse(lower, "lower");
    }

    /// <summary>
    ///     Tests Operator functionality with Operator Different1.
    /// </summary>
    [TestMethod]
    public void TestOperatorDifferent1()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.100/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

        bool different = ipn1 != ipn2;

        Assert.IsTrue(different, "different");
    }

    /// <summary>
    ///     Tests Operator functionality with Operator Different2.
    /// </summary>
    [TestMethod]
    public void TestOperatorDifferent2()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.1/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.1/32");

        bool different = ipn1 != ipn2;

        Assert.IsFalse(different, "different");
    }

    /// <summary>
    ///     Tests Operator functionality with Operator Equal1.
    /// </summary>
    [TestMethod]
    public void TestOperatorEqual1()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.100/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

        bool eq = ipn1 == ipn2;

        Assert.IsFalse(eq, "eq");
    }

    /// <summary>
    ///     Tests Operator functionality with Operator Equal2.
    /// </summary>
    [TestMethod]
    public void TestOperatorEqual2()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.1/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.1/32");

        bool eq = ipn1 == ipn2;

        Assert.IsTrue(eq, "eq");
    }
}