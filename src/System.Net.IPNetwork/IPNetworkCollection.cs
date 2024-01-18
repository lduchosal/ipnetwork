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
                BigInteger count = BigInteger.Pow(2, this._cidrSubnet - this._cidr);
                return count;
            }
        }

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
        public void Dispose()
        {
            // nothing to dispose
            return;
        }

        #endregion

        #region IEnumerator Members

        object IEnumerator.Current
        {
            get { return this.Current; }
        }

        public bool MoveNext()
        {
            this._enumerator++;
            if (this._enumerator >= this.Count)
            {
                return false;
            }

            return true;
        }

        public void Reset()
        {
            this._enumerator = -1;
        }

        #endregion

        #endregion
    }
}
