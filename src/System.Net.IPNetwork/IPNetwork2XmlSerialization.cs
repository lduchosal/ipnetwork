// <copyright file="IPNetwork2XmlSerialization.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Net.Sockets;

/// <summary>
/// Serialization.
/// </summary>
public sealed partial class IPNetwork2
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IPNetwork2"/> class.
    /// Created for DataContractSerialization. Better use static methods IPNetwork.Parse() to create IPNetworks.
    /// </summary>
    public IPNetwork2()
        : this(0, AddressFamily.InterNetwork, 0)
    {
    }
}