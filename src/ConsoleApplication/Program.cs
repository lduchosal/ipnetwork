// <copyright file="Program.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using Gnu.Getopt;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Console app for IPNetwork.
/// </summary>
public static class Program
{
    private static readonly Dictionary<int, ArgParsed> Args = new ();

    private static readonly ArgParsed[] ArgsList =
    [
        // Print options
        new ArgParsed('i', "Print options", "network", (ac, _) => { ac.IPNetwork = true; }),
        new ArgParsed('n', "Print options", "network address", (ac, _) => { ac.Network = true; }),
        new ArgParsed('m', "Print options", "netmask", (ac, _) => { ac.Netmask = true; }),
        new ArgParsed('c', "Print options", "cidr", (ac, _) => { ac.Cidr = true; }),
        new ArgParsed('b', "Print options", "broadcast", (ac, _) => { ac.Broadcast = true; }),
        new ArgParsed('f', "Print options", "first usable ip address", (ac, _) => { ac.FirstUsable = true; }),
        new ArgParsed('l', "Print options", "last usable ip address", (ac, _) => { ac.LastUsable = true; }),
        new ArgParsed('u', "Print options", "number of usable ip addresses", (ac, _) => { ac.Usable = true; }),
        new ArgParsed('t', "Print options", "total number of ip addresses", (ac, _) => { ac.Total = true; }),

        // Output options
        new ArgParsed('j', "Output options", "JSON output", (ac, _) => { ac.Json = true; }),

        // Parse options
        new ArgParsed('D', "Parse options", "IPv4 only - use default cidr (ClassA/8, ClassB/16, ClassC/24)",
            (ac, _) => { ac.CidrParse = CidrParse.Default; }),
        new ArgParsed('d', "Parse options", "use cidr if not provided (default /32)", (ac, arg) =>
        {
            if (!IPNetwork2.TryParseCidr(arg, Sockets.AddressFamily.InterNetwork, out byte? cidr))
            {
                ac.ParseErrors.Add(string.Format("Invalid cidr {0}", cidr));
                ac.Action = Action.Usage;
                return;
            }

            ac.CidrParse = CidrParse.Value;
            ac.CidrParsed = (byte)cidr;
        }, argName: "cidr"),

        // Actions
        new ArgParsed('h', "Actions", "help message",
            (ac, _) => { ac.Action = Action.Usage; },
            example: "ipnetwork -h"),
        new ArgParsed('s', "Actions", "split network into cidr subnets", (ac, arg) =>
        {
            if (!IPNetwork2.TryParseCidr(arg, Sockets.AddressFamily.InterNetwork, out byte? cidr))
            {
                ac.ParseErrors.Add(string.Format("Invalid cidr {0}", cidr));
                ac.Action = Action.Usage;
                return;
            }

            ac.Action = Action.Subnet;
            ac.SubnetCidr = (byte)cidr;
        }, argName: "cidr", example: "ipnetwork -s 24 10.0.0.0/8"),
        new ArgParsed('w', "Actions", "supernet networks into smallest possible subnets",
            (ac, _) => { ac.Action = Action.Supernet; },
            example: "ipnetwork -w 10.0.0.0/24 10.0.1.0/24"),
        new ArgParsed('W', "Actions", "supernet networks into one single subnet",
            (ac, _) => { ac.Action = Action.WideSupernet; },
            example: "ipnetwork -W 10.0.0.0/24 10.0.10.0/24"),
        new ArgParsed('x', "Actions", "list all ip addresses in networks",
            (ac, _) => { ac.Action = Action.ListIPAddress; },
            example: "ipnetwork -x 10.0.0.0/30"),
        new ArgParsed('C', "Actions", "network contain networks", (ac, arg) =>
        {
            if (!TryParseIPNetwork(arg, ac.CidrParse, ac.CidrParsed, out IPNetwork2? ipnetwork))
            {
                ac.ParseErrors.Add($"Unable to parse ipnetwork {arg}");
                ac.Action = Action.Usage;
                return;
            }

            ac.Action = Action.ContainNetwork;
            ac.ContainNetwork = ipnetwork;
        }, argName: "network", example: "ipnetwork -C 10.0.0.0/8 10.0.1.0/24"),
        new ArgParsed('o', "Actions", "network overlap networks", (ac, arg) =>
        {
            if (!TryParseIPNetwork(arg, ac.CidrParse, ac.CidrParsed, out IPNetwork2? ipnetwork))
            {
                ac.ParseErrors.Add(string.Format("Unable to parse ipnetwork {0}", arg));
                ac.Action = Action.Usage;
                return;
            }

            ac.Action = Action.OverlapNetwork;
            ac.OverlapNetwork = ipnetwork;
        }, argName: "network", example: "ipnetwork -o 10.0.0.0/8 192.168.0.0/16"),
        new ArgParsed('S', "Actions", "subtract network from networks", (ac, arg) =>
        {
            if (!TryParseIPNetwork(arg, ac.CidrParse, ac.CidrParsed, out IPNetwork2? ipnetwork))
            {
                ac.ParseErrors.Add(string.Format("Unable to parse ipnetwork {0}", arg));
                ac.Action = Action.Usage;
                return;
            }

            ac.Action = Action.SubtractNetwork;
            ac.SubtractNetwork = ipnetwork;
        }, argName: "network", example: "ipnetwork -S 10.0.1.0/24 10.0.0.0/23"),

        // Hidden
        new ArgParsed('?', (_, _) => { }),
    ];

