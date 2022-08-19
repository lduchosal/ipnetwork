// <copyright file="IPNetwork.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace System.Net
{
    /// <summary>
    /// IP Network utility class.
    /// Use IPNetwork.Parse to create instances.
    /// </summary>
    [Serializable]
    public sealed class IPNetwork : IComparable<IPNetwork>, ISerializable
    {
        #region properties

        private BigInteger _ipaddress;
        private AddressFamily _family;
        private byte _cidr;

        [DataMember(Name = "IPNetwork", IsRequired = true)]
        public string Value
        {
            get
            {
                return this.ToString();
            }

            set
            {
                var ipnetwork = IPNetwork.Parse(value);
                this._ipaddress = ipnetwork._ipaddress;
                this._family = ipnetwork._family;
                this._cidr = ipnetwork._cidr;
            }
        }

        #endregion

        #region accessors

        private BigInteger _network
        {
            get
            {
                BigInteger uintNetwork = this._ipaddress & this._netmask;
                return uintNetwork;
            }
        }

        /// <summary>
        /// Network address
        /// </summary>
        public IPAddress Network
        {
            get
            {
                return this._network.ToIPAddress(this._family);
            }
        }

        /// <summary>
        /// Address Family
        /// </summary>
        public AddressFamily AddressFamily
        {
            get
            {
                return this._family;
            }
        }

        private BigInteger _netmask
        {
            get
            {
                return this._cidr.ToUint(this._family);
            }
        }

        /// <summary>
        /// Netmask
        /// </summary>
        public IPAddress Netmask
        {
            get
            {
                return this._netmask.ToIPAddress(this._family);
            }
        }

        private BigInteger _broadcast
        {
            get
            {
                int width = this._family == Sockets.AddressFamily.InterNetwork ? 4 : 16;
                BigInteger uintBroadcast = this._network + this._netmask.PositiveReverse(width);
                return uintBroadcast;
            }
        }

        private static BigInteger CreateBroadcast(ref BigInteger network, BigInteger netmask, AddressFamily family)
        {
            int width = family == AddressFamily.InterNetwork ? 4 : 16;
            BigInteger uintBroadcast = network + netmask.PositiveReverse(width);

            return uintBroadcast;
        }

        /// <summary>
        /// Broadcast address
        /// </summary>
        public IPAddress Broadcast
        {
            get
            {
                if (this._family == Sockets.AddressFamily.InterNetworkV6)
                {
                    return null;
                }

                return this._broadcast.ToIPAddress(this._family);
            }
        }

        /// <summary>
        /// First usable IP adress in Network
        /// </summary>
        public IPAddress FirstUsable
        {
            get
            {
                BigInteger fisrt = this._family == Sockets.AddressFamily.InterNetworkV6
                    ? this._network
                    : (this.Usable <= 0) ? this._network : this._network + 1;
                return fisrt.ToIPAddress(this._family);
            }
        }

        /// <summary>
        /// Last usable IP adress in Network
        /// </summary>
        public IPAddress LastUsable
        {
            get
            {
                BigInteger last = this._family == Sockets.AddressFamily.InterNetworkV6
                    ? this._broadcast
                    : (this.Usable <= 0)
                    ? this._network
                    : this._broadcast - 1
                    ;

                return last.ToIPAddress(this._family);
            }
        }

        /// <summary>
        /// Number of usable IP adress in Network
        /// </summary>
        public BigInteger Usable
        {
            get
            {
                if (this._family == Sockets.AddressFamily.InterNetworkV6)
                {
                    return this.Total;
                }

                byte[] mask = new byte[] { 0xff, 0xff, 0xff, 0xff, 0x00 };
                BigInteger bmask = new BigInteger(mask);
                BigInteger usableIps = (_cidr > 30) ? 0 : ((bmask >> _cidr) - 1);
                return usableIps;
            }
        }

        /// <summary>
        /// Number of IP adress in Network
        /// </summary>
        public BigInteger Total
        {
            get
            {
                int max = this._family == Sockets.AddressFamily.InterNetwork ? 32 : 128;
                BigInteger count = BigInteger.Pow(2, max - _cidr);
                return count;
            }
        }

        /// <summary>
        /// The CIDR netmask notation
        /// </summary>
        public byte Cidr
        {
            get
            {
                return this._cidr;
            }
        }

        #endregion

        #region constructor

#if TRAVISCI
        public
#else
        internal
#endif
            IPNetwork(BigInteger ipaddress, AddressFamily family, byte cidr)
        {
            Init(ipaddress, family, cidr);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IPNetwork"/> class.
        /// Creates a new IPNetwork
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="cidr"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public IPNetwork(IPAddress ipaddress, byte cidr)
        {
            if (ipaddress == null)
            {
                throw new ArgumentNullException(nameof(ipaddress));
            }

            var uintIpAddress = ipaddress.ToBigInteger();

            Init(uintIpAddress, ipaddress.AddressFamily, cidr);
        }

        private void Init(BigInteger ipaddress, AddressFamily family, byte cidr)
        {
            var maxCidr = family == AddressFamily.InterNetwork ? 32 : 128;
            if (cidr > maxCidr)
            {
                throw new ArgumentOutOfRangeException("cidr");
            }

            _ipaddress = ipaddress;
            _family = family;
            _cidr = cidr;
        }

        #endregion

        #region parsers

        /// <summary>
        /// 192.168.168.100 - 255.255.255.0
        ///
        /// ```
        /// Network   : 192.168.168.0
        /// Netmask   : 255.255.255.0
        /// Cidr      : 24
        /// Start     : 192.168.168.1
        /// End       : 192.168.168.254
        /// Broadcast : 192.168.168.255
        /// ```
        ///
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="netmask"></param>
        /// <returns></returns>
        public static IPNetwork Parse(string ipaddress, string netmask)
        {
            IPNetwork.InternalParse(false, ipaddress, netmask, out var ipnetwork);
            return ipnetwork;
        }

        /// <summary>
        /// 192.168.168.100/24
        ///
        /// Network   : 192.168.168.0
        /// Netmask   : 255.255.255.0
        /// Cidr      : 24
        /// Start     : 192.168.168.1
        /// End       : 192.168.168.254
        /// Broadcast : 192.168.168.255
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="cidr"></param>
        /// <returns></returns>
        public static IPNetwork Parse(string ipaddress, byte cidr)
        {
            IPNetwork.InternalParse(false, ipaddress, cidr, out var ipnetwork);
            return ipnetwork;
        }

        /// <summary>
        /// 192.168.168.100 255.255.255.0
        ///
        /// Network   : 192.168.168.0
        /// Netmask   : 255.255.255.0
        /// Cidr      : 24
        /// Start     : 192.168.168.1
        /// End       : 192.168.168.254
        /// Broadcast : 192.168.168.255
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="netmask"></param>
        /// <returns></returns>
        public static IPNetwork Parse(IPAddress ipaddress, IPAddress netmask)
        {
            IPNetwork.InternalParse(false, ipaddress, netmask, out var ipnetwork);
            return ipnetwork;
        }

        /// <summary>
        /// 192.168.0.1/24
        /// 192.168.0.1 255.255.255.0
        ///
        /// Network   : 192.168.0.0
        /// Netmask   : 255.255.255.0
        /// Cidr      : 24
        /// Start     : 192.168.0.1
        /// End       : 192.168.0.254
        /// Broadcast : 192.168.0.255
        /// </summary>
        /// <param name="network"></param>
        /// <param name="sanitanize"></param>
        /// <returns></returns>
        public static IPNetwork Parse(string network, bool sanitanize = true)
        {
            IPNetwork.InternalParse(false, network, CidrGuess.ClassFull, sanitanize, out var ipnetwork);
            return ipnetwork;
        }

        /// <summary>
        /// 192.168.0.1/24
        /// 192.168.0.1 255.255.255.0
        ///
        /// Network   : 192.168.0.0
        /// Netmask   : 255.255.255.0
        /// Cidr      : 24
        /// Start     : 192.168.0.1
        /// End       : 192.168.0.254
        /// Broadcast : 192.168.0.255
        /// </summary>
        /// <param name="network"></param>
        /// <param name="cidrGuess"></param>
        /// <param name="sanitanize"></param>
        /// <returns></returns>
        public static IPNetwork Parse(string network, ICidrGuess cidrGuess, bool sanitanize = true)
        {
            IPNetwork.InternalParse(false, network, cidrGuess, sanitanize, out var ipnetwork);
            return ipnetwork;
        }

        #endregion

        #region TryParse

        /// <summary>
        /// 192.168.168.100 - 255.255.255.0
        ///
        /// Network   : 192.168.168.0
        /// Netmask   : 255.255.255.0
        /// Cidr      : 24
        /// Start     : 192.168.168.1
        /// End       : 192.168.168.254
        /// Broadcast : 192.168.168.255
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="netmask"></param>
        /// <param name="ipnetwork"></param>
        /// <returns></returns>
        public static bool TryParse(string ipaddress, string netmask, out IPNetwork ipnetwork)
        {
            IPNetwork.InternalParse(true, ipaddress, netmask, out var ipnetwork2);
            bool parsed = ipnetwork2 != null;
            ipnetwork = ipnetwork2;

            return parsed;
        }

        /// <summary>
        /// 192.168.168.100/24
        ///
        /// Network   : 192.168.168.0
        /// Netmask   : 255.255.255.0
        /// Cidr      : 24
        /// Start     : 192.168.168.1
        /// End       : 192.168.168.254
        /// Broadcast : 192.168.168.255
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="cidr"></param>
        /// <param name="ipnetwork"></param>
        /// <returns></returns>
        public static bool TryParse(string ipaddress, byte cidr, out IPNetwork ipnetwork)
        {
            IPNetwork.InternalParse(true, ipaddress, cidr, out var ipnetwork2);
            bool parsed = ipnetwork2 != null;
            ipnetwork = ipnetwork2;

            return parsed;
        }

        /// <summary>
        /// 192.168.0.1/24
        /// 192.168.0.1 255.255.255.0
        ///
        /// Network   : 192.168.0.0
        /// Netmask   : 255.255.255.0
        /// Cidr      : 24
        /// Start     : 192.168.0.1
        /// End       : 192.168.0.254
        /// Broadcast : 192.168.0.255
        /// </summary>
        /// <param name="network"></param>
        /// <param name="ipnetwork"></param>
        /// <returns></returns>
        public static bool TryParse(string network, out IPNetwork ipnetwork)
        {
            bool sanitanize = true;
            IPNetwork.InternalParse(true, network, CidrGuess.ClassFull, sanitanize, out var ipnetwork2);
            bool parsed = ipnetwork2 != null;
            ipnetwork = ipnetwork2;

            return parsed;
        }

        /// <summary>
        /// 192.168.0.1/24
        /// 192.168.0.1 255.255.255.0
        ///
        /// Network   : 192.168.0.0
        /// Netmask   : 255.255.255.0
        /// Cidr      : 24
        /// Start     : 192.168.0.1
        /// End       : 192.168.0.254
        /// Broadcast : 192.168.0.255
        /// </summary>
        /// <param name="network"></param>
        /// <param name="sanitanize"></param>
        /// <param name="ipnetwork"></param>
        /// <returns></returns>
        public static bool TryParse(string network, bool sanitanize, out IPNetwork ipnetwork)
        {
            IPNetwork.InternalParse(true, network, CidrGuess.ClassFull, sanitanize, out var ipnetwork2);
            bool parsed = ipnetwork2 != null;
            ipnetwork = ipnetwork2;

            return parsed;
        }

        /// <summary>
        /// 192.168.0.1/24
        /// 192.168.0.1 255.255.255.0
        ///
        /// Network   : 192.168.0.0
        /// Netmask   : 255.255.255.0
        /// Cidr      : 24
        /// Start     : 192.168.0.1
        /// End       : 192.168.0.254
        /// Broadcast : 192.168.0.255
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="netmask"></param>
        /// <param name="ipnetwork"></param>
        /// <returns></returns>
        public static bool TryParse(IPAddress ipaddress, IPAddress netmask, out IPNetwork ipnetwork)
        {
            IPNetwork.InternalParse(true, ipaddress, netmask, out var ipnetwork2);
            bool parsed = ipnetwork2 != null;
            ipnetwork = ipnetwork2;

            return parsed;
        }

        #endregion

        #region InternalParse

        /// <summary>
        /// 192.168.168.100 - 255.255.255.0
        ///
        /// Network   : 192.168.168.0
        /// Netmask   : 255.255.255.0
        /// Cidr      : 24
        /// Start     : 192.168.168.1
        /// End       : 192.168.168.254
        /// Broadcast : 192.168.168.255
        /// </summary>
        /// <param name="tryParse"></param>
        /// <param name="ipaddress"></param>
        /// <param name="netmask"></param>
        /// <param name="ipnetwork"></param>
        /// <returns></returns>
        private static void InternalParse(bool tryParse, string ipaddress, string netmask, out IPNetwork ipnetwork)
        {
            if (string.IsNullOrEmpty(ipaddress))
            {
                if (tryParse == false)
                {
                    throw new ArgumentNullException("ipaddress");
                }

                ipnetwork = null;
                return;
            }

            if (string.IsNullOrEmpty(netmask))
            {
                if (tryParse == false)
                {
                    throw new ArgumentNullException("netmask");
                }

                ipnetwork = null;
                return;
            }

            bool ipaddressParsed = IPAddress.TryParse(ipaddress, out var ip);
            if (ipaddressParsed == false)
            {
                if (tryParse == false)
                {
                    throw new ArgumentException("ipaddress");
                }

                ipnetwork = null;
                return;
            }

            bool netmaskParsed = IPAddress.TryParse(netmask, out var mask);
            if (netmaskParsed == false)
            {
                if (tryParse == false)
                {
                    throw new ArgumentException("netmask");
                }

                ipnetwork = null;
                return;
            }

            IPNetwork.InternalParse(tryParse, ip, mask, out ipnetwork);
        }

        private static void InternalParse(bool tryParse, string network, ICidrGuess cidrGuess, bool sanitanize, out IPNetwork ipnetwork)
        {
            if (string.IsNullOrEmpty(network))
            {
                if (tryParse == false)
                {
                    throw new ArgumentNullException("network");
                }

                ipnetwork = null;
                return;
            }

            if (sanitanize)
            {
                network = Regex.Replace(network, @"[^0-9a-fA-F\.\/\s\:]+", string.Empty);
                network = Regex.Replace(network, @"\s{2,}", " ");
                network = network.Trim();
            }

            var splitOptions = sanitanize ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
            string[] args = network.Split(new char[] { ' ', '/' }, splitOptions);
            byte cidr = 0;

            if (args.Length == 1)
            {
                string cidrlessNetwork = args[0];
                if (cidrGuess.TryGuessCidr(cidrlessNetwork, out cidr))
                {
                    IPNetwork.InternalParse(tryParse, cidrlessNetwork, cidr, out ipnetwork);
                    return;
                }

                if (tryParse == false)
                {
                    throw new ArgumentException("network");
                }

                ipnetwork = null;
                return;
            }

            if (byte.TryParse(args[1], out cidr))
            {
                IPNetwork.InternalParse(tryParse, args[0], cidr, out ipnetwork);
                return;
            }

            IPNetwork.InternalParse(tryParse, args[0], args[1], out ipnetwork);
            return;
        }

        /// <summary>
        /// 192.168.168.100 255.255.255.0
        ///
        /// Network   : 192.168.168.0
        /// Netmask   : 255.255.255.0
        /// Cidr      : 24
        /// Start     : 192.168.168.1
        /// End       : 192.168.168.254
        /// Broadcast : 192.168.168.255
        /// </summary>
        /// <param name="tryParse"></param>
        /// <param name="ipaddress"></param>
        /// <param name="netmask"></param>
        /// <param name="ipnetwork"></param>
        /// <returns></returns>
        private static void InternalParse(bool tryParse, IPAddress ipaddress, IPAddress netmask, out IPNetwork ipnetwork)
        {
            if (ipaddress == null)
            {
                if (tryParse == false)
                {
                    throw new ArgumentNullException("ipaddress");
                }

                ipnetwork = null;
                return;
            }

            if (netmask == null)
            {
                if (tryParse == false)
                {
                    throw new ArgumentNullException("netmask");
                }

                ipnetwork = null;
                return;
            }

            BigInteger uintIpAddress = ipaddress.ToBigInteger();
            bool parsed = netmask.TryToCidr(out var cidr2);
            if (parsed == false)
            {
                if (tryParse == false)
                {
                    throw new ArgumentException("netmask");
                }

                ipnetwork = null;
                return;
            }

            byte cidr = (byte)cidr2;

            IPNetwork ipnet = new IPNetwork(uintIpAddress, ipaddress.AddressFamily, cidr);
            ipnetwork = ipnet;

            return;
        }

        /// <summary>
        /// 192.168.168.100/24
        ///
        /// Network   : 192.168.168.0
        /// Netmask   : 255.255.255.0
        /// Cidr      : 24
        /// Start     : 192.168.168.1
        /// End       : 192.168.168.254
        /// Broadcast : 192.168.168.255
        /// </summary>
        /// <param name="tryParse"></param>
        /// <param name="ipaddress"></param>
        /// <param name="cidr"></param>
        /// <param name="ipnetwork"></param>
        /// <returns></returns>
        private static void InternalParse(bool tryParse, string ipaddress, byte cidr, out IPNetwork ipnetwork)
        {
            if (string.IsNullOrEmpty(ipaddress))
            {
                if (tryParse == false)
                {
                    throw new ArgumentNullException("ipaddress");
                }

                ipnetwork = null;
                return;
            }

            bool ipaddressParsed = IPAddress.TryParse(ipaddress, out var ip);
            if (ipaddressParsed == false)
            {
                if (tryParse == false)
                {
                    throw new ArgumentException("ipaddress");
                }

                ipnetwork = null;
                return;
            }

            bool parsedNetmask = cidr.TryToNetmask(ip.AddressFamily, out var mask);
            if (parsedNetmask == false)
            {
                if (tryParse == false)
                {
                    throw new ArgumentException("cidr");
                }

                ipnetwork = null;
                return;
            }

            IPNetwork.InternalParse(tryParse, ip, mask, out ipnetwork);
        }
        #endregion

        #region utils

        #region ValidNetmask

        /// <summary>
        /// return true if netmask is a valid netmask
        /// 255.255.255.0, 255.0.0.0, 255.255.240.0, ...
        /// </summary>
        /// <see href="http://www.actionsnip.com/snippets/tomo_atlacatl/calculate-if-a-netmask-is-valid--as2-"/>
        /// <param name="netmask"></param>
        /// <returns></returns>
        public static bool ValidNetmask(IPAddress netmask)
        {
            if (netmask == null)
            {
                throw new ArgumentNullException("netmask");
            }

            BigInteger uintNetmask = netmask.ToBigInteger();
            bool valid = IPNetwork.InternalValidNetmask(uintNetmask, netmask.AddressFamily);

            return valid;
        }

#if TRAVISCI
        public
#else
        internal
#endif
            static bool InternalValidNetmask(BigInteger netmask, AddressFamily family)
        {
            if (family != AddressFamily.InterNetwork
                && family != AddressFamily.InterNetworkV6)
            {
                throw new ArgumentException("family");
            }

            var mask = family == AddressFamily.InterNetwork
                ? new BigInteger(0x0ffffffff)
                : new BigInteger(new byte[]
                {
                    0xff, 0xff, 0xff, 0xff,
                    0xff, 0xff, 0xff, 0xff,
                    0xff, 0xff, 0xff, 0xff,
                    0xff, 0xff, 0xff, 0xff,
                    0x00,
                });

            BigInteger neg = (~netmask) & mask;
            bool isNetmask = ((neg + 1) & neg) == 0;

            return isNetmask;
        }

        #endregion

        #endregion

        #region contains

        /// <summary>
        /// return true if ipaddress is contained in network
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <returns></returns>
        public bool Contains(IPAddress ipaddress)
        {
            if (ipaddress == null)
            {
                throw new ArgumentNullException("ipaddress");
            }

            if (AddressFamily != ipaddress.AddressFamily)
            {
                return false;
            }

            BigInteger uintNetwork = _network;
            BigInteger uintBroadcast = CreateBroadcast(ref uintNetwork, this._netmask, this._family);
            BigInteger uintAddress = ipaddress.ToBigInteger();

            bool contains = uintAddress >= uintNetwork
                && uintAddress <= uintBroadcast;

            return contains;
        }

        [Obsolete("static Contains is deprecated, please use instance Contains.")]
        public static bool Contains(IPNetwork network, IPAddress ipaddress)
        {
            if (network == null)
            {
                throw new ArgumentNullException("network");
            }

            return network.Contains(ipaddress);
        }

        /// <summary>
        /// return true is network2 is fully contained in network
        /// </summary>
        /// <param name="network2"></param>
        /// <returns></returns>
        public bool Contains(IPNetwork network2)
        {
            if (network2 == null)
            {
                throw new ArgumentNullException("network2");
            }

            BigInteger uintNetwork = _network;
            BigInteger uintBroadcast = CreateBroadcast(ref uintNetwork, this._netmask, this._family);

            BigInteger uintFirst = network2._network;
            BigInteger uintLast = CreateBroadcast(ref uintFirst, network2._netmask, network2._family);

            bool contains = uintFirst >= uintNetwork
                && uintLast <= uintBroadcast;

            return contains;
        }

        [Obsolete("static Contains is deprecated, please use instance Contains.")]
        public static bool Contains(IPNetwork network, IPNetwork network2)
        {
            if (network == null)
            {
                throw new ArgumentNullException("network");
            }

            return network.Contains(network2);
        }

        #endregion

        #region overlap

        /// <summary>
        /// return true is network2 overlap network
        /// </summary>
        /// <param name="network2"></param>
        /// <returns></returns>
        public bool Overlap(IPNetwork network2)
        {
            if (network2 == null)
            {
                throw new ArgumentNullException("network2");
            }

            BigInteger uintNetwork = _network;
            BigInteger uintBroadcast = _broadcast;

            BigInteger uintFirst = network2._network;
            BigInteger uintLast = network2._broadcast;

            bool overlap =
                (uintFirst >= uintNetwork && uintFirst <= uintBroadcast)
                || (uintLast >= uintNetwork && uintLast <= uintBroadcast)
                || (uintFirst <= uintNetwork && uintLast >= uintBroadcast)
                || (uintFirst >= uintNetwork && uintLast <= uintBroadcast);

            return overlap;
        }

        [Obsolete("static Overlap is deprecated, please use instance Overlap.")]
        public static bool Overlap(IPNetwork network, IPNetwork network2)
        {
            if (network == null)
            {
                throw new ArgumentNullException("network");
            }

            return network.Overlap(network2);
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return string.Format("{0}/{1}", this.Network, this.Cidr);
        }

        #endregion

        #region IANA block

        private static readonly Lazy<IPNetwork> _iana_ablock_reserved = new Lazy<IPNetwork>(() => IPNetwork.Parse("10.0.0.0/8"));
        private static readonly Lazy<IPNetwork> _iana_bblock_reserved = new Lazy<IPNetwork>(() => IPNetwork.Parse("172.16.0.0/12"));
        private static readonly Lazy<IPNetwork> _iana_cblock_reserved = new Lazy<IPNetwork>(() => IPNetwork.Parse("192.168.0.0/16"));

        /// <summary>
        /// 10.0.0.0/8
        /// </summary>
        /// <returns></returns>
        public static IPNetwork IANA_ABLK_RESERVED1
        {
            get
            {
                return _iana_ablock_reserved.Value;
            }
        }

        /// <summary>
        /// 172.12.0.0/12
        /// </summary>
        /// <returns></returns>
        public static IPNetwork IANA_BBLK_RESERVED1
        {
            get
            {
                return _iana_bblock_reserved.Value;
            }
        }

        /// <summary>
        /// 192.168.0.0/16
        /// </summary>
        /// <returns></returns>
        public static IPNetwork IANA_CBLK_RESERVED1
        {
            get
            {
                return _iana_cblock_reserved.Value;
            }
        }

        /// <summary>
        /// return true if ipaddress is contained in
        /// IANA_ABLK_RESERVED1, IANA_BBLK_RESERVED1, IANA_CBLK_RESERVED1
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <returns></returns>
        public static bool IsIANAReserved(IPAddress ipaddress)
        {
            if (ipaddress == null)
            {
                throw new ArgumentNullException("ipaddress");
            }

            return IPNetwork.IANA_ABLK_RESERVED1.Contains(ipaddress)
                || IPNetwork.IANA_BBLK_RESERVED1.Contains(ipaddress)
                || IPNetwork.IANA_CBLK_RESERVED1.Contains(ipaddress);
        }

        /// <summary>
        /// return true if ipnetwork is contained in
        /// IANA_ABLK_RESERVED1, IANA_BBLK_RESERVED1, IANA_CBLK_RESERVED1
        /// </summary>
        /// <returns></returns>
        public bool IsIANAReserved()
        {
            return IPNetwork.IANA_ABLK_RESERVED1.Contains(this)
                || IPNetwork.IANA_BBLK_RESERVED1.Contains(this)
                || IPNetwork.IANA_CBLK_RESERVED1.Contains(this);
        }

        [Obsolete("static IsIANAReserved is deprecated, please use instance IsIANAReserved.")]
        public static bool IsIANAReserved(IPNetwork ipnetwork)
        {
            if (ipnetwork == null)
            {
                throw new ArgumentNullException("ipnetwork");
            }

            return ipnetwork.IsIANAReserved();
        }

        #endregion

        #region Subnet

        /// <summary>
        /// Subnet a network into multiple nets of cidr mask
        /// Subnet 192.168.0.0/24 into cidr 25 gives 192.168.0.0/25, 192.168.0.128/25
        /// Subnet 10.0.0.0/8 into cidr 9 gives 10.0.0.0/9, 10.128.0.0/9
        /// </summary>
        /// <param name="cidr"></param>
        /// <returns></returns>
        public IPNetworkCollection Subnet(byte cidr)
        {
            IPNetwork.InternalSubnet(false, this, cidr, out var ipnetworkCollection);

            return ipnetworkCollection;
        }

        [Obsolete("static Subnet is deprecated, please use instance Subnet.")]
        public static IPNetworkCollection Subnet(IPNetwork network, byte cidr)
        {
            if (network == null)
            {
                throw new ArgumentNullException("network");
            }

            return network.Subnet(cidr);
        }

        /// <summary>
        /// Subnet a network into multiple nets of cidr mask
        /// Subnet 192.168.0.0/24 into cidr 25 gives 192.168.0.0/25, 192.168.0.128/25
        /// Subnet 10.0.0.0/8 into cidr 9 gives 10.0.0.0/9, 10.128.0.0/9
        /// </summary>
        /// <param name="cidr"></param>
        /// <param name="ipnetworkCollection"></param>
        /// <returns></returns>
        public bool TrySubnet(byte cidr, out IPNetworkCollection ipnetworkCollection)
        {
            IPNetwork.InternalSubnet(true, this, cidr, out var inc);
            if (inc == null)
            {
                ipnetworkCollection = null;
                return false;
            }

            ipnetworkCollection = inc;
            return true;
        }

        [Obsolete("static TrySubnet is deprecated, please use instance TrySubnet.")]
        public static bool TrySubnet(IPNetwork network, byte cidr, out IPNetworkCollection ipnetworkCollection)
        {
            if (network == null)
            {
                throw new ArgumentNullException("network");
            }

            return network.TrySubnet(cidr, out ipnetworkCollection);
        }

#if TRAVISCI
        public
#else
        internal
#endif
            static void InternalSubnet(bool trySubnet, IPNetwork network, byte cidr, out IPNetworkCollection ipnetworkCollection)
        {
            if (network == null)
            {
                if (trySubnet == false)
                {
                    throw new ArgumentNullException("network");
                }

                ipnetworkCollection = null;
                return;
            }

            int maxCidr = network._family == Sockets.AddressFamily.InterNetwork ? 32 : 128;
            if (cidr > maxCidr)
            {
                if (trySubnet == false)
                {
                    throw new ArgumentOutOfRangeException("cidr");
                }

                ipnetworkCollection = null;
                return;
            }

            if (cidr < network.Cidr)
            {
                if (trySubnet == false)
                {
                    throw new ArgumentException("cidr");
                }

                ipnetworkCollection = null;
                return;
            }

            ipnetworkCollection = new IPNetworkCollection(network, cidr);
            return;
        }

        #endregion

        #region Supernet

        /// <summary>
        /// Supernet two consecutive cidr equal subnet into a single one
        /// 192.168.0.0/24 + 192.168.1.0/24 = 192.168.0.0/23
        /// 10.1.0.0/16 + 10.0.0.0/16 = 10.0.0.0/15
        /// 192.168.0.0/24 + 192.168.0.0/25 = 192.168.0.0/24
        /// </summary>
        /// <param name="network2"></param>
        /// <returns></returns>
        public IPNetwork Supernet(IPNetwork network2)
        {
            IPNetwork.InternalSupernet(false, this, network2, out var supernet);
            return supernet;
        }

        [Obsolete("static Supernet is deprecated, please use instance Supernet.")]
        public static IPNetwork Supernet(IPNetwork network, IPNetwork network2)
        {
            return network.Supernet(network2);
        }

        /// <summary>
        /// Try to supernet two consecutive cidr equal subnet into a single one
        /// 192.168.0.0/24 + 192.168.1.0/24 = 192.168.0.0/23
        /// 10.1.0.0/16 + 10.0.0.0/16 = 10.0.0.0/15
        /// 192.168.0.0/24 + 192.168.0.0/25 = 192.168.0.0/24
        /// </summary>
        /// <param name="network2"></param>
        /// <param name="supernet"></param>
        /// <returns></returns>
        public bool TrySupernet(IPNetwork network2, out IPNetwork supernet)
        {
            IPNetwork.InternalSupernet(true, this, network2, out var outSupernet);
            bool parsed = outSupernet != null;
            supernet = outSupernet;
            return parsed;
        }

        [Obsolete("static TrySupernet is deprecated, please use instance TrySupernet.")]
        public static bool TrySupernet(IPNetwork network, IPNetwork network2, out IPNetwork supernet)
        {
            if (network == null)
            {
                throw new ArgumentNullException("network");
            }

            return network.TrySupernet(network2, out supernet);
        }

#if TRAVISCI
        public
#else
        internal
#endif
            static void InternalSupernet(bool trySupernet, IPNetwork network1, IPNetwork network2, out IPNetwork supernet)
        {
            if (network1 == null)
            {
                if (trySupernet == false)
                {
                    throw new ArgumentNullException("network1");
                }

                supernet = null;
                return;
            }

            if (network2 == null)
            {
                if (trySupernet == false)
                {
                    throw new ArgumentNullException("network2");
                }

                supernet = null;
                return;
            }

            if (network1.Contains(network2))
            {
                supernet = new IPNetwork(network1._network, network1._family, network1.Cidr);
                return;
            }

            if (network2.Contains(network1))
            {
                supernet = new IPNetwork(network2._network, network2._family, network2.Cidr);
                return;
            }

            if (network1._cidr != network2._cidr)
            {
                if (trySupernet == false)
                {
                    throw new ArgumentException("cidr");
                }

                supernet = null;
                return;
            }

            IPNetwork first = (network1._network < network2._network) ? network1 : network2;
            IPNetwork last = (network1._network > network2._network) ? network1 : network2;

            // Starting from here :
            // network1 and network2 have the same cidr,
            // network1 does not contain network2,
            // network2 does not contain network1,
            // first is the lower subnet
            // last is the higher subnet
            if ((first._broadcast + 1) != last._network)
            {
                if (trySupernet == false)
                {
                    throw new ArgumentOutOfRangeException("network");
                }

                supernet = null;
                return;
            }

            BigInteger uintSupernet = first._network;
            byte cidrSupernet = (byte)(first._cidr - 1);

            IPNetwork networkSupernet = new IPNetwork(uintSupernet, first._family, cidrSupernet);
            if (networkSupernet._network != first._network)
            {
                if (trySupernet == false)
                {
                    throw new ArgumentException("network");
                }

                supernet = null;
                return;
            }

            supernet = networkSupernet;
            return;
        }

        #endregion

        #region GetHashCode

        public override int GetHashCode()
        {
            return string.Format(
                "{0}|{1}|{2}",
                this._family.GetHashCode(),
                this._network.GetHashCode(),
                this._cidr.GetHashCode()).GetHashCode();
        }

        #endregion

        #region SupernetArray

        /// <summary>
        /// Supernet a list of subnet
        /// 192.168.0.0/24 + 192.168.1.0/24 = 192.168.0.0/23
        /// 192.168.0.0/24 + 192.168.1.0/24 + 192.168.2.0/24 + 192.168.3.0/24 = 192.168.0.0/22
        /// </summary>
        /// <param name="ipnetworks"></param>
        /// <returns></returns>
        public static IPNetwork[] Supernet(IPNetwork[] ipnetworks)
        {
            InternalSupernet(false, ipnetworks, out var supernet);
            return supernet;
        }

        /// <summary>
        /// Supernet a list of subnet
        /// 192.168.0.0/24 + 192.168.1.0/24 = 192.168.0.0/23
        /// 192.168.0.0/24 + 192.168.1.0/24 + 192.168.2.0/24 + 192.168.3.0/24 = 192.168.0.0/22
        /// </summary>
        /// <param name="ipnetworks"></param>
        /// <param name="supernet"></param>
        /// <returns></returns>
        public static bool TrySupernet(IPNetwork[] ipnetworks, out IPNetwork[] supernet)
        {
            bool supernetted = InternalSupernet(true, ipnetworks, out supernet);
            return supernetted;
        }

#if TRAVISCI
        public
#else
        internal
#endif
        static bool InternalSupernet(bool trySupernet, IPNetwork[] ipnetworks, out IPNetwork[] supernet)
        {
            if (ipnetworks == null)
            {
                if (trySupernet == false)
                {
                    throw new ArgumentNullException("ipnetworks");
                }

                supernet = null;
                return false;
            }

            if (ipnetworks.Length <= 0)
            {
                supernet = new IPNetwork[0];
                return true;
            }

            List<IPNetwork> supernetted = new List<IPNetwork>();
            List<IPNetwork> ipns = IPNetwork.Array2List(ipnetworks);
            Stack<IPNetwork> current = IPNetwork.List2Stack(ipns);
            int previousCount = 0;
            int currentCount = current.Count;

            while (previousCount != currentCount)
            {
                supernetted.Clear();
                while (current.Count > 1)
                {
                    IPNetwork ipn1 = current.Pop();
                    IPNetwork ipn2 = current.Peek();

                    bool success = ipn1.TrySupernet(ipn2, out var outNetwork);
                    if (success)
                    {
                        current.Pop();
                        current.Push(outNetwork);
                    }
                    else
                    {
                        supernetted.Add(ipn1);
                    }
                }

                if (current.Count == 1)
                {
                    supernetted.Add(current.Pop());
                }

                previousCount = currentCount;
                currentCount = supernetted.Count;
                current = IPNetwork.List2Stack(supernetted);
            }

            supernet = supernetted.ToArray();
            return true;
        }

        private static Stack<IPNetwork> List2Stack(List<IPNetwork> list)
        {
            Stack<IPNetwork> stack = new Stack<IPNetwork>();
            list.ForEach(new Action<IPNetwork>(
                delegate(IPNetwork ipn)
                {
                    stack.Push(ipn);
                }));
            return stack;
        }

        private static List<IPNetwork> Array2List(IPNetwork[] array)
        {
            List<IPNetwork> ipns = new List<IPNetwork>();
            ipns.AddRange(array);
            IPNetwork.RemoveNull(ipns);
            ipns.Sort(new Comparison<IPNetwork>(
                delegate(IPNetwork ipn1, IPNetwork ipn2)
                {
                    int networkCompare = ipn1._network.CompareTo(ipn2._network);
                    if (networkCompare == 0)
                    {
                        int cidrCompare = ipn1._cidr.CompareTo(ipn2._cidr);
                        return cidrCompare;
                    }

                    return networkCompare;
                }));
            ipns.Reverse();

            return ipns;
        }

        private static void RemoveNull(List<IPNetwork> ipns)
        {
            ipns.RemoveAll(new Predicate<IPNetwork>(
                delegate(IPNetwork ipn)
                {
                    if (ipn == null)
                    {
                        return true;
                    }

                    return false;
                }));
        }

        #endregion

        #region WideSubnet

        public static IPNetwork WideSubnet(string start, string end)
        {
            if (string.IsNullOrEmpty(start))
            {
                throw new ArgumentNullException("start");
            }

            if (string.IsNullOrEmpty(end))
            {
                throw new ArgumentNullException("end");
            }

            if (!IPAddress.TryParse(start, out var startIP))
            {
                throw new ArgumentException("start");
            }

            if (!IPAddress.TryParse(end, out var endIP))
            {
                throw new ArgumentException("end");
            }

            if (startIP.AddressFamily != endIP.AddressFamily)
            {
                throw new NotSupportedException("MixedAddressFamily");
            }

            IPNetwork ipnetwork = new IPNetwork(0, startIP.AddressFamily, 0);
            for (byte cidr = 32; cidr >= 0; cidr--)
            {
                IPNetwork wideSubnet = IPNetwork.Parse(start, cidr);
                if (wideSubnet.Contains(endIP))
                {
                    ipnetwork = wideSubnet;
                    break;
                }
            }

            return ipnetwork;
        }

        public static bool TryWideSubnet(IPNetwork[] ipnetworks, out IPNetwork ipnetwork)
        {
            IPNetwork.InternalWideSubnet(true, ipnetworks, out var ipn);
            if (ipn == null)
            {
                ipnetwork = null;
                return false;
            }

            ipnetwork = ipn;

            return true;
        }

        public static IPNetwork WideSubnet(IPNetwork[] ipnetworks)
        {
            IPNetwork.InternalWideSubnet(false, ipnetworks, out var ipn);
            return ipn;
        }

        internal static void InternalWideSubnet(bool tryWide, IPNetwork[] ipnetworks, out IPNetwork ipnetwork)
        {
            if (ipnetworks == null)
            {
                if (tryWide == false)
                {
                    throw new ArgumentNullException("ipnetworks");
                }

                ipnetwork = null;
                return;
            }

            IPNetwork[] nnin = Array.FindAll<IPNetwork>(ipnetworks, new Predicate<IPNetwork>(
                delegate(IPNetwork ipnet)
                {
                    return ipnet != null;
                }));

            if (nnin.Length <= 0)
            {
                if (tryWide == false)
                {
                    throw new ArgumentException("ipnetworks");
                }

                ipnetwork = null;
                return;
            }

            if (nnin.Length == 1)
            {
                IPNetwork ipn0 = nnin[0];
                ipnetwork = ipn0;
                return;
            }

            Array.Sort<IPNetwork>(nnin);
            IPNetwork nnin0 = nnin[0];
            BigInteger uintNnin0 = nnin0._ipaddress;

            IPNetwork nninX = nnin[nnin.Length - 1];
            IPAddress ipaddressX = nninX.Broadcast;

            AddressFamily family = ipnetworks[0]._family;
            foreach (var ipnx in ipnetworks)
            {
                if (ipnx._family != family)
                {
                    throw new ArgumentException("MixedAddressFamily");
                }
            }

            IPNetwork ipn = new IPNetwork(0, family, 0);
            for (byte cidr = nnin0._cidr; cidr >= 0; cidr--)
            {
                IPNetwork wideSubnet = new IPNetwork(uintNnin0, family, cidr);
                if (wideSubnet.Contains(ipaddressX))
                {
                    ipn = wideSubnet;
                    break;
                }
            }

            ipnetwork = ipn;
            return;
        }

        #endregion

        #region Print

        /// <summary>
        /// Print an ipnetwork in a clear representation string
        /// </summary>
        /// <returns></returns>
        public string Print()
        {
            using (var sw = new StringWriter())
            {
                sw.WriteLine("IPNetwork   : {0}", ToString());
                sw.WriteLine("Network     : {0}", Network);
                sw.WriteLine("Netmask     : {0}", Netmask);
                sw.WriteLine("Cidr        : {0}", Cidr);
                sw.WriteLine("Broadcast   : {0}", Broadcast);
                sw.WriteLine("FirstUsable : {0}", FirstUsable);
                sw.WriteLine("LastUsable  : {0}", LastUsable);
                sw.WriteLine("Usable      : {0}", Usable);

                return sw.ToString();
            }
        }

        [Obsolete("static Print is deprecated, please use instance Print.")]
        public static string Print(IPNetwork ipnetwork)
        {
            if (ipnetwork == null)
            {
                throw new ArgumentNullException("ipnetwork");
            }

            return ipnetwork.Print();
        }

        #endregion

        #region TryGuessCidr

        /// <summary>
        /// Delegate to CidrGuess ClassFull guessing of cidr
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="cidr"></param>
        /// <returns></returns>
        public static bool TryGuessCidr(string ip, out byte cidr)
        {
            return CidrGuess.ClassFull.TryGuessCidr(ip, out cidr);
        }

        /// <summary>
        /// Try to parse cidr. Have to be >= 0 and &lt;= 32 or 128
        /// </summary>
        /// <param name="sidr"></param>
        /// <param name="family"></param>
        /// <param name="cidr"></param>
        /// <returns></returns>
        public static bool TryParseCidr(string sidr, AddressFamily family, out byte? cidr)
        {
            byte b = 0;
            if (!byte.TryParse(sidr, out b))
            {
                cidr = null;
                return false;
            }

            if (!b.TryToNetmask(family, out var netmask))
            {
                cidr = null;
                return false;
            }

            cidr = b;
            return true;
        }

        #endregion

        #region ListIPAddress

        /// <summary>
        /// List all ip addresses in a subnet
        /// </summary>
        /// <param name="ipnetwork"></param>
        /// <returns></returns>
        [Obsolete("static ListIPAddress is deprecated, please use instance ListIPAddress.")]
        public static IPAddressCollection ListIPAddress(IPNetwork ipnetwork)
        {
            return ipnetwork.ListIPAddress();
        }

        /// <summary>
        /// List all ip addresses in a subnet
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IPAddressCollection ListIPAddress(FilterEnum filter = FilterEnum.All)
        {
            return new IPAddressCollection(this, filter);
        }

        #endregion

        //
        // Need a better way to do it
        //
        // #region TrySubstractNetwork
        //
        //         public static bool TrySubstractNetwork(IPNetwork[] ipnetworks, IPNetwork substract, out IEnumerable<IPNetwork> result) {
        //
        //             if (ipnetworks == null) {
        //                 result = null;
        //                 return false;
        //             }
        //             if (ipnetworks.Length <= 0) {
        //                 result = null;
        //                 return false;
        //             }
        //             if (substract == null) {
        //                 result = null;
        //                 return false;
        //             }
        //             var results = new List<IPNetwork>();
        //             foreach (var ipn in ipnetworks) {
        //                 if (!Overlap(ipn, substract)) {
        //                     results.Add(ipn);
        //                     continue;
        //                 }
        //
        //                 var collection = ipn.Subnet(substract.Cidr);
        //                 var rtemp = new List<IPNetwork>();
        //                 foreach(var subnet in collection) {
        //                     if (subnet != substract) {
        //                         rtemp.Add(subnet);
        //                     }
        //                 }
        //                 var supernets = Supernet(rtemp.ToArray());
        //                 results.AddRange(supernets);
        //             }
        //             result = results;
        //             return true;
        //         }
        // #endregion
        #region IComparable<IPNetwork> Members

        public static Int32 Compare(IPNetwork left, IPNetwork right)
        {
            // two null IPNetworks are equal
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
            {
                return 0;
            }

            // two same IPNetworks are equal
            if (ReferenceEquals(left, right))
            {
                return 0;
            }

            // null is always sorted first
            if (ReferenceEquals(left, null))
            {
                return -1;
            }

            if (ReferenceEquals(right, null))
            {
                return 1;
            }

            // first test family
            var result = left._family.CompareTo(right._family);
            if (result != 0)
            {
                return result;
            }

            // second test the network
            result = left._network.CompareTo(right._network);
            if (result != 0)
            {
                return result;
            }

            // then test the cidr
            result = left._cidr.CompareTo(right._cidr);
            return result;
        }

        public Int32 CompareTo(IPNetwork other)
        {
            return Compare(this, other);
        }

        public Int32 CompareTo(Object obj)
        {
            // null is at less
            if (obj == null)
            {
                return 1;
            }

            // convert to a proper Cidr object
            var other = obj as IPNetwork;

            // type problem if null
            if (other == null)
            {
                throw new ArgumentException(
                    "The supplied parameter is an invalid type. Please supply an IPNetwork type.",
                    "obj");
            }

            // perform the comparision
            return CompareTo(other);
        }

        #endregion

        #region IEquatable<IPNetwork> Members

        public static Boolean Equals(IPNetwork left, IPNetwork right)
        {
            return Compare(left, right) == 0;
        }

        public Boolean Equals(IPNetwork other)
        {
            return Equals(this, other);
        }

        public override Boolean Equals(Object obj)
        {
            return Equals(this, obj as IPNetwork);
        }

        #endregion

        #region Operators

        /// <summary>
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Boolean operator ==(IPNetwork left, IPNetwork right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Boolean operator !=(IPNetwork left, IPNetwork right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Boolean operator <(IPNetwork left, IPNetwork right)
        {
            return Compare(left, right) < 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Boolean operator >(IPNetwork left, IPNetwork right)
        {
            return Compare(left, right) > 0;
        }

        #endregion

        #region XmlSerialization

        /// <summary>
        /// Initializes a new instance of the <see cref="IPNetwork"/> class.
        /// Created for DataContractSerialization. Better use static methods IPNetwork.Parse() to create IPNetworks
        /// </summary>
        public IPNetwork()
            : this(0, AddressFamily.InterNetwork, 0)
        {
        }

        #endregion

        #region ISerializable
        internal struct IPNetworkInteral
        {
            public BigInteger IPAddress;
            public byte Cidr;
            public AddressFamily AddressFamily;
        }

        private IPNetwork(SerializationInfo info, StreamingContext context)
        {
            var sipnetwork = (string)info.GetValue("IPNetwork", typeof(string));
            var ipnetwork = IPNetwork.Parse(sipnetwork);

            this._ipaddress = ipnetwork._ipaddress;
            this._cidr = ipnetwork._cidr;
            this._family = ipnetwork._family;
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("IPNetwork", this.ToString());
        }

        #endregion

        #region WildcardMask

        /// <summary>
        /// Netmask Inverse
        /// https://en.wikipedia.org/wiki/Wildcard_mask
        ///
        /// A wildcard mask is a mask of bits that indicates which parts of an IP address are available for examination.
        /// In the Cisco IOS,[1] they are used in several places, for example:
        ///    To indicate the size of a network or subnet for some routing protocols, such as OSPF.
        ///    To indicate what IP addresses should be permitted or denied in access control lists(ACLs).
        ///
        /// A wildcard mask can be thought of as an inverted subnet mask.For example,
        /// a subnet mask of 255.255.255.0 (binary equivalent = 11111111.11111111.11111111.00000000)
        /// inverts to a wildcard mask of 0.0.0.255 (binary equivalent = 00000000.00000000.00000000.11111111).
        ///
        /// A wild card mask is a matching rule.[2] The rule for a wildcard mask is:
        ///     0 means that the equivalent bit must match
        ///     1 means that the equivalent bit does not matter
        ///
        /// Any wildcard bit-pattern can be masked for examination.For example, a wildcard mask of 0.0.0.254
        /// (binary equivalent = 00000000.00000000.00000000.11111110) applied to IP address 10.10.10.2
        /// (00001010.00001010.00001010.00000010) will match even-numbered IP addresses 10.10.10.0, 10.10.10.2,
        /// 10.10.10.4, 10.10.10.6 etc.
        ///
        /// Same mask applied to 10.10.10.1 (00001010.00001010.00001010.00000001) will match
        /// odd-numbered IP addresses 10.10.10.1, 10.10.10.3, 10.10.10.5 etc.
        ///
        /// A network and wildcard mask combination of 1.1.1.1 0.0.0.0 would match an interface configured
        /// exactly with 1.1.1.1 only, and nothing else.
        ///
        /// Wildcard masks are used in situations where subnet masks may not apply.For example,
        /// when two affected hosts fall in different subnets, the use of a wildcard mask will
        /// group them together.
        ///
        /// List of wildcard masks
        /// Slash Netmask Wildcard mask
        /// /32   255.255.255.255    0.0.0.0
        /// /31   255.255.255.254    0.0.0.1
        /// /30   255.255.255.252    0.0.0.3
        /// /29   255.255.255.248    0.0.0.7
        /// /28   255.255.255.240    0.0.0.15
        /// /27   255.255.255.224    0.0.0.31
        /// /26   255.255.255.192    0.0.0.63
        /// /25   255.255.255.128    0.0.0.127
        /// /24   255.255.255.0      0.0.0.255
        /// /23   255.255.254.0      0.0.1.255
        /// /22   255.255.252.0      0.0.3.255
        /// /21   255.255.248.0      0.0.7.255
        /// /20   255.255.240.0      0.0.15.255
        /// /19   255.255.224.0      0.0.31.255
        /// /18   255.255.192.0      0.0.63.255
        /// /17   255.255.128.0      0.0.127.255
        /// /16   255.255.0.0        0.0.255.255
        /// /15   255.254.0.0        0.1.255.255
        /// /14   255.252.0.0        0.3.255.255
        /// /13   255.248.0.0        0.7.255.255
        /// /12   255.240.0.0        0.15.255.255
        /// /11   255.224.0.0        0.31.255.255
        /// /10   255.192.0.0        0.63.255.255
        /// /9    255.128.0.0        0.127.255.255
        /// /8    255.0.0.0          0.255.255.255
        /// /7    254.0.0.0          1.255.255.255
        /// /6    252.0.0.0          3.255.255.255
        /// /5    248.0.0.0          7.255.255.255
        /// /4    240.0.0.0          15.255.255.255
        /// /3    224.0.0.0          31.255.255.255
        /// /2    192.0.0.0          63.255.255.255
        /// /1    128.0.0.0          127.255.255.255
        /// /0    0.0.0.0            255.255.255.255
        ///
        /// </summary>
        public IPAddress WildcardMask
        {
            get
            {
                byte cidr = this._family == AddressFamily.InterNetwork ? (byte)32 : (byte)128;
                var netmask = cidr.ToUint(this._family);
                var wildcardmask = netmask - this._netmask;

                return wildcardmask.ToIPAddress(this._family);
            }
        }
        #endregion

        [Obsolete("BitsSet is deprecated, please use IPAddressExtensions.BitsSet")]
        public static uint BitsSet(IPAddress ip)
        {
            return ip.BitsSet();
        }

        [Obsolete("ToCidr is deprecated, please use IPAddressExtensions.ToCidr")]
        public static byte ToCidr(IPAddress ip)
        {
            return ip.ToCidr();
        }

        [Obsolete("ToIPAddress is deprecated, please use BigIntegerExtensions.ToIPAddress")]
        public static IPAddress ToIPAddress(BigInteger bigint, AddressFamily family)
        {
            return bigint.ToIPAddress(family);
        }

        [Obsolete("ToNetmask is deprecated, please use UintExtension.ToNetmask")]
        public static IPAddress ToNetmask(byte cidr, AddressFamily family)
        {
            return cidr.ToNetmask(family);
        }

        [Obsolete("ToBigInteger is deprecated, please use IPAddressExtensions.ToBigInteger")]
        public static BigInteger ToBigInteger(IPAddress mask)
        {
            return mask.ToBigInteger();
        }

        [Obsolete("ToUint is deprecated, please use UintExtension.ToUint")]
        public static BigInteger ToUint(byte cidr, AddressFamily family)
        {
            return cidr.ToUint(family);
        }

        [Obsolete("TryToBigInteger is deprecated, please use IPAddress.TryToBigInteger")]
        public static bool TryToBigInteger(IPAddress mask, out BigInteger? result)
        {
            return mask.TryToBigInteger(out result);
        }

        [Obsolete("TryToCidr is deprecated, please use IPAddress.TryToCidr")]
        public static bool TryToCidr(IPAddress mask, out byte? result)
        {
            return mask.TryToCidr(out result);
        }

        [Obsolete("TryToNetmask is deprecated, please use UintExtension.TryToNetmask")]
        public static bool TryToNetmask(byte cidr, AddressFamily family, out IPAddress result)
        {
            return cidr.TryToNetmask(family, out result);
        }

        [Obsolete("InternalToBigInteger is deprecated, please use UintExtension.InternalToBigInteger")]
        internal static void InternalToBigInteger(bool parsed, byte cidr, AddressFamily interNetwork, out BigInteger? result)
        {
            UintExtensions.InternalToBigInteger(parsed, cidr, interNetwork, out result);
        }

        [Obsolete("Resize is deprecated, please use BigIntegerExtensions.Resize")]
        public static byte[] Resize(byte[] vs, AddressFamily family)
        {
            return BigIntegerExtensions.Resize(vs, family);
        }

        [Obsolete("TryToUint is deprecated, please use UintExtensions.TryToUint")]
        internal static bool TryToUint(byte cidr, AddressFamily family, out BigInteger? result)
        {
            return cidr.TryToUint(family, out result);
        }

        [Obsolete("InternalToNetmask is deprecated, please use UintExtensions.InternalToNetmask")]
        internal static void InternalToNetmask(bool parsed, byte cidr, AddressFamily family, out IPAddress result)
        {
            UintExtensions.InternalToNetmask(parsed, cidr, family, out result);
        }
    }
}
