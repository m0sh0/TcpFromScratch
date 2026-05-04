namespace MyTCPServer.Stores;

public interface ITodoStore
{
    List<Todo> GetAll();
    Todo? GetById(string id);
    void Add(Todo todo);
    
    bool Delete(string id);
}