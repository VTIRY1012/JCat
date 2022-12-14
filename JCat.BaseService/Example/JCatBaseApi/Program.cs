using JCat.BaseService;
using JCat.BaseService.Config;
using JCat.BaseService.Converter;
using JCat.BaseService.Extensions;
using JCat.BaseService.Extensions.Service;
using JCatBaseSDK;
using JCatBaseSDK.Model;
using JCatService;
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
builder.Services.AddBOServices();
builder.Services.AddBORepositories();
builder.Services.AddTestClient(options =>
{
    options.Settings = new TestClientSettings("https://localhost:7048", EnvironmentMode.RunTimeSettings.Application.ApplicationKey, JCatConverterSettings.GetBaseOptions());
    options.Encoder = new TestAppKeyEncoder();
});
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
