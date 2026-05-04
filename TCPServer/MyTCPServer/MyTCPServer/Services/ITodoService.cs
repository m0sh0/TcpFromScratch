namespace MyTCPServer.Services;

public interface ITodoService
{
    Response GetAll();
    
    Response GetById(string id);
    Response AddTodo(string body);
    
    Response DeleteTodo(string id);
}