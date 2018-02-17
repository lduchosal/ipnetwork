using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace System.Net.TestProject
{
    [TestClass]
    public class BigIntegerToUnitTest
    {

        [TestMethod]
        public void TestToOctalString1()
        {

            byte[] bytes = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x00 };
            BigInteger convertme = new BigInteger(bytes);
            string result = convertme.ToOctalString();

            Assert.AreEqual("037777777777", result);

        }

        [TestMethod]
        public void TestToOctalString3()
        {

            var bigi = BigInteger.Parse("1048576");
            bigi++;
            string result = bigi.ToOctalString();

            Assert.AreEqual("04000001", result);

        }


        [TestMethod]
        public void TestToOctalString01()
        {

            var bigi = BigInteger.Zero;
            bigi++;
            string result = bigi.ToOctalString();

            Assert.AreEqual("01", result);

        }


        [TestMethod]
        public void TestToOctalString02()
        {

            var bigi = BigInteger.Zero;
            bigi--;
            string result = bigi.ToOctalString();

            Assert.AreEqual("377", result);

        }

        [TestMethod]
        public void TestToOctalString03()
        {

            var bigi = BigInteger.Zero;
            bigi--;
            bigi--;
            bigi--;
            bigi--;
            bigi--;
            bigi--;
            bigi--;
            string result = bigi.ToOctalString();

            Assert.AreEqual("371", result);

        }

        [TestMethod]
        public void TestToHexadecimalString1()
        {

            byte[] bytes = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x00 };
            BigInteger convertme = new BigInteger(bytes);
            string result = convertme.ToHexadecimalString();

            Assert.AreEqual("0FFFFFFFF", result);

        }

        [TestMethod]
        public void TestToBinaryString1()
        {

            byte[] bytes = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x00 };
            BigInteger convertme = new BigInteger(bytes);
            string result = convertme.ToBinaryString();

            Assert.AreEqual("011111111111111111111111111111111", result);

        }

        [TestMethod]
        public void TestToBinaryString01()
        {

            var bigi = BigInteger.Zero;
            bigi++;
            string result = bigi.ToBinaryString();

            Assert.AreEqual("01", result);

        }

        [TestMethod]
        public void TestToBinaryString2()
        {

            BigInteger convertme = new BigInteger(-1);
            string result = convertme.ToBinaryString();

            Assert.AreEqual("11111111", result);

        }
        [TestMethod]
        public void TestToBinaryString3()
        {

            byte[] bytes = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            BigInteger convertme = new BigInteger(bytes);
            string result = convertme.ToBinaryString();

            Assert.AreEqual("11111111", result);

        }
    }
}
