using System.Threading;
using ds.seven.core.Data.Caching;
using ds.seven.core.DI;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Xunit;

namespace ds.seven.tests.Unit.Data.Caching;

public class MemoryCacheServiceTest : BaseTest
{
    private readonly ICacheService _cacheService;
    private readonly IMemoryCache _memoryCache;
    private const string CacheKey = "KEY";

    public MemoryCacheServiceTest()
    {
        _cacheService = DependencyResolver.GetService<ICacheService>();
        _memoryCache = DependencyResolver.GetService<IMemoryCache>();
        _cacheService.Remove(CacheKey);
    }

    [Fact]
    public void Test_Get_Should_Get_Item_In_Cache()
    {
        _memoryCache.TryGetValue(CacheKey, out _).Should().BeFalse();
        _cacheService.Set(CacheKey,"test-value", 10);

        _cacheService.Get(CacheKey).Should().Be("test-value");

    }
    
    [Fact]
    public void Test_Set_Should_Set_Item_In_Cache()
    {
        _memoryCache.TryGetValue(CacheKey, out _).Should().BeFalse();
        
        _cacheService.Set(CacheKey,"test-value", 10);

        _memoryCache.TryGetValue(CacheKey, out var cachedValue).Should().BeTrue();
        cachedValue.Should().Be("test-value");

    }
    
    [Fact]
    public void Test_Remove_Should_Remove_Item_In_Cache()
    {
        _memoryCache.TryGetValue(CacheKey, out _).Should().BeFalse();
        _cacheService.Set(CacheKey,"test-value", 10);
        _memoryCache.TryGetValue(CacheKey, out var cachedValue).Should().BeTrue();
        
        _cacheService.Remove(CacheKey);
        
        _memoryCache.TryGetValue(CacheKey, out _).Should().BeFalse();
    }
    
    [Fact]
    public void Test_Set_Should_Obey_Expiry()
    {
        _memoryCache.TryGetValue(CacheKey, out _).Should().BeFalse();
        _cacheService.Set(CacheKey,"test-value", 2);
        _memoryCache.TryGetValue(CacheKey, out var cachedValue).Should().BeTrue();
        
        Thread.Sleep(3000);
        
        _memoryCache.TryGetValue(CacheKey, out _).Should().BeFalse();
    }
}