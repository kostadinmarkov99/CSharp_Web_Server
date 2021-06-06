using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MyWebServer.HttpServer;

namespace WebServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var server = new  HttpServer("127.0.0.1", 9090);

            await server.Start();
        }
    }
}
