// <copyright file="IPNetworkV6ParseStringTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// ParseString.
/// </summary>
public class IPNetworkV6ParseStringTests
{
    /// <summary>
    /// Test ParseString.
    /// </summary>
    [TestMethod]
    public void TestParseString1()
    {
        string ipaddress = "2001:0db8:: ffff:ffff:ffff:ffff:ffff:ffff:ffff:fff0";

        string network = "2001:db8::";
        string netmask = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:fff0";
        string firstUsable = "2001:db8::";
        string lastUsable = "2001:db8::f";
        byte cidr = 124;
        uint usable = 16;

        var ipnetwork = IPNetwork2.Parse(ipaddress);
        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.AreEqual(null, ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test ParseString.
    /// </summary>
    [TestMethod]
    public void TestParseString3()
    {
        string ipaddress = ":: ::";

        string network = "::";
        string netmask = "::";
        string firstUsable = "::";
        string lastUsable = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        var usable = BigInteger.Pow(2, 128);

        var ipnetwork = IPNetwork2.Parse(ipaddress);
        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.AreEqual(null, ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(0, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test ParseString.
    /// </summary>
    [TestMethod]
    public void TestParseString4()
    {
        string ipaddress = "::/0";

        string network = "::";
        string netmask = "::";
        string firstUsable = "::";
        string lastUsable = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        var usable = BigInteger.Pow(2, 128);

        var ipnetwork = IPNetwork2.Parse(ipaddress);
        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.AreEqual(null, ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(0, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test ParseString.
    /// </summary>
    [TestMethod]
    public void TestParseString5()
    {
        string ipaddress = "::/32";

        string network = "::";
        string netmask = "ffff:ffff::";
        string firstUsable = "::";
        string lastUsable = "::ffff:ffff:ffff:ffff:ffff:ffff";
        byte cidr = 32;
        var usable = BigInteger.Pow(2, 128 - cidr);

        var ipnetwork = IPNetwork2.Parse(ipaddress);
        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.AreEqual(null, ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test ParseString.
    /// </summary>
    [TestMethod]
    public void TestParseString6()
    {
        string ipaddress = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff/128";

        string network = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        string netmask = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        string firstUsable = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        string lastUsable = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        byte cidr = 128;
        uint usable = 1;

        var ipnetwork = IPNetwork2.Parse(ipaddress);
        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.AreEqual(null, ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test ParseString uppercase lowercase.
    /// </summary>
    [TestMethod]
    public void TestParseString7_Uppercase_ipv6_must_parse()
    {
        string ipaddress = "2FFF:FFFF:123::/60";

        string network = "2fff:ffff:123::";
        string netmask = "ffff:ffff:ffff:fff0::";
        string firstUsable = "2fff:ffff:123::";
        string lastUsable = "2fff:ffff:123:f:ffff:ffff:ffff:ffff";
        byte cidr = 60;

        // BigInteger usable = 295147905179352825856;
        var ipnetwork = IPNetwork2.Parse(ipaddress);
        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.AreEqual(null, ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");

        // Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test ParseString uppercase lowercase.
    /// </summary>
    [TestMethod]
    public void TestParseString8_Uppercase_ipv6_must_parse()
    {
        string ipaddress = "2FFF:FFFE:123::/60";

        string network = "2fff:fffe:123::";
        string netmask = "ffff:ffff:ffff:fff0::";
        string firstUsable = "2fff:fffe:123::";
        string lastUsable = "2fff:fffe:123:f:ffff:ffff:ffff:ffff";
        byte cidr = 60;

        // BigInteger usable = 295147905179352825856;
        var ipnetwork = IPNetwork2.Parse(ipaddress);
        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.AreEqual(null, ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");

        // Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test ParseString uppercase lowercase.
    /// </summary>
    [TestMethod]
    public void TestParseString9_Uppercase_ipv6_must_parse()
    {
        string ipaddress = "2FFF:FFFC:123::/60";

        string network = "2fff:fffc:123::";
        string netmask = "ffff:ffff:ffff:fff0::";
        string firstUsable = "2fff:fffc:123::";
        string lastUsable = "2fff:fffc:123:f:ffff:ffff:ffff:ffff";
        byte cidr = 60;

        // BigInteger usable = 295147905179352825856;
        var ipnetwork = IPNetwork2.Parse(ipaddress);
        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.AreEqual(null, ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");

        // Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test ParseString uppercase lowercase.
    /// </summary>
    [TestMethod]
    public void TestParseString10_Uppercase_ipv6_must_parse()
    {
        string ipaddress = "2FFF:FFFA:123::/60";

        string network = "2fff:fffa:123::";
        string netmask = "ffff:ffff:ffff:fff0::";
        string firstUsable = "2fff:fffa:123::";
        string lastUsable = "2fff:fffa:123:f:ffff:ffff:ffff:ffff";
        byte cidr = 60;

        // BigInteger usable = 295147905179352825856;
        var ipnetwork = IPNetwork2.Parse(ipaddress);
        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.AreEqual(null, ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");

        // Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test ParseString uppercase lowercase.
    /// </summary>
    [TestMethod]
    public void TestParseString11_Uppercase_ipv6_must_parse()
    {
        string ipaddress = "FFFF:FFF1:123::/60";

        string network = "ffff:fff1:123::";
        string netmask = "ffff:ffff:ffff:fff0::";
        string firstUsable = "ffff:fff1:123::";
        string lastUsable = "ffff:fff1:123:f:ffff:ffff:ffff:ffff";
        byte cidr = 60;

        // BigInteger usable = 295147905179352825856;
        var ipnetwork = IPNetwork2.Parse(ipaddress);
        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.AreEqual(null, ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");

        // Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test ParseString uppercase lowercase.
    /// </summary>
    [TestMethod]
    public void TestParseIPAddressNoNetmask1()
    {
        string ipaddress = "2001:0db8::";
        var ipnetwork = IPNetwork2.Parse(ipaddress);

        string network = "2001:db8::";
        string netmask = "ffff:ffff:ffff:ffff::";
        string firstUsable = "2001:db8::";
        string lastUsable = "2001:db8::ffff:ffff:ffff:ffff";
        byte cidr = 64;
        var usable = BigInteger.Pow(2, 64);

        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.AreEqual(null, ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test ParseString uppercase lowercase.
    /// </summary>
    [TestMethod]
    public void TestParseIPAddressNoNetmask4()
    {
        string ipaddress = "::";
        var ipnetwork = IPNetwork2.Parse(ipaddress);

        string network = "::";
        string netmask = "ffff:ffff:ffff:ffff::";
        string firstUsable = "::";
        string lastUsable = "::ffff:ffff:ffff:ffff";
        byte cidr = 64;
        var usable = BigInteger.Pow(2, 64);

        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.AreEqual(null, ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test ParseString uppercase lowercase.
    /// </summary>
    [TestMethod]
    public void TestParseIPAddressNoNetmask5()
    {
        string ipaddress = "2001:0db8::1";
        var ipnetwork = IPNetwork2.Parse(ipaddress);

        string network = "2001:db8::";
        string netmask = "ffff:ffff:ffff:ffff::";
        string firstUsable = "2001:db8::";
        string lastUsable = "2001:db8::ffff:ffff:ffff:ffff";
        byte cidr = 64;
        var usable = BigInteger.Pow(2, 64);

        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.AreEqual(null, ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test ParseString uppercase lowercase.
    /// </summary>
    [TestMethod]
    public void TestParseIPAddressNoNetmask1_ClassFull()
    {
        string ipaddress = "2001:0db8::";
        ICidrGuess cidrGess = CidrGuess.ClassFull;

        var ipnetwork = IPNetwork2.Parse(ipaddress, cidrGess);

        string network = "2001:db8::";
        string netmask = "ffff:ffff:ffff:ffff::";
        string firstUsable = "2001:db8::";
        string lastUsable = "2001:db8::ffff:ffff:ffff:ffff";
        byte cidr = 64;
        var usable = BigInteger.Pow(2, 64);

        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.AreEqual(null, ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test ParseString uppercase lowercase.
    /// </summary>
    [TestMethod]
    public void TestParseIPAddressNoNetmask4_ClassFull()
    {
        string ipaddress = "::";
        ICidrGuess cidrGess = CidrGuess.ClassFull;

        var ipnetwork = IPNetwork2.Parse(ipaddress, cidrGess);

        string network = "::";
        string netmask = "ffff:ffff:ffff:ffff::";
        string firstUsable = "::";
        string lastUsable = "::ffff:ffff:ffff:ffff";
        byte cidr = 64;
        var usable = BigInteger.Pow(2, 64);

        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.AreEqual(null, ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test ParseString ClassFull.
    /// </summary>
    [TestMethod]
    public void TestParseIPAddressNoNetmask5_ClassFull()
    {
        string ipaddress = "2001:0db8::1";
        ICidrGuess cidrGess = CidrGuess.ClassFull;

        var ipnetwork = IPNetwork2.Parse(ipaddress, cidrGess);

        string network = "2001:db8::";
        string netmask = "ffff:ffff:ffff:ffff::";
        string firstUsable = "2001:db8::";
        string lastUsable = "2001:db8::ffff:ffff:ffff:ffff";
        byte cidr = 64;
        var usable = BigInteger.Pow(2, 64);

        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.AreEqual(null, ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test ParseString ClassLess.
    /// </summary>
    [TestMethod]
    public void TestParseIPAddressNoNetmask1_ClassLess()
    {
        string ipaddress = "2001:0db8::";
        ICidrGuess cidrGess = CidrGuess.ClassLess;

        var ipnetwork = IPNetwork2.Parse(ipaddress, cidrGess);

        string network = "2001:db8::";
        string netmask = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        string firstUsable = "2001:db8::";
        string lastUsable = "2001:db8::";
        byte cidr = 128;
        int usable = 1;

        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.AreEqual(null, ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test ParseString ClassLess.
    /// </summary>
    [TestMethod]
    public void TestParseIPAddressNoNetmask4_ClassLess()
    {
        string ipaddress = "::";
        ICidrGuess cidrGess = CidrGuess.ClassLess;

        var ipnetwork = IPNetwork2.Parse(ipaddress, cidrGess);

        string network = "::";
        string netmask = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        string firstUsable = "::";
        string lastUsable = "::";
        byte cidr = 128;
        int usable = 1;

        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.AreEqual(null, ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test ParseString ClassLess.
    /// </summary>
    [TestMethod]
    public void TestParseIPAddressNoNetmask5_ClassLess()
    {
        string ipaddress = "2001:0db8::1";
        ICidrGuess cidrGess = CidrGuess.ClassLess;

        var ipnetwork = IPNetwork2.Parse(ipaddress, cidrGess);

        string network = "2001:db8::1";
        string netmask = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff";
        string firstUsable = "2001:db8::1";
        string lastUsable = "2001:db8::1";
        byte cidr = 128;
        int usable = 1;

        Assert.AreEqual(network, ipnetwork.Network.ToString(), "Network");
        Assert.AreEqual(netmask, ipnetwork.Netmask.ToString(), "Netmask");
        Assert.AreEqual(null, ipnetwork.Broadcast, "Broadcast");
        Assert.AreEqual(cidr, ipnetwork.Cidr, "Cidr");
        Assert.AreEqual(usable, ipnetwork.Usable, "Usable");
        Assert.AreEqual(firstUsable, ipnetwork.FirstUsable.ToString(), "FirstUsable");
        Assert.AreEqual(lastUsable, ipnetwork.LastUsable.ToString(), "LastUsable");
    }

    /// <summary>
    /// Test ParseString garbage.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestParseStringAe1()
    {
        string ipaddress = "garbage";
        IPNetwork2.Parse(ipaddress);
    }

    /// <summary>
    /// Test ParseString too long.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestParseStringAe2()
    {
        string ipaddress = "0:0:0:0:0:0:1:0:0 0:1:2:3:4:5:6:7:8";
        IPNetwork2.Parse(ipaddress);
    }

    /// <summary>
    /// Test ParseString null.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestParseStringAne1()
    {
        IPNetwork2.Parse(null);
    }
}
