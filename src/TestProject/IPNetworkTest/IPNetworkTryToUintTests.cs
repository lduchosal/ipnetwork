// <copyright file="IPNetworkTryToUintTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class IPNetworkTryToUintTests
{
    /// <summary>
    ///     Tests Try To Uint functionality with Try To Uint1.
    /// </summary>
    [TestMethod]
    public void TestTryToUint1()
    {
        bool parsed = IPNetwork2.TryToUint(32, AddressFamily.InterNetwork, out BigInteger result);

        Assert.IsTrue(parsed, "parsed");
        Assert.AreNotEqual(default(BigInteger), result, "uint");
    }
}