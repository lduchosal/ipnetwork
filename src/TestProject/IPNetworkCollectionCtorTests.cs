// <copyright file="IPNetworkCollectionCtorTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

/// <summary>
/// Test constructor af IPNetworkCollection.
/// </summary>
[TestClass]
public class IPNetworkCollectionCtorTests
{
    /// <summary>
    /// Test ctor with too big cidr.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestCtor1()
    {
    }

    /// <summary>
    /// Test with invalid params.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestCtor2()
    {
    }
}