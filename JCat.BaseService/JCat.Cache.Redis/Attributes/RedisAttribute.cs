using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JCat.Cache.Redis.Attributes;
public class GetRedisAttribute : Attribute, IAsyncActionFilter
{
    public bool UsePrefix { get; set; } = true;
    public string CacheKey { get; set; } = string.Empty;
    public RedisDatabaseEnum DatabaseEnum { get; set; } = RedisDatabaseEnum.Important;
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var result = await context.GetRedsiCacheAsync(UsePrefix, CacheKey, DatabaseEnum);
        if (result == null)
        {
            await next();
        }
        else
        {
            await context.HttpContext.Response.WriteAsync(result);
            return;
        }
    }
}

public class SetRedisAttribute : Attribute, IAsyncActionFilter
{
    public bool UsePrefix { get; set; } = true;
    public string CacheKey { get; set; } = string.Empty;
    public int CacheTime { get; set; } = RedisConst.DefaultCacheTime;
    public RedisDatabaseEnum DatabaseEnum { get; set; } = RedisDatabaseEnum.Important;
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var result = await next();

        var value = result.GetResult();
        await context.SetRedsiCacheAsync(UsePrefix, CacheKey, value, CacheTime, DatabaseEnum);
    }
}

public class RemoveRedisExecutingAttribute : Attribute, IAsyncActionFilter
{
    public bool UsePrefix { get; set; } = true;
    public string CacheKey { get; set; } = string.Empty;
    public RedisDatabaseEnum DatabaseEnum { get; set; } = RedisDatabaseEnum.Important;
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        await context.RemoveRedsiCacheAsync(UsePrefix, CacheKey, DatabaseEnum);
        await next();
    }
}

public class RemoveRedisExecutedAttribute : Attribute, IAsyncActionFilter
{
    public bool UsePrefix { get; set; } = true;
    public string CacheKey { get; set; } = string.Empty;
    public RedisDatabaseEnum DatabaseEnum { get; set; } = RedisDatabaseEnum.Important;
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        await next();
        await context.RemoveRedsiCacheAsync(UsePrefix, CacheKey, DatabaseEnum);
    }
}