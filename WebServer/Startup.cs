using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MyWebServer.HttpServer;
using MyWebServer.Server.Responses;

namespace WebServer
{
    class Program
    {
        public static async Task Main() => await new HttpServer(routingTable => routingTable
                        .MapGet("/", new TextResponse("Hello From Kostadin!")).
                         MapGet("/Cats", request =>
                         {
                             const string nameKey = "Name";

                             var query = request.Query;

                             var catName = query.ContainsKey(nameKey) ?  query[nameKey] : "the cats";

                             var result = $"<h1>Hello from the {catName}!</h1>";

                             if (query.ContainsKey("Name"))
                             {

                             }
                             return new HtmlResponse(result);
                         })
                         .MapGet("/Dogs", new HtmlResponse("<h1>Hello From The Dogs</h1>"))).Start();

    }
}
