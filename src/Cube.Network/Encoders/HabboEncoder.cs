using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace Cube.Network.Encoders;

public class HabboEncoder : MessageToByteEncoder<object>
{
    protected override void Encode(IChannelHandlerContext context, object message, IByteBuffer output)
    {
        throw new NotImplementedException();
    }
}