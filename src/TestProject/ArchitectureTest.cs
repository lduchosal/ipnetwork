// <copyright file="ArchitectureTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

using System.Net;
using NetArchTest.Rules;

/// <summary>
/// Architecture tests enforcing structural rules across the codebase.
/// </summary>
[TestClass]
public class ArchitectureTest
{
    private static readonly System.Reflection.Assembly LibraryAssembly = typeof(IPNetwork2).Assembly;
    private static readonly System.Reflection.Assembly CliAssembly = typeof(Program).Assembly;

    // ---------------------------------------------------------------
    // Layer dependency rules
    // ---------------------------------------------------------------

    /// <summary>
    /// The library (System.Net.IPNetwork) must not depend on the CLI (ConsoleApplication).
    /// </summary>
    [TestMethod]
    public void Library_ShouldNot_DependOnCli()
    {
        TestResult result = Types.InAssembly(LibraryAssembly)
            .ShouldNot()
            .HaveDependencyOnAny("ipnetwork")
            .GetResult();

        Assert.IsTrue(result.IsSuccessful, FormatFailures("Library depends on CLI", result));
    }

    /// <summary>
    /// The library must not reference Gnu.Getopt.
    /// </summary>
    [TestMethod]
    public void Library_ShouldNot_DependOnGnuGetopt()
    {
        TestResult result = Types.InAssembly(LibraryAssembly)
            .ShouldNot()
            .HaveDependencyOnAny("Gnu.Getopt")
            .GetResult();

        Assert.IsTrue(result.IsSuccessful, FormatFailures("Library depends on Gnu.Getopt", result));
    }

    // ---------------------------------------------------------------
    // IFormatter interface rules
    // ---------------------------------------------------------------

    /// <summary>
    /// All classes implementing IFormatter must be in System.Net namespace.
    /// </summary>
    [TestMethod]
    public void Formatters_ShouldResideIn_SystemNet()
    {
        TestResult result = Types.InAssembly(CliAssembly)
            .That()
            .ImplementInterface<IFormatter>()
            .Should()
            .ResideInNamespace("System.Net")
            .GetResult();

        Assert.IsTrue(result.IsSuccessful, FormatFailures("Formatter not in System.Net namespace", result));
    }

    /// <summary>
    /// All IFormatter implementations must actually implement the interface.
    /// </summary>
    [TestMethod]
    public void Formatters_ShouldImplement_IFormatter()
    {
        Type[] formatters = GetImplementations<IFormatter>(CliAssembly);
        Assert.IsNotEmpty(formatters, "No IFormatter implementations found");
    }

    /// <summary>
    /// There must be at least two formatter implementations (text and JSON).
    /// </summary>
    [TestMethod]
    public void Formatters_ShouldHave_AtLeastTwoImplementations()
    {
        Type[] formatters = GetImplementations<IFormatter>(CliAssembly);
        Assert.IsGreaterThanOrEqualTo(2, formatters.Length,
            $"Expected at least 2 IFormatter implementations, found {formatters.Length}");
    }

    // ---------------------------------------------------------------
    // ActionOutput discriminated union rules
    // ---------------------------------------------------------------

    /// <summary>
    /// ActionOutput must be abstract.
    /// </summary>
    [TestMethod]
    public void ActionOutput_ShouldBe_Abstract()
    {
        Assert.IsTrue(typeof(ActionOutput).IsAbstract, "ActionOutput must be abstract");
    }

    /// <summary>
    /// All nested types in ActionOutput must be sealed.
    /// </summary>
    [TestMethod]
    public void ActionOutput_NestedTypes_ShouldBe_Sealed()
    {
        Type[] nestedTypes = typeof(ActionOutput).GetNestedTypes();

        Assert.IsNotEmpty(nestedTypes, "ActionOutput has no nested types");

        foreach (Type type in nestedTypes)
        {
            Assert.IsTrue(type.IsSealed, $"ActionOutput.{type.Name} must be sealed");
        }
    }

    /// <summary>
    /// All nested types in ActionOutput must inherit from ActionOutput.
    /// </summary>
    [TestMethod]
    public void ActionOutput_NestedTypes_ShouldInherit_ActionOutput()
    {
        Type[] nestedTypes = typeof(ActionOutput).GetNestedTypes();

        foreach (Type type in nestedTypes)
        {
            Assert.IsTrue(
                typeof(ActionOutput).IsAssignableFrom(type),
                $"ActionOutput.{type.Name} does not inherit from ActionOutput");
        }
    }

