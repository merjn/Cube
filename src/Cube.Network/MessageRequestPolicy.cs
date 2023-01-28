using Microsoft.Extensions.ObjectPool;

namespace Cube.Network;

public class MessageRequestPolicy : IPooledObjectPolicy<MessageRequest>
{
    public MessageRequest Create()
    {
        return new MessageRequest();
    }

    public bool Return(MessageRequest obj)
    {
        return true;
    }
}