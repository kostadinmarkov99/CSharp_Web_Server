using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var address = IPAddress.Parse("127.0.0.1");
            var port = 9090;

            var serverListener = new TcpListener(address, port);

            serverListener.Start();

            Console.WriteLine($"Server started in port {port}...");
            Console.WriteLine("Listening for requests...");

            while (true)
            {
                var connection = await serverListener.AcceptTcpClientAsync();

                var networkStream = connection.GetStream();

                var bufferLength = 1024;
                var buffer = new byte[bufferLength];

                var totalBytes = 0;

                var requestBuilder = new StringBuilder();

                while (networkStream.DataAvailable)
                {
                    var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferLength);

                    totalBytes += bytesRead;

                    if(totalBytes > 10 * 1024)
                    {
                        connection.Close();
                    }

                    requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                }


                Console.WriteLine(requestBuilder);


                var content = "<h1>Hello From My Server!</h1>";
                var contentLength = Encoding.UTF8.GetByteCount(content);

                var responce = $@"
HTTP/1.1 200 OK
Server: My Server
Date: {DateTime.Now.ToString("r")}
Content-Length: {contentLength}
Content-Type: text/html; charset=UTF-8

{content}";

                var responceBytes = Encoding.UTF8.GetBytes(responce);

                await networkStream.WriteAsync(responceBytes, 0, responceBytes.Length);

                connection.Close();
            }
        }
    }
}
