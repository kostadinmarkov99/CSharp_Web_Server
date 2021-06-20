using MyWebServer.Server.Common;
using MyWebServer.Server.Http;
using MyWebServer.Server.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.Routing
{
    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<HttpMethod, Dictionary<string, Func<HttpRequest, HttpResponse>>> routes;

        public RoutingTable()
        {
            this.routes = new()
            {
                [HttpMethod.Get] = new(),
                [HttpMethod.Post] = new(),
                [HttpMethod.Put] = new(),
                [HttpMethod.Delete] = new(),
            };
        }
        public IRoutingTable Map(HttpMethod method, string path, HttpResponse response)
        {
            Guard.AgainsNull(path, nameof(response));

            return this.Map(method, path, request => response);
        }

        public IRoutingTable Map(HttpMethod method, string path, Func<HttpRequest, HttpResponse> responseFunction)
        {
            Guard.AgainsNull(path, nameof(path));
            Guard.AgainsNull(responseFunction, nameof(responseFunction));

            this.routes[method][path] = responseFunction;

            return this;
        }

        public IRoutingTable MapGet(string path, HttpResponse response) =>
            MapGet(path, request => response);

        public IRoutingTable MapGet(string path, Func<HttpRequest, HttpResponse> responseFunction)
            => Map(HttpMethod.Get, path, responseFunction);

        public IRoutingTable MapPost(string path, HttpResponse response) =>
            MapPost(path, request => response);

        public IRoutingTable MapPost(string path, Func<HttpRequest, HttpResponse> responseFunction)
            => Map(HttpMethod.Post, path, responseFunction);

        public HttpResponse ExecuteRequest(HttpRequest request)
        {
            var requestMethod = request.Method;
            var requestPath = request.Path;

            var ss = this.routes;
            var ssa = this.routes[requestMethod];

            if (!this.routes.ContainsKey(requestMethod) 
                || !this.routes[requestMethod].ContainsKey(requestPath))
            {
                return new NotFoundResponse();
            }

            var responseFunction = this.routes[requestMethod][requestPath];

            return responseFunction(request);
        }
    }
}
