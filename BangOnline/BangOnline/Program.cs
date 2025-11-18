using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;

namespace BangOnline
{
    class Program
    {

        

        private static Game game = new Game();
        private static Server server = new Server();
        static void Main(string[] args)
        {
            Console.Write("Are you host or guest? (type host/guest) ");
            if (Console.ReadLine() == "host")
            {

                server.SetupServer();

            }

            game.StartGame();


        }
      
    }
}




/*
 - input validation + what to eneter
 
 
 
 
 
 */