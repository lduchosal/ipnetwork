
// <copyright file="IPNetwork2Extensions.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>
namespace System.Net;

/// <summary>
/// Extension methods for IPNetwork2 to support ULA operations.
/// </summary>
public static class IPNetwork2UlaExtension
{
    /// <summary>
    /// Determines if this network is a Unique Local Address (ULA).
    /// </summary>
    /// <param name="network">The network to check.</param>
    /// <returns>True if the network is a ULA, false otherwise.</returns>
    public static bool IsUla(this IPNetwork2 network)
    {
        return UniqueLocalAddress.IsUlaPrefix(network);
    }

    /// <summary>
    /// Determines if this network is a locally assigned ULA (fd00::/8).
    /// </summary>
    /// <param name="network">The network to check.</param>
    /// <returns>True if the network is locally assigned ULA, false otherwise.</returns>
    public static bool IsLocallyAssignedUla(this IPNetwork2 network)
    {
        return UniqueLocalAddress.IsLocallyAssignedUla(network);
    }

}