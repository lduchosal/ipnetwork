!!IPNetwork utility command line

IPNetwork utility command line take care of complex network, ip, netmask, 
subnet, cidr calculation for command line. It works with IPv4, 
it is written in C# and has a light and clean API and is fully unit tested.

Below some examples :

--------
{{
Usage: ipnetwork [-inmcbflu] [-d cidr|-D] [-h|-s cidr|-S|-w|-W|-x|-C network|-o network] networks ...

Print options
        -i : network
        -n : network address
        -m : netmask
        -c : cidr
        -b : broadcast
        -f : first usable ip address
        -l : last usable ip address
        -u : number of usable ip addresses

Parse options
        -d cidr : use cidr if not provided (default /32)
        -D      : use default cidr (ClassA/8, ClassB/16, ClassC/24)

Actions
        -h         : help message
        -s cidr    : split network into cidr subnets
        -w         : supernet networks into smallest possible subnets
        -W         : supernet networks into one single subnet
        -x         : list all ipadresses in networks
        -C network : network contain networks
        -o network : network overlap networks

networks  : one or more network addresses
            (1.2.3.4 10.0.0.0/8 10.0.0.0/255.0.0.0 ...)
}}
       
----
!!! Example 1
Display ipnetwork informations :

{{
c:\> ipnetwork 10.0.0.0/8

IPNetwork   : 10.0.0.0/8
Network     : 10.0.0.0
Netmask     : 255.0.0.0
Cidr        : 8
Broadcast   : 10.255.255.255
FirstUsable : 10.0.0.1
LastUsable  : 10.255.255.254
Usable      : 16777214
}}

----
!!! Example 2
Split network into cidr

{{
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
}}

----
!!! Example 3
supernet networks into smallest possible subnets

{{
C:\>ipnetwork -w 192.168.0.0/24 192.168.1.0/24

IPNetwork   : 192.168.0.0/23
Network     : 192.168.0.0
Netmask     : 255.255.254.0
Cidr        : 23
Broadcast   : 192.168.1.255
FirstUsable : 192.168.0.1
LastUsable  : 192.168.1.254
Usable      : 510
}}

----
!!! Example 4
supernet networks into smallest possible subnets

{{
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
}}

----
!!! Example 5
supernet networks into smallest possible subnets

{{
C:\>ipnetwork -W 192.168.0.0/24 192.168.129.0/24
IPNetwork   : 192.168.0.0/16
Network     : 192.168.0.0
Netmask     : 255.255.0.0
Cidr        : 16
Broadcast   : 192.168.255.255
FirstUsable : 192.168.0.1
LastUsable  : 192.168.255.254
Usable      : 65534
}}

----
!!! Example 6
Split network into cidr, display full network only

{{
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
}}

----
!!! Example 7
Test if an ip is contained in a network

{{
C:\>ipnetwork -C 10.0.0.1 10.0.0.0/8 10.0.1.0/24

10.0.0.1/32 contains 10.0.0.0/8 : False
10.0.0.1/32 contains 10.0.1.0/24 : False
}}

----
!!! Example 8
Test if a network overlap another network

{{
C:\>ipnetwork -o 10.0.0.1/24 10.0.0.0/8 10.0.1.0/24

10.0.0.0/24 overlaps 10.0.0.0/8 : True
10.0.0.0/24 overlaps 10.0.1.0/24 : False
}}

----
!!! Example 9
remove one ip from a class and regroup them into the smallest possible network

{{
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
}}

