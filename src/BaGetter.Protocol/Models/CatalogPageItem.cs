using System;
using System.Text.Json.Serialization;

namespace BaGetter.Protocol.Models;

/// <summary>
/// An item in the <see cref="CatalogIndex"/> that references a <see cref="CatalogPage"/>.
/// </summary>
/// <remarks>
/// See: <see href="https://docs.microsoft.com/en-us/nuget/api/catalog-resource#catalog-page-object-in-the-index"/><br/>
/// Based off: <see href="https://github.com/NuGet/NuGet.Services.Metadata/blob/64af0b59c5a79e0143f0808b39946df9f16cb2e7/src/NuGet.Protocol.Catalog/Models/CatalogPageItem.cs"/>
/// </remarks>
public class CatalogPageItem
{
    /// <summary>
    /// The URL to this item's corresponding <see cref="CatalogPage"/>.
    /// </summary>
    [JsonPropertyName("@id")]
    public string CatalogPageUrl { get; set; }

    /// <summary>
    /// A timestamp of the most recent commit in this page.
    /// </summary>
    [JsonPropertyName("commitTimeStamp")]
    public DateTimeOffset CommitTimestamp { get; set; }

    /// <summary>
    /// The number of items in the page.
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; set; }
}
