using ds.seven.core.Data;
using ds.seven.core.Domain;
using ds.seven.core.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ds.seven.core.DI;

public static class DependencyResolver
{
    private static IServiceProvider? _serviceProvider;
    private static bool _initialised;

    private static readonly object LockObject = new();

    public static void Init(IServiceCollection services, IConfiguration configuration, HttpMessageHandler? httpMessageHandler = null)
    {
        if (_initialised)
        {
            return;
        }

        lock (LockObject)
        {
            if (_initialised)
            {
                return;
            }

            Configure(services, configuration, httpMessageHandler);
            _initialised = true;
        }
    }

    public static T GetService<T>() where T : class
    {
        return _serviceProvider!.GetService<T>()!;
    }

    private static void Configure(IServiceCollection services, IConfiguration configuration, HttpMessageHandler? httpMessageHandler)
    {
        services.Configure<AppSettings>(configuration);
        services.AddScoped(sp => sp.GetRequiredService<IOptionsSnapshot<AppSettings>>().Value);
        
        DataInjection.Configure(services, httpMessageHandler);
        ServiceInjection.Configure(services, configuration);

        _serviceProvider = services.BuildServiceProvider();
    }

}