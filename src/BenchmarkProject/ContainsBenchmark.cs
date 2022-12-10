
namespace BenchmarkProject
{
    using BenchmarkDotNet.Attributes;
    using System.Net;

    public class ContainsBenchmark
    {
        private const int N = 10000;
        private readonly IPAddress _ipaddress = IPAddress.Parse("1.1.1.1");
        private readonly IPNetwork _ipnetwork = IPNetwork.Parse("0.0.0.0/0");
        private readonly IPNetwork _ipnetwork2 = IPNetwork.Parse("10.0.0.0/8");

        public ContainsBenchmark()
        {
        }

        [Benchmark]
        public bool ContainsIPAddressV1() => _ipnetwork.Contains(_ipaddress);

        [Benchmark]
        public bool ContainsIPAddressV2() => _ipnetwork.Contains2(_ipaddress);

        [Benchmark]
        public bool ContainsIPNetworkV1() => _ipnetwork.Contains(_ipnetwork2);

        [Benchmark]
        public bool ContainsIPNetworkV2() => _ipnetwork.Contains2(_ipnetwork2);
        [Benchmark]
        public void ContainsIPNetworkAllV1() { foreach(var ip in this._ipnetwork2.ListIPAddress() ) { _ipnetwork.Contains(ip); } }

        [Benchmark]
        public void ContainsIPNetworkAllV2() { foreach (var ip in this._ipnetwork2.ListIPAddress()) { _ipnetwork.Contains(ip); } }
    }
}
