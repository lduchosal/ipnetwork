// <copyright file="IPNetworkCollection.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Numerics;

    /// <summary>
    /// Represents a collection of IP networks based on a given parent IP network and subnet CIDR.
    /// </summary>
    public class IPNetworkCollection : IEnumerable<IPNetwork2>, IEnumerator<IPNetwork2>
    {
        private BigInteger _enumerator;
        private byte _cidrSubnet;
        private IPNetwork2 _ipnetwork;

        private byte _cidr
        {
            get { return this._ipnetwork.Cidr; }
        }

        private BigInteger _broadcast
        {
            get { return IPNetwork2.ToBigInteger(this._ipnetwork.Broadcast); }
        }

        private BigInteger _lastUsable
        {
            get { return IPNetwork2.ToBigInteger(this._ipnetwork.LastUsable); }
        }

        private BigInteger _network
        {
            get { return IPNetwork2.ToBigInteger(this._ipnetwork.Network); }
        }

#if TRAVISCI
        public
#else
        /// <summary>
        /// Initializes a new instance of the <see cref="IPNetworkCollection"/> class.
        /// Represents a collection of IP networks based on a given parent IP network and subnet CIDR.
        /// </summary>
        /// <remarks>
        /// This class is used to generate a collection of IP networks by dividing the given parent IP network into subnets based on the provided subnet CIDR (Classless Inter-Domain Routing
        /// ) value.
        /// </remarks>
        internal
#endif
        IPNetworkCollection(IPNetwork2 ipnetwork, byte cidrSubnet)
        {
            int maxCidr = ipnetwork.AddressFamily == Sockets.AddressFamily.InterNetwork ? 32 : 128;
            if (cidrSubnet > maxCidr)
            {
                throw new ArgumentOutOfRangeException("cidrSubnet");
            }

            if (cidrSubnet < ipnetwork.Cidr)
            {
                throw new ArgumentException("cidrSubnet");
            }

            this._cidrSubnet = cidrSubnet;
            this._ipnetwork = ipnetwork;
            this._enumerator = -1;
        }

        #region Count, Array, Enumerator

        /// <summary>
        /// Gets the total number of IP addresses in the subnet.
        /// </summary>
        public BigInteger Count
        {
            get
            {
                var count = BigInteger.Pow(2, this._cidrSubnet - this._cidr);
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
                    throw new ArgumentOutOfRangeException("i");
                }

                BigInteger last = this._ipnetwork.AddressFamily == Sockets.AddressFamily.InterNetworkV6
                    ? this._lastUsable : this._broadcast;
                BigInteger increment = (last - this._network) / this.Count;
                BigInteger uintNetwork = this._network + ((increment + 1) * i);
                var ipn = new IPNetwork2(uintNetwork, this._ipnetwork.AddressFamily, this._cidrSubnet);
                return ipn;
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator<IPNetwork2> IEnumerable<IPNetwork2>.GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        #region IEnumerator<IPNetwork> Members

        /// <inheritdoc/>
        public IPNetwork2 Current
        {
            get { return this[this._enumerator]; }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Releases all resources used by the object.
        /// </summary>
        /// <remarks>
        /// This method implements the IDisposable interface and releases any resources
        /// held by the object. In this particular implementation, there are no resources
        /// to dispose of, so the method does nothing.
        /// </remarks>
        /// </summary>
        public void Dispose()
        {
            // nothing to dispose
            return;
        }

        #endregion

        #region IEnumerator Members
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
            this._enumerator++;
            if (this._enumerator >= this.Count)
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
            this._enumerator = -1;
        }

        #endregion

        #endregion
    }
}
