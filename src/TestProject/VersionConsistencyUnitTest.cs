// <copyright file="VersionConsistencyUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

using System.Reflection;
using System.Text.RegularExpressions;

/// <summary>
/// Guards the single source of the version: every assembly must carry the <c>Version</c> of
/// src/Directory.Build.props, or the one a release passes with /p:Version. The test project records
/// that version at build time as the <c>ExpectedVersion</c> assembly metadata.
/// </summary>
[TestClass]
public class VersionConsistencyUnitTest
{
    /// <summary>
    /// The strong-named library pins AssemblyVersion to the major version and carries the full
    /// version in its file and informational versions.
    /// </summary>
    [TestMethod]
    public void TestLibraryVersionsFollowTheSolutionVersion()
    {
        string expected = ExpectedVersion();
        string numeric = Regex.Match(expected, @"^\d+\.\d+\.\d+").Value;
        string major = Regex.Match(expected, @"^\d+").Value;
        Assembly library = typeof(IPNetwork2).Assembly;

        Assert.AreEqual(new Version($"{major}.0.0.0"), library.GetName().Version, "AssemblyVersion");
        Assert.AreEqual(
            $"{numeric}.0",
            library.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version,
            "AssemblyFileVersion");
        StringAssert.StartsWith(
            library.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion,
            expected,
            "AssemblyInformationalVersion");
    }

#if NET5_0_OR_GREATER
    /// <summary>
    /// The console application reports its AssemblyVersion with --version, so it carries the full version.
    /// </summary>
    [TestMethod]
    public void TestConsoleVersionFollowsTheSolutionVersion()
    {
        string numeric = Regex.Match(ExpectedVersion(), @"^\d+\.\d+\.\d+").Value;

        Assert.AreEqual(new Version($"{numeric}.0"), typeof(Program).Assembly.GetName().Version, "AssemblyVersion");
    }
#endif

    private static string ExpectedVersion()
    {
        string? expected = typeof(VersionConsistencyUnitTest).Assembly
            .GetCustomAttributes<AssemblyMetadataAttribute>()
            .FirstOrDefault(attribute => attribute.Key == "ExpectedVersion")
            ?.Value;

        Assert.IsFalse(string.IsNullOrEmpty(expected), "The test project does not record ExpectedVersion.");
        return expected!;
    }
}
