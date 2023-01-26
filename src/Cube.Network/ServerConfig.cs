namespace Cube.Network;

public class ServerConfig
{
    public int Port { get; set; } = 3000;
    
    public int WorkerThreads { get; set; } = 1;
}