// <copyright file="Program.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Reflection;
using Gnu.Getopt;

/// <summary>
/// Console app for IPNetwork.
/// </summary>
public static class Program
{
    private static readonly Dictionary<int, ArgParsed> Args = new ();

    private static readonly ArgParsed[] ArgsList =
    [
        new ArgParsed('i', (ac, _) => { ac.IPNetwork = true; }),
        new ArgParsed('n', (ac, _) => { ac.Network = true; }),
        new ArgParsed('m', (ac, _) => { ac.Netmask = true; }),
        new ArgParsed('c', (ac, _) => { ac.Cidr = true; }),
        new ArgParsed('b', (ac, _) => { ac.Broadcast = true; }),
        new ArgParsed('f', (ac, _) => { ac.FirstUsable = true; }),
        new ArgParsed('l', (ac, _) => { ac.LastUsable = true; }),
        new ArgParsed('u', (ac, _) => { ac.Usable = true; }),
        new ArgParsed('t', (ac, _) => { ac.Total = true; }),
        new ArgParsed('w', (ac, _) => { ac.Action = ActionEnum.Supernet; }),
        new ArgParsed('W', (ac, _) => { ac.Action = ActionEnum.WideSupernet; }),
        new ArgParsed('h', (ac, _) => { ac.Action = ActionEnum.Usage; }),
        new ArgParsed('x', (ac, _) => { ac.Action = ActionEnum.ListIPAddress; }),
        new ArgParsed('?', (_, _) => { }),
        new ArgParsed('D', (ac, _) => { ac.CidrParse = CidrParseEnum.Default; }),
        new ArgParsed('d', (ac, arg) =>
        {
            if (!IPNetwork2.TryParseCidr(arg, Sockets.AddressFamily.InterNetwork, out byte? cidr))
            {
                Console.WriteLine("Invalid cidr {0}", cidr);
                ac.Action = ActionEnum.Usage;
                return;
            }

            ac.CidrParse = CidrParseEnum.Value;
            ac.CidrParsed = (byte)cidr!;
        }),
        new ArgParsed('s', (ac, arg) =>
        {
            if (!IPNetwork2.TryParseCidr(arg, Sockets.AddressFamily.InterNetwork, out byte? cidr))
            {
                Console.WriteLine("Invalid cidr {0}", cidr);
                ac.Action = ActionEnum.Usage;
                return;
            }

            ac.Action = ActionEnum.Subnet;
            ac.SubnetCidr = (byte)cidr!;
        }),
        new ArgParsed('C', (ac, arg) =>
        {
            if (!TryParseIPNetwork(arg, ac.CidrParse, ac.CidrParsed, out IPNetwork2 ipnetwork))
            {
                Console.WriteLine("Unable to parse ipnetwork {0}", arg);
                ac.Action = ActionEnum.Usage;
                return;
            }

            ac.Action = ActionEnum.ContainNetwork;
            ac.ContainNetwork = ipnetwork;
        }),
        new ArgParsed('o', (ac, arg) =>
        {
            if (!TryParseIPNetwork(arg, ac.CidrParse, ac.CidrParsed, out IPNetwork2 ipnetwork))
            {
                Console.WriteLine("Unable to parse ipnetwork {0}", arg);
                ac.Action = ActionEnum.Usage;
                return;
            }

            ac.Action = ActionEnum.OverlapNetwork;
            ac.OverlapNetwork = ipnetwork;
        }),
        new ArgParsed('S', (ac, arg) =>
        {
            if (!TryParseIPNetwork(arg, ac.CidrParse, ac.CidrParsed, out IPNetwork2 ipnetwork))
            {
                Console.WriteLine("Unable to parse ipnetwork {0}", arg);
                ac.Action = ActionEnum.Usage;
                return;
            }

            ac.Action = ActionEnum.SubtractNetwork;
            ac.SubtractNetwork = ipnetwork;
        })
    ];

    /// <summary>
    /// Program entry point.
    /// </summary>
    /// <param name="args">program arguments.</param>
    public static void Main(string[] args)
    {
        ProgramContext ac = ParseArgs(args);

        switch (ac.Action)
        {
            case ActionEnum.Subnet:
                SubnetNetworks(ac);
                break;
            case ActionEnum.Supernet:
                SupernetNetworks(ac);
                break;
            case ActionEnum.WideSupernet:
                WideSupernetNetworks(ac);
                break;
            case ActionEnum.PrintNetworks:
                PrintNetworks(ac);
                break;
            case ActionEnum.ContainNetwork:
                ContainNetwork(ac);
                break;
            case ActionEnum.OverlapNetwork:
                OverlapNetwork(ac);
                break;
            case ActionEnum.ListIPAddress:
                ListIPAddress(ac);
                break;
            case ActionEnum.Usage:
            case ActionEnum.SubtractNetwork:
            default:
                Usage();
                break;
        }
    }

