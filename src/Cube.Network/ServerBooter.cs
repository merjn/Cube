using Cube.Network.Channel;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Microsoft.Extensions.Logging;

namespace Cube.Network;

public class ServerBooter
{
    private readonly ServerConfig _serverConfig;
    private readonly IChannelInitializer _channelInitializer;
    private readonly ILogger<ServerBooter> _logger;

    public ServerBooter(
        IChannelInitializer channelInitializer, 
        ServerConfig serverConfig, 
        ILogger<ServerBooter> logger) 
    {
        _channelInitializer = channelInitializer;
        _serverConfig = serverConfig;
        _logger = logger;
    }
    
    public async Task RunAsync()
    {
        // The boss group is responsible for accepting new connections.
        var bossGroup = new MultithreadEventLoopGroup(1);
        
        // The worker group is responsible for handling the traffic of the accepted connections.
        // How many Threads are spawned is determined by the _serverConfig.WorkerThreads property.
        // As a starting point, we use the number of available processors. You can always monitor the performance
        // of the server and adjust this value if necessary.
        var workerGroup = new MultithreadEventLoopGroup(_serverConfig.WorkerThreads);

        try
        {
            var bootstrap = new ServerBootstrap();

            bootstrap.Group(bossGroup, workerGroup);
            bootstrap.Channel<TcpServerSocketChannel>();
            bootstrap.Option(ChannelOption.SoBacklog, _serverConfig.BacklogQueue);
            bootstrap.ChildHandler(new ActionChannelInitializer<IChannel>(_channelInitializer.InitChannel));
            
            // Bind the server to the specified port.
            var bootstrapChannel = await bootstrap.BindAsync(_serverConfig.Port);

            _logger.Log(LogLevel.Information, "Server started on port {0}", _serverConfig.Port);
            
            // Wait for the user to press enter.
            Console.ReadLine();
            
            // Close the server.
            await bootstrapChannel.CloseAsync();
        }
        finally
        {
            _logger.Log(LogLevel.Information, "Shutting down server...");
            
            await Task.WhenAll(bossGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
                workerGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)));
        }
    }
}