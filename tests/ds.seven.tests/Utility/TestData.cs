using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using D = ds.seven.core.Domain;

namespace ds.seven.tests.Utility;

public static class TestData
{
    public static IEnumerable<D.User>? GetData()
    {
        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        return JsonSerializer.Deserialize<IEnumerable<D.User>>(File.ReadAllText("./Data/data.json"), serializeOptions);
    }
}