using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml.Serialization;

namespace System.Net.TestSerialization.NetFramework
{
    /// <summary>
    /// Summary description for SerializeXmlTest
    /// </summary>
    [TestClass]
    public class SerializeXmlTest
    {

        [TestMethod]
        public void Test_Serialize_Xml()
        {
            var ipnetwork = IPNetwork.Parse("10.0.0.1/8");

            var mem = new MemoryStream();

            var serializer = new XmlSerializer(typeof(IPNetwork));
            serializer.Serialize(mem, ipnetwork);

            var result = Encoding.UTF8.GetString(mem.ToArray());

            string expected = @"<?xml version=""1.0""?>
<IPNetwork xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Value>10.0.0.0/8</Value>
</IPNetwork>";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Deserialize_Xml()
        {
            string xml = @"<?xml version=""1.0""?>
<IPNetwork xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Value>10.0.0.0/8</Value>
</IPNetwork>";
            var bytes = Encoding.UTF8.GetBytes(xml);
            var mem = new MemoryStream(bytes);

            var serializer = new XmlSerializer(typeof(IPNetwork));
            var result = serializer.Deserialize(mem);

            IPNetwork expected = IPNetwork.Parse("10.0.0.1/8");
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void Test_Serialize_Deserialize_Xml()
        {
            var ipnetwork = IPNetwork.Parse("10.0.0.1/8");

            var mem = new MemoryStream();

            var serializer = new XmlSerializer(typeof(IPNetwork));
            serializer.Serialize(mem, ipnetwork);

            string result = Encoding.UTF8.GetString(mem.ToArray());
            Console.WriteLine(result);

            mem.Position = 0;
            var ipnetwork2 = serializer.Deserialize(mem);

            Assert.AreEqual(ipnetwork, ipnetwork2);
        }



        [TestMethod]
        [TestCategory("LongRunning")]
        public void Test_1_000_000_Serialize_Xml()
        {
            var ipnetwork = IPNetwork.Parse("10.0.0.1/8");

            var serializer = new XmlSerializer(typeof(IPNetwork));
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
        public void Test_1_000_000_Deserialize_Xml()
        {
            string xml = @"<?xml version=""1.0""?>
<IPNetwork xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Value>10.0.0.0/8</Value>
</IPNetwork>";
            var bytes = Encoding.UTF8.GetBytes(xml);
            var mem = new MemoryStream(bytes);

            var serializer = new XmlSerializer(typeof(IPNetwork));

            for (int i = 0; i < 1000000; i++)
            {
                var result = serializer.Deserialize(mem);
                mem.Position = 0;
            }

            //  17.98 seconds(Ad hoc).
        }

        [TestMethod]
        [TestCategory("LongRunning")]
        public void Test_1_000_000_Serialize_Deserialize_Xml()
        {
            var ipnetwork = IPNetwork.Parse("10.0.0.1/8");

            var serializer = new XmlSerializer(typeof(IPNetwork));
            var mem = new MemoryStream();

            for (int i = 0; i < 1000000; i++)
            {
                serializer.Serialize(mem, ipnetwork);

                mem.Position = 0;
                var ipnetwork2 = serializer.Deserialize(mem);

                mem.SetLength(0);

            }

            // 17.48 seconds(Ad hoc).

        }

    }
}
