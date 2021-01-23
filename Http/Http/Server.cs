namespace Http
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    public class Server
    {
        private const int port = 8080;
        private IPAddress localHostIpAddress = IPAddress.Loopback;
        private TcpListener listener;
        private bool isRunning;

        public Server()
        {
            this.listener = new TcpListener(localHostIpAddress, port);
            isRunning = false;
        }

        //starts the listening process
        //the listening process should be asynchronous to ensure each concurrent client functionality
        public void Run()
        {
            this.listener.Start();
            this.isRunning = true;

            Console.WriteLine($"Server started at http://{localHostIpAddress}:{port}");

            while (this.isRunning)
            {
                Console.WriteLine("Waiting for client...");
                var client = this.listener.AcceptSocketAsync().GetAwaiter().GetResult();
                Task.Run(() =>
                {
                    this.Listen(client);
                });
            }
        }

        public async Task Listen(Socket client)
        {
            //var connectionHandler = new ConnectionHandler
        }
    }
}
