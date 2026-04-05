// <copyright file="ConsoleJsonOutputTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

using System.Net;
using System.Text.Json;

/// <summary>
/// Tests for JSON output (-j flag).
/// </summary>
[TestClass]
public class ConsoleJsonOutputTest
{
    private static string CaptureOutput(string[] args)
    {
        var originalOut = Console.Out;
        using var sw = new StringWriter();
        Console.SetOut(sw);
        try
        {
            Program.Main(args);
        }
        finally
        {
            Console.SetOut(originalOut);
        }

        return sw.ToString().Trim();
    }

    /// <summary>
    /// Single network always returns an array with one element.
    /// </summary>
    [TestMethod]
    public void TestJsonOutputSingleNetworkReturnsArray()
    {
        string output = CaptureOutput(["-j", "10.0.0.0/8"]);
        using var doc = JsonDocument.Parse(output);
        var root = doc.RootElement;

        Assert.AreEqual(JsonValueKind.Array, root.ValueKind);
        Assert.AreEqual(1, root.GetArrayLength());

        var net = root[0];
        Assert.AreEqual("10.0.0.0/8", net.GetProperty("ipnetwork").GetString());
        Assert.AreEqual("10.0.0.0", net.GetProperty("network").GetString());
        Assert.AreEqual("255.0.0.0", net.GetProperty("netmask").GetString());
        Assert.AreEqual(8, net.GetProperty("cidr").GetInt32());
        Assert.AreEqual("10.255.255.255", net.GetProperty("broadcast").GetString());
        Assert.AreEqual("10.0.0.1", net.GetProperty("firstUsable").GetString());
        Assert.AreEqual("10.255.255.254", net.GetProperty("lastUsable").GetString());
        Assert.AreEqual("16777214", net.GetProperty("usable").GetString());
        Assert.AreEqual("16777216", net.GetProperty("total").GetString());
    }

    /// <summary>
    /// IPv6 /128 has null broadcast.
    /// </summary>
    [TestMethod]
    public void TestJsonOutputIpv6Network()
    {
        string output = CaptureOutput(["-j", "2001:db8:1:2:3:4:5:6/128"]);
        using var doc = JsonDocument.Parse(output);
        var net = doc.RootElement[0];

        Assert.AreEqual("2001:db8:1:2:3:4:5:6/128", net.GetProperty("ipnetwork").GetString());
        Assert.AreEqual(128, net.GetProperty("cidr").GetInt32());
        Assert.AreEqual(JsonValueKind.Null, net.GetProperty("broadcast").ValueKind);
    }

    /// <summary>
    /// Large IPv6 network - total must not overflow.
    /// </summary>
    [TestMethod]
    public void TestJsonOutputLargeIpv6Total()
    {
        string output = CaptureOutput(["-j", "-t", "2001:db8::/32"]);
        using var doc = JsonDocument.Parse(output);
        var net = doc.RootElement[0];

        Assert.AreEqual("79228162514264337593543950336", net.GetProperty("total").GetString());
    }

    /// <summary>
    /// Multiple networks returns array.
    /// </summary>
    [TestMethod]
    public void TestJsonOutputMultipleNetworks()
    {
        string output = CaptureOutput(["-j", "10.0.0.0/8", "192.168.0.0/16"]);
        using var doc = JsonDocument.Parse(output);
        var root = doc.RootElement;

        Assert.AreEqual(JsonValueKind.Array, root.ValueKind);
        Assert.AreEqual(2, root.GetArrayLength());
        Assert.AreEqual("10.0.0.0/8", root[0].GetProperty("ipnetwork").GetString());
        Assert.AreEqual("192.168.0.0/16", root[1].GetProperty("ipnetwork").GetString());
    }

    /// <summary>
    /// Subnet returns array of groups (one group per input network).
    /// </summary>
    [TestMethod]
    public void TestJsonOutputSubnet()
    {
        string output = CaptureOutput(["-j", "-s", "9", "10.0.0.0/8"]);
        using var doc = JsonDocument.Parse(output);
        var root = doc.RootElement;

        Assert.AreEqual(JsonValueKind.Array, root.ValueKind);
        Assert.AreEqual(1, root.GetArrayLength());

        var group = root[0];
        Assert.AreEqual(JsonValueKind.Array, group.ValueKind);
        Assert.AreEqual(2, group.GetArrayLength());
        Assert.AreEqual("10.0.0.0/9", group[0].GetProperty("ipnetwork").GetString());
        Assert.AreEqual("10.128.0.0/9", group[1].GetProperty("ipnetwork").GetString());
    }

