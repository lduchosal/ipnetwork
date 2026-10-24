// <copyright file="IPNetwork2.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Runtime.Serialization;

/// <summary>
/// IP Network utility class.
/// Use IPNetwork.Parse to create instances.
/// </summary>
[Serializable]
[CLSCompliant(true)]
public sealed partial class IPNetwork2 : ISerializable
{
    /// <summary>
    /// Returns a string representation of the object.
    /// </summary>
    /// <returns>
    /// A string representation of the object which includes the Network and Cidr values separated by a "/".
    /// </returns>
    public override string ToString()
    {
        // One snapshot for both halves: printing the network of one assignment next to the CIDR of
        // another would describe a network that was never assigned.
        Snapshot snapshot = this.state;
        return $"{ToIPAddress(snapshot.Network, snapshot.Family)}/{snapshot.Cidr}";
    }
}
