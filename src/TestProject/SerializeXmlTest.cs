// <copyright file="SerializeXmlTest.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

/// <summary>
/// Test.
/// </summary>
[TestClass]
public class SerializeXmlTest
{
    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_Serialize_Xml()
    {
            var ipnetwork = IPNetwork2.Parse("10.0.0.1/8");

            var mem = new MemoryStream();

            var serializer = new XmlSerializer(typeof(IPNetwork2));
            serializer.Serialize(mem, ipnetwork);

            string result = Encoding.UTF8.GetString(mem.ToArray());

            // string expected = $@"﻿<?xml version=""1.0"" encoding=""utf-8""?>{Environment.NewLine}<IPNetwork xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">{Environment.NewLine}  <Value>10.0.0.0/8</Value>{Environment.NewLine}</IPNetwork>";
            bool ok = result.Contains("<Value>10.0.0.0/8</Value>");

            Assert.IsTrue(ok, result);
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_Deserialize_Xml()
    {
            string xml = @"<?xml version=""1.0""?>
<IPNetwork2 xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Value>10.0.0.0/8</Value>
</IPNetwork2>";
            byte[] bytes = Encoding.UTF8.GetBytes(xml);
            var mem = new MemoryStream(bytes);

            var serializer = new XmlSerializer(typeof(IPNetwork2));
            object result = serializer.Deserialize(mem);

            var expected = IPNetwork2.Parse("10.0.0.1/8");
            Assert.AreEqual(expected, result);
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    public void Test_Serialize_Deserialize_Xml()
    {
            var ipnetwork = IPNetwork2.Parse("10.0.0.1/8");

            var mem = new MemoryStream();

            var serializer = new XmlSerializer(typeof(IPNetwork2));
            serializer.Serialize(mem, ipnetwork);

            string result = Encoding.UTF8.GetString(mem.ToArray());
            Console.WriteLine(result);

            mem.Position = 0;
            object ipnetwork2 = serializer.Deserialize(mem);

            Assert.AreEqual(ipnetwork, ipnetwork2);
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    [TestCategory("LongRunning")]
    public void Test_1_000_000_Serialize_Xml()
    {
            var ipnetwork = IPNetwork2.Parse("10.0.0.1/8");

            var serializer = new XmlSerializer(typeof(IPNetwork2));
            var mem = new MemoryStream();

            for (int i = 0; i < 1000000; i++)
            {
                serializer.Serialize(mem, ipnetwork);
                mem.SetLength(0);
            }

            // 5.13 seconds(Ad hoc).
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    [TestCategory("LongRunning")]
    public void Test_1_000_000_Deserialize_Xml()
    {
            string xml = @"<?xml version=""1.0""?>
<IPNetwork2 xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Value>10.0.0.0/8</Value>
</IPNetwork2>";
            byte[] bytes = Encoding.UTF8.GetBytes(xml);
            var mem = new MemoryStream(bytes);

            var serializer = new XmlSerializer(typeof(IPNetwork2));

            for (int i = 0; i < 1000000; i++)
            {
                object result = serializer.Deserialize(mem);
                mem.Position = 0;
            }

            // 17.98 seconds(Ad hoc).
        }

    /// <summary>
    /// Test.
    /// </summary>
    [TestMethod]
    [TestCategory("LongRunning")]
    public void Test_1_000_000_Serialize_Deserialize_Xml()
    {
            var ipnetwork = IPNetwork2.Parse("10.0.0.1/8");

            var serializer = new XmlSerializer(typeof(IPNetwork2));
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