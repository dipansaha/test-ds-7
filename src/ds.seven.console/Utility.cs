using ds.seven.core.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ds.seven.console;

public static class Utility
{
    public static void Setup()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{GetEnv()}.json", optional: true, reloadOnChange: true)
            .Build();
        
        var services = new ServiceCollection();
        DependencyResolver.Init(services, configuration);
    }

    private static string GetEnv()
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        return string.IsNullOrEmpty(env) ? "DEV" : env;
    }
}