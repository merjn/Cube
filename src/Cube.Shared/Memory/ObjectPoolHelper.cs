namespace Cube.Shared.Memory;

using System;
using System.Buffers;
using System.Collections.Concurrent;

public class ObjectPoolHelper<T> : IDisposable where T : notnull, new()
{
    private readonly MemoryPool<T> _memoryPool;
    private readonly ConcurrentDictionary<T, IMemoryOwner<T>> _memoryHandles;

    public ObjectPoolHelper()
    {
        _memoryPool = MemoryPool<T>.Shared;
        _memoryHandles = new ConcurrentDictionary<T, MemoryHandle>();
    }

    public T Rent()
    {
        var memoryOwner = _memoryPool.Rent();
        
        // Get the actual object from the memory
        var obj = memoryOwner.Memory.Span[0];
        _memoryHandles.TryAdd(obj, memoryOwner);

        return obj;
    }

    public void Return(T obj) {
        if (!_memoryHandles.TryGetValue(obj, out var memoryOwner)) {
            throw new Exception("Object not found in pool");
        }
        
        // Remove the object from the pool
        _memoryHandles.Remove(obj);
        
        // Return the memory to the pool
        memoryOwner.Dispose();
    }

    public void Dispose() {
        
    }
}