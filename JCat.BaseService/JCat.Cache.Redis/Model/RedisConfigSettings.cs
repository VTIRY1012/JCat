using System.Text.Json;

namespace JCat.Cache.Redis.Model;
public class RedisConfigSettings
{
    public RedisConfigSettings(
        string connectionString,
        JsonSerializerOptions? jsonSerializerOptions,
        string prefixString)
    {
        this.ConnectionString = connectionString;
        this.JsonSerializerOptions = jsonSerializerOptions;
        this.PrefixString = prefixString;
    }

    public string ConnectionString { get; set; } = string.Empty;
    public JsonSerializerOptions? JsonSerializerOptions { get; set; }
    public string PrefixString { get; set; } = string.Empty;
}