// <copyright file="IPNetwork2WildcardMask.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Numerics;
using System.Net.Sockets;

/// <summary>
/// WildcardNetmask.
/// </summary>
public sealed partial class IPNetwork2
{
    /// <summary>
    /// Gets netmask Inverse
    /// https://en.wikipedia.org/wiki/Wildcard_mask
    ///
    /// A wildcard mask is a mask of bits that indicates which parts of an IP address are available for examination.
    /// In the Cisco IOS,[1] they are used in several places, for example:
    ///    To indicate the size of a network or subnet for some routing protocols, such as OSPF.
    ///    To indicate what IP addresses should be permitted or denied in access control lists(ACLs).
    ///
    /// A wildcard mask can be thought of as an inverted subnet mask.For example,
    /// a subnet mask of 255.255.255.0 (binary equivalent = 11111111.11111111.11111111.00000000)
    /// inverts to a wildcard mask of 0.0.0.255 (binary equivalent = 00000000.00000000.00000000.11111111).
    ///
    /// A wild card mask is a matching rule.[2] The rule for a wildcard mask is:
    ///     0 means that the equivalent bit must match
    ///     1 means that the equivalent bit does not matter
    ///
    /// Any wildcard bit-pattern can be masked for examination.For example, a wildcard mask of 0.0.0.254
    /// (binary equivalent = 00000000.00000000.00000000.11111110) applied to IP address 10.10.10.2
    /// (00001010.00001010.00001010.00000010) will match even-numbered IP addresses 10.10.10.0, 10.10.10.2,
    /// 10.10.10.4, 10.10.10.6 etc.
    ///
    /// Same mask applied to 10.10.10.1 (00001010.00001010.00001010.00000001) will match
    /// odd-numbered IP addresses 10.10.10.1, 10.10.10.3, 10.10.10.5 etc.
    ///
    /// A network and wildcard mask combination of 1.1.1.1 0.0.0.0 would match an interface configured
    /// exactly with 1.1.1.1 only, and nothing else.
    ///
    /// Wildcard masks are used in situations where subnet masks may not apply.For example,
    /// when two affected hosts fall in different subnets, the use of a wildcard mask will
    /// group them together.
    ///
    /// List of wildcard masks
    /// Slash Netmask Wildcard mask
    /// /32   255.255.255.255    0.0.0.0
    /// /31   255.255.255.254    0.0.0.1
    /// /30   255.255.255.252    0.0.0.3
    /// /29   255.255.255.248    0.0.0.7
    /// /28   255.255.255.240    0.0.0.15
    /// /27   255.255.255.224    0.0.0.31
    /// /26   255.255.255.192    0.0.0.63
    /// /25   255.255.255.128    0.0.0.127
    /// /24   255.255.255.0      0.0.0.255
    /// /23   255.255.254.0      0.0.1.255
    /// /22   255.255.252.0      0.0.3.255
    /// /21   255.255.248.0      0.0.7.255
    /// /20   255.255.240.0      0.0.15.255
    /// /19   255.255.224.0      0.0.31.255
    /// /18   255.255.192.0      0.0.63.255
    /// /17   255.255.128.0      0.0.127.255
    /// /16   255.255.0.0        0.0.255.255
    /// /15   255.254.0.0        0.1.255.255
    /// /14   255.252.0.0        0.3.255.255
    /// /13   255.248.0.0        0.7.255.255
    /// /12   255.240.0.0        0.15.255.255
    /// /11   255.224.0.0        0.31.255.255
    /// /10   255.192.0.0        0.63.255.255
    /// /9    255.128.0.0        0.127.255.255
    /// /8    255.0.0.0          0.255.255.255
    /// /7    254.0.0.0          1.255.255.255
    /// /6    252.0.0.0          3.255.255.255
    /// /5    248.0.0.0          7.255.255.255
    /// /4    240.0.0.0          15.255.255.255
    /// /3    224.0.0.0          31.255.255.255
    /// /2    192.0.0.0          63.255.255.255
    /// /1    128.0.0.0          127.255.255.255
    /// /0    0.0.0.0            255.255.255.255.
    ///
    /// </summary>
    public IPAddress WildcardMask
    {
        get
        {
            byte cidr = this.family == AddressFamily.InterNetwork ? (byte)32 : (byte)128;
            BigInteger netmask = IPNetwork2.ToUint(cidr, this.family);
            BigInteger wildcardmask = netmask - this.InternalNetmask;

            return IPNetwork2.ToIPAddress(wildcardmask, this.family);
        }
    }
}