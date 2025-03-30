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
    /// Gets or sets position.
    /// </summary>
    public int Arg { get; set; }

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
}