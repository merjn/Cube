using Cube.Api.Network.Communication;
using Cube.Api.Router;

namespace Cube.Router.Repository;

public class RouteRepository : IRouteRepository
{
    public bool HasRoute(IMessageRequest messageRequest)
    {
        return false; // WIP
    }

    public IRoute GetRoute(short header)
    {
        throw new NotImplementedException();
    }
}