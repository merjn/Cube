using Cube.Api.Network.Communication;
using Cube.Api.Router;
using Cube.Api.Router.Exceptions;
using LanguageExt;

namespace Cube.Router;

public class Router : IRouter
{
    private readonly IRouteRepository _routeRepository;
    private readonly IMiddlewareDispatcherFactory _middlewareDispatcherFactory;
    
    public Router(IRouteRepository routeRepository, IMiddlewareDispatcherFactory middlewareDispatcherFactory)
    {
        _routeRepository = routeRepository;
        _middlewareDispatcherFactory = middlewareDispatcherFactory;
    }
    
    public Either<RouteNotFoundException, IMessageResponse> Dispatch(IMessageRequest message)
    {
        if (!_routeRepository.HasRoute(message))
        {
            return Either<RouteNotFoundException, IMessageResponse>.Left(new RouteNotFoundException(message.GetHeader()));
        }
        
        var route = _routeRepository.GetRoute(message.GetHeader());
        var middlewares = route.GetMiddlewares();
        
        middlewares.Add(new RouteExecutionMiddleware(route));

        var middlewareExecutor = _middlewareDispatcherFactory.Create(middlewares);
        
        return Either<RouteNotFoundException, IMessageResponse>.Right(middlewareExecutor.Handle(message));
    }
}