// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using BenchmarkProject;

Console.WriteLine("Hello, World!");
var summary = BenchmarkRunner.Run<ContainsBenchmark>();
