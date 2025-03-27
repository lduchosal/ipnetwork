// <copyright file="IPNetworkCollectionResetTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject
{
    [TestClass]
    public class IPNetworkCollectionResetTests
    {
        [TestMethod]
        public void TestReset1()
        {
            var ipn = IPNetwork2.Parse("192.168.1.0/29");
            using (IPNetworkCollection ipns = ipn.Subnet(32))
            {
                IPNetwork2 ipn0 = ipns.Current;
                ipns.MoveNext();
                ipns.Reset();
                IPNetwork2 ipn1 = ipns.Current;

                Assert.AreEqual(ipn0, ipn1, "reset");
            }
        }
    }
}