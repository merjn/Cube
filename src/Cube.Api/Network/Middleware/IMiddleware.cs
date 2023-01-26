using Cube.Api.Network.Communication;

namespace Cube.Api.Network.Middleware;

public interface IMiddleware
{
    /// <summary>
    /// Process the middleware.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public IMessageResponse Process(IMessageRequest message, IMessageHandler next);
}