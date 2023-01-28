using Cube.Api.Network;
using Cube.Api.Network.Communication;
using Cube.Api.Router.Exceptions;
using LanguageExt;

namespace Cube.Api.Router;

public interface IRouter
{
    public Either<RouteNotFoundException, IMessageResponse> Dispatch(IMessageRequest message);
}