// <copyright file="IPNetworkResizeTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;

/// <summary>
/// Test resize fuctionnalities.
/// </summary>
[TestClass]
public class IPNetworkResizeTests
{
    /// <summary>
    /// Resize a too big ipnetowkr 
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestResize1()
    {
            byte[] resut = IPNetwork2.Resize(new byte[33], AddressFamily.InterNetwork);
        }
}