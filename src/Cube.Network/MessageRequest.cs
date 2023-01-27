using Cube.Api.Network;
using Cube.Api.Network.Communication;
using DotNetty.Buffers;

namespace Cube.Network;

public class MessageRequest : IMessageRequest
{
    public short Header { get; set; }
    
    public IByteBuffer Buffer { get; set; }

    public short GetHeader()
    {
        return Header;
    }

    public short ReadShort()
    {
        return Buffer.ReadShort();
    }
}