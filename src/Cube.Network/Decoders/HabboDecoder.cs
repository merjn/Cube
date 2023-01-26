using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace Cube.Network.Decoders;

public class HabboDecoder : ByteToMessageDecoder
{
    protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
    {
        var header = input.ReadShort();
        var buffer = Unpooled.CopiedBuffer(input.ReadBytes(input.ReadableBytes));
        
        output.Add(new MessageRequest(header, buffer));
    }
}