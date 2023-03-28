using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPserver
{
    internal class Program
    {

        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 8888);
            listener.Start();
            Console.WriteLine("Server started.");

            try
            {
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("Client connected.");

                    HandleClient(client);

                    client.Close();
                    Console.WriteLine("Client disconnected.");
                }
            }
            finally
            {
                listener.Stop();
                Console.WriteLine("Server stopped.");
            }
        }

        static void HandleClient(TcpClient client)
        {
            try
            {
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead;

                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        Console.WriteLine("Received: {0}", message);

                        // Echo the message back to the client.
                        byte[] response = Encoding.ASCII.GetBytes(message);
                        stream.Write(response, 0, response.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
            }
        }
    }
}