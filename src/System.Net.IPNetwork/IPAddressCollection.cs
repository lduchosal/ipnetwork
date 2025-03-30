// <copyright file="IPAddressCollection.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net;

using System.Collections;
using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// Represents different filters for a collection of items.
/// </summary>
public enum FilterEnum
{
    /// <summary>
    /// Every IPAdresses are returned
    /// </summary>
    All,

    /// <summary>
    /// Returns only usable IPAdresses
    /// </summary>
    Usable,
}

/// <summary>
/// Represents a collection of IP addresses within a specific IP network.
/// </summary>
public class IPAddressCollection : IEnumerable<IPAddress>, IEnumerator<IPAddress>
{
    private readonly IPNetwork2 ipnetwork;
    private readonly FilterEnum filter;
    private BigInteger enumerator;

    /// <summary>
    /// Initializes a new instance of the <see cref="IPAddressCollection"/> class.
    /// </summary>
    /// <param name="ipnetwork">The network.</param>
    /// <param name="filter">The filter.</param>
    internal IPAddressCollection(IPNetwork2 ipnetwork, FilterEnum filter)
    {
        this.ipnetwork = ipnetwork;
        this.filter = filter;
        this.Reset();
    }

    #region Count, Array, Enumerator

    /// <summary>
    /// Gets the count of IP addresses within the network.
    /// </summary>
    /// <value>
    /// The count of IP addresses within the network.
    /// </value>
    public BigInteger Count
    {
        get
        {
            BigInteger count = this.ipnetwork.Total;
            if (this.filter == FilterEnum.Usable)
            {
                count -= 2;
            }

            if (count < 0)
            {
                count = 0;
            }

            return count;
        }
    }

    /// <summary>
    /// Gets the IP address corresponding to the given index from the IPNetwork collection.
    /// </summary>
    /// <param name="i">The index of the IP address to retrieve.</param>
    /// <returns>The IP address corresponding to the given index.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the given index is greater than or equal to the Count property of the IPNetwork collection.</exception>
    public IPAddress this[BigInteger i]
    {
        get
        {
            if (i >= this.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(i));
            }

            byte width = this.ipnetwork.AddressFamily == Sockets.AddressFamily.InterNetwork ? (byte)32 : (byte)128;
            IPNetworkCollection ipn = this.ipnetwork.Subnet(width);

            BigInteger index = i;
            if (this.filter == FilterEnum.Usable)
            {
                index++;
            }

            return ipn[index].Network;
        }
    }

    #endregion

    #region Legacy Enumeration

    /// <summary>
    /// Gets the current <see cref="IPAddress"/> from the collection.
    /// </summary>
    /// <returns>The current <see cref="IPAddress"/>.</returns>
    public IPAddress Current
    {
        get
        {
            return this[this.enumerator];
        }
    }

    /// <inheritdoc/>
    object IEnumerator.Current
    {
        get
        {
            return this.Current;
        }
    }

    /// <inheritdoc />
    public bool MoveNext()
    {
        this.enumerator++;
        if (this.enumerator >= this.Count)
        {
            return false;
        }

        return true;
    }

    /// <inheritdoc />
    public void Reset()
    {
        this.enumerator = -1;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        // nothing to dispose
    }

    #endregion

    #region Enumeration

    /// <inheritdoc/>
    IEnumerator<IPAddress> IEnumerable<IPAddress>.GetEnumerator()
    {
        return new Enumerator(this);
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return new Enumerator(this);
    }

    private struct Enumerator : IEnumerator<IPAddress>
    {
        private readonly IPAddressCollection collection;
        private BigInteger enumerator;

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public IPAddress Current
        {
            get
            {
                return this.collection[this.enumerator];
            }
        }

        public void Dispose()
        {
            // nothing to dispose
        }

        public bool MoveNext()
        {
            this.enumerator++;
            if (this.enumerator >= this.collection.Count)
            {
                return false;
            }

            return true;
        }

        public void Reset()
        {
            this.enumerator = -1;
        }

        public Enumerator(IPAddressCollection collection)
        {
            this.collection = collection;
            this.enumerator = -1;
        }
    }

    #endregion
}