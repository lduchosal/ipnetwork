// <copyright file="ProgramContext.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

/// <summary>
/// The execution context.
/// </summary>
public class ProgramContext
{
    /// <summary>
    /// Get or set the IPNetwork.
    /// </summary>
    public bool IPNetwork { get; set; }

    /// <summary>
    /// Get or set the Network.
    /// </summary>
    public bool Network { get; set; }

    /// <summary>
    /// Get or set the Netmask.
    /// </summary>
    public bool Netmask { get; set; }

    /// <summary>
    /// Get or set the Cidr.
    /// </summary>
    public bool Cidr { get; set; }

    /// <summary>
    /// Get or set the Broadcast.
    /// </summary>
    public bool Broadcast { get; set; }

    /// <summary>
    /// Get or set the FirstUsable.
    /// </summary>
    public bool FirstUsable { get; set; }

    /// <summary>
    /// Get or set the LastUsable.
    /// </summary>
    public bool LastUsable { get; set; }

    /// <summary>
    /// Get or set the Usable.
    /// </summary>
    public bool Usable { get; set; }

    /// <summary>
    /// Get or set the Total.
    /// </summary>
    public bool Total { get; set; }

    /// <summary>
    /// Get or set the CidrParse.
    /// </summary>
    public CidrParseEnum CidrParse { get; set; } = CidrParseEnum.Value;

    /// <summary>
    /// Get or set the CidrParsed.
    /// </summary>
    public byte CidrParsed { get; set; } = 32;

    /// <summary>
    /// Get or set the ContainNetwork.
    /// </summary>
    public IPNetwork2 ContainNetwork { get; set; }

    /// <summary>
    /// Get or set the OverlapNetwork.
    /// </summary>
    public IPNetwork2 OverlapNetwork { get; set; }

    /// <summary>
    /// Get or set the SubtractNetwork.
    /// </summary>
    public IPNetwork2 SubtractNetwork { get; set; }

    /// <summary>
    /// Get or set the Action.
    /// </summary>
    public ActionEnum Action { get; set; } = ActionEnum.PrintNetworks;

    /// <summary>
    /// Get or set the SubnetCidr.
    /// </summary>
    public byte SubnetCidr { get; set; }

    /// <summary>
    /// Get or set the NetworksString.
    /// </summary>
    public string[] NetworksString { get; set; }

    /// <summary>
    /// Get or set the Networks.
    /// </summary>
    public IPNetwork2[] Networks { get; set; }
}