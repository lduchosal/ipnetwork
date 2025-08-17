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
    ///     Tests Operator functionality with Operator Greater.
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
    ///     Tests Operator functionality with Operator Greater.
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
    ///     Tests Operator functionality with Operator Greater.
    /// </summary>
    [TestMethod]
    public void TestOperatorGreater3()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.1/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.1/32");

        bool greater = ipn1 > ipn2;

        Assert.IsFalse(greater, "greater");
    }
    
    
    /// <summary>
    ///     Tests Operator functionality with Operator Greater.
    /// </summary>
    [TestMethod]
    public void TestOperatorGreater4()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.0/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.0/24");

        bool greater = ipn1 > ipn2;

        Assert.IsFalse(greater, "greater");
    }

    /// <summary>
    ///     Tests Operator functionality with Operator Greater.
    /// </summary>
    [TestMethod]
    public void TestOperatorGreaterOrEqual1()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.1/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

        bool greater = ipn1 >= ipn2;

        Assert.IsFalse(greater, "greater");
    }
    
    /// <summary>
    ///     Tests Operator functionality with Operator Greater.
    /// </summary>
    [TestMethod]
    public void TestOperatorGreaterOrEqual2()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.100/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

        bool greater = ipn1 >= ipn2;

        Assert.IsTrue(greater, "greater");
    }
    
    /// <summary>
    ///     Tests Operator functionality with Operator Greater.
    /// </summary>
    [TestMethod]
    public void TestOperatorGreaterOrEqual3()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.1/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.1/32");

        bool greater = ipn1 >= ipn2;

        Assert.IsTrue(greater, "greater");
    }
    
    /// <summary>
    /// Tests Operator functionality with Operator Greater.
    /// </summary>
    [TestMethod]
    public void TestOperatorGreaterOrEqual4()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.0/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.0/24");

        bool greater = ipn1 >= ipn2;

        Assert.IsFalse(greater, "greater");
    }
    
    /// <summary>
    /// Tests Operator functionality with Operator Greater.
    /// </summary>
    [TestMethod]
    public void TestOperatorGreaterOrEqual5()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.1/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.1/24");

        bool greater = ipn1 >= ipn2;

        Assert.IsTrue(greater, "greater");
    }

    /// <summary>
    ///     Tests Operator functionality with Operator Lower.
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
    ///     Tests Operator functionality with Operator Lower.
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
    /// Tests Operator functionality with Operator Lower.
    /// </summary>
    [TestMethod]
    public void TestOperatorLower3()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.0/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.0/32");

        bool lower = ipn1 < ipn2;

        Assert.IsFalse(lower, "lower");
    }

    /// <summary>
    ///     Tests Operator functionality with Operator Lower.
    /// </summary>
    [TestMethod]
    public void TestOperatorLowerOrEqual1()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.1/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

        bool lower = ipn1 <= ipn2;

        Assert.IsTrue(lower, "lower");
    }

    /// <summary>
    ///     Tests Operator functionality with Operator Lower.
    /// </summary>
    [TestMethod]
    public void TestOperatorLowerOrEqual2()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.100/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.2/32");

        bool lower = ipn1 <= ipn2;

        Assert.IsFalse(lower, "lower");
    }

    /// <summary>
    /// Tests Operator functionality with Operator Lower.
    /// </summary>
    [TestMethod]
    public void TestOperatorLowerOrEqual3()
    {
        var ipn1 = IPNetwork2.Parse("10.0.0.0/32");
        var ipn2 = IPNetwork2.Parse("10.0.0.0/32");

        bool lower = ipn1 <= ipn2;

        Assert.IsTrue(lower, "lower");
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
    
    /// <summary>
    /// Tests Operator functionality with Operator -.
    /// </summary>
    [TestMethod]
    [DataRow("10.0.0.1/32", "10.0.0.1/32", 0)]
    [DataRow("10.0.0.1/24", "10.0.0.1/25", 1)]
    [DataRow("10.0.0.1/24", "10.0.0.1/26", 2)]
    [DataRow("10.0.0.1/24", "10.0.0.1/27", 3)]
    [DataRow("10.0.0.1/24", "10.0.0.1/28", 4)]
    [DataRow("10.0.0.1/24", "10.0.0.1/29", 5)]
    [DataRow("10.0.0.1/24", "10.0.0.1/30", 6)]
    [DataRow("10.0.0.1/24", "10.0.0.1/31", 7)]
    [DataRow("10.0.0.1/24", "10.0.0.1/32", 8)]
    [DataRow("0.0.0.0/0", "0.0.0.0/32", 32)]
    public void TestOperatorSubtract(string left, string right, int count)
    {
        var ipn1 = IPNetwork2.Parse(left);
        var ipn2 = IPNetwork2.Parse(right);

        var result = ipn1 - ipn2;

        Assert.HasCount(count, result, "subtract");
    }
    
    
    /// <summary>
    /// Tests Operator functionality with Operator -.
    /// </summary>
    [TestMethod]
    [DataRow("10.0.0.0/32", "10.0.0.1/32", 1)]
    [DataRow("10.0.0.0/32", "10.0.0.2/32", 2)]
    [DataRow("10.0.0.0/32", "10.0.0.3/32", 2)]
    [DataRow("10.0.0.0/24", "10.0.1.3/32", 2)]
    [DataRow("10.0.0.0/24", "10.0.1.3/24", 1)]
    [DataRow("10.0.0.1/32", "10.0.0.1/32", 1)]
    [DataRow("10.0.0.1/24", "10.0.0.1/25", 1)]
    [DataRow("10.0.0.1/24", "10.0.0.1/26", 1)]
    [DataRow("10.0.0.1/24", "10.0.0.1/27", 1)]
    [DataRow("10.0.0.1/24", "10.0.0.1/28", 1)]
    [DataRow("10.0.0.1/24", "10.0.0.1/29", 1)]
    [DataRow("10.0.0.1/24", "10.0.0.1/30", 1)]
    [DataRow("10.0.0.1/24", "10.0.0.1/31", 1)]
    [DataRow("10.0.0.1/24", "10.0.0.1/32", 1)]
    [DataRow("0.0.0.0/0", "0.0.0.0/32", 1)]
    public void TestOperatorAdd(string left, string right, int count)
    {
        var ipn1 = IPNetwork2.Parse(left);
        var ipn2 = IPNetwork2.Parse(right);

        var result = ipn1 + ipn2;

        Assert.HasCount(count, result, "add");
    }
    
    /// <summary>
    /// Tests Operator functionality with Operator -.
    /// </summary>
    [TestMethod]
    [DataRow("10.0.0.0/32", 1, "10.0.0.0/31")]
    [DataRow("10.0.0.0/32", 3, "10.0.0.0/30")]
    [DataRow("10.0.0.0/32", 7, "10.0.0.0/29")]
    [DataRow("10.0.0.0/32", 15, "10.0.0.0/28")]
    [DataRow("10.0.0.0/32", 31, "10.0.0.0/27")]
    [DataRow("10.0.0.0/32", 63, "10.0.0.0/26")]
    [DataRow("10.0.0.0/32", 127, "10.0.0.0/25")]
    [DataRow("10.0.0.0/32", 255, "10.0.0.0/24")]
    [DataRow("10.0.0.0/32", 511, "10.0.0.0/23")]
    [DataRow("10.0.0.0/32", 1023, "10.0.0.0/22")]
    [DataRow("10.0.0.0/32", 2047, "10.0.0.0/21")]
    [DataRow("10.0.0.0/32", 4095, "10.0.0.0/20")]
    [DataRow("10.0.0.0/32", -1, "9.255.255.255/32, 10.0.0.0/32")]
    [DataRow("10.0.0.0/32", -10, "9.255.255.246/31, 9.255.255.248/29, 10.0.0.0/32")]
    [DataRow("10.0.0.0/32", 2, "10.0.0.0/31, 10.0.0.2/32")]
    [DataRow("::/128", 1, "::/127")]
    [DataRow("::/128", 3, "::/126")]
    [DataRow("::f/128", 1, "::f/128, ::10/128")]
    [DataRow("1::ffff/128", 1, "1::ffff/128, 1::1:0/128")]
    public void TestOperatorAdd2(string left, int right, string expected)
    {
        var ipn1 = IPNetwork2.Parse(left);
        var result = ipn1 + right;
        string sresult = string.Join(", ", result);

        Assert.AreEqual(expected, sresult);
    }
}