namespace MyTCPServer.Stores;

public class TodoStore : ITodoStore
{
    private static TodoStore? _instance;
    public static TodoStore Instance => _instance ??= new TodoStore();
    private readonly object _lock = new();

    private readonly List<Todo> _todos =
    [
        new Todo("1", "Wash clothes", false),
        new Todo("2", "Buy groceries", true),
        new Todo("3", "Do laundry", false),
        new Todo("4", "Clean room", true)
    ];

    public List<Todo> GetAll()
    {
        lock (_lock)
        {
            return _todos.ToList();
        }
    }

    public Todo? GetById(string id)
    {
        lock (_lock)
        {
            return _todos.FirstOrDefault(t => t.Id == id);
        }
    }

    public void Add(Todo todo)
    {
        lock (_lock)
        {
            _todos.Add(todo);
        }
    }

    public bool Delete(string id)
    {
        lock (_lock)
        {
            Todo? todo = _todos.FirstOrDefault(t => t.Id == id);

            if (todo == null)
            {
                return false;
            }

            _todos.Remove(todo);
            return true;
        } 
    }
}