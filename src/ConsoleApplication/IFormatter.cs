// <copyright file="IFormatter.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

/// <summary>
/// Formats action output. Implement this interface for each output format (text, JSON, XML, YAML, etc.).
/// </summary>
public interface IFormatter
{
    /// <summary>
    /// Writes the action output. Entry point for formatting.
    /// </summary>
    void Write(ActionOutput output, ProgramContext ac);

    /// <summary>Writes a flat list of networks.</summary>
    void Write(ActionOutput.Networks output, ProgramContext ac);

    /// <summary>Writes grouped lists of networks (subnet results).</summary>
    void Write(ActionOutput.NetworkGroups output, ProgramContext ac);

    /// <summary>Writes subtraction results.</summary>
    void Write(ActionOutput.SubtractResults output, ProgramContext ac);

    /// <summary>Writes containment test results.</summary>
    void Write(ActionOutput.ContainResults output, ProgramContext ac);

    /// <summary>Writes overlap test results.</summary>
    void Write(ActionOutput.OverlapResults output, ProgramContext ac);

    /// <summary>Writes IP address list.</summary>
    void Write(ActionOutput.IpAddresses output, ProgramContext ac);

    /// <summary>Writes an error.</summary>
    void Write(ActionOutput.Error output, ProgramContext ac);

    /// <summary>Writes usage / help information.</summary>
    void Write(ActionOutput.UsageInfo output, ProgramContext ac);
}
