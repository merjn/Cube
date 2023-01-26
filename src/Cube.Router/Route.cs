using Cube.Api.Network.Communication;
using Cube.Api.Network.Middleware;
using Cube.Api.Router;

namespace Cube.Router;

public class Route : IRoute
{
    
    public List<IMiddleware> GetMiddlewares()
    {
        throw new NotImplementedException();
    }

    public IMessageHandler GetHandler()
    {
        throw new NotImplementedException();
    }

    IMessageRequest IRoute.GetMessageEvent()
    {
        throw new NotImplementedException();
    }
}