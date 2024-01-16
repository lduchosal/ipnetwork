﻿// <copyright file="IPNetworkCollection.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Numerics;

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

        public IPNetwork2 Current
        {
            get { return this[this._enumerator]; }
        }

        #endregion

        #region IDisposable Members

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
