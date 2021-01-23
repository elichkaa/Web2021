using System;

namespace MyFirstMvcApp
{
    using System.Threading.Tasks;
    using SUS.Http;

    class Program
    {
        static async Task Main(string[] args)
        {
            var httpServer = new HttpServer();
            httpServer.AddRoute("/", Home);
            httpServer.AddRoute("/favicon.ico", FavIcon);
            httpServer.AddRoute("/about", About);
            httpServer.AddRoute("/users/login", Login);
            await httpServer.StartAsync(12345);
        }

        private static HttpResponse Home(HttpRequest request)
        {
            throw new NotImplementedException();
        }
        private static HttpResponse FavIcon(HttpRequest request)
        {
            throw new NotImplementedException();
        }

        private static HttpResponse About(HttpRequest request)
        {
            throw new NotImplementedException();
        }
        private static HttpResponse Login(HttpRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
