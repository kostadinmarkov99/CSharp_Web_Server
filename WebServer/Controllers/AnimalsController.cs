using MyWebServer.Server;
using MyWebServer.Server.Http;
using MyWebServer.Server.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebServer.Server.Controllers;

namespace WebServer.Controllers
{
    public class AnimalsController : Controller
    {
        public AnimalsController(HttpRequest request) : base(request)
        {

        }

        public HttpResponse Cats()
        {
            const string nameKey = "Name";

            var query = this.Request.Query;

            var catName = query.ContainsKey(nameKey) ? query[nameKey] : "the cats";

            var result = $"<h1>Hello from the {catName}!</h1>";

            if (query.ContainsKey("Name"))
            {

            }
            return Html(result);
        }

        public HttpResponse Dogs()
         => Html("<h1>Hello From The Dogs</h1>");
    }
}
