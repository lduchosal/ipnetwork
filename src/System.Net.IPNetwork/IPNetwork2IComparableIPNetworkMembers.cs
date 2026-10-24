// <copyright file="IPNetwork2IComparableIPNetworkMembers.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

/// <summary>
/// IComparable.
/// </summary>
public sealed partial class IPNetwork2
{
    /// <summary>
    /// Compares two IPNetwork2 instances.
    /// </summary>
    /// <param name="left">The first IPNetwork2 instance to compare.</param>
    /// <param name="right">The second IPNetwork2 instance to compare.</param>
    /// <returns>
    /// A value indicating the relative order of the two IPNetwork2 instances.
    /// Zero if the instances are equal.
    /// A negative value if <paramref name="left"/> is less than <paramref name="right"/>.
    /// A positive value if <paramref name="left"/> is greater than <paramref name="right"/>.
    /// </returns>
    public static int Compare(IPNetwork2? left, IPNetwork2? right)
    {
        // two null IPNetworks are equal
        if (left is null && right is null)
        {
            return 0;
        }

        // two same IPNetworks are equal
        if (ReferenceEquals(left, right))
        {
            return 0;
        }

        // null is always sorted first
        if (left is null)
        {
            return -1;
        }

        if (right is null)
        {
            return 1;
        }

        // one snapshot per side, so that a network rewritten halfway through the comparison is
        // either wholly the old one or wholly the new one
        Snapshot leftState = left.state;
        Snapshot rightState = right.state;

        // first test family
        int result = leftState.Family.CompareTo(rightState.Family);
        if (result != 0)
        {
            return result;
        }

        // second test the network
        result = leftState.Network.CompareTo(rightState.Network);
        if (result != 0)
        {
            return result;
        }

        // then test the cidr
        result = (leftState.Cidr * -1).CompareTo(rightState.Cidr * -1);
        return result;
    }

    /// <summary>
    /// Compare two ipnetworks.
    /// </summary>
    /// <param name="other">The other network to compare to.</param>
    /// <returns>A signed number indicating the relative values of this instance and value.</returns>
    public int CompareTo(IPNetwork2? other)
    {
        return Compare(this, other);
    }

    /// <summary>
    /// Compare two ipnetworks.
    /// </summary>
    /// <param name="obj">The other object to compare to.</param>
    /// <returns>A signed number indicating the relative values of this instance and value.</returns>
    public int CompareTo(object? obj)
    {
        // null is at less
        if (obj == null)
        {
            return 1;
        }

        // convert to a proper Cidr object
        if (obj is not IPNetwork2 other)
        {
            throw new ArgumentException(
                "The supplied parameter is an invalid type. Please supply an IPNetwork type.",
                nameof(obj));
        }

        // perform the comparision
        return this.CompareTo(other);
    }
}