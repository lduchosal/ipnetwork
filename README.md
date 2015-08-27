# IPNetwork
IPNetwork command line and C# library take care of complex network, IP, IPv4, IPv6, netmask, CIDR, subnet, subnetting, supernet, and supernetting calculation for .NET developers. It works with IPv4 as well as IPv6, is written in C#, has a light and clean API, and is fully unit-tested.


---

## IPNetwork utility classes for .Net

IPNetwork utility classes take care of complex network, IP, IPv4, IPv6, netmask, CIDR, subnet, subnetting, supernet, and supernetting calculation for .NET developers. It works with IPv4 as well as IPv6, is written in C#, has a light and clean API, and is fully unit-tested.

/!\ Breaking changes as of version 2.0 : namespace changed from LukeSkywalker.IPNetwork to System.Net.IPNetwork

---
### Installation :

PM> nuget install IPNetwork2

---
### Example 1 (IPv6) :
```
IPNetwork ipnetwork = IPNetwork.Parse("2001:0db8::/64");

Console.WriteLine("Network : {0}", ipnetwork.Network);
Console.WriteLine("Netmask : {0}", ipnetwork.Netmask);
Console.WriteLine("Broadcast : {0}", ipnetwork.Broadcast);
Console.WriteLine("FirstUsable : {0}", ipnetwork.FirstUsable);
Console.WriteLine("LastUsable : {0}", ipnetwork.LastUsable);
Console.WriteLine("Usable : {0}", ipnetwork.Usable);
Console.WriteLine("Cidr : {0}", ipnetwork.Cidr);
```
Output
```
Network : 2001:db8::
Netmask : ffff:ffff:ffff:ffff::
Broadcast : 
FirstUsable : 2001:db8::
LastUsable : 2001:db8::ffff:ffff:ffff:ffff
Usable : 18446744073709551616
Cidr : 64
```


---
### Example 2 (IPv6) :
```


IPNetwork ipnetwork = IPNetwork.Parse("2001:0db8::/64");

IPAddress ipaddress = IPAddress.Parse("2001:0db8::1");
IPAddress ipaddress2 = IPAddress.Parse("2001:0db9::1");

IPNetwork ipnetwork2 = IPNetwork.Parse("2001:0db8::1/128");
IPNetwork ipnetwork3 = IPNetwork.Parse("2001:0db9::1/64");

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
Console.WriteLine("{0} overlap {1} : {2}", ipnetwork, ipnetwork3, overlap2);

```
Output
```
2001:db8::/64 contains 2001:db8::1 : True
2001:db8::/64 contains 2001:db9::1 : False
2001:db8::/64 contains 2001:db8::1/128 : True
2001:db8::/64 contains 2001:db9::/64 : False
2001:db8::/64 overlap 2001:db8::1/128 : True
2001:db8::/64 overlap 2001:db9::/64 : False
```


---
### Example 3 (IPv6) :
```
IPNetwork wholeInternet = IPNetwork.Parse("::/0");
byte newCidr = 2;
IPNetworkCollection subneted = IPNetwork.Subnet(wholeInternet, newCidr);

Console.WriteLine("{0} was subnetted into {1} subnets", wholeInternet, subneted.Count);
Console.WriteLine("First: {0}", subneted[0]);
Console.WriteLine("Last : {0}", subneted[subneted.Count - 1]);
Console.WriteLine("All  :");

foreach (IPNetwork ipnetwork in subneted) {
    Console.WriteLine("{0}", ipnetwork);
}

```
Output
```
::/0 was subnetted into 4 subnets
First: ::/2
Last : c000::/2
All  :
::/2
4000::/2
8000::/2
c000::/2
```

---
### Example 4 (IPv6) :
```
IPNetwork ipnetwork1 = IPNetwork.Parse("2001:0db8::/32");
IPNetwork ipnetwork2 = IPNetwork.Parse("2001:0db9::/32");
IPNetwork[] ipnetwork3 = IPNetwork.Supernet(new[] { ipnetwork1, ipnetwork2 });

Console.WriteLine("{0} + {1} = {2}", ipnetwork1, ipnetwork2, ipnetwork3[0]);
```
Output
```
2001:db8::/32 + 2001:db9::/32 = 2001:db8::/31
```

