using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Net.TestProject
{
    [TestClass]
    public class IPNetworkCollectionUnitTest
    {

        #region ctor 

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor1()
        {
            IPNetworkCollection ipn = new IPNetworkCollection(IPNetwork.IANA_ABLK_RESERVED1, 33);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCtor2()
        {
            IPNetworkCollection ipn = new IPNetworkCollection(IPNetwork.IANA_ABLK_RESERVED1, 2);
        }

        #endregion

        #region Current 

        [TestMethod]
        public void TestCurrent()
        {
            IPNetwork ipn = IPNetwork.Parse("192.168.0.0/32");
            using (IPNetworkCollection ipns = ipn.Subnet(32))
            {
                var ipnse = (Collections.IEnumerator)ipns;
                ipnse.MoveNext();
                var ipn0 = ipnse.Current;

                Assert.AreEqual("192.168.0.0/32", ipn0.ToString(), "ipn0");
            }
        }

        #endregion

        #region Enumerator 

        [TestMethod]
        public void TestEnumerator()
        {
            IPNetwork ipn = IPNetwork.Parse("192.168.0.0/32");
            using (IPNetworkCollection ipns = ipn.Subnet(32))
            {
                var ipnse = (Collections.IEnumerable)ipns;
                var ee = ipnse.GetEnumerator();
                ee.MoveNext();
                var ipn0 = ee.Current;
                Assert.AreEqual("192.168.0.0/32", ipn0.ToString(), "ipn0");
            }
        }

        #endregion

        #region Reset 

        [TestMethod]
        public void TestReset1()
        {
            IPNetwork ipn = IPNetwork.Parse("192.168.1.0/29");
            using (IPNetworkCollection ipns = ipn.Subnet(32))
            {

                var ipn0 = ipns.Current;
                ipns.MoveNext();
                ipns.Reset();
                var ipn1 = ipns.Current;

                Assert.AreEqual(ipn0, ipn1, "reset");
            }
        }

        #endregion

        #region MoveNext 

        [TestMethod]
        public void MoveNext1()
        {
            IPNetwork ipn = IPNetwork.Parse("192.168.1.0/30");
            using (IPNetworkCollection ipns = ipn.Subnet(32))
            {
                bool next = ipns.MoveNext();
                Assert.AreEqual(true, next, "next");
            }
        }

        [TestMethod]
        public void MoveNext2()
        {
            IPNetwork ipn = IPNetwork.Parse("192.168.1.0/30");
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
}
