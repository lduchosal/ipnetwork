// <copyright file="IPNetwork2ISerializable.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Runtime.Serialization;

/// <summary>
/// ISerializable.
/// </summary>
public sealed partial class IPNetwork2
{
    private IPNetwork2(SerializationInfo info, StreamingContext context)
    {
        string sipnetwork = info.GetString("IPNetwork") ?? throw new SerializationException("Missing IPNetwork value");
        var ipnetwork = Parse(sipnetwork);

        this.ipaddress = ipnetwork.ipaddress;
        this.cidr = ipnetwork.cidr;
        this.family = ipnetwork.family;
        this.cachedHashCode = new Lazy<int>(this.ComputeHashCode);
    }

    /// <inheritdoc/>
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("IPNetwork", this.ToString());
    }
}