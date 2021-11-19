using Gnu.Getopt;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Reflection;

namespace System.Net.ConsoleApplication
{
    public class Program {

        public static void Main(string[] args) {

            ProgramContext ac = Program.ParseArgs(args);

            if (ac.Action == ActionEnum.Subnet) {
                Program.SubnetNetworks(ac);
            } else if (ac.Action == ActionEnum.Supernet) {
                Program.SupernetNetworks(ac);
            } else if (ac.Action == ActionEnum.WideSupernet) {
                Program.WideSupernetNetworks(ac);
            } else if (ac.Action == ActionEnum.PrintNetworks) {
                Program.PrintNetworks(ac);
            } else if (ac.Action == ActionEnum.ContainNetwork) {
                Program.ContainNetwork(ac);
            } else if (ac.Action == ActionEnum.OverlapNetwork) {
                Program.OverlapNetwork(ac);
            } else if (ac.Action == ActionEnum.ListIPAddress) {
                Program.ListIPAddress(ac);
            /**
             * Need a better way to do it
             * 
            } else if (ac.Action == ActionEnum.SubstractNetwork) {
                Program.SubstractNetwork(ac);
                 * 
            */
            } else {
                Program.Usage();
            }

        }

        private static void ListIPAddress(ProgramContext ac) {
            foreach (IPNetwork ipnetwork in ac.Networks) {
                foreach (IPAddress ipaddress in ipnetwork.ListIPAddress()) {
                    Console.WriteLine("{0}", ipaddress.ToString());
                }
            }
        }

        private static void ContainNetwork(ProgramContext ac) {
            foreach (IPNetwork ipnetwork in ac.Networks) {
                bool contain = ac.ContainNetwork.Contains(ipnetwork);
                Console.WriteLine("{0} contains {1} : {2}", ac.ContainNetwork, ipnetwork, contain);
            }            
        }

        private static void OverlapNetwork(ProgramContext ac) {
            foreach (IPNetwork ipnetwork in ac.Networks) {
                bool overlap = ac.OverlapNetwork.Overlap(ipnetwork);
                Console.WriteLine("{0} overlaps {1} : {2}", ac.OverlapNetwork, ipnetwork, overlap);
            }
        }
        /**
         * Need a better way to do it
         * 
        private static void SubstractNetwork(ProgramContext ac) {
            
            IEnumerable<IPNetwork> result = null;
            if (!IPNetwork.TrySubstractNetwork(ac.Networks, ac.SubstractNetwork, out result)) {
                Console.WriteLine("Unable to substract subnet from these networks");
            }
            
            foreach (IPNetwork ipnetwork in result.OrderBy( s => s.ToString() )) {
                Console.WriteLine("{0}", ipnetwork);
                //Program.PrintNetwork(ac, ipnetwork);
            }
        }
        **/

        private static void WideSupernetNetworks(ProgramContext ac) {

            IPNetwork widesubnet = null;
            if (!IPNetwork.TryWideSubnet(ac.Networks, out widesubnet)) {
                Console.WriteLine("Unable to wide subnet these networks");
            }
            Program.PrintNetwork(ac, widesubnet);

        }

        private static void SupernetNetworks(ProgramContext ac) {

            IPNetwork[] supernet = null;
            if (!IPNetwork.TrySupernet(ac.Networks, out supernet)) {
                Console.WriteLine("Unable to supernet these networks");
            }
            Program.PrintNetworks(ac, supernet, supernet.Length);

        }

        private static void PrintNetworks(ProgramContext ac, IEnumerable<IPNetwork> ipnetworks, BigInteger networkLength) {
            int i = 0;
            foreach (IPNetwork ipn in ipnetworks) {
                i++;
                Program.PrintNetwork(ac, ipn);
                Program.PrintSeparator(networkLength, i);
            }
        }

        private static void SubnetNetworks(ProgramContext ac) {
            BigInteger i = 0;
            foreach (IPNetwork ipnetwork in ac.Networks) {
                i++;
                int networkLength = ac.Networks.Length;
                IPNetworkCollection ipnetworks = null;
                if (!ipnetwork.TrySubnet(ac.SubnetCidr, out ipnetworks)) {
                    Console.WriteLine("Unable to subnet ipnetwork {0} into cidr {1}", ipnetwork, ac.SubnetCidr);
                    Program.PrintSeparator(networkLength, i);
                    continue;
                }

                Program.PrintNetworks(ac, ipnetworks, ipnetworks.Count);
                Program.PrintSeparator(networkLength, i);
            }
        }

