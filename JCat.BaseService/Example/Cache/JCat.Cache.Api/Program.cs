using JCat.BaseService;
using JCat.BaseService.Config;
using JCat.BaseService.Converter;
using JCat.BaseService.Extensions;
using JCat.BaseService.Extensions.Service;
using JCat.Cache.Redis;
using JCat.Cache.Redis.Model;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureEnvironmentRuntime();
builder.Logging.AddBaseLog();
builder.Configuration.AddBaseConfigurations();
builder.Services.AddBaseServices();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();
builder.Services.AddCors();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.AddBaseJsonSettings());

// todo: set redis connection string.
var redisConnectionString = "";
builder.Services.AddRedisCahce(new RedisConfigSettings(redisConnectionString, JCatConverterSettings.GetBaseOptions(), EnvironmentMode.RunTimeSettings.Application.ApplicationName));
var app = builder.Build();

app.UseCors(policy =>
{
    policy
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();
});
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.AddBaseMiddleware();

// Routes
app.MapGet("/", async Task<JResult> () => await Task.FromResult(new JResult(HttpStatusCode.OK, "Healthy")));
app.MapControllers();
app.Run();
