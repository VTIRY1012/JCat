using JCat.Cache.Redis.Model;
using System.Text.Json;

namespace JCat.Cache.Redis;

internal static class RedisConfig
{
    internal static string ConnectionString { get; private set; } = string.Empty;
    internal static JsonSerializerOptions? JsonSerializerOptions { get; private set; }
    internal static string PrefixString { get; private set; } = string.Empty;
    internal static void InitializeRedis(RedisConfigSettings settings)
    {
        ConnectionString = settings.ConnectionString;
        JsonSerializerOptions = settings.JsonSerializerOptions;
        PrefixString = settings.PrefixString;
    }
}