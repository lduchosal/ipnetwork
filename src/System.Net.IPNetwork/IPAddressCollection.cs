// <copyright file="IPAddressCollection.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Numerics;

    public enum FilterEnum
    {
        All,
        Usable,
    }

    /// <summary>
    /// A collection of IP Adresses
    /// </summary>
    public class IPAddressCollection : IEnumerable<IPAddress>, IEnumerator<IPAddress>
    {
        private readonly IPNetwork _ipnetwork;
        private readonly FilterEnum _filter;
        private BigInteger _enumerator;

        internal IPAddressCollection(IPNetwork ipnetwork, FilterEnum filter)
        {
            this._ipnetwork = ipnetwork;
            this._filter = filter;
            this.Reset();
        }

        #region Count, Array, Enumerator
        public BigInteger Count
        {
            get
            {
                BigInteger count = this._ipnetwork.Total;
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

        public IPAddress this[BigInteger i]
        {
            get
            {
                if (i >= this.Count)
                {
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
        public IPAddress Current
        {
            get
            {
                return this[this._enumerator];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
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

        public void Dispose()
        {
            // nothing to dispose
        }
        #endregion

        #region Enumeration
        IEnumerator<IPAddress> IEnumerable<IPAddress>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        private struct Enumerator : IEnumerator<IPAddress>
        {
            private readonly IPAddressCollection _collection;
            private BigInteger _enumerator;

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
                    return this._collection[this._enumerator];
                }
            }

            public void Dispose()
            {
                // nothing to dispose
            }

            public bool MoveNext()
            {
                this._enumerator++;
                if (this._enumerator >= this._collection.Count)
                {
                    return false;
                }

                return true;
            }

            public void Reset()
            {
                this._enumerator = -1;
            }

            public Enumerator(IPAddressCollection collection)
            {
                this._collection = collection;
                this._enumerator = -1;
            }
        }
        #endregion
    }
}
