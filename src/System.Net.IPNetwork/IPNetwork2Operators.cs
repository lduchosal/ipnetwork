// <copyright file="IPNetwork2Operators.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace System.Net;

/// <summary>
/// Opertors.
/// </summary>
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
    /// <returns>true if left is less than right; otherwise, false.</returns>
    public static bool operator <=(IPNetwork2 left, IPNetwork2 right)
    {
        return Compare(left, right) <= 0;
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
    
    /// <summary>
    /// Compares two IPNetwork.
    /// </summary>
    /// <param name="left">left instance.</param>
    /// <param name="right">Right instance.</param>
    /// <returns>true if left is greater than right; otherwise, false.</returns>
    public static bool operator >=(IPNetwork2 left, IPNetwork2 right)
    {
        return Compare(left, right) >= 0;
    }
    
    /// <summary>
    /// Subtract two IPNetwork.
    /// </summary>
    /// <param name="left">left instance.</param>
    /// <param name="right">Right instance.</param>
    /// <returns>The symmetric difference (subtraction) of two networks.</returns>
    public static List<IPNetwork2> operator -(IPNetwork2 left, IPNetwork2 right)
    {
        return left.Subtract(right);
    }
    
    /// <summary>
    /// Add two IPNetwork.
    /// </summary>
    /// <param name="left">left instance.</param>
    /// <param name="right">Right instance.</param>
    /// <returns>Try to supernet two consecutive cidr equal subnet into a single one, otherwise return both netowkrs.</returns>
    public static List<IPNetwork2> operator +(IPNetwork2 left, IPNetwork2 right)
    {
        if (left.TrySupernet(right, out var result))
        {
            return [result];
        }
        return [left, right];
    }
    
    /// <summary>
    /// Add IPNetwork.
    /// </summary>
    /// <param name="left">left instance.</param>
    /// <param name="add">number.</param>
    /// <returns>Try to supernet two consecutive cidr equal subnet into a single one, otherwise return both netowkrs.</returns>
    public static IEnumerable<IPNetwork2> operator +(IPNetwork2 left, int add)
    {
        var uintFirstLeft = ToBigInteger(left.First);
        var uintLastLeft = ToBigInteger(left.Last);
        var uintRight = uintLastLeft+add;

        var start = uintFirstLeft > uintRight ? uintRight : uintFirstLeft;
        var end = uintFirstLeft > uintRight ? uintFirstLeft : uintRight;

        var startIp = ToIPAddress(start, left.AddressFamily);
        var endIp = ToIPAddress(end, left.AddressFamily);
        
        InternalParseRange(false, startIp, endIp, out IEnumerable<IPNetwork2> networks);
        return networks;
    }
}