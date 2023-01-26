using Cube.Api.Network;
using Cube.Api.Network.Communication;

namespace Cube.Api.Router;

public interface IRouter
{
    public IMessageResponse DispatchAsync(IMessageRequest message);
}