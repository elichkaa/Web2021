namespace SUS.MvcFramework
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using Http;

    public abstract class Controller
    {
        public HttpResponse View([CallerMemberName] string viewPath = null)
        {
            var className = this.GetType().Name.Split("Controller");
            var responseHtml = System.IO.File.ReadAllText("Views/" + className[0] + "/" + viewPath + ".html");
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }

        //files - img, ico, mp4 etc.
        public HttpResponse File(string filePath, string contentType)
        {
            var responseHtml = System.IO.File.ReadAllBytes(filePath);
            var response = new HttpResponse(contentType, responseHtml);
            return response;
        }
    }
}
