using JCat.Cache.Redis.Interface;
using JCat.Cache.Redis.Model;
using Microsoft.Extensions.DependencyInjection;

namespace JCat.Cache.Redis;
public static class RedisExtension
{
    public static void AddRedisCahce(this IServiceCollection services, RedisConfigSettings configSettings)
    {
        RedisConfig.InitializeRedis(configSettings);
        services.AddSingleton<IRedisCache, RedisCache>();
        services.AddSingleton<IPrefixRedisCache, PrefixRedisCache>();
    }
}