using System.Net.Sockets;
using System.Text;

namespace TCPclient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TcpClient client = new TcpClient("localhost", 8888);
                Console.WriteLine("Connected to server.");

                NetworkStream stream = client.GetStream();

                // Send a message to the server.
                string message = "Hello, server!";
                byte[] data = Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Sent: {0}", message);

                // Read the server's response.
                data = new byte[1024];
                int bytesRead = stream.Read(data, 0, data.Length);
                message = Encoding.ASCII.GetString(data, 0, bytesRead);
                Console.WriteLine("Received: {0}", message);

                // Close the connection.
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}