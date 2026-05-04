namespace MyTCPServer;

public record Todo(string Id, string Title, bool IsCompleted)
{
    public string Id { get; } = Id;
    public string Title { get; } = Title;
    public bool IsCompleted { get; } = IsCompleted;
}