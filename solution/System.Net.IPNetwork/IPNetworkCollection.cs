using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace System.Net.IPNetwork {
    public class IPNetworkCollection : IEnumerable<IPNetwork>, IEnumerator<IPNetwork> {


        private double _enumerator;
        private byte _cidrSubnet;
        private IPNetwork _ipnetwork;

        private byte _cidr {
            get { return this._ipnetwork.Cidr; }
        }
        private uint _broadcast {
            get { return IPNetwork.ToUint(this._ipnetwork.Broadcast); }
        }
        private uint _network {
            get { return IPNetwork.ToUint(this._ipnetwork.Network); }
        }

        internal IPNetworkCollection(IPNetwork ipnetwork, byte cidrSubnet) {

            if (cidrSubnet > 32) {
                throw new ArgumentOutOfRangeException("cidrSubnet");
            }

            if (cidrSubnet < ipnetwork.Cidr) {
                throw new ArgumentException("cidr");
            }

            this._cidrSubnet = cidrSubnet;
            this._ipnetwork = ipnetwork;
            this._enumerator = -1;
        }

        #region Count, Array, Enumerator

        public double Count
        {
            get
            {
                double count = Math.Pow(2, this._cidrSubnet - this._cidr);
                return count; 
            }
        }

        public IPNetwork this[double i] {
            get
            {
                if (i >= this.Count)
                {
                    throw new ArgumentOutOfRangeException("i");
                }
                double size = this.Count;
                int increment = (int)((this._broadcast - this._network) / size);
                uint uintNetwork = (uint)(this._network + ((increment + 1) * i));
                IPNetwork ipn = new IPNetwork(uintNetwork, this._cidrSubnet);
                return ipn;
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator<IPNetwork> IEnumerable<IPNetwork>.GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        #region IEnumerator<IPNetwork> Members

        public IPNetwork Current
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
