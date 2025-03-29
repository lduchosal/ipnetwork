// <copyright file="IPNetwork2ToString.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

public sealed partial class IPNetwork2
{
    /// <summary>
    /// Returns a string representation of the object.
    /// </summary>
    /// <returns>
    /// A string representation of the object which includes the Network and Cidr values separated by a "/".
    /// </returns>
    public override string ToString()
    {
        return string.Format("{0}/{1}", this.Network, this.Cidr);
    }
}