// <copyright file="IPNetwork2.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Runtime.Serialization;

/// <summary>
/// IP Network utility class.
/// Use IPNetwork.Parse to create instances.
/// </summary>
[Serializable]
[CLSCompliant(true)]
public sealed partial class IPNetwork2 : IComparable<IPNetwork2>, ISerializable
{
}
