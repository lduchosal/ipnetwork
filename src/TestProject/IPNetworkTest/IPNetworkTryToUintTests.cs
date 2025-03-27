// <copyright file="IPNetworkTryToUintTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest
{
    [TestClass]
    public class IPNetworkTryToUintTests
    {
        /// <summary>
        ///     Tests Try To Uint functionality with Try To Uint1.
        /// </summary>
        [TestMethod]
        public void TestTryToUint1()
        {
            BigInteger? result = null;
            bool parsed = IPNetwork2.TryToUint(32, AddressFamily.InterNetwork, out result);

            Assert.IsNotNull(result, "uint");
            Assert.AreEqual(true, parsed, "parsed");
        }
    }
}