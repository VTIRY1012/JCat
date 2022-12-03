using JCat.BaseService;
using JCat.Cache.Redis.Attributes;
using JCat.Cache.Redis.Interface;
using Microsoft.AspNetCore.Mvc;

namespace JCat.Cache.Api.Controller;

[ApiController]
[Route("Redis")]
public class RedisController : BaseServiceController
{
    private readonly IRedisCache _redisCache;
    private readonly IPrefixRedisCache _prefixRedisCache;
    public RedisController(
        IRedisCache redisCache,
        IPrefixRedisCache prefixRedisCache)
    {
        _redisCache = redisCache;
        _prefixRedisCache = prefixRedisCache;
    }

    private const string testKey = "test";

    [HttpGet]
    [Route("")]
    public async Task<JResult> GetAsync()
    {
        var result = await _redisCache.GetAsync<TestRedisModel>(testKey);
        return Successed(result);
    }

    [HttpPost]
    [Route("")]
    public async Task<JResult> SetAsync()
    {
        var result = await _redisCache.SetAsync(testKey, new TestRedisModel());
        return Successed(result);
    }

    [HttpDelete]
    [Route("")]
    public async Task<JResult> RemoveAsync()
    {
        var result = await _redisCache.RemoveAsnyc(testKey);
        return Successed(result);
    }

    // Prefix Key
    [HttpGet]
    [Route("Prefix")]
    public async Task<JResult> GetPrefixAsync()
    {
        var result = await _prefixRedisCache.GetAsync<TestRedisModel>(testKey);
        return Successed(result);
    }

    [HttpPost]
    [Route("Prefix")]
    public async Task<JResult> SetPrefixAsync()
    {
        var value = new TestRedisModel() { Id= 456, Name = "test2" };
        var result = await _prefixRedisCache.SetAsync(testKey, value);
        return Successed(result);
    }

    [HttpDelete]
    [Route("Prefix")]
    public async Task<JResult> RemovePrefixAsync()
    {
        var result = await _prefixRedisCache.RemoveAsnyc(testKey);
        return Successed(result);
    }
}

[ApiController]
[Route("RedisAttribute")]
public class RedisAttributeController : BaseServiceController
{
    private const string testKey = "test";

    [GetRedis(CacheKey = testKey, UsePrefix = false)]
    [HttpGet]
    [Route("")]
    public async Task<JResult> GetAttributeAsync([FromQuery] TestRedisParamModel model)
    {
        return Successed(null).NullReturnNotFound();
    }

    [SetRedis(CacheKey = testKey, UsePrefix = false)]
    [HttpPost]
    [Route("")]
    public async Task<JResult> SetAttributeAsync([FromBody] TestRedisParamModel model)
    {
        return Successed(model);
    }

    [RemoveRedisExecuted(CacheKey = testKey, UsePrefix = false)]
    [HttpDelete]
    [Route("")]
    public async Task<JResult> RemoveAttributeAsync([FromBody] TestRedisParamModel model)
    {
        return Successed(null).NullReturnNotFound();
    }

    // Prefix Key
    [GetRedis(CacheKey = testKey)]
    [HttpGet]
    [Route("Prefix")]
    public async Task<JResult> GetPrefixAttributeAsync([FromQuery] TestRedisParamModel model)
    {
        return Successed(null).NullReturnNotFound();
    }

    [SetRedis(CacheKey = testKey)]
    [HttpPost]
    [Route("Prefix")]
    public async Task<JResult> SetPrefixAttributeAsync([FromBody] TestRedisParamModel model)
    {
        return Successed(model);
    }

    [RemoveRedisExecuted(CacheKey = testKey)]
    [HttpDelete]
    [Route("Prefix")]
    public async Task<JResult> RemovePrefixAttributeAsync([FromBody] TestRedisParamModel model)
    {
        return Successed(null).NullReturnNotFound();
    }
}

public class TestRedisModel
{
    public int Id { get; set; } = 123;
    public string Name { get; set; } = "test";
}

public class TestRedisParamModel
{
    public int Id { get; set; } = 123;
    public string Name { get; set; } = "test";
    public List<string> List { get; set; } = new List<string>() { "cat", "dog" };
}