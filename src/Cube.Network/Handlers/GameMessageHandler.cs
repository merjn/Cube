using Cube.Api.Router;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;

namespace Cube.Network.Handlers;

public class GameMessageHandler : SimpleChannelInboundHandler<MessageRequest>
{
    private readonly IRouter _router;
    private readonly ILogger<GameMessageHandler> _logger;

    public GameMessageHandler(IRouter router, ILogger<GameMessageHandler> logger)
    {
        _router = router;
        _logger = logger;
    }
    
    /// <summary>
    /// Pass the request to the router.
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="msg"></param>
    protected override void ChannelRead0(IChannelHandlerContext ctx, MessageRequest msg)
    {
        try
        {
            _logger.Log(LogLevel.Debug, "Received message from {0}", ctx.Channel.RemoteAddress);

            var response = _router.Dispatch(msg);

            var serverMessages = response.GetMessages();
            for (var i = 0; i < serverMessages.Count; i++)
            {
                // Send it synchronously, because we don't want to send multiple messages at once. The game client requires
                // a packet to be fully received before it can process the next one.
                ctx.WriteAndFlushAsync(serverMessages[i]);
            }
        }
        finally
        {
            msg.GetBuffer().Release();
        }
    }
}