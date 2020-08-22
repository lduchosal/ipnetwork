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
        private readonly FilterEnum _filter;
        private BigInteger _enumerator;

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

        #region Legacy Enumeration
        public IPAddress Current {
            get {
                return this[_enumerator];
            }
        }

        object IEnumerator.Current {
            get {
                return Current;
            }
        }

        public bool MoveNext()
        {
            _enumerator++;
            if (_enumerator >= this.Count) {
                return false;
            }
            return true;
        }

        public void Reset()
        {
            _enumerator = -1;
        }

        public void Dispose()
        {
            // nothing to dispose
        }
        #endregion

        #region Enumeration
        IEnumerator<IPAddress> IEnumerable<IPAddress>.GetEnumerator() {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return new Enumerator(this);
        }

        struct Enumerator : IEnumerator<IPAddress>
        {
            private readonly IPAddressCollection _collection;
            private BigInteger _enumerator;

            object IEnumerator.Current {
                get {
                    return Current;
                }
            }

            public IPAddress Current {
                get {
                    return _collection[_enumerator];
                }
            }

            public void Dispose()
            {
                // nothing to dispose
            }

            public bool MoveNext()
            {
                _enumerator++;
                if (_enumerator >= _collection.Count) {
                    return false;
                }
                return true;
            }

            public void Reset()
            {
                _enumerator = -1;
            }

            public Enumerator(IPAddressCollection collection)
            {
                _collection = collection;
                _enumerator = -1;
            }
        }
        #endregion
    }
}
