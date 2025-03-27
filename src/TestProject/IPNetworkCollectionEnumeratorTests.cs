// <copyright file="IPNetworkCollectionEnumeratorTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject
{
    [TestClass]
    public class IPNetworkCollectionEnumeratorTests
    {
        [TestMethod]
        public void TestEnumerator()
        {
            var ipn = IPNetwork2.Parse("192.168.0.0/32");
            using (IPNetworkCollection ipns = ipn.Subnet(32))
            {
                var ipnse = (IEnumerable)ipns;
                IEnumerator ee = ipnse.GetEnumerator();
                ee.MoveNext();
                object ipn0 = ee.Current;
                Assert.AreEqual("192.168.0.0/32", ipn0.ToString(), "ipn0");
            }
        }
    }
}