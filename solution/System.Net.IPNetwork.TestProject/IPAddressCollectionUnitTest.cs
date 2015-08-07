using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Collections.Generic;
using System.Collections;

namespace System.Net.IPNetwork.TestProject {
    /// <summary>
    /// IPNetworkUnitTest test every single method
    /// </summary>
    [TestClass]
    public class IPAddressCollectionUnitTest {

        #region Parse

        [TestMethod]
        public void TestAtIndexIPAddress() {

            IPNetwork ipn = IPNetwork.Parse("192.168.1.0/29");
            using (var ips = IPNetwork.ListIPAddress(ipn)) {
                Assert.AreEqual("192.168.1.0", ips[0].ToString(), "0");
                Assert.AreEqual("192.168.1.1", ips[1].ToString(), "1");
                Assert.AreEqual("192.168.1.2", ips[2].ToString(), "2");
                Assert.AreEqual("192.168.1.3", ips[3].ToString(), "3");
                Assert.AreEqual("192.168.1.4", ips[4].ToString(), "4");
                Assert.AreEqual("192.168.1.5", ips[5].ToString(), "5");
                Assert.AreEqual("192.168.1.6", ips[6].ToString(), "6");
                Assert.AreEqual("192.168.1.7", ips[7].ToString(), "7");
            }

        }

        [TestMethod]
        public void TestIterateIPAddress() {

            IPNetwork ipn = IPNetwork.Parse("192.168.1.0/29");
            IPAddress last = null;
            IPAddress fisrt = null;
            int count = 0;
            using (var ips = IPNetwork.ListIPAddress(ipn)) {
                foreach (var ip in ips) {
                    if (fisrt == null) fisrt = ip;
                    last = ip;
                    count++;
                }
                Assert.IsNotNull(last, "last is null");
                Assert.IsNotNull(fisrt, "fisrt is null");
                Assert.AreEqual("192.168.1.0", fisrt.ToString(), "first");
                Assert.AreEqual("192.168.1.7", last.ToString(), "last");
                Assert.AreEqual(8, count, "count");
            }

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestOutOfRangeIPAddress() {

            IPNetwork ipn = IPNetwork.Parse("192.168.1.0/29");
            using (var ips = IPNetwork.ListIPAddress(ipn)) {
                Console.Write("This is out of range : {0} ", ips[8]);
            }

        }

        [TestMethod]
        public void TestCountIPAddress() {

            IPNetwork ipn = IPNetwork.Parse("192.168.1.0/29");
            using (IPAddressCollection ips = IPNetwork.ListIPAddress(ipn)) {
                Assert.AreEqual(8, ips.Count, "Count");
            }
        }

        [TestMethod]
        public void TestReset() {

            IPNetwork ipn = IPNetwork.Parse("192.168.1.0/29");
            using (IPAddressCollection ips = IPNetwork.ListIPAddress(ipn)) {
                ips.Reset();
            }
        }

        [TestMethod]
        public void TestResetEnumerator() {

            IPNetwork ipn = IPNetwork.Parse("192.168.1.0/29");
            using (IEnumerator<IPAddress> ips = IPNetwork.ListIPAddress(ipn)) {
                ips.Reset();
                while (ips.MoveNext()) {
                    Assert.IsNotNull(ips.Current);
                }
                ips.Reset();
                while (ips.MoveNext()) {
                    Assert.IsNotNull(ips.Current);
                }

            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEnumeratorFailed() {

            IPNetwork ipn = IPNetwork.Parse("192.168.1.0/29");
            using (IEnumerator<IPAddress> ips = IPNetwork.ListIPAddress(ipn)) {
                ips.Reset();
                while (ips.MoveNext()) {
                    Assert.IsNotNull(ips.Current);
                }
                Console.WriteLine("This is out of range : {0}", ips.Current);

            }
        }

        [TestMethod]
        public void TestEnumeratorMoveNext() {

            IPNetwork ipn = IPNetwork.Parse("192.168.1.0/29");
            using (IEnumerator<IPAddress> ips = IPNetwork.ListIPAddress(ipn)) {
                Assert.IsTrue(ips.MoveNext());
                Assert.IsTrue(ips.MoveNext());
                Assert.IsTrue(ips.MoveNext());
                Assert.IsTrue(ips.MoveNext());
                Assert.IsTrue(ips.MoveNext());
                Assert.IsTrue(ips.MoveNext());
                Assert.IsTrue(ips.MoveNext());
                Assert.IsTrue(ips.MoveNext());
                Assert.IsFalse(ips.MoveNext());
                Assert.IsFalse(ips.MoveNext());


            }
        }

        [TestMethod]
        public void TestEnumeratorMoveNext2() {

            IPNetwork ipn = IPNetwork.Parse("192.168.1.0/31");
            using (IEnumerator<IPAddress> ips = IPNetwork.ListIPAddress(ipn)) {
                Assert.IsTrue(ips.MoveNext());
                Assert.IsTrue(ips.MoveNext());
                Assert.IsFalse(ips.MoveNext());
                ips.Reset();
                Assert.IsTrue(ips.MoveNext());
                Assert.IsTrue(ips.MoveNext());
                Assert.IsFalse(ips.MoveNext());


            }
        }

        [TestMethod]
        public void TestEnumeratorCurrent() {

            IPNetwork ipn = IPNetwork.Parse("192.168.1.0/31");
            IEnumerator ips = IPNetwork.ListIPAddress(ipn);
            Assert.IsNotNull(ips.Current);
            Assert.IsTrue(ips.MoveNext());
            Assert.IsNotNull(ips.Current);
            Assert.IsTrue(ips.MoveNext());
            Assert.IsFalse(ips.MoveNext());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEnumeratorCurrentOor() {

            IPNetwork ipn = IPNetwork.Parse("192.168.1.0/31");
            IEnumerator ips = IPNetwork.ListIPAddress(ipn);
            Assert.IsNotNull(ips.Current);
            Assert.IsTrue(ips.MoveNext());
            Assert.IsNotNull(ips.Current);
            Assert.IsTrue(ips.MoveNext());
            Assert.IsFalse(ips.MoveNext());
            Console.WriteLine("This is out of range : {0} ", ips.Current);
        }

        [TestMethod]
        public void TestEnumeratorIterate() {

            IPNetwork ipn = IPNetwork.Parse("192.168.1.0/31");
            IEnumerator ips = IPNetwork.ListIPAddress(ipn);
            while (ips.MoveNext()) {
                Assert.IsNotNull(ips.Current);
            }
        }

        #endregion

    }
}
