using ds.seven.core.Service.Writer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ds.seven.core.Service;

public static class ServiceInjection
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWriter<string>, ConsoleWriter>();

    }
}