using System.Text;

namespace MyTCPServer;

public class Response(int statusCode, string contentType, string body)
{
    public int StatusCode { get; } = statusCode;
    public string ContentType { get; } = contentType;
    public string Body { get; } = body;
}