using System.Text;

namespace MyTCPServer;

public static class ResponseCreator
{
    public static string ParseResponse(Response responseObj)
    {
        string statusText = responseObj.StatusCode switch
        {
            200 => "OK",
            404 => "Not Found",
            400 => "Bad Request",
            _ => "Unknown"
        };
        string response = $"HTTP/1.1 {responseObj.StatusCode} {statusText}\r\n" +
                          $"Content-Type: {responseObj.ContentType}\r\n" +
                          $"Content-Length: {Encoding.UTF8.GetByteCount(responseObj.Body)}\r\n" +
                          "\r\n" +
                          responseObj.Body;
        
        return response;
    }
}