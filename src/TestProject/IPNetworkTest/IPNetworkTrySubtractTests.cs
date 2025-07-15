// <copyright file="SubstractUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class IPNetworkTrySubtractTests
{
    
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_IPNetwork_TrySubtract_First()
    {
        // Prepare
        var network1 = IPNetwork2.Parse("0.0.0.0", 0);
        var network2 = IPNetwork2.Parse("10.0.0.1", 32);
        
        // Act
        bool subtracted = network1.TrySubtract(network2, out var result);
        
        // Assert
        string expected = "0.0.0.0/5, 8.0.0.0/7, 10.0.0.0/32, 10.0.0.2/31, 10.0.0.4/30, 10.0.0.8/29, 10.0.0.16/28, 10.0.0.32/27, 10.0.0.64/26, 10.0.0.128/25, 10.0.1.0/24, 10.0.2.0/23, 10.0.4.0/22, 10.0.8.0/21, 10.0.16.0/20, 10.0.32.0/19, 10.0.64.0/18, 10.0.128.0/17, 10.1.0.0/16, 10.2.0.0/15, 10.4.0.0/14, 10.8.0.0/13, 10.16.0.0/12, 10.32.0.0/11, 10.64.0.0/10, 10.128.0.0/9, 11.0.0.0/8, 12.0.0.0/6, 16.0.0.0/4, 32.0.0.0/3, 64.0.0.0/2, 128.0.0.0/1";
        string ips = string.Join(", ", result);

        Assert.AreEqual(true, subtracted);
        Assert.AreEqual(expected, ips);
    }

    /// <summary>
    /// subtract.
    /// </summary>
    [DataTestMethod]
    [DataRow("0.0.0.0/0", "10.0.0.1/32", "0.0.0.0/5, 8.0.0.0/7, 10.0.0.0/32, 10.0.0.2/31, 10.0.0.4/30, 10.0.0.8/29, 10.0.0.16/28, 10.0.0.32/27, 10.0.0.64/26, 10.0.0.128/25, 10.0.1.0/24, 10.0.2.0/23, 10.0.4.0/22, 10.0.8.0/21, 10.0.16.0/20, 10.0.32.0/19, 10.0.64.0/18, 10.0.128.0/17, 10.1.0.0/16, 10.2.0.0/15, 10.4.0.0/14, 10.8.0.0/13, 10.16.0.0/12, 10.32.0.0/11, 10.64.0.0/10, 10.128.0.0/9, 11.0.0.0/8, 12.0.0.0/6, 16.0.0.0/4, 32.0.0.0/3, 64.0.0.0/2, 128.0.0.0/1")]
    [DataRow("1.1.1.0/31", "1.1.1.1/32", "1.1.1.0/32")]
    [DataRow("1.1.1.0/30", "1.1.1.1/32", "1.1.1.0/32, 1.1.1.2/31")]
    [DataRow("0.0.0.0/24", "0.0.0.1/32", "0.0.0.0/32, 0.0.0.2/31, 0.0.0.4/30, 0.0.0.8/29, 0.0.0.16/28, 0.0.0.32/27, 0.0.0.64/26, 0.0.0.128/25")]
    [DataRow("0.0.0.0/24", "1.0.0.0/24", "0.0.0.0/24")]
    [DataRow("0.0.0.0/24", "0.0.0.0/24", "")]
    [DataRow("0.0.0.0/24", "0.0.0.0/23", "")]
    [DataRow("0.0.0.0/24", "0.0.0.0/0", "")]
    [DataRow("0.0.0.0/0", "0.0.0.0/0", "")]
    public void Test_IPNetwork_TrySubtract(string ipnetwork1, string ipnetwork2, string expected)
    {
        // Prepare
        var network1 = IPNetwork2.Parse(ipnetwork1);
        var network2 = IPNetwork2.Parse(ipnetwork2);
        
        // Act
        bool subtracted = network1.TrySubtract(network2, out var result);
        
        // Assert
        string ips = string.Join(", ", result);

        Assert.AreEqual(true, subtracted);
        Assert.AreEqual(expected, ips);
    }
    
    /// <summary>
    /// subtract.
    /// </summary>
    [TestMethod]
    public void Test_IPNetwork_TrySubtract_Failed()
    {
        // Prepare
        var network1 = IPNetwork2.Parse("0.0.0.0");

        // Act
        bool subtracted = network1.TrySubtract(null, out var result);
        Assert.AreEqual(false, subtracted);
        Assert.IsNull(result);

    }
}