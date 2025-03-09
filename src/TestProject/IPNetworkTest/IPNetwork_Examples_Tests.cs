// <copyright file="IPNetwork_Examples_Tests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkTest;
using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// IPNetworkUnitTest test every single method.
/// </summary>
[TestClass]
public class IPNetwork_Examples_Tests
{

    [TestMethod]
    public void Example1()
    {
            var ipnetwork = IPNetwork2.Parse("192.168.168.100/24");

            Console.WriteLine("Network : {0}", ipnetwork.Network);
            Console.WriteLine("Netmask : {0}", ipnetwork.Netmask);
            Console.WriteLine("Broadcast : {0}", ipnetwork.Broadcast);
            Console.WriteLine("FirstUsable : {0}", ipnetwork.FirstUsable);
            Console.WriteLine("LastUsable : {0}", ipnetwork.LastUsable);
            Console.WriteLine("Usable : {0}", ipnetwork.Usable);
            Console.WriteLine("Cidr : {0}", ipnetwork.Cidr);
        }

    [TestMethod]
    public void Example2()
    {
            var ipnetwork = IPNetwork2.Parse("192.168.0.0/24");
            var ipaddress = IPAddress.Parse("192.168.0.100");
            var ipaddress2 = IPAddress.Parse("192.168.1.100");

            var ipnetwork2 = IPNetwork2.Parse("192.168.0.128/25");
            var ipnetwork3 = IPNetwork2.Parse("192.168.1.1/24");

            bool contains1 = ipnetwork.Contains(ipaddress);
            bool contains2 = ipnetwork.Contains(ipaddress2);
            bool contains3 = ipnetwork.Contains(ipnetwork2);
            bool contains4 = ipnetwork.Contains(ipnetwork3);

            bool overlap1 = ipnetwork.Overlap(ipnetwork2);
            bool overlap2 = ipnetwork.Overlap(ipnetwork3);

            Console.WriteLine("{0} contains {1} : {2}", ipnetwork, ipaddress, contains1);
            Console.WriteLine("{0} contains {1} : {2}", ipnetwork, ipaddress2, contains2);
            Console.WriteLine("{0} contains {1} : {2}", ipnetwork, ipnetwork2, contains3);
            Console.WriteLine("{0} contains {1} : {2}", ipnetwork, ipnetwork3, contains4);

            Console.WriteLine("{0} overlap {1} : {2}", ipnetwork, ipnetwork2, overlap1);
            Console.WriteLine("{0} overlap {1} : {2}", ipnetwork, ipnetwork3, overlap2);
        }

    [TestMethod]
    public void Example2b()
    {
            var ipnetwork1 = IPNetwork2.Parse("10.1.0.0/16");
            var ipnetwork2 = IPNetwork2.Parse("192.168.1.0/24");

            var ipaddress1 = IPAddress.Parse("192.168.1.1");
            var ipaddress2 = IPAddress.Parse("192.168.2.100");
            var ipaddress3 = IPAddress.Parse("10.1.2.3");
            var ipaddress4 = IPAddress.Parse("10.4.5.6");

            bool contains1 = ipnetwork2.Contains(ipaddress1);
            bool contains2 = ipnetwork2.Contains(ipaddress2);
            bool contains3 = ipnetwork1.Contains(ipaddress3);
            bool contains4 = ipnetwork1.Contains(ipaddress4);

            Console.WriteLine("{0} contains {1} : {2}", ipnetwork1, ipaddress1, contains1);
            Console.WriteLine("{0} contains {1} : {2}", ipnetwork1, ipaddress2, contains2);
            Console.WriteLine("{0} contains {1} : {2}", ipnetwork2, ipaddress3, contains3);
            Console.WriteLine("{0} contains {1} : {2}", ipnetwork2, ipaddress4, contains4);
        }

    [TestMethod]
    public void Example3()
    {
            IPNetwork2 iana_a_block = IPNetwork2.IANA_ABLK_RESERVED1;
            IPNetwork2 iana_b_block = IPNetwork2.IANA_BBLK_RESERVED1;
            IPNetwork2 iana_c_block = IPNetwork2.IANA_CBLK_RESERVED1;

            Console.WriteLine("IANA_ABLK_RESERVED1 is {0}", iana_a_block);
            Console.WriteLine("IANA_BBLK_RESERVED1 is {0}", iana_b_block);
            Console.WriteLine("IANA_CBLK_RESERVED1 is {0}", iana_c_block);
        }

    [TestMethod]
    public void Example4()
    {
            var wholeInternet = IPNetwork2.Parse("0.0.0.0/0");
            byte newCidr = 2;
            IPNetworkCollection subneted = wholeInternet.Subnet(newCidr);

            Console.WriteLine("{0} was subnetted into {1} subnets", wholeInternet, subneted.Count);
            Console.WriteLine("First: {0}", subneted[0]);
            Console.WriteLine("Last : {0}", subneted[subneted.Count - 1]);
            Console.WriteLine("All  :");

            foreach (IPNetwork2 ipnetwork in subneted)
            {
                Console.WriteLine("{0}", ipnetwork);
            }
        }

