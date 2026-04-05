// <copyright file="ActionComputer.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Pure computation logic for all CLI actions. Single dispatch point.
/// </summary>
public static class ActionComputer
{
    private const string Synopsis =
        "ipnetwork [-inmcbflu] [-j] [-d cidr|-D] [-h|-s cidr|-S network|-w|-W|-x|-C network|-o network] networks ...";

    private static readonly List<string> NetworksExamples =
        ["1.2.3.4", "10.0.0.0/8", "10.0.0.0/255.0.0.0", "2001:db8::/32", "2001:db8:1:2:3:4:5:6/128"];

    /// <summary>
    /// Compute the result for the given action. This is the only switch on Action in the codebase.
    /// </summary>
    public static ActionOutput Compute(ProgramContext ac, ArgParsed[] argsList)
    {
        return ac.Action switch
        {
            Action.Usage => BuildUsageInfo(ac, argsList),
            Action.PrintNetworks => new ActionOutput.Networks { Items = [.. ac.Networks] },
            Action.Subnet => ComputeSubnet(ac),
            Action.Supernet => ComputeSupernet(ac),
            Action.WideSupernet => ComputeWideSupernet(ac),
            Action.ContainNetwork => ComputeContain(ac),
            Action.OverlapNetwork => ComputeOverlap(ac),
            Action.SubtractNetwork => ComputeSubtract(ac),
            Action.ListIPAddress => new ActionOutput.IpAddresses
            {
                Items = EnumerateIPs(ac),
                InputNetworks = ac.Networks
            },
            _ => new ActionOutput.Error { Message = "Unknown action" }
        };
    }

    private static ActionOutput ComputeSubnet(ProgramContext ac)
    {
        var groups = new List<List<IPNetwork2>>();
        foreach (var ipnetwork in ac.Networks)
        {
            var group = new List<IPNetwork2>();
            if (ipnetwork.TrySubnet(ac.SubnetCidr, out IPNetworkCollection? subnets) && subnets is not null)
            {
                foreach (var s in subnets)
                {
                    group.Add(s);
                }
            }

            groups.Add(group);
        }

        return new ActionOutput.NetworkGroups
        {
            Groups = groups,
            InputNetworks = ac.Networks,
            SubnetCidr = ac.SubnetCidr
        };
    }

    private static ActionOutput ComputeSupernet(ProgramContext ac)
    {
        if (!IPNetwork2.TrySupernet(ac.Networks, out IPNetwork2[]? result))
        {
            return new ActionOutput.Error { Message = "Unable to supernet these networks" };
        }

        return new ActionOutput.Networks { Items = [.. result] };
    }

    private static ActionOutput ComputeWideSupernet(ProgramContext ac)
    {
        if (!IPNetwork2.TryWideSubnet(ac.Networks, out IPNetwork2? result))
        {
            return new ActionOutput.Error { Message = "Unable to wide subnet these networks" };
        }

        return new ActionOutput.Networks { Items = [result] };
    }

    private static ActionOutput ComputeContain(ProgramContext ac)
    {
        if (ac.ContainNetwork is null)
        {
            return new ActionOutput.ContainResults { Items = [] };
        }

        var results = new List<ContainInfo>();
        foreach (var ipnetwork in ac.Networks)
        {
            results.Add(new ContainInfo
            {
                Network = ac.ContainNetwork.ToString(),
                Test = ipnetwork.ToString(),
                Contains = ac.ContainNetwork.Contains(ipnetwork)
            });
        }

        return new ActionOutput.ContainResults { Items = results };
    }

    private static ActionOutput ComputeOverlap(ProgramContext ac)
    {
        if (ac.OverlapNetwork is null)
        {
            return new ActionOutput.OverlapResults { Items = [] };
        }

        var results = new List<OverlapInfo>();
        foreach (var ipnetwork in ac.Networks)
        {
            results.Add(new OverlapInfo
            {
                Network = ac.OverlapNetwork.ToString(),
                Test = ipnetwork.ToString(),
                Overlaps = ac.OverlapNetwork.Overlap(ipnetwork)
            });
        }

        return new ActionOutput.OverlapResults { Items = results };
    }

    private static ActionOutput ComputeSubtract(ProgramContext ac)
    {
        if (ac.SubtractNetwork is null)
        {
            return new ActionOutput.SubtractResults { Items = [] };
        }

        var results = new List<IPNetwork2>();
        foreach (var ipnetwork in ac.Networks)
        {
            foreach (var subtracted in ipnetwork.Subtract(ac.SubtractNetwork))
            {
                results.Add(subtracted);
            }
        }

        return new ActionOutput.SubtractResults { Items = results };
    }

    private static IEnumerable<string> EnumerateIPs(ProgramContext ac)
    {
        foreach (var ipnetwork in ac.Networks)
        {
            foreach (var ip in ipnetwork.ListIPAddress())
            {
                yield return ip.ToString();
            }
        }
    }

    private static ActionOutput BuildUsageInfo(ProgramContext ac, ArgParsed[] argsList)
    {
        var groupOrder = new List<string>();
        var groups = new Dictionary<string, List<UsageOption>>();

        foreach (var arg in argsList)
        {
            if (arg.Group is null || arg.Description is null)
            {
                continue;
            }

            if (!groups.TryGetValue(arg.Group, out var options))
            {
                options = [];
                groups[arg.Group] = options;
                groupOrder.Add(arg.Group);
            }

            options.Add(new UsageOption
            {
                Flag = ((char)arg.Arg).ToString(),
                ArgName = arg.ArgName,
                Description = arg.Description,
                Example = arg.Example
            });
        }

        string version = typeof(Program).Assembly.GetName().Version?.ToString() ?? "unknown";

        return new ActionOutput.UsageInfo
        {
            Errors = ac.ParseErrors,
            Version = version,
            Synopsis = Synopsis,
            OptionGroups = groupOrder.Select(name => new UsageOptionGroup
            {
                Name = name,
                Options = groups[name]
            }).ToList(),
            NetworksDescription = "one or more network addresses",
            NetworksExamples = NetworksExamples
        };
    }
}
