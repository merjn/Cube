using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace Cube.Network.Decoders;

public class HabboDecoder : ByteToMessageDecoder
{
    private readonly MessageRequestPool _pool;
    
    public HabboDecoder(MessageRequestPool pool)
    {
        _pool = pool;
    }
    
    protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
    {
        var header = input.ReadShort();
        
        output.Add(_pool.Rent(header, input));
    }
}