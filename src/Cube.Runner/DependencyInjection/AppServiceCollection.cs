using Cube.Api.Network;
using Cube.Network;
using Microsoft.Extensions.DependencyInjection;

namespace Cube.Runner.DependencyInjection;

static class AppServiceCollection
{
    /// <summary>
    /// Load the dependencies into the dependency injection container.
    /// </summary>
    /// <param name="services"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void Load(IServiceCollection services)
    {
        LoadNetwork(services);
    }
    
    private static void LoadNetwork(IServiceCollection services)
    {
        // TODO: Get config data from config file
        services.AddSingleton(new ServerConfig());
        services.AddScoped<ServerBootstrapFactory>();
        services.AddScoped<IServerRunner, ServerRunner>();
    }
}