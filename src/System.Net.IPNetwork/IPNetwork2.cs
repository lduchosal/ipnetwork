// <copyright file="IPNetwork2.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using Runtime.Serialization;

/// <summary>
/// IP Network utility class.
/// Use IPNetwork.Parse to create instances.
/// </summary>
[Serializable]
[CLSCompliant(true)]
public sealed partial class IPNetwork2 : IComparable<IPNetwork2>, ISerializable
{
    /// <summary>
    /// Returns a string representation of the object.
    /// </summary>
    /// <returns>
    /// A string representation of the object which includes the Network and Cidr values separated by a "/".
    /// </returns>
    public override string ToString()
    {
        return $"{this.Network}/{this.Cidr}";
    }
}
