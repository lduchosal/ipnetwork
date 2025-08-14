// <copyright file="CidrNetworkAwareUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

#nullable enable
namespace TestProject;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class CidrNetworkAwareUnitTest
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    [DataRow(null, 0, false)]
    [DataRow("null", 0, false)]
    [DataRow("invalidcidr", 0, false)]
    [DataRow("in.va.lid.ci.dr", 0, false)]
    [DataRow("de:ad:be:ef::", 064)]
    [DataRow("0::", 0)]
    [DataRow("1::", 16)]
    [DataRow("::1", 128)]
    
    // IPv4 patterns
    [DataRow("10.0.0.0", 8)]
    [DataRow("0.0.0.0", 0)]
    [DataRow("172.0.0.0", 8)]
    [DataRow("192.0.0.0", 8)]
    [DataRow("224.0.0.0", 8)]
    [DataRow("240.0.0.0", 8)]
    [DataRow("192.0.43.8", 32)]
    [DataRow("192.0.43.0", 24)]
    [DataRow("192.43.0.0", 16)]

    // IPv4 wildcard patterns (.255 endings)
    [DataRow("192.43.255.255", 16 )]
    [DataRow("10.255.255.255", 8 )]
    [DataRow("192.0.43.255", 24)]
    [DataRow("192.0.255.255", 16)]
    [DataRow("192.255.255.255", 8)]
    [DataRow("255.255.255.255", 0)]

    // IPv6 exact address
    [DataRow("2001:db8::1", 128)]
    [DataRow("::", 0)]
    [DataRow("2001:0db8::", 32)]

    // IPv6 with trailing zeros (network boundaries)
    [DataRow("2001:db8::", 32)]                        // common /64 subnet
    [DataRow("2001:db8:1:2:3:4:5:0", 112)]             // last hextet zero
    [DataRow("2001:db8:1:2:3:4:0:0", 96)]              // last 2 hextets zero
    [DataRow("2001:db8:1:2:3:0:0:0", 80)]              // last 3 hextets zero
    [DataRow("2001:db8:1:2:0:0:0:0", 64)]              // last 4 hextets zero
    [DataRow("2001:db8:1:0:0:0:0:0", 48)]
    [DataRow("2001:db8:0:0:0:0:0:0", 32)]
    public void TestTryGuess(string? message, int expectedCidr, bool expectedParsed = true)
    {
        var cidrguess = new CidrNetworkAware();
        bool parsed = cidrguess.TryGuessCidr(message, out byte cidr);

        Assert.AreEqual(expectedParsed, parsed, "parsed");
        Assert.AreEqual(expectedCidr, cidr, "cidr");
    }
}