// <copyright file="IPNetworkUsableTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class IPNetworkUsableTests
{
    /// <summary>
    ///     Tests Usable functionality with a /32 network.
    /// </summary>
    [TestMethod]
    [DataRow("0.0.0.0/32", (uint)1)]
    [DataRow("0.0.0.0/31", (uint)2)]
    [DataRow("0.0.0.0/30", (uint)2)]
    [DataRow("0.0.0.0/24", (uint)254)]
    [DataRow("0.0.0.0/8", (uint)16777214)]
    [DataRow("0.0.0.0/16", (uint)65534)]
    [DataRow("0.0.0.0/0", 4294967294)]
    public void TestUsable(string ipnetwork, uint usable)
    {
        var network = IPNetwork2.Parse(ipnetwork);
        Assert.AreEqual(usable, network.Usable, "Usable");
    }
}