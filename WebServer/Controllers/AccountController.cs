using MyWebServer.Server.Controllers;
using MyWebServer.Server.Http;
using MyWebServer.Server.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(HttpRequest request) : base(request)
        {
        }

        public HttpResponse ActionWithCookie()
        {
            const string cookieName = "My-Cookie";

            if (this.Request.Cookies.ContainsKey(cookieName))
            {
                var cookie = this.Request.Cookies[cookieName];

                return Text($"Cookies already exist - {cookie}");
            }

            this.Response.AddCookie(cookieName, "My-Value");
            this.Response.AddCookie("My-Second-Cookie", "My-Second-Value");

            return Text("Cookies set!");
        }
    }
}
