// <copyright file="TextFormatter.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Collections.Generic;
using System.IO;
using System.Numerics;

/// <summary>
/// Renders action results as human-readable text.
/// </summary>
public class TextFormatter : IFormatter
{
    private readonly TextWriter _writer;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextFormatter"/> class.
    /// </summary>
    public TextFormatter(TextWriter writer)
    {
        _writer = writer;
    }

    /// <inheritdoc/>
    public void Write(ActionOutput output, ProgramContext ac)
    {
        output.WriteTo(this, ac);
    }

    /// <inheritdoc/>
    public void Write(ActionOutput.Networks n, ProgramContext ac)
    {
        WriteNetworkList(n.Items, ac);
    }

    /// <inheritdoc/>
    public void Write(ActionOutput.NetworkGroups g, ProgramContext ac)
    {
        for (int i = 0; i < g.Groups.Count; i++)
        {
            var group = g.Groups[i];
            if (group.Count == 0)
            {
                _writer.WriteLine("Unable to subnet ipnetwork {0} into cidr {1}", g.InputNetworks[i], g.SubnetCidr);
            }
            else
            {
                WriteNetworkList(group, ac);
            }

            WriteSeparator(g.Groups.Count, i + 1);
        }
    }

    /// <inheritdoc/>
    public void Write(ActionOutput.SubtractResults sub, ProgramContext ac)
    {
        foreach (var ipn in sub.Items)
        {
            _writer.WriteLine("{0}", ipn);
        }
    }

    /// <inheritdoc/>
    public void Write(ActionOutput.ContainResults c, ProgramContext ac)
    {
        foreach (var r in c.Items)
        {
            _writer.WriteLine("{0} contains {1} : {2}", r.Network, r.Test, r.Contains);
        }
    }

    /// <inheritdoc/>
    public void Write(ActionOutput.OverlapResults o, ProgramContext ac)
    {
        foreach (var r in o.Items)
        {
            _writer.WriteLine("{0} overlaps {1} : {2}", r.Network, r.Test, r.Overlaps);
        }
    }

    /// <inheritdoc/>
    public void Write(ActionOutput.IpAddresses ip, ProgramContext ac)
    {
        foreach (var ipnetwork in ip.InputNetworks)
        {
            if (ipnetwork.Cidr < 16)
            {
                Console.Error.WriteLine(
                    "WARNING: listing all IP addresses in {0} ({1} addresses). This may take a very long time.",
                    ipnetwork,
                    ipnetwork.Total);
            }
        }

        foreach (string addr in ip.Items)
        {
            _writer.WriteLine("{0}", addr);
        }
    }

    /// <inheritdoc/>
    public void Write(ActionOutput.Error e, ProgramContext ac)
    {
        _writer.WriteLine(e.Message);
    }

    /// <inheritdoc/>
    public void Write(ActionOutput.UsageInfo usage, ProgramContext ac)
    {
        _writer.WriteLine("Usage: {0}", usage.Synopsis);
        _writer.WriteLine("Version: {0}", usage.Version);

        foreach (var group in usage.OptionGroups)
        {
            _writer.WriteLine();
            _writer.WriteLine(group.Name);

            int maxWidth = 0;
            foreach (var opt in group.Options)
            {
                int width = 1 + opt.Flag.Length;
                if (opt.ArgName is not null)
                {
                    width += 1 + opt.ArgName.Length;
                }

                if (width > maxWidth)
                {
                    maxWidth = width;
                }
            }

            foreach (var opt in group.Options)
            {
                string flagPart = opt.ArgName is not null
                    ? $"-{opt.Flag} {opt.ArgName}"
                    : $"-{opt.Flag}";
                _writer.WriteLine("\t{0} : {1}", flagPart.PadRight(maxWidth), opt.Description);
            }
        }

        _writer.WriteLine();
        _writer.WriteLine("networks  : {0} ", usage.NetworksDescription);
        _writer.WriteLine("            ({0} )", string.Join(" ", usage.NetworksExamples));
    }

    private void WriteNetworkList(List<IPNetwork2> networks, ProgramContext ac)
    {
        for (int i = 0; i < networks.Count; i++)
        {
            WriteNetwork(networks[i], ac);
            WriteSeparator(networks.Count, i + 1);
        }
    }

    private void WriteNetwork(IPNetwork2 ipn, ProgramContext ac)
    {
        if (ac.IPNetwork) { _writer.WriteLine("IPNetwork   : {0}", ipn); }
        if (ac.Network) { _writer.WriteLine("Network     : {0}", ipn.Network); }
        if (ac.Netmask) { _writer.WriteLine("Netmask     : {0}", ipn.Netmask); }
        if (ac.Cidr) { _writer.WriteLine("Cidr        : {0}", ipn.Cidr); }
        if (ac.Broadcast) { _writer.WriteLine("Broadcast   : {0}", ipn.Broadcast); }
        if (ac.FirstUsable) { _writer.WriteLine("FirstUsable : {0}", ipn.FirstUsable); }
        if (ac.LastUsable) { _writer.WriteLine("LastUsable  : {0}", ipn.LastUsable); }
        if (ac.Usable) { _writer.WriteLine("Usable      : {0}", ipn.Usable); }
        if (ac.Total) { _writer.WriteLine("Total       : {0}", ipn.Total); }
    }

    private void WriteSeparator(BigInteger max, BigInteger index)
    {
        if (max > 1 && index != max)
        {
            _writer.WriteLine("--");
        }
    }
}