    /// <summary>
    /// Supernet returns array.
    /// </summary>
    [TestMethod]
    public void TestJsonOutputSupernet()
    {
        string output = CaptureOutput(["-j", "-w", "10.0.0.0/24", "10.0.1.0/24"]);
        using var doc = JsonDocument.Parse(output);
        var root = doc.RootElement;

        Assert.AreEqual(JsonValueKind.Array, root.ValueKind);
        Assert.AreEqual(1, root.GetArrayLength());
        Assert.AreEqual("10.0.0.0/23", root[0].GetProperty("ipnetwork").GetString());
    }

    /// <summary>
    /// Wide supernet returns array.
    /// </summary>
    [TestMethod]
    public void TestJsonOutputWideSupernet()
    {
        string output = CaptureOutput(["-j", "-W", "10.0.0.0/24", "10.0.10.0/24"]);
        using var doc = JsonDocument.Parse(output);
        var root = doc.RootElement;

        Assert.AreEqual(JsonValueKind.Array, root.ValueKind);
        Assert.AreEqual(1, root.GetArrayLength());
        Assert.AreEqual("10.0.0.0/20", root[0].GetProperty("ipnetwork").GetString());
    }

    /// <summary>
    /// Contain returns array of results.
    /// </summary>
    [TestMethod]
    public void TestJsonOutputContain()
    {
        string output = CaptureOutput(["-j", "-C", "10.0.0.0/8", "10.0.1.0/24"]);
        using var doc = JsonDocument.Parse(output);
        var root = doc.RootElement;

        Assert.AreEqual(JsonValueKind.Array, root.ValueKind);
        Assert.AreEqual(1, root.GetArrayLength());
        Assert.AreEqual("10.0.0.0/8", root[0].GetProperty("network").GetString());
        Assert.AreEqual("10.0.1.0/24", root[0].GetProperty("test").GetString());
        Assert.IsTrue(root[0].GetProperty("contains").GetBoolean());
    }

    /// <summary>
    /// Contain with multiple test networks returns array.
    /// </summary>
    [TestMethod]
    public void TestJsonOutputContainMultiple()
    {
        string output = CaptureOutput(["-j", "-C", "10.0.0.0/8", "10.0.1.0/24", "192.168.0.0/16"]);
        using var doc = JsonDocument.Parse(output);
        var root = doc.RootElement;

        Assert.AreEqual(JsonValueKind.Array, root.ValueKind);
        Assert.AreEqual(2, root.GetArrayLength());
        Assert.IsTrue(root[0].GetProperty("contains").GetBoolean());
        Assert.IsFalse(root[1].GetProperty("contains").GetBoolean());
    }

    /// <summary>
    /// Overlap returns array of results.
    /// </summary>
    [TestMethod]
    public void TestJsonOutputOverlap()
    {
        string output = CaptureOutput(["-j", "-o", "10.0.0.0/8", "10.0.1.0/24"]);
        using var doc = JsonDocument.Parse(output);
        var root = doc.RootElement;

        Assert.AreEqual(JsonValueKind.Array, root.ValueKind);
        Assert.AreEqual(1, root.GetArrayLength());
        Assert.AreEqual("10.0.0.0/8", root[0].GetProperty("network").GetString());
        Assert.AreEqual("10.0.1.0/24", root[0].GetProperty("test").GetString());
        Assert.IsTrue(root[0].GetProperty("overlaps").GetBoolean());
    }

    /// <summary>
    /// Overlap with multiple test networks returns array.
    /// </summary>
    [TestMethod]
    public void TestJsonOutputOverlapMultiple()
    {
        string output = CaptureOutput(["-j", "-o", "10.0.0.0/8", "10.0.1.0/24", "192.168.0.0/16"]);
        using var doc = JsonDocument.Parse(output);
        var root = doc.RootElement;

        Assert.AreEqual(JsonValueKind.Array, root.ValueKind);
        Assert.AreEqual(2, root.GetArrayLength());
        Assert.IsTrue(root[0].GetProperty("overlaps").GetBoolean());
        Assert.IsFalse(root[1].GetProperty("overlaps").GetBoolean());
    }

    /// <summary>
    /// Subtract returns array.
    /// </summary>
    [TestMethod]
    public void TestJsonOutputSubtract()
    {
        string output = CaptureOutput(["-j", "-S", "10.0.1.0/24", "10.0.0.0/23"]);
        using var doc = JsonDocument.Parse(output);
        var root = doc.RootElement;

        Assert.AreEqual(JsonValueKind.Array, root.ValueKind);
        Assert.AreEqual(1, root.GetArrayLength());
        Assert.AreEqual("10.0.0.0/24", root[0].GetProperty("ipnetwork").GetString());
    }

