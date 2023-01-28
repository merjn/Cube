using Cube.Api.Network.Communication;
using Cube.Api.Router;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;

namespace Cube.Network.Handlers;

public class GameMessageHandler : SimpleChannelInboundHandler<MessageRequest>
{
    private readonly IRouter _router;
    private readonly MessageRequestPool _pool;
    private readonly ILogger<GameMessageHandler> _logger;

    public GameMessageHandler(IRouter router, MessageRequestPool pool, ILogger<GameMessageHandler> logger)
    {
        _router = router;
        _pool = pool;
        _logger = logger;
    }
    
    protected override void ChannelRead0(IChannelHandlerContext ctx, MessageRequest msg)
    {
        try
        {
            _logger.Log(LogLevel.Debug, "Received message from {0}", ctx.Channel.RemoteAddress);

            var response = _router.Dispatch(msg);
            
            response.Match(
                (right) => WriteResponse(ctx, right),
                (left) => _logger.Log(LogLevel.Error, left.ToString())
            );
        }
        finally
        {
            msg.Buffer.Release();
            _pool.Return(msg);
        }
    }
    
    private void WriteResponse(IChannelHandlerContext ctx, IMessageResponse response)
    {
        var serverMessages = response.GetMessages();
        for (var i = 0; i < serverMessages.Count; i++)
        {
            // Send it synchronously, because we don't want to send multiple messages at once. The game client requires
            // a packet to be fully received before it can process the next one.
            ctx.WriteAndFlushAsync(serverMessages[i]);
        }
    }
}