---
### Example 5 :
```
IPNetwork ipnetwork = IPNetwork.Parse("192.168.168.100/24");

Console.WriteLine("Network : {0}", ipnetwork.Network);
Console.WriteLine("Netmask : {0}", ipnetwork.Netmask);
Console.WriteLine("Broadcast : {0}", ipnetwork.Broadcast);
Console.WriteLine("FirstUsable : {0}", ipnetwork.FirstUsable);
Console.WriteLine("LastUsable : {0}", ipnetwork.LastUsable);
Console.WriteLine("Usable : {0}", ipnetwork.Usable);
Console.WriteLine("Cidr : {0}", ipnetwork.Cidr);
```
Output
```
Network : 192.168.168.0
Netmask : 255.255.255.0
Broadcast : 192.168.168.255
FirstUsable : 192.168.168.1
LastUsable : 192.168.168.254
Usable : 254
Cidr : 24
```

---
### Example 6 :

```
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
```
Output

```
192.168.0.0/24 contains 192.168.0.100 : True
192.168.0.0/24 contains 192.168.1.100 : False
192.168.0.0/24 contains 192.168.0.128/25 : True
192.168.0.0/24 contains 192.168.1.0/24 : False
192.168.0.0/24 overlap 192.168.0.128/25 : True
192.168.0.0/24 overlap 192.168.1.0/24 : False
```

---
### Example 7 :
```
IPNetwork iana_a_block = IPNetwork.IANA_ABLK_RESERVED1;
IPNetwork iana_b_block = IPNetwork.IANA_BBLK_RESERVED1;
IPNetwork iana_c_block = IPNetwork.IANA_CBLK_RESERVED1;

Console.WriteLine("IANA_ABLK_RESERVED1 is {0}", iana_a_block);
Console.WriteLine("IANA_BBLK_RESERVED1 is {0}", iana_b_block);
Console.WriteLine("IANA_CBLK_RESERVED1 is {0}", iana_c_block);
```
Output
```
IANA_ABLK_RESERVED1 is 10.0.0.0/8
IANA_BBLK_RESERVED1 is 172.16.0.0/12
IANA_CBLK_RESERVED1 is 192.168.0.0/16
```
---
### Example 8 :

```
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
```
Output
```
0.0.0.0/0 was subnetted into 4 subnets
First: 0.0.0.0/2
Last : 192.0.0.0/2
All  :
0.0.0.0/2
64.0.0.0/2
128.0.0.0/2
192.0.0.0/2
```

---
### Example 9 :

```
IPNetwork ipnetwork1 = IPNetwork.Parse("192.168.0.0/24");
IPNetwork ipnetwork2 = IPNetwork.Parse("192.168.1.0/24");
IPNetwork[] ipnetwork3 = IPNetwork.Supernet(new[]{ipnetwork1, ipnetwork2});

Console.WriteLine("{0} + {1} = {2}", ipnetwork1, ipnetwork2, ipnetwork3[0]);
```
Output
```
192.168.0.0/24 + 192.168.1.0/24 = 192.168.0.0/23
```

---
## IPNetwork utility command line

IPNetwork utility command line take care of complex network, ip, netmask, 
subnet, cidr calculation for command line. It works with IPv4, 
it is written in C# and has a light and clean API and is fully unit tested.

Below some examples :

---
```
Provide at least one ipnetwork
Usage: ipnetwork [-inmcbflu] [-d cidr|-D] [-h|-s cidr|-S|-w|-W|-x|-C network|-o network] networks ...
Version: 2.0.1.0

Print options
        -i : network
        -n : network address
        -m : netmask
        -c : cidr
        -b : broadcast
        -f : first usable ip address
        -l : last usable ip address
        -u : number of usable ip addresses
        -t : total number of ip addresses

Parse options
        -d cidr : use cidr if not provided (default /32)
        -D      : IPv4 only - use default cidr (ClassA/8, ClassB/16, ClassC/24)

Actions
        -h         : help message
        -s cidr    : split network into cidr subnets
        -w         : supernet networks into smallest possible subnets
        -W         : supernet networks into one single subnet
        -x         : list all ipadresses in networks
        -C network : network contain networks
        -o network : network overlap networks
        -S network : substract network from subnet

networks  : one or more network addresses
            (1.2.3.4 10.0.0.0/8 10.0.0.0/255.0.0.0 2001:db8::/32 2001:db8:1:2:3:4:5:6/128 )
```
 
----
### Example 10 :
Display ipnetwork informations :

```
c:\> ipnetwork 10.0.0.0/8

IPNetwork   : 10.0.0.0/8
Network     : 10.0.0.0
Netmask     : 255.0.0.0
Cidr        : 8
Broadcast   : 10.255.255.255
FirstUsable : 10.0.0.1
LastUsable  : 10.255.255.254
Usable      : 16777214
```

----
### Example 11 :
Split network into cidr

