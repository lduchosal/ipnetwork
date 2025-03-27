// <copyright file="IPNetworkResizeTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

[TestClass]
public class IPNetworkResizeTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestResize1()
    {
            byte[] resut = IPNetwork2.Resize(new byte[33], AddressFamily.InterNetwork);
        }
}