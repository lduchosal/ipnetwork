// <copyright file="IPNetworkCollection.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Collections;
using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// Represents a collection of IP networks based on a given parent IP network and subnet CIDR.
/// </summary>
public class IPNetworkCollection : IEnumerable<IPNetwork2>, IEnumerator<IPNetwork2>
{
    private readonly byte cidrSubnet;
    private readonly IPNetwork2 ipnetwork;
    private BigInteger enumerator;

    private byte Cidr
    {
        get { return this.ipnetwork.Cidr; }
    }

    private BigInteger Broadcast
    {
        get { return IPNetwork2.ToBigInteger(this.ipnetwork.Broadcast); }
    }

    private BigInteger LastUsable
    {
        get { return IPNetwork2.ToBigInteger(this.ipnetwork.LastUsable); }
    }

    private BigInteger Network
    {
        get { return IPNetwork2.ToBigInteger(this.ipnetwork.Network); }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IPNetworkCollection"/> class.
    /// Represents a collection of IP networks based on a given parent IP network and subnet CIDR.
    /// </summary>
    /// <remarks>
    /// This class is used to generate a collection of IP networks by dividing the given parent IP network into subnets based on the provided subnet CIDR (Classless Inter-Domain Routing
    /// ) value.
    /// </remarks>
    /// <param name="ipnetwork">The network.</param>
    /// <param name="cidrSubnet">The subnet.</param>
    /// <exception cref="ArgumentOutOfRangeException">Cidr is out of range.</exception>
    /// <exception cref="ArgumentException">Network is invalid.</exception>
#if TRAVISCI
    public
#else
    internal 
#endif
        IPNetworkCollection(IPNetwork2 ipnetwork, byte cidrSubnet)
    {
        int maxCidr = ipnetwork.AddressFamily == Sockets.AddressFamily.InterNetwork ? 32 : 128;
        if (cidrSubnet > maxCidr)
        {
            throw new ArgumentOutOfRangeException(nameof(cidrSubnet));
        }

        if (cidrSubnet < ipnetwork.Cidr)
        {
            throw new ArgumentException("cidrSubnet");
        }

        this.cidrSubnet = cidrSubnet;
        this.ipnetwork = ipnetwork;
        this.enumerator = -1;
    }

    /// <summary>
    /// Gets the total number of IP addresses in the subnet.
    /// </summary>
    public BigInteger Count
    {
        get
        {
            var count = BigInteger.Pow(2, this.cidrSubnet - this.Cidr);
            return count;
        }
    }

    /// <summary>
    /// Retrieves an IPNetwork2 object from the collection by index.
    /// </summary>
    /// <param name="i">The index of the IPNetwork2 object to retrieve.</param>
    /// <returns>
    /// The IPNetwork2 object at the specified index.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the specified index is greater than or equal to the Count of the collection.</exception>
    public IPNetwork2 this[BigInteger i]
    {
        get
        {
            if (i >= this.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(i));
            }

            BigInteger last = this.ipnetwork.AddressFamily == Sockets.AddressFamily.InterNetworkV6
                ? this.LastUsable
                : this.Broadcast;
            BigInteger increment = (last - this.Network) / this.Count;
            BigInteger uintNetwork = this.Network + ((increment + 1) * i);
            var ipn = new IPNetwork2(uintNetwork, this.ipnetwork.AddressFamily, this.cidrSubnet);
            return ipn;
        }
    }

    /// <inheritdoc/>
    IEnumerator<IPNetwork2> IEnumerable<IPNetwork2>.GetEnumerator()
    {
        return this;
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this;
    }

    /// <inheritdoc/>
    public IPNetwork2 Current
    {
        get { return this[this.enumerator]; }
    }

    /// <summary>
    /// Releases all resources used by the object.
    /// </summary>
    /// <remarks>
    /// This method implements the IDisposable interface and releases any resources
    /// held by the object. In this particular implementation, there are no resources
    /// to dispose of, so the method does nothing.
    /// </remarks>
    public void Dispose()
    {
        // nothing to dispose
    }

    /// <summary>
    /// Gets the element in the collection at the current position of the enumerator.
    /// </summary>
    object IEnumerator.Current
    {
        get { return this.Current; }
    }

    /// <summary>
    /// Moves the enumerator to the next element in the collection.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the enumerator was successfully moved to the next element;
    /// <see langword="false"/> if the enumerator has reached the end of the collection.
    /// </returns>
    public bool MoveNext()
    {
        this.enumerator++;
        if (this.enumerator >= this.Count)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Sets the enumerator to its initial position, which is before the first element in the collection.
    /// </summary>
    public void Reset()
    {
        this.enumerator = -1;
    }
}