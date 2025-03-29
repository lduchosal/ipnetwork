// <copyright file="IPNetwork2ListIPAddress.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

/// <summary>
/// ListIPAddress.
/// </summary>
public sealed partial class IPNetwork2
{
    /// <summary>
    /// List all ip addresses in a subnet.
    /// </summary>
    /// <param name="ipnetwork">The network to list IPAdresses.</param>
    /// <returns>All the IPAdresses contained in ipnetwork.</returns>
    [Obsolete("static ListIPAddress is deprecated, please use instance ListIPAddress.")]
    public static IPAddressCollection ListIPAddress(IPNetwork2 ipnetwork)
    {
        return ipnetwork.ListIPAddress();
    }

    /// <summary>
    /// List all ip addresses in a subnet.
    /// </summary>
    /// <param name="filter">Filter IPAdresses from IPNetwork.</param>
    /// <returns>The filterted IPAdresses contained in ipnetwork.</returns>
    public IPAddressCollection ListIPAddress(FilterEnum filter = FilterEnum.All)
    {
        return new IPAddressCollection(this, filter);
    }
}