using Cube.Api.Network.Middleware;

namespace Cube.Router;

public class MiddlewareDispatcherFactory : IMiddlewareDispatcherFactory
{
    public MiddlewareDispatcher Create(IReadOnlyList<IMiddleware> middlewares)
    {
        return new MiddlewareDispatcher(middlewares);
    }
}