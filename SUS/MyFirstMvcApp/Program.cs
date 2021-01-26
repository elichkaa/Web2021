using System;

namespace MyFirstMvcApp
{
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SUS.Http;

    class Program
    {
        static async Task Main(string[] args)
        {
            var httpServer = new HttpServer();
            httpServer.AddRoute("/", Home);
            httpServer.AddRoute("/about", About);
            httpServer.AddRoute("/login", Login);
            await httpServer.StartAsync(12345);
        }

        private static HttpResponse Home(HttpRequest request)
        {
            var responseHtml = "<h1>Welcome!</h1>"
                               + request.Headers.FirstOrDefault(x => x.Name == "User-Agent")?.Value;
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }
        private static HttpResponse About(HttpRequest request)
        {
            var responseHtml = "<h1>About</h1>";
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }
        private static HttpResponse Login(HttpRequest request)
        {
            var responseHtml = "<h1>Login</h1>";
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }
    }
}
