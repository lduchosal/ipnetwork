// <copyright file="IPNetwork2GetHashCode.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// GetHashcode.
/// </summary>
public sealed partial class IPNetwork2
{
    /// <inheritdoc />
    /// <remarks>
    /// The hash is memoized on the state snapshot, not on the instance, so rewriting
    /// <see cref="Value"/> drops the memoized value along with the network it described. Two equal
    /// networks therefore keep hashing equally, whether or not either of them has been mutated.
    /// </remarks>
    [SuppressMessage(
        "Minor Bug",
        "S2328:\"GetHashCode\" should not reference mutable fields",
        Justification = "Deliberate since 5.0.0: the hash must follow Value to honour the Equals/GetHashCode contract.")]
    public override int GetHashCode()
    {
        return this.state.HashCode;
    }
}
