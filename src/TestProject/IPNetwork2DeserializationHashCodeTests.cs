// <copyright file="IPNetwork2DeserializationHashCodeTests.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace TestProject;

/// <summary>
/// Tests for issue #372: HashCode not computed correctly after deserialization.
/// Deserialized IPNetwork2 objects must have the same GetHashCode() as
/// equivalent objects created via Parse().
/// </summary>
[TestClass]
public class IPNetwork2DeserializationHashCodeTests
{
    /// <summary>
    /// DataContract deserialized object must have same hashCode as parsed object.
    /// </summary>
    [TestMethod]
    public void TestHashCode_DataContractDeserialization_MatchesParsed()
    {
        var original = IPNetwork2.Parse("10.0.0.0/8");
        string serialized = DataContractSerializeHelper.Serialize(original);
        var deserialized = DataContractSerializeHelper.Deserialize<IPNetwork2>(serialized);

        Assert.AreEqual(original.GetHashCode(), deserialized.GetHashCode());
    }

    /// <summary>
    /// DataContract deserialized object must work correctly as Dictionary key.
    /// </summary>
    [TestMethod]
    public void TestHashCode_DataContractDeserialization_WorksAsDictionaryKey()
    {
        var original = IPNetwork2.Parse("192.168.1.0/24");
        string serialized = DataContractSerializeHelper.Serialize(original);
        var deserialized = DataContractSerializeHelper.Deserialize<IPNetwork2>(serialized);

        var dict = new Dictionary<IPNetwork2, string>
        {
            { original, "found" },
        };

        Assert.IsTrue(dict.ContainsKey(deserialized));
    }

    /// <summary>
    /// DataContract deserialized object must work correctly in HashSet.
    /// </summary>
    [TestMethod]
    public void TestHashCode_DataContractDeserialization_WorksInHashSet()
    {
        var original = IPNetwork2.Parse("172.16.0.0/12");
        string serialized = DataContractSerializeHelper.Serialize(original);
        var deserialized = DataContractSerializeHelper.Deserialize<IPNetwork2>(serialized);

        var set = new HashSet<IPNetwork2> { original };

        Assert.Contains(deserialized, set);
    }

    /// <summary>
    /// JSON deserialized object must have same hashCode as parsed object.
    /// </summary>
    [TestMethod]
    public void TestHashCode_JsonDeserialization_MatchesParsed()
    {
        var original = IPNetwork2.Parse("10.0.0.0/8");
        string serialized = JsonConvert.SerializeObject(original);
        var deserialized = JsonConvert.DeserializeObject<IPNetwork2>(serialized);

        Assert.AreEqual(original.GetHashCode(), deserialized!.GetHashCode());
    }

    /// <summary>
    /// JSON deserialized object must work correctly as Dictionary key.
    /// </summary>
    [TestMethod]
    public void TestHashCode_JsonDeserialization_WorksAsDictionaryKey()
    {
        var original = IPNetwork2.Parse("192.168.1.0/24");
        string serialized = JsonConvert.SerializeObject(original);
        var deserialized = JsonConvert.DeserializeObject<IPNetwork2>(serialized);

        var dict = new Dictionary<IPNetwork2, string>
        {
            { original, "found" },
        };

        Assert.IsTrue(dict.ContainsKey(deserialized!));
    }

    /// <summary>
    /// Multiple different networks deserialized must not all share the same hashCode.
    /// </summary>
    [TestMethod]
    public void TestHashCode_DataContractDeserialization_DifferentNetworks_DifferentHashCodes()
    {
        string[] networks = new[]
        {
            "10.0.0.0/8",
            "192.168.1.0/24",
            "172.16.0.0/12",
        };

        var hashCodes = new HashSet<int>();
        foreach (string net in networks)
        {
            var original = IPNetwork2.Parse(net);
            string serialized = DataContractSerializeHelper.Serialize(original);
            var deserialized = DataContractSerializeHelper.Deserialize<IPNetwork2>(serialized);
            hashCodes.Add(deserialized.GetHashCode());
        }

        Assert.HasCount(3, hashCodes);
    }
}
