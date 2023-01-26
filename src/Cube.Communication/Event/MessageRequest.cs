using Cube.Api.Network;
using Cube.Api.Network.Communication;

namespace Cube.Communication.Event;

public class MessageRequest : IMessageRequest
{
    public MessageRequest(IPacketReader reader)
    {
        throw new NotImplementedException();
    }
    
    public short GetHeader()
    {
        throw new NotImplementedException();
    }

    public short ReadShort()
    {
        throw new NotImplementedException();
    }
}