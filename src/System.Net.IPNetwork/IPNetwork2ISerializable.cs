// <copyright file="IPNetwork2ISerializable.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Net.Sockets;
using System.Numerics;
using System.Runtime.Serialization;

public sealed partial class IPNetwork2
{
    /// <summary>
    /// Represents an internal structure to hold an IP address, its CIDR value, and address family.
    /// Used for internal operations within the IPNetwork2 class.
    /// </summary>
    internal struct IPNetworkInteral
    {
        /// <summary>
        /// Represents the IP address value.
        /// </summary>
        public BigInteger IPAddress;

        /// <summary>
        /// Represents the CIDR (Classless Inter-Domain Routing) value.
        /// </summary>
        public byte Cidr;

        /// <summary>
        /// Represents the address family (IPv4 or IPv6).
        /// </summary>
        public AddressFamily AddressFamily;
    }

    private IPNetwork2(SerializationInfo info, StreamingContext context)
    {
        string sipnetwork = (string)info.GetValue("IPNetwork", typeof(string));
        var ipnetwork = IPNetwork2.Parse(sipnetwork);

        this.ipaddress = ipnetwork.ipaddress;
        this.cidr = ipnetwork.cidr;
        this.family = ipnetwork.family;
    }

    /// <inheritdoc/>
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("IPNetwork", this.ToString());
    }
}