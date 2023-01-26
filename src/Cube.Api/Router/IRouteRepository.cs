using Cube.Api.Network;
using Cube.Api.Network.Communication;

namespace Cube.Api.Router;

public interface IRouteRepository
{
    /// <summary>
    /// Check if an event has a route.
    /// </summary>
    /// <param name="???"></param>
    /// <returns></returns>
    public bool HasRoute(IMessageRequest messageRequest);
    
    /// <summary>
    /// Get the route for the given header.
    /// </summary>
    /// <param name="header"></param>
    /// <returns></returns>
    public IRoute GetRoute(short header);
}