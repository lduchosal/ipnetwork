using System.Net.Sockets;

namespace System.Net
{

    public sealed class CidrClassLess : ICidrGuess
    {
        /// <summary>
        /// 
        /// IPV4 : 32
        /// IPV6 : 128
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="cidr"></param>
        /// <returns></returns>
        public bool TryGuessCidr(string ip, out byte cidr)
        {

            IPAddress ipaddress = null;
            bool parsed = IPAddress.TryParse(string.Format("{0}", ip), out ipaddress);
            if (parsed == false)
            {
                cidr = 0;
                return false;
            }

            if (ipaddress.AddressFamily == AddressFamily.InterNetworkV6)
            {
                cidr = 128;
                return true;
            }
            cidr = 32;
            return true;
        }
    }
}
