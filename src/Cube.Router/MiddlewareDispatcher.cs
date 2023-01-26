using Cube.Api.Network.Communication;
using Cube.Api.Network.Middleware;

namespace Cube.Router;

public class MiddlewareDispatcher : IMessageHandler
{
    /// <summary>
    /// Contains a list of middleware that will be executed.
    /// </summary>
    private readonly IReadOnlyList<IMiddleware> _middlewares;
    
    public MiddlewareDispatcher(IReadOnlyList<IMiddleware> middlewares)
    {
        _middlewares = middlewares;
    }


    /// <summary>
    /// Handle each middleware.
    /// </summary>
    /// <param name="messageRequest"></param>
    /// <returns></returns>
    public IMessageResponse Handle(IMessageRequest messageRequest)
    {
        var middleware = _middlewares.First();
        var next = new MiddlewareDispatcher(_middlewares.Skip(1).ToList());
        
        return middleware.Process(messageRequest, next);
    }
}