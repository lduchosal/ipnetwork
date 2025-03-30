// <copyright file="IPNetworkV6ExamplesTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject.IPNetworkV6;

/// <summary>
/// Examples.
/// </summary>
public class IPNetworkV6ExamplesTests
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Example1()
    {
        var ipnetwork = IPNetwork2.Parse("2001:0db8::/64");

        Console.WriteLine("Network : {0}", ipnetwork.Network);
        Console.WriteLine("Netmask : {0}", ipnetwork.Netmask);
        Console.WriteLine("Broadcast : {0}", ipnetwork.Broadcast);
        Console.WriteLine("FirstUsable : {0}", ipnetwork.FirstUsable);
        Console.WriteLine("LastUsable : {0}", ipnetwork.LastUsable);
        Console.WriteLine("Usable : {0}", ipnetwork.Usable);
        Console.WriteLine("Cidr : {0}", ipnetwork.Cidr);
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Example2()
    {
        var ipnetwork = IPNetwork2.Parse("2001:0db8::/64");

        var ipaddress = IPAddress.Parse("2001:0db8::1");
        var ipaddress2 = IPAddress.Parse("2001:0db9::1");

        var ipnetwork2 = IPNetwork2.Parse("2001:0db8::1/128");
        var ipnetwork3 = IPNetwork2.Parse("2001:0db9::1/64");

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

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Example4()
    {
        var wholeInternet = IPNetwork2.Parse("::/0");
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

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Example5()
    {
        var ipnetwork1 = IPNetwork2.Parse("2001:0db8::/64");
        var ipnetwork2 = IPNetwork2.Parse("2001:0db9::/64");
        IPNetwork2[] ipnetwork3 = IPNetwork2.Supernet([ipnetwork1, ipnetwork2]);

        Console.WriteLine("{0} + {1} = {2}", ipnetwork1, ipnetwork2, ipnetwork3[0]);
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Example6()
    {
        var ipnetwork = IPNetwork2.Parse("fe80::202:b3ff:fe1e:8329/24");

        var ipaddress = IPAddress.Parse("2001:db8::");
        var ipaddress2 = IPAddress.Parse("fe80::202:b3ff:fe1e:1");

        bool contains1 = ipnetwork.Contains(ipaddress);
        bool contains2 = ipnetwork.Contains(ipaddress2);

        Console.WriteLine("{0} contains {1} : {2}", ipnetwork, ipaddress, contains1);
        Console.WriteLine("{0} contains {1} : {2}", ipnetwork, ipaddress2, contains2);
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Example8()
    {
        var network = IPNetwork2.Parse("::/124");
        IPNetworkCollection ips = network.Subnet(128);

        foreach (IPNetwork2 ip in ips)
        {
            Console.WriteLine("{0}", ip);
        }
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Example11()
    {
        var defaultParse = IPNetwork2.Parse("::1");
        var classFullParse = IPNetwork2.Parse("::1", CidrGuess.ClassFull);
        var classLessParse = IPNetwork2.Parse("::1", CidrGuess.ClassLess);

        Console.WriteLine("IPV6 Default Parse : {0}", defaultParse);
        Console.WriteLine("IPV6 ClassFull Parse : {0}", classFullParse);
        Console.WriteLine("IPV6 ClassLess Parse : {0}", classLessParse);
    }
}
