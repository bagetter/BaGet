using System.Threading.Tasks;
using BaGetter.Protocol.Internal;
using Xunit;

namespace BaGetter.Protocol.Tests;

public class RawPackageMetadataClientTests : IClassFixture<ProtocolFixture>
{
    private readonly RawPackageMetadataClient _target;

    public RawPackageMetadataClientTests(ProtocolFixture fixture)
    {
        _target = fixture.MetadataClient;
    }

    [Fact]
    public async Task GetRegistrationIndexInlinedItems()
    {
        var result = await _target.GetRegistrationIndexOrNullAsync("Test.Package");

        Assert.NotNull(result);
        Assert.Equal(2, result.Pages.Count);

        Assert.True(result.Pages[0].Count == 1);
        Assert.True(result.Pages[0].ItemsOrNull.Count == 1);
        Assert.Equal("1.0.0", result.Pages[0].Lower);
        Assert.Equal("1.0.0", result.Pages[0].Upper);
        Assert.StartsWith(TestData.RegistrationIndexInlinedItemsUrl, result.Pages[0].RegistrationPageUrl);

        Assert.True(result.Pages[1].Count == 2);
        Assert.True(result.Pages[1].ItemsOrNull.Count == 2);
        Assert.Equal("2.0.0", result.Pages[1].Lower);
        Assert.Equal("3.0.0", result.Pages[1].Upper);
        Assert.StartsWith(TestData.RegistrationIndexInlinedItemsUrl, result.Pages[1].RegistrationPageUrl);
    }

    [Fact]
    public async Task GetRegistrationIndexLikeGithubPackages()
    {
        var result = await _target.GetRegistrationIndexOrNullAsync("my.github.pkg");

        Assert.NotNull(result);
        Assert.Equal(1, result.Count);
        Assert.Collection(result.Pages, page =>
        {
            Assert.Equal(2, page.Count);
            Assert.Collection(page.ItemsOrNull,
                pkg1 =>
                {
                    Assert.Equal("2.1.6", pkg1.PackageMetadata.Version);
                    Assert.Collection(pkg1.PackageMetadata.Tags,
                        tag1 => Assert.Equal("json bson serializer", tag1)
                    );
                },
                pkg2 =>
                {
                    Assert.Equal("2.1.5", pkg2.PackageMetadata.Version);
                    Assert.Collection(pkg2.PackageMetadata.Tags,
                        tag1 => Assert.Equal("", tag1)
                    );
                }
            );
        });
    }

    [Fact]
    public async Task GetRegistrationIndexPagedItems()
    {
        var result = await _target.GetRegistrationIndexOrNullAsync("Paged.Package");

        Assert.NotNull(result);
        Assert.Equal(2, result.Pages.Count);

        Assert.True(result.Pages[0].Count == 1);
        Assert.Null(result.Pages[0].ItemsOrNull);
        Assert.Equal("1.0.0", result.Pages[0].Lower);
        Assert.Equal("1.0.0", result.Pages[0].Upper);

        Assert.True(result.Pages[1].Count == 2);
        Assert.Null(result.Pages[1].ItemsOrNull);
        Assert.Equal("2.0.0", result.Pages[1].Lower);
        Assert.Equal("3.0.0", result.Pages[1].Upper);
    }

    [Fact]
    public async Task GetRegistrationPage()
    {
        var result = await _target.GetRegistrationPageAsync(TestData.RegistrationPageUrl);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.NotNull(result.ItemsOrNull);
        Assert.Equal(2, result.ItemsOrNull.Count);
        Assert.Equal("2.0.0", result.Lower);
        Assert.Equal("3.0.0", result.Upper);

        var firstMetadata = result.ItemsOrNull[0].PackageMetadata;

        Assert.Equal("Paged.Package", firstMetadata.PackageId);
        Assert.Equal("2.0.0+build", firstMetadata.Version);
    }

    [Fact]
    public async Task GetRegistrationPageDeprecated()
    {
        // TODO
        await Task.Yield();
    }

    [Fact]
    public async Task GetsRegistrationLeaf()
    {
        var result = await _target.GetRegistrationLeafAsync(TestData.RegistrationLeafListedUrl);

        Assert.NotNull(result);
        Assert.True(result.Listed);
        Assert.Equal(2010, result.Published.Year);

        Assert.Equal(
            "https://test.example/v3/content/test.package/1.0.0/test.package.1.0.0.nupkg",
            result.PackageContentUrl);
        Assert.Equal(
            "https://test.example/v3/metadata/test.package/index.json",
            result.RegistrationIndexUrl);
        Assert.Equal(
            "https://test.example/v3/metadata/test.package/1.0.0.json",
            result.RegistrationLeafUrl);
    }

    [Fact]
    public async Task GetsRegistrationLeafUnlisted()
    {
        var result = await _target.GetRegistrationLeafAsync(TestData.RegistrationLeafUnlistedUrl);

        Assert.NotNull(result);
        Assert.False(result.Listed);
        Assert.Equal(2010, result.Published.Year);

        Assert.Equal(
            "https://test.example/v3/content/paged.package/2.0.0/paged.package.2.0.0.nupkg",
            result.PackageContentUrl);
        Assert.Equal(
            "https://test.example/v3/metadata/paged.package/index.json",
            result.RegistrationIndexUrl);
        Assert.Equal(
            "https://test.example/v3/metadata/paged.package/2.0.0.json",
            result.RegistrationLeafUrl);
    }
}
