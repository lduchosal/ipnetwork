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
    public static int Compare(IPNetwork2 left, IPNetwork2 right)
    {
        // two null IPNetworks are equal
        if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
        {
            return 0;
        }

        // two same IPNetworks are equal
        if (ReferenceEquals(left, right))
        {
            return 0;
        }

        // null is always sorted first
        if (ReferenceEquals(left, null))
        {
            return -1;
        }

        if (ReferenceEquals(right, null))
        {
            return 1;
        }

        // first test family
        int result = left.family.CompareTo(right.family);
        if (result != 0)
        {
            return result;
        }

        // second test the network
        result = left.InternalNetwork.CompareTo(right.InternalNetwork);
        if (result != 0)
        {
            return result;
        }

        // then test the cidr
        result = left.cidr.CompareTo(right.cidr);
        return result;
    }

    /// <summary>
    /// Compare two ipnetworks.
    /// </summary>
    /// <param name="other">The other network to compare to.</param>
    /// <returns>A signed number indicating the relative values of this instance and value..</returns>
    public int CompareTo(IPNetwork2 other)
    {
        return Compare(this, other);
    }

    /// <summary>
    /// Compare two ipnetworks.
    /// </summary>
    /// <param name="obj">The other object to compare to.</param>
    /// <returns>A signed number indicating the relative values of this instance and value..</returns>
    public int CompareTo(object obj)
    {
        // null is at less
        if (obj == null)
        {
            return 1;
        }

        // convert to a proper Cidr object
        var other = obj as IPNetwork2;

        // type problem if null
        if (other == null)
        {
            throw new ArgumentException(
                "The supplied parameter is an invalid type. Please supply an IPNetwork type.",
                nameof(obj));
        }

        // perform the comparision
        return this.CompareTo(other);
    }
}