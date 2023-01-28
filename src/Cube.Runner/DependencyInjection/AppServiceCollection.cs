using Cube.Api.Network;
using Cube.Api.Router;
using Cube.Network;
using Cube.Network.Channel;
using Cube.Network.Decoders;
using Cube.Network.Encoders;
using Cube.Network.Handlers;
using Cube.Router;
using Cube.Router.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;

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
        services.AddScoped<ILoggerFactory, LoggerFactory>();
        
        services.AddSingleton<IRouteRepository>(new RouteRepository());
        services.AddSingleton<IMiddlewareDispatcherFactory, MiddlewareDispatcherFactory>();

        services.AddSingleton<IRouter, Router.Router>();
    }
    
    private static void LoadNetwork(IServiceCollection services)
    {
        services.AddSingleton<MessageRequestPool>(provider =>
        {
            var objectPool = new DefaultObjectPool<MessageRequest>(new MessageRequestPolicy());
            var logger = (provider.GetService<ILoggerFactory>() ?? throw new InvalidOperationException()).CreateLogger<MessageRequestPool>();
            
            return new MessageRequestPool(logger, objectPool);
        });

        services.AddSingleton<ILogger<GameMessageHandler>>((provider) => (provider.GetService<ILoggerFactory>() ?? throw new InvalidOperationException()).CreateLogger<GameMessageHandler>());
        
        services.AddSingleton(new ServerConfig());
        services.AddSingleton<HabboEncoder>();
        services.AddSingleton<HabboDecoder>();
        services.AddSingleton<GameMessageHandler>();
        
        services.AddScoped<IChannelInitializer, ChannelInitializer>();
        
        services.AddSingleton<ILogger<ServerBooter>>((provider) => (provider.GetService<ILoggerFactory>() ?? throw new InvalidOperationException()).CreateLogger<ServerBooter>());

        services.AddSingleton<ServerBooter>();
    }
}