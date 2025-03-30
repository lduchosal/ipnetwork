// <copyright file="IPNetwork2ISerializable.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using Sockets;
using Numerics;
using Runtime.Serialization;

/// <summary>
/// ISerializable.
/// </summary>
public sealed partial class IPNetwork2
{
    private IPNetwork2(SerializationInfo info, StreamingContext context)
    {
        string sipnetwork = (string)info.GetValue("IPNetwork", typeof(string));
        var ipnetwork = Parse(sipnetwork);

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