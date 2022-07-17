using System;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ds.seven.core.DI;

namespace ds.seven.tests;

public abstract class BaseTest
{
    protected BaseTest(bool isInitDependencyResolver = true)
    {
        if (isInitDependencyResolver)
            Init();
    }

    protected void Init(HttpMessageHandler? handler = null)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{GetEnv()}.json", optional: true, reloadOnChange: true)
            .Build();

        var services = new ServiceCollection();
        DependencyResolver.Init(services, configuration, handler);
    }

    private static string GetEnv()
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        return string.IsNullOrEmpty(env) ? "DEV" : env;
    }
}