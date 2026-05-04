namespace MyTCPServer.Factory;

public static class ResponseFactory
{
    public static Response Ok(string body)
        => new(200, "text/plain", body);
    
    public static Response Created(string body)
        => new(201, "text/plain", body);
    
    public static Response NotFound()
        => new(404, "text/plain", "Not Found");
    
    public static Response BadRequest(string message)
        => new(400, "application/json", $"{{\"error\": \"{message}\"}}");
}