using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BaGetter.Protocol.Models;

/// <summary>
/// The full list of versions for a single package.
/// </summary>
/// <remarks>See: <see href="https://docs.microsoft.com/en-us/nuget/api/package-base-address-resource#enumerate-package-versions"/></remarks>
public class PackageVersionsResponse
{
    /// <summary>
    /// The versions, lowercased and normalized.
    /// </summary>
    [JsonPropertyName("versions")]
    public IReadOnlyList<string> Versions { get; set; }
}
