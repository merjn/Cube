namespace Cube.Network;

public class ServerConfig
{
    public int Port { get; set; } = 3000;

    public int WorkerThreads { get; set; } = Environment.ProcessorCount * 2;

    public int BacklogQueue { get; set; } = 128; // 128 is the default value in DotNetty
}