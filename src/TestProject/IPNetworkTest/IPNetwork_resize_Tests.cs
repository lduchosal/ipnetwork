// <copyright file="IPNetwork_resize_Tests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;
using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// IPNetworkUnitTest test every single method.
/// </summary>
[TestClass]
public class IPNetwork_resize_Tests
{

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestResize1()
    {
        byte[] resut = IPNetwork2.Resize(new byte[33], System.Net.Sockets.AddressFamily.InterNetwork);
    }

    }