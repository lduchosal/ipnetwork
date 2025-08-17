namespace System.Net;

/// <summary>
/// Represents different filters for a collection of items.
/// </summary>
public enum Filter
{
    /// <summary>
    /// Every IPAdresses are returned
    /// </summary>
    All,

    /// <summary>
    /// Returns only usable IPAdresses
    /// </summary>
    Usable,
}

/// <summary>
/// Represents different filters for a collection of items.
/// </summary>
[Obsolete("Use Filter instead")]
public enum FilterEnum
{
    /// <summary>
    /// Every IPAdresses are returned
    /// </summary>
    All,

    /// <summary>
    /// Returns only usable IPAdresses
    /// </summary>
    Usable,
}