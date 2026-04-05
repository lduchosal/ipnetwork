// <copyright file="ArgParsed.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

/// <summary>
/// The args from the command line.
/// </summary>
public class ArgParsed
{
    /// <summary>
    /// Gets the option character.
    /// </summary>
    public int Arg { get; }

    /// <summary>
    /// Gets the option group for usage display (e.g. "Print options").
    /// </summary>
    public string? Group { get; }

    /// <summary>
    /// Gets the argument name for usage display (e.g. "cidr", "network").
    /// </summary>
    public string? ArgName { get; }

    /// <summary>
    /// Gets the description for usage display.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Gets the usage example (e.g. "ipnetwork -s 24 10.0.0.0/8").
    /// </summary>
    public string? Example { get; }

    private event ArgParsedDelegate OnArgParsed;

    /// <summary>
    /// An arg has been parsed.
    /// </summary>
    /// <param name="ac">The context.</param>
    /// <param name="arg">The arg.</param>
    public delegate void ArgParsedDelegate(ProgramContext ac, string arg);

    /// <summary>
    /// Run the program with the args.
    /// </summary>
    /// <param name="ac">The context.</param>
    /// <param name="arg">The arg.</param>
    public void Run(ProgramContext ac, string arg)
    {
        this.OnArgParsed?.Invoke(ac, arg);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArgParsed"/> class.
    /// </summary>
    /// <param name="arg">The arg.</param>
    /// <param name="onArgParsed">The event on parse.</param>
    public ArgParsed(int arg, ArgParsedDelegate onArgParsed)
    {
        this.Arg = arg;
        this.OnArgParsed += onArgParsed;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArgParsed"/> class with usage metadata.
    /// </summary>
    /// <param name="arg">The option character.</param>
    /// <param name="group">The option group name.</param>
    /// <param name="description">The option description.</param>
    /// <param name="onArgParsed">The event on parse.</param>
    /// <param name="argName">The optional argument name (e.g. "cidr").</param>
    /// <param name="example">An optional usage example.</param>
    public ArgParsed(int arg, string group, string description, ArgParsedDelegate onArgParsed, string? argName = null, string? example = null)
    {
        this.Arg = arg;
        this.Group = group;
        this.ArgName = argName;
        this.Description = description;
        this.Example = example;
        this.OnArgParsed += onArgParsed;
    }
}
