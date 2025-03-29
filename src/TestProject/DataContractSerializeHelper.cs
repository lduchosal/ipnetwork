// <copyright file="DataContractSerializeHelper.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

/// <summary>
/// Test serialization.
/// </summary>
public static class DataContractSerializeHelper
{
    /// <summary>
    /// Serialize.
    /// </summary>
    /// <param name="obj">The object to serialize.</param>
    /// <param name="formatting">Should it be formtted.</param>
    /// <typeparam name="T">Type of object.</typeparam>
    /// <returns>A string representing the object.</returns>
    public static string Serialize<T>(T obj, bool formatting = true)
        where T : new()
    {
            if (obj == null)
            {
                return string.Empty;
            }

            var serializer = new DataContractSerializer(typeof(T));
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = formatting,
            };
            using (var textWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serializer.WriteObject(xmlWriter, obj);
                }

                string result = textWriter.ToString();
                return result;
            }
        }

    /// <summary>
    /// Deserialize.
    /// </summary>
    /// <param name="xml">The xml string representing the object.</param>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <returns>The object.</returns>
    public static T Deserialize<T>(string xml)
        where T : new()
    {
            if (string.IsNullOrWhiteSpace(xml))
            {
                return new T();
            }

            using (var textReader = new StringReader(xml))
            using (var xmlReader = XmlReader.Create(textReader))
            {
                var serializer = new DataContractSerializer(typeof(T));
                var result = (T)serializer.ReadObject(xmlReader);
                return result;
            }
        }
}