// <copyright file="IPNetworkCollectionUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net.TestProject;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class IPNetworkCollectionUnitTest
{
    #region ctor

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

    #endregion

    #region Current

    [TestMethod]
    public void TestCurrent()
    {
            var ipn = IPNetwork2.Parse("192.168.0.0/32");
            using (IPNetworkCollection ipns = ipn.Subnet(32))
            {
                var ipnse = (Collections.IEnumerator)ipns;
                ipnse.MoveNext();
                object ipn0 = ipnse.Current;

                Assert.AreEqual("192.168.0.0/32", ipn0.ToString(), "ipn0");
            }
        }

    #endregion

    #region Enumerator

    [TestMethod]
    public void TestEnumerator()
    {
            var ipn = IPNetwork2.Parse("192.168.0.0/32");
            using (IPNetworkCollection ipns = ipn.Subnet(32))
            {
                var ipnse = (Collections.IEnumerable)ipns;
                Collections.IEnumerator ee = ipnse.GetEnumerator();
                ee.MoveNext();
                object ipn0 = ee.Current;
                Assert.AreEqual("192.168.0.0/32", ipn0.ToString(), "ipn0");
            }
        }

    #endregion

    #region Reset

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

    #endregion

    #region MoveNext

    [TestMethod]
    public void MoveNext1()
    {
            var ipn = IPNetwork2.Parse("192.168.1.0/30");
            using (IPNetworkCollection ipns = ipn.Subnet(32))
            {
                bool next = ipns.MoveNext();
                Assert.AreEqual(true, next, "next");
            }
        }

    [TestMethod]
    public void MoveNext2()
    {
            var ipn = IPNetwork2.Parse("192.168.1.0/30");
            using (IPNetworkCollection ipns = ipn.Subnet(32))
            {
                ipns.MoveNext();
                ipns.MoveNext();
                ipns.MoveNext();
                ipns.MoveNext();
                bool next = ipns.MoveNext();

                Assert.AreEqual(false, next, "next");
            }
        }

    #endregion
}