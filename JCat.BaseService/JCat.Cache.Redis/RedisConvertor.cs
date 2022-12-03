using System.Text.Json;

namespace JCat.Cache.Redis;

public static class RedisConvertor
{
    public static string Serialize<T>(T value)
    {
        return JsonSerializer.Serialize(value, RedisConfig.JsonSerializerOptions);
    }

    public static T Deserialize<T>(string value)
    {
        return JsonSerializer.Deserialize<T>(value, RedisConfig.JsonSerializerOptions);
    }
}