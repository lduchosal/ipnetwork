IPNetwork utility classes for .Net

IPNetwork utility classes take care of complex network, ip, netmask, 
subnet, cidr calculation for .Net developpers. It works with IPv4, 
it is written in C# and has a light and clean API and is fully unit tested.

Example #1 :

IPNetwork ipnetwork = IPNetwork.Parse("192.168.168.100/24");

Console.WriteLine("Network : {0}", ipnetwork.Network);
Console.WriteLine("Netmask : {0}", ipnetwork.Netmask);
Console.WriteLine("Broadcast : {0}", ipnetwork.Broadcast);
Console.WriteLine("FirstUsable : {0}", ipnetwork.FirstUsable);
Console.WriteLine("LastUsable : {0}", ipnetwork.LastUsable);
Console.WriteLine("Usable : {0}", ipnetwork.Usable);
Console.WriteLine("Cidr : {0}", ipnetwork.Cidr);

Output

Network : 192.168.168.0
Netmask : 255.255.255.0
Broadcast : 192.168.168.255
FirstUsable : 192.168.168.1
LastUsable : 192.168.168.254
Usable : 254
Cidr : 24


Example #2 :


IPNetwork ipnetwork = IPNetwork.Parse("192.168.0.0/24");
IPAddress ipaddress = IPAddress.Parse("192.168.0.100");
IPAddress ipaddress2 = IPAddress.Parse("192.168.1.100");

IPNetwork ipnetwork2 = IPNetwork.Parse("192.168.0.128/25");
IPNetwork ipnetwork3 = IPNetwork.Parse("192.168.1.1/24");

bool contains1 = IPNetwork.Contains(ipnetwork, ipaddress);
bool contains2 = IPNetwork.Contains(ipnetwork, ipaddress2);
bool contains3 = IPNetwork.Contains(ipnetwork, ipnetwork2);
bool contains4 = IPNetwork.Contains(ipnetwork, ipnetwork3);

bool overlap1 = IPNetwork.Overlap(ipnetwork, ipnetwork2);
bool overlap2 = IPNetwork.Overlap(ipnetwork, ipnetwork3);

Console.WriteLine("{0} contains {1} : {2}", ipnetwork, ipaddress, contains1);
Console.WriteLine("{0} contains {1} : {2}", ipnetwork, ipaddress2, contains2);
Console.WriteLine("{0} contains {1} : {2}", ipnetwork, ipnetwork2, contains3);
Console.WriteLine("{0} contains {1} : {2}", ipnetwork, ipnetwork3, contains4);

Console.WriteLine("{0} overlap {1} : {2}", ipnetwork, ipnetwork2, overlap1);
Console.WriteLine("{0} overlap {1} : {2}", ipnetwork, ipnetwork3, overlap2); A

Output


192.168.0.0/24 contains 192.168.0.100 : True
192.168.0.0/24 contains 192.168.1.100 : False
192.168.0.0/24 contains 192.168.0.128/25 : True
192.168.0.0/24 contains 192.168.1.0/24 : False
192.168.0.0/24 overlap 192.168.0.128/25 : True
192.168.0.0/24 overlap 192.168.1.0/24 : False


Example #3 :

IPNetwork iana_a_block = IPNetwork.IANA_ABLK_RESERVED1;
IPNetwork iana_b_block = IPNetwork.IANA_BBLK_RESERVED1;
IPNetwork iana_c_block = IPNetwork.IANA_CBLK_RESERVED1;

Console.WriteLine("IANA_ABLK_RESERVED1 is {0}", iana_a_block);
Console.WriteLine("IANA_BBLK_RESERVED1 is {0}", iana_b_block);
Console.WriteLine("IANA_CBLK_RESERVED1 is {0}", iana_c_block);

Output

IANA_ABLK_RESERVED1 is 10.0.0.0/8
IANA_BBLK_RESERVED1 is 172.16.0.0/12
IANA_CBLK_RESERVED1 is 192.168.0.0/16

Example #4 :


IPNetwork wholeInternet = IPNetwork.Parse("0.0.0.0/0");
byte newCidr = 2;
IPNetwork subneted = IPNetwork.Subnet(wholeInternet, newCidr);

Console.WriteLine("{0} was subnetted into {1} subnets", wholeInternet, subneted.Count);
Console.WriteLine("First: {0}", subneted[0]);
Console.WriteLine("Last : {0}", subneted[subneted.Count - 1]);
Console.WriteLine("All  :");

foreach (IPNetwork ipnetwork in subneted)
{
    Console.WriteLine("{0}", ipnetwork);
}

Output

0.0.0.0/0 was subnetted into 4 subnets
First: 0.0.0.0/2
Last : 192.0.0.0/2
All  :
0.0.0.0/2
64.0.0.0/2
128.0.0.0/2
192.0.0.0/2

Example #5 :


IPNetwork ipnetwork1 = IPNetwork.Parse("192.168.0.0/24");
IPNetwork ipnetwork2 = IPNetwork.Parse("192.168.1.0/24");
IPNetwork[] ipnetwork3 = IPNetwork.Supernet(new[]{ipnetwork1, ipnetwork2});

Console.WriteLine("{0} + {1} = {2}", ipnetwork1, ipnetwork2, ipnetwork3[0]);

Output

192.168.0.0/24 + 192.168.1.0/24 = 192.168.0.0/23

Have fun !
Luke S.