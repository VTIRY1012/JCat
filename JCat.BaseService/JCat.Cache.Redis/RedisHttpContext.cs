using JCat.Cache.Redis.Interface;
using JCat.Cache.Redis.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace JCat.Cache.Redis;

internal static class RedisHttpContext
{
    internal static IRedisCache GetRedisCache(this ActionExecutingContext context, bool usePrefix)
    {
        if (usePrefix)
        {
            return context.HttpContext.RequestServices.GetService<IPrefixRedisCache>();
        }

        return context.HttpContext.RequestServices.GetService<IRedisCache>();
    }

    internal static string GetFullCacheKey(this ActionExecutingContext context, string cacheKey)
    {
        var args = context.ActionArguments;
        var param = RedisConvertor.Serialize(args);
        var hash = RedisHash.ParameterToKeyBySHA256(param, cacheKey);
        var key = $"{cacheKey}_{hash}";
        return key;
    }

    internal static object GetResult(this ActionExecutedContext context)
    {
        var objectResult = context.Result as ObjectResult;
        if (objectResult == null || objectResult.Value == null)
        {
            return null;
        }

        var result = RedisConvertor.Deserialize<DefaultResult>(RedisConvertor.Serialize(objectResult.Value));
        return result?.Data;
    }

    internal static DefaultResult ToDefaultResult(this object obj)
    {
        if (obj == null)
        {
            return null;
        }

        var result = new DefaultResult(HttpStatusCode.OK, obj);
        return result;
    }

    // Cache Excute
    internal static async Task SetRedsiCacheAsync(this ActionExecutingContext context, bool usePrefix, string cacheKey, object? cacheValue, int cacheTime, RedisDatabaseEnum databaseEnum)
    {
        var key = context.GetFullCacheKey(cacheKey);
        var redisCahce = context.GetRedisCache(usePrefix);
        _ = await redisCahce?.SetAsync(key, cacheValue, cacheTime, databaseEnum);
    }

    internal static async Task<string?> GetRedsiCacheAsync(this ActionExecutingContext context, bool usePrefix, string cacheKey, RedisDatabaseEnum databaseEnum)
    {
        var key = context.GetFullCacheKey(cacheKey);
        var redisCahce = context.GetRedisCache(usePrefix);
        var cacheResult = await redisCahce?.GetAsync<object>(key, databaseEnum);
        if (cacheResult == null)
        {
            return null;
        }

        var result = RedisConvertor.Serialize(cacheResult.ToDefaultResult());
        return result;
    }

    internal static async Task RemoveRedsiCacheAsync(this ActionExecutingContext context, bool usePrefix, string cacheKey, RedisDatabaseEnum databaseEnum)
    {
        var key = context.GetFullCacheKey(cacheKey);
        var redisCahce = context.GetRedisCache(usePrefix);
        await redisCahce?.RemoveAsnyc(key, databaseEnum);
    }
}