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
    public void TestCtor1()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new IPNetworkCollection(IPNetwork2.IANA_ABLK_RESERVED1, 33));
    }

    /// <summary>
    /// Test with invalid params.
    /// </summary>
    [TestMethod]
    public void TestCtor2()
    {
        Assert.ThrowsExactly<ArgumentException>(() => new IPNetworkCollection(IPNetwork2.IANA_ABLK_RESERVED1, 2));
    }
}