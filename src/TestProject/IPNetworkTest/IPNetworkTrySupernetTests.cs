// <copyright file="IPNetworkTrySupernetTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class IPNetworkTrySupernetTests
{
    /// <summary>
    ///     Tests Try Supernet functionality with Try Supernet1.
    /// </summary>
    [TestMethod]
    public void TestTrySupernet1()
    {
            var network1 = IPNetwork2.Parse("192.168.0.1/24");
            var network2 = IPNetwork2.Parse("192.168.1.1/24");
            var supernetExpected = IPNetwork2.Parse("192.168.0.0/23");
            bool parsed = true;
            bool result = network1.TrySupernet(network2, out IPNetwork2 supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestTrySupernet2()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            IPNetwork2 network1 = null;
            var network2 = IPNetwork2.Parse("192.168.1.1/24");
            IPNetwork2 supernet;

#pragma warning disable 0618
            IPNetwork2.TrySupernet(network1, network2, out supernet);
#pragma warning restore 0618
        });
    }

    /// <summary>
    ///     Tests Try Supernet functionality with Try Supernet Static2.
    /// </summary>
    [TestMethod]
    public void TestTrySupernetStatic2()
    {
            IPNetwork2 network1 = IPNetwork2.IANA_ABLK_RESERVED1;
            var network2 = IPNetwork2.Parse("192.168.1.1/24");
            IPNetwork2 supernet;

#pragma warning disable 0618
            IPNetwork2.TrySupernet(network1, network2, out supernet);
#pragma warning restore 0618
        }

    /// <summary>
    ///     Tests Try Supernet functionality with Try Supernet3.
    /// </summary>
    [TestMethod]
    public void TestTrySupernet3()
    {
            var network1 = IPNetwork2.Parse("192.168.1.1/24");
            IPNetwork2 network2 = null;
            IPNetwork2 supernetExpected = null;
            bool parsed = false;
            bool result = network1.TrySupernet(network2, out IPNetwork2 supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

    /// <summary>
    ///     Tests Try Supernet functionality with Try Supernet4.
    /// </summary>
    [TestMethod]
    public void TestTrySupernet4()
    {
            var network1 = IPNetwork2.Parse("192.168.0.1/24");
            var network2 = IPNetwork2.Parse("192.168.1.1/25");
            IPNetwork2 supernetExpected = null;
            bool parsed = false;
            bool result = network1.TrySupernet(network2, out IPNetwork2 supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

    /// <summary>
    ///     Tests Try Supernet functionality with Try Supernet5.
    /// </summary>
    [TestMethod]
    public void TestTrySupernet5()
    {
            var network1 = IPNetwork2.Parse("192.168.0.1/24");
            var network2 = IPNetwork2.Parse("192.168.5.1/24");
            IPNetwork2 supernetExpected = null;
            bool parsed = false;
            bool result = network1.TrySupernet(network2, out IPNetwork2 supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

    /// <summary>
    ///     Tests Try Supernet functionality with Try Supernet6.
    /// </summary>
    [TestMethod]
    public void TestTrySupernet6()
    {
            var network1 = IPNetwork2.Parse("192.168.0.1/24");
            var network2 = IPNetwork2.Parse("192.168.0.1/25");
            var supernetExpected = IPNetwork2.Parse("192.168.0.0/24");
            bool parsed = true;
            bool result = network1.TrySupernet(network2, out IPNetwork2 supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

    /// <summary>
    ///     Tests Try Supernet functionality with Try Supernet7.
    /// </summary>
    [TestMethod]
    public void TestTrySupernet7()
    {
            var network1 = IPNetwork2.Parse("192.168.0.1/25");
            var network2 = IPNetwork2.Parse("192.168.0.1/24");
            var supernetExpected = IPNetwork2.Parse("192.168.0.0/24");
            bool parsed = true;
            bool result = network1.TrySupernet(network2, out IPNetwork2 supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

    /// <summary>
    ///     Tests Try Supernet functionality with Try Supernet8.
    /// </summary>
    [TestMethod]
    public void TestTrySupernet8()
    {
            var network1 = IPNetwork2.Parse("192.168.1.1/24");
            var network2 = IPNetwork2.Parse("192.168.2.1/24");
            IPNetwork2 supernetExpected = null;
            bool parsed = false;
            bool result = network1.TrySupernet(network2, out IPNetwork2 supernet);

            Assert.AreEqual(supernetExpected, supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

    /// <summary>
    ///     Tests Try Supernet functionality with Try Supernet9.
    /// </summary>
    [TestMethod]
    public void TestTrySupernet9()
    {
            var network1 = IPNetwork2.Parse("192.168.1.1/24");
            var network2 = IPNetwork2.Parse("192.168.2.1/24");
            IPNetwork2[] network3 = [network1, network2];
            IPNetwork2[] supernetExpected = [network1, network2];
            bool parsed = true;
            bool result = IPNetwork2.TrySupernet(network3, out IPNetwork2[] supernet);

            Assert.AreEqual(supernetExpected[0], supernet[0], "supernet");
            Assert.AreEqual(supernetExpected[1], supernet[1], "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

    /// <summary>
    ///     Tests Try Supernet functionality with Try Supernet10.
    /// </summary>
    [TestMethod]
    public void TestTrySupernet10()
    {
            var network1 = IPNetwork2.Parse("192.168.0.1/24");
            var network2 = IPNetwork2.Parse("192.168.1.1/24");
            IPNetwork2[] network3 = [network1, network2];
            IPNetwork2[] supernetExpected = [IPNetwork2.Parse("192.168.0.0/23")];
            bool parsed = true;
            bool result = IPNetwork2.TrySupernet(network3, out IPNetwork2[] supernet);

            Assert.AreEqual(supernetExpected[0], supernet[0], "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }

    /// <summary>
    ///     Tests Try Supernet functionality with Try Supernet11.
    /// </summary>
    [TestMethod]
    public void TestTrySupernet11()
    {
            IPNetwork2[] network3 = null;
            _ = new[] { IPNetwork2.Parse("192.168.0.0/23") };
            bool parsed = false;
            bool result = IPNetwork2.TrySupernet(network3, out IPNetwork2[] supernet);

            Assert.IsNull(supernet, "supernet");
            Assert.AreEqual(parsed, result, "parsed");
        }
}