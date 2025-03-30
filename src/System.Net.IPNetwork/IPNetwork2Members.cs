// <copyright file="IPNetwork2Members.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Numerics;
using System.Net.Sockets;
using System.Runtime.Serialization;

/// <summary>
/// Properties and members of IPNetwork2.
/// </summary>
public partial class IPNetwork2
{
    private readonly object sync = new ();
    private readonly int hashCode;
    private BigInteger ipaddress;
    private byte cidr;
    private BigInteger? cachedBroadcast;

    private AddressFamily family;

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets the value of the IPNetwork property.
    /// </summary>
    [DataMember(Name = "IPNetwork", IsRequired = true)]
    public string Value
    {
        get
        {
            return this.ToString();
        }

        set
        {
            var ipnetwork = Parse(value);
            this.ipaddress = ipnetwork.ipaddress;
            this.family = ipnetwork.family;
            this.cidr = ipnetwork.cidr;
            lock (this.sync)
            {
                this.cachedBroadcast = null;
            }
        }
    }

    /// <summary>
    /// Gets network address.
    /// </summary>
    public IPAddress Network
    {
        get
        {
            return ToIPAddress(this.InternalNetwork, this.family);
        }
    }

    /// <summary>
    /// Gets address Family.
    /// </summary>
    public AddressFamily AddressFamily
    {
        get
        {
            return this.family;
        }
    }

    /// <summary>
    /// Gets netmask.
    /// </summary>
    public IPAddress Netmask
    {
        get
        {
            return ToIPAddress(this.InternalNetmask, this.family);
        }
    }

    /// <summary>
    /// Gets broadcast address.
    /// </summary>
    public IPAddress Broadcast
    {
        get
        {
            if (this.family == AddressFamily.InterNetworkV6)
            {
                return null;
            }

            return ToIPAddress(this.InternalBroadcast, this.family);
        }
    }

    /// <summary>
    /// Gets first usable IP adress in Network.
    /// </summary>
    public IPAddress FirstUsable
    {
        get
        {
            BigInteger first = this.family == AddressFamily.InterNetworkV6
                ? this.InternalNetwork
                : (this.Usable <= 0)
                    ? this.InternalNetwork
                    : this.InternalNetwork + 1;
            return ToIPAddress(first, this.family);
        }
    }

    /// <summary>
    /// Gets last usable IP adress in Network.
    /// </summary>
    public IPAddress LastUsable
    {
        get
        {
            BigInteger last = this.family == AddressFamily.InterNetworkV6
                ? this.InternalBroadcast
                : (this.Usable <= 0)
                    ? this.InternalNetwork
                    : this.InternalBroadcast - 1;
            return ToIPAddress(last, this.family);
        }
    }

    /// <summary>
    /// Gets number of usable IP adress in Network.
    /// </summary>
    public BigInteger Usable
    {
        get
        {
            if (this.family == AddressFamily.InterNetworkV6)
            {
                return this.Total;
            }

            byte[] mask = [0xff, 0xff, 0xff, 0xff, 0x00];
            var bmask = new BigInteger(mask);
            BigInteger usableIps = (this.cidr > 30) ? 0 : ((bmask >> this.cidr) - 1);
            return usableIps;
        }
    }

    /// <summary>
    /// Gets number of IP adress in Network.
    /// </summary>
    public BigInteger Total
    {
        get
        {
            int max = this.family == AddressFamily.InterNetwork ? 32 : 128;
            var count = BigInteger.Pow(2, max - this.cidr);
            return count;
        }
    }

    /// <summary>
    /// Gets the CIDR netmask notation.
    /// </summary>
    public byte Cidr
    {
        get
        {
            return this.cidr;
        }
    }

    /// <summary>
    /// Gets the broadcast address calculated from the network address and the netmask.
    /// </summary>
    internal BigInteger InternalBroadcast
    {
        get
        {
            var cached = this.cachedBroadcast;
            if (cached != null)
            {
                return cached.Value;
            }

            lock (this.sync)
            {
                var cached2 = this.cachedBroadcast;
                if (cached2 != null)
                {
                    return cached2.Value;
                }

                var network = this.InternalNetwork;
                var computed = CreateBroadcast(ref network, this.InternalNetmask, this.family);
                this.cachedBroadcast = computed;
                return computed;
            }
        }
    }

    /// <summary>
    /// Gets the network address calculated by applying the subnet mask to the IP address.
    /// </summary>
    internal BigInteger InternalNetwork
    {
        get
        {
            BigInteger uintNetwork = this.ipaddress & this.InternalNetmask;
            return uintNetwork;
        }
    }

    /// <summary>
    /// Gets the netmask as a BigInteger representation based on the CIDR and address family.
    /// </summary>
    internal BigInteger InternalNetmask
    {
        get
        {
            return ToUint(this.cidr, this.family);
        }
    }
}