        //private static void PrintSeparator(Array network, int index) {
        //    if (network.Length > 1 && index != network.Length) {
        //        Console.WriteLine("--");
        //    }
        //}
        private static void PrintSeparator(BigInteger max, BigInteger index) {
            if (max > 1 && index != max) {
                Console.WriteLine("--");
            }
        }

        private static void PrintNetworks(ProgramContext ac) {
            int i = 0;
            foreach (IPNetwork ipnetwork in ac.Networks) {
                i++;
                Program.PrintNetwork(ac, ipnetwork);
                Program.PrintSeparator(ac.Networks.Length, i);
            }
        }

        private static void PrintNetwork(ProgramContext ac, IPNetwork ipn) {

            using (var sw = new StringWriter())
            {
                if (ac.IPNetwork)   sw.WriteLine("IPNetwork   : {0}", ipn.ToString());
                if (ac.Network)     sw.WriteLine("Network     : {0}", ipn.Network.ToString());
                if (ac.Netmask)     sw.WriteLine("Netmask     : {0}", ipn.Netmask.ToString());
                if (ac.Cidr)        sw.WriteLine("Cidr        : {0}", ipn.Cidr);
                if (ac.Broadcast)   sw.WriteLine("Broadcast   : {0}", ipn.Broadcast.ToString());
                if (ac.FirstUsable) sw.WriteLine("FirstUsable : {0}", ipn.FirstUsable.ToString());
                if (ac.LastUsable)  sw.WriteLine("LastUsable  : {0}", ipn.LastUsable.ToString());
                if (ac.Usable)      sw.WriteLine("Usable      : {0}", ipn.Usable);
                if (ac.Total)       sw.WriteLine("Total       : {0}", ipn.Total);

                Console.Write(sw.ToString());
            }
        }

