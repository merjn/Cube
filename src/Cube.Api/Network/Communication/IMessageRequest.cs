namespace Cube.Api.Network.Communication;

public interface IMessageRequest
{
    /**
     * The header of the event.
     */
    public short GetHeader();

    /**
     * Reads a short from the buffer.
     *
     * @return The short.
     */
    public short ReadShort();
}