```
c:\> ipnetwork -s 9 10.0.0.0/8

IPNetwork   : 10.0.0.0/9
Network     : 10.0.0.0
Netmask     : 255.128.0.0
Cidr        : 9
Broadcast   : 10.127.255.255
FirstUsable : 10.0.0.1
LastUsable  : 10.127.255.254
Usable      : 8388606
--
IPNetwork   : 10.128.0.0/9
Network     : 10.128.0.0
Netmask     : 255.128.0.0
Cidr        : 9
Broadcast   : 10.255.255.255
FirstUsable : 10.128.0.1
LastUsable  : 10.255.255.254
Usable      : 8388606
```

----
### Example 12 :
supernet networks into smallest possible subnets

```
C:\>ipnetwork -w 192.168.0.0/24 192.168.1.0/24

IPNetwork   : 192.168.0.0/23
Network     : 192.168.0.0
Netmask     : 255.255.254.0
Cidr        : 23
Broadcast   : 192.168.1.255
FirstUsable : 192.168.0.1
LastUsable  : 192.168.1.254
Usable      : 510
```

----
### Example 13 :
supernet networks into smallest possible subnets

```
c:\> ipnetwork -w 192.168.0.0/24 192.168.2.0/24 

IPNetwork   : 192.168.0.0/24
Network     : 192.168.0.0
Netmask     : 255.255.255.0
Cidr        : 24
Broadcast   : 192.168.0.255
FirstUsable : 192.168.0.1
LastUsable  : 192.168.0.254
Usable      : 254
--
IPNetwork   : 192.168.2.0/24
Network     : 192.168.2.0
Netmask     : 255.255.255.0
Cidr        : 24
Broadcast   : 192.168.2.255
FirstUsable : 192.168.2.1
LastUsable  : 192.168.2.254
Usable      : 254
```

----
### Example 14 :
supernet networks into smallest possible subnets

```
C:\>ipnetwork -W 192.168.0.0/24 192.168.129.0/24
IPNetwork   : 192.168.0.0/16
Network     : 192.168.0.0
Netmask     : 255.255.0.0
Cidr        : 16
Broadcast   : 192.168.255.255
FirstUsable : 192.168.0.1
LastUsable  : 192.168.255.254
Usable      : 65534
```

----
### Example 15 :
Split network into cidr, display full network only

```
C:\>ipnetwork -i -s 12 10.0.0.0/8 | grep -v \-\-

IPNetwork   : 10.0.0.0/12
IPNetwork   : 10.16.0.0/12
IPNetwork   : 10.32.0.0/12
IPNetwork   : 10.48.0.0/12
IPNetwork   : 10.64.0.0/12
IPNetwork   : 10.80.0.0/12
IPNetwork   : 10.96.0.0/12
IPNetwork   : 10.112.0.0/12
IPNetwork   : 10.128.0.0/12
IPNetwork   : 10.144.0.0/12
IPNetwork   : 10.160.0.0/12
IPNetwork   : 10.176.0.0/12
IPNetwork   : 10.192.0.0/12
IPNetwork   : 10.208.0.0/12
IPNetwork   : 10.224.0.0/12
IPNetwork   : 10.240.0.0/12
```

----
### Example 16 :
Test if an ip is contained in a network

```
C:\>ipnetwork -C 10.0.0.1 10.0.0.0/8 10.0.1.0/24

10.0.0.1/32 contains 10.0.0.0/8 : False
10.0.0.1/32 contains 10.0.1.0/24 : False
```

----
### Example 17 :
Test if a network overlap another network

```
C:\>ipnetwork -o 10.0.0.1/24 10.0.0.0/8 10.0.1.0/24

10.0.0.0/24 overlaps 10.0.0.0/8 : True
10.0.0.0/24 overlaps 10.0.1.0/24 : False
```

----
### Example 18 :
remove one ip from a class and regroup them into the smallest possible network

```
C:\> ipnetwork -i -s 32 192.168.0.0/24 \
          | grep -v \-\- \
          | awk "{print $3;}" \
          | grep -v 192.168.0.213/32 
          | xargs ipnetwork -i -w \
          | grep -v \-\- 
          
IPNetwork   : 192.168.0.224/27
IPNetwork   : 192.168.0.216/29
IPNetwork   : 192.168.0.214/31
IPNetwork   : 192.168.0.212/32
IPNetwork   : 192.168.0.208/30
IPNetwork   : 192.168.0.192/28
IPNetwork   : 192.168.0.128/26
IPNetwork   : 192.168.0.0/25
```

----
### Example 18 (IPv6) :
IPv6 networks
```
C:\> ipnetwork.exe 2001:0db8::/128
IPNetwork   : 2001:db8::/128
Network     : 2001:db8::
Netmask     : ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff
Cidr        : 128
Broadcast   : 2001:db8::
FirstUsable : 2001:db8::
LastUsable  : 2001:db8::
Usable      : 0
Total       : 1
```


Have fun !
