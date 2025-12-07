// <copyright file="IPAddressExtensionTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

/// <summary>
///     A collection of unit tests exercising the IPAddressExtensions class.
/// </summary>
[TestClass]
public class IPAddressExtensionTests
{
    /// <summary>
    ///     Test converting a variety of IPv4 addreses into single-address networks.
    /// </summary>
    [TestMethod]
    [DataRow("0.0.0.0", 4)]
    [DataRow("0.0.0.1", 4)]
    [DataRow("0.255.255.254", 4)]
    [DataRow("0.255.255.255", 4)]
    [DataRow("1.0.0.0", 4)]
    [DataRow("1.1.1.1", 4)]
    [DataRow("123.45.67.89", 4)]
    [DataRow("126.255.255.254", 4)]
    [DataRow("126.255.255.255", 4)]
    [DataRow("127.0.0.0", 4)]
    [DataRow("127.0.0.1", 4)]
    [DataRow("127.255.255.254", 4)]
    [DataRow("127.255.255.255", 4)]
    [DataRow("192.168.0.0", 4)]
    [DataRow("192.168.0.1", 4)]
    [DataRow("192.168.0.254", 4)]
    [DataRow("192.168.0.255", 4)]
    [DataRow("192.168.255.255", 4)]
    [DataRow("239.255.255.255", 4)]
    [DataRow("240.0.0.0", 4)]
    [DataRow("255.255.255.255", 4)]
    
    [DataRow("::1",16)]
    [DataRow("::",16)]
    [DataRow("::1234:ABCD",16)]
    [DataRow("1234::ABCD",16)]
    [DataRow("1234:ABCD::",16)]
    [DataRow("1:2:3:4:5:6:7:8",16)]
    [DataRow("FFFF:FFFF:FFFF:FFFF:FFFF:FFFF:FFFF:FFFF",16)]
    public void TestIPAddressToIPNetwork_SingleAddress_IPv4(string ipAddress, int bytecount)
    {
        // Prepare
        var ipAddr = IPAddress.Parse(ipAddress);
        int expectedSize = bytecount * 8;
        var net = ipAddr.AsIPNetwork();
        
        Assert.AreEqual($"{ipAddr}/{expectedSize}", $"{net}");
        Assert.AreEqual(ipAddr, net.FirstUsable);
        Assert.AreEqual(ipAddr, net.LastUsable);
        Assert.AreEqual(1, net.Total);
        Assert.AreEqual(expectedSize, net.Cidr);
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void IPAddressToIPNetwork_SingleAddress_IPv4_Random()
    {
        /* Start the collection of test cases with the addresses from the caller. */
        int byteCount = 4;
        var addrs = RandomIPs(byteCount);
        int expectedSize = byteCount * 8;

        /* Loop through all of the test cases. */
        foreach (IPAddress ipAddr in addrs.Select(IPAddress.Parse))
        {
            var net = ipAddr.AsIPNetwork();
            
            Assert.AreEqual($"{ipAddr}/{expectedSize}", $"{net}");
            Assert.AreEqual(ipAddr, net.FirstUsable);
            Assert.AreEqual(ipAddr, net.LastUsable);
            Assert.AreEqual(1, net.Total);
            Assert.AreEqual(expectedSize, net.Cidr);
        }
    }

    /// <summary>
    ///     Generate random but deterministic IPs.
    /// </summary>
    /// <param name="byteCount">4 for IPv4. 16 for IPv6.</param>
    /// <returns>Collection of random IP addresses.</returns>
    private static IEnumerable<string> RandomIPs(int byteCount)
    {
        /* Start from a fixed starting byte array.
      ng a GUID's bytes so the sequence will be unique, with the first
      e XOR'd with the byte count so the two sequences will be different. */
        byte[] hashInput = Guid.Parse("12f2c3ba-7bd1-4ec3-922c-a5625b8f5dd5").ToByteArray();
        hashInput[0] ^= (byte)byteCount;

        /* Loop many times. */
        foreach (int _ in Enumerable.Range(1, 1000))
        {
            /* Hash the current interation to get a new block of deterministic bytes. */
            hashInput = SHA256.HashData(hashInput);

            /* Convert the first n bytes for an address. 4 will have an IPv4. 16 will make an IPv6. */
            yield return new IPAddress(hashInput.Take(byteCount).ToArray()).ToString();
        }
    }
}