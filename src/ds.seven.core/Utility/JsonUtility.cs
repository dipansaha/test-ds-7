using System.Text.Json;

namespace ds.seven.core.Utility;

public static class JsonUtility
{
    public static string ToJsonString<T>(this T obj)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        
        return JsonSerializer.Serialize(obj, options);
    }
    
    public static T? FromJsonString<T>(this string jsonStr)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return JsonSerializer.Deserialize<T>(jsonStr, options);
    }
}