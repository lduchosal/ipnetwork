// <copyright file="Polyfills.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

#if NETSTANDARD2_0
namespace System.Diagnostics.CodeAnalysis;

[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
internal sealed class NotNullWhenAttribute : Attribute
{
    public NotNullWhenAttribute(bool returnValue) => ReturnValue = returnValue;

    public bool ReturnValue { get; }
}
#endif
