using System.Text.Json;
using ds.seven.core.Exception;
using ds.seven.core.Utility;
using D = ds.seven.core.Domain;

namespace ds.seven.core.Data.Provider;

public class JsonFileDataProvider : IDataProvider
{
    public Task<IReadOnlyCollection<D.User>?> Load()
    {
        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        return Task.FromResult(JsonSerializer.Deserialize<IReadOnlyCollection<D.User>>(File.ReadAllText("./Data/data.json"), serializeOptions)) ;
    }
}