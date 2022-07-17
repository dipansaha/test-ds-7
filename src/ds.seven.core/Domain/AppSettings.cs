namespace ds.seven.core.Domain;

public class AppSettings
{
    public Data Data { get; set; } = new();
}

public class Data
{
    public string APIUrl { get; set; } = string.Empty;
    public int CacheExpiryInSeconds { get; set; } = 10;
}   