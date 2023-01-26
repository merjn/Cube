using Cube.Api.Network;
using Cube.Api.Network.Communication;
using Cube.Api.Network.Middleware;

namespace Cube.Api.Router;

public interface IRoute
{
    /// <summary>
    /// Get the message event for this route.
    /// </summary>
    /// <returns></returns>
    public IMessageRequest GetMessageEvent();
    
    /// <summary>
    /// Get the middlewares for this route.
    /// </summary>
    /// <returns></returns>
    public List<IMiddleware> GetMiddlewares();
    
    /// <summary>
    /// Get the handler for this route.
    /// </summary>
    /// <returns></returns>
    public IMessageHandler GetHandler();
}