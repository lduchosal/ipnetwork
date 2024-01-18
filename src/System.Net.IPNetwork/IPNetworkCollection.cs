// <copyright file="IPNetworkCollection.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Numerics;

    /// <summary>
    /// A collection of IPNetwork2
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
        /// Count the nnumber of IPAddresses in a IPNetworkCollection
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

        IEnumerator<IPNetwork2> IEnumerable<IPNetwork2>.GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        #region IEnumerator<IPNetwork> Members

        /// <summary>
        /// Gets the current IEnumerator item
        /// </summary>
        public IPNetwork2 Current
        {
            get { return this[this._enumerator]; }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Dispose the IPNetwork instance
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
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
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
