using ds.seven.core.Exception;
using ds.seven.core.Utility;
using D = ds.seven.core.Domain;

namespace ds.seven.core.Data.Provider;

public class JsonApiDataProvider : IDataProvider
{
    private readonly D.AppSettings _appSettings;
    private readonly HttpClient _httpClient;

    

    public JsonApiDataProvider(D.AppSettings appSettings, HttpClient httpClient)
    {
        _appSettings = appSettings;
        _httpClient = httpClient;
    }
    
    public async Task<IReadOnlyCollection<D.User>?> Load()
    {
        var response = await _httpClient.GetAsync(new Uri(_appSettings.Data.APIUrl));

        if (!response.IsSuccessStatusCode)
            throw new ApiNotAvailableException();
        
        var content = await response.Content.ReadAsStringAsync();

        try
        {
            return content.FromJsonString<IReadOnlyCollection<D.User>>() ??
                   throw new InvalidOperationException("data can not be null");
        }
        catch (System.Exception)
        {
            throw new InvalidJsonException();
        }
    }
}