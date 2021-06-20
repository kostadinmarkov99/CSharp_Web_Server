using MyWebServer.Server.Http;
using MyWebServer.Server.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.HttpServer
{
    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener listener;

        private readonly RoutingTable routingTable;
        
        public HttpServer(string ipAddress, int port, Action<IRoutingTable> routingTableConfiguration)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            listener = new TcpListener(this.ipAddress, this.port);

            routingTableConfiguration(this.routingTable = new RoutingTable());
        }

        public HttpServer(int port, Action<IRoutingTable> routingTable) : this("127.0.0.1", port, routingTable)
        {

        }
        
        public HttpServer(Action<IRoutingTable> routingTable) : this(5000, routingTable)
        {

        }

        public async Task Start()
        {
            this.listener.Start();

            Console.WriteLine($"Server started in port {port}...");
            Console.WriteLine("Listening for requests...");

            while (true)
            {
                var connection = await this.listener.AcceptTcpClientAsync();

                var networkStream = connection.GetStream();

                var requestText = ReadRequest(networkStream, connection);

                string data = await requestText;
   
                var request = HttpRequest.Parse(data);

                var response = this.routingTable.ExecuteRequest(request);

                await WriteResponse(networkStream, response);

                connection.Close();
            }
        }

        private async Task<string> ReadRequest(NetworkStream networkStream, TcpClient connection)
        {
            var bufferLength = 1024;
            var buffer = new byte[bufferLength];

            var totalBytes = 0;

            var requestBuilder = new StringBuilder();

            do
            {
                var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferLength);

                totalBytes += bytesRead;

                if (totalBytes > 10 * 1024)
                {
                    connection.Close();
                }

                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }
            while (networkStream.DataAvailable);

            return requestBuilder.ToString();
        }

        private async Task WriteResponse(NetworkStream networkStream, HttpResponse response)
        {
            
            var responceBytes = Encoding.UTF8.GetBytes(response.ToString());

            await networkStream.WriteAsync(responceBytes, 0, responceBytes.Length);
        }
    }
}
