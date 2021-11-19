namespace System.Net.ConsoleApplication
{
    public class ProgramContext {

        public bool IPNetwork;
        public bool Network;
        public bool Netmask;
        public bool Cidr;
        public bool Broadcast;
        public bool FirstUsable;
        public bool LastUsable;
        public bool Usable;
        public bool Total;

        public CidrParseEnum CidrParse = CidrParseEnum.Value;
        public byte CidrParsed = 32;

        public IPNetwork ContainNetwork;
        public IPNetwork OverlapNetwork;
        public IPNetwork SubstractNetwork;

        public ActionEnum Action = ActionEnum.PrintNetworks;
        public byte SubnetCidr;

        public string[] NetworksString;
        public IPNetwork[] Networks;

    }
}
