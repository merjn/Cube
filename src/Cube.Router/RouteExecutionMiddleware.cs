using Cube.Api.Network.Communication;
using Cube.Api.Network.Middleware;
using Cube.Api.Router;

namespace Cube.Router;

public class RouteExecutionMiddleware : IMiddleware
{
    private readonly IRoute _route;
    
    public RouteExecutionMiddleware(IRoute route)
    {
        _route = route;
    }
    
    public IMessageResponse Process(IMessageRequest message, IMessageHandler next)
    {
        return _route.GetHandler().Handle(message);
    }
}