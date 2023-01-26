using Cube.Network.Decoders;
using Cube.Network.Encoders;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;

namespace Cube.Network;

public class ServerBootstrapFactory : IDisposable
{
    private IEventLoopGroup _bossGroup;
    private IEventLoopGroup _workerGroup;

    private readonly ServerConfig _serverConfig;
    
    public ServerBootstrapFactory(ServerConfig serverConfig)
    {
        _serverConfig = serverConfig;
        
        // The boss group is responsible for accepting new connections.
        _bossGroup = new MultithreadEventLoopGroup(1);
        
        // The worker group is responsible for handling the connections. 
        // The amount of threads should be equal to the amount of cores available. Otherwise, you will be wasting resources.
        // If you have a lot of cores, you can increase the amount of threads to increase the performance.
        _workerGroup = new MultithreadEventLoopGroup(_serverConfig.WorkerThreads);
    }
    
    public ServerBootstrap Create()
    {
        var bootstrap = new ServerBootstrap();
            
        // Set the boss group and worker group so that the server can handle multiple connections.
        bootstrap.Group(_bossGroup, _workerGroup);
        bootstrap.Channel<TcpServerSocketChannel>();
        bootstrap.Option(ChannelOption.SoBacklog, 100);
        
        // Set the channel initializer. Use the one that pools the channels.
        bootstrap.ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
        {
            var pipeline = channel.Pipeline;

            pipeline.AddLast(new HabboDecoder());
            pipeline.AddLast(new GameMessageHandler());
        }));

        return bootstrap;
    }

    // Make Dispose async
    public async Task DisposeAsync()
    {
        Console.WriteLine("Killing boss group and worker group");
        
        await Task.WhenAll(_bossGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
            _workerGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)));
    }
    
    public void Dispose()
    {
        DisposeAsync().Wait();
    }
}