    /// <summary>
    /// List IP addresses returns array of strings.
    /// </summary>
    [TestMethod]
    public void TestJsonOutputListIpAddress()
    {
        string output = CaptureOutput(["-j", "-x", "10.0.0.0/30"]);
        using var doc = JsonDocument.Parse(output);
        var root = doc.RootElement;

        Assert.AreEqual(JsonValueKind.Array, root.ValueKind);
        Assert.AreEqual(4, root.GetArrayLength());
        Assert.AreEqual("10.0.0.0", root[0].GetString());
        Assert.AreEqual("10.0.0.3", root[3].GetString());
    }

    /// <summary>
    /// Selected fields only appear in output.
    /// </summary>
    [TestMethod]
    public void TestJsonOutputSelectedFields()
    {
        string output = CaptureOutput(["-j", "-n", "-c", "10.0.0.0/8"]);
        using var doc = JsonDocument.Parse(output);
        var net = doc.RootElement[0];

        Assert.AreEqual("10.0.0.0", net.GetProperty("network").GetString());
        Assert.AreEqual(8, net.GetProperty("cidr").GetInt32());
        Assert.IsFalse(net.TryGetProperty("broadcast", out _));
        Assert.IsFalse(net.TryGetProperty("netmask", out _));
    }

    /// <summary>
    /// Output is always valid JSON.
    /// </summary>
    [TestMethod]
    public void TestJsonOutputIsValidJson()
    {
        string output = CaptureOutput(["-j", "192.168.1.0/24"]);
        Assert.IsNotNull(JsonDocument.Parse(output));
    }

    /// <summary>
    /// JSON help output contains version and optionGroups.
    /// </summary>
    [TestMethod]
    public void TestJsonOutputHelp()
    {
        string output = CaptureOutput(["-j", "-h"]);
        using var doc = JsonDocument.Parse(output);
        var root = doc.RootElement;

        Assert.AreEqual(JsonValueKind.Array, root.ValueKind);
        Assert.AreEqual(1, root.GetArrayLength());

        var usage = root[0];
        Assert.IsTrue(usage.TryGetProperty("version", out _));
        Assert.IsTrue(usage.TryGetProperty("synopsis", out _));
        Assert.IsTrue(usage.TryGetProperty("optionGroups", out var groups));
        Assert.IsTrue(groups.GetArrayLength() > 0);
        Assert.IsTrue(usage.TryGetProperty("positionalArgs", out _));
    }

    /// <summary>
    /// JSON help contains examples on action options.
    /// </summary>
    [TestMethod]
    public void TestJsonOutputHelpContainsExamples()
    {
        string output = CaptureOutput(["-j", "-h"]);
        using var doc = JsonDocument.Parse(output);
        var usage = doc.RootElement[0];
        var groups = usage.GetProperty("optionGroups");

        bool foundExample = false;
        foreach (var group in groups.EnumerateArray())
        {
            foreach (var opt in group.GetProperty("options").EnumerateArray())
            {
                if (opt.TryGetProperty("example", out _))
                {
                    foundExample = true;
                }
            }
        }

        Assert.IsTrue(foundExample);
    }

    /// <summary>
    /// JSON error output does not include usage.
    /// </summary>
    [TestMethod]
    public void TestJsonOutputParseError()
    {
        string output = CaptureOutput(["-j", "-w", "10.0.0.0/8"]);
        using var doc = JsonDocument.Parse(output);
        var root = doc.RootElement;

        Assert.AreEqual(JsonValueKind.Array, root.ValueKind);
        Assert.AreEqual(1, root.GetArrayLength());
        Assert.IsTrue(root[0].TryGetProperty("error", out _));
        Assert.IsFalse(root[0].TryGetProperty("version", out _));
    }

    /// <summary>
    /// Text help output contains Usage: line.
    /// </summary>
    [TestMethod]
    public void TestTextOutputHelp()
    {
        string output = CaptureOutput(["-h"]);
        Assert.IsTrue(output.Contains("Usage: ipnetwork"));
        Assert.IsTrue(output.Contains("Print options"));
        Assert.IsTrue(output.Contains("Actions"));
        Assert.IsTrue(output.Contains("networks"));
    }

    /// <summary>
    /// Text help contains examples.
    /// </summary>
    [TestMethod]
    public void TestTextOutputHelpContainsExamples()
    {
        string output = CaptureOutput(["-h"]);
        Assert.IsTrue(output.Contains("ipnetwork -s 24 10.0.0.0/8"));
        Assert.IsTrue(output.Contains("ipnetwork -C 10.0.0.0/8 10.0.1.0/24"));
    }

