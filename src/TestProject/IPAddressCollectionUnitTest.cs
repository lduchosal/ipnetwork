// <copyright file="IPAddressCollectionUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

/// <summary>
///     IPNetworkUnitTest test every single method.
/// </summary>
[TestClass]
public class IPAddressCollectionUnitTest
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_Usable_AtIndexIPAddress()
    {
        var ipn = IPNetwork2.Parse("192.168.1.0/29");
        using IPAddressCollection ips = ipn.ListIPAddress(Filter.Usable);
        Assert.AreEqual("192.168.1.1", ips[0].ToString(), "0");
        Assert.AreEqual("192.168.1.2", ips[1].ToString(), "1");
        Assert.AreEqual("192.168.1.3", ips[2].ToString(), "2");
        Assert.AreEqual("192.168.1.4", ips[3].ToString(), "3");
        Assert.AreEqual("192.168.1.5", ips[4].ToString(), "4");
        Assert.AreEqual("192.168.1.6", ips[5].ToString(), "5");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_Usable_IteratorIPAddress()
    {
        var ipn = IPNetwork2.Parse("192.168.1.0/29");
        using IPAddressCollection ips = ipn.ListIPAddress(Filter.Usable);
        int i = 0;
        foreach (IPAddress ip in ips)
        {
            Assert.AreEqual(ips[i], ip, i.ToString());
            i++;
        }
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_Usable_AtIndexIPAddress_31()
    {
        var ipn = IPNetwork2.Parse("192.168.1.0/31");
        using IPAddressCollection ips = ipn.ListIPAddress(Filter.Usable);
        Assert.AreEqual(0, ips.Count, "Count");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_Usable_AtIndexIPAddress_32()
    {
        var ipn = IPNetwork2.Parse("192.168.1.0/32");
        using IPAddressCollection ips = ipn.ListIPAddress(Filter.Usable);
        Assert.AreEqual(0, ips.Count, "Count");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_All_AtIndexIPAddress()
    {
        var ipn = IPNetwork2.Parse("192.168.1.0/29");
        using IPAddressCollection ips = ipn.ListIPAddress();
        Assert.AreEqual("192.168.1.0", ips[0].ToString(), "0");
        Assert.AreEqual("192.168.1.1", ips[1].ToString(), "1");
        Assert.AreEqual("192.168.1.2", ips[2].ToString(), "2");
        Assert.AreEqual("192.168.1.3", ips[3].ToString(), "3");
        Assert.AreEqual("192.168.1.4", ips[4].ToString(), "4");
        Assert.AreEqual("192.168.1.5", ips[5].ToString(), "5");
        Assert.AreEqual("192.168.1.6", ips[6].ToString(), "6");
        Assert.AreEqual("192.168.1.7", ips[7].ToString(), "6");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestAtIndexIPAddress()
    {
        var ipn = IPNetwork2.Parse("192.168.1.0/29");
        using IPAddressCollection ips = ipn.ListIPAddress();
        Assert.AreEqual("192.168.1.0", ips[0].ToString(), "0");
        Assert.AreEqual("192.168.1.1", ips[1].ToString(), "1");
        Assert.AreEqual("192.168.1.2", ips[2].ToString(), "2");
        Assert.AreEqual("192.168.1.3", ips[3].ToString(), "3");
        Assert.AreEqual("192.168.1.4", ips[4].ToString(), "4");
        Assert.AreEqual("192.168.1.5", ips[5].ToString(), "5");
        Assert.AreEqual("192.168.1.6", ips[6].ToString(), "6");
        Assert.AreEqual("192.168.1.7", ips[7].ToString(), "7");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestIterateIPAddress()
    {
        var ipn = IPNetwork2.Parse("192.168.1.0/29");
        IPAddress last = null;
        IPAddress first = null;
        int count = 0;
        using IPAddressCollection ips = ipn.ListIPAddress();
        foreach (IPAddress ip in ips)
        {
            first ??= ip;
            last = ip;
            count++;
        }

        object current = ((IEnumerator)ips).Current;
        ((IEnumerator)ips).Reset();

        Assert.IsNotNull(current, "current is no null");
        Assert.IsNotNull(last, "last is null");
        Assert.IsNotNull(first, "first is null");
        Assert.AreEqual("192.168.1.0", first.ToString(), "first");
        Assert.AreEqual("192.168.1.7", last.ToString(), "last");
        Assert.AreEqual(8, count, "count");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestOutOfRangeIPAddress()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
        {
            var ipn = IPNetwork2.Parse("192.168.1.0/29");
            using IPAddressCollection ips = ipn.ListIPAddress();
            Console.Write("This is out of range : {0} ", ips[8]);
        });
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestCountIPAddress()
    {
        var ipn = IPNetwork2.Parse("192.168.1.0/29");
        using IPAddressCollection ips = ipn.ListIPAddress();
        Assert.HasCount(8, ips, "Count");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestReset()
    {
        var ipn = IPNetwork2.Parse("192.168.1.0/29");
        using IPAddressCollection ips = ipn.ListIPAddress();
        ips.Reset();
        Assert.HasCount(8, ips, "Count");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestResetEnumerator()
    {
        var ipn = IPNetwork2.Parse("192.168.1.0/29");
        using IPAddressCollection ips = ipn.ListIPAddress();
        ips.Reset();
        while (ips.MoveNext())
        {
            Assert.IsNotNull(ips.Current);
        }

        ips.Reset();
        while (ips.MoveNext())
        {
            Assert.IsNotNull(ips.Current);
        }
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestEnumeratorFailed()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
        {
            var ipn = IPNetwork2.Parse("192.168.1.0/29");
            using IPAddressCollection ips = ipn.ListIPAddress();
            ips.Reset();
            while (ips.MoveNext())
            {
                Assert.IsNotNull(ips.Current);
            }

            Console.WriteLine("This is out of range : {0}", ips.Current);
        });
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestEnumeratorMoveNext()
    {
        var ipn = IPNetwork2.Parse("192.168.1.0/29");
        using IPAddressCollection ips = ipn.ListIPAddress();
        Assert.IsTrue(ips.MoveNext());
        Assert.IsTrue(ips.MoveNext());
        Assert.IsTrue(ips.MoveNext());
        Assert.IsTrue(ips.MoveNext());
        Assert.IsTrue(ips.MoveNext());
        Assert.IsTrue(ips.MoveNext());
        Assert.IsTrue(ips.MoveNext());
        Assert.IsTrue(ips.MoveNext());
        Assert.IsFalse(ips.MoveNext());
        Assert.IsFalse(ips.MoveNext());
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestEnumeratorMoveNext2()
    {
        var ipn = IPNetwork2.Parse("192.168.1.0/31");
        using IPAddressCollection ips = ipn.ListIPAddress();
        Assert.IsTrue(ips.MoveNext());
        Assert.IsTrue(ips.MoveNext());
        Assert.IsFalse(ips.MoveNext());
        ips.Reset();
        Assert.IsTrue(ips.MoveNext());
        Assert.IsTrue(ips.MoveNext());
        Assert.IsFalse(ips.MoveNext());
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestEnumerableCurrent()
    {
        var ipn = IPNetwork2.Parse("192.168.1.0/31");
        IEnumerable ips = ipn.ListIPAddress();
        var enumerator = ips.GetEnumerator();
        Assert.IsNotNull(enumerator);
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestEnumeratorCurrent()
    {
        var ipn = IPNetwork2.Parse("192.168.1.0/31");
        IEnumerator ips = ipn.ListIPAddress();
        Assert.IsNotNull(ips.Current);
        Assert.IsTrue(ips.MoveNext());
        Assert.IsNotNull(ips.Current);
        Assert.IsTrue(ips.MoveNext());
        Assert.IsFalse(ips.MoveNext());
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestEnumeratorCurrentOor()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
        {
            var ipn = IPNetwork2.Parse("192.168.1.0/31");
            IEnumerator ips = ipn.ListIPAddress();
            Assert.IsNotNull(ips.Current);
            Assert.IsTrue(ips.MoveNext());
            Assert.IsNotNull(ips.Current);
            Assert.IsTrue(ips.MoveNext());
            Assert.IsFalse(ips.MoveNext());
            Console.WriteLine("This is out of range : {0} ", ips.Current);
        });
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void TestEnumeratorIterate()
    {
        var ipn = IPNetwork2.Parse("192.168.1.0/31");
        IEnumerator ips = ipn.ListIPAddress();
        while (ips.MoveNext())
        {
            Assert.IsNotNull(ips.Current);
        }
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_ipv6_AtIndexIPAddress()
    {
        var ipn = IPNetwork2.Parse("::/125");
        using IPAddressCollection ips = ipn.ListIPAddress();
        Assert.AreEqual("::", ips[0].ToString(), "0");
        Assert.AreEqual("::1", ips[1].ToString(), "1");
        Assert.AreEqual("::2", ips[2].ToString(), "2");
        Assert.AreEqual("::3", ips[3].ToString(), "3");
        Assert.AreEqual("::4", ips[4].ToString(), "4");
        Assert.AreEqual("::5", ips[5].ToString(), "5");
        Assert.AreEqual("::6", ips[6].ToString(), "6");
        Assert.AreEqual("::7", ips[7].ToString(), "7");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_ipv6_IterateIPAddress()
    {
        var ipn = IPNetwork2.Parse("::/125");
        IPAddress last = null;
        IPAddress first = null;
        int count = 0;
        using IPAddressCollection ips = ipn.ListIPAddress();
        foreach (IPAddress ip in ips)
        {
            first ??= ip;
            last = ip;
            count++;
        }

        Assert.IsNotNull(last, "last is null");
        Assert.IsNotNull(first, "first is null");
        Assert.AreEqual("::", first.ToString(), "first");
        Assert.AreEqual("::7", last.ToString(), "last");
        Assert.AreEqual(8, count, "count");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_ipv6_OutOfRangeIPAddress()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
        {
            var ipn = IPNetwork2.Parse("::/125");
            using IPAddressCollection ips = ipn.ListIPAddress();
            Console.Write("This is out of range : {0} ", ips[8]);
        });
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_ipv6_CountIPAddress()
    {
        var ipn = IPNetwork2.Parse("::/125");
        using IPAddressCollection ips = ipn.ListIPAddress();
        Assert.AreEqual(8, ips.Count, "Count");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_ipv6_CountIPAddress2()
    {
        var ipn = IPNetwork2.Parse("::/0");
        var max = BigInteger.Pow(2, 128);
        using IPAddressCollection ips = ipn.ListIPAddress();
        Assert.AreEqual(max, ips.Count, "Count");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_ipv6_Reset()
    {
        var ipn = IPNetwork2.Parse("::/125");
        using IPAddressCollection ips = ipn.ListIPAddress();
        ips.Reset();
        Assert.AreEqual(8, ips.Count, "Count");
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Tes_ipv6_tResetEnumerator()
    {
        var ipn = IPNetwork2.Parse("::/125");
        using IPAddressCollection ips = ipn.ListIPAddress();
        ips.Reset();
        while (ips.MoveNext())
        {
            Assert.IsNotNull(ips.Current);
        }

        ips.Reset();
        while (ips.MoveNext())
        {
            Assert.IsNotNull(ips.Current);
        }
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_ipv6_EnumeratorFailed()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
        {
            var ipn = IPNetwork2.Parse("::/125");
            using IPAddressCollection ips = ipn.ListIPAddress();
            ips.Reset();
            while (ips.MoveNext())
            {
                Assert.IsNotNull(ips.Current);
            }
            Console.WriteLine("This is out of range : {0}", ips.Current);
        });
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_ipv6_EnumeratorMoveNext()
    {
        var ipn = IPNetwork2.Parse("::/125");
        using IPAddressCollection ips = ipn.ListIPAddress();
        Assert.IsTrue(ips.MoveNext());
        Assert.IsTrue(ips.MoveNext());
        Assert.IsTrue(ips.MoveNext());
        Assert.IsTrue(ips.MoveNext());
        Assert.IsTrue(ips.MoveNext());
        Assert.IsTrue(ips.MoveNext());
        Assert.IsTrue(ips.MoveNext());
        Assert.IsTrue(ips.MoveNext());
        Assert.IsFalse(ips.MoveNext());
        Assert.IsFalse(ips.MoveNext());
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_ipv6_EnumeratorMoveNext2()
    {
        var ipn = IPNetwork2.Parse("::/127");
        using IPAddressCollection ips = ipn.ListIPAddress();
        Assert.IsTrue(ips.MoveNext());
        Assert.IsTrue(ips.MoveNext());
        Assert.IsFalse(ips.MoveNext());
        ips.Reset();
        Assert.IsTrue(ips.MoveNext());
        Assert.IsTrue(ips.MoveNext());
        Assert.IsFalse(ips.MoveNext());
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_ipv6_EnumeratorCurrent()
    {
        var ipn = IPNetwork2.Parse("::/127");
        IEnumerator ips = ipn.ListIPAddress();
        Assert.IsNotNull(ips.Current);
        Assert.IsTrue(ips.MoveNext());
        Assert.IsNotNull(ips.Current);
        Assert.IsTrue(ips.MoveNext());
        Assert.IsFalse(ips.MoveNext());
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_ipv6_EnumeratorCurrentOor()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
        {
            var ipn = IPNetwork2.Parse("::/127");
            IEnumerator ips = ipn.ListIPAddress();
            Assert.IsNotNull(ips.Current);
            Assert.IsTrue(ips.MoveNext());
            Assert.IsNotNull(ips.Current);
            Assert.IsTrue(ips.MoveNext());
            Assert.IsFalse(ips.MoveNext());
            Console.WriteLine("This is out of range : {0} ", ips.Current);
        });
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_ipv6_EnumeratorIterate()
    {
        var ipn = IPNetwork2.Parse("::/127");
        IEnumerator ips = ipn.ListIPAddress();
        while (ips.MoveNext())
        {
            Assert.IsNotNull(ips.Current);
        }
    }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_ipv6_DefaultNetmask()
    {
        var ipnetwork = IPNetwork2.Parse("::1");
        Assert.AreEqual(64, ipnetwork.Cidr, "Cidr");
    }
    
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    [Obsolete("Obsolete")]
    public void TestListIPAddressFilterEnumAll()
    {
        var ipn = IPNetwork2.Parse("192.168.1.0/29");
        using IPAddressCollection ips = ipn.ListIPAddress(FilterEnum.All);
        Assert.HasCount(8, ips, "Count");
    }
    
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    [Obsolete("Obsolete")]
    public void TestListIPAddressFilterEnumUsable()
    {
        var ipn = IPNetwork2.Parse("192.168.1.0/29");
        using IPAddressCollection ips = ipn.ListIPAddress(FilterEnum.Usable);
        Assert.HasCount(6, ips, "Count");
    }
}