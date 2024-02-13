using System.Threading;
using System.Threading.Tasks;
using BaGetter.Protocol.Models;

namespace BaGetter.Core;

/// <summary>
/// The service used to search for packages.
/// </summary>
/// <remarks>See: <see href="https://docs.microsoft.com/en-us/nuget/api/search-query-service-resource"/></remarks>
public interface ISearchService
{
    /// <summary>
    /// Perform a search query.
    /// </summary>
    /// <remarks>See: <see href="https://docs.microsoft.com/en-us/nuget/api/search-query-service-resource#search-for-packages"/></remarks>
    /// <param name="request">The search request.</param>
    /// <param name="cancellationToken">A token to cancel the task.</param>
    /// <returns>The search response.</returns>
    Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Perform an autocomplete query.
    /// </summary>
    /// <remarks>See: <see href="https://docs.microsoft.com/en-us/nuget/api/search-autocomplete-service-resource#search-for-package-ids"/></remarks>
    /// <param name="request">The autocomplete request.</param>
    /// <param name="cancellationToken">A token to cancel the task.</param>
    /// <returns>The autocomplete response.</returns>
    Task<AutocompleteResponse> AutocompleteAsync(AutocompleteRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Enumerate listed package versions.
    /// </summary>
    /// <remarks>See: <see href="https://docs.microsoft.com/en-us/nuget/api/search-autocomplete-service-resource#enumerate-package-versions"/></remarks>
    /// <param name="request">The autocomplete request.</param>
    /// <param name="cancellationToken">A token to cancel the task.</param>
    /// <returns>The package versions that matched the request.</returns>
    Task<AutocompleteResponse> ListPackageVersionsAsync(VersionsRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Find the packages that depend on a given package.
    /// </summary>
    /// <param name="packageId">The package whose dependents should be found.</param>
    /// <param name="cancellationToken">A token to cancel the task.</param>
    /// <returns>The dependents response.</returns>
    Task<DependentsResponse> FindDependentsAsync(string packageId, CancellationToken cancellationToken);
}
