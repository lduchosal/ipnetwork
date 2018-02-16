using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Net.TestProject
{
    [TestClass]
    public class IPNetworkCollectionUnitTest
    {

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
