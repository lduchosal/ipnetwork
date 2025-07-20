// <copyright file="IPNetworkCtorTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test Ctor.
/// </summary>
[TestClass]
public class IPNetworkCtorTests
{
    /// <summary>
    /// Test Ctor with too big of a CIDR.
    /// </summary>
    [TestMethod]
    public void TestCtor1()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
        {
            new IPNetwork2(BigInteger.Zero, AddressFamily.InterNetwork, 33);
        });
    }
}