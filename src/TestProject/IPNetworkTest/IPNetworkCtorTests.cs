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
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestCtor1()
    {
        new IPNetwork2(BigInteger.Zero, AddressFamily.InterNetwork, 33);
    }
}