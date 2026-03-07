// <copyright file="IPNetworkPrintTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test.
/// </summary>
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

}