// <copyright file="IPNetworkCollectionCtorTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject
{
    [TestClass]
    public class IPNetworkCollectionCtorTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor1()
    {
            var ipn = new IPNetworkCollection(IPNetwork2.IANA_ABLK_RESERVED1, 33);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCtor2()
    {
            var ipn = new IPNetworkCollection(IPNetwork2.IANA_ABLK_RESERVED1, 2);
        }
    }
}