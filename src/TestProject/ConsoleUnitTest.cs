// <copyright file="ConsoleUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class ConsoleUnitTest
{
    /// <summary>
    /// Test.
    /// </summary>
    [DataTestMethod]
    [DataRow(new[] { "10.0.0.0/8" })]
    [DataRow(new[] { "-Nnmcbflu", "10.0.0.0/8" })]
    [DataRow(new[] { "-N", "-n", "-m", "-c", "-b", "-f", "-l", "-u", "10.0.0.0/8" })]
    [DataRow(new[] { "10.0.0.0" })]
    [DataRow(new[] { "-s", "16", "10.0.0.0/8" })]
    [DataRow(new[]
    {
        "-d", "24", "-x", "-S", "192.168.168.0/24", "192.168.169.1/24", "192.168.170.2/24", "192.168.171.3/24",
        "192.168.172.3/24", "1.1.1.1",
    })]
    [DataRow(new[]
    {
        "-i", "192.168.168.0/24", "192.168.169.1/24", "192.168.170.2/24", "192.168.171.3/24", "192.168.172.3/24",
        "1.1.1.1",
    })]
    [DataRow(new[]
    {
        "-d", "24", "-w", "192.168.168.0/24", "192.168.169.1/24", "192.168.170.2/24", "192.168.171.3/24",
        "192.168.172.3/24", "1.1.1.1",
    })]
    [DataRow(new[] { ":" })]
    [DataRow(new[] { "-C", "10.0.0.0/8", "1.1.1.1" })]
    [DataRow(new[] { "-o", "10.0.0.0/8", "1.1.1.1" })]
    public void TestProgramMain(string[] args)
    {
            Program.Main(args);
        }
}