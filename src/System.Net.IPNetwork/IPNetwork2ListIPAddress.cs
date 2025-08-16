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
    /// <param name="filter">Filter IPAdresses from IPNetwork.</param>
    /// <returns>The filterted IPAdresses contained in ipnetwork.</returns>
    public IPAddressCollection ListIPAddress(Filter filter = Filter.All)
    {
        return new IPAddressCollection(this, filter);
    }
}