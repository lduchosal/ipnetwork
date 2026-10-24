// <copyright file="IPNetwork2Members.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Numerics;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;

/// <summary>
/// Properties and members of IPNetwork2.
/// </summary>
public partial class IPNetwork2
{
    /// <summary>
    /// The whole mutable state of the instance, held behind a single reference.
    /// <para>
    /// The address, the family and the CIDR only ever change together, by one store of this field, so a
    /// reader always observes a network that was actually assigned and never a mix of two. Writing them
    /// as three separate fields could not offer that: a reader could pair the new CIDR with the old
    /// address and compute a network nobody ever asked for.
    /// </para>
    /// <para>
    /// Everything derived from that triple is memoized on the snapshot rather than on the instance,
    /// which is what invalidates the broadcast address and the hash code when <see cref="Value"/>
    /// rewrites the network: the old snapshot, caches included, is simply dropped.
    /// </para>
    /// </summary>
    private volatile Snapshot state;

    /// <summary>
    /// Gets or sets the network in CIDR notation.
    /// </summary>
    /// <remarks>
    /// The setter replaces the whole network at once. Readers of a single property, or of a single
    /// method such as <see cref="Contains(IPAddress)"/> or <see cref="ToString"/>, therefore see either
    /// the network before the assignment or the one after it. Combining several properties into one
    /// observation remains the caller's own responsibility, as for any mutable object.
    /// </remarks>
    [DataMember(Name = "IPNetwork", IsRequired = true)]
    public string Value
    {
        get
        {
            return this.ToString();
        }

        set
        {
            this.state = Parse(value).state;
        }
    }

    /// <summary>
    /// Gets network address.
    /// </summary>
    public IPAddress Network
    {
        get
        {
            Snapshot snapshot = this.state;
            return ToIPAddress(snapshot.Network, snapshot.Family);
        }
    }

    /// <summary>
    /// Gets address Family.
    /// </summary>
    public AddressFamily AddressFamily
    {
        get
        {
            return this.state.Family;
        }
    }

    /// <summary>
    /// Gets netmask.
    /// </summary>
    public IPAddress Netmask
    {
        get
        {
            Snapshot snapshot = this.state;
            return ToIPAddress(snapshot.Netmask, snapshot.Family);
        }
    }

    /// <summary>
    /// Gets broadcast address.
    /// </summary>
    public IPAddress? Broadcast
    {
        get
        {
            Snapshot snapshot = this.state;
            if (snapshot.Family == AddressFamily.InterNetworkV6)
            {
                return null;
            }

            return ToIPAddress(snapshot.Broadcast, snapshot.Family);
        }
    }

    /// <summary>
    /// Gets first usable IPAddress in Network.
    /// Per RFC 3021, /31 networks have no reserved network address.
    /// </summary>
    public IPAddress FirstUsable
    {
        get
        {
            Snapshot snapshot = this.state;
            BigInteger first = snapshot.Network;
            if (snapshot.Family == AddressFamily.InterNetwork
                && snapshot.Cidr < 31)
            {
                first += 1;
            }

            return ToIPAddress(first, snapshot.Family);
        }
    }

    /// <summary>
    /// Gets last usable IPAddress in Network.
    /// Per RFC 3021, /31 networks have no reserved broadcast address.
    /// </summary>
    public IPAddress LastUsable
    {
        get
        {
            Snapshot snapshot = this.state;
            BigInteger last = snapshot.Broadcast;
            if (snapshot.Family == AddressFamily.InterNetwork
                && snapshot.Cidr < 31)
            {
                last -= 1;
            }

            return ToIPAddress(last, snapshot.Family);
        }
    }

    /// <summary>
    /// Gets first IPAddress in Network.
    /// </summary>
    public IPAddress First
    {
        get
        {
            Snapshot snapshot = this.state;
            return ToIPAddress(snapshot.Network, snapshot.Family);
        }
    }

    /// <summary>
    /// Gets last IPAddress in Network.
    /// </summary>
    public IPAddress Last
    {
        get
        {
            Snapshot snapshot = this.state;
            return ToIPAddress(snapshot.Broadcast, snapshot.Family);
        }
    }

    /// <summary>
    /// Gets number of usable IPAddress in Network.
    /// 
    /// According to : https://en.wikipedia.org/wiki/Classless_Inter-Domain_Routing#IPv4_CIDR_blocks
    /// Address 	Mask			Total 	Usable 	Network 	Broadcast 	FirstUsable 	LastUsable 	Typical use
    /// 1.0.0.1/32 	255.255.255.255 	1 	**1** 	1.0.0.1 	1.0.0.1 	1.0.0.1 	1.0.0.1 	Host route
    /// 1.0.0.1/31 	255.255.255.254 	2 	**2** 	1.0.0.1 	1.0.0.2 	1.0.0.1 	1.0.0.2 	Point-to-point links (RFC 3021)
    /// ...
    /// </summary>
    public BigInteger Usable
    {
        get
        {
            Snapshot snapshot = this.state;
            if (snapshot.Family == AddressFamily.InterNetworkV6)
            {
                return TotalOf(snapshot);
            }

            byte[] mask = [0xff, 0xff, 0xff, 0xff, 0x00];
            var bmask = new BigInteger(mask);
            BigInteger usableIps;
            if (snapshot.Cidr == 32)
            {
                usableIps = 1;
            }
            else if (snapshot.Cidr == 31)
            {
                usableIps = 2;
            }
            else
            {
                usableIps = (bmask >> snapshot.Cidr) - 1;
            }

            return usableIps;
        }
    }

