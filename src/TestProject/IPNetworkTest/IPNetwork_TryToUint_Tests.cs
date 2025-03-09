// <copyright file="IPNetwork_TryToUint_Tests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using System.Numerics;

namespace TestProject.IPNetworkTest;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// IPNetworkUnitTest test every single method.
/// </summary>
[TestClass]
public class IPNetwork_TryToUint_Tests
{

    [TestMethod]
    public void TestTryToUint1()
    {
            BigInteger? result = null;
            bool parsed = IPNetwork2.TryToUint(32, System.Net.Sockets.AddressFamily.InterNetwork, out result);

            Assert.IsNotNull(result, "uint");
            Assert.AreEqual(true, parsed, "parsed");
        }

    }