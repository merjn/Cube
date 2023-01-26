using Cube.Api.Network;
using Cube.Network.Decoders;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;

namespace Cube.Network;

public class ServerRunner : IServerRunner
{
    private readonly ServerBootstrapFactory _serverBootstrapFactory;
    
    public ServerRunner(ServerBootstrapFactory serverBootstrapFactory)
    {
        _serverBootstrapFactory = serverBootstrapFactory;
    }
    
    public async Task StartAsync()
    {
        var bootstrap = _serverBootstrapFactory.Create();

        try
        {
            var bootstrapChannel = await bootstrap.BindAsync(3000);
            
            Console.ReadLine();

            await bootstrapChannel.CloseAsync();
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
        }

    }
}