using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Net.TestProject
{
    [TestClass]
    public class CidrClassLessTest
    {
        #region IPV4

        [TestMethod]
        public void TestTryGuessCidrNull()
        {
            var cidrguess = new CidrClassLess();

            byte cidr;
            bool parsed = cidrguess.TryGuessCidr(null, out cidr);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(0, cidr, "cidr");
        }

        [TestMethod]
        public void TestTryGuessCidrA()
        {
            var cidrguess = new CidrClassLess();

            byte cidr;
            bool parsed = cidrguess.TryGuessCidr("10.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(32, cidr, "cidr");
        }

        [TestMethod]
        public void TestTryGuessCidrB()
        {
            var cidrguess = new CidrClassLess();

            byte cidr;
            bool parsed = cidrguess.TryGuessCidr("172.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(32, cidr, "cidr");
        }

        [TestMethod]
        public void TestTryGuessCidrC()
        {
            var cidrguess = new CidrClassLess();

            byte cidr;
            bool parsed = cidrguess.TryGuessCidr("192.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(32, cidr, "cidr");
        }

        [TestMethod]
        public void TestTryGuessCidrD()
        {
            var cidrguess = new CidrClassLess();

            byte cidr;
            bool parsed = cidrguess.TryGuessCidr("224.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(32, cidr, "cidr");
        }
        [TestMethod]

        public void TestTryGuessCidrE()
        {
            var cidrguess = new CidrClassLess();

            byte cidr;
            bool parsed = cidrguess.TryGuessCidr("240.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(32, cidr, "cidr");
        }

        #endregion

        #region IPV6

        [TestMethod]
        public void TestIpV6TryGuessCidrNull()
        {
            var cidrguess = new CidrClassLess();

            byte cidr;
            bool parsed = cidrguess.TryGuessCidr(null, out cidr);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(0, cidr, "cidr");
        }

        [TestMethod]
        public void TestIpV6TryGuessCidr1()
        {
            var cidrguess = new CidrClassLess();

            byte cidr;
            bool parsed = cidrguess.TryGuessCidr("::", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(128, cidr, "cidr");
        }

        [TestMethod]
        public void TestIpV6TryGuessCidr2()
        {
            var cidrguess = new CidrClassLess();

            byte cidr;
            bool parsed = cidrguess.TryGuessCidr("2001:0db8::", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(128, cidr, "cidr");
        }


        #endregion

    }
}
