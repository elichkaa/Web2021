using System;

namespace MyFirstMvcApp
{
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Controllers;
    using SUS.Http;
    using SUS.MvcFramework;

    class Program
    {
        static async Task Main(string[] args)
        {
            List<Route> routeTable = new List<Route>();
            var httpServer = new HttpServer(routeTable);
            routeTable.Add(new Route("/", new HomeController().Home));
            routeTable.Add(new Route("/login", new UsersController().Login));
            routeTable.Add(new Route("/register", new UsersController().Register));
            routeTable.Add(new Route("/cards/add", new CardsController().Add));
            routeTable.Add(new Route("/cards/all", new CardsController().All));
            routeTable.Add(new Route("/cards/collection", new CardsController().Collection));

            routeTable.Add(new Route("/css/bootstrap.min.css", new StaticFilesController().BootstrapCss));
            routeTable.Add(new Route("/css/custom.css", new StaticFilesController().CustomCss));
            routeTable.Add(new Route("/js/bootstrap.bundle.css", new StaticFilesController().BootstrapJs));
            routeTable.Add(new Route("/js/custom.js", new StaticFilesController().CustomJs));

            await Host.RunAsync(routeTable, 12345);
        }
    }
}
