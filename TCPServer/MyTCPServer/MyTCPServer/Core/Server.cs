using System.Net.Sockets;

namespace MyTCPServer.Core;

public class Server(TcpListener listener)
{
    private TcpListener Listener { get; } = listener;

    public async Task StartAsync()
    {
        while (true)
        {
            TcpClient client = await Listener.AcceptTcpClientAsync();
            _ = Task.Run(() =>
            {
                try
                {
                    HandleClient(client);
                }
                catch (IOException)
                {
                }
                finally
                {
                    client.Close();
                }
            });
        }
    }
    
    private static void HandleClient(TcpClient client)
    {
        ClientHandler handler = new(client);
        handler.Handle();
    }
}