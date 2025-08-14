// <copyright file="ConsoleUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

/// <summary>
/// Test the console.
/// </summary>
[TestClass]
public class ConsoleUnitTest
{
    /// <summary>
    /// Test the console.
    /// </summary>
    /// <param name="args">The arguments.</param>
    [TestMethod]
    [DataRow(["10.0.0.0/8"])]
    [DataRow(["-Nnmcbflu", "10.0.0.0/8"])]
    [DataRow(["-N", "-n", "-m", "-c", "-b", "-f", "-l", "-u", "10.0.0.0/8"])]
    [DataRow(["10.0.0.0"])]
    [DataRow(["-s", "16", "10.0.0.0/8"])]
    [DataRow([
        "-d", "24", "-x", "-S", "192.168.168.0/24", "192.168.169.1/24", "192.168.170.2/24", "192.168.171.3/24",
        "192.168.172.3/24", "1.1.1.1"
    ])]
    [DataRow([
        "-i", "192.168.168.0/24", "192.168.169.1/24", "192.168.170.2/24", "192.168.171.3/24", "192.168.172.3/24",
        "1.1.1.1"
    ])]
    [DataRow([
        "-d", "24", "-w", "192.168.168.0/24", "192.168.169.1/24", "192.168.170.2/24", "192.168.171.3/24",
        "192.168.172.3/24", "1.1.1.1"
    ])]
    [DataRow([":"])]
    [DataRow(["-C", "10.0.0.0/8", "1.1.1.1"])]
    [DataRow(["-o", "10.0.0.0/8", "1.1.1.1"])]
    public void TestProgramMain(string[] args)
    {
            Program.Main(args);
        }
}