using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Net.TestSerialization.NetFramework
{
    public static class DataContractSerializeHelper
    {
        public static string Serialize<T>(T obj, bool formatting = true) where T : new()
        {
            if (obj == null)
                return string.Empty;

            var serializer = new DataContractSerializer(typeof(T));
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = formatting
            };
            using (var textWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serializer.WriteObject(xmlWriter, obj);
                }

                var result = textWriter.ToString();
                return result;
            }
        }

        public static T Deserialize<T>(string xml) where T : new()
        {
            if (string.IsNullOrWhiteSpace(xml))
                return new T();

            using (var textReader = new StringReader(xml))
            using (var xmlReader = XmlReader.Create(textReader))
            {
                var serializer = new DataContractSerializer(typeof(T));
                var result = (T) serializer.ReadObject(xmlReader);
                return result;
            }
        }
    }

    [TestClass]
    public class SerializeDataContractTest
    {
        [TestMethod]
        public void Test_Serialize_DataContract()
        {
            var ipnetwork = IPNetwork.Parse("10.0.0.1/8");

            string result = DataContractSerializeHelper.Serialize(ipnetwork);
        
            string expected = $"<IPNetwork xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:x=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Net\">{Environment.NewLine}  <IPNetwork i:type=\"x:string\" xmlns=\"\">10.0.0.0/8</IPNetwork>{Environment.NewLine}</IPNetwork>";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Deserialize_DataContract()
        {
            var ipnetwork = IPNetwork.Parse("10.0.0.1/8");
            string serialized = DataContractSerializeHelper.Serialize(ipnetwork);
            
            var result = DataContractSerializeHelper.Deserialize<IPNetwork>(serialized);
            
            Assert.AreEqual(ipnetwork, result);
        }
    }
}