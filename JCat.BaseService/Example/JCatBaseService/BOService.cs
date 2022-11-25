using JCatService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace JCatService
{
    public static class BOServiceExtension
    {
        public static void AddBOServices(this IServiceCollection services)
        {
            services.AddScoped<IExampleService, ExampleService>();
        }

        public static void AddBORepositories(this IServiceCollection services)
        {

        }
    }
}