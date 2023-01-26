namespace Cube.Api.Network.Communication;

public interface IMessageResponse
{
    /// <summary>
    /// Get all server messages.
    /// </summary>
    /// <returns></returns>
    public IReadOnlyList<IServerMessage> GetMessages();
}