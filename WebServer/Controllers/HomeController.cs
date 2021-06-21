using MyWebServer.Server;
using MyWebServer.Server.Http;
using MyWebServer.Server.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebServer.Server.Controllers;

namespace WebServer.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(HttpRequest request) : base(request)
        {
        }
        public HttpResponse Index()
            => Text("Hello From Kostadin!");

        public HttpResponse LocalRedirect() => Redirect("/Cats");

        public HttpResponse ToSoftUni() => Redirect("https://softuni.bg");

        public HttpResponse Error() => throw new InvalidOperationException("Invalid action!");
    }
}
