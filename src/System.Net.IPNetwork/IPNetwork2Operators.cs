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
    /// Behavior
    ///  The addition operator (+) performs the following operations:
    ///  Network Expansion: Adds the specified number of IP addresses to the network range
    ///  Optimal Grouping: Attempts to create the most efficient network representation
    ///  Multiple Networks: When a single contiguous network cannot represent the result, returns multiple networks
    ///  CIDR Optimization: Automatically calculates the appropriate subnet mask for the expanded range
    /// </summary>
    /// <param name="left">left instance.</param>
    /// <param name="add">number.</param>
    /// <returns>Adds the specified number of IP addresses to the network range.</returns>
    public static IEnumerable<IPNetwork2> operator +(IPNetwork2 left, int add)
    {
        var start = ToBigInteger(left.First);
        var last = ToBigInteger(left.Last);
        var end = last+ add;

        if (end < start)
        {
            return [];
        }

        var startIp = ToIPAddress(start, left.AddressFamily);
        var endIp = ToIPAddress(end, left.AddressFamily);
        
        InternalParseRange(false, startIp, endIp, out IEnumerable<IPNetwork2> networks);
        return networks;
    }
    
    /// <summary>
    /// Add IPNetwork.
    /// </summary>
    /// <param name="left">left instance.</param>
    /// <param name="subtract">number.</param>
    /// <returns>Try to supernet two consecutive cidr equal subnet into a single one, otherwise return both netowkrs.</returns>
    public static IEnumerable<IPNetwork2> operator -(IPNetwork2 left, int subtract)
    {
        return left + (subtract * -1);
    }
}