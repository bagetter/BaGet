using System.Text.Json.Serialization;
using BaGetter.Protocol.Internal;

namespace BaGetter.Protocol.Models;

/// <summary>
/// Represents a package dependency.
/// </summary>
/// <remarks>See: <see href="https://docs.microsoft.com/en-us/nuget/api/registration-base-url-resource#package-dependency"/></remarks>
public class DependencyItem
{
    /// <summary>
    /// The ID of the package dependency.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    /// The allowed version range of the dependency.
    /// </summary>
    [JsonPropertyName("range")]
    [JsonConverter(typeof(PackageDependencyRangeJsonConverter))]
    public string Range { get; set; }
}