    private static void ListIPAddress(ProgramContext ac)
    {
        foreach (IPNetwork2 ipnetwork in ac.Networks)
        {
            foreach (IPAddress ipaddress in ipnetwork.ListIPAddress())
            {
                Console.WriteLine("{0}", ipaddress.ToString());
            }
        }
    }

    private static void ContainNetwork(ProgramContext ac)
    {
        foreach (IPNetwork2 ipnetwork in ac.Networks)
        {
            bool contain = ac.ContainNetwork.Contains(ipnetwork);
            Console.WriteLine("{0} contains {1} : {2}", ac.ContainNetwork, ipnetwork, contain);
        }
    }

    private static void OverlapNetwork(ProgramContext ac)
    {
        foreach (IPNetwork2 ipnetwork in ac.Networks)
        {
            bool overlap = ac.OverlapNetwork.Overlap(ipnetwork);
            Console.WriteLine("{0} overlaps {1} : {2}", ac.OverlapNetwork, ipnetwork, overlap);
        }
    }

    private static void WideSupernetNetworks(ProgramContext ac)
    {
        if (!IPNetwork2.TryWideSubnet(ac.Networks, out IPNetwork2 widesubnet))
        {
            Console.WriteLine("Unable to wide subnet these networks");
        }

        PrintNetwork(ac, widesubnet);
    }

    private static void SupernetNetworks(ProgramContext ac)
    {
        if (!IPNetwork2.TrySupernet(ac.Networks, out IPNetwork2[] supernet))
        {
            Console.WriteLine("Unable to supernet these networks");
        }

        PrintNetworks(ac, supernet, supernet.Length);
    }

    private static void PrintNetworks(ProgramContext ac, IEnumerable<IPNetwork2> ipnetworks, BigInteger networkLength)
    {
        int i = 0;
        foreach (IPNetwork2 ipn in ipnetworks)
        {
            i++;
            PrintNetwork(ac, ipn);
            PrintSeparator(networkLength, i);
        }
    }

    private static void SubnetNetworks(ProgramContext ac)
    {
        BigInteger i = 0;
        foreach (IPNetwork2 ipnetwork in ac.Networks)
        {
            i++;
            int networkLength = ac.Networks.Length;
            if (!ipnetwork.TrySubnet(ac.SubnetCidr, out IPNetworkCollection ipnetworks))
            {
                Console.WriteLine("Unable to subnet ipnetwork {0} into cidr {1}", ipnetwork, ac.SubnetCidr);
                PrintSeparator(networkLength, i);
                continue;
            }

            PrintNetworks(ac, ipnetworks, ipnetworks.Count);
            PrintSeparator(networkLength, i);
        }
    }

    // private static void PrintSeparator(Array network, int index) {
    //    if (network.Length > 1 && index != network.Length) {
    //        Console.WriteLine("--");
    //    }
    // }
    private static void PrintSeparator(BigInteger max, BigInteger index)
    {
        if (max > 1 && index != max)
        {
            Console.WriteLine("--");
        }
    }

    private static void PrintNetworks(ProgramContext ac)
    {
        int i = 0;
        foreach (IPNetwork2 ipnetwork in ac.Networks)
        {
            i++;
            PrintNetwork(ac, ipnetwork);
            PrintSeparator(ac.Networks.Length, i);
        }
    }

