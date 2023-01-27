using DotNetty.Transport.Channels;

namespace Cube.Network.Channel;

public interface IChannelInitializer
{
    public void InitChannel(IChannel channel);
}