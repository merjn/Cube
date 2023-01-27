using Cube.Api.Network;
using Cube.Network;
using Cube.Network.Channel;
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
        LoadRouter(services);
        LoadNetwork(services);
    }

    private static void LoadRouter(IServiceCollection services)
    {
        
    }
    
    private static void LoadNetwork(IServiceCollection services)
    {
        // Create a new instance of ChannelInitializer.
        //
        // // TODO: Get config data from config file
        // services.AddSingleton(new ServerConfig());
        // services.AddScoped<ServerBooter>();
        // services.AddScoped<IServerRunner, ServerRunner>();
    }
}