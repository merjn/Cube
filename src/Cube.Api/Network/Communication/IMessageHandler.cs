namespace Cube.Api.Network.Communication;

public interface IMessageHandler
{
    /// <summary>
    /// Handle the message event.
    /// </summary>
    /// <param name="messageRequest"></param>
    /// <returns></returns>
    public IMessageResponse Handle(IMessageRequest messageRequest);
}