using ds.seven.core.Data.Caching;
using ds.seven.core.Data.Provider;
using D = ds.seven.core.Domain;
using ds.seven.core.Utility;

namespace ds.seven.core.Data;

public class DataLoader : IDataLoader
{
    private readonly D.AppSettings _appSettings;
    private readonly ICacheService _cacheService;
    private readonly IDataProvider _dataProvider;
    
    private const string UserCacheKey = "UserCacheKey";

    public DataLoader(D.AppSettings appSettings, ICacheService cacheService, IDataProvider dataProvider)
    {
        _appSettings = appSettings;
        _cacheService = cacheService;
        _dataProvider = dataProvider;
    }
    
    public async Task<ICollection<D.User>?> Load()
    {
        var cachedData = _cacheService.Get(UserCacheKey);

        if (!string.IsNullOrWhiteSpace(cachedData))
        {
            return cachedData.FromJsonString<ICollection<D.User>>();
        }
        
        var apiData = await _dataProvider.Load();
        
        _cacheService.Set(UserCacheKey, apiData.ToJsonString(), _appSettings.Data.CacheExpiryInSeconds);

        return (ICollection<D.User>?) apiData;

    }
}