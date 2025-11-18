using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;

namespace BangOnline
{
    public class Server
    {

        private readonly Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private readonly List<Socket> clientSockets = new List<Socket>();
        private const int BUFFER_SIZE = 4096;
        private const int PORT = 100;
        private readonly byte[] buffer = new byte[BUFFER_SIZE];
        private GameState gs = new GameState();
        private GameMaterial material = new GameMaterial();

        private int numberOfPlayers;

        private int readyPlayers = 0;

        public Server()
        {
            
        }

        public void SetupServer()
        {
            Console.Write("Choose number of players (4-7): ");
            numberOfPlayers = Int32.Parse(Console.ReadLine());
            //Console.WriteLine("Setting up server...");
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallback, null);
            //Console.WriteLine("Server setup complete");

        }


        public void CloseAllSockets()
        {
            foreach (Socket socket in clientSockets)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            serverSocket.Close();
        }

        private void DistributeCards()
        {
            material.GetShuffledDeck(gs);
            material.DistributeCards(gs);

            // send json to all players
            SendGameStateJsonToAll();



        }

        private void AcceptCallback(IAsyncResult AR)
        {
            Socket socket;

            try
            {
                socket = serverSocket.EndAccept(AR);
            }
            catch (ObjectDisposedException) 
            {
                return;
            }

            clientSockets.Add(socket);
            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);
            //Console.WriteLine("Client connected, waiting for request...");
            serverSocket.BeginAccept(AcceptCallback, null);
        }

        private void SaveGameStateJson(string jsonString, string parameter)
        {

            switch (parameter)
            {
                case "all":
                    gs = JsonSerializer.Deserialize<GameState>(jsonString);
                    break;
                case "character":
                    PlayerInfo ModifiedPlayer = JsonSerializer.Deserialize<PlayerInfo>(jsonString);
                    gs.Players[ModifiedPlayer.ID].ChosenCharacter = ModifiedPlayer.ChosenCharacter;
                    gs.Players[ModifiedPlayer.ID].Lives = ModifiedPlayer.Lives + ((gs.Players[ModifiedPlayer.ID].Role == "Sheriff") ? 1 : 0);
                    break;

                default:
                    break;
            }
        }

        private void DistributeRolesAndCharacters()
        {
            material.GetRoles(gs);
            material.GetCharacters(gs);
            // send json to all players
            SendGameStateJsonToAll();

        }

        private string GetGameStateJson()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(gs, options);
        }

        private void SendGameStateJsonToAll()
        {
            byte[] data = Encoding.ASCII.GetBytes(GetGameStateJson());
            foreach (Socket client in clientSockets)
            {
                client.Send(data);
            }
        }

        private void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            int received;

            try
            {
                received = current.EndReceive(AR);
            }
            catch (SocketException)
            {
                //Console.WriteLine("Client forcefully disconnected");
                current.Close();
                clientSockets.Remove(current);
                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            //Console.WriteLine("Received Text: " + text);






            if (text.Length > 1 && text.ToLower().Substring(0, 1) == "a")
            {
                string nick = text.Substring(2);
                int id = (clientSockets.Count() - 1);
                gs.Players.Add(id, new PlayerInfo());
                gs.Players[id].ID = id;
                gs.Players[id].Name = nick;
                gs.Players[id].IsAlive = true;
                byte[] data = Encoding.ASCII.GetBytes(id.ToString());
                current.Send(data);


                readyPlayers++;
                if (readyPlayers == numberOfPlayers)
                {
                    readyPlayers = 0;
                    DistributeRolesAndCharacters();

                }

            }
            else if (text.Length > 1 && text.ToLower().Substring(0, 1) == "b")
            {
                SaveGameStateJson(text.Substring(2), "character");


                readyPlayers++;
                if (readyPlayers == numberOfPlayers)
                {
                   DistributeCards();

                }

            }
            else if (text.Length > 4 && text.ToLower().Substring(0, 4) == "move")
            {
                SaveGameStateJson(text.Substring(5), "all");
                SendGameStateJsonToAll();
            }
            else if (text.ToLower() == "exit")
            {
                current.Shutdown(SocketShutdown.Both);
                current.Close();
                clientSockets.Remove(current);
                //Console.WriteLine("Client disconnected");
                return;
            }
            else
            {
                //Console.WriteLine("Text is an invalid request");
                byte[] data = Encoding.ASCII.GetBytes("Invalid request");
                current.Send(data);
                //Console.WriteLine("Warning Sent");
            }

            current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
        }
    }
}
