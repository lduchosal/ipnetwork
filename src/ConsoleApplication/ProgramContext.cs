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
    /// Gets or sets a value indicating whether gets or sets the  IPNetwork.
    /// </summary>
    public bool IPNetwork { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets the Network.
    /// </summary>
    public bool Network { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets the Netmask.
    /// </summary>
    public bool Netmask { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets the Cidr.
    /// </summary>
    public bool Cidr { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets the Broadcast.
    /// </summary>
    public bool Broadcast { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets the FirstUsable.
    /// </summary>
    public bool FirstUsable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets the LastUsable.
    /// </summary>
    public bool LastUsable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets the Usable.
    /// </summary>
    public bool Usable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets the Total.
    /// </summary>
    public bool Total { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets the CidrParse.
    /// </summary>
    public CidrParseEnum CidrParse { get; set; } = CidrParseEnum.Value;

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets the CidrParsed.
    /// </summary>
    public byte CidrParsed { get; set; } = 32;

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets the ContainNetwork.
    /// </summary>
    public IPNetwork2 ContainNetwork { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets the OverlapNetwork.
    /// </summary>
    public IPNetwork2 OverlapNetwork { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets the SubtractNetwork.
    /// </summary>
    public IPNetwork2 SubtractNetwork { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets the Action.
    /// </summary>
    public ActionEnum Action { get; set; } = ActionEnum.PrintNetworks;

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets the SubnetCidr.
    /// </summary>
    public byte SubnetCidr { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets the NetworksString.
    /// </summary>
    public string[] NetworksString { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets the Networks.
    /// </summary>
    public IPNetwork2[] Networks { get; set; }
}