    private static void PrintNetwork(ProgramContext ac, IPNetwork2 ipn)
    {
        using var sw = new StringWriter();
        if (ac.IPNetwork)
        {
            sw.WriteLine("IPNetwork   : {0}", ipn);
        }

        if (ac.Network)
        {
            sw.WriteLine("Network     : {0}", ipn.Network.ToString());
        }

        if (ac.Netmask)
        {
            sw.WriteLine("Netmask     : {0}", ipn.Netmask.ToString());
        }

        if (ac.Cidr)
        {
            sw.WriteLine("Cidr        : {0}", ipn.Cidr);
        }

        if (ac.Broadcast)
        {
            sw.WriteLine("Broadcast   : {0}", ipn.Broadcast.ToString());
        }

        if (ac.FirstUsable)
        {
            sw.WriteLine("FirstUsable : {0}", ipn.FirstUsable.ToString());
        }

        if (ac.LastUsable)
        {
            sw.WriteLine("LastUsable  : {0}", ipn.LastUsable.ToString());
        }

        if (ac.Usable)
        {
            sw.WriteLine("Usable      : {0}", ipn.Usable);
        }

        if (ac.Total)
        {
            sw.WriteLine("Total       : {0}", ipn.Total);
        }

        Console.Write(sw.ToString());
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
        var g = new Getopt("ipnetwork", args, "inmcbfltud:Dhs:wWxC:o:S:");
        var ac = new ProgramContext();

        while ((c = g.getopt()) != -1)
        {
            string optArg = g.Optarg;
            Args[c].Run(ac, optArg);
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

        if (ac.Networks.Length == 0)
        {
            Console.WriteLine("Provide at least one ipnetwork");
            ac.Action = ActionEnum.Usage;
        }

        if (ac.Action == ActionEnum.Supernet
            && ipnetworks.Count < 2)
        {
            Console.WriteLine("Supernet action required at least two ipnetworks");
            ac.Action = ActionEnum.Usage;
        }

        if (ac.Action == ActionEnum.WideSupernet
            && ipnetworks.Count < 2)
        {
            Console.WriteLine("WideSupernet action required at least two ipnetworks");
            ac.Action = ActionEnum.Usage;
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
            if (!TryParseIPNetwork(ips, ac.CidrParse, ac.CidrParsed, out IPNetwork2 ipnetwork))
            {
                Console.WriteLine("Unable to parse ipnetwork {0}", ips);
                continue;
            }

            ipnetworks.Add(ipnetwork);
        }

        ac.Networks = ipnetworks.ToArray();
    }

    private static bool TryParseIPNetwork(string ip, CidrParseEnum cidrParseEnum, byte cidr, out IPNetwork2 ipn)
    {
        IPNetwork2 ipnetwork = null;
        switch (cidrParseEnum)
        {
            case CidrParseEnum.Default when !IPNetwork2.TryParse(ip, out ipnetwork):
                ipn = null;
                return false;
            case CidrParseEnum.Value:
            {
                if (!IPNetwork2.TryParse(ip, cidr, out ipnetwork))
                {
                    if (!IPNetwork2.TryParse(ip, out ipnetwork))
                    {
                        ipn = null;
                        return false;
                    }
                }

                break;
            }
        }

        ipn = ipnetwork;
        return true;
    }

    private static bool PrintNoValue(ProgramContext ac)
    {
        ArgumentNullException.ThrowIfNull(ac);

        return ac.IPNetwork == false
               && ac.Network == false
               && ac.Netmask == false
               && ac.Cidr == false
               && ac.Broadcast == false
               && ac.FirstUsable == false
               && ac.LastUsable == false
               && ac.Total == false
               && ac.Usable == false;
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

    private static void Usage()
    {
        Assembly assembly = Assembly.GetEntryAssembly()
                            ?? Assembly.GetExecutingAssembly()
            ;
        var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
        string version = fvi.FileVersion;

        Console.WriteLine(
            "Usage: ipnetwork [-inmcbflu] [-d cidr|-D] [-h|-s cidr|-S|-w|-W|-x|-C network|-o network] networks ...");
        Console.WriteLine("Version: {0}", version);
        Console.WriteLine();
        Console.WriteLine("Print options");
        Console.WriteLine("\t-i : network");
        Console.WriteLine("\t-n : network address");
        Console.WriteLine("\t-m : netmask");
        Console.WriteLine("\t-c : cidr");
        Console.WriteLine("\t-b : broadcast");
        Console.WriteLine("\t-f : first usable ip address");
        Console.WriteLine("\t-l : last usable ip address");
        Console.WriteLine("\t-u : number of usable ip addresses");
        Console.WriteLine("\t-t : total number of ip addresses");
        Console.WriteLine();
        Console.WriteLine("Parse options");
        Console.WriteLine("\t-d cidr : use cidr if not provided (default /32)");
        Console.WriteLine("\t-D      : IPv4 only - use default cidr (ClassA/8, ClassB/16, ClassC/24)");
        Console.WriteLine();
        Console.WriteLine("Actions");
        Console.WriteLine("\t-h         : help message");
        Console.WriteLine("\t-s cidr    : split network into cidr subnets");
        Console.WriteLine("\t-w         : supernet networks into smallest possible subnets");
        Console.WriteLine("\t-W         : supernet networks into one single subnet");
        Console.WriteLine("\t-x         : list all ipaddresses in networks");
        Console.WriteLine("\t-C network : network contain networks");
        Console.WriteLine("\t-o network : network overlap networks");
        Console.WriteLine("\t-S network : subtract network from subnet");
        Console.WriteLine(string.Empty);
        Console.WriteLine("networks  : one or more network addresses ");
        Console.WriteLine(
            "            (1.2.3.4 10.0.0.0/8 10.0.0.0/255.0.0.0 2001:db8::/32 2001:db8:1:2:3:4:5:6/128 )");
    }
}