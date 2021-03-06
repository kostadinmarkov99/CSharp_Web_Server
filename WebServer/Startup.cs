using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MyWebServer.HttpServer;
using MyWebServer.Server.Results    ;
using WebServer.Controllers;
using MyWebServer.Server.Controllers;

namespace WebServer
{
    class Program
    {
        public static async Task Main() =>
                    await new HttpServer(routingTable => routingTable
                        .MapGet<HomeController>("/", c => c.Index())
                        .MapGet<HomeController>("/ToCats", c => c.LocalRedirect())
                        .MapGet<HomeController>("/Softuni", c => c.ToSoftUni())
                        .MapGet<HomeController>("/Error", c => c.Error())
                        .MapGet<AnimalsController>("/Cats", c => c.Cats())
                        .MapGet<AnimalsController>("/Dogs", c => c.Dogs())
                        .MapGet<AnimalsController>("/Bunnies", c => c.Bunnies())
                        .MapGet<AnimalsController>("/Turtles", c => c.Turtles())
                        .MapGet<AccountController>("/Cookies", c => c.ActionWithCookie())
                        .MapGet<AccountController>("/Session", c => c.ActionWithSession())
                        .MapGet<CatsController>("/Cats/Create", c => c.Create())
                        .MapPost<CatsController>("/Cats/Save", c => c.Save())
                        ).Start();

    }
}
