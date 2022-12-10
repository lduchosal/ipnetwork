﻿// <copyright file="ConsoleUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net.TestProject
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ConsoleUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            ConsoleApplication.Program.Main(new[] { "10.0.0.0/8" });
        }

        [TestMethod]
        public void TestMethod2()
        {
            ConsoleApplication.Program.Main(new[] { "-Nnmcbflu", "10.0.0.0/8" });
        }

        [TestMethod]
        public void TestMethod3()
        {
            ConsoleApplication.Program.Main(new[] { "-N", "-n", "-m", "-c", "-b", "-f", "-l", "-u", "10.0.0.0/8" });
        }

        [TestMethod]
        public void TestMethod4()
        {
            ConsoleApplication.Program.Main(new[] { "10.0.0.0" });
        }

        [TestMethod]
        public void TestMethod5()
        {
            ConsoleApplication.Program.Main(new[] { "-s", "16", "10.0.0.0/8" });
        }

        [TestMethod]
        public void TestMethod6()
        {
            ConsoleApplication.Program.Main(new[] { "-d", "24", "-x", "-S", "192.168.168.0/24", "192.168.169.1/24", "192.168.170.2/24", "192.168.171.3/24", "192.168.172.3/24", "1.1.1.1" });
        }

        [TestMethod]
        public void TestMethod7()
        {
            ConsoleApplication.Program.Main(new[] { "-i", "192.168.168.0/24", "192.168.169.1/24", "192.168.170.2/24", "192.168.171.3/24", "192.168.172.3/24", "1.1.1.1" });
        }

        [TestMethod]
        public void TestMethod8()
        {
            ConsoleApplication.Program.Main(new[] { "-d", "24", "-w", "192.168.168.0/24", "192.168.169.1/24", "192.168.170.2/24", "192.168.171.3/24", "192.168.172.3/24", "1.1.1.1" });
        }

        [TestMethod]
        public void TestMethod9()
        {
            ConsoleApplication.Program.Main(new[] { ":" });
        }

        [TestMethod]
        public void TestMethod10()
        {
            ConsoleApplication.Program.Main(new[] { "-C", "10.0.0.0/8", "1.1.1.1" });
        }

        [TestMethod]
        public void TestMethod11()
        {
            ConsoleApplication.Program.Main(new[] { "-o", "10.0.0.0/8", "1.1.1.1" });
        }
    }
}
