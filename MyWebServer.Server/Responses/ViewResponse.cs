using MyWebServer.Server.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.Responses
{
    public class ViewResponse : HttpResponse
    {
        private readonly string filePath;

        private const char PathSeparator = '/';
        public ViewResponse(string viewPath, string controllerName, object model) : base(HttpStatusCode.OK)
            => this.GetHtml(viewPath, controllerName, model);

        private void GetHtml(string viewName, string controllerName, object model)
        {
            if (!viewName.Contains(PathSeparator))
            {
                viewName = controllerName + PathSeparator + viewName;
            }

            var viewPath = Path.GetFullPath($"./Views/" + viewName.TrimStart(PathSeparator) + ".cshtml");

            if (!File.Exists(viewPath))
            {
                this.PrepareMissingViewError(viewPath);

                return;
            }

            var viewContent = File.ReadAllText(viewPath);

            if(model != null)
            {
                viewContent = this.PopulateModel(viewContent, model);
            }

            this.PrepareContent(viewContent, HttpContentType.Html);
        }

        private void PrepareMissingViewError(string viewPath)
        {
            this.StatusCode = HttpStatusCode.Found;

            var errorMessage = $"View '{viewPath}' was not found.";

            this.PrepareContent(errorMessage, HttpContentType.PlainText);
        }

        private string PopulateModel(string viewContent, object model)
        {
            var data = model.
                GetType()
                .GetProperties().
                Select(pr => new
                {
                    Name = pr.Name,
                    Value = pr.GetValue(model)
                });

            foreach (var entry in data)
            {
                const string openingBreckets = "{{";
                const string closingBreckets = "}}";

                viewContent = viewContent.Replace($"{openingBreckets}{entry.Name}{closingBreckets}", entry.Value.ToString());
            }

            return viewContent;
        }
    }
}
