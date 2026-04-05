// <copyright file="JsonFormatter.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Collections.Generic;
using System.IO;
using System.Text.Json;

/// <summary>
/// Renders action results as JSON.
/// </summary>
public class JsonFormatter : IFormatter
{
    private readonly TextWriter _textWriter;
    private Utf8JsonWriter _json = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonFormatter"/> class.
    /// </summary>
    public JsonFormatter(TextWriter textWriter)
    {
        _textWriter = textWriter;
    }

    /// <inheritdoc/>
    public void Write(ActionOutput output, ProgramContext ac)
    {
        using var stream = new MemoryStream();
        using var json = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });
        _json = json;

        json.WriteStartArray();
        output.WriteTo(this, ac);
        json.WriteEndArray();
        json.Flush();

        _textWriter.Write(System.Text.Encoding.UTF8.GetString(stream.ToArray()));
        _textWriter.WriteLine();
    }

    /// <inheritdoc/>
    public void Write(ActionOutput.Networks n, ProgramContext ac)
    {
        WriteNetworks(n.Items, ac);
    }

    /// <inheritdoc/>
    public void Write(ActionOutput.NetworkGroups g, ProgramContext ac)
    {
        for (int i = 0; i < g.Groups.Count; i++)
        {
            var group = g.Groups[i];
            if (group.Count == 0)
            {
                _json.WriteStartObject();
                _json.WriteString("error",
                    $"Unable to subnet ipnetwork {g.InputNetworks[i]} into cidr {g.SubnetCidr}");
                _json.WriteEndObject();
            }
            else
            {
                _json.WriteStartArray();
                WriteNetworks(group, ac);
                _json.WriteEndArray();
            }
        }
    }

    /// <inheritdoc/>
    public void Write(ActionOutput.SubtractResults sub, ProgramContext ac)
    {
        WriteNetworks(sub.Items, ac);
    }

    /// <inheritdoc/>
    public void Write(ActionOutput.ContainResults c, ProgramContext ac)
    {
        foreach (var r in c.Items)
        {
            _json.WriteStartObject();
            _json.WriteString("network", r.Network);
            _json.WriteString("test", r.Test);
            _json.WriteBoolean("contains", r.Contains);
            _json.WriteEndObject();
        }
    }

    /// <inheritdoc/>
    public void Write(ActionOutput.OverlapResults o, ProgramContext ac)
    {
        foreach (var r in o.Items)
        {
            _json.WriteStartObject();
            _json.WriteString("network", r.Network);
            _json.WriteString("test", r.Test);
            _json.WriteBoolean("overlaps", r.Overlaps);
            _json.WriteEndObject();
        }
    }

    /// <inheritdoc/>
    public void Write(ActionOutput.IpAddresses ip, ProgramContext ac)
    {
        foreach (string addr in ip.Items)
        {
            _json.WriteStringValue(addr);
        }
    }

    /// <inheritdoc/>
    public void Write(ActionOutput.Error e, ProgramContext ac)
    {
        _json.WriteStartObject();
        _json.WriteString("error", e.Message);
        _json.WriteEndObject();
    }

    /// <inheritdoc/>
    public void Write(ActionOutput.UsageInfo usage, ProgramContext ac)
    {
        _json.WriteStartObject();
        _json.WriteString("version", usage.Version);
        _json.WriteString("synopsis", usage.Synopsis);

        _json.WritePropertyName("optionGroups");
        _json.WriteStartArray();
        foreach (var group in usage.OptionGroups)
        {
            _json.WriteStartObject();
            _json.WriteString("name", group.Name);
            _json.WritePropertyName("options");
            _json.WriteStartArray();
            foreach (var opt in group.Options)
            {
                _json.WriteStartObject();
                _json.WriteString("flag", opt.Flag);
                if (opt.ArgName is not null)
                {
                    _json.WriteString("argName", opt.ArgName);
                }

                _json.WriteString("description", opt.Description);
                _json.WriteEndObject();
            }

            _json.WriteEndArray();
            _json.WriteEndObject();
        }

        _json.WriteEndArray();

        _json.WritePropertyName("positionalArgs");
        _json.WriteStartObject();
        _json.WriteString("name", "networks");
        _json.WriteString("description", usage.NetworksDescription);
        _json.WritePropertyName("examples");
        _json.WriteStartArray();
        foreach (string example in usage.NetworksExamples)
        {
            _json.WriteStringValue(example);
        }

        _json.WriteEndArray();
        _json.WriteEndObject();

        _json.WriteEndObject();
    }

    private void WriteNetworks(List<IPNetwork2> networks, ProgramContext ac)
    {
        foreach (var ipn in networks)
        {
            WriteNetwork(ipn, ac);
        }
    }

    private void WriteNetwork(IPNetwork2 ipn, ProgramContext ac)
    {
        _json.WriteStartObject();
        if (ac.IPNetwork) { _json.WriteString("ipnetwork", ipn.ToString()); }
        if (ac.Network) { _json.WriteString("network", ipn.Network?.ToString()); }
        if (ac.Netmask) { _json.WriteString("netmask", ipn.Netmask?.ToString()); }
        if (ac.Cidr) { _json.WriteNumber("cidr", ipn.Cidr); }

        if (ac.Broadcast)
        {
            if (ipn.Broadcast is not null)
            {
                _json.WriteString("broadcast", ipn.Broadcast.ToString());
            }
            else
            {
                _json.WriteNull("broadcast");
            }
        }

        if (ac.FirstUsable) { _json.WriteString("firstUsable", ipn.FirstUsable?.ToString()); }
        if (ac.LastUsable) { _json.WriteString("lastUsable", ipn.LastUsable?.ToString()); }
        if (ac.Usable) { _json.WriteString("usable", ipn.Usable.ToString()); }
        if (ac.Total) { _json.WriteString("total", ipn.Total.ToString()); }
        _json.WriteEndObject();
    }
}
