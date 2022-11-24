using JCat.BaseService.Config;
using JCat.BaseService.Const;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace JCat.BaseService.Extensions
{
    public static class JCatStartupExtension
    {
        // builder
        public static IWebHostBuilder ConfigureEnvironmentRuntime(this IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, builder) =>
            {
                var runtime = new JRunTime
                {
                    ASPNETCORE_ENVIRONMENT = context.Configuration[EnvironmentVariablesConst.ModeKey],
                    Application = new JApplication()
                    {
                        ApplicationKey = context.Configuration[EnvironmentVariablesConst.ApplicationKey],
                        ApplicationName = context.Configuration[EnvironmentVariablesConst.ApplicationName]
                    }
                };
                EnvironmentMode.Initialize(runtime);
            });
            return builder;
        }

        public static void AddBaseLog(this ILoggingBuilder logging)
        {
            logging.AddJsonConsole();
        }

        public static void AddBaseConfigurations(this ConfigurationManager configuration)
        {
            configuration.AddEnvironmentVariables();
            configuration.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
        }

        public static void AddBaseServices(this IServiceCollection services)
        {

        }

        // todo: coming soon, read config server.
        // todo: coming soon, cache library.
    }
}
