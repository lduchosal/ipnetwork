namespace TestProject.IPNetworkTest;

/// <summary>
/// Tests about the ParseRange
/// </summary>
[TestClass]
public class IPNetworkParseRangeTests
{
    /// <summary>
    /// Test the ParseRange
    /// </summary>
    /// <param name="range"></param>
    /// <param name="expected"></param>
    [TestMethod]
    [DataRow("192.168.1.45 - 192.168.1.65", "192.168.1.45/32, 192.168.1.46/31, 192.168.1.48/28, 192.168.1.64/31")]
    [DataRow("192.168.1.32 - 192.168.1.63", "192.168.1.32/27")]
    [DataRow("192.168.0.1 - 192.168.0.1", "192.168.0.1/32")]
    [DataRow("192.168.0.0 - 192.168.0.0", "192.168.0.0/32")]
    [DataRow("192.168.0.0 - 192.168.0.1", "192.168.0.0/31")]
    [DataRow("192.168.0.0 - 192.168.0.3", "192.168.0.0/30")]
    [DataRow("192.168.0.0 - 192.168.0.7", "192.168.0.0/29")]
    [DataRow("192.168.0.0 - 192.168.0.15", "192.168.0.0/28")]
    [DataRow("192.168.0.0 - 192.168.0.31", "192.168.0.0/27")]
    [DataRow("192.168.0.0 - 192.168.0.63", "192.168.0.0/26")]
    [DataRow("192.168.0.0 - 192.168.0.127", "192.168.0.0/25")]
    [DataRow("192.168.0.0 - 192.168.0.255", "192.168.0.0/24")]
    [DataRow("192.168.0.0 - 192.168.1.255", "192.168.0.0/23")]
    [DataRow("192.168.0.0 - 192.168.3.255", "192.168.0.0/22")]
    [DataRow("192.168.0.0 - 192.168.7.255", "192.168.0.0/21")]
    [DataRow("192.168.0.0 - 192.168.15.255", "192.168.0.0/20")]
    [DataRow("192.168.0.0 - 192.168.31.255", "192.168.0.0/19")]
    [DataRow("192.168.0.0 - 192.168.63.255", "192.168.0.0/18")]
    [DataRow("192.168.0.0 - 192.168.127.255", "192.168.0.0/17")]
    [DataRow("192.168.0.0 - 192.168.255.255", "192.168.0.0/16")]
    [DataRow("0.0.0.0 - 127.255.255.255", "0.0.0.0/1")]
    [DataRow("0.0.0.0 - 128.0.0.0", "0.0.0.0/1, 128.0.0.0/32")]
    public void TestParseRange(string range, string expected)
    {
        var result = IPNetwork2.ParseRange(range);
        string sresult = string.Join(", ", result);
        Assert.AreEqual(expected, sresult);
    }
    
    /// <summary>
    /// Test the ParseRange
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="expected"></param>
    [TestMethod]
    [DataRow("192.168.1.45", "192.168.1.65", "192.168.1.45/32, 192.168.1.46/31, 192.168.1.48/28, 192.168.1.64/31")]
    [DataRow("192.168.1.32", "192.168.1.63", "192.168.1.32/27")]
    [DataRow("192.168.0.1", "192.168.0.1", "192.168.0.1/32")]
    [DataRow("192.168.0.0", "192.168.0.0", "192.168.0.0/32")]
    [DataRow("192.168.0.0", "192.168.0.1", "192.168.0.0/31")]
    [DataRow("192.168.0.0", "192.168.0.3", "192.168.0.0/30")]
    [DataRow("192.168.0.0", "192.168.0.7", "192.168.0.0/29")]
    [DataRow("192.168.0.0", "192.168.0.15", "192.168.0.0/28")]
    [DataRow("192.168.0.0", "192.168.0.31", "192.168.0.0/27")]
    [DataRow("192.168.0.0", "192.168.0.63", "192.168.0.0/26")]
    [DataRow("192.168.0.0", "192.168.0.127", "192.168.0.0/25")]
    [DataRow("192.168.0.0", "192.168.0.255", "192.168.0.0/24")]
    [DataRow("192.168.0.0", "192.168.1.255", "192.168.0.0/23")]
    [DataRow("192.168.0.0", "192.168.3.255", "192.168.0.0/22")]
    [DataRow("192.168.0.0", "192.168.7.255", "192.168.0.0/21")]
    [DataRow("192.168.0.0", "192.168.15.255", "192.168.0.0/20")]
    [DataRow("192.168.0.0", "192.168.31.255", "192.168.0.0/19")]
    [DataRow("192.168.0.0", "192.168.63.255", "192.168.0.0/18")]
    [DataRow("192.168.0.0", "192.168.127.255", "192.168.0.0/17")]
    [DataRow("192.168.0.0", "192.168.255.255", "192.168.0.0/16")]
    [DataRow("0.0.0.0", "127.255.255.255", "0.0.0.0/1")]
    [DataRow("0.0.0.0", "128.0.0.0", "0.0.0.0/1, 128.0.0.0/32")]
    public void TestParseRange(string start, string end, string expected)
    {
        var result = IPNetwork2.ParseRange(start, end);
        string sresult = string.Join(", ", result);
        Assert.AreEqual(expected, sresult);
    }

