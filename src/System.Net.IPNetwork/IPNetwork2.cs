// <copyright file="IPNetwork2.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Net.Sockets;
    using System.Numerics;
    using System.Runtime.Serialization;
    using System.Text.RegularExpressions;

    /// <summary>
    /// IP Network utility class.
    /// Use IPNetwork.Parse to create instances.
    /// </summary>
    [Serializable]
    public sealed class IPNetwork2 : IComparable<IPNetwork2>, ISerializable
    {
        #region properties

        private readonly object _sync = new object();
        private readonly int _hashCode;
        private BigInteger _ipaddress;
        private byte _cidr;
        private BigInteger? _cachedBroadcast;

        private AddressFamily _family;

        /// <summary>
        /// Gets or sets the value of the IPNetwork property.
        /// </summary>
        [DataMember(Name = "IPNetwork", IsRequired = true)]
        public string Value
        {
            get
            {
                return this.ToString();
            }

            set
            {
                var ipnetwork = IPNetwork2.Parse(value);
                this._ipaddress = ipnetwork._ipaddress;
                this._family = ipnetwork._family;
                this._cidr = ipnetwork._cidr;
                lock (_sync)
                {
                    this._cachedBroadcast = null;
                }
            }
        }

        #endregion

        #region accessors

        internal BigInteger _network
        {
            get
            {
                BigInteger uintNetwork = this._ipaddress & this._netmask;
                return uintNetwork;
            }
        }

        /// <summary>
        /// Gets network address.
        /// </summary>
        public IPAddress Network
        {
            get
            {
                return IPNetwork2.ToIPAddress(this._network, this._family);
            }
        }

        /// <summary>
        /// Gets address Family.
        /// </summary>
        public AddressFamily AddressFamily
        {
            get
            {
                return this._family;
            }
        }

        internal BigInteger _netmask
        {
            get
            {
                return IPNetwork2.ToUint(this._cidr, this._family);
            }
        }

        /// <summary>
        /// Gets netmask.
        /// </summary>
        public IPAddress Netmask
        {
            get
            {
                return IPNetwork2.ToIPAddress(this._netmask, this._family);
            }
        }

        internal BigInteger _broadcast
        {
            get
            {
                var cached = this._cachedBroadcast;
                if (cached != null)
                {
                    return cached.Value;
                }

                lock (_sync)
                {
                    var cached2 = this._cachedBroadcast;
                    if (cached2 != null)
                    {
                        return cached2.Value;
                    }

                    var network = this._network;
                    var computed = CreateBroadcast(ref network, this._netmask, this._family);
                    this._cachedBroadcast = computed;
                    return computed;
                }
            }
        }

        /// <summary>
        /// Gets broadcast address.
        /// </summary>
        public IPAddress Broadcast
        {
            get
            {
                if (this._family == Sockets.AddressFamily.InterNetworkV6)
                {
                    return null;
                }

                return IPNetwork2.ToIPAddress(this._broadcast, this._family);
            }
        }

        /// <summary>
        /// Gets first usable IP adress in Network.
        /// </summary>
        public IPAddress FirstUsable
        {
            get
            {
                BigInteger fisrt = this._family == Sockets.AddressFamily.InterNetworkV6
                    ? this._network
                    : (this.Usable <= 0) ? this._network : this._network + 1;
                return IPNetwork2.ToIPAddress(fisrt, this._family);
            }
        }

        /// <summary>
        /// Gets last usable IP adress in Network.
        /// </summary>
        public IPAddress LastUsable
        {
            get
            {
                BigInteger last = this._family == Sockets.AddressFamily.InterNetworkV6
                    ? this._broadcast
                    : (this.Usable <= 0) ? this._network : this._broadcast - 1;
                return IPNetwork2.ToIPAddress(last, this._family);
            }
        }

        /// <summary>
        /// Gets number of usable IP adress in Network.
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
                var bmask = new BigInteger(mask);
                BigInteger usableIps = (this._cidr > 30) ? 0 : ((bmask >> this._cidr) - 1);
                return usableIps;
            }
        }

        /// <summary>
        /// Gets number of IP adress in Network.
        /// </summary>
        public BigInteger Total
        {
            get
            {
                int max = this._family == Sockets.AddressFamily.InterNetwork ? 32 : 128;
                var count = BigInteger.Pow(2, max - this._cidr);
                return count;
            }
        }

        /// <summary>
        /// Gets the CIDR netmask notation.
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
        /// <summary>
        /// Initializes a new instance of the <see cref="IPNetwork2"/> class with the specified IP address, address family, and CIDR.
        /// </summary>
        /// <param name="ipaddress">The IP address of the network.</param>
        /// <param name="family">The address family of the network.</param>
        /// <param name="cidr">The CIDR (Classless Inter-Domain Routing) notation of the network.</param>
        internal
