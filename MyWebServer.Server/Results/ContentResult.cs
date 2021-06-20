using MyWebServer.Server.Common;
using MyWebServer.Server.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.Results
{
    public class ContentResult : ActionResult
    {
        public ContentResult(HttpResponse response, string content, string contentType) : base(response)
        {
            this.PrepareContent(content, contentType);
        }
    }
}
