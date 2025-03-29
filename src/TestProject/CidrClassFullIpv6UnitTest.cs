// <copyright file="CidrClassFullIpv6UnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class CidrClassFullIpv6UnitTest
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestIpV6TryGuessCidrNull()
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
    public void TestIpV6TryGuessCidr1()
    {
            var cidrguess = new CidrClassFull();

            byte cidr;
            bool parsed = cidrguess.TryGuessCidr("::", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(64, cidr, "cidr");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestIpV6TryGuessCidr2()
    {
            var cidrguess = new CidrClassFull();

            byte cidr;
            bool parsed = cidrguess.TryGuessCidr("2001:0db8::", out cidr);

            Assert.AreEqual(true, parsed, "parsed");
            Assert.AreEqual(64, cidr, "cidr");
        }
}