    /// <summary>
    /// Test the ParseRange
    /// </summary>
    /// <param name="range"></param>
    [TestMethod]
    [DataRow("")]
    public void TestParseRange_ArgumentNull(string range)
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => IPNetwork2.ParseRange(range));
    }

    /// <summary>
    /// Test the ParseRange
    /// </summary>
    /// <param name="range"></param>
    [TestMethod]
    [DataRow("start - end")]
    [DataRow("218763123 - 1239012")]
    public void TestParseRange_Argument(string range)
    {
        Assert.ThrowsExactly<ArgumentException>(() => IPNetwork2.ParseRange(range));
    }

    /// <summary>
    /// Test the ParseRange
    /// </summary>
    /// <param name="range"></param>
    [TestMethod]
    [DataRow("empty")]
    [DataRow("failed")]
    [DataRow("start - ")]
    [DataRow("start - buggy - toomuch")]
    public void TestParseRange_ArgumentOutOfRange(string range)
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => IPNetwork2.ParseRange(range));
    }

    /// <summary>
    /// Test the TryParseRange
    /// </summary>
    /// <param name="range"></param>
    /// <param name="expected"></param>
    [TestMethod]
    [DataRow("192.168.1.45 - 192.168.1.65", "192.168.1.45/32, 192.168.1.46/31, 192.168.1.48/28, 192.168.1.64/31")]
    [DataRow("192.168.1.32 - 192.168.1.63", "192.168.1.32/27")]
    [DataRow("192.168.0.1 - 192.168.0.1", "192.168.0.1/32")]
    [DataRow("192.168.0.0 - 192.168.0.0", "192.168.0.0/32")]
    [DataRow("192.168.0.0 - 192.168.0.1", "192.168.0.0/31")]
    [DataRow("192.168.0.0 - 192.168.0.3", "192.168.0.0/30")]
    [DataRow("192.168.0.0 - 192.168.0.7", "192.168.0.0/29")]
    [DataRow("192.168.0.0 - 192.168.0.15", "192.168.0.0/28")]
    [DataRow("192.168.0.0 - 192.168.0.31", "192.168.0.0/27")]
    [DataRow("192.168.0.0 - 192.168.0.63", "192.168.0.0/26")]
    [DataRow("192.168.0.0 - 192.168.0.127", "192.168.0.0/25")]
    [DataRow("192.168.0.0 - 192.168.0.255", "192.168.0.0/24")]
    [DataRow("192.168.0.0 - 192.168.1.255", "192.168.0.0/23")]
    [DataRow("192.168.0.0 - 192.168.3.255", "192.168.0.0/22")]
    [DataRow("192.168.0.0 - 192.168.7.255", "192.168.0.0/21")]
    [DataRow("192.168.0.0 - 192.168.15.255", "192.168.0.0/20")]
    [DataRow("192.168.0.0 - 192.168.31.255", "192.168.0.0/19")]
    [DataRow("192.168.0.0 - 192.168.63.255", "192.168.0.0/18")]
    [DataRow("192.168.0.0 - 192.168.127.255", "192.168.0.0/17")]
    [DataRow("192.168.0.0 - 192.168.255.255", "192.168.0.0/16")]
    [DataRow("0.0.0.0 - 127.255.255.255", "0.0.0.0/1")]
    [DataRow("0.0.0.0 - 128.0.0.0", "0.0.0.0/1, 128.0.0.0/32")]
    public void TestTryParseRange(string range, string expected)
    {
        bool parsed = IPNetwork2.TryParseRange(range, out var result);
        string sresult = string.Join(", ", result);
        Assert.IsTrue(parsed);
        Assert.AreEqual(expected, sresult);
    }
    
    /// <summary>
    /// Test the TryParseRange
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="expected"></param>
    [TestMethod]
    [DataRow("192.168.1.45", "192.168.1.65", "192.168.1.45/32, 192.168.1.46/31, 192.168.1.48/28, 192.168.1.64/31")]
    [DataRow("192.168.1.32", "192.168.1.63", "192.168.1.32/27")]
    [DataRow("192.168.0.1", "192.168.0.1", "192.168.0.1/32")]
    [DataRow("192.168.0.0", "192.168.0.0", "192.168.0.0/32")]
    [DataRow("192.168.0.0", "192.168.0.1", "192.168.0.0/31")]
    [DataRow("192.168.0.0", "192.168.0.3", "192.168.0.0/30")]
    [DataRow("192.168.0.0", "192.168.0.7", "192.168.0.0/29")]
    [DataRow("192.168.0.0", "192.168.0.15", "192.168.0.0/28")]
    [DataRow("192.168.0.0", "192.168.0.31", "192.168.0.0/27")]
    [DataRow("192.168.0.0", "192.168.0.63", "192.168.0.0/26")]
    [DataRow("192.168.0.0", "192.168.0.127", "192.168.0.0/25")]
    [DataRow("192.168.0.0", "192.168.0.255", "192.168.0.0/24")]
    [DataRow("192.168.0.0", "192.168.1.255", "192.168.0.0/23")]
    [DataRow("192.168.0.0", "192.168.3.255", "192.168.0.0/22")]
    [DataRow("192.168.0.0", "192.168.7.255", "192.168.0.0/21")]
    [DataRow("192.168.0.0", "192.168.15.255", "192.168.0.0/20")]
    [DataRow("192.168.0.0", "192.168.31.255", "192.168.0.0/19")]
    [DataRow("192.168.0.0", "192.168.63.255", "192.168.0.0/18")]
    [DataRow("192.168.0.0", "192.168.127.255", "192.168.0.0/17")]
    [DataRow("192.168.0.0", "192.168.255.255", "192.168.0.0/16")]
    [DataRow("0.0.0.0", "127.255.255.255", "0.0.0.0/1")]
    [DataRow("0.0.0.0", "128.0.0.0", "0.0.0.0/1, 128.0.0.0/32")]
    public void TestTryParseRange(string start, string end, string expected)
    {
        bool parsed = IPNetwork2.TryParseRange(start, end, out var result);
        string sresult = string.Join(", ", result);
        Assert.IsTrue(parsed);
        Assert.AreEqual(expected, sresult);
    }

    /// <summary>
    /// Test the InternalParseRange
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="expected"></param>
    [TestMethod]
    [DataRow("0.0.0.0", "127.255.255.255", "0.0.0.0/1")]
    [DataRow("0.0.0.0", "128.0.0.0", "0.0.0.0/1, 128.0.0.0/32")]
    public void TestInternalParseRange(string start, string end, string expected)
    {
        bool parsed = IPNetwork2.InternalParseRange(true, start, end, out var result);
        string sresult = string.Join(", ", result);
        Assert.IsTrue(parsed);
        Assert.AreEqual(expected, sresult);
    }

    /// <summary>
    /// Test the InternalParseRange
    /// </summary>
    /// <param name="range"></param>
    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    public void TestInternalParseRangeFalse4(string range)
    {
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            IPNetwork2.InternalParseRange(false, range, out var _);
        });
    }

    /// <summary>
    /// Test the InternalParseRange
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    [TestMethod]
    [DataRow(null, "128.0.0.0")]
    [DataRow("0.0.0.0", null)]
    [DataRow("0.0.0.0", "::1")]
    public void TestInternalParseRangeFalse6(string start, string end)
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
        {
            IPNetwork2.InternalParseRange(false, start, end, out var _);
        });
    }
    /// <summary>
    /// Test the InternalParseRange
    /// </summary>
    /// <param name="range"></param>
    [TestMethod]
    [DataRow("null - 128.0.0.0")]
    [DataRow("0.0.0.0 - null")]
    [DataRow("0.0.0.0 - ::1")]
    [DataRow("::1 - 0.0.0.0")]
    [DataRow("")]
    [DataRow("1.1.1.1 - 2.2.2.2 - 3.3.3.3")]
    [DataRow("1.1.1.1 - 2.2.2.2 - 3.3.3.3 - 4.4.4.4")]
    public void TestInternalParseRangeTrue(string range)
    { 
        bool parsed = IPNetwork2.InternalParseRange(true, range, out var _);
        Assert.IsFalse(parsed);
    }

    /// <summary>
    /// Test the InternalParseRange
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    [TestMethod]
    [DataRow(null, "128.0.0.0")]
    [DataRow("0.0.0.0", null)]
    [DataRow("0.0.0.0", "::1")]
    public void TestInternalParseRangeTrue(string start, string end)
    {
        bool parsed = IPNetwork2.InternalParseRange(true, start, end, out var _);
        Assert.IsFalse(parsed);
    }

    /// <summary>
    /// Test the InternalParseRange
    /// </summary>
    [TestMethod]
    public void TestInternalParseRangeTrue()
    {
        IPAddress start = null;
        IPAddress end = null;
        bool parsed = IPNetwork2.InternalParseRange(true, start, end, out var _);
        Assert.IsFalse(parsed);
    }

    /// <summary>
    /// Test the InternalParseRange
    /// </summary>
    [TestMethod]
    public void TestInternalParseRangeFalse5()
    {
        IPAddress start = null;
        IPAddress end = null;
        Assert.ThrowsExactly<ArgumentNullException>(()=> IPNetwork2.InternalParseRange(false, start, end, out _));
    }

    /// <summary>
    /// Test the InternalParseRange
    /// </summary>
    [TestMethod]
    public void TestInternalParseRangeFalse2()
    {
        IPAddress start = IPAddress.Loopback;
        IPAddress end = null;
        Assert.ThrowsExactly<ArgumentNullException>(()=> IPNetwork2.InternalParseRange(false, start, end, out _));
    }

    /// <summary>
    /// Test the InternalParseRange
    /// </summary>
    [TestMethod]
    public void TestInternalParseRangeTrue2()
    {
        IPAddress start = IPAddress.Loopback;
        IPAddress end = null;
        bool parsed = IPNetwork2.InternalParseRange(true, start, end, out _);
        Assert.IsFalse(parsed);
    }

    /// <summary>
    /// Test the InternalParseRange
    /// </summary>
    /// <param name="range"></param>
    [TestMethod]
    [DataRow("null - 128.0.0.0")]
    [DataRow("0.0.0.0 - null")]
    [DataRow("0.0.0.0 - ::1")]
    [DataRow("::1 - 0.0.0.0")]
    public void TestInternalParseRangeFalse(string range)
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
        {
            IPNetwork2.InternalParseRange(false, range, out var _);
        });
    }
    
    /// <summary>
    /// Test the InternalParseRange
    /// </summary>
    /// <param name="range"></param>
    [TestMethod]
    [DataRow("1.1.1.1 - 2.2.2.2 - 3.3.3.3")]
    public void TestInternalParseRangeFalse3(string range)
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
        {
            IPNetwork2.InternalParseRange(false, range, out var _);
        });
    }

    /// <summary>
    /// Test the InternalParseRange
    /// </summary>
    [TestMethod]
    public void TestInternalParseRangeTrueAddressFamily()
    {
        IPAddress start = IPAddress.Parse("0.0.0.0");
        IPAddress end = IPAddress.Parse("::1");
        bool parsed = IPNetwork2.InternalParseRange(true, start, end, out var _);
        Assert.IsFalse(parsed);
    }

    /// <summary>
    /// Test the InternalParseRange
    /// </summary>
    [TestMethod]
    public void TestInternalParseRangeFalseAddressFamily()
    {
        IPAddress start = IPAddress.Parse("0.0.0.0");
        IPAddress end = IPAddress.Parse("::1");
        Assert.ThrowsExactly<ArgumentException>(()=> IPNetwork2.InternalParseRange(false, start, end, out var _));
    }

    /// <summary>
    /// Test the InternalParseRange
    /// </summary>
    /// <param name="range"></param>
    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    public void TestInternalParseRangeFalse_Null(string range)
    {
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            IPNetwork2.InternalParseRange(false, range, out var _);
        });
    }
}