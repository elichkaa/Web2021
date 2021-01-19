using System;

namespace Http
{
    using System.Net;
    using System.Net.Http;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            const string NewLine = "\r\n";
            TcpListener tcpListener = new TcpListener(
                IPAddress.Loopback, 8080);
            tcpListener.Start();
            while (true)
            {
                //todo: add more functionalities
                var client = tcpListener.AcceptTcpClient();
                using (var stream = client.GetStream())
                {
                    byte[] buffer = new byte[10000];
                    var lenght = stream.Read(buffer, 0, buffer.Length);

                    string requestString =
                        Encoding.UTF8.GetString(buffer, 0, lenght);
                    Console.WriteLine(requestString);

                    string html = $"<h1>Hello from the coolest server! The time is {DateTime.UtcNow}</h1>" +
                                  $"<form action=/tweet method=post><input name=username /></br><input name=password />" +
                                  $"<input type=submit /></form>";

                    string response = "HTTP/1.1 200 OK" + NewLine +
                                      "Server: the coolest server 2021" + NewLine +
                                      "Content-Type: text/html; charset=utf-8" + NewLine +
                                      "Content-Length: " + html.Length + NewLine +
                                      NewLine +
                                      html + NewLine;

                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseBytes);

                    Console.WriteLine(new string('=', 70));
                }
            }
        }
        private static async Task ReadData()
        {
            Console.OutputEncoding = Encoding.UTF8;
            string url = "https://softuni.bg/";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            Console.WriteLine(response.StatusCode);
        }
    }
}
