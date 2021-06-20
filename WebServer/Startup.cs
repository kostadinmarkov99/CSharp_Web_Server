using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MyWebServer.HttpServer;
using MyWebServer.Server.Responses;
using WebServer.Controllers;
using MyWebServer.Server.Controllers;

namespace WebServer
{
    class Program
    {
        public static async Task Main() => await new HttpServer(routingTable => routingTable
                        .MapGet<HomeController>("/", c => c.Index())
                        .MapGet<AnimalsController>("/Cats", c => c.Cats())
                        .MapGet<AnimalsController>("/Dogs", c => c.Dogs())).Start();

    }
}
