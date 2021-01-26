namespace SUS.MvcFramework
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Http;

    public static class Host
    {
        public static async Task RunAsync(List<Route> routeTable, int port)
        {
            IHttpServer server = new HttpServer(routeTable);
            await server.StartAsync(port);
        }
    }
}