    /// <summary>
    /// Text error output does not include usage.
    /// </summary>
    [TestMethod]
    public void TestTextOutputParseError()
    {
        string output = CaptureOutput(["-w", "10.0.0.0/8"]);
        Assert.IsTrue(output.Contains("Supernet action required"));
        Assert.IsFalse(output.Contains("Usage:"));
    }

    /// <summary>
    /// Text wide supernet with two networks.
    /// </summary>
    [TestMethod]
    public void TestTextOutputWideSupernetSuccess()
    {
        string output = CaptureOutput(["-W", "10.0.0.0/24", "10.0.1.0/24"]);
        Assert.IsTrue(output.Contains("10.0.0.0/23"));
    }

    /// <summary>
    /// Text wide supernet error.
    /// </summary>
    [TestMethod]
    public void TestTextOutputWideSupernetError()
    {
        string output = CaptureOutput(["-W", "10.0.0.0/8"]);
        Assert.IsTrue(output.Contains("WideSupernet action required"));
    }

    /// <summary>
    /// JSON supernet with non-adjacent networks returns both.
    /// </summary>
    [TestMethod]
    public void TestJsonOutputSupernetNonAdjacent()
    {
        string output = CaptureOutput(["-j", "-w", "10.0.0.0/8", "192.168.0.0/16"]);
        using var doc = JsonDocument.Parse(output);
        Assert.AreEqual(2, doc.RootElement.GetArrayLength());
    }

    /// <summary>
    /// JSON wide supernet.
    /// </summary>
    [TestMethod]
    public void TestJsonOutputWideSupernet2()
    {
        string output = CaptureOutput(["-j", "-W", "10.0.0.0/24", "10.0.1.0/24"]);
        using var doc = JsonDocument.Parse(output);
        Assert.AreEqual(1, doc.RootElement.GetArrayLength());
    }

    /// <summary>
    /// Text list IP addresses.
    /// </summary>
    [TestMethod]
    public void TestTextOutputListIpAddress()
    {
        string output = CaptureOutput(["-x", "10.0.0.0/30"]);
        Assert.IsTrue(output.Contains("10.0.0.0"));
        Assert.IsTrue(output.Contains("10.0.0.3"));
    }

    /// <summary>
    /// Text contain output.
    /// </summary>
    [TestMethod]
    public void TestTextOutputContain()
    {
        string output = CaptureOutput(["-C", "10.0.0.0/8", "10.0.1.0/24"]);
        Assert.IsTrue(output.Contains("10.0.0.0/8 contains 10.0.1.0/24 : True"));
    }

    /// <summary>
    /// Text overlap output.
    /// </summary>
    [TestMethod]
    public void TestTextOutputOverlap()
    {
        string output = CaptureOutput(["-o", "10.0.0.0/8", "10.0.1.0/24"]);
        Assert.IsTrue(output.Contains("10.0.0.0/8 overlaps 10.0.1.0/24 : True"));
    }

    /// <summary>
    /// Text subtract output.
    /// </summary>
    [TestMethod]
    public void TestTextOutputSubtract()
    {
        string output = CaptureOutput(["-S", "10.0.1.0/24", "10.0.0.0/23"]);
        Assert.IsTrue(output.Contains("10.0.0.0/24"));
    }

    /// <summary>
    /// Text subnet output.
    /// </summary>
    [TestMethod]
    public void TestTextOutputSubnet()
    {
        string output = CaptureOutput(["-s", "9", "10.0.0.0/8"]);
        Assert.IsTrue(output.Contains("10.0.0.0/9"));
        Assert.IsTrue(output.Contains("10.128.0.0/9"));
    }

    /// <summary>
    /// Text supernet output.
    /// </summary>
    [TestMethod]
    public void TestTextOutputSupernet()
    {
        string output = CaptureOutput(["-w", "10.0.0.0/24", "10.0.1.0/24"]);
        Assert.IsTrue(output.Contains("10.0.0.0/23"));
    }

    /// <summary>
    /// Text default cidr parse.
    /// </summary>
    [TestMethod]
    public void TestTextOutputDefaultCidr()
    {
        string output = CaptureOutput(["-D", "-c", "10.0.0.0"]);
        Assert.IsTrue(output.Contains("8"));
    }

    /// <summary>
    /// Multiple networks text output with separator.
    /// </summary>
    [TestMethod]
    public void TestTextOutputMultipleNetworks()
    {
        string output = CaptureOutput(["-i", "10.0.0.0/8", "192.168.0.0/16"]);
        Assert.IsTrue(output.Contains("10.0.0.0/8"));
        Assert.IsTrue(output.Contains("192.168.0.0/16"));
        Assert.IsTrue(output.Contains("--"));
    }
}
