// <copyright file="SubstractUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class IPNetworkV6SubtractTests
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_IPNetworkV6_Subtract_First()
    {
        // Prepare
        var network1 = IPNetwork2.Parse("::0", 0);
        var network2 = IPNetwork2.Parse("1::1", 64);
        
        // Act
        var result = network1.Subtract(network2);
        
        // Assert
        string expected = "::/16, 1:0:0:1::/64, 1:0:0:2::/63, 1:0:0:4::/62, 1:0:0:8::/61, 1:0:0:10::/60, 1:0:0:20::/59, 1:0:0:40::/58, 1:0:0:80::/57, 1:0:0:100::/56, 1:0:0:200::/55, 1:0:0:400::/54, 1:0:0:800::/53, 1:0:0:1000::/52, 1:0:0:2000::/51, 1:0:0:4000::/50, 1:0:0:8000::/49, 1:0:1::/48, 1:0:2::/47, 1:0:4::/46, 1:0:8::/45, 1:0:10::/44, 1:0:20::/43, 1:0:40::/42, 1:0:80::/41, 1:0:100::/40, 1:0:200::/39, 1:0:400::/38, 1:0:800::/37, 1:0:1000::/36, 1:0:2000::/35, 1:0:4000::/34, 1:0:8000::/33, 1:1::/32, 1:2::/31, 1:4::/30, 1:8::/29, 1:10::/28, 1:20::/27, 1:40::/26, 1:80::/25, 1:100::/24, 1:200::/23, 1:400::/22, 1:800::/21, 1:1000::/20, 1:2000::/19, 1:4000::/18, 1:8000::/17, 2::/15, 4::/14, 8::/13, 10::/12, 20::/11, 40::/10, 80::/9, 100::/8, 200::/7, 400::/6, 800::/5, 1000::/4, 2000::/3, 4000::/2, 8000::/1";
        string ips = string.Join(", ", result);

        Assert.AreEqual(expected, ips);
    }

    /// <summary>
    /// subtract.
    /// </summary>
    [TestMethod]
    [DataRow("::0/0", "10::/32", "::/12, 10:1::/32, 10:2::/31, 10:4::/30, 10:8::/29, 10:10::/28, 10:20::/27, 10:40::/26, 10:80::/25, 10:100::/24, 10:200::/23, 10:400::/22, 10:800::/21, 10:1000::/20, 10:2000::/19, 10:4000::/18, 10:8000::/17, 11::/16, 12::/15, 14::/14, 18::/13, 20::/11, 40::/10, 80::/9, 100::/8, 200::/7, 400::/6, 800::/5, 1000::/4, 2000::/3, 4000::/2, 8000::/1")]
    [DataRow("1::/31", "1::1/32", "1:1::/32")]
    [DataRow("1::/30", "1::1/64", "1:0:0:1::/64, 1:0:0:2::/63, 1:0:0:4::/62, 1:0:0:8::/61, 1:0:0:10::/60, 1:0:0:20::/59, 1:0:0:40::/58, 1:0:0:80::/57, 1:0:0:100::/56, 1:0:0:200::/55, 1:0:0:400::/54, 1:0:0:800::/53, 1:0:0:1000::/52, 1:0:0:2000::/51, 1:0:0:4000::/50, 1:0:0:8000::/49, 1:0:1::/48, 1:0:2::/47, 1:0:4::/46, 1:0:8::/45, 1:0:10::/44, 1:0:20::/43, 1:0:40::/42, 1:0:80::/41, 1:0:100::/40, 1:0:200::/39, 1:0:400::/38, 1:0:800::/37, 1:0:1000::/36, 1:0:2000::/35, 1:0:4000::/34, 1:0:8000::/33, 1:1::/32, 1:2::/31")]
    [DataRow("::/24", "::1/32", "0:1::/32, 0:2::/31, 0:4::/30, 0:8::/29, 0:10::/28, 0:20::/27, 0:40::/26, 0:80::/25")]
    [DataRow("::/24", "1::/24", "::/24")]
    [DataRow("::/24", "::/24", "")]
    [DataRow("::/24", "::/23", "")]
    [DataRow("::/24", "::/0", "")]
    [DataRow("::/0", "::/0", "")]
    [DataRow("::/0", "::/1", "8000::/1")]
    public void Test_IPNetworkV6_Subtract(string ipnetwork1, string ipnetwork2, string expected)
    {
        // Prepare
        var network1 = IPNetwork2.Parse(ipnetwork1);
        var network2 = IPNetwork2.Parse(ipnetwork2);
        
        // Act
        var result = network1.Subtract(network2);
        
        // Assert
        string ips = string.Join(", ", result);

        Assert.AreEqual(expected, ips);
    }
    
    /// <summary>
    /// subtract.
    /// </summary>
    [TestMethod]
    public void Test_IPNetworkV6_Subtract_Exception()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            // Prepare
            var network1 = IPNetwork2.Parse("::0");

            // Act
            network1.Subtract(null);
        });
    }
}