#endif
            IPNetwork2(BigInteger ipaddress, AddressFamily family, byte cidr)
        {
            this.Init(ipaddress, family, cidr);
            this._hashCode = this.ComputeHashCode();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IPNetwork2"/> class.
        /// Creates a new IPNetwork.
        /// </summary>
        /// <param name="ipaddress">An ipaddress.</param>
        /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
        /// <exception cref="ArgumentNullException">ipaddress is null.</exception>
        public IPNetwork2(IPAddress ipaddress, byte cidr)
        {
            if (ipaddress == null)
            {
                throw new ArgumentNullException("ipaddress");
            }

            BigInteger uintIpAddress = ToBigInteger(ipaddress);

            this.Init(uintIpAddress, ipaddress.AddressFamily, cidr);
            this._hashCode = this.ComputeHashCode();
        }

        private void Init(BigInteger ipaddress, AddressFamily family, byte cidr)
        {
            int maxCidr = family == AddressFamily.InterNetwork ? 32 : 128;
            if (cidr > maxCidr)
            {
                throw new ArgumentOutOfRangeException("cidr");
            }

            this._ipaddress = ipaddress;
            this._family = family;
            this._cidr = cidr;
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
        /// ```.
        ///
        /// </summary>
        /// <param name="ipaddress">A string containing an ip address to convert.</param>
        /// <param name="netmask">A string representing a netmask in std format (255.255.255.0).</param>
        /// <returns>An IPNetwork equivalent to the network contained in ipaddress/netmask.</returns>
        public static IPNetwork2 Parse(string ipaddress, string netmask)
        {
            IPNetwork2.InternalParse(false, ipaddress, netmask, out IPNetwork2 ipnetwork);
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
        /// Broadcast : 192.168.168.255.
        /// </summary>
        /// <param name="ipaddress">A string containing an ip address to convert.</param>
        /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
        /// <returns>An IPNetwork equivalent to the network contained in ipaddress/cidr.</returns>
        public static IPNetwork2 Parse(string ipaddress, byte cidr)
        {
            IPNetwork2.InternalParse(false, ipaddress, cidr, out IPNetwork2 ipnetwork);
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
        /// Broadcast : 192.168.168.255.
        /// </summary>
        /// <param name="ipaddress">A string containing an ip address to convert.</param>
        /// <param name="netmask">A netmask to be used to create the IPNetwork.</param>
        /// <returns>An IPNetwork equivalent to the network contained in ipaddress/netmask.</returns>
        public static IPNetwork2 Parse(IPAddress ipaddress, IPAddress netmask)
        {
            IPNetwork2.InternalParse(false, ipaddress, netmask, out IPNetwork2 ipnetwork);
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
        /// Broadcast : 192.168.0.255.
        /// </summary>
        /// <param name="network">A string containing an ip network to convert.</param>
        /// <returns>An IPNetwork equivalent to the network contained in string network.</returns>
        public static IPNetwork2 Parse(string network)
        {
            IPNetwork2.InternalParse(false, network, CidrGuess.ClassFull, true, out IPNetwork2 ipnetwork);
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
        /// Broadcast : 192.168.0.255.
        /// </summary>
        /// <param name="network">A string containing an ip network to convert.</param>
        /// <param name="sanitanize">Whether to sanitize network or not.</param>
        /// <returns>An IPNetwork equivalent to the network contained in string network.</returns>
        public static IPNetwork2 Parse(string network, bool sanitanize)
        {
            IPNetwork2.InternalParse(false, network, CidrGuess.ClassFull, sanitanize, out IPNetwork2 ipnetwork);
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
        /// Broadcast : 192.168.0.255.
        /// </summary>
        /// <param name="network">A string containing an ip network to convert.</param>
        /// <param name="cidrGuess">A ICidrGuess implementation that will be used to guess CIDR during converion.</param>
        /// <returns>An IPNetwork equivalent to the network contained in string network.</returns>
        public static IPNetwork2 Parse(string network, ICidrGuess cidrGuess)
        {
            IPNetwork2.InternalParse(false, network, cidrGuess, true, out IPNetwork2 ipnetwork);
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
        /// Broadcast : 192.168.0.255.
        /// </summary>
        /// <param name="network">A string containing an ip network to convert.</param>
        /// <param name="cidrGuess">A ICidrGuess implementation that will be used to guess CIDR during converion.</param>
        /// <param name="sanitanize">Whether to sanitize network or not.</param>
        /// <returns>An IPNetwork equivalent to the network contained in string network.</returns>
        public static IPNetwork2 Parse(string network, ICidrGuess cidrGuess, bool sanitanize)
        {
            IPNetwork2.InternalParse(false, network, cidrGuess, sanitanize, out IPNetwork2 ipnetwork);
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
        /// Broadcast : 192.168.168.255.
        /// </summary>
        /// <param name="ipaddress">A string containing an ip address to convert.</param>
        /// <param name="netmask">A string containing a netmaks to convert (255.255.255.0).</param>
        /// <param name="ipnetwork">When this method returns, contains the IPNetwork value equivalent of the ip adress contained in ipaddress with the netmask corresponding to cidr, if the conversion succeeded, or null if the conversion failed. The conversion fails if the s parameter is null or Empty, is not of the correct format, or represents an invalid ip address. This parameter is passed uninitialized; any value originally supplied in result will be overwritten.</param>
        /// <returns>true if ipaddress/netmask was converted successfully; otherwise, false..</returns>
        public static bool TryParse(string ipaddress, string netmask, out IPNetwork2 ipnetwork)
        {
            IPNetwork2.InternalParse(true, ipaddress, netmask, out IPNetwork2 ipnetwork2);
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
        /// Broadcast : 192.168.168.255.
        /// </summary>
        /// <param name="ipaddress">A string containing an ip address to convert.</param>
        /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
        /// <param name="ipnetwork">When this method returns, contains the IPNetwork value equivalent of the ip adress contained in ipaddress with the netmask corresponding to cidr, if the conversion succeeded, or null if the conversion failed. The conversion fails if the s parameter is null or Empty, is not of the correct format, or represents an invalid ip address. This parameter is passed uninitialized; any value originally supplied in result will be overwritten.</param>
        /// <returns>true if ipaddress/cidr was converted successfully; otherwise, false..</returns>
        public static bool TryParse(string ipaddress, byte cidr, out IPNetwork2 ipnetwork)
        {
            IPNetwork2.InternalParse(true, ipaddress, cidr, out IPNetwork2 ipnetwork2);
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
        /// Broadcast : 192.168.0.255.
        /// </summary>
        /// <param name="network">A string containing an ip network to convert.</param>
        /// <param name="ipnetwork">When this method returns, contains the IPNetwork value equivalent of the ip adress contained in ipaddress with the netmask corresponding to cidr, if the conversion succeeded, or null if the conversion failed. The conversion fails if the s parameter is null or Empty, is not of the correct format, or represents an invalid ip address. This parameter is passed uninitialized; any value originally supplied in result will be overwritten.</param>
        /// <returns>true if network was converted successfully; otherwise, false..</returns>
        public static bool TryParse(string network, out IPNetwork2 ipnetwork)
        {
            bool sanitanize = true;
            IPNetwork2.InternalParse(true, network, CidrGuess.ClassFull, sanitanize, out IPNetwork2 ipnetwork2);
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
        /// Broadcast : 192.168.0.255.
        /// </summary>
        /// <param name="network">A string containing an ip network to convert.</param>
        /// <param name="sanitanize">Whether to sanitize network or not.</param>
        /// <param name="ipnetwork">When this method returns, contains the IPNetwork value equivalent of the ip adress contained in ipaddress with the netmask corresponding to cidr, if the conversion succeeded, or null if the conversion failed. The conversion fails if the s parameter is null or Empty, is not of the correct format, or represents an invalid ip address. This parameter is passed uninitialized; any value originally supplied in result will be overwritten.</param>
        /// <returns>true if network was converted successfully; otherwise, false..</returns>
        public static bool TryParse(string network, bool sanitanize, out IPNetwork2 ipnetwork)
        {
            IPNetwork2.InternalParse(true, network, CidrGuess.ClassFull, sanitanize, out IPNetwork2 ipnetwork2);
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
        /// Broadcast : 192.168.0.255.
        /// </summary>
        /// <param name="ipaddress">An IPAdresse to convert.</param>
        /// <param name="netmask">A IPAdresse to be used as netmaks to convert.</param>
        /// <param name="ipnetwork">When this method returns, contains the IPNetwork value equivalent of the ip adress contained in ipaddress with the netmask corresponding to cidr, if the conversion succeeded, or null if the conversion failed. The conversion fails if the s parameter is null or Empty, is not of the correct format, or represents an invalid ip address. This parameter is passed uninitialized; any value originally supplied in result will be overwritten.</param>
        /// <returns>true if network was converted successfully; otherwise, false..</returns>
        public static bool TryParse(IPAddress ipaddress, IPAddress netmask, out IPNetwork2 ipnetwork)
        {
            IPNetwork2.InternalParse(true, ipaddress, netmask, out IPNetwork2 ipnetwork2);
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
        /// Broadcast : 192.168.168.255.
        /// </summary>
        /// <param name="tryParse">Whether to throw exception or not during conversion.</param>
        /// <param name="ipaddress">A string containing an ip address to convert.</param>
        /// <param name="netmask">A string containing a netmask to convert (255.255.255.0).</param>
        /// <param name="ipnetwork">The resulting IPNetwork.</param>
        private static void InternalParse(bool tryParse, string ipaddress, string netmask, out IPNetwork2 ipnetwork)
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

            bool ipaddressParsed = IPAddress.TryParse(ipaddress, out IPAddress ip);
            if (ipaddressParsed == false)
            {
                if (tryParse == false)
                {
                    throw new ArgumentException("ipaddress");
                }

                ipnetwork = null;
                return;
            }

            bool netmaskParsed = IPAddress.TryParse(netmask, out IPAddress mask);
            if (netmaskParsed == false)
            {
                if (tryParse == false)
                {
                    throw new ArgumentException("netmask");
                }

                ipnetwork = null;
                return;
            }

            IPNetwork2.InternalParse(tryParse, ip, mask, out ipnetwork);
        }

        private static void InternalParse(bool tryParse, string network, ICidrGuess cidrGuess, bool sanitanize, out IPNetwork2 ipnetwork)
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
                network = Regex.Replace(network, @"[^0-9a-fA-F\.\/\s\:]+", string.Empty, RegexOptions.None, TimeSpan.FromMilliseconds(100));
                network = Regex.Replace(network, @"\s{2,}", " ", RegexOptions.None, TimeSpan.FromMilliseconds(100));
                network = network.Trim();
            }

            StringSplitOptions splitOptions = sanitanize ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
            string[] args = network.Split(new char[] { ' ', '/' }, splitOptions);
            byte cidr = 0;

            if (args.Length == 1)
            {
                string cidrlessNetwork = args[0];
                if (cidrGuess.TryGuessCidr(cidrlessNetwork, out cidr))
                {
                    IPNetwork2.InternalParse(tryParse, cidrlessNetwork, cidr, out ipnetwork);
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
                IPNetwork2.InternalParse(tryParse, args[0], cidr, out ipnetwork);
                return;
            }

            IPNetwork2.InternalParse(tryParse, args[0], args[1], out ipnetwork);
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
        /// Broadcast : 192.168.168.255.
        /// </summary>
        /// <param name="tryParse">Whether to throw exception or not during conversion.</param>
        /// <param name="ipaddress">An ip address to convert.</param>
        /// <param name="netmask">A netmask to convert (255.255.255.0).</param>
        /// <param name="ipnetwork">The resulting IPNetwork.</param>
        private static void InternalParse(bool tryParse, IPAddress ipaddress, IPAddress netmask, out IPNetwork2 ipnetwork)
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

            var uintIpAddress = IPNetwork2.ToBigInteger(ipaddress);
            bool parsed = IPNetwork2.TryToCidr(netmask, out byte? cidr2);
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

            var ipnet = new IPNetwork2(uintIpAddress, ipaddress.AddressFamily, cidr);
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
        /// Broadcast : 192.168.168.255.
        /// </summary>
        /// <param name="tryParse">Whether to throw exception or not during conversion.</param>
        /// <param name="ipaddress">A string containing an ip address to convert.</param>
        /// <param name="cidr">A byte representing the CIDR to be used in conversion (/24).</param>
        /// <param name="ipnetwork">The resulting IPNetwork.</param>
        private static void InternalParse(bool tryParse, string ipaddress, byte cidr, out IPNetwork2 ipnetwork)
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

            bool ipaddressParsed = IPAddress.TryParse(ipaddress, out IPAddress ip);
            if (ipaddressParsed == false)
            {
                if (tryParse == false)
                {
                    throw new ArgumentException("ipaddress");
                }

                ipnetwork = null;
                return;
            }

            bool parsedNetmask = IPNetwork2.TryToNetmask(cidr, ip.AddressFamily, out IPAddress mask);
            if (parsedNetmask == false)
            {
                if (tryParse == false)
                {
                    throw new ArgumentException("cidr");
                }

                ipnetwork = null;
                return;
            }

            IPNetwork2.InternalParse(tryParse, ip, mask, out ipnetwork);
        }
        #endregion

        #region converters

        #region ToUint

        /// <summary>
        /// Convert an ipadress to decimal
        /// 0.0.0.0 -> 0
        /// 0.0.1.0 -> 256.
        /// </summary>
        /// <param name="ipaddress">A string containing an ip address to convert.</param>
        /// <returns>A number representing the ipaddress.</returns>
        public static BigInteger ToBigInteger(IPAddress ipaddress)
        {
            IPNetwork2.InternalToBigInteger(false, ipaddress, out BigInteger? uintIpAddress);

            return (BigInteger)uintIpAddress;
        }

        /// <summary>
        /// Convert an ipadress to decimal
        /// 0.0.0.0 -> 0
        /// 0.0.1.0 -> 256.
        /// </summary>
        /// <param name="ipaddress">A string containing an ip address to convert.</param>
        /// <param name="uintIpAddress">A number representing the IPAdress.</param>
        /// <returns>true if ipaddress was converted successfully; otherwise, false.</returns>
        public static bool TryToBigInteger(IPAddress ipaddress, out BigInteger? uintIpAddress)
        {
            IPNetwork2.InternalToBigInteger(true, ipaddress, out BigInteger? uintIpAddress2);
            bool parsed = uintIpAddress2 != null;
            uintIpAddress = uintIpAddress2;

            return parsed;
        }

#if TRAVISCI
        public
#else
        internal
#endif
            static void InternalToBigInteger(bool tryParse, IPAddress ipaddress, out BigInteger? uintIpAddress)
        {
            if (ipaddress == null)
            {
                if (tryParse == false)
                {
                    throw new ArgumentNullException("ipaddress");
                }

                uintIpAddress = null;
                return;
            }

#if NET5_0 || NETSTANDARD2_1
            byte[] bytes = ipaddress.AddressFamily == AddressFamily.InterNetwork ? new byte[4] : new byte[16];
            Span<byte> span = bytes.AsSpan();
            if (!ipaddress.TryWriteBytes(span, out _))
            {
                if (tryParse == false)
                {
                    throw new ArgumentException("ipaddress");
                }

                uintIpAddress = null;
                return;
            }

            uintIpAddress = new BigInteger(span, isUnsigned: true, isBigEndian: true);
#elif NET45 || NET46 || NET47 || NETSTANDARD20
            byte[] bytes = ipaddress.GetAddressBytes();
            bytes.AsSpan().Reverse();

            // add trailing 0 to make unsigned
            byte[] unsigned = new byte[bytes.Length + 1];
            Buffer.BlockCopy(bytes, 0, unsigned, 0, bytes.Length);
            uintIpAddress = new BigInteger(unsigned);
#else
            byte[] bytes = ipaddress.GetAddressBytes();
            Array.Reverse(bytes);

            // add trailing 0 to make unsigned
            byte[] unsigned = new byte[bytes.Length + 1];
            Buffer.BlockCopy(bytes, 0, unsigned, 0, bytes.Length);
            uintIpAddress = new BigInteger(unsigned);
#endif
        }

        /// <summary>
        /// Convert a cidr to BigInteger netmask.
        /// </summary>
        /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
        /// <param name="family">Either IPv4 or IPv6.</param>
        /// <returns>A number representing the netmask exprimed in CIDR.</returns>
        public static BigInteger ToUint(byte cidr, AddressFamily family)
        {
            IPNetwork2.InternalToBigInteger(false, cidr, family, out BigInteger? uintNetmask);

            return (BigInteger)uintNetmask;
        }

        /// <summary>
        /// Convert a cidr to uint netmask.
        /// </summary>
        /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
        /// <param name="family">Either IPv4 or IPv6.</param>
        /// <param name="uintNetmask">A number representing the netmask.</param>
        /// <returns>true if cidr was converted successfully; otherwise, false.</returns>
        public static bool TryToUint(byte cidr, AddressFamily family, out BigInteger? uintNetmask)
        {
            IPNetwork2.InternalToBigInteger(true, cidr, family, out BigInteger? uintNetmask2);
            bool parsed = uintNetmask2 != null;
            uintNetmask = uintNetmask2;

            return parsed;
        }

        /// <summary>
        /// Convert a cidr to uint netmask.
        /// </summary>
        /// <param name="tryParse">Whether to throw exception or not during conversion.</param>
        /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
        /// <param name="family">Either IPv4 or IPv6.</param>
        /// <param name="uintNetmask">A number representing the netmask.</param>
#if TRAVISCI
        public
#else
        internal
#endif
            static void InternalToBigInteger(bool tryParse, byte cidr, AddressFamily family, out BigInteger? uintNetmask)
        {
            if (family == AddressFamily.InterNetwork && cidr > 32)
            {
                if (tryParse == false)
                {
                    throw new ArgumentOutOfRangeException("cidr");
                }

                uintNetmask = null;
                return;
            }

            if (family == AddressFamily.InterNetworkV6 && cidr > 128)
            {
                if (tryParse == false)
                {
                    throw new ArgumentOutOfRangeException("cidr");
                }

                uintNetmask = null;
                return;
            }

            if (family != AddressFamily.InterNetwork
                && family != AddressFamily.InterNetworkV6)
            {
                if (tryParse == false)
                {
                    throw new NotSupportedException(family.ToString());
                }

                uintNetmask = null;
                return;
            }

            if (family == AddressFamily.InterNetwork)
            {
                uintNetmask = cidr == 0 ? 0 : 0xffffffff << (32 - cidr);
                return;
            }

            var mask = new BigInteger(new byte[]
            {
                0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff,
                0xff, 0xff, 0xff, 0xff,
                0x00,
            });

            BigInteger masked = cidr == 0 ? 0 : mask << (128 - cidr);
            byte[] m = masked.ToByteArray();
            byte[] bmask = new byte[17];
            int copy = m.Length > 16 ? 16 : m.Length;
            Array.Copy(m, 0, bmask, 0, copy);
            uintNetmask = new BigInteger(bmask);
        }

        #endregion

        #region ToCidr

        /// <summary>
        /// Convert netmask to CIDR
        ///  255.255.255.0 -> 24
        ///  255.255.0.0   -> 16
        ///  255.0.0.0     -> 8.
        /// </summary>
        /// <param name="tryParse">Whether to throw exception or not during conversion.</param>
        /// <param name="netmask">A number representing the netmask to convert.</param>
        /// <param name="family">Either IPv4 or IPv6.</param>
        /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
        private static void InternalToCidr(bool tryParse, BigInteger netmask, AddressFamily family, out byte? cidr)
        {
            if (!IPNetwork2.InternalValidNetmask(netmask, family))
            {
                if (tryParse == false)
                {
                    throw new ArgumentException("netmask");
                }

                cidr = null;
                return;
            }

            byte cidr2 = IPNetwork2.BitsSet(netmask, family);
            cidr = cidr2;

            return;
        }

        /// <summary>
        /// Convert netmask to CIDR
        ///  255.255.255.0 -> 24
        ///  255.255.0.0   -> 16
        ///  255.0.0.0     -> 8.
        /// </summary>
        /// <param name="netmask">An IPAdress representing the CIDR to convert.</param>
        /// <returns>A byte representing the CIDR converted from the netmask.</returns>
        public static byte ToCidr(IPAddress netmask)
        {
            IPNetwork2.InternalToCidr(false, netmask, out byte? cidr);
            return (byte)cidr;
        }

        /// <summary>
        /// Convert netmask to CIDR
        ///  255.255.255.0 -> 24
        ///  255.255.0.0   -> 16
        ///  255.0.0.0     -> 8.
        /// </summary>
        /// <param name="netmask">An IPAdress representing the CIDR to convert.</param>
        /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
        /// <returns>true if netmask was converted successfully; otherwise, false.</returns>
        public static bool TryToCidr(IPAddress netmask, out byte? cidr)
        {
            IPNetwork2.InternalToCidr(true, netmask, out byte? cidr2);
            bool parsed = cidr2 != null;
            cidr = cidr2;
            return parsed;
        }

        private static void InternalToCidr(bool tryParse, IPAddress netmask, out byte? cidr)
        {
            if (netmask == null)
            {
                if (tryParse == false)
                {
                    throw new ArgumentNullException("netmask");
                }

                cidr = null;
                return;
            }

            bool parsed = IPNetwork2.TryToBigInteger(netmask, out BigInteger? uintNetmask2);

            // 20180217 lduchosal
            // impossible to reach code.
            // if (parsed == false) {
            //     if (tryParse == false) {
            //         throw new ArgumentException("netmask");
            //     }
            //     cidr = null;
            //     return;
            // }
            var uintNetmask = (BigInteger)uintNetmask2;

            IPNetwork2.InternalToCidr(tryParse, uintNetmask, netmask.AddressFamily, out byte? cidr2);
            cidr = cidr2;

            return;
        }

        #endregion

        #region ToNetmask

        /// <summary>
        /// Convert CIDR to netmask
        ///  24 -> 255.255.255.0
        ///  16 -> 255.255.0.0
        ///  8 -> 255.0.0.0.
        /// </summary>
        /// <see href="http://snipplr.com/view/15557/cidr-class-for-ipv4/"/>
        /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
        /// <param name="family">Either IPv4 or IPv6.</param>
        /// <returns>An IPAdress representing cidr.</returns>
        public static IPAddress ToNetmask(byte cidr, AddressFamily family)
        {
            IPNetwork2.InternalToNetmask(false, cidr, family, out IPAddress netmask);

            return netmask;
        }

        /// <summary>
        /// Convert CIDR to netmask
        ///  24 -> 255.255.255.0
        ///  16 -> 255.255.0.0
        ///  8 -> 255.0.0.0.
        /// </summary>
        /// <see href="http://snipplr.com/view/15557/cidr-class-for-ipv4/"/>
        /// <param name="cidr">A byte representing the netmask in cidr format (/24).</param>
        /// <param name="family">Either IPv4 or IPv6.</param>
        /// <param name="netmask">The resulting netmask.</param>
        /// <returns>true if cidr was converted successfully; otherwise, false.</returns>
        public static bool TryToNetmask(byte cidr, AddressFamily family, out IPAddress netmask)
        {
            IPNetwork2.InternalToNetmask(true, cidr, family, out IPAddress netmask2);
            bool parsed = netmask2 != null;
            netmask = netmask2;

            return parsed;
        }

#if TRAVISCI
        public
#else
        internal
#endif
            static void InternalToNetmask(bool tryParse, byte cidr, AddressFamily family, out IPAddress netmask)
        {
            if (family != AddressFamily.InterNetwork
                && family != AddressFamily.InterNetworkV6)
            {
                if (tryParse == false)
                {
                    throw new ArgumentException("family");
                }

                netmask = null;
                return;
            }

            // 20180217 lduchosal
            // impossible to reach code, byte cannot be negative :
            //
            // if (cidr < 0) {
            //     if (tryParse == false) {
            //         throw new ArgumentOutOfRangeException("cidr");
            //     }
            //     netmask = null;
            //     return;
            // }
            int maxCidr = family == Sockets.AddressFamily.InterNetwork ? 32 : 128;
            if (cidr > maxCidr)
            {
                if (tryParse == false)
                {
                    throw new ArgumentOutOfRangeException("cidr");
                }

                netmask = null;
                return;
            }

            BigInteger mask = IPNetwork2.ToUint(cidr, family);
            var netmask2 = IPNetwork2.ToIPAddress(mask, family);
            netmask = netmask2;

            return;
        }

        #endregion

        #endregion

        #region utils

        #region BitsSet

        /// <summary>
        /// Count bits set to 1 in netmask.
        /// </summary>
        /// <see href="http://stackoverflow.com/questions/109023/best-algorithm-to-count-the-number-of-set-bits-in-a-32-bit-integer"/>
        /// <param name="netmask">A number representing the netmask to count bits from.</param>
        /// <param name="family">Either IPv4 or IPv6.</param>
        /// <returns>The number of bytes set to 1.</returns>
        private static byte BitsSet(BigInteger netmask, AddressFamily family)
        {
            string s = netmask.ToBinaryString();

            return (byte)s.Replace("0", string.Empty)
                .ToCharArray()
                .Length;
        }

        /// <summary>
        /// Count bits set to 1 in netmask.
        /// </summary>
        /// <param name="netmask">A number representing the netmask to count bits from.</param>
        /// <returns>The number of bytes set to 1.</returns>
        public static uint BitsSet(IPAddress netmask)
        {
            var uintNetmask = IPNetwork2.ToBigInteger(netmask);
            uint bits = IPNetwork2.BitsSet(uintNetmask, netmask.AddressFamily);

            return bits;
        }

        #endregion

        #region ValidNetmask

        /// <summary>
        /// return true if netmask is a valid netmask
        /// 255.255.255.0, 255.0.0.0, 255.255.240.0, ...
        /// </summary>
        /// <see href="http://www.actionsnip.com/snippets/tomo_atlacatl/calculate-if-a-netmask-is-valid--as2-"/>
        /// <param name="netmask">A number representing the netmask to validate.</param>
        /// <returns>true if netmask is a valid IP Netmask; otherwise, false.</returns>
        public static bool ValidNetmask(IPAddress netmask)
        {
            if (netmask == null)
            {
                throw new ArgumentNullException("netmask");
            }

            var uintNetmask = IPNetwork2.ToBigInteger(netmask);
            bool valid = IPNetwork2.InternalValidNetmask(uintNetmask, netmask.AddressFamily);

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

            BigInteger mask = family == AddressFamily.InterNetwork
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

        #region ToIPAddress

        /// <summary>
        /// Transform a uint ipaddress into IPAddress object.
        /// </summary>
        /// <param name="ipaddress">A number representing an ip address to convert.</param>
        /// <param name="family">Either IPv4 or IPv6.</param>
        /// <returns>An ip adress.</returns>
        public static IPAddress ToIPAddress(BigInteger ipaddress, AddressFamily family)
        {
            int width = family == AddressFamily.InterNetwork ? 4 : 16;
            byte[] bytes = ipaddress.ToByteArray();
            byte[] bytes2 = new byte[width];
            int copy = bytes.Length > width ? width : bytes.Length;
            Array.Copy(bytes, 0, bytes2, 0, copy);
            Array.Reverse(bytes2);

            byte[] sized = Resize(bytes2, family);
            var ip = new IPAddress(sized);
            return ip;
        }

#if TRAVISCI
        public
#else
        internal
#endif
            static byte[] Resize(byte[] bytes, AddressFamily family)
        {
            if (family != AddressFamily.InterNetwork
                && family != AddressFamily.InterNetworkV6)
            {
                throw new ArgumentException("family");
            }

            int width = family == AddressFamily.InterNetwork ? 4 : 16;

            if (bytes.Length > width)
            {
                throw new ArgumentException("bytes");
            }

            byte[] result = new byte[width];
            Array.Copy(bytes, 0, result, 0, bytes.Length);

            return result;
        }

        #endregion

        #endregion

        #region contains

        /// <summary>
        /// return true if ipaddress is contained in network.
        /// </summary>
        /// <param name="contains">A string containing an ip address to convert.</param>
        /// <returns>true if ipaddress is contained into the IP Network; otherwise, false.</returns>
        public bool Contains(IPAddress contains)
        {
            if (contains == null)
            {
                throw new ArgumentNullException("contains");
            }

            if (this.AddressFamily != contains.AddressFamily)
            {
                return false;
            }

            BigInteger uintNetwork = this._network;
            BigInteger uintBroadcast = this._broadcast; // CreateBroadcast(ref uintNetwork, this._netmask, this._family);
            var uintAddress = IPNetwork2.ToBigInteger(contains);

            bool result = uintAddress >= uintNetwork
                && uintAddress <= uintBroadcast;

            return result;
        }

        /// <summary>
        /// Determines whether the given IP address is part of the given IP network.
        /// </summary>
        /// <param name="network">The IP network.</param>
        /// <param name="ipaddress">The IP address.</param>
        /// <returns>
        /// <c>true</c> if the IP address is part of the IP network; otherwise, <c>false</c>.
        /// </returns>
        [Obsolete("static Contains is deprecated, please use instance Contains.")]
        public static bool Contains(IPNetwork2 network, IPAddress ipaddress)
        {
            if (network == null)
            {
                throw new ArgumentNullException("network");
            }

            return network.Contains(ipaddress);
        }

        /// <summary>
        /// return true is network2 is fully contained in network.
        /// </summary>
        /// <param name="contains">The network to test.</param>
        /// <returns>It returns the boolean value. If network2 is in IPNetwork then it returns True, otherwise returns False.</returns>
        public bool Contains(IPNetwork2 contains)
        {
            if (contains == null)
            {
                throw new ArgumentNullException("contains");
            }

            BigInteger uintNetwork = this._network;
            BigInteger uintBroadcast = this._broadcast; // CreateBroadcast(ref uintNetwork, this._netmask, this._family);

            BigInteger uintFirst = contains._network;
            BigInteger uintLast = contains._broadcast; // CreateBroadcast(ref uintFirst, network2._netmask, network2._family);

            bool result = uintFirst >= uintNetwork
                && uintLast <= uintBroadcast;

            return result;
        }

        /// <summary>
        /// Determines if the given <paramref name="network"/> contains the specified <paramref name="network2"/>.
        /// </summary>
        /// <param name="network">The network to check for containment.</param>
        /// <param name="network2">The network to check if it is contained.</param>
        /// <returns>
        /// <c>true</c> if the <paramref name="network"/> contains the <paramref name="network2"/>; otherwise, <c>false</c>.
        /// </returns>
        [Obsolete("static Contains is deprecated, please use instance Contains.")]
        public static bool Contains(IPNetwork2 network, IPNetwork2 network2)
        {
            if (network == null)
            {
                throw new ArgumentNullException("network");
            }

            return network.Contains(network2);
        }

        private static BigInteger CreateBroadcast(ref BigInteger network, BigInteger netmask, AddressFamily family)
        {
            int width = family == AddressFamily.InterNetwork ? 4 : 16;
            BigInteger uintBroadcast = network + netmask.PositiveReverse(width);

            return uintBroadcast;
        }

        #endregion

        #region overlap

        /// <summary>
        /// return true is network2 overlap network.
        /// </summary>
        /// <param name="network2">The network to test.</param>
        /// <returns>true if network2 overlaps into the IP Network; otherwise, false.</returns>
        public bool Overlap(IPNetwork2 network2)
        {
            if (network2 == null)
            {
                throw new ArgumentNullException("network2");
            }

            BigInteger uintNetwork = this._network;
            BigInteger uintBroadcast = this._broadcast;

            BigInteger uintFirst = network2._network;
            BigInteger uintLast = network2._broadcast;

            bool overlap =
                (uintFirst >= uintNetwork && uintFirst <= uintBroadcast)
                || (uintLast >= uintNetwork && uintLast <= uintBroadcast)
                || (uintFirst <= uintNetwork && uintLast >= uintBroadcast)
                || (uintFirst >= uintNetwork && uintLast <= uintBroadcast);

            return overlap;
        }

        /// <summary>
        /// Determines if two IPNetwork2 objects overlap each other.
        /// </summary>
        /// <param name="network">The first IPNetwork2 object.</param>
        /// <param name="network2">The second IPNetwork2 object.</param>
        /// <returns>Returns true if the two IPNetwork2 objects overlap, otherwise false.</returns>
        [Obsolete("static Overlap is deprecated, please use instance Overlap.")]
        public static bool Overlap(IPNetwork2 network, IPNetwork2 network2)
        {
            if (network == null)
            {
                throw new ArgumentNullException("network");
            }

            return network.Overlap(network2);
        }

        #endregion

        #region ToString

        /// <summary>
        /// Returns a string representation of the object.
        /// </summary>
        /// <returns>
        /// A string representation of the object which includes the Network and Cidr values separated by a "/".
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}/{1}", this.Network, this.Cidr);
        }

        #endregion

        #region IANA block

        private static readonly Lazy<IPNetwork2> _iana_ablock_reserved = new Lazy<IPNetwork2>(() => IPNetwork2.Parse("10.0.0.0/8"));
        private static readonly Lazy<IPNetwork2> _iana_bblock_reserved = new Lazy<IPNetwork2>(() => IPNetwork2.Parse("172.16.0.0/12"));
        private static readonly Lazy<IPNetwork2> _iana_cblock_reserved = new Lazy<IPNetwork2>(() => IPNetwork2.Parse("192.168.0.0/16"));

        /// <summary>
        /// Gets 10.0.0.0/8.
        /// </summary>
        /// <returns>The IANA reserved IPNetwork 10.0.0.0/8.</returns>
        public static IPNetwork2 IANA_ABLK_RESERVED1
        {
            get
            {
                return _iana_ablock_reserved.Value;
            }
        }

        /// <summary>
        /// Gets 172.12.0.0/12.
        /// </summary>
        /// <returns>The IANA reserved IPNetwork 172.12.0.0/12.</returns>
        public static IPNetwork2 IANA_BBLK_RESERVED1
        {
            get
            {
                return _iana_bblock_reserved.Value;
            }
        }

        /// <summary>
        /// Gets 192.168.0.0/16.
        /// </summary>
        /// <returns>The IANA reserved IPNetwork 192.168.0.0/16.</returns>
        public static IPNetwork2 IANA_CBLK_RESERVED1
        {
            get
            {
                return _iana_cblock_reserved.Value;
            }
        }

        /// <summary>
        /// return true if ipaddress is contained in
        /// IANA_ABLK_RESERVED1, IANA_BBLK_RESERVED1, IANA_CBLK_RESERVED1.
        /// </summary>
        /// <param name="ipaddress">A string containing an ip address to convert.</param>
        /// <returns>true if ipaddress is a IANA reserverd IP Netowkr ; otherwise, false.</returns>
        public static bool IsIANAReserved(IPAddress ipaddress)
        {
            if (ipaddress == null)
            {
                throw new ArgumentNullException("ipaddress");
            }

            return IPNetwork2.IANA_ABLK_RESERVED1.Contains(ipaddress)
                || IPNetwork2.IANA_BBLK_RESERVED1.Contains(ipaddress)
                || IPNetwork2.IANA_CBLK_RESERVED1.Contains(ipaddress);
        }

        /// <summary>
        /// return true if ipnetwork is contained in
        /// IANA_ABLK_RESERVED1, IANA_BBLK_RESERVED1, IANA_CBLK_RESERVED1.
        /// </summary>
        /// <returns>true if the ipnetwork is a IANA reserverd IP Netowkr ; otherwise, false.</returns>
        public bool IsIANAReserved()
        {
            return IPNetwork2.IANA_ABLK_RESERVED1.Contains(this)
                || IPNetwork2.IANA_BBLK_RESERVED1.Contains(this)
                || IPNetwork2.IANA_CBLK_RESERVED1.Contains(this);
        }

        /// <summary>
        /// Determines whether the specified IP network is reserved according to the IANA Reserved ranges.
        /// </summary>
        /// <param name="ipnetwork">The IP network to check.</param>
        /// <returns>
        /// <c>true</c> if the specified IP network is reserved according to the IANA Reserved ranges; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method is obsolete and should not be used. Please use the instance method <see cref="IsIANAReserved"/> instead.
        /// </para>
        /// <para>
        /// Throws an <see cref="ArgumentNullException"/> if <paramref name="ipnetwork"/> is <c>null</c>.
        /// </para>
        /// </remarks>
        [Obsolete("static IsIANAReserved is deprecated, please use instance IsIANAReserved.")]
        public static bool IsIANAReserved(IPNetwork2 ipnetwork)
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
        /// Subnet 10.0.0.0/8 into cidr 9 gives 10.0.0.0/9, 10.128.0.0/9.
        /// </summary>
        /// <param name="cidr">A byte representing the CIDR to be used to subnet the current IPNetwork.</param>
        /// <returns>A IPNetworkCollection splitted by CIDR.</returns>
        public IPNetworkCollection Subnet(byte cidr)
        {
            IPNetwork2.InternalSubnet(false, this, cidr, out IPNetworkCollection ipnetworkCollection);

            return ipnetworkCollection;
        }

        /// <summary>
        /// Subnet method is used to divide the given IP network into subnets with the specified CIDR.
        /// </summary>
        /// <param name="network">The IP network to be subnetted.</param>
        /// <param name="cidr">The CIDR (Classless Inter-Domain Routing) value used to subnet the network.</param>
        /// <returns>
        /// A collection of subnets created from the given network using the specified CIDR.
        /// </returns>
        [Obsolete("static Subnet is deprecated, please use instance Subnet.")]
        public static IPNetworkCollection Subnet(IPNetwork2 network, byte cidr)
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
        /// Subnet 10.0.0.0/8 into cidr 9 gives 10.0.0.0/9, 10.128.0.0/9.
        /// </summary>
        /// <param name="cidr">A byte representing the CIDR to be used to subnet the current IPNetwork.</param>
        /// <param name="ipnetworkCollection">The resulting subnetted IPNetwork.</param>
        /// <returns>true if network was split successfully; otherwise, false.</returns>
        public bool TrySubnet(byte cidr, out IPNetworkCollection ipnetworkCollection)
        {
            IPNetwork2.InternalSubnet(true, this, cidr, out IPNetworkCollection inc);
            if (inc == null)
            {
                ipnetworkCollection = null;
                return false;
            }

            ipnetworkCollection = inc;
            return true;
        }

        /// <summary>
        /// Subnet a network into multiple nets of cidr mask
        /// Subnet 192.168.0.0/24 into cidr 25 gives 192.168.0.0/25, 192.168.0.128/25
        /// Subnet 10.0.0.0/8 into cidr 9 gives 10.0.0.0/9, 10.128.0.0/9.
        /// </summary>
        /// <param name="network"></param>
        /// <param name="cidr">A byte representing the CIDR to be used to subnet the current IPNetwork.</param>
        /// <param name="ipnetworkCollection">The resulting subnetted IPNetwork.</param>
        /// <returns>true if network was split successfully; otherwise, false.</returns>
        [Obsolete("static TrySubnet is deprecated, please use instance TrySubnet.")]
        public static bool TrySubnet(IPNetwork2 network, byte cidr, out IPNetworkCollection ipnetworkCollection)
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
            static void InternalSubnet(bool trySubnet, IPNetwork2 network, byte cidr, out IPNetworkCollection ipnetworkCollection)
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
        /// 192.168.0.0/24 + 192.168.0.0/25 = 192.168.0.0/24.
        /// </summary>
        /// <param name="network2">The network to supernet with.</param>
        /// <returns>A supernetted IP Network.</returns>
        public IPNetwork2 Supernet(IPNetwork2 network2)
        {
            IPNetwork2.InternalSupernet(false, this, network2, out IPNetwork2 supernet);
            return supernet;
        }

        /// <summary>
        /// Supernet two consecutive cidr equal subnet into a single one
        /// 192.168.0.0/24 + 192.168.1.0/24 = 192.168.0.0/23
        /// 10.1.0.0/16 + 10.0.0.0/16 = 10.0.0.0/15
        /// 192.168.0.0/24 + 192.168.0.0/25 = 192.168.0.0/24.
        /// </summary>
        /// <param name="network"></param>
        /// <param name="network2">The network to supernet with.</param>
        /// <returns>A supernetted IP Network.</returns>
        [Obsolete("static Supernet is deprecated, please use instance Supernet.")]
        public static IPNetwork2 Supernet(IPNetwork2 network, IPNetwork2 network2)
        {
            return network.Supernet(network2);
        }

        /// <summary>
        /// Try to supernet two consecutive cidr equal subnet into a single one
        /// 192.168.0.0/24 + 192.168.1.0/24 = 192.168.0.0/23
        /// 10.1.0.0/16 + 10.0.0.0/16 = 10.0.0.0/15
        /// 192.168.0.0/24 + 192.168.0.0/25 = 192.168.0.0/24.
        /// </summary>
        /// <param name="network2">The network to supernet with.</param>
        /// <param name="supernet">The resulting IPNetwork.</param>
        /// <returns>true if network2 was supernetted successfully; otherwise, false.</returns>
        public bool TrySupernet(IPNetwork2 network2, out IPNetwork2 supernet)
        {
            IPNetwork2.InternalSupernet(true, this, network2, out IPNetwork2 outSupernet);
            bool parsed = outSupernet != null;
            supernet = outSupernet;
            return parsed;
        }

        /// <summary>
        /// Try to supernet two consecutive cidr equal subnet into a single one
        /// 192.168.0.0/24 + 192.168.1.0/24 = 192.168.0.0/23
        /// 10.1.0.0/16 + 10.0.0.0/16 = 10.0.0.0/15
        /// 192.168.0.0/24 + 192.168.0.0/25 = 192.168.0.0/24.
        /// </summary>
        /// <param name="network"></param>
        /// <param name="network2">The network to supernet with.</param>
        /// <param name="supernet">The resulting IPNetwork.</param>
        /// <returns>true if network2 was supernetted successfully; otherwise, false.</returns>
        [Obsolete("static TrySupernet is deprecated, please use instance TrySupernet.")]
        public static bool TrySupernet(IPNetwork2 network, IPNetwork2 network2, out IPNetwork2 supernet)
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
            static void InternalSupernet(bool trySupernet, IPNetwork2 network1, IPNetwork2 network2, out IPNetwork2 supernet)
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
                supernet = new IPNetwork2(network1._network, network1._family, network1.Cidr);
                return;
            }

            if (network2.Contains(network1))
            {
                supernet = new IPNetwork2(network2._network, network2._family, network2.Cidr);
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

            IPNetwork2 first = (network1._network < network2._network) ? network1 : network2;
            IPNetwork2 last = (network1._network > network2._network) ? network1 : network2;

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
                    throw new ArgumentOutOfRangeException("network1");
                }

                supernet = null;
                return;
            }

            BigInteger uintSupernet = first._network;
            byte cidrSupernet = (byte)(first._cidr - 1);

            var networkSupernet = new IPNetwork2(uintSupernet, first._family, cidrSupernet);
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

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this._hashCode;
        }

        /// <summary>
        /// 20221105 : ldvhcosal
        /// GetHashCode uses mutable attributes. That introduce undefined behaviour on Hashtable and dictionary.
        /// </summary>
        /// <returns>An number representing the hashCode.</returns>
        private int ComputeHashCode()
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
        /// 192.168.0.0/24 + 192.168.1.0/24 + 192.168.2.0/24 + 192.168.3.0/24 = 192.168.0.0/22.
        /// </summary>
        /// <param name="ipnetworks">A list of IPNetwork to merge into common supernets.</param>
        /// <returns>The result of IPNetwork if merges succeed, the first ipnetwork otherwise.</returns>
        public static IPNetwork2[] Supernet(IPNetwork2[] ipnetworks)
        {
            InternalSupernet(false, ipnetworks, out IPNetwork2[] supernet);
            return supernet;
        }

        /// <summary>
        /// Supernet a list of subnet
        /// 192.168.0.0/24 + 192.168.1.0/24 = 192.168.0.0/23
        /// 192.168.0.0/24 + 192.168.1.0/24 + 192.168.2.0/24 + 192.168.3.0/24 = 192.168.0.0/22.
        /// </summary>
        /// <param name="ipnetworks">A list of IPNetwork to merge into common supernets.</param>
        /// <param name="supernet">The result of IPNetwork merges.</param>
        /// <returns>true if ipnetworks was supernetted successfully; otherwise, false.</returns>
        public static bool TrySupernet(IPNetwork2[] ipnetworks, out IPNetwork2[] supernet)
        {
            bool supernetted = InternalSupernet(true, ipnetworks, out supernet);
            return supernetted;
        }

#if TRAVISCI
        public
#else
        internal
#endif
        static bool InternalSupernet(bool trySupernet, IPNetwork2[] ipnetworks, out IPNetwork2[] supernet)
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
                supernet = new IPNetwork2[0];
                return true;
            }

            var supernetted = new List<IPNetwork2>();
            List<IPNetwork2> ipns = IPNetwork2.Array2List(ipnetworks);
            Stack<IPNetwork2> current = IPNetwork2.List2Stack(ipns);
            int previousCount = 0;
            int currentCount = current.Count;

            while (previousCount != currentCount)
            {
                supernetted.Clear();
                while (current.Count > 1)
                {
                    IPNetwork2 ipn1 = current.Pop();
                    IPNetwork2 ipn2 = current.Peek();

                    bool success = ipn1.TrySupernet(ipn2, out IPNetwork2 outNetwork);
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
                current = IPNetwork2.List2Stack(supernetted);
            }

            supernet = supernetted.ToArray();
            return true;
        }

        private static Stack<IPNetwork2> List2Stack(List<IPNetwork2> list)
        {
            var stack = new Stack<IPNetwork2>();
            list.ForEach(new Action<IPNetwork2>(
                delegate(IPNetwork2 ipn)
                {
                    stack.Push(ipn);
                }));
            return stack;
        }

        private static List<IPNetwork2> Array2List(IPNetwork2[] array)
        {
            var ipns = new List<IPNetwork2>();
            ipns.AddRange(array);
            IPNetwork2.RemoveNull(ipns);
            ipns.Sort(new Comparison<IPNetwork2>(
                delegate(IPNetwork2 ipn1, IPNetwork2 ipn2)
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

        private static void RemoveNull(List<IPNetwork2> ipns)
        {
            ipns.RemoveAll(new Predicate<IPNetwork2>(
                delegate(IPNetwork2 ipn)
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

        /// <summary>
        /// Finds the widest subnet that can contain both the start and end IP addresses.
        /// </summary>
        /// <param name="start">The starting IP address.</param>
        /// <param name="end">The ending IP address.</param>
        /// <returns>The widest subnet that contains both the start and end IP addresses.</returns>
        /// <exception cref="ArgumentNullException">Thrown when either the start or end IP address is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown when the start or end IP addresses are not valid.</exception>
        /// <exception cref="NotSupportedException">Thrown when the start and end IP addresses have different address families.</exception>
        public static IPNetwork2 WideSubnet(string start, string end)
        {
            if (string.IsNullOrEmpty(start))
            {
                throw new ArgumentNullException("start");
            }

            if (string.IsNullOrEmpty(end))
            {
                throw new ArgumentNullException("end");
            }

            if (!IPAddress.TryParse(start, out IPAddress startIP))
            {
                throw new ArgumentException("start");
            }

            if (!IPAddress.TryParse(end, out IPAddress endIP))
            {
                throw new ArgumentException("end");
            }

            if (startIP.AddressFamily != endIP.AddressFamily)
            {
                throw new NotSupportedException("MixedAddressFamily");
            }

            var ipnetwork = new IPNetwork2(0, startIP.AddressFamily, 0);
            for (byte cidr = 32; cidr >= 0; cidr--)
            {
                var wideSubnet = IPNetwork2.Parse(start, cidr);
                if (wideSubnet.Contains(endIP))
                {
                    ipnetwork = wideSubnet;
                    break;
                }
            }

            return ipnetwork;
        }

        /// <summary>
        /// Attempts to find the widest subnet that contains both the start and end IP addresses. objects.
        /// </summary>
        /// <param name="ipnetworks">An array of IPNetwork2 objects to wide subnet.</param>
        /// <param name="ipnetwork">When this method returns, contains the wide subnet of the IPNetwork2 objects, if wide subnet was successful; otherwise, null.</param>
        /// <returns>true if wide subnet was successful; otherwise, false.</returns>
        public static bool TryWideSubnet(IPNetwork2[] ipnetworks, out IPNetwork2 ipnetwork)
        {
            IPNetwork2.InternalWideSubnet(true, ipnetworks, out IPNetwork2 ipn);
            if (ipn == null)
            {
                ipnetwork = null;
                return false;
            }

            ipnetwork = ipn;

            return true;
        }

        /// <summary>
        /// Finds the widest subnet from an array of IP networks. </summary> <param name="ipnetworks">An array of IPNetwork2 objects representing the IP networks.</param> <returns>The widest subnet as an IPNetwork2 object.</returns>
        /// /
        public static IPNetwork2 WideSubnet(IPNetwork2[] ipnetworks)
        {
            IPNetwork2.InternalWideSubnet(false, ipnetworks, out IPNetwork2 ipn);
            return ipn;
        }

        internal static void InternalWideSubnet(bool tryWide, IPNetwork2[] ipnetworks, out IPNetwork2 ipnetwork)
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

            IPNetwork2[] nnin = Array.FindAll<IPNetwork2>(ipnetworks, new Predicate<IPNetwork2>(
                delegate(IPNetwork2 ipnet)
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
                IPNetwork2 ipn0 = nnin[0];
                ipnetwork = ipn0;
                return;
            }

            Array.Sort<IPNetwork2>(nnin);
            IPNetwork2 nnin0 = nnin[0];
            BigInteger uintNnin0 = nnin0._ipaddress;

            IPNetwork2 nninX = nnin[nnin.Length - 1];
            IPAddress ipaddressX = nninX.Broadcast;

            AddressFamily family = ipnetworks[0]._family;
            foreach (IPNetwork2 ipnx in ipnetworks)
            {
                if (ipnx._family != family)
                {
                    throw new ArgumentException("MixedAddressFamily");
                }
            }

            var ipn = new IPNetwork2(0, family, 0);
            for (byte cidr = nnin0._cidr; cidr >= 0; cidr--)
            {
                var wideSubnet = new IPNetwork2(uintNnin0, family, cidr);
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
        /// Print an ipnetwork in a clear representation string.
        /// </summary>
        /// <returns>Dump an IPNetwork representation as string.</returns>
        public string Print()
        {
            using (var sw = new StringWriter())
            {
                sw.WriteLine("IPNetwork   : {0}", this.ToString());
                sw.WriteLine("Network     : {0}", this.Network);
                sw.WriteLine("Netmask     : {0}", this.Netmask);
                sw.WriteLine("Cidr        : {0}", this.Cidr);
                sw.WriteLine("Broadcast   : {0}", this.Broadcast);
                sw.WriteLine("FirstUsable : {0}", this.FirstUsable);
                sw.WriteLine("LastUsable  : {0}", this.LastUsable);
                sw.WriteLine("Usable      : {0}", this.Usable);

                return sw.ToString();
            }
        }

        /// <summary>
        /// Print an ipnetwork in a clear representation string.
        /// </summary>
        /// <returns>Dump an IPNetwork representation as string.</returns>
        [Obsolete("static Print is deprecated, please use instance Print.")]
        public static string Print(IPNetwork2 ipnetwork)
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
        /// Delegate to CidrGuess ClassFull guessing of cidr.
        /// </summary>
        /// <param name="ip">A string representing an IPAdress that will be used to guess the corresponding CIDR.</param>
        /// <param name="cidr">The resulting CIDR as byte.</param>
        /// <returns>true if cidr was guessed successfully; otherwise, false.</returns>
        public static bool TryGuessCidr(string ip, out byte cidr)
        {
            return CidrGuess.ClassFull.TryGuessCidr(ip, out cidr);
        }

        /// <summary>
        /// Try to parse cidr. Have to be >= 0 and &lt;= 32 or 128.
        /// </summary>
        /// <param name="sidr">A string representing a byte CIRD (/24).</param>
        /// <param name="family">Either IPv4 or IPv6.</param>
        /// <param name="cidr">The resulting CIDR as byte.</param>
        /// <returns>true if cidr was converted successfully; otherwise, false.</returns>
        public static bool TryParseCidr(string sidr, AddressFamily family, out byte? cidr)
        {
            byte b = 0;
            if (!byte.TryParse(sidr, out b))
            {
                cidr = null;
                return false;
            }

            if (!IPNetwork2.TryToNetmask(b, family, out IPAddress netmask))
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
        /// List all ip addresses in a subnet.
        /// </summary>
        /// <param name="ipnetwork">The network to list IPAdresses.</param>
        /// <returns>All the IPAdresses contained in ipnetwork.</returns>
        [Obsolete("static ListIPAddress is deprecated, please use instance ListIPAddress.")]
        public static IPAddressCollection ListIPAddress(IPNetwork2 ipnetwork)
        {
            return ipnetwork.ListIPAddress();
        }

        /// <summary>
        /// List all ip addresses in a subnet.
        /// </summary>
        /// <param name="filter">Filter IPAdresses from IPNetwork.</param>
        /// <returns>The filterted IPAdresses contained in ipnetwork.</returns>
        public IPAddressCollection ListIPAddress(FilterEnum filter = FilterEnum.All)
        {
            return new IPAddressCollection(this, filter);
        }

        #endregion

        #region IComparable<IPNetwork> Members

        /// <summary>
        /// Compares two IPNetwork2 instances.
        /// </summary>
        /// <param name="left">The first IPNetwork2 instance to compare.</param>
        /// <param name="right">The second IPNetwork2 instance to compare.</param>
        /// <returns>
        /// A value indicating the relative order of the two IPNetwork2 instances.
        /// Zero if the instances are equal.
        /// A negative value if <paramref name="left"/> is less than <paramref name="right"/>.
        /// A positive value if <paramref name="left"/> is greater than <paramref name="right"/>.
        /// </returns>
        public static int Compare(IPNetwork2 left, IPNetwork2 right)
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
            int result = left._family.CompareTo(right._family);
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

        /// <summary>
        /// Compare two ipnetworks.
        /// </summary>
        /// <param name="other">The other network to compare to.</param>
        /// <returns>A signed number indicating the relative values of this instance and value..</returns>
        public int CompareTo(IPNetwork2 other)
        {
            return Compare(this, other);
        }

        /// <summary>
        /// Compare two ipnetworks.
        /// </summary>
        /// <param name="obj">The other object to compare to.</param>
        /// <returns>A signed number indicating the relative values of this instance and value..</returns>
        public int CompareTo(object obj)
        {
            // null is at less
            if (obj == null)
            {
                return 1;
            }

            // convert to a proper Cidr object
            var other = obj as IPNetwork2;

            // type problem if null
            if (other == null)
            {
                throw new ArgumentException(
                    "The supplied parameter is an invalid type. Please supply an IPNetwork type.",
                    "obj");
            }

            // perform the comparision
            return this.CompareTo(other);
        }

        #endregion

        #region IEquatable<IPNetwork> Members

        /// <summary>
        /// Compare two ipnetworks.
        /// </summary>
        /// <param name="left">An IPNetwork to compare.</param>
        /// <param name="right">An other IPNetwork to compare to.</param>
        /// <returns>true if obj has the same value as this instance; otherwise, false.</returns>
        public static bool Equals(IPNetwork2 left, IPNetwork2 right)
        {
            return Compare(left, right) == 0;
        }

        /// <summary>
        /// Compare two ipnetworks.
        /// </summary>
        /// <param name="other">An IPNetwork to compare to this instance.</param>
        /// <returns>true if obj has the same value as this instance; otherwise, false.</returns>
        public bool Equals(IPNetwork2 other)
        {
            return Equals(this, other);
        }

        /// <summary>
        /// Compare two ipnetworks.
        /// </summary>
        /// <param name="obj">An object value to compare to this instance.</param>
        /// <returns>true if obj has the same value as this instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return Equals(this, obj as IPNetwork2);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Compares two IPNetwork.
        /// </summary>
        /// <param name="left">left instance.</param>
        /// <param name="right">Right instance.</param>
        /// <returns>true if left equals right; otherwise, false.</returns>
        public static bool operator ==(IPNetwork2 left, IPNetwork2 right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Compares two IPNetwork.
        /// </summary>
        /// <param name="left">left instance.</param>
        /// <param name="right">Right instance.</param>
        /// <returns>true if left does not equals right; otherwise, false.</returns>
        public static bool operator !=(IPNetwork2 left, IPNetwork2 right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Compares two IPNetwork.
        /// </summary>
        /// <param name="left">left instance.</param>
        /// <param name="right">Right instance.</param>
        /// <returns>true if left is less than right; otherwise, false.</returns>
        public static bool operator <(IPNetwork2 left, IPNetwork2 right)
        {
            return Compare(left, right) < 0;
        }

        /// <summary>
        /// Compares two IPNetwork.
        /// </summary>
        /// <param name="left">left instance.</param>
        /// <param name="right">Right instance.</param>
        /// <returns>true if left is greater than right; otherwise, false.</returns>
        public static bool operator >(IPNetwork2 left, IPNetwork2 right)
        {
            return Compare(left, right) > 0;
        }

        #endregion

        #region XmlSerialization

        /// <summary>
        /// Initializes a new instance of the <see cref="IPNetwork2"/> class.
        /// Created for DataContractSerialization. Better use static methods IPNetwork.Parse() to create IPNetworks.
        /// </summary>
        public IPNetwork2()
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

        private IPNetwork2(SerializationInfo info, StreamingContext context)
        {
            string sipnetwork = (string)info.GetValue("IPNetwork", typeof(string));
            var ipnetwork = IPNetwork2.Parse(sipnetwork);

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
        /// Gets netmask Inverse
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
        /// /0    0.0.0.0            255.255.255.255.
        ///
        /// </summary>
        public IPAddress WildcardMask
        {
            get
            {
                byte cidr = this._family == AddressFamily.InterNetwork ? (byte)32 : (byte)128;
                BigInteger netmask = IPNetwork2.ToUint(cidr, this._family);
                BigInteger wildcardmask = netmask - this._netmask;

                return IPNetwork2.ToIPAddress(wildcardmask, this._family);
            }
        }
        #endregion
    }
}
