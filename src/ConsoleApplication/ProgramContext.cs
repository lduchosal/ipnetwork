// <copyright file="ProgramContext.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using IPNetwork2;

namespace IPNetwork2.ConsoleApplication
{
    public class ProgramContext
    {
        public bool IPNetwork { get; set; }

        public bool Network { get; set; }

        public bool Netmask { get; set; }

        public bool Cidr { get; set; }

        public bool Broadcast { get; set; }

        public bool FirstUsable { get; set; }

        public bool LastUsable { get; set; }

        public bool Usable { get; set; }

        public bool Total { get; set; }

        public CidrParseEnum CidrParse { get; set; } = CidrParseEnum.Value;

        public byte CidrParsed { get; set; } = 32;

        public IPNetwork ContainNetwork { get; set; }

        public IPNetwork OverlapNetwork { get; set; }

        public IPNetwork SubtractNetwork { get; set; }

        public ActionEnum Action { get; set; } = ActionEnum.PrintNetworks;

        public byte SubnetCidr { get; set; }

        public string[] NetworksString { get; set; }

        public IPNetwork[] Networks { get; set; }
    }
}
