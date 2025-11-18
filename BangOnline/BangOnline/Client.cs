using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
namespace BangOnline
{
    public class Client
    {
        private readonly Socket ClientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private const int PORT = 100;

        private InputValidator inputValidator = new InputValidator();

        public Client()
        {
        }

        public int ConnectToServer()
        {
            int attempts = 0;
            int id = 0;
            while (!ClientSocket.Connected)
            {
                try
                {
                    attempts++;
                    Console.WriteLine("Connection attempt " + attempts);
                    // Change IPAddress.Loopback to a remote IP to connect to a remote host.
                    ClientSocket.Connect(IPAddress.Loopback, PORT);
                    Console.WriteLine("Choose your nickname:");
                    string nickname = inputValidator.ChooseNick();
                    SendString("a " + nickname);
                    id = Int32.Parse(ReceiveResponse());

                }
                catch (SocketException)
                {
                    Console.Clear();
                }
            }

            Console.Clear();
            Console.WriteLine("Connected");
            return id;
        }

        public void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        public void Exit()
        {
            SendString("exit"); // Tell the server we are exiting
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
            Environment.Exit(0);
        }

        public string ReceiveResponse()
        {
            var buffer = new byte[4096];
            int received = ClientSocket.Receive(buffer, SocketFlags.None);
            if (received == 0) return "";
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            string text = Encoding.ASCII.GetString(data);
            //Console.WriteLine(text);
            return text;

        }
    }
}
