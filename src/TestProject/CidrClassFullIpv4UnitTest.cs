// <copyright file="CidrClassFullIpv4UnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class CidrClassFullIpv4UnitTest
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryGuessCidrNull()
    {
            var cidrguess = new CidrClassFull();

            byte cidr;
            bool parsed = cidrguess.TryGuessCidr(null, out cidr);

            Assert.AreEqual(false, parsed, "parsed");
            Assert.AreEqual(0, cidr, "cidr");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryGuessCidrA()
    {
            var cidrguess = new CidrClassFull();

            byte cidr;
            bool parsed = cidrguess.TryGuessCidr("10.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(8, cidr, "cidr");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryGuessCidrB()
    {
            var cidrguess = new CidrClassFull();

            byte cidr;
            bool parsed = cidrguess.TryGuessCidr("172.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(16, cidr, "cidr");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryGuessCidrC()
    {
            var cidrguess = new CidrClassFull();

            byte cidr;
            bool parsed = cidrguess.TryGuessCidr("192.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(24, cidr, "cidr");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryGuessCidrD()
    {
            var cidrguess = new CidrClassFull();

            byte cidr;
            bool parsed = cidrguess.TryGuessCidr("224.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(24, cidr, "cidr");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTryGuessCidrE()
    {
            var cidrguess = new CidrClassFull();

            byte cidr;
            bool parsed = cidrguess.TryGuessCidr("240.0.0.0", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(24, cidr, "cidr");
        }
}