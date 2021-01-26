
namespace SUS.Http
{
    using System;
    using System.Threading.Tasks;

    public interface IHttpServer
    {
        Task StartAsync(int port);
    }
}
