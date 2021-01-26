namespace SUS.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class HttpServer : IHttpServer
    {
        private List<Route> routingTable;

        public HttpServer(List<Route> routeTable)
        {
            this.routingTable = routeTable;
        }

        public async Task StartAsync(int port)
        {
            var tcpListener = new TcpListener(IPAddress.Any, port);
            tcpListener.Start();
            while (true)
            {
                var tcpClient = await tcpListener.AcceptTcpClientAsync();
                ProcessTcpClientAsync(tcpClient);
            }
        }

        private async Task ProcessTcpClientAsync(TcpClient tcpClient)
        {
            using (var stream = tcpClient.GetStream())
            {
                //read request
                int position = 0;
                byte[] buffer = new byte[HttpConstants.BufferSize];
                var data = new List<byte>();
                while (true)
                {
                    //get next buffer start position
                    int count = await stream.ReadAsync(buffer, 0, buffer.Length);
                    position += count;
                    //if count is lower than buffer.Length, we add the last bytes and break while
                    if (count < buffer.Length)
                    {
                        var bytesWithoutZeroes = new byte[count];
                        Array.Copy(buffer, bytesWithoutZeroes, count);
                        data.AddRange(bytesWithoutZeroes);
                        break;
                    }

                    data.AddRange(buffer);
                }

                var request = Encoding.UTF8.GetString(data.ToArray());
                var httpRequest = new HttpRequest(request);
                //Console.WriteLine(httpRequest.ToString());
                Console.WriteLine($"{httpRequest.Method} {httpRequest.Path} => {httpRequest.Headers.Count} headers");

                //write request + html
                HttpResponse response;
                var route = routingTable.FirstOrDefault(x => x.Path == httpRequest.Path);
                if (route != null)
                {
                    response = route.Action(httpRequest);
                }
                else
                {
                    //404 Not Found
                    response = new HttpResponse("text/html", new byte[0], HttpStatusCode.NotFound);
                }

                response.Headers.Add(new Header("Server", "SUS Server 1.0"));
                response.Cookies.Add(new ResponseCookie("sid", Guid.NewGuid().ToString()) { HttpOnly = true });
                var responseHeaderBytes = Encoding.UTF8.GetBytes(response.ToString());
                await stream.WriteAsync(responseHeaderBytes, 0, responseHeaderBytes.Length);
                await stream.WriteAsync(response.Body, 0, response.Body.Length);
            }

            //close tcpClient
            tcpClient.Close();
        }
    }
}
