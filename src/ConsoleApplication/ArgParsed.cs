// <copyright file="ArgParsed.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace IPNetwork2.ConsoleApplication
{
    public class ArgParsed
    {
        public int Arg { get; set; }

        private event ArgParsedDelegate OnArgParsed;

        public delegate void ArgParsedDelegate(ProgramContext ac, string arg);

        public void Run(ProgramContext ac, string arg)
        {
            this.OnArgParsed?.Invoke(ac, arg);
        }

        public ArgParsed(int arg, ArgParsedDelegate onArgParsed)
        {
            this.Arg = arg;
            this.OnArgParsed += onArgParsed;
        }
    }
}
