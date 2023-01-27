using Cube.Network.Decoders;
using Cube.Network.Encoders;
using Cube.Network.Handlers;
using DotNetty.Transport.Channels;

namespace Cube.Network.Channel;

public class ChannelInitializer : IChannelInitializer
{
    /// <summary>
    /// Encodes messages to the client.
    /// </summary>
    private readonly HabboEncoder _habboEncoder;
    
    /// <summary>
    /// Decodes messages from the client.
    /// </summary>
    private readonly HabboDecoder _habboDecoder;
    
    /// <summary>
    /// Handles messages from the client.
    /// </summary>
    private readonly GameMessageHandler _gameMessageHandler;

    /// <summary>
    /// Creates a new child handler factory.
    /// </summary>
    /// <param name="habboEncoder"></param>
    /// <param name="habboDecoder"></param>
    /// <param name="gameMessageHandler"></param>
    public ChannelInitializer(
        HabboEncoder habboEncoder, 
        HabboDecoder habboDecoder, 
        GameMessageHandler gameMessageHandler
    ) 
    {
        _habboEncoder = habboEncoder;
        _habboDecoder = habboDecoder;
        _gameMessageHandler = gameMessageHandler;
    }

    public void InitChannel(IChannel channel)
    {
        var pipeline = channel.Pipeline;
            
        pipeline.AddLast("encoder", _habboEncoder);
        pipeline.AddLast("decoder", _habboDecoder);
        pipeline.AddLast("handler", _gameMessageHandler);
    }
}