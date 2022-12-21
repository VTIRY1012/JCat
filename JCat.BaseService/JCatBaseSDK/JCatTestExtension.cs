using JCatBaseSDK.Interface;
using JCatBaseSDK.Model;
using Microsoft.Extensions.DependencyInjection;

namespace JCatBaseSDK;
public static class JCatTestExtension
{
    public static void AddTestClient(this IServiceCollection services, Action<TestClientOptions> options)
    {
        var clientOptions = Activator.CreateInstance<TestClientOptions>();
        options(clientOptions);

        services.AddSingleton<ITestClientSettings>(clientOptions.Settings);
        services.AddSingleton<ITestAppKeyEncoder>(clientOptions.Encoder);
        services.AddTransient<ITestClient, JCatTestClient>();
        services.AddHttpClient();
        services.AddHttpContextAccessor();
    }
}