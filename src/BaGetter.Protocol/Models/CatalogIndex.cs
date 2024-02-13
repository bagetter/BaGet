using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BaGetter.Protocol.Models;

/// <summary>
/// The catalog index is the entry point for the catalog resource.<br/>
/// Use this to discover catalog pages, which in turn can be used to discover catalog leafs.
/// </summary>
/// <remarks>
/// See: <see href="https://docs.microsoft.com/en-us/nuget/api/catalog-resource#catalog-index"/><br/>
/// Based off: <see href="https://github.com/NuGet/NuGet.Services.Metadata/blob/64af0b59c5a79e0143f0808b39946df9f16cb2e7/src/NuGet.Protocol.Catalog/Models/CatalogIndex.cs"/>
/// </remarks>
public class CatalogIndex
{
    /// <summary>
    /// A timestamp of the most recent commit.
    /// </summary>
    [JsonPropertyName("commitTimeStamp")]
    public DateTimeOffset CommitTimestamp { get; set; }

    /// <summary>
    /// The number of catalog pages in the catalog index.
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; set; }

    /// <summary>
    /// The items used to discover <see cref="CatalogPage"/>s.
    /// </summary>
    [JsonPropertyName("items")]
    public List<CatalogPageItem> Items { get; set; }
}
