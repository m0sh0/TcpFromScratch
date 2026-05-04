using MyTCPServer.EndPoints;
using MyTCPServer.Factory;
using MyTCPServer.Http;
using MyTCPServer.Services;

namespace MyTCPServer.Core;

public class Router(HttpRequest req, ITodoService todoService)
{
    private HttpRequest Request => req;
    private readonly TodoEndPoints _todoEndpoints = new(todoService);
    private readonly CookieEndPoints _cookieEndpoints = new();
    
    public Response Route()
    {
        return _todoEndpoints.Handle(Request)
               ?? _cookieEndpoints.Handle(Request)
               ?? ResponseFactory.NotFound();

    }
}