
// <copyright file="SupernetUnitTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Net.TestProject
{
    /// <summary>
    /// SupernetUnitTest test every single method
    /// </summary>
    [TestClass]
    public class SupernetUnitTest
    {

        [TestMethod]
        public void TestSupernetInternal1()
        {
            IPNetwork result;
            IPNetwork.InternalSupernet(true, null, null, out result);

            Assert.AreEqual(null, result, "supernet");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSupernetInternal2()
        {
            IPNetwork result;
            IPNetwork.InternalSupernet(false, null, null, out result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]

        public void Issue33__TestSupernet__Bug_or_default_behavior()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.0.0/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.2.0/24");
            IPNetwork expected = IPNetwork.Parse("192.168.0.0/23");
            IPNetwork supernet = network1.Supernet(network2);

            Assert.AreEqual(expected, supernet, "supernet");
        }

        [TestMethod]
        public void Issue33__TestWideSubnet__Bug_or_default_behavior()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.0.0/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.2.0/24");
            IPNetwork expected = IPNetwork.Parse("192.168.0.0/22");
            IPNetwork widenetwork = IPNetwork.WideSubnet(new[] { network1, network2 });

            Assert.AreEqual(expected, widenetwork, "widesubnet");
        }

        [TestMethod]
        public void Issue162__Test_IPrangeToCIDRnotation()
        {
            var network1 = "172.64.0.0";
            var network2 = "172.71.255.255";

            var final = IPNetwork.WideSubnet(network1, network2);
            var result = final.ToString();

            string expected = "172.64.0.0/13";
            Assert.AreEqual(expected, result, "Supernet");
        }

        [TestMethod]
        public void TestSupernet1()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.1.1/24");
            IPNetwork expected = IPNetwork.Parse("192.168.0.0/23");
            IPNetwork supernet = network1.Supernet(network2);

            Assert.AreEqual(expected, supernet, "supernet");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestSupernet2()
        {
            IPNetwork network1 = null;
            IPNetwork network2 = IPNetwork.Parse("192.168.1.1/24");
            IPNetwork supernet = network1.Supernet(network2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSupernet3()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.1.1/24");
            IPNetwork network2 = null;
            IPNetwork supernet = network1.Supernet(network2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSupernet4()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.1.1/25");
            IPNetwork supernet = network1.Supernet(network2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestSupernet5()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.5.1/24");
            IPNetwork supernet = network1.Supernet(network2);
        }

        [TestMethod]
        public void TestSupernet6()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.0.1/25");
            IPNetwork expected = IPNetwork.Parse("192.168.0.0/24");
            IPNetwork supernet = network1.Supernet(network2);

            Assert.AreEqual(expected, supernet, "supernet");
        }

        [TestMethod]
        public void TestSupernet7()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/25");
            IPNetwork network2 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork expected = IPNetwork.Parse("192.168.0.0/24");
            IPNetwork supernet = network1.Supernet(network2);

            Assert.AreEqual(expected, supernet, "supernet");
        }

        [TestMethod]
        public void TestSupernetStatic1()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.0.1/25");
            IPNetwork network2 = IPNetwork.Parse("192.168.0.1/24");
            IPNetwork expected = IPNetwork.Parse("192.168.0.0/24");
#pragma warning disable CS0618 // Type or member is obsolete
            IPNetwork supernet = IPNetwork.Supernet(network1, network2);
#pragma warning restore CS0618 // Type or member is obsolete

            Assert.AreEqual(expected, supernet, "supernet");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSupernet8()
        {
            IPNetwork network1 = IPNetwork.Parse("192.168.1.1/24");
            IPNetwork network2 = IPNetwork.Parse("192.168.2.1/24");
            IPNetwork supernet = network1.Supernet(network2);
        }

        [TestMethod]
        public void TestSupernet9()
        {
            IPNetwork ipnetwork1 = IPNetwork.Parse("200.16.0.0/24");
            IPNetwork ipnetwork2 = IPNetwork.Parse("200.16.1.0/24");
            IPNetwork ipnetwork3 = IPNetwork.Parse("200.16.2.0/24");
            IPNetwork ipnetwork4 = IPNetwork.Parse("200.16.3.0/24");

            IPNetwork result = IPNetwork.Supernet(new[] { ipnetwork1, ipnetwork2, ipnetwork3, ipnetwork4 })[0];
            IPNetwork expected = IPNetwork.Parse("200.16.0.0/22");

            Assert.AreEqual(expected, result, "supernet");
        }

    }
}
