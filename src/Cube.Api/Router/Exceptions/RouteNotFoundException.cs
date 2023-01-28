namespace Cube.Api.Router.Exceptions;

public class RouteNotFoundException : Exception
{
    public RouteNotFoundException(short header) : base($"Route with header {header} not found.")
    {
    }
}