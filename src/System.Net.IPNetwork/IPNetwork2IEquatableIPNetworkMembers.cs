// <copyright file="IPNetwork2IEquatableIPNetworkMembers.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

/// <summary>
/// IEquatable.
/// </summary>
public sealed partial class IPNetwork2 : IComparable<IPNetwork2>, IEquatable<IPNetwork2>
{
    /// <summary>
    /// Compare two ipnetworks.
    /// </summary>
    /// <param name="left">An IPNetwork to compare.</param>
    /// <param name="right">An other IPNetwork to compare to.</param>
    /// <returns>true if obj has the same value as this instance; otherwise, false.</returns>
    public static bool Equals(IPNetwork2 left, IPNetwork2 right)
    {
        return Compare(left, right) == 0;
    }

    /// <summary>
    /// Compare two ipnetworks.
    /// </summary>
    /// <param name="other">An IPNetwork to compare to this instance.</param>
    /// <returns>true if obj has the same value as this instance; otherwise, false.</returns>
    public bool Equals(IPNetwork2 other)
    {
        return Equals(this, other);
    }

    /// <summary>
    /// Compare two ipnetworks.
    /// </summary>
    /// <param name="obj">An object value to compare to this instance.</param>
    /// <returns>true if obj has the same value as this instance; otherwise, false.</returns>
    public override bool Equals(object obj)
    {
        return Equals(this, obj as IPNetwork2);
    }
}