using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class TCPServer
{
    static void Main(string[] args)
    {
        try
        {
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8080);
            server.Start();

            Console.WriteLine("Server started");

            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Client connected");

            NetworkStream stream = client.GetStream();

            while (true)
            {
                byte[] data = new byte[256];
                int bytes = stream.Read(data, 0, data.Length);
                string message = Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Client: {0}", message);

                Console.Write("You: ");
                message = Console.ReadLine();

                data = Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }

            stream.Close();
            client.Close();
            server.Stop();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: {0}", e);
        }

        Console.ReadKey();
    }
}

