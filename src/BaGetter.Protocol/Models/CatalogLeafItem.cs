using System;
using System.Text.Json.Serialization;

namespace BaGetter.Protocol.Models;

/// <summary>
/// An item in a <see cref="CatalogPage"/> that references a <see cref="CatalogLeaf"/>.
/// </summary>
/// <remarks>
/// See: <see href="https://docs.microsoft.com/en-us/nuget/api/catalog-resource#catalog-item-object-in-a-page"/><br/>
/// Based off: <see href="https://github.com/NuGet/NuGet.Services.Metadata/blob/64af0b59c5a79e0143f0808b39946df9f16cb2e7/src/NuGet.Protocol.Catalog/Models/CatalogLeafItem.cs"/>
/// </remarks>
public class CatalogLeafItem : ICatalogLeafItem
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
    public string Type { get; set; }

    /// <summary>
    /// The commit timestamp of this catalog item.
    /// </summary>
    [JsonPropertyName("commitTimeStamp")]
    public DateTimeOffset CommitTimestamp { get; set; }

    /// <summary>
    /// The package ID of the catalog item.
    /// </summary>
    [JsonPropertyName("nuget:id")]
    public string PackageId { get; set; }

    /// <summary>
    /// The package version of the catalog item.
    /// </summary>
    [JsonPropertyName("nuget:version")]
    public string PackageVersion { get; set; }
}
