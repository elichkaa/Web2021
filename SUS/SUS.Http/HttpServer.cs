
namespace SUS.Http
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class HttpServer : IHttpServer
    {
        private IDictionary<string, Func<HttpRequest, HttpResponse>> routingTable =
            new Dictionary<string, Func<HttpRequest, HttpResponse>>();

        private const string NewLine = "\r\n";
        private const int BufferSize = 4096;

        public void AddRoute(string path, Func<HttpRequest, HttpResponse> action)
        {
            if (routingTable.ContainsKey(path))
            {
                routingTable[path] = action;
            }
            else
            {
                routingTable.Add(path, action);
            }
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
                byte[] buffer = new byte[BufferSize];
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
                Console.WriteLine(request);


                //write request + html
                var resposeHtml = "<h1>Welcome!</h1>";
                var htmlBytes = Encoding.UTF8.GetBytes(resposeHtml);
                var responseHttp = "HTTP/1.1 200 OK" + NewLine
                                                     + "Server: SUS Server 1.0" + NewLine
                                                     + "Content-Type: text/html" + NewLine
                                                     + "Content-Length: " + htmlBytes.Length + NewLine
                                                     + NewLine;
                var httpBytes = Encoding.UTF8.GetBytes(responseHttp);
                await stream.WriteAsync(httpBytes, 0, httpBytes.Length);
                await stream.WriteAsync(htmlBytes, 0, htmlBytes.Length);
            }

            //close tcpClient
            tcpClient.Close();
        }
    }
}
