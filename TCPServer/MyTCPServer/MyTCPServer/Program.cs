using System.Net;
using System.Net.Sockets; 
using MyTCPServer.Client;
using MyTCPServer.Core;

namespace MyTCPServer;

class Program
{
    static async Task Main(string[] args)
    {
        TcpListener listener = new(IPAddress.Any, 8081);
        listener.Start();

        Server server = new(listener);

        _ = Task.Run(async () =>
        {
            await Task.Delay(500);
    
            MyHttpClient httpClient = new();
            string response = await httpClient.SendAsync("COOKIE", "cookie");
            Console.WriteLine("Client received:");
            Console.WriteLine(response);
        });
        await server.StartAsync();

    }
}