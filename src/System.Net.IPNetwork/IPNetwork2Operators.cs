// <copyright file="IPNetwork2Operators.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

public sealed partial class IPNetwork2
{
    /// <summary>
    /// Compares two IPNetwork.
    /// </summary>
    /// <param name="left">left instance.</param>
    /// <param name="right">Right instance.</param>
    /// <returns>true if left equals right; otherwise, false.</returns>
    public static bool operator ==(IPNetwork2 left, IPNetwork2 right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Compares two IPNetwork.
    /// </summary>
    /// <param name="left">left instance.</param>
    /// <param name="right">Right instance.</param>
    /// <returns>true if left does not equals right; otherwise, false.</returns>
    public static bool operator !=(IPNetwork2 left, IPNetwork2 right)
    {
        return !Equals(left, right);
    }

    /// <summary>
    /// Compares two IPNetwork.
    /// </summary>
    /// <param name="left">left instance.</param>
    /// <param name="right">Right instance.</param>
    /// <returns>true if left is less than right; otherwise, false.</returns>
    public static bool operator <(IPNetwork2 left, IPNetwork2 right)
    {
        return Compare(left, right) < 0;
    }

    /// <summary>
    /// Compares two IPNetwork.
    /// </summary>
    /// <param name="left">left instance.</param>
    /// <param name="right">Right instance.</param>
    /// <returns>true if left is greater than right; otherwise, false.</returns>
    public static bool operator >(IPNetwork2 left, IPNetwork2 right)
    {
        return Compare(left, right) > 0;
    }
}