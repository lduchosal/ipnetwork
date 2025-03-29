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
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void IPAddressToIPNetwork_SingleAddress_IPv4()
    {
        IPAddressToIPNetwork_SingleAddress_Internal(
            4,
            "0.0.0.0",
            "0.0.0.1",
            "0.255.255.254",
            "0.255.255.255",
            "1.0.0.0",
            "1.1.1.1",
            "123.45.67.89",
            "126.255.255.254",
            "126.255.255.255",
            "127.0.0.0",
            "127.0.0.1",
            "127.255.255.254",
            "127.255.255.255",
            "192.168.0.0",
            "192.168.0.1",
            "192.168.0.254",
            "192.168.0.255",
            "192.168.255.255",
            "239.255.255.255",
            "240.0.0.0",
            "255.255.255.255");
    }

    /// <summary>
    ///     Test converting a variety of IPv6 addreses into single-address networks.
    /// </summary>
    [TestMethod]
    public void IPAddressToIPNetwork_SingleAddress_IPv6()
    {
        IPAddressToIPNetwork_SingleAddress_Internal(
            16,
            "::1",
            "::",
            "::1234:ABCD",
            "1234::ABCD",
            "1234:ABCD::",
            "1:2:3:4:5:6:7:8",
            "FFFF:FFFF:FFFF:FFFF:FFFF:FFFF:FFFF:FFFF");
    }

    /// <summary>
    ///     Shared test case, called upon by the above two test cases.
    /// </summary>
    /// <param name="byteCount">Number of bytes in the type of address being tester.</param>
    /// <param name="interestingAddrs">A collection of interesting IP addresses to include in the test.</param>
    private static void IPAddressToIPNetwork_SingleAddress_Internal(
        int byteCount,
        params string[] interestingAddrs)
    {
        /* Start the collection of test cases with the addresses from the caller. */
        var addrs = interestingAddrs.ToList();

        /* Populate with random but deterministic addresses. */
        addrs.AddRange(RandomIPs(byteCount));

        /* Loop through all of the test cases. */
        foreach (IPAddress ipAddr in addrs.Select(IPAddress.Parse))
        {
            /* Convert to network, then pass the network object to a checker. */
            TestForSingleAddressNetwork(
                ipAddr,
                ipAddr.AsIPNetwork(),
                byteCount * 8);
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
        foreach (int i in Enumerable.Range(1, 1000))
        {
            /* Hash the current interation to get a new block of deterministic bytes. */
            using (var hash = SHA256.Create())
            {
                hashInput = hash.ComputeHash(hashInput);
            }

            /* Convert the first n bytes for an address. 4 will have an IPv4. 16 will make an IPv6. */
            yield return new IPAddress(hashInput.Take(byteCount).ToArray()).ToString();
        }
    }

    /// <summary>
    ///     Test if a single address network is valid.
    /// </summary>
    /// <param name="ipAddr">Expected addresss.</param>
    /// <param name="net">Actual network.</param>
    /// <param name="expectedSize">Expected CIDRsize. (32 or 128).</param>
    private static void TestForSingleAddressNetwork(IPAddress ipAddr, IPNetwork2 net, int expectedSize)
    {
        Assert.AreEqual($"{ipAddr}/{expectedSize}", $"{net}");
        Assert.AreEqual(ipAddr, net.FirstUsable);
        Assert.AreEqual(ipAddr, net.LastUsable);
        Assert.AreEqual(1, net.Total);
        Assert.AreEqual(expectedSize, net.Cidr);
    }
}