    /// <summary>
    /// Program entry point.
    /// </summary>
    /// <param name="args">program arguments.</param>
    public static void Main(string[] args)
    {
        ProgramContext ac = ParseArgs(args);
        ActionOutput output = ActionComputer.Compute(ac, ArgsList);
        IFormatter formatter = ac.Json ? new JsonFormatter(Console.Out) : new TextFormatter(Console.Out);
        formatter.Write(output, ac);
    }

    static Program()
    {
        foreach (ArgParsed ap in ArgsList)
        {
            Args.Add(ap.Arg, ap);
        }
    }

    private static ProgramContext ParseArgs(string[] args)
    {
        int c;
        var g = new Getopt("ipnetwork", args, "jinmcbfltud:Dhs:wWxC:o:S:");
        var ac = new ProgramContext();

        while ((c = g.getopt()) != -1)
        {
            string? optArg = g.Optarg;
            Args[c].Run(ac, optArg ?? string.Empty);
        }

        var ipnetworks = new List<string>();
        for (int i = g.Optind; i < args.Length; i++)
        {
            if (!string.IsNullOrEmpty(args[i]))
            {
                ipnetworks.Add(args[i]);
            }
        }

        ac.NetworksString = ipnetworks.ToArray();
        ParseIPNetworks(ac);

        if (ac.Networks.Length == 0 && ac.Action != Action.Usage)
        {
            ac.ParseErrors.Add("Provide at least one ipnetwork");
            ac.Action = Action.Usage;
        }

        if (ac.Action == Action.Supernet
            && ipnetworks.Count < 2)
        {
            ac.ParseErrors.Add("Supernet action required at least two ipnetworks");
            ac.Action = Action.Usage;
        }

        if (ac.Action == Action.WideSupernet
            && ipnetworks.Count < 2)
        {
            ac.ParseErrors.Add("WideSupernet action required at least two ipnetworks");
            ac.Action = Action.Usage;
        }

        if (PrintNoValue(ac))
        {
            PrintAll(ac);
        }

        if (g.Optind == 0)
        {
            PrintAll(ac);
        }

        return ac;
    }

    private static void ParseIPNetworks(ProgramContext ac)
    {
        var ipnetworks = new List<IPNetwork2>();
        foreach (string ips in ac.NetworksString)
        {
            if (!TryParseIPNetwork(ips, ac.CidrParse, ac.CidrParsed, out IPNetwork2? ipnetwork))
            {
                ac.ParseErrors.Add(string.Format("Unable to parse ipnetwork {0}", ips));
                continue;
            }

            ipnetworks.Add(ipnetwork);
        }

        ac.Networks = ipnetworks.ToArray();
    }

    private static bool TryParseIPNetwork(string ip, CidrParse cidrParse, byte cidr, [NotNullWhen(true)] out IPNetwork2? ipn)
    {
        IPNetwork2? ipnetwork = null;
        switch (cidrParse)
        {
            case CidrParse.Default when !IPNetwork2.TryParse(ip, out ipnetwork):
                ipn = null;
                return false;
            case CidrParse.Value:
            {
                if (!IPNetwork2.TryParse(ip, cidr, out ipnetwork)
                    && !IPNetwork2.TryParse(ip, out ipnetwork))
                {
                    ipn = null;
                    return false;
                }

                break;
            }
        }

        if (ipnetwork is null)
        {
            ipn = null;
            return false;
        }

        ipn = ipnetwork;
        return true;
    }

    private static bool PrintNoValue(ProgramContext ac)
    {
        ArgumentNullException.ThrowIfNull(ac);

        return ac is { IPNetwork: false, Network: false }
               && !ac.Netmask
               && !ac.Cidr
               && !ac.Broadcast
               && !ac.FirstUsable
               && !ac.LastUsable
               && !ac.Total
               && !ac.Usable;
    }

    private static void PrintAll(ProgramContext ac)
    {
        ArgumentNullException.ThrowIfNull(ac);

        ac.IPNetwork = true;
        ac.Network = true;
        ac.Netmask = true;
        ac.Cidr = true;
        ac.Broadcast = true;
        ac.FirstUsable = true;
        ac.LastUsable = true;
        ac.Usable = true;
        ac.Total = true;
    }

}