    [TestMethod]
    public void Example5()
    {
            var ipnetwork1 = IPNetwork2.Parse("192.168.0.0/24");
            var ipnetwork2 = IPNetwork2.Parse("192.168.1.0/24");
            IPNetwork2[] ipnetwork3 = IPNetwork2.Supernet(new[] { ipnetwork1, ipnetwork2 });

            Console.WriteLine("{0} + {1} = {2}", ipnetwork1, ipnetwork2, ipnetwork3[0]);
        }

    [TestMethod]
    public void Example7()
    {
            var ipnetwork = IPNetwork2.Parse("192.168.168.100/24");

            var ipaddress = IPAddress.Parse("192.168.168.200");
            var ipaddress2 = IPAddress.Parse("192.168.0.200");

            bool contains1 = ipnetwork.Contains(ipaddress);
            bool contains2 = ipnetwork.Contains(ipaddress2);

            Console.WriteLine("{0} contains {1} : {2}", ipnetwork, ipaddress, contains1);
            Console.WriteLine("{0} contains {1} : {2}", ipnetwork, ipaddress2, contains2);
        }

    [TestMethod]
    public void Example9()
    {
            var network = IPNetwork2.Parse("192.168.0.1");
            var network2 = IPNetwork2.Parse("192.168.0.254");

            IPNetwork2 ipnetwork = network.Supernet(network2);

            Console.WriteLine("Network : {0}", ipnetwork.Network);
            Console.WriteLine("Netmask : {0}", ipnetwork.Netmask);
            Console.WriteLine("Broadcast : {0}", ipnetwork.Broadcast);
            Console.WriteLine("FirstUsable : {0}", ipnetwork.FirstUsable);
            Console.WriteLine("LastUsable : {0}", ipnetwork.LastUsable);
            Console.WriteLine("Usable : {0}", ipnetwork.Usable);
            Console.WriteLine("Cidr : {0}", ipnetwork.Cidr);
        }

    [TestMethod]
    public void When_TrySupernet_192_168_0_0_cidr24_add_192_168_10_0_cidr24_Then_Should_Invalid()
    {
            var network = IPNetwork2.Parse("192.168.0.0/24");
            var network2 = IPNetwork2.Parse("192.168.10.0/24");

            bool supernetted = network.TrySupernet(network2, out IPNetwork2 ipnetwork);
            Assert.AreEqual(false, supernetted);
        }

    [TestMethod]
    public void When_TryWideSubnet_192_168_0_0_cidr24_add_192_168_10_0_cidr24_Then_Should_Invalid()
    {
            var network = IPNetwork2.Parse("192.168.0.0/24");
            var network2 = IPNetwork2.Parse("192.168.10.0/24");

            bool wideSubnetted = IPNetwork2.TryWideSubnet(new[] { network, network2 }, out IPNetwork2 ipnetwork);
            Assert.AreEqual(true, wideSubnetted);
            Assert.AreEqual("192.168.0.0/20", ipnetwork.ToString());

            Console.WriteLine("Network : {0}", ipnetwork.Network);
            Console.WriteLine("Netmask : {0}", ipnetwork.Netmask);
            Console.WriteLine("Broadcast : {0}", ipnetwork.Broadcast);
            Console.WriteLine("FirstUsable : {0}", ipnetwork.FirstUsable);
            Console.WriteLine("LastUsable : {0}", ipnetwork.LastUsable);
            Console.WriteLine("Usable : {0}", ipnetwork.Usable);
            Console.WriteLine("Cidr : {0}", ipnetwork.Cidr);
        }

    [TestMethod]
    public void Example10()
    {
            var ipnetwork = IPNetwork2.Parse("192.168.0.1/25");

            Console.WriteLine("Network : {0}", ipnetwork.Network);
            Console.WriteLine("Netmask : {0}", ipnetwork.Netmask);
            Console.WriteLine("Broadcast : {0}", ipnetwork.Broadcast);
            Console.WriteLine("FirstUsable : {0}", ipnetwork.FirstUsable);
            Console.WriteLine("LastUsable : {0}", ipnetwork.LastUsable);
            Console.WriteLine("Usable : {0}", ipnetwork.Usable);
            Console.WriteLine("Cidr : {0}", ipnetwork.Cidr);
        }

    [TestMethod]
    public void Example11()
    {
            var defaultParse = IPNetwork2.Parse("192.168.0.0"); // default to ClassFull
            var classFullParse = IPNetwork2.Parse("192.168.0.0", CidrGuess.ClassFull);
            var classLessParse = IPNetwork2.Parse("192.168.0.0", CidrGuess.ClassLess);

            Console.WriteLine("IPV4 Default Parse : {0}", defaultParse);
            Console.WriteLine("IPV4 ClassFull Parse : {0}", classFullParse);
            Console.WriteLine("IPV4 ClassLess Parse : {0}", classLessParse);
        }

    }