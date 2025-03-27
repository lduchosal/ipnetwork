// <copyright file="IPNetworkCollectionCurrentTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject
{
    [TestClass]
    public class IPNetworkCollectionCurrentTests
    {
        [TestMethod]
        public void TestCurrent()
        {
            var ipn = IPNetwork2.Parse("192.168.0.0/32");
            using (IPNetworkCollection ipns = ipn.Subnet(32))
            {
                var ipnse = (IEnumerator)ipns;
                ipnse.MoveNext();
                object ipn0 = ipnse.Current;

                Assert.AreEqual("192.168.0.0/32", ipn0.ToString(), "ipn0");
            }
        }
    }
}