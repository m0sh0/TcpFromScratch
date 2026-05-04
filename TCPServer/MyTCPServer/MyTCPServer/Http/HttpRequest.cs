namespace MyTCPServer.Http;

public class HttpRequest(string method, string path)
{
    public string Method { get; } = method;
    public string Path { get; } = path;
    public string Body { get; init; } = string.Empty;
}