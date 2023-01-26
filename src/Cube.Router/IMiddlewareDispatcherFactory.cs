using Cube.Api.Network.Middleware;

namespace Cube.Router;

public interface IMiddlewareDispatcherFactory
{
    public MiddlewareDispatcher Create(IReadOnlyList<IMiddleware> middlewares);
}