// <copyright file="ActionResult.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Collections.Generic;

/// <summary>
/// Discriminated union for all action outputs.
/// </summary>
public abstract class ActionOutput
{
    private ActionOutput() { }

    /// <summary>
    /// Dispatches to the appropriate Write overload on the formatter.
    /// </summary>
    public abstract void WriteTo(IFormatter formatter, ProgramContext ac);

    /// <summary>
    /// A flat list of networks (PrintNetworks, Supernet, WideSupernet).
    /// </summary>
    public sealed class Networks : ActionOutput
    {
        /// <summary>Gets the networks.</summary>
        public required List<IPNetwork2> Items { get; init; }

        /// <inheritdoc/>
        public override void WriteTo(IFormatter formatter, ProgramContext ac)
            => formatter.Write(this, ac);
    }

    /// <summary>
    /// Grouped lists of networks (Subnet — one group per input network).
    /// </summary>
    public sealed class NetworkGroups : ActionOutput
    {
        /// <summary>Gets the groups.</summary>
        public required List<List<IPNetwork2>> Groups { get; init; }

        /// <summary>Gets the original input networks (for error messages).</summary>
        public required IPNetwork2[] InputNetworks { get; init; }

        /// <summary>Gets the subnet CIDR (for error messages).</summary>
        public required byte SubnetCidr { get; init; }

        /// <inheritdoc/>
        public override void WriteTo(IFormatter formatter, ProgramContext ac)
            => formatter.Write(this, ac);
    }

    /// <summary>
    /// Subtracted networks.
    /// </summary>
    public sealed class SubtractResults : ActionOutput
    {
        /// <summary>Gets the subtracted networks.</summary>
        public required List<IPNetwork2> Items { get; init; }

        /// <inheritdoc/>
        public override void WriteTo(IFormatter formatter, ProgramContext ac)
            => formatter.Write(this, ac);
    }

    /// <summary>
    /// Containment test results.
    /// </summary>
    public sealed class ContainResults : ActionOutput
    {
        /// <summary>Gets the results.</summary>
        public required List<ContainInfo> Items { get; init; }

        /// <inheritdoc/>
        public override void WriteTo(IFormatter formatter, ProgramContext ac)
            => formatter.Write(this, ac);
    }

    /// <summary>
    /// Overlap test results.
    /// </summary>
    public sealed class OverlapResults : ActionOutput
    {
        /// <summary>Gets the results.</summary>
        public required List<OverlapInfo> Items { get; init; }

        /// <inheritdoc/>
        public override void WriteTo(IFormatter formatter, ProgramContext ac)
            => formatter.Write(this, ac);
    }

    /// <summary>
    /// Lazy enumeration of IP addresses.
    /// </summary>
    public sealed class IpAddresses : ActionOutput
    {
        /// <summary>Gets the IP addresses.</summary>
        public required IEnumerable<string> Items { get; init; }

        /// <summary>Gets the input networks (for text warning on large networks).</summary>
        public required IPNetwork2[] InputNetworks { get; init; }

        /// <inheritdoc/>
        public override void WriteTo(IFormatter formatter, ProgramContext ac)
            => formatter.Write(this, ac);
    }

    /// <summary>
    /// An error message.
    /// </summary>
    public sealed class Error : ActionOutput
    {
        /// <summary>Gets the error message.</summary>
        public required string Message { get; init; }

        /// <inheritdoc/>
        public override void WriteTo(IFormatter formatter, ProgramContext ac)
            => formatter.Write(this, ac);
    }

    /// <summary>
    /// Usage / help information.
    /// </summary>
    public sealed class UsageInfo : ActionOutput
    {
        /// <summary>Gets the program version.</summary>
        public required string Version { get; init; }

        /// <summary>Gets the synopsis line.</summary>
        public required string Synopsis { get; init; }

        /// <summary>Gets the option groups.</summary>
        public required List<UsageOptionGroup> OptionGroups { get; init; }

        /// <summary>Gets the positional argument description.</summary>
        public required string NetworksDescription { get; init; }

        /// <summary>Gets the positional argument examples.</summary>
        public required List<string> NetworksExamples { get; init; }

        /// <inheritdoc/>
        public override void WriteTo(IFormatter formatter, ProgramContext ac)
            => formatter.Write(this, ac);
    }
}

/// <summary>
/// A group of options for usage display.
/// </summary>
public sealed class UsageOptionGroup
{
    /// <summary>Gets the group name (e.g. "Print options").</summary>
    public required string Name { get; init; }

    /// <summary>Gets the options in this group.</summary>
    public required List<UsageOption> Options { get; init; }
}

/// <summary>
/// A single option for usage display.
/// </summary>
public sealed class UsageOption
{
    /// <summary>Gets the flag character.</summary>
    public required string Flag { get; init; }

    /// <summary>Gets the optional argument name (e.g. "cidr").</summary>
    public string? ArgName { get; init; }

    /// <summary>Gets the description.</summary>
    public required string Description { get; init; }
}

/// <summary>
/// Result of a network containment test.
/// </summary>
public sealed class ContainInfo
{
    /// <summary>Gets the container network.</summary>
    public required string Network { get; init; }

    /// <summary>Gets the tested network.</summary>
    public required string Test { get; init; }

    /// <summary>Gets a value indicating whether the container contains the test network.</summary>
    public required bool Contains { get; init; }
}

/// <summary>
/// Result of a network overlap test.
/// </summary>
public sealed class OverlapInfo
{
    /// <summary>Gets the reference network.</summary>
    public required string Network { get; init; }

    /// <summary>Gets the tested network.</summary>
    public required string Test { get; init; }

    /// <summary>Gets a value indicating whether the networks overlap.</summary>
    public required bool Overlaps { get; init; }
}
