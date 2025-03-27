// <copyright file="IPNetworkPrintTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

[TestClass]
public class IPNetworkPrintTests
{
    /// <summary>
    ///     Tests Print functionality.
    /// </summary>
    [TestMethod]
    public void Print()
    {
            var ipn = IPNetwork2.Parse("0.0.0.0/0");
            string print = ipn.Print().Replace("\r", string.Empty);
            string expected = @"IPNetwork   : 0.0.0.0/0
Network     : 0.0.0.0
Netmask     : 0.0.0.0
Cidr        : 0
Broadcast   : 255.255.255.255
FirstUsable : 0.0.0.1
LastUsable  : 255.255.255.254
Usable      : 4294967294
".Replace("\r", string.Empty);

            Assert.AreEqual(expected, print, "Print");
        }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void PrintNull()
    {
            IPNetwork2 ipn = null;
#pragma warning disable 0618
            string print = IPNetwork2.Print(ipn);
#pragma warning restore 0618
        }

    /// <summary>
    ///     Tests Print functionality.
    /// </summary>
    [TestMethod]
    public void PrintStatic()
    {
            IPNetwork2 ipn = IPNetwork2.IANA_ABLK_RESERVED1;
#pragma warning disable 0618
            string print = IPNetwork2.Print(ipn);
#pragma warning restore 0618
        }
}