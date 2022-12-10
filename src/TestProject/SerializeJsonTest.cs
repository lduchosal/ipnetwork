﻿// <copyright file="SerializeJsonTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net.TestSerialization.NetFramework
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;

    [TestClass]
    public class SerializeJsonTest
    {
        [TestMethod]
        public void Test_Serialize_Json()
        {
            var ipnetwork = IPNetwork.Parse("10.0.0.1/8");

            string result = JsonConvert.SerializeObject(ipnetwork);

            string expected = "{\"IPNetwork\":\"10.0.0.0/8\"}";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Deserialize_Json()
        {
            string json = "{\"IPNetwork\":\"10.0.0.0/8\"}";

            IPNetwork result = JsonConvert.DeserializeObject<IPNetwork>(json);

            var expected = IPNetwork.Parse("10.0.0.1/8");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Serialize_Deserialize_Json()
        {
            var ipnetwork = IPNetwork.Parse("10.0.0.1/8");

            string json = JsonConvert.SerializeObject(ipnetwork);
            IPNetwork result = JsonConvert.DeserializeObject<IPNetwork>(json);

            Assert.AreEqual(ipnetwork, result);
        }

        [TestMethod]
        [TestCategory("LongRunning")]
        public void Test_1_000_000_Serialize_Json()
        {
            var ipnetwork = IPNetwork.Parse("10.0.0.1/8");

            for (int i = 0; i < 1000000; i++)
            {
                string json = JsonConvert.SerializeObject(ipnetwork);
            }

            // 3.06 seconds(Ad hoc).
        }

        [TestMethod]
        [TestCategory("LongRunning")]
        public void Test_1_000_000_Deserialize_Json()
        {
            string json = "{\"IPNetwork\":\"10.0.0.0/8\"}";

            for (int i = 0; i < 1000000; i++)
            {
                IPNetwork result = JsonConvert.DeserializeObject<IPNetwork>(json);
            }

            // 10.20 seconds(Ad hoc).
        }

        [TestMethod]
        [TestCategory("LongRunning")]
        public void Test_1_000_000_Serialize_Deserialize_Json()
        {
            var ipnetwork = IPNetwork.Parse("10.0.0.1/8");

            for (int i = 0; i < 1000000; i++)
            {
                string json = JsonConvert.SerializeObject(ipnetwork);
                IPNetwork result = JsonConvert.DeserializeObject<IPNetwork>(json);
            }

            // 13.49 seconds(Ad hoc).
        }
    }
}
