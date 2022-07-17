using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Xunit;

using ds.seven.core.Data;
using ds.seven.core.Data.Caching;
using ds.seven.core.Data.Provider;
using ds.seven.core.DI;
using ds.seven.core.Domain;
using ds.seven.core.Utility;
using ds.seven.tests.Utility;

namespace ds.seven.tests.Unit.Data;

public class DataLoaderTest : BaseTest
{
    private readonly AppSettings _appSettings;
    private readonly ICacheService _cacheService;
    private readonly IDataProvider _dataProvider;
    private const string UserCacheKey = "UserCacheKey";

    public DataLoaderTest()
    {
        _appSettings = DependencyResolver.GetService<AppSettings>();
        _cacheService = Substitute.For<ICacheService>();
        _dataProvider = Substitute.For<IDataProvider>();
    }

    [Fact]
    public async Task Test_Load_Should_Load_Data_From_Cache()
    {
        _cacheService.Get(Arg.Any<string>()).Returns(TestData.GetData().ToJsonString());

        var dataLoader = new DataLoader(_appSettings, _cacheService, _dataProvider);

        var response = await dataLoader.Load();

        response.Should().BeEquivalentTo(TestData.GetData());
        _cacheService.Received(1).Get(Arg.Is(UserCacheKey));
    }
    
    [Fact]
    public async Task Test_Load_Should_Load_Data_From_Json_Api_When_Cahce_Is_Empty()
    {
        var data = TestData.GetData();
        _cacheService.Get(Arg.Any<string>()).Returns(string.Empty);
        _dataProvider.Load().Returns((IReadOnlyCollection<User>?) data);
        _cacheService.Set(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>());

        var dataLoader = new DataLoader(_appSettings, _cacheService, _dataProvider);

        var response = await dataLoader.Load();

        response.Should().BeEquivalentTo(TestData.GetData());
        _cacheService.Received(1).Get(Arg.Is(UserCacheKey));
        await _dataProvider.Received(1).Load();
        _cacheService.Received(1).Set(Arg.Is(UserCacheKey), Arg.Is(data.ToJsonString()), Arg.Is(_appSettings.Data.CacheExpiryInSeconds));
    }
}