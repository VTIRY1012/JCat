using JCat.Cache.Redis.Interface;
using StackExchange.Redis;

namespace JCat.Cache.Redis;
public class RedisCache : IRedisCache
{
    private static Lazy<ConnectionMultiplexer> _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
    {
        var connectionString = RedisConfig.ConnectionString;
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentNullException(RedisMessage.ConnectionStringNotFoundError);
        }
        return ConnectionMultiplexer.Connect(connectionString);
    });

    public static ConnectionMultiplexer RedisConnection => _lazyConnection.Value;
    public static IDatabase Database(int db) => RedisConnection.GetDatabase(db);

    public async Task<bool> IsExistAsync(string key, RedisDatabaseEnum redisDatabaseEnum = RedisDatabaseEnum.Important)
    {
        var result = await Database((int)redisDatabaseEnum).KeyExistsAsync(key);
        return result;
    }

    public async Task<T> GetAsync<T>(string key, RedisDatabaseEnum redisDatabaseEnum = RedisDatabaseEnum.Important)
    {
        var isExist = await IsExistAsync(key, redisDatabaseEnum);
        if (!isExist)
        {
            return default;
        }

        var result = await Database((int)redisDatabaseEnum).StringGetAsync(key);
        if (result.HasValue)
        {
            return RedisConvertor.Deserialize<T>(result);
        }

        return default;
    }

    public async Task<T> SetAsync<T>(string key, T value, int seconds = RedisConst.DefaultCacheTime, RedisDatabaseEnum redisDatabaseEnum = RedisDatabaseEnum.Important) where T : new()
    {
        var expiresAt = DateTimeOffset.UtcNow.AddSeconds(seconds);
        TimeSpan timeSpan = expiresAt.UtcDateTime.Subtract(DateTime.UtcNow);
        var isSuccess = await Database((int)redisDatabaseEnum).StringSetAsync(key, RedisConvertor.Serialize(value), timeSpan, When.Always, CommandFlags.None);
        if (isSuccess)
        {
            return value;
        }

        return default;
    }

    public async Task<bool> RemoveAsnyc(string key, RedisDatabaseEnum redisDatabaseEnum = RedisDatabaseEnum.Important)
    {
        return await Database((int)redisDatabaseEnum).KeyDeleteAsync(key);
    }
}

public class PrefixRedisCache : IPrefixRedisCache
{
    private readonly IRedisCache _redisCache;
    public PrefixRedisCache(IRedisCache redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task<bool> IsExistAsync(string key, RedisDatabaseEnum redisDatabaseEnum = RedisDatabaseEnum.Important)
    {
        key = ToPrefix(key);
        return await _redisCache.IsExistAsync(key, redisDatabaseEnum);
    }

    public async Task<T> GetAsync<T>(string key, RedisDatabaseEnum redisDatabaseEnum = RedisDatabaseEnum.Important)
    {
        key = ToPrefix(key);
        return await _redisCache.GetAsync<T>(key, redisDatabaseEnum);
    }

    public async Task<T> SetAsync<T>(string key, T value, int seconds = 600, RedisDatabaseEnum redisDatabaseEnum = RedisDatabaseEnum.Important) where T : new()
    {
        key = ToPrefix(key);
        return await _redisCache.SetAsync<T>(key, value, seconds, redisDatabaseEnum);
    }

    public async Task<bool> RemoveAsnyc(string key, RedisDatabaseEnum redisDatabaseEnum = RedisDatabaseEnum.Important)
    {
        key = ToPrefix(key);
        return await _redisCache.RemoveAsnyc(key, redisDatabaseEnum);
    }

    #region Private
    private static string ToPrefix(string key)
    {
        return $"{RedisConfig.PrefixString}__{key}";
    }
    #endregion
}