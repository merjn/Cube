namespace Cube.Api.Network;

public interface IServerRunner
{
    /**
     * Starts the server.
     *
     * @return Task
     */
    public Task StartAsync();
}