using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace Cube.Network.Decoders;

public class HabboDecoder : ByteToMessageDecoder
{
    protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
    {
        var header = input.ReadShort();

        output.Add(new MessageRequest(header, input));
    }
}