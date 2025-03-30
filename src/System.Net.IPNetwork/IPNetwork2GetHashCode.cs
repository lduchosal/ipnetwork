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
        return this.hashCode;
    }

    /// <summary>
    /// 20221105 : ldvhcosal
    /// GetHashCode uses mutable attributes. That introduce undefined behaviour on Hashtable and dictionary.
    /// </summary>
    /// <returns>An number representing the hashCode.</returns>
    private int ComputeHashCode()
    {
        return $"{this.family.GetHashCode()}|{this.InternalNetwork.GetHashCode()}|{this.cidr.GetHashCode()}".GetHashCode();
    }
}
