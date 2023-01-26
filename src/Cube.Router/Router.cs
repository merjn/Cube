using Cube.Api.Network.Communication;
using Cube.Api.Router;
using Cube.Router.Exceptions;

namespace Cube.Router;

public class Router : IRouter
{
    private readonly IRouteRepository _routeRepository;
    
    public Router(IRouteRepository routeRepository)
    {
        _routeRepository = routeRepository;
    }
    
    public IMessageResponse DispatchAsync(IMessageRequest message)
    {
        if (!_routeRepository.HasRoute(message))
        {
            throw new RouteNotFoundException(message.GetHeader());
        }
        
        var route = _routeRepository.GetRoute(message.GetHeader());
        var middlewares = route.GetMiddlewares();
        
        middlewares.Add(new RouteExecutionMiddleware(route));
        
        var middlewareExecutor = new MiddlewareDispatcher(middlewares);
        
        return middlewareExecutor.Handle(message);
    }
}