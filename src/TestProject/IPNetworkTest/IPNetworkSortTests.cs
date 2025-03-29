// <copyright file="IPNetworkSortTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class IPNetworkSortTests
{
    /// <summary>
    ///     Tests Sort functionality with Sort1.
    /// </summary>
    [TestMethod]
    public void TestSort1()
    {
            string[] ips = { "1.1.1.1", "255.255.255.255", "2.2.2.2", "0.0.0.0" };
            var ipns = new List<IPNetwork2>();
            foreach (string ip in ips)
            {
                IPNetwork2 ipn;
                if (IPNetwork2.TryParse(ip, 32, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            ipns.Sort();
            Assert.AreEqual("0.0.0.0/32", ipns[0].ToString(), "0");
            Assert.AreEqual("1.1.1.1/32", ipns[1].ToString(), "1");
            Assert.AreEqual("2.2.2.2/32", ipns[2].ToString(), "2");
            Assert.AreEqual("255.255.255.255/32", ipns[3].ToString(), "3");
        }

    /// <summary>
    ///     Tests Sort functionality with Sort2.
    /// </summary>
    [TestMethod]
    public void TestSort2()
    {
            string[] ips = { "0.0.0.100/32", "0.0.0.0/24" };
            var ipns = new List<IPNetwork2>();
            foreach (string ip in ips)
            {
                IPNetwork2 ipn;
                if (IPNetwork2.TryParse(ip, out ipn))
                {
                    ipns.Add(ipn);
                }
            }

            ipns.Sort();
            Assert.AreEqual("0.0.0.0/24", ipns[0].ToString(), "0");
            Assert.AreEqual("0.0.0.100/32", ipns[1].ToString(), "1");
        }
}