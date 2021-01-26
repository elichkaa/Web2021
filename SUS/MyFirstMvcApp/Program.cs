using System;

namespace MyFirstMvcApp
{
    using System.Text;
    using System.Threading.Tasks;
    using Controllers;
    using SUS.Http;

    class Program
    {
        static async Task Main(string[] args)
        {
            var httpServer = new HttpServer();
            httpServer.AddRoute("/", new HomeController().Home);
            httpServer.AddRoute("/about", new HomeController().About);
            httpServer.AddRoute("/login", new UsersController().Login);
            httpServer.AddRoute("/register", new UsersController().Register);
            await httpServer.StartAsync(12345);
        }
    }
}
