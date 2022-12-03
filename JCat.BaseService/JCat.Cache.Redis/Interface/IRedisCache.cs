namespace JCat.Cache.Redis.Interface;
public interface IRedisCache
{
    Task<bool> IsExistAsync(string key, RedisDatabaseEnum redisDatabaseEnum = RedisDatabaseEnum.Important);
    Task<T> GetAsync<T>(string key, RedisDatabaseEnum redisDatabaseEnum = RedisDatabaseEnum.Important);
    Task<T> SetAsync<T>(string key, T value, int seconds = RedisConst.DefaultCacheTime, RedisDatabaseEnum redisDatabaseEnum = RedisDatabaseEnum.Important) where T : new();
    Task<bool> RemoveAsnyc(string key, RedisDatabaseEnum redisDatabaseEnum = RedisDatabaseEnum.Important);
}