// <copyright file="ICidrGuess.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

/// <summary>
/// An interface to have multiple implemntatino ao CIDR Guesser.
/// </summary>
public interface ICidrGuess
{
    /// <summary>
    /// Try to guess the CIDR.
    /// </summary>
    /// <param name="ip">An ip adress to guess the ip network CIDR.</param>
    /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
    /// <returns>true if ip was converted successfully; otherwise, false.</returns>
    bool TryGuessCidr(string ip, out byte cidr);
}