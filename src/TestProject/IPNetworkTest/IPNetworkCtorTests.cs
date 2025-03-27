// <copyright file="IPNetworkCtorTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest
{
    [TestClass]
    public class IPNetworkCtorTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor1()
    {
        new IPNetwork2(BigInteger.Zero, AddressFamily.InterNetwork, 33);
    }
    }
}