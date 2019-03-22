using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace System.Net
{

    public enum FilterEnum
    {
        All,
        Usable
    }

    public class IPAddressCollection : IEnumerable<IPAddress>, IEnumerator<IPAddress> {

        private readonly IPNetwork _ipnetwork;
        private BigInteger _enumerator;
        private readonly FilterEnum _filter;

        internal IPAddressCollection(IPNetwork ipnetwork, FilterEnum filter) {
            this._ipnetwork = ipnetwork;
            this._filter = filter;
            Reset();
        }


        #region Count, Array, Enumerator

        public BigInteger Count {
            get {

                BigInteger count = _ipnetwork.Total;
                if (this._filter == FilterEnum.Usable)
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

        public IPAddress this[BigInteger i] {
            get {
                if (i >= this.Count) {
                    throw new ArgumentOutOfRangeException("i");
                }
                byte width = this._ipnetwork.AddressFamily == Sockets.AddressFamily.InterNetwork ? (byte)32 : (byte)128;
                IPNetworkCollection ipn = this._ipnetwork.Subnet(width);

                BigInteger index = i;
                if (this._filter == FilterEnum.Usable)
                {
                    index++;
                }
                return ipn[index].Network;
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
