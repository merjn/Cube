using System.Buffers;
using DotNetty.Transport.Channels;

namespace Cube.Network;

public class GameMessageHandler : SimpleChannelInboundHandler<MessageRequest>
{
    protected override void ChannelRead0(IChannelHandlerContext ctx, MessageRequest msg)
    {
        Console.WriteLine("hoi");
        Console.WriteLine(msg);
    }
}