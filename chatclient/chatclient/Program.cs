using System;
using System.Net.Sockets;
using System.Text;

class TCPClient
{
    static void Main(string[] args)
    {
        try
        {
            TcpClient client = new TcpClient();
            client.Connect("127.0.0.1", 8080);

            Console.WriteLine("Connected to server");

            NetworkStream stream = client.GetStream();
            string message = "";

            while (true)
            {
                Console.Write("You: ");
                message = Console.ReadLine();

                byte[] data = Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);

                data = new byte[256];
                int bytes = stream.Read(data, 0, data.Length);
                message = Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Server: {0}", message);
            }

            stream.Close();
            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: {0}", e);
        }

        Console.ReadKey();
    }
}
