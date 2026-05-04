using MyTCPServer.Factory;
using MyTCPServer.Http;
using MyTCPServer.Services;

namespace MyTCPServer.EndPoints;

public class TodoEndPoints(ITodoService todoService)
{
    public Response? Handle(HttpRequest request)
    { 
        if (request is { Path: "/todos", Method: "GET" })
        {
            return todoService.GetAll();
        }

        if (request.Method == "GET" && request.Path.StartsWith("/todos/"))
        {
            string? id = GetId(request.Path);
            return id != null
                ? todoService.GetById(id)
                : ResponseFactory.BadRequest("Body is null");
        }

        if (request is { Path: "/todos", Method: "POST" })
        {
            return todoService.AddTodo(request.Body);
        }

        if (request.Method == "DELETE" && request.Path.StartsWith("/todos/"))
        {
            string? id = GetId(request.Path);
            return id != null
                ? todoService.DeleteTodo(id)
                : ResponseFactory.BadRequest("Id is null");
        }
        return null;
    }

    private static string? GetId(string path)
    {
        string[] segments = path.Split('/');
        if (segments.Length < 3)
        {
            return null;
        }
        return segments[2];
    }
}