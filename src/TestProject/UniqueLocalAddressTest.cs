// <copyright file="UniqueLocalAddressTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

/// <summary>
/// Test unit for UniqueLocalAddress
/// </summary>
[TestClass]
public class UniqueLocalAddressTest
{
    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestGenerateUlaPrefix_ReturnsValidPrefix()
    {
        // Act
        var ula = UniqueLocalAddress.GenerateUlaPrefix();

        // Assert
        Assert.IsNotNull(ula);
        Assert.AreEqual(AddressFamily.InterNetworkV6, ula.AddressFamily);
        Assert.AreEqual(48, ula.Cidr);
        Assert.IsTrue(UniqueLocalAddress.IsUlaPrefix(ula));
        Assert.IsTrue(UniqueLocalAddress.IsLocallyAssignedUla(ula));
    }

    
    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestGenerateUlaPrefix_ReturnsInvalidNetowkr()
    {
        // Act
        var nonula = IPNetwork2.Parse("1.1.1.1");

        // Assert
        Assert.IsFalse(UniqueLocalAddress.IsLocallyAssignedUla(nonula));
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestGenerateUlaPrefix_WithMac_ReturnsValidPrefix()
    {
        // Arrange
        byte[] macAddress = [0x00, 0x1B, 0x21, 0x3C, 0x9D, 0x4F];

        // Act
        var ula = UniqueLocalAddress.GenerateUlaPrefix(macAddress);

        // Assert
        Assert.IsNotNull(ula);
        Assert.AreEqual(AddressFamily.InterNetworkV6, ula.AddressFamily);
        Assert.AreEqual(48, ula.Cidr);
        Assert.IsTrue(UniqueLocalAddress.IsUlaPrefix(ula));
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestGenerateUlaPrefix_WithSeed_ReturnsDeterministicResult()
    {
        // Arrange
        string seed = "test-seed-123";

        // Act
        var ula1 = UniqueLocalAddress.GenerateUlaPrefix(seed);
        var ula2 = UniqueLocalAddress.GenerateUlaPrefix(seed);

        // Assert
        Assert.AreEqual(ula1.Network, ula2.Network);
        Assert.AreEqual(ula1.Cidr, ula2.Cidr);
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    [DataRow("test-seed-123", "fd91:2ea0:f3c9::/48")]
    [DataRow("seed", "fd92:713d:4709::/48")]
    public void TestGenerateUlaPrefix(string seed, string ipnetwork)
    {
        // Arrange
        // Act
        var ula1 = UniqueLocalAddress.GenerateUlaPrefix(seed);
        var ula2 = IPNetwork2.Parse(ipnetwork);
        // Assert
        Assert.AreEqual(ula1, ula2);
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestGenerateUlaPrefix_WithDifferentSeeds_ReturnsDifferentResults()
    {
        // Arrange
        string seed1 = "seed1";
        string seed2 = "seed2";

        // Act
        var ula1 = UniqueLocalAddress.GenerateUlaPrefix(seed1);
        var ula2 = UniqueLocalAddress.GenerateUlaPrefix(seed2);

        // Assert
        Assert.AreNotEqual(ula1.Network, ula2.Network);
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestGenerateUlaPrefix_WithNullMac_ThrowsException()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => UniqueLocalAddress.GenerateUlaPrefix((byte[])null));
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestGenerateUlaPrefix_WithInvalidMacLength_ThrowsException()
    {
        byte[] invalidMac = [0x00, 0x1B, 0x21]; // Only 3 bytes
        Assert.ThrowsExactly<ArgumentException>(() => UniqueLocalAddress.GenerateUlaPrefix(invalidMac));
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestGenerateUlaPrefix_WithEmptySeed_ThrowsException()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => UniqueLocalAddress.GenerateUlaPrefix(string.Empty));
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestCreateUlaSubnet_ValidSubnet()
    {
        // Arrange
        var ulaPrefix = UniqueLocalAddress.GenerateUlaPrefix("test");
        ushort subnetId = 0x0001;

        // Act
        var subnet = UniqueLocalAddress.CreateUlaSubnet(ulaPrefix, subnetId);

        // Assert
        Assert.IsNotNull(subnet);
        Assert.AreEqual(64, subnet.Cidr);
        Assert.IsTrue(UniqueLocalAddress.IsUlaPrefix(subnet));
        Assert.IsTrue(ulaPrefix.Contains(subnet.Network));
    }
    
    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestCreateUlaSubnet_InvalidSubnet()
    {
        // Arrange
        var invalidUlaPrefix = IPNetwork2.Parse("1.1.1.1");
        ushort subnetId = 0x0001;

        // Act
        Assert.ThrowsExactly<ArgumentException>(() => UniqueLocalAddress.CreateUlaSubnet(invalidUlaPrefix, subnetId));
    }

    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestCreateUlaSubnet_WithNonUlaPrefix_ThrowsException()
    {
        var nonUlaPrefix = IPNetwork2.Parse("2001:db8::/48");
        Assert.ThrowsExactly<ArgumentException>(() => UniqueLocalAddress.CreateUlaSubnet(nonUlaPrefix, 1));
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestCreateUlaSubnet_WithWrongPrefixLength_ThrowsException()
    {
        var wrongPrefix = IPNetwork2.Parse("fd00::/64");
        Assert.ThrowsExactly<ArgumentException>(() => UniqueLocalAddress.CreateUlaSubnet(wrongPrefix, 1));
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestIsUla_WithUlaAddress_ReturnsTrue()
    {
        // Arrange
        var ulaAddress = IPAddress.Parse("fd12:3456:789a::1");

        // Act & Assert
        Assert.IsTrue(UniqueLocalAddress.IsUla(ulaAddress));
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestIsUla_WithGlobalAddress_ReturnsFalse()
    {
        // Arrange
        var globalAddress = IPAddress.Parse("2001:db8::1");

        // Act & Assert
        Assert.IsFalse(UniqueLocalAddress.IsUla(globalAddress));
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestIsUla_WithIPv4Address_ReturnsFalse()
    {
        // Arrange
        var ipv4Address = IPAddress.Parse("192.168.1.1");

        // Act & Assert
        Assert.IsFalse(UniqueLocalAddress.IsUla(ipv4Address));
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestIsUlaPrefix_WithUlaNetwork_ReturnsTrue()
    {
        // Arrange
        var ulaNetwork = IPNetwork2.Parse("fd12:3456:789a::/48");

        // Act & Assert
        Assert.IsTrue(UniqueLocalAddress.IsUlaPrefix(ulaNetwork));
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestIsLocallyAssignedUla_WithLocallyAssigned_ReturnsTrue()
    {
        // Arrange
        var localUla = IPNetwork2.Parse("fd00::/48");

        // Act & Assert
        Assert.IsTrue(UniqueLocalAddress.IsLocallyAssignedUla(localUla));
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestIsLocallyAssignedUla_WithCentrallyAssigned_ReturnsFalse()
    {
        // Arrange
        var centralUla = IPNetwork2.Parse("fc00::/48");

        // Act & Assert
        Assert.IsFalse(UniqueLocalAddress.IsLocallyAssignedUla(centralUla));
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestUlaConstants_HaveCorrectValues()
    {
        // Assert
        Assert.AreEqual("fd00::/8", UniqueLocalAddress.UlaLocallyAssigned.ToString());
        Assert.AreEqual("fc00::/8", UniqueLocalAddress.UlaCentrallyAssigned.ToString());
        Assert.AreEqual("fc00::/7", UniqueLocalAddress.UlaRange.ToString());
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestUlaPrefix_StartsWithFd()
    {
        // Arrange & Act
        var ula = UniqueLocalAddress.GenerateUlaPrefix();
        byte[] addressBytes = ula.Network.GetAddressBytes();

        // Assert
        Assert.AreEqual(0xfd, addressBytes[0]);
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestIsUla_Extension_ReturnsTrue()
    {
        // Arrange
        var ula = UniqueLocalAddress.GenerateUlaPrefix();

        // Act & Assert
        Assert.IsTrue(ula.IsUla());
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestIsLocallyAssignedUla_Extension_ReturnsTrue()
    {
        // Arrange
        var ula = UniqueLocalAddress.GenerateUlaPrefix();

        // Act & Assert
        Assert.IsTrue(ula.IsLocallyAssignedUla());
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void TestUlaGeneration_ProducesUniqueResults()
    {
        // Arrange
        var generatedUlas = new HashSet<string>();
        int iterations = 100;

        // Act
        for (int i = 0; i < iterations; i++)
        {
            var ula = UniqueLocalAddress.GenerateUlaPrefix();
            generatedUlas.Add(ula.Network.ToString());
        }

        // Assert - Should produce mostly unique results
        Assert.IsTrue(generatedUlas.Count > iterations * 0.9,
            "ULA generation should produce highly unique results");
    }

    // ----------------- small helpers -----------------
    private static IPNetwork2 N(string s) => IPNetwork2.Parse(s);
    private static IPAddress A(string s) => IPAddress.Parse(s);

    private static void AssertIsUla48(IPNetwork2 prefix)
    {
        Assert.IsNotNull(prefix, "prefix is null");
        Assert.IsTrue(UniqueLocalAddress.IsUlaPrefix(prefix), "Not recognized as ULA prefix.");
        Assert.IsTrue(UniqueLocalAddress.IsLocallyAssignedUla(prefix),
            "Generated prefix must be locally-assigned (fd00::/8).");
        Assert.AreEqual(48, prefix.Cidr, "ULA site prefix should be /48.");
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void Constants_Are_Valid_And_Classified()
    {
        Assert.AreEqual(8, UniqueLocalAddress.UlaLocallyAssigned.Cidr);
        Assert.AreEqual(8, UniqueLocalAddress.UlaCentrallyAssigned.Cidr);
        Assert.AreEqual(7, UniqueLocalAddress.UlaRange.Cidr);

        Assert.IsTrue(UniqueLocalAddress.IsUlaPrefix(UniqueLocalAddress.UlaRange));
        Assert.IsTrue(UniqueLocalAddress.IsUlaPrefix(UniqueLocalAddress.UlaLocallyAssigned));
        Assert.IsTrue(UniqueLocalAddress.IsUlaPrefix(UniqueLocalAddress.UlaCentrallyAssigned));

        Assert.IsTrue(UniqueLocalAddress.IsLocallyAssignedUla(UniqueLocalAddress.UlaLocallyAssigned));
        Assert.IsFalse(UniqueLocalAddress.IsLocallyAssignedUla(UniqueLocalAddress.UlaCentrallyAssigned));
    }


    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void GenerateUlaPrefix_NoSeed_Returns_Valid_Local_Ula48()
    {
        var p = UniqueLocalAddress.GenerateUlaPrefix();
        AssertIsUla48(p);
    }

    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void GenerateUlaPrefix_NoSeed_Tends_To_Vary()
    {
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        for (int i = 0; i < 8; i++)
        {
            seen.Add(UniqueLocalAddress.GenerateUlaPrefix().ToString());
        }

        Assert.IsTrue(seen.Count >= 2, "Multiple calls should usually produce at least two distinct prefixes.");
    }

    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void GenerateUlaPrefix_Mac_Is_Deterministic_And_Valid()
    {
        byte[] mac = new byte[] { 0x02, 0x11, 0x22, 0x33, 0x44, 0x55 }; // locally-administered example
        var p1 = UniqueLocalAddress.GenerateUlaPrefix(mac);
        var p2 = UniqueLocalAddress.GenerateUlaPrefix(mac);
        AssertIsUla48(p1);
        Assert.AreEqual(p1.ToString(), p2.ToString());
    }

    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void GenerateUlaPrefix_Different_Mac_Usually_Different()
    {
        var p1 = UniqueLocalAddress.GenerateUlaPrefix(new byte[] { 0, 1, 2, 3, 4, 5 });
        var p2 = UniqueLocalAddress.GenerateUlaPrefix(new byte[] { 0, 1, 2, 3, 4, 6 });
        AssertIsUla48(p1);
        AssertIsUla48(p2);
        Assert.AreNotEqual(p1.ToString(), p2.ToString(), "Different MACs should usually yield different prefixes.");
    }

    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void GenerateUlaPrefix_Mac_Null_Or_Wrong_Length_Throws()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => UniqueLocalAddress.GenerateUlaPrefix((byte[])null!));
        Assert.ThrowsExactly<ArgumentException>(() => UniqueLocalAddress.GenerateUlaPrefix(Array.Empty<byte>()));
        Assert.ThrowsExactly<ArgumentException>(() =>
            UniqueLocalAddress.GenerateUlaPrefix(new byte[] { 1, 2, 3, 4, 5 })); // 5
        Assert.ThrowsExactly<ArgumentException>(() =>
            UniqueLocalAddress.GenerateUlaPrefix(new byte[] { 1, 2, 3, 4, 5, 6, 7 })); // 7
    }

    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void GenerateUlaPrefix_Seed_Is_Deterministic_And_Valid()
    {
        string s = "unit-test-seed";
        var p1 = UniqueLocalAddress.GenerateUlaPrefix(s);
        var p2 = UniqueLocalAddress.GenerateUlaPrefix(s);
        AssertIsUla48(p1);
        Assert.AreEqual(p1.ToString(), p2.ToString());
    }

    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void GenerateUlaPrefix_Different_Seeds_Usually_Different()
    {
        var p1 = UniqueLocalAddress.GenerateUlaPrefix("seed-A");
        var p2 = UniqueLocalAddress.GenerateUlaPrefix("seed-B");
        AssertIsUla48(p1);
        AssertIsUla48(p2);
        Assert.AreNotEqual(p1.ToString(), p2.ToString());
    }

    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void GenerateUlaPrefix_Seed_Null_Or_Empty_Throws()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => UniqueLocalAddress.GenerateUlaPrefix((string)null!));
        Assert.ThrowsExactly<ArgumentNullException>(() => UniqueLocalAddress.GenerateUlaPrefix(string.Empty));
    }

    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void CreateUlaSubnet_Basic_Mapping_To_64()
    {
        var site = N("fd12:3456:789a::/48");
        var s0 = UniqueLocalAddress.CreateUlaSubnet(site, 0);
        var s1 = UniqueLocalAddress.CreateUlaSubnet(site, 1);
        var sFFFF = UniqueLocalAddress.CreateUlaSubnet(site, 0xFFFF);

        Assert.AreEqual(64, s0.Cidr);
        Assert.AreEqual(64, s1.Cidr);
        Assert.AreEqual(64, sFFFF.Cidr);

        Assert.AreEqual("fd12:3456:789a::/64", s0.ToString());
        Assert.AreEqual("fd12:3456:789a:1::/64", s1.ToString());
        Assert.AreEqual("fd12:3456:789a:ffff::/64", sFFFF.ToString());
    }

    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void CreateUlaSubnet_SubnetId_OutOfRange_Throws()
    {
        var site = N("fd12:3456:789a::/48");
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => UniqueLocalAddress.CreateUlaSubnet(site, -1));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => UniqueLocalAddress.CreateUlaSubnet(site, 0x1_0000));
    }

    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void CreateUlaSubnet_NonUlaPrefix_Or_Not_48_Throws()
    {
        Assert.ThrowsExactly<ArgumentException>(() => UniqueLocalAddress.CreateUlaSubnet(N("2001:db8::/32"), 0));
        Assert.ThrowsExactly<ArgumentException>(() =>
            UniqueLocalAddress.CreateUlaSubnet(N("fd12:3456::/40"), 0)); // not /48
        Assert.ThrowsExactly<ArgumentException>(() =>
            UniqueLocalAddress.CreateUlaSubnet(N("fc00::/8"), 0)); // too broad
    }

    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    [DataRow("fc00::", true)]
    [DataRow("fd00::", true)]
    [DataRow("fdff:ffff:ffff:ffff:ffff:ffff:ffff:ffff", true)]
    [DataRow("fbff::1", false)] // below ULA range
    [DataRow("fe00::1", false)] // above ULA range
    [DataRow("fe80::1", false)] // link-local
    [DataRow("::1", false)] // loopback
    [DataRow("2001:db8::1", false)] // global (doc)
    [DataRow("ff02::1", false)] // multicast
    [DataRow("::ffff:192.0.2.1", false)] // IPv4-mapped
    public void IsUla_Address_Classification(string ip, bool expected)
    {
        Assert.AreEqual(expected, UniqueLocalAddress.IsUla(A(ip)));
    }

    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    [DataRow("fc00::/7", true)] // entire ULA space
    [DataRow("fc00::/8", true)]
    [DataRow("fd00::/8", true)]
    [DataRow("fd12:3456:789a::/48", true)]
    [DataRow("fe80::/64", false)]
    [DataRow("2001:db8::/32", false)]
    [DataRow("fb00::/8", false)] // supernet below
    [DataRow("fe00::/8", false)] // supernet above
    public void IsUlaPrefix_Classification(string net, bool expected)
    {
        Assert.AreEqual(expected, UniqueLocalAddress.IsUlaPrefix(N(net)));
    }

    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    [DataRow("fd12:3456:789a::/48", true)]
    [DataRow("fd00::/8", true)]
    [DataRow("fc00::/8", false)]
    [DataRow("fc12::/48", false)]
    [DataRow("fe80::/64", false)]
    [DataRow("2001:db8::/32", false)]
    public void IsLocallyAssignedUla_Classification(string net, bool expected)
    {
        Assert.AreEqual(expected, UniqueLocalAddress.IsLocallyAssignedUla(N(net)));
    }

    /// <summary>
    /// Test
    /// </summary>
    [TestMethod]
    public void Ula_Boundary_Addresses()
    {
        // lowest and highest addresses inside ULA range
        Assert.IsTrue(UniqueLocalAddress.IsUla(A("fc00::")));
        Assert.IsTrue(UniqueLocalAddress.IsUla(A("fdff:ffff:ffff:ffff:ffff:ffff:ffff:ffff")));

        // just outside (low/high)
        Assert.IsFalse(UniqueLocalAddress.IsUla(A("fbff:ffff:ffff:ffff:ffff:ffff:ffff:ffff")));
        Assert.IsFalse(UniqueLocalAddress.IsUla(A("fe00::")));
    }
}