    /// <summary>
    /// All nested types in ActionOutput must override WriteTo.
    /// </summary>
    [TestMethod]
    public void ActionOutput_NestedTypes_ShouldOverride_WriteTo()
    {
        Type[] nestedTypes = typeof(ActionOutput).GetNestedTypes();

        foreach (Type type in nestedTypes)
        {
            System.Reflection.MethodInfo? method = type.GetMethod("WriteTo",
                [typeof(IFormatter), typeof(ProgramContext)]);
            Assert.IsNotNull(method,
                $"ActionOutput.{type.Name} must override WriteTo(IFormatter, ProgramContext)");
            Assert.AreEqual(type, method.DeclaringType,
                $"ActionOutput.{type.Name} must override WriteTo, not inherit it");
        }
    }

    /// <summary>
    /// ActionOutput must not have public constructors (closed hierarchy).
    /// </summary>
    [TestMethod]
    public void ActionOutput_ShouldNotHave_PublicConstructors()
    {
        System.Reflection.ConstructorInfo[] constructors = typeof(ActionOutput)
            .GetConstructors(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

        Assert.IsEmpty(constructors,
            "ActionOutput must not have public constructors (closed hierarchy)");
    }

    /// <summary>
    /// There should be at least 8 ActionOutput subtypes.
    /// </summary>
    [TestMethod]
    public void ActionOutput_ShouldHave_AtLeast8Subtypes()
    {
        Type[] nestedTypes = typeof(ActionOutput).GetNestedTypes();
        Assert.IsGreaterThanOrEqualTo(8, nestedTypes.Length,
            $"Expected at least 8 ActionOutput subtypes, found {nestedTypes.Length}");
    }

    // ---------------------------------------------------------------
    // IPNetwork2 rules
    // ---------------------------------------------------------------

    /// <summary>
    /// IPNetwork2 must be sealed.
    /// </summary>
    [TestMethod]
    public void IPNetwork2_ShouldBe_Sealed()
    {
        Assert.IsTrue(typeof(IPNetwork2).IsSealed, "IPNetwork2 must be sealed");
    }

    /// <summary>
    /// IPNetwork2 must implement ISerializable.
    /// </summary>
    [TestMethod]
    public void IPNetwork2_ShouldImplement_ISerializable()
    {
        Assert.IsTrue(
            typeof(System.Runtime.Serialization.ISerializable).IsAssignableFrom(typeof(IPNetwork2)),
            "IPNetwork2 must implement ISerializable");
    }

    // ---------------------------------------------------------------
    // ICidrGuess strategy pattern rules
    // ---------------------------------------------------------------

    /// <summary>
    /// All ICidrGuess implementations must be sealed.
    /// </summary>
    [TestMethod]
    public void CidrGuess_Implementations_ShouldBe_Sealed()
    {
        TestResult result = Types.InAssembly(LibraryAssembly)
            .That()
            .ImplementInterface<ICidrGuess>()
            .Should()
            .BeSealed()
            .GetResult();

        Assert.IsTrue(result.IsSuccessful, FormatFailures("ICidrGuess implementation not sealed", result));
    }

    /// <summary>
    /// There must be at least 3 ICidrGuess implementations.
    /// </summary>
    [TestMethod]
    public void CidrGuess_ShouldHave_AtLeast3Implementations()
    {
        Type[] implementations = GetImplementations<ICidrGuess>(LibraryAssembly);
        Assert.IsGreaterThanOrEqualTo(3, implementations.Length,
            $"Expected at least 3 ICidrGuess implementations, found {implementations.Length}");
    }

    // ---------------------------------------------------------------
    // Static class rules
    // ---------------------------------------------------------------

    /// <summary>
    /// Program must be static.
    /// </summary>
    [TestMethod]
    public void Program_ShouldBe_Static()
    {
        Assert.IsTrue(typeof(Program).IsAbstract && typeof(Program).IsSealed,
            "Program must be static");
    }

    /// <summary>
    /// ActionComputer must be static.
    /// </summary>
    [TestMethod]
    public void ActionComputer_ShouldBe_Static()
    {
        Assert.IsTrue(typeof(ActionComputer).IsAbstract && typeof(ActionComputer).IsSealed,
            "ActionComputer must be static");
    }

    // ---------------------------------------------------------------
    // Naming convention rules
    // ---------------------------------------------------------------

    /// <summary>
    /// Interfaces in the CLI assembly must start with 'I'.
    /// </summary>
    [TestMethod]
    public void Cli_Interfaces_ShouldStartWith_I()
    {
        TestResult result = Types.InAssembly(CliAssembly)
            .That()
            .AreInterfaces()
            .Should()
            .HaveNameStartingWith("I")
            .GetResult();

        Assert.IsTrue(result.IsSuccessful, FormatFailures("Interface does not start with 'I'", result));
    }

    /// <summary>
    /// Interfaces in the library must start with 'I'.
    /// </summary>
    [TestMethod]
    public void Library_Interfaces_ShouldStartWith_I()
    {
        TestResult result = Types.InAssembly(LibraryAssembly)
            .That()
            .AreInterfaces()
            .Should()
            .HaveNameStartingWith("I")
            .GetResult();

        Assert.IsTrue(result.IsSuccessful, FormatFailures("Library interface does not start with 'I'", result));
    }

    /// <summary>
    /// All enums in the CLI must be public.
    /// </summary>
    [TestMethod]
    public void Cli_Enums_ShouldBe_Public()
    {
        TestResult result = Types.InAssembly(CliAssembly)
            .That()
            .AreEnums()
            .Should()
            .BePublic()
            .GetResult();

        Assert.IsTrue(result.IsSuccessful, FormatFailures("Enum is not public", result));
    }

    // ---------------------------------------------------------------
    // Namespace rules
    // ---------------------------------------------------------------

    /// <summary>
    /// All CLI types must reside in System.Net namespace.
    /// </summary>
    [TestMethod]
    public void Cli_Types_ShouldResideIn_SystemNet()
    {
        TestResult result = Types.InAssembly(CliAssembly)
            .Should()
            .ResideInNamespace("System.Net")
            .GetResult();

        Assert.IsTrue(result.IsSuccessful, FormatFailures("CLI type not in System.Net", result));
    }

    /// <summary>
    /// All public library types must reside in System.Net namespace.
    /// </summary>
    [TestMethod]
    public void Library_PublicTypes_ShouldResideIn_SystemNet()
    {
        TestResult result = Types.InAssembly(LibraryAssembly)
            .That()
            .ArePublic()
            .Should()
            .ResideInNamespace("System.Net")
            .GetResult();

        Assert.IsTrue(result.IsSuccessful, FormatFailures("Library type not in System.Net", result));
    }

    // ---------------------------------------------------------------
    // DTO / data class rules
    // ---------------------------------------------------------------

    /// <summary>
    /// ContainInfo must be sealed.
    /// </summary>
    [TestMethod]
    public void ContainInfo_ShouldBe_Sealed()
    {
        Assert.IsTrue(typeof(ContainInfo).IsSealed, "ContainInfo must be sealed");
    }

    /// <summary>
    /// OverlapInfo must be sealed.
    /// </summary>
    [TestMethod]
    public void OverlapInfo_ShouldBe_Sealed()
    {
        Assert.IsTrue(typeof(OverlapInfo).IsSealed, "OverlapInfo must be sealed");
    }

    /// <summary>
    /// UsageOptionGroup must be sealed.
    /// </summary>
    [TestMethod]
    public void UsageOptionGroup_ShouldBe_Sealed()
    {
        Assert.IsTrue(typeof(UsageOptionGroup).IsSealed, "UsageOptionGroup must be sealed");
    }

    /// <summary>
    /// UsageOption must be sealed.
    /// </summary>
    [TestMethod]
    public void UsageOption_ShouldBe_Sealed()
    {
        Assert.IsTrue(typeof(UsageOption).IsSealed, "UsageOption must be sealed");
    }

    // ---------------------------------------------------------------
    // No Console dependency in JsonFormatter
    // ---------------------------------------------------------------

    /// <summary>
    /// JsonFormatter must not depend on System.Console (writes to injected TextWriter).
    /// </summary>
    [TestMethod]
    public void JsonFormatter_ShouldNot_DependOn_SystemConsole()
    {
        TestResult result = Types.InAssembly(CliAssembly)
            .That()
            .HaveName("JsonFormatter")
            .ShouldNot()
            .HaveDependencyOnAny("System.Console")
            .GetResult();

        Assert.IsTrue(result.IsSuccessful,
            FormatFailures("JsonFormatter depends on System.Console", result));
    }

    // ---------------------------------------------------------------
    // Extension method rules
    // ---------------------------------------------------------------

    /// <summary>
    /// Extension method classes in the library must be static.
    /// </summary>
    [TestMethod]
    public void Library_ExtensionClasses_ShouldBe_Static()
    {
        TestResult result = Types.InAssembly(LibraryAssembly)
            .That()
            .HaveNameEndingWith("Extensions")
            .Should()
            .BeStatic()
            .GetResult();

        Assert.IsTrue(result.IsSuccessful, FormatFailures("Extension class is not static", result));
    }

    // ---------------------------------------------------------------
    // Test class rules
    // ---------------------------------------------------------------

    /// <summary>
    /// All test classes must be public.
    /// </summary>
    [TestMethod]
    public void TestClasses_ShouldBe_Public()
    {
        System.Reflection.Assembly testAssembly = typeof(ArchitectureTest).Assembly;
        TestResult result = Types.InAssembly(testAssembly)
            .That()
            .HaveNameEndingWith("Test")
            .Should()
            .BePublic()
            .GetResult();

        Assert.IsTrue(result.IsSuccessful, FormatFailures("Test class is not public", result));
    }

    /// <summary>
    /// Test classes must not depend on Gnu.Getopt directly.
    /// </summary>
    [TestMethod]
    public void TestClasses_ShouldNot_DependOnGnuGetopt()
    {
        System.Reflection.Assembly testAssembly = typeof(ArchitectureTest).Assembly;
        TestResult result = Types.InAssembly(testAssembly)
            .That()
            .HaveNameEndingWith("Test")
            .ShouldNot()
            .HaveDependencyOnAny("Gnu.Getopt")
            .GetResult();

        Assert.IsTrue(result.IsSuccessful, FormatFailures("Test class depends on Gnu.Getopt", result));
    }

    // ---------------------------------------------------------------
    // Collection classes rules
    // ---------------------------------------------------------------

    /// <summary>
    /// IPAddressCollection must implement IEnumerable of IPAddress.
    /// </summary>
    [TestMethod]
    public void IPAddressCollection_ShouldImplement_IEnumerable()
    {
        Assert.IsTrue(
            typeof(IEnumerable<System.Net.IPAddress>)
                .IsAssignableFrom(typeof(IPAddressCollection)),
            "IPAddressCollection must implement IEnumerable<IPAddress>");
    }

    /// <summary>
    /// IPNetworkCollection must implement IEnumerable of IPNetwork2.
    /// </summary>
    [TestMethod]
    public void IPNetworkCollection_ShouldImplement_IEnumerable()
    {
        Assert.IsTrue(
            typeof(IEnumerable<IPNetwork2>)
                .IsAssignableFrom(typeof(IPNetworkCollection)),
            "IPNetworkCollection must implement IEnumerable<IPNetwork2>");
    }

    // ---------------------------------------------------------------
    // ProgramContext rules
    // ---------------------------------------------------------------

    /// <summary>
    /// ProgramContext must have a public parameterless constructor.
    /// </summary>
    [TestMethod]
    public void ProgramContext_ShouldHave_PublicParameterlessConstructor()
    {
        System.Reflection.ConstructorInfo? ctor = typeof(ProgramContext).GetConstructor(Type.EmptyTypes);
        Assert.IsNotNull(ctor, "ProgramContext must have a public parameterless constructor");
        Assert.IsTrue(ctor.IsPublic, "ProgramContext constructor must be public");
    }

    // ---------------------------------------------------------------
    // Helpers
    // ---------------------------------------------------------------

    private static Type[] GetImplementations<T>(System.Reflection.Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(t => typeof(T).IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false })
            .ToArray();
    }

    private static string FormatFailures(string message, TestResult result)
    {
        if (result.IsSuccessful || result.FailingTypes == null)
        {
            return message;
        }

        string names = string.Join(", ", result.FailingTypes.Select(t => t.FullName));
        return $"{message}: [{names}]";
    }
}
