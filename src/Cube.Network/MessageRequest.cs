using Cube.Api.Network;
using Cube.Api.Network.Communication;
using DotNetty.Buffers;

namespace Cube.Network;

public class MessageRequest : IMessageRequest
{
    private short _header;
    private IByteBuffer _buf;

    public MessageRequest(short header, IByteBuffer buffer)
    {
        _header = header;
        _buf = buffer;
    }

    public short GetHeader()
    {
        return _header;
    }

    public short ReadShort()
    {
        return _buf.ReadShort();
    }
}