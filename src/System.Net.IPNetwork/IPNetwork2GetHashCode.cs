// <copyright file="IPNetwork2GetHashCode.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

/// <summary>
/// GetHashcode.
/// </summary>
public sealed partial class IPNetwork2
{
    /// <inheritdoc />
    public override int GetHashCode()
    {
        return this.cachedHashCode.Value;
    }

    private int ComputeHashCode()
    {
        int hash = 17;
        hash = (hash * 31) + this.family.GetHashCode();
        hash = (hash * 31) + this.InternalNetwork.GetHashCode();
        hash = (hash * 31) + this.cidr.GetHashCode();
        return hash;
    }
}