    /// <summary>
    /// Gets number of IPAddress in Network.
    /// </summary>
    public BigInteger Total
    {
        get
        {
            return TotalOf(this.state);
        }
    }

    /// <summary>
    /// Gets the CIDR netmask notation.
    /// </summary>
    public byte Cidr
    {
        get
        {
            return this.state.Cidr;
        }
    }

    /// <summary>
    /// Gets the broadcast address calculated from the network address and the netmask.
    /// </summary>
    internal BigInteger InternalBroadcast
    {
        get
        {
            return this.state.Broadcast;
        }
    }

    /// <summary>
    /// Gets the network address calculated by applying the subnet mask to the IP address.
    /// </summary>
    internal BigInteger InternalNetwork
    {
        get
        {
            return this.state.Network;
        }
    }

    /// <summary>
    /// Gets the address the network was built from, host bits included.
    /// </summary>
    internal BigInteger InternalIpAddress
    {
        get
        {
            return this.state.IpAddress;
        }
    }

    private static BigInteger TotalOf(Snapshot snapshot)
    {
        int max = snapshot.Family == AddressFamily.InterNetwork ? 32 : 128;
        return BigInteger.Pow(2, max - snapshot.Cidr);
    }

    /// <summary>
    /// An immutable (address, family, cidr) triple, plus the values derived from it.
    /// <para>
    /// Being immutable is what makes the triple publishable by a single store, and what lets several
    /// instances share one snapshot: <see cref="IPNetwork2.Copy"/> hands out a network backed by the
    /// same snapshot, so a caller who rewrites the copy cannot reach the original.
    /// </para>
    /// </summary>
    private sealed class Snapshot
    {
        private readonly BigInteger ipaddress;
        private readonly AddressFamily family;
        private readonly byte cidr;

        /// <summary>
        /// Lazily computed broadcast address.
        /// <para>
        /// This is deliberately a reference rather than a <see cref="Nullable{T}"/> of
        /// <see cref="BigInteger"/>: a <c>BigInteger?</c> spans three fields (<c>hasValue</c>, the sign
        /// and the magnitude array) and therefore cannot be written or read atomically, so a reader
        /// could observe it half initialised and compute a wrong broadcast address. A reference is
        /// published by a single store, and <c>volatile</c> orders that store after the box is fully
        /// initialised.
        /// </para>
        /// </summary>
        private volatile StrongBox<BigInteger>? cachedBroadcast;

        /// <summary>
        /// Lazily computed hash code, or zero while it has not been computed yet. An <see cref="int"/>
        /// is written and read atomically, so no box is needed here.
        /// </summary>
        private int cachedHashCode;

        internal Snapshot(BigInteger ipaddress, AddressFamily family, byte cidr)
        {
            this.ipaddress = ipaddress;
            this.family = family;
            this.cidr = cidr;
        }

        internal BigInteger IpAddress
        {
            get { return this.ipaddress; }
        }

        internal AddressFamily Family
        {
            get { return this.family; }
        }

        internal byte Cidr
        {
            get { return this.cidr; }
        }

        internal BigInteger Netmask
        {
            get { return ToUint(this.cidr, this.family); }
        }

        internal BigInteger Network
        {
            get { return this.ipaddress & this.Netmask; }
        }

        internal BigInteger Broadcast
        {
            get
            {
                // Volatile read: either the cache is not published yet, or it is fully initialised.
                StrongBox<BigInteger>? cached = this.cachedBroadcast;
                if (cached != null)
                {
                    return cached.Value;
                }

                BigInteger network = this.Network;
                BigInteger computed = CreateBroadcast(ref network, this.Netmask, this.family);

                // No lock: CreateBroadcast is pure, so racing threads compute the same value and the
                // duplicated work is cheaper than serialising every first access.
                this.cachedBroadcast = new StrongBox<BigInteger>(computed);

                return computed;
            }
        }

        internal int HashCode
        {
            get
            {
                int cached = Volatile.Read(ref this.cachedHashCode);
                if (cached != 0)
                {
                    return cached;
                }

                int hash = 17;
                hash = (hash * 31) + this.family.GetHashCode();
                hash = (hash * 31) + this.Network.GetHashCode();
                hash = (hash * 31) + this.cidr.GetHashCode();

                // Zero marks "not computed yet", so a network that hashes to zero is simply recomputed
                // on every call: the computation is pure, only the memoization is lost.
                Volatile.Write(ref this.cachedHashCode, hash);
                return hash;
            }
        }
    }
}
