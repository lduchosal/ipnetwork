namespace TestProject;

/// <summary>
///     Test IPAddress Behaviour on MacOS
/// </summary>
[TestClass]
public class IPAddressTest
{
    /// <summary>
    ///     Mixed IPv6 / IPvç notation
    ///     Why This Happens
    ///     .NET automatically uses mixed notation for IPv6 addresses when:
    ///     The first 96 bits are zero (IPv4-compatible)
    ///     The first 80 bits are zero and bits 81-96 are either all zeros or all ones (IPv4-mapped)
    ///     The pattern suggests an embedded IPv4 address
    ///     Your bytes ::0.1.0.0 are being interpreted as an IPv4-compatible IPv6 address.
    /// </summary>
    [TestMethod]
    public void TestIPAddress()
    {
        byte[] bytes = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0];
        var ip = new IPAddress(bytes);
        Assert.AreEqual("::0.1.0.0", ip.ToString());
    }
}