        private static Dictionary<int, ArgParsed> Args = new Dictionary<int, ArgParsed>();
        private static ArgParsed[] ArgsList = new [] {
            new ArgParsed('i', delegate(ProgramContext ac, string arg) { ac.IPNetwork = true; } ),
            new ArgParsed('n', delegate(ProgramContext ac, string arg) { ac.Network = true; } ),
            new ArgParsed('m', delegate(ProgramContext ac, string arg) { ac.Netmask = true; } ),
            new ArgParsed('c', delegate(ProgramContext ac, string arg) { ac.Cidr = true; } ),
            new ArgParsed('b', delegate(ProgramContext ac, string arg) { ac.Broadcast = true; } ),
            new ArgParsed('f', delegate(ProgramContext ac, string arg) { ac.FirstUsable = true; } ),
            new ArgParsed('l', delegate(ProgramContext ac, string arg) { ac.LastUsable = true; } ),
            new ArgParsed('u', delegate(ProgramContext ac, string arg) { ac.Usable = true; } ),
            new ArgParsed('t', delegate(ProgramContext ac, string arg) { ac.Total = true; } ),
            new ArgParsed('w', delegate(ProgramContext ac, string arg) { ac.Action = ActionEnum.Supernet; } ),
            new ArgParsed('W', delegate(ProgramContext ac, string arg) { ac.Action = ActionEnum.WideSupernet; } ),
            new ArgParsed('h', delegate(ProgramContext ac, string arg) { ac.Action = ActionEnum.Usage; } ),
            new ArgParsed('x', delegate(ProgramContext ac, string arg) { ac.Action = ActionEnum.ListIPAddress; } ),
            new ArgParsed('?', delegate(ProgramContext ac, string arg) { } ),
            new ArgParsed('D', delegate(ProgramContext ac, string arg) { ac.CidrParse = CidrParseEnum.Default; } ),
            new ArgParsed('d', delegate(ProgramContext ac, string arg) { 

                byte? cidr = 0;
                if (!IPNetwork.TryParseCidr(arg, Sockets.AddressFamily.InterNetwork, out cidr)) {
                    Console.WriteLine("Invalid cidr {0}", cidr);
                    ac.Action = ActionEnum.Usage;
                    return;
                }
                ac.CidrParse = CidrParseEnum.Value;
                ac.CidrParsed = (byte)cidr;
                
            }),
            new ArgParsed('s', delegate(ProgramContext ac, string arg) { 
                
                byte? cidr = null;
                if (!IPNetwork.TryParseCidr(arg, Sockets.AddressFamily.InterNetwork, out cidr)) {
                    Console.WriteLine("Invalid cidr {0}", cidr);
                    ac.Action = ActionEnum.Usage;
                    return;
                }

                ac.Action = ActionEnum.Subnet;
                ac.SubnetCidr = (byte)cidr;

            }),
            new ArgParsed('C', delegate(ProgramContext ac, string arg) { 
                
                IPNetwork ipnetwork = null;
                if (!Program.TryParseIPNetwork(arg, ac.CidrParse, ac.CidrParsed, out ipnetwork)) {
                    Console.WriteLine("Unable to parse ipnetwork {0}", arg);
                    ac.Action = ActionEnum.Usage;
                    return;
                }

                ac.Action = ActionEnum.ContainNetwork;
                ac.ContainNetwork = ipnetwork;

            }),
            new ArgParsed('o', delegate(ProgramContext ac, string arg) { 
                
                IPNetwork ipnetwork = null;
                if (!Program.TryParseIPNetwork(arg, ac.CidrParse, ac.CidrParsed, out ipnetwork)) {
                    Console.WriteLine("Unable to parse ipnetwork {0}", arg);
                    ac.Action = ActionEnum.Usage;
                    return;
                }

                ac.Action = ActionEnum.OverlapNetwork;
                ac.OverlapNetwork = ipnetwork;

            }),
            new ArgParsed('S', delegate(ProgramContext ac, string arg) { 
                
                IPNetwork ipnetwork = null;
                if (!Program.TryParseIPNetwork(arg, ac.CidrParse, ac.CidrParsed, out ipnetwork)) {
                    Console.WriteLine("Unable to parse ipnetwork {0}", arg);
                    ac.Action = ActionEnum.Usage;
                    return;
                }

                ac.Action = ActionEnum.SubstractNetwork;
                ac.SubstractNetwork = ipnetwork;

            })

        };


        static Program() {
            foreach (ArgParsed ap in Program.ArgsList) {
                Program.Args.Add(ap.Arg, ap);
            }
        }


        private static ProgramContext ParseArgs(string[] args) {
            int c;
            Getopt g = new Getopt("ipnetwork", args, "inmcbfltud:Dhs:wWxC:o:S:");
            ProgramContext ac = new ProgramContext();

			while ((c = g.getopt()) != -1) {
                string optArg = g.Optarg;
                Program.Args[c].Run(ac, optArg);
            }

            List<string> ipnetworks = new List<string>();
            for (int i = g.Optind; i < args.Length; i++) {
                if (!string.IsNullOrEmpty(args[i])) {
                    ipnetworks.Add(args[i]);
                }
            }
            ac.NetworksString = ipnetworks.ToArray();
            Program.ParseIPNetworks(ac);

            if (ac.Networks.Length == 0) {
                Console.WriteLine("Provide at least one ipnetwork");
                ac.Action = ActionEnum.Usage;
            }

            if (ac.Action == ActionEnum.Supernet
                && ipnetworks.Count < 2) {
                Console.WriteLine("Supernet action required at least two ipnetworks");
                ac.Action = ActionEnum.Usage;
            }

            if (ac.Action == ActionEnum.WideSupernet
                && ipnetworks.Count < 2) {
                Console.WriteLine("WideSupernet action required at least two ipnetworks");
                ac.Action = ActionEnum.Usage;
            }

            if (Program.PrintNoValue(ac)) {
                Program.PrintAll(ac);
            }

            if (g.Optind == 0) {
                Program.PrintAll(ac);
            }

            return ac;
        }

