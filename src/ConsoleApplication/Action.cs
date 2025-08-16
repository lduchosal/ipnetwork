// <copyright file="ActionEnum.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

/// <summary>
/// Switch and actions.
/// </summary>
public enum Action
{
    /// <summary>
    /// Prints usage.
    /// </summary>
    Usage,

    /// <summary>
    /// Print networks.
    /// </summary>
    PrintNetworks,

    /// <summary>
    /// Find a subnet.
    /// </summary>
    Subnet,

    /// <summary>
    /// Find a supernet.
    /// </summary>
    Supernet,

    /// <summary>
    /// Find a wide subnet.
    /// </summary>
    WideSupernet,

    /// <summary>
    /// List IPAddress.
    /// </summary>
    ListIPAddress,

    /// <summary>
    /// Check if network contains.
    /// </summary>
    ContainNetwork,

    /// <summary>
    /// Check if network overlaps.
    /// </summary>
    OverlapNetwork,

    /// <summary>
    /// Substract networks.
    /// </summary>
    SubtractNetwork
}