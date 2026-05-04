using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MyTCPServer.Client;

public class MyHttpClient
{
    public async Task<string> SendAsync(string method, string path)
    {
        TcpClient client = new();
        await client.ConnectAsync("localhost", 8081);

        NetworkStream stream = client.GetStream();

        string request = $"{method} /{path} HTTP/1.1\r\n" +
                         "Host: localhost:8081\r\n" +
                         "\r\n";
        
        // write the response
        byte[] requestBytes = Encoding.UTF8.GetBytes(request);
        await stream.WriteAsync(requestBytes);
        
        // read the response
        byte[] responseBytes = new byte[1024];
        int bytesRead = await stream.ReadAsync(responseBytes);
        string response = Encoding.UTF8.GetString(responseBytes, 0, bytesRead);
        
        return response;
    }
}