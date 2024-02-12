using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BaGetter.Protocol.Models;

/// <summary>
/// A catalog leaf. Represents a single package event.<br/>
/// Leafs can be discovered from a <see cref="CatalogPage"/>.
/// </summary>
/// <remarks>
/// See: <see href="https://docs.microsoft.com/en-us/nuget/api/catalog-resource#catalog-leaf"/><br/>
/// Based off: <see href="https://github.com/NuGet/NuGet.Services.Metadata/blob/64af0b59c5a79e0143f0808b39946df9f16cb2e7/src/NuGet.Protocol.Catalog/Models/CatalogLeaf.cs"/>
/// </remarks>
public class CatalogLeaf : ICatalogLeafItem
{
    /// <summary>
    /// The URL to the current catalog leaf.
    /// </summary>
    [JsonPropertyName("@id")]
    public string CatalogLeafUrl { get; set; }

    /// <summary>
    /// The type of the current catalog leaf.
    /// </summary>
    [JsonPropertyName("@type")]
    public IReadOnlyList<string> Type { get; set; }

    /// <summary>
    /// The catalog commit ID associated with this catalog item.
    /// </summary>
    [JsonPropertyName("catalog:commitId")]
    public string CommitId { get; set; }

    /// <summary>
    /// The commit timestamp of this catalog item.
    /// </summary>
    [JsonPropertyName("catalog:commitTimeStamp")]
    public DateTimeOffset CommitTimestamp { get; set; }

    /// <summary>
    /// The package ID of the catalog item.
    /// </summary>
    [JsonPropertyName("id")]
    public string PackageId { get; set; }

    /// <summary>
    /// The published date of the package catalog item.
    /// </summary>
    [JsonPropertyName("published")]
    public DateTimeOffset Published { get; set; }

    /// <summary>
    /// The package version of the catalog item.
    /// </summary>
    [JsonPropertyName("version")]
    public string PackageVersion { get; set; }
}
