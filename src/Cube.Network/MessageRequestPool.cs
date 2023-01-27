using System.Buffers;
using System.Collections.Concurrent;
using DotNetty.Buffers;
using Microsoft.Extensions.Logging;

namespace Cube.Network;

/// <summary>
/// Each new Habbo packet creates a new MessageRequest object. This object is then passed to the router, which will
/// then pass it to the correct handler. This object is then disposed of, and the memory is returned to the pool.
///
/// This is useful because it allows us to reuse the same memory over and over again, instead of allocating new memory -
/// causing a lot of garbage collection.
/// </summary>
public class MessageRequestPool : IDisposable
{
    private readonly MemoryPool<MessageRequest> _memory;
    private readonly ConcurrentDictionary<MessageRequest, IMemoryOwner<MessageRequest>> _storedObjects;
    private readonly ILogger<MessageRequestPool> _logger;

    private bool _disposed = false;
    
    /// <summary>
    /// Create a new message request pool.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="capacity"></param>
    public MessageRequestPool(ILogger<MessageRequestPool> logger, int capacity)
    {
        _logger = logger;
        _memory = MemoryPool<MessageRequest>.Shared;
        _storedObjects = new ConcurrentDictionary<MessageRequest, IMemoryOwner<MessageRequest>>(Environment.ProcessorCount, capacity);
    }

    public MessageRequest Rent(short header, IByteBuffer byteBuffer)
    {
        IMemoryOwner<MessageRequest> memoryOwner;
        lock (_memory)
        {
            memoryOwner = _memory.Rent();
        }

        // Get the actual object from the memory
        var obj = memoryOwner.Memory.Span[0];

        obj.Header = header;
        obj.Buffer = byteBuffer;

        if (_storedObjects.TryAdd(obj, memoryOwner))
        {
            return obj;
        }

        _logger.LogError("Could not add rented object to the stored objects dictionary.");

        // Dispose, because we couldn't add it to the stored objects dictionary. This should never happen.
        // If it does, it means that there's something wrong with the dictionary.
        memoryOwner.Dispose();
        
        // In order to prevent a null reference exception, we'll just return a new object.
        return new MessageRequest
        {
            Header = header,
            Buffer = byteBuffer
        };
    }

    public void Return(MessageRequest obj)
    {
        if (_storedObjects.TryRemove(obj, out var memoryOwner))
        {
            memoryOwner.Dispose();
            return;
        }
        
        _logger.LogError("Unable to dispose memory object - it wasn't stored in the dictionary.");
    }
    
    // Implement Dispose pattern.
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return; 
        
        if (disposing)
        {
            // Dispose managed state (managed objects).
            foreach (var (_, memoryOwner) in _storedObjects)
            {
                memoryOwner.Dispose();
            }
        }
        
        lock (_memory)
        {
            _memory.Dispose();
        }
        
        _disposed = true;
    }
}