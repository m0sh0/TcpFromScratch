using System.Text.Json;
using MyTCPServer.Factory;
using MyTCPServer.Models;
using MyTCPServer.Stores;

namespace MyTCPServer.Services;

public class TodoService(ITodoStore store) : ITodoService
{ 
    private ITodoStore Store { get; } = store;
    
    public Response GetAll()
    {
        List<Todo> todos = Store.GetAll();
        string json = JsonSerializer.Serialize(todos);
        return ResponseFactory.Ok(json);
    }

    public Response GetById(string id)
    {
        Todo? todo = Store.GetById(id);

        return todo != null
            ? ResponseFactory.Ok(JsonSerializer.Serialize(todo))
            : ResponseFactory.NotFound();
    }

    public Response AddTodo(string body)
    {
        CreateTodoRequest? request = JsonSerializer.Deserialize<CreateTodoRequest>(body);

        if (request == null || string.IsNullOrWhiteSpace(request.Title))
        {
            return ResponseFactory.BadRequest("Title required");
        }
        
        Todo todo = new(Guid.NewGuid().ToString(), request.Title, request.IsCompleted);
        Store.Add(todo);

        string json = JsonSerializer.Serialize(todo);
        return ResponseFactory.Created(json);
    }

    public Response DeleteTodo(string id)
    {
        return Store.Delete(id) 
            ? ResponseFactory.Ok("Removed") 
            : ResponseFactory.NotFound();
    }
}