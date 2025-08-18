# IPNetwork 

[![AppVeyor Build Status](https://ci.appveyor.com/api/projects/status/github/lduchosal/ipnetwork?branch=master&svg=true)](https://ci.appveyor.com/project/lduchosal/ipnetwork/branch/master)
[![Nuget](https://img.shields.io/badge/nuget-ipnetwork2-blue.svg)](https://www.nuget.org/packages/ipnetwork2)
[![Nuget](https://img.shields.io/nuget/dt/IPNetwork2)](https://www.nuget.org/packages/ipnetwork2)
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2Flduchosal%2Fipnetwork.svg?type=shield)](https://app.fossa.com/projects/git%2Bgithub.com%2Flduchosal%2Fipnetwork?ref=badge_shield)
[![Coverage Status](https://coveralls.io/repos/github/lduchosal/ipnetwork/badge.svg?branch=master)](https://coveralls.io/github/lduchosal/ipnetwork?branch=master)
![GitHub last commit](https://img.shields.io/github/last-commit/lduchosal/ipnetwork)
[![CodeFactor](https://www.codefactor.io/repository/github/lduchosal/ipnetwork/badge)](https://www.codefactor.io/repository/github/lduchosal/ipnetwork)
[![SonarCloud](https://github.com/lduchosal/ipnetwork/actions/workflows/sonarcloud.yml/badge.svg)](https://github.com/lduchosal/ipnetwork/actions/workflows/sonarcloud.yml)
[![.NET](https://github.com/lduchosal/ipnetwork/actions/workflows/dotnet.yml/badge.svg)](https://github.com/lduchosal/ipnetwork/actions/workflows/dotnet.yml)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=lduchosal_ipnetwork&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=lduchosal_ipnetwork)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=lduchosal_ipnetwork&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=lduchosal_ipnetwork)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=lduchosal_ipnetwork&metric=coverage)](https://sonarcloud.io/summary/new_code?id=lduchosal_ipnetwork)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=lduchosal_ipnetwork&metric=sqale_index)](https://sonarcloud.io/summary/new_code?id=lduchosal_ipnetwork)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=lduchosal_ipnetwork&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=lduchosal_ipnetwork)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=lduchosal_ipnetwork&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=lduchosal_ipnetwork)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=lduchosal_ipnetwork&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=lduchosal_ipnetwork)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=lduchosal_ipnetwork&metric=duplicated_lines_density)](https://sonarcloud.io/summary/new_code?id=lduchosal_ipnetwork)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=lduchosal_ipnetwork&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=lduchosal_ipnetwork)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=lduchosal_ipnetwork&metric=bugs)](https://sonarcloud.io/summary/new_code?id=lduchosal_ipnetwork)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=lduchosal_ipnetwork&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=lduchosal_ipnetwork)

IPNetwork command line and C# library take care of complex network, IP, IPv4, IPv6, netmask, CIDR, subnet, subnetting, supernet, and supernetting calculation for .NET developers. It works with IPv4 as well as IPv6, is written in C#, has a light and clean API, and is fully unit-tested.

---

## IPNetwork utility classes for .Net

IPNetwork utility classes take care of complex network, IP, IPv4, IPv6, netmask, CIDR, subnet, subnetting, supernet, and supernetting calculation for .NET developers. It works with IPv4 as well as IPv6, is written in C#, has a light and clean API, and is fully unit-tested with 100% code coverage.

---

### Installation

PM> nuget install IPNetwork2

---

### Example 1 (IPv4)

```C#
IPNetwork2 ipnetwork = IPNetwork2.Parse("192.168.168.100/24");

Console.WriteLine("Network : {0}", ipnetwork.Network);
Console.WriteLine("Cidr : {0}", ipnetwork.Cidr);
Console.WriteLine("Netmask : {0}", ipnetwork.Netmask);
Console.WriteLine("Broadcast : {0}", ipnetwork.Broadcast);

Console.WriteLine("FirstUsable : {0}", ipnetwork.FirstUsable);
Console.WriteLine("LastUsable : {0}", ipnetwork.LastUsable);
Console.WriteLine("Usable : {0}", ipnetwork.Usable);

Console.WriteLine("First : {0}", ipnetwork.First);
Console.WriteLine("Last : {0}", ipnetwork.Last);
Console.WriteLine("Total : {0}", ipnetwork.Total);
```

Output

```MD
Network : 192.168.168.0
Cidr : 24
Netmask : 255.255.255.0
Broadcast : 192.168.168.255

FirstUsable : 192.168.168.1
LastUsable : 192.168.168.254
Usable : 254

First : 192.168.168.0
Last : 192.168.168.255
Total : 256
```

---

### Example 2 (IPv6)

```C#
IPNetwork2 ipnetwork = IPNetwork2.Parse("2001:0db8::/64");

Console.WriteLine("Network : {0}", ipnetwork.Network);
Console.WriteLine("Netmask : {0}", ipnetwork.Netmask);
Console.WriteLine("Broadcast : {0}", ipnetwork.Broadcast);
Console.WriteLine("FirstUsable : {0}", ipnetwork.FirstUsable);
Console.WriteLine("LastUsable : {0}", ipnetwork.LastUsable);
Console.WriteLine("Usable : {0}", ipnetwork.Usable);
Console.WriteLine("Cidr : {0}", ipnetwork.Cidr);
```

Output

```JS
Network : 2001:db8::
Netmask : ffff:ffff:ffff:ffff::
Broadcast :
FirstUsable : 2001:db8::
LastUsable : 2001:db8::ffff:ffff:ffff:ffff
Usable : 18446744073709551616
Cidr : 64
```

---

### Example 3 (IPv6)

```C#
IPNetwork2 ipnetwork = IPNetwork2.Parse("2001:0db8::/64");

IPAddress ipaddress = IPAddress.Parse("2001:0db8::1");
IPAddress ipaddress2 = IPAddress.Parse("2001:0db9::1");

IPNetwork2 ipnetwork2 = IPNetwork2.Parse("2001:0db8::1/128");
IPNetwork2 ipnetwork3 = IPNetwork2.Parse("2001:0db9::1/64");

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
```

Output

```JS
2001:db8::/64 contains 2001:db8::1 : True
2001:db8::/64 contains 2001:db9::1 : False
2001:db8::/64 contains 2001:db8::1/128 : True
2001:db8::/64 contains 2001:db9::/64 : False
2001:db8::/64 overlap 2001:db8::1/128 : True
2001:db8::/64 overlap 2001:db9::/64 : False
```

---

### Example 4 (IPv6)

```C#
IPNetwork2 wholeInternet = IPNetwork2.Parse("::/0");
byte newCidr = 2;
IPNetworkCollection subneted = wholeInternet.Subnet(newCidr);

Console.WriteLine("{0} was subnetted into {1} subnets", wholeInternet, subneted.Count);
Console.WriteLine("First: {0}", subneted[0]);
Console.WriteLine("Last : {0}", subneted[subneted.Count - 1]);
Console.WriteLine("All  :");

foreach (IPNetwork2 ipnetwork in subneted) {
    Console.WriteLine("{0}", ipnetwork);
}
```

Output

```JS
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

### Example 5 (IPv6)

```C#
IPNetwork2 ipnetwork1 = IPNetwork2.Parse("2001:0db8::/32");
IPNetwork2 ipnetwork2 = IPNetwork2.Parse("2001:0db9::/32");
IPNetwork2[] ipnetwork3 = IPNetwork2.Supernet(new[] { ipnetwork1, ipnetwork2 });

Console.WriteLine("{0} + {1} = {2}", ipnetwork1, ipnetwork2, ipnetwork3[0]);
```

Output

```JS
2001:db8::/32 + 2001:db9::/32 = 2001:db8::/31
```

---

### Example 6

```C#
IPNetwork2 ipnetwork = IPNetwork2.Parse("192.168.0.0/24");
IPAddress ipaddress = IPAddress.Parse("192.168.0.100");
IPAddress ipaddress2 = IPAddress.Parse("192.168.1.100");

IPNetwork2 ipnetwork2 = IPNetwork2.Parse("192.168.0.128/25");
IPNetwork2 ipnetwork3 = IPNetwork2.Parse("192.168.1.1/24");

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
Console.WriteLine("{0} overlap {1} : {2}", ipnetwork, ipnetwork3, overlap2); A
```

Output

```JS
192.168.0.0/24 contains 192.168.0.100 : True
192.168.0.0/24 contains 192.168.1.100 : False
192.168.0.0/24 contains 192.168.0.128/25 : True
192.168.0.0/24 contains 192.168.1.0/24 : False
192.168.0.0/24 overlap 192.168.0.128/25 : True
192.168.0.0/24 overlap 192.168.1.0/24 : False
```

---

### Example 7

```C#
IPNetwork2 iana_a_block = IPNetwork2.IANA_ABLK_RESERVED1;
IPNetwork2 iana_b_block = IPNetwork2.IANA_BBLK_RESERVED1;
IPNetwork2 iana_c_block = IPNetwork2.IANA_CBLK_RESERVED1;

Console.WriteLine("IANA_ABLK_RESERVED1 is {0}", iana_a_block);
Console.WriteLine("IANA_BBLK_RESERVED1 is {0}", iana_b_block);
Console.WriteLine("IANA_CBLK_RESERVED1 is {0}", iana_c_block);
```

Output

```JS
IANA_ABLK_RESERVED1 is 10.0.0.0/8
IANA_BBLK_RESERVED1 is 172.16.0.0/12
IANA_CBLK_RESERVED1 is 192.168.0.0/16
```

---

### Example 8

```C#
IPNetwork2 wholeInternet = IPNetwork2.Parse("0.0.0.0/0");
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
```

Output

```JS
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

### Example 9 Supernet

```C#
IPNetwork2 ipnetwork1 = IPNetwork2.Parse("192.168.0.0/24");
IPNetwork2 ipnetwork2 = IPNetwork2.Parse("192.168.1.0/24");
IPNetwork2[] ipnetwork3 = IPNetwork2.Supernet(new[]{ipnetwork1, ipnetwork2});

Console.WriteLine("{0} + {1} = {2}", ipnetwork1, ipnetwork2, ipnetwork3[0]);
```

Output

```JS
192.168.0.0/24 + 192.168.1.0/24 = 192.168.0.0/23
```

---

### Example 9 Operator +

```C#
IPNetwork2 ipnetwork1 = IPNetwork2.Parse("192.168.0.0/24");
IPNetwork2 ipnetwork2 = IPNetwork2.Parse("192.168.1.0/24");
IPNetwork2[] ipnetwork3 = ipnetwork1 + ipnetwork2;

Console.WriteLine("{0} + {1} = {2}", ipnetwork1, ipnetwork2, ipnetwork3[0]);
```

Output

```JS
192.168.0.0/24 + 192.168.1.0/24 = 192.168.0.0/23
```

---

### Example 10 - ClassLess network parse

If you don't specify the network cidr, IPNetwork will try to guess the CIDR for you. There are two strategies to guess ClassFull (default) and ClassLess.

#### ClassFull (default strategy)

is based on the default Class A, B or C networks.
IPV4 :

- Class A: 0 - 127 with a mask of 255.0.0.0 (/8)
- Class B: 128 - 191 with a mask of 255.255.0.0 (/16)
- Class C: 192 - 223 with a mask of 255.255.255.0 (/24)

IPV6 : /64

#### ClassLess

IPV4 : /32
IPV6 : /128


#### NetworkAware

IPV4 :

Rule of thumb
• Ends with .0 → /24
• Ends with .0.0 or .255.255 → /16
• Ends with .0.0.0 or .255.255.255 → /8
• Else → /32


IPV6 : 

Rule of thumb
• Ends with :0000 → /112
• Ends with :0000:0000 → /96
• Ends with three trailing :0000 → /80
• …
• Ends with four trailing :0000 → /64
• Else → /128

#### IPv4

```C#
IPNetwork2 defaultParse= IPNetwork2.Parse("192.168.0.0"); // default to ClassFull
IPNetwork2 classFullParse = IPNetwork2.Parse("192.168.0.0", CidrGuess.ClassFull);
IPNetwork2 classLessParse = IPNetwork2.Parse("192.168.0.0", CidrGuess.ClassLess);
IPNetwork2 networkAwareParse = IPNetwork2.Parse("192.168.0.0", CidrGuess.NetworkAware);

Console.WriteLine("IPV4 Default Parse : {0}", defaultStrategy);
Console.WriteLine("IPV4 ClassFull Parse : {0}", classFullParse);
Console.WriteLine("IPV4 ClassLess Parse : {0}", classLessParse);
Console.WriteLine("IPV4 NetworkAware Parse : {0}", networkAwareParse);
```

Output

```JS
IPV4 Default Parse : 192.168.0.0/24
IPV4 ClassFull Parse : 192.168.0.0/24
IPV4 ClassLess Parse : 192.168.0.0/32
IPV4 NetworkAware Parse : 192.168.0.0/16
```

#### IPv6

```C#
IPNetwork2 defaultParse = IPNetwork2.Parse("::1"); // default to ClassFull
IPNetwork2 classFullParse = IPNetwork2.Parse("::1", CidrGuess.ClassFull);
IPNetwork2 classLessParse = IPNetwork2.Parse("::1", CidrGuess.ClassLess);
IPNetwork2 networkAwareParse = IPNetwork2.Parse("::1", CidrGuess.NetworkAware);

Console.WriteLine("IPV6 Default Parse : {0}", defaultParse);
Console.WriteLine("IPV6 ClassFull Parse : {0}", classFullParse);
Console.WriteLine("IPV6 ClassLess Parse : {0}", classLessParse);
Console.WriteLine("IPV6 NetworkAware Parse : {0}", networkAwareParse);
```

Output

```JS
IPV6 Default Parse : ::/64
IPV6 ClassFull Parse : ::/64
IPV6 ClassLess Parse : ::1/128
IPV6 ClassLess Parse : ::1/128
```

---

### Example 11 - Subtract network class

Here's a C# implementation for IP network symmetric difference (subtraction)

#### IPv4

```C#
// Prepare
var network1 = IPNetwork2.Parse("0.0.0.0", 0);
var network2 = IPNetwork2.Parse("10.0.0.1", 32);

// Act
var result = network1.Subtract(network2);

// Assert
string ips = string.Join(", ", result);
Console.WriteLine("{0}", ips);
```
Resut:
```JS
    0.0.0.0/5, 8.0.0.0/7, 10.0.0.0/32, 10.0.0.2/31, 10.0.0.4/30, 10.0.0.8/29,
    10.0.0.16/28, 10.0.0.32/27, 10.0.0.64/26, 10.0.0.128/25, 10.0.1.0/24,
    10.0.2.0/23, 10.0.4.0/22, 10.0.8.0/21, 10.0.16.0/20, 10.0.32.0/19, 10.0.64.0/18,
    10.0.128.0/17, 10.1.0.0/16, 10.2.0.0/15, 10.4.0.0/14, 10.8.0.0/13, 10.16.0.0/12,
    10.32.0.0/11, 10.64.0.0/10, 10.128.0.0/9, 11.0.0.0/8, 12.0.0.0/6, 16.0.0.0/4,
    32.0.0.0/3, 64.0.0.0/2, 128.0.0.0/1
```
---

#### IPv6

```C#
// Prepare
var network1 = IPNetwork2.Parse("::", 0);
var network2 = IPNetwork2.Parse("::", 1);

// Act
var result = network1.Subtract(network2);

// Assert
string ips = string.Join(", ", result);
Console.WriteLine("{0}", ips);
```
Resut:
```JS
8000::/1
```

---

#### operator -

```C#
// Prepare
var network1 = IPNetwork2.Parse("0.0.0.0", 0);
var network2 = IPNetwork2.Parse("10.0.0.1", 32);

// Act
var result = network1 - network2;

// Assert
string ips = string.Join(", ", result);
Console.WriteLine("{0}", ips);
```
Resut:
```JS
    0.0.0.0/5, 8.0.0.0/7, 10.0.0.0/32, 10.0.0.2/31, 10.0.0.4/30, 10.0.0.8/29,
    10.0.0.16/28, 10.0.0.32/27, 10.0.0.64/26, 10.0.0.128/25, 10.0.1.0/24,
    10.0.2.0/23, 10.0.4.0/22, 10.0.8.0/21, 10.0.16.0/20, 10.0.32.0/19, 10.0.64.0/18,
    10.0.128.0/17, 10.1.0.0/16, 10.2.0.0/15, 10.4.0.0/14, 10.8.0.0/13, 10.16.0.0/12,
    10.32.0.0/11, 10.64.0.0/10, 10.128.0.0/9, 11.0.0.0/8, 12.0.0.0/6, 16.0.0.0/4,
    32.0.0.0/3, 64.0.0.0/2, 128.0.0.0/1
```
---

## IPv6 Unique Local Address (ULA)

Unique Local Addresses are IPv6 addresses in the range fc00::/7 that are not routed on the public Internet. They are the IPv6 equivalent of private IPv4 addresses (e.g. 10.0.0.0/8, 192.168.0.0/16).

```C#
// Generate a random ULA prefix
var randomUla = UniqueLocalAddress.GenerateUlaPrefix();
Console.WriteLine($"Random ULA: {randomUla}"); // e.g., fd12:3456:789a::/48

// Create subnets with int subnet IDs (CLS-compliant)
var subnet1 = UniqueLocalAddress.CreateUlaSubnet(randomUla, 1);
var subnet2 = UniqueLocalAddress.CreateUlaSubnet(randomUla, 2);
var maxSubnet = UniqueLocalAddress.CreateUlaSubnet(randomUla, UniqueLocalAddress.MaxSubnetId);

Console.WriteLine($"Subnet 1: {subnet1}"); // e.g., fd12:3456:
```

---
## ParseRange and TryParseRange

A C# utility for converting IP address ranges into optimal CIDR blocks, supporting both IPv4 and IPv6 addresses.
Both IPv4 and IPv6 ranges are supported
The algorithm generates the minimal set of CIDR blocks that exactly cover the specified range
Input format must be "startIP-endIP" with a single hyphen separator
Whitespace around IP addresses is automatically trimmed
Mixed IPv4/IPv6 ranges are not supported (both addresses must be the same family)

### IPv4 Example

```C#
string ipv4Range = "192.168.1.45 - 192.168.1.65";
var ipv4Blocks = IPNetwork2.ParseRange(ipv4Range);

Console.WriteLine($"CIDR blocks for {ipv4Range}:");
foreach (var block in ipv4Blocks)
{
Console.WriteLine($"  {block}");
}
```

Output
```
CIDR blocks for 192.168.1.45 - 192.168.1.65:
  192.168.1.45/32
  192.168.1.46/31
  192.168.1.48/28
  192.168.1.64/31
```

### IPv6 Example

```C#
string ipv6Range = "2001:db8::1000 - 2001:db8::1fff";
var ipv6Blocks = IPNetwork2.ParseRange(ipv6Range);

Console.WriteLine($"CIDR blocks for {ipv6Range}:");
foreach (var block in ipv6Blocks)
{
Console.WriteLine($"  {block}");
}
```

Ouput
````
CIDR blocks for 2001:db8::1000 - 2001:db8::1fff:
  2001:db8::1000/116
````

---

## IPNetwork utility command line

IPNetwork utility command line take care of complex network, ip, netmask,
subnet, cidr calculation for command line. It works with IPv4,
it is written in C# and has a light and clean API and is fully unit tested.

Below some examples :

---

```JS
Provide at least one ipnetwork
Usage: ipnetwork [-inmcbflu] [-d cidr|-D] [-h|-s cidr|-S|-w|-W|-x|-C network|-o network] networks ..
Version: 3.2.0

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

---

### Example 10

Display ipnetwork informations :

```JS
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

---

### Example 11

Split network into cidr

```JS
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

---

### Example 12

supernet networks into smallest possible subnets

```JS
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

---

### Example 13

supernet networks into smallest possible subnets

```JS
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

---

### Example 14

supernet networks into smallest possible subnets

```JS
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

---

### Example 15

Split network into cidr, display full network only

```JS
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

---

### Example 16

Test if an ip is contained in a network

```JS
C:\>ipnetwork -C 10.0.0.1 10.0.0.0/8 10.0.1.0/24

10.0.0.1/32 contains 10.0.0.0/8 : False
10.0.0.1/32 contains 10.0.1.0/24 : False
```

---

### Example 17

Test if a network overlap another network

```JS
C:\>ipnetwork -o 10.0.0.1/24 10.0.0.0/8 10.0.1.0/24

10.0.0.0/24 overlaps 10.0.0.0/8 : True
10.0.0.0/24 overlaps 10.0.1.0/24 : False
```

---

### Example 18

remove one ip from a class and regroup them into the smallest possible network

```JS
C:\> ipnetwork -i -s 32 192.168.0.0/24 \
          | grep -v \-\- \
          | awk "{print $3;}" \
          | grep -v 192.168.0.213/32 \
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

---

### Example 18 (IPv6)

IPv6 networks

```JS
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

## License
[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2Flduchosal%2Fipnetwork.svg?type=large)](https://app.fossa.io/projects/git%2Bgithub.com%2Flduchosal%2Fipnetwork?ref=badge_large)