        private static void ParseIPNetworks(ProgramContext ac) {

            List<IPNetwork> ipnetworks = new List<IPNetwork>();
            foreach (string ips in ac.NetworksString) {
                IPNetwork ipnetwork = null;
                if (!Program.TryParseIPNetwork(ips, ac.CidrParse, ac.CidrParsed, out ipnetwork)) {
                    Console.WriteLine("Unable to parse ipnetwork {0}", ips);
                    continue;
                }
                ipnetworks.Add(ipnetwork);
            }
            ac.Networks = ipnetworks.ToArray();

        }

        private static bool TryParseIPNetwork(string ip, CidrParseEnum cidrParseEnum, byte cidr, out IPNetwork ipn) {

            IPNetwork ipnetwork = null;
            if (cidrParseEnum == CidrParseEnum.Default) {
                if (!IPNetwork.TryParse(ip, out ipnetwork)) {
                    ipn = null;
                    return false;
                }
            }
            else if (cidrParseEnum == CidrParseEnum.Value) {
                if (!IPNetwork.TryParse(ip, cidr, out ipnetwork)) {
                    if (!IPNetwork.TryParse(ip, out ipnetwork)) {
                        ipn = null;
                        return false;
                    }
                }
            }
            ipn = ipnetwork;
            return true;
        }


        private static bool PrintNoValue(ProgramContext ac) {
            if (ac == null) {
                throw new ArgumentNullException("ac");
            }

            return ac.IPNetwork == false
                  && ac.Network == false
                  && ac.Netmask == false
                  && ac.Cidr == false
                  && ac.Broadcast == false
                  && ac.FirstUsable == false
                  && ac.LastUsable == false
                  && ac.Total == false
                  && ac.Usable == false;
        }

        private static void PrintAll(ProgramContext ac) {
            if (ac == null) {
                throw new ArgumentNullException("ac");
            }

            ac.IPNetwork = true;
            ac.Network = true;
            ac.Netmask = true;
            ac.Cidr = true;
            ac.Broadcast = true;
            ac.FirstUsable = true;
            ac.LastUsable = true;
            ac.Usable = true;
            ac.Total = true;
        }

        private static void Usage() {

            Assembly assembly = Assembly.GetEntryAssembly()
                ?? Assembly.GetExecutingAssembly()
                ?? Assembly.GetCallingAssembly()
                ;
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

            Console.WriteLine("Usage: ipnetwork [-inmcbflu] [-d cidr|-D] [-h|-s cidr|-S|-w|-W|-x|-C network|-o network] networks ...");
            Console.WriteLine("Version: {0}", version);
            Console.WriteLine();
            Console.WriteLine("Print options");
            Console.WriteLine("\t-i : network");
            Console.WriteLine("\t-n : network address");
            Console.WriteLine("\t-m : netmask");
            Console.WriteLine("\t-c : cidr");
            Console.WriteLine("\t-b : broadcast");
            Console.WriteLine("\t-f : first usable ip address");
            Console.WriteLine("\t-l : last usable ip address");
            Console.WriteLine("\t-u : number of usable ip addresses");
            Console.WriteLine("\t-t : total number of ip addresses");
            Console.WriteLine();
            Console.WriteLine("Parse options");
            Console.WriteLine("\t-d cidr : use cidr if not provided (default /32)");
            Console.WriteLine("\t-D      : IPv4 only - use default cidr (ClassA/8, ClassB/16, ClassC/24)");
            Console.WriteLine();
            Console.WriteLine("Actions");
            Console.WriteLine("\t-h         : help message");
            Console.WriteLine("\t-s cidr    : split network into cidr subnets");
            Console.WriteLine("\t-w         : supernet networks into smallest possible subnets");
            Console.WriteLine("\t-W         : supernet networks into one single subnet");
            Console.WriteLine("\t-x         : list all ipadresses in networks");
            Console.WriteLine("\t-C network : network contain networks");
            Console.WriteLine("\t-o network : network overlap networks");
            Console.WriteLine("\t-S network : substract network from subnet");
            Console.WriteLine("");
            Console.WriteLine("networks  : one or more network addresses ");
            Console.WriteLine("            (1.2.3.4 10.0.0.0/8 10.0.0.0/255.0.0.0 2001:db8::/32 2001:db8:1:2:3:4:5:6/128 )");

        }
    }
}
