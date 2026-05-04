using System.Net.Sockets;
using System.Text;
using MyTCPServer.Http;
using MyTCPServer.Services;
using MyTCPServer.Stores;

namespace MyTCPServer.Core;

public class ClientHandler(TcpClient client)
{
   private TcpClient Client { get; } = client;
   private NetworkStream Stream => Client.GetStream();
   private static TodoService Service => new(TodoStore.Instance);
   
   private string ReadStream()
   {
      NetworkStream stream = Stream;
      int totalRead = 0;
      byte[] readBuffer = new byte[1024];
        
      while (stream.DataAvailable || totalRead == 0)
      {
         int n = stream.Read(readBuffer, totalRead, readBuffer.Length - totalRead);
         if (n == 0)
         {
            break;
         }
        
         totalRead += n;
      }
        
      string receivedMessage = Encoding.UTF8.GetString(readBuffer, 0, totalRead);
      return receivedMessage;
   }

   private async Task WriteStream(Response responseObj)
   {
      string parsedResponse = ResponseCreator.ParseResponse(responseObj);
      byte[] messageBytes = Encoding.UTF8.GetBytes(parsedResponse);
      await Stream.WriteAsync(messageBytes);
   }

   private static HttpRequest ParseRequest(string request)
   {
      string[] parts = request.Split("\r\n\r\n", 2);
      string[] lines = parts[0].Split("\r\n");
      string[] requestLine = lines[0].Split(" ");
      
      string method = requestLine[0];
      string path = requestLine[1];

      return new HttpRequest(method, path)
      {
         Body = parts.Length > 1 ? parts[1] : string.Empty
      };
   }
   
   public void Handle()
   {
      string raw = ReadStream();
      
      HttpRequest req = ParseRequest(raw);
      Router router = new(req, Service);
      
      Response response = router.Route();

      WriteStream(response).Wait();

      Client.Close();
   }
}