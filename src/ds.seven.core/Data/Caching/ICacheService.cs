namespace ds.seven.core.Data.Caching;

public interface ICacheService
{
    string Get(string key);

    void Set(string key, string value, int expiryInSeconds);

    void Remove(string key);
}