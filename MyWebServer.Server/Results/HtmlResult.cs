using MyWebServer.Server.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.Results
{
    public class HtmlResult : ContentResult
    {
        public HtmlResult(HttpResponse response, string html) : base(response, html, HttpContentType.Html)
        {

        }
    }
}
