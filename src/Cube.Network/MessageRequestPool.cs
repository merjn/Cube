using DotNetty.Buffers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;

namespace Cube.Network;

public class MessageRequestPool
{
    private readonly ObjectPool<MessageRequest> _objectPool;
    private readonly ILogger<MessageRequestPool> _logger;
    
    /// <summary>
    /// Create a new message request pool.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="objectPool"></param>
    public MessageRequestPool(ILogger<MessageRequestPool> logger, ObjectPool<MessageRequest> objectPool)
    {
        _logger = logger;
        _objectPool = objectPool;
    }

    public MessageRequest Get(short header, IByteBuffer buffer)
    {
        MessageRequest messageRequest = _objectPool.Get();
        messageRequest.Header = header;
        messageRequest.Buffer = buffer;
        
        _logger.LogInformation("Message request object retrieved from pool.");

        return messageRequest;
    }
    
    public void Return(MessageRequest messageRequest)
    {
        _logger.LogInformation("Message request object removed from pool.");

        _objectPool.Return(messageRequest);
    }
}