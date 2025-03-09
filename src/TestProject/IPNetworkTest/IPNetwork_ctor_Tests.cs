// <copyright file="IPNetwork_ctor_Tests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;
using System;
using System.Net;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// IPNetworkUnitTest test every single method.
/// </summary>
[TestClass]
public class IPNetwork_ctor_Tests
{

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestCtor1()
    {
            new IPNetwork2(BigInteger.Zero, System.Net.Sockets.AddressFamily.InterNetwork, 33);
        }
    }