// <copyright file="SerializeBinaryFormatterTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>


#pragma warning disable SYSLIB0011 // Type or member is obsolete

namespace TestProject
{
    using System.Runtime.Serialization.Formatters.Binary;

    [TestClass]
    public class SerializeBinaryFormatterTest
    {
        [TestMethod]
        [Ignore] // ("Assembly version is writen in binary serilaization.")
        public void Test_Serialize_BinaryFormatter()
        {
            var ipnetwork = IPNetwork2.Parse("10.0.0.1/8");

            var serializer = new BinaryFormatter();
            var mem = new MemoryStream();

            serializer.Serialize(mem, ipnetwork);
            string result = Convert.ToBase64String(mem.ToArray());

            string expected =
                "AAEAAAD/////AQAAAAAAAAAMAgAAAFdTeXN0ZW0uTmV0LklQTmV0d29yaywgVmVyc2lvbj0yLjUuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPTcxNzM0M2NjMmMyNWVkY2YFAQAAABVTeXN0ZW0uTmV0LklQTmV0d29yazIBAAAACUlQTmV0d29yawECAAAABgMAAAAKMTAuMC4wLjAvOAs=";

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Deserialize_BinaryFormatter()
        {
            string base64 =
                "AAEAAAD/////AQAAAAAAAAAMAgAAAFdTeXN0ZW0uTmV0LklQTmV0d29yaywgVmVyc2lvbj0yLjUuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPTcxNzM0M2NjMmMyNWVkY2YFAQAAABVTeXN0ZW0uTmV0LklQTmV0d29yazIBAAAACUlQTmV0d29yawECAAAABgMAAAAKMTAuMC4wLjAvOAs=";
            byte[] bytes = Convert.FromBase64String(base64);
            var mem = new MemoryStream(bytes);

            var serializer = new BinaryFormatter();
            object result = serializer.Deserialize(mem);

            var expected = IPNetwork2.Parse("10.0.0.1/8");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Serialize_Deserialize_BinaryFormatter()
        {
            var ipnetwork = IPNetwork2.Parse("10.0.0.1/8");

            var serializer = new BinaryFormatter();
            var mem = new MemoryStream();

            serializer.Serialize(mem, ipnetwork);

            mem.Position = 0;
            object ipnetwork2 = serializer.Deserialize(mem);

            Assert.AreEqual(ipnetwork, ipnetwork2);
        }

        [TestMethod]
        [TestCategory("LongRunning")]
        public void Test_1_000_000_Serialize_BinaryFormatter()
        {
            var ipnetwork = IPNetwork2.Parse("10.0.0.1/8");

            var serializer = new BinaryFormatter();
            var mem = new MemoryStream();

            for (int i = 0; i < 1000000; i++)
            {
                serializer.Serialize(mem, ipnetwork);
                mem.SetLength(0);
            }

            // 5.13 seconds(Ad hoc).
        }

        [TestMethod]
        [TestCategory("LongRunning")]
        public void Test_1_000_000_Deserialize_BinaryFormatter()
        {
            string base64 =
                "AAEAAAD/////AQAAAAAAAAAMAgAAAFdTeXN0ZW0uTmV0LklQTmV0d29yaywgVmVyc2lvbj0yLjUuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPTcxNzM0M2NjMmMyNWVkY2YFAQAAABVTeXN0ZW0uTmV0LklQTmV0d29yazIBAAAACUlQTmV0d29yawECAAAABgMAAAAKMTAuMC4wLjAvOAs=";
            byte[] bytes = Convert.FromBase64String(base64);
            var mem = new MemoryStream(bytes);

            var serializer = new BinaryFormatter();

            for (int i = 0; i < 1000000; i++)
            {
                object result = serializer.Deserialize(mem);
                mem.Position = 0;
            }

            // 11.949 seconds(Ad hoc).
        }

        [TestMethod]
        [TestCategory("LongRunning")]
        public void Test_1_000_000_Serialize_Deserialize_BinaryFormatter()
        {
            var ipnetwork = IPNetwork2.Parse("10.0.0.1/8");

            var serializer = new BinaryFormatter();
            var mem = new MemoryStream();

            for (int i = 0; i < 1000000; i++)
            {
                serializer.Serialize(mem, ipnetwork);

                mem.Position = 0;
                object ipnetwork2 = serializer.Deserialize(mem);

                mem.SetLength(0);
            }

            // 17.48 seconds(Ad hoc).
        }
    }
#pragma warning restore SYSLIB0011 // Type or member is obsolete
}