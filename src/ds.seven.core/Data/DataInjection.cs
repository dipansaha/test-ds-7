using System.Net;
using ds.seven.core.Data.Caching;
using ds.seven.core.Data.Provider;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace ds.seven.core.Data;

public static class DataInjection
{
    public static void Configure(IServiceCollection services, HttpMessageHandler? httpMessageHandler)
    {
        var jsonApiDataProvider = services.AddHttpClient<IDataProvider, JsonApiDataProvider>()
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddPolicyHandler(GetRetryPolicy());

        if (httpMessageHandler != null)
            jsonApiDataProvider.ConfigurePrimaryHttpMessageHandler(() => httpMessageHandler);

        // services.AddScoped<IDataProvider, JsonFileDataProvider>();


        services.AddMemoryCache();
        services.AddScoped<ICacheService, MemoryCacheService>();
        services.AddScoped<IDataLoader, DataLoader>();
    }

    static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode >= HttpStatusCode.NotFound)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromMilliseconds(Math.Pow(10,
                retryAttempt)));
    }
}