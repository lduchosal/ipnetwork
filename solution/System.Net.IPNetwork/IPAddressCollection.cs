using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections;

namespace System.Net.IPNetwork {
    public class IPAddressCollection : IEnumerable<IPAddress>, IEnumerator<IPAddress> {

        private IPNetwork _ipnetwork;
        private double _enumerator;

        internal IPAddressCollection(IPNetwork ipnetwork) {
            this._ipnetwork = ipnetwork;
            this._enumerator = -1;
        }


        #region Count, Array, Enumerator

        public double Count {
            get {
                return this._ipnetwork.Total;
            }
        }

        public IPAddress this[double i] {
            get {
                if (i >= this.Count) {
                    throw new ArgumentOutOfRangeException("i");
                }

                IPNetworkCollection ipn = IPNetwork.Subnet(this._ipnetwork, 32);
                return ipn[i].Network;
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator<IPAddress> IEnumerable<IPAddress>.GetEnumerator() {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this;
        }

        #region IEnumerator<IPNetwork> Members

        public IPAddress Current {
            get { return this[this._enumerator]; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose() {
            // nothing to dispose
            return;
        }

        #endregion

        #region IEnumerator Members

        object IEnumerator.Current {
            get { return this.Current; }
        }

        public bool MoveNext() {
            this._enumerator++;
            if (this._enumerator >= this.Count) {
                return false;
            }
            return true;

        }

        public void Reset() {
            this._enumerator = -1;
        }

        #endregion

        #endregion
    }
}
