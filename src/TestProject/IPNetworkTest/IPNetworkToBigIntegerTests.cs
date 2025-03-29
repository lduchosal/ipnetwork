// <copyright file="IPNetworkToBigIntegerTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test BigInterger conversions.
/// </summary>
[TestClass]
public class IPNetworkToBigIntegerTests
{
    /// <summary>
    ///     Tests To Big Integer functionality with To Big Integer32.
    /// </summary>
    [TestMethod]
    public void TestToBigInteger32()
    {
            var mask = IPAddress.Parse("255.255.255.255");
            uint uintMask = 0xffffffff;
            var result = IPNetwork2.ToBigInteger(mask);

            Assert.AreEqual(uintMask, result, "uint");
        }

    /// <summary>
    ///     Tests To Big Integer functionality with To Big Integer24.
    /// </summary>
    [TestMethod]
    public void TestToBigInteger24()
    {
            var mask = IPAddress.Parse("255.255.255.0");
            uint uintMask = 0xffffff00;
            BigInteger? result = IPNetwork2.ToBigInteger(mask);

            Assert.AreEqual(uintMask, result, "uint");
        }

    /// <summary>
    ///     Tests To Big Integer functionality with To Big Integer16.
    /// </summary>
    [TestMethod]
    public void TestToBigInteger16()
    {
            var mask = IPAddress.Parse("255.255.0.0");
            uint uintMask = 0xffff0000;
            BigInteger? result = IPNetwork2.ToBigInteger(mask);

            Assert.AreEqual(uintMask, result, "uint");
        }

    /// <summary>
    ///     Tests To Big Integer functionality with To Big Integer8.
    /// </summary>
    [TestMethod]
    public void TestToBigInteger8()
    {
            var mask = IPAddress.Parse("255.0.0.0");
            uint uintMask = 0xff000000;
            BigInteger? result = IPNetwork2.ToBigInteger(mask);

            Assert.AreEqual(uintMask, result, "uint");
        }

    /// <summary>
    ///     Tests To Big Integer functionality with To Big Integer0.
    /// </summary>
    [TestMethod]
    public void TestToBigInteger0()
    {
            var mask = IPAddress.Parse("0.0.0.0");
            uint uintMask = 0x00000000;
            BigInteger? result = IPNetwork2.ToBigInteger(mask);

            Assert.AreEqual(uintMask, result, "uint");
        }

    /// <summary>
    /// Try to convert from null.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestToBigIntegerAne()
    {
            BigInteger? result = IPNetwork2.ToBigInteger(null);
        }

    /// <summary>
    /// Try to convert from null IPAddress.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestToBigIntegerAne3()
    {
            IPAddress ip = null;
            BigInteger? result = IPNetwork2.ToBigInteger(ip);
        }

    /// <summary>
    ///     Tests To Big Integer functionality with To Big Integer ANE2.
    /// </summary>
    [TestMethod]
    public void TestToBigIntegerAne2()
    {
            BigInteger? result = IPNetwork2.ToBigInteger(IPAddress.IPv6Any);
            uint expected = 0;
            Assert.AreEqual(expected, result, "result");
        }

    /// <summary>
    /// Try to convert from invalid cidr.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestToBigIntegerByte()
    {
            BigInteger result = IPNetwork2.ToUint(33, AddressFamily.InterNetwork);
        }

    /// <summary>
    ///     Tests To Big Integer functionality with To Big Integer Byte2.
    /// </summary>
    [TestMethod]
    public void TestToBigIntegerByte2()
    {
            BigInteger result = IPNetwork2.ToUint(32, AddressFamily.InterNetwork);
            uint expected = 4294967295;
            Assert.AreEqual(expected, result, "result");
        }

    /// <summary>
    ///     Tests To Big Integer functionality with To Big Integer Byte3.
    /// </summary>
    [TestMethod]
    public void TestToBigIntegerByte3()
    {
            BigInteger result = IPNetwork2.ToUint(0, AddressFamily.InterNetwork);
            uint expected = 0;
            Assert.AreEqual(expected, result, "result");
        }

    /// <summary>
    ///     Tests To Big Integer functionality with To Big Integer Internal1.
    /// </summary>
    [TestMethod]
    public void TestToBigIntegerInternal1()
    {
            BigInteger? result = null;
            IPNetwork2.InternalToBigInteger(true, 33, AddressFamily.InterNetwork, out result);
            Assert.AreEqual(null, result, "result");
        }

    /// <summary>
    ///     Tests To Big Integer functionality with To Big Integer Internal2.
    /// </summary>
    [TestMethod]
    public void TestToBigIntegerInternal2()
    {
            BigInteger? result = null;
            IPNetwork2.InternalToBigInteger(true, 129, AddressFamily.InterNetworkV6, out result);
            Assert.AreEqual(null, result, "result");
        }

    /// <summary>
    /// Try to convert from invalid IPV6 cidr.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestToBigIntegerInternal3()
    {
            BigInteger? result = null;
            IPNetwork2.InternalToBigInteger(false, 129, AddressFamily.InterNetworkV6, out result);
        }

    /// <summary>
    /// Try to convert from invalid AddressFamily.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(NotSupportedException))]
    public void TestToBigIntegerInternal4()
    {
            BigInteger? result = null;
            IPNetwork2.InternalToBigInteger(false, 32, AddressFamily.AppleTalk, out result);
        }

    /// <summary>
    ///     Tests To Big Integer functionality with To Big Integer Internal5.
    /// </summary>
    [TestMethod]
    public void TestToBigIntegerInternal5()
    {
            BigInteger? result = null;
            IPNetwork2.InternalToBigInteger(true, 32, AddressFamily.AppleTalk, out result);
            Assert.AreEqual(null, result, "result");
        }
}