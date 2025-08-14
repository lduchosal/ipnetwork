// <copyright file="CidrGuess.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

/// <summary>
/// A static helper CidrGuess class.
/// </summary>
public static class CidrGuess
{
    /// <summary>
    /// Gets classFull guesser.
    /// </summary>
    public static ICidrGuess ClassFull { get => CidrClassfull.Value; }

    /// <summary>
    /// Gets classLess guesser.
    /// </summary>
    public static ICidrGuess ClassLess { get => CidrClassless.Value; }

    
    /// <summary>
    /// Gets a NetworkAware guesser.
    /// </summary>
    public static ICidrGuess NetworkAware { get => CidrNetworkAware.Value; }

    private static readonly Lazy<ICidrGuess> CidrClassless = new (() => new CidrClassLess());
    private static readonly Lazy<ICidrGuess> CidrClassfull = new (() => new CidrClassFull());
    private static readonly Lazy<ICidrGuess> CidrNetworkAware = new (() => new CidrNetworkAware());
}