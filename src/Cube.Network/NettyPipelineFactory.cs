using DotNetty.Transport.Channels;

namespace Cube.Network;

public class NettyPipelineFactory
{
    public IReadOnlyList<IChannelHandler> GetPipeline()
    {
        return new List<Object>();
    }
}