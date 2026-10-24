// <copyright file="IPNetwork2IanaBlockUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

using System.Reflection;

/// <summary>
/// Tests the public IANA_* blocks of <see cref="IPNetwork2"/>.
/// </summary>
[TestClass]
public class IPNetwork2IanaBlockUnitTest
{
    /// <summary>
    /// Each IANA_* property describes its block, and hands out a fresh copy on every call so that no
    /// caller can rewrite the block for the whole process.
    /// </summary>
    /// <param name="property">The name of the IANA_* property.</param>
    /// <param name="expected">The block the property is expected to describe.</param>
    [TestMethod]
    [DataRow("IANA_ABLK_RESERVED1", "10.0.0.0/8")]
    [DataRow("IANA_BBLK_RESERVED1", "172.16.0.0/12")]
    [DataRow("IANA_CBLK_RESERVED1", "192.168.0.0/16")]
    [DataRow("IANA_THIS_NETWORK", "0.0.0.0/8")]
    [DataRow("IANA_LOOPBACK", "127.0.0.0/8")]
    [DataRow("IANA_LINK_LOCAL", "169.254.0.0/16")]
    [DataRow("IANA_IETF_PROTOCOL", "192.0.0.0/24")]
    [DataRow("IANA_TEST_NET1", "192.0.2.0/24")]
    [DataRow("IANA_BENCHMARK", "198.18.0.0/15")]
    [DataRow("IANA_TEST_NET2", "198.51.100.0/24")]
    [DataRow("IANA_TEST_NET3", "203.0.113.0/24")]
    [DataRow("IANA_MULTICAST", "224.0.0.0/4")]
    [DataRow("IANA_RESERVED", "240.0.0.0/4")]
    [DataRow("IANA_BROADCAST", "255.255.255.255/32")]
    [DataRow("IANA6_UNSPECIFIED", "::/128")]
    [DataRow("IANA6_LOOPBACK", "::1/128")]
    [DataRow("IANA6_IPV4_MAPPED", "::ffff:0.0.0.0/96")]
    [DataRow("IANA6_IPV4_TRANSLATION", "64:ff9b::/96")]
    [DataRow("IANA6_TEREDO", "2001::/32")]
    [DataRow("IANA6_DOCUMENTATION", "2001:db8::/32")]
    [DataRow("IANA6_UNIQUE_LOCAL", "fc00::/7")]
    [DataRow("IANA6_LINK_LOCAL", "fe80::/10")]
    [DataRow("IANA6_MULTICAST", "ff00::/8")]
    public void TestIanaBlockIsAFreshCopyOfItsBlock(string property, string expected)
    {
        PropertyInfo? info = typeof(IPNetwork2).GetProperty(property, BindingFlags.Public | BindingFlags.Static);
        Assert.IsNotNull(info, $"IPNetwork2.{property} does not exist.");

        var first = (IPNetwork2)info.GetValue(null)!;
        var second = (IPNetwork2)info.GetValue(null)!;

        Assert.AreEqual(IPNetwork2.Parse(expected), first, property);
        Assert.AreEqual(first, second, property);
        Assert.AreNotSame(first, second, $"IPNetwork2.{property} hands out a shared instance.");

        first.Value = "8.8.8.0/24";
        Assert.AreEqual(IPNetwork2.Parse(expected), second, $"Rewriting one copy of IPNetwork2.{property} changed another.");
        Assert.AreEqual(IPNetwork2.Parse(expected), info.GetValue(null), $"Rewriting a copy of IPNetwork2.{property} changed the block itself.");
    }
}
