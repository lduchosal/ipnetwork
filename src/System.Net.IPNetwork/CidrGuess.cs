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
    /// Gets classFull guess.
    /// </summary>
    public static ICidrGuess ClassFull { get => CidrClassfull.Value; }

    /// <summary>
    /// Gets classLess guess.
    /// </summary>
    public static ICidrGuess ClassLess { get => CidrClassless.Value; }

    private static readonly Lazy<ICidrGuess> CidrClassless = new (() => new CidrClassLess());
    private static readonly Lazy<ICidrGuess> CidrClassfull = new (() => new CidrClassFull());
}