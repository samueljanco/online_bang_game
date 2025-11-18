using System;
using System.Text;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;
namespace BangOnline
{
    public class Game
    {
        private Client client = new Client();

        private GameState gs = new GameState();

        private GameMaterial material = new GameMaterial();

        private MoveValidator validator = new MoveValidator();

        private MoveExecutor moveExecutor = new MoveExecutor();

        private BlueCardEffectExecutor blueCardExecutor = new BlueCardEffectExecutor();

        private UIFormater formater = new UIFormater();

        private InputValidator inputValidator = new InputValidator();

        private Random rnd = new Random();

        private int ID = 0;

        
        


        public Game()
        {
        }

        public void StartGame() {
           
           

           //Initailization

            ID = client.ConnectToServer();
            
            formater.RenderRequiredMessurments();
            Console.WriteLine("Waiting for other players...");
            SaveGameStateJson();
            GetRole();
            ChooseCharacter();
            SaveGameStateJson();
            Console.Clear();



            GameLoop();
            client.Exit();


        }

        
        private int PlayersAliveCount() {
            int aliveCount = 0;
            foreach (PlayerInfo player in gs.Players.Values)
            {
                if (player.IsAlive) {
                    aliveCount++;
                }
            }
            return aliveCount;
        }



        private List<PlayerInfo> PlayersAlive()
        {
            List<PlayerInfo> alive = new List<PlayerInfo>();
            foreach (PlayerInfo player in gs.Players.Values)
            {
                if (player.IsAlive)
                {
                    alive.Add(player);
                }
            }
            return alive;
        }

        private int GetSheriffID()
        {
            
            foreach (PlayerInfo player in gs.Players.Values)
            {
                if (player.Role == "Sheriff")
                {
                    return player.ID;
                }
            }
            return -1;
            
        }

        private string GameHasWinner() {

            if (!gs.Players[GetSheriffID()].IsAlive && PlayersAliveCount() > 1) {
                return "Bandits";
            } else if (!gs.Players[GetSheriffID()].IsAlive && PlayersAlive()[0].Role == "Renegade") {
                return "Renegade";
            } else if (!PlayersAlive().Exists(p => p.Role == "Bandit" || p.Role == "Renegade")) {
                return "Sheriff";
            }
            return "None";
        }

        private void SaveGameStateJson()
        {
            string jsonString = client.ReceiveResponse();
            gs = JsonSerializer.Deserialize<GameState>(jsonString);
        }

        private string GetGameStateJson()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(gs, options);
        }

        

        private void GetRole()
        {
            Console.WriteLine($"Your role is {gs.Players[ID].Role}");
        }

        private void ChooseCharacter()
        {

            Console.WriteLine("Choose your character:");
            Console.WriteLine($"(1) {gs.Players[ID].Characters[0]}");
            Console.WriteLine($"(2) {gs.Players[ID].Characters[1]}");

            


            PlayerInfo pi = new PlayerInfo();
            pi.ID = ID;
            pi.ChosenCharacter = gs.Players[ID].Characters[inputValidator.ChooseCharacter()];
            pi.Lives = material.Characters[pi.ChosenCharacter].Lives;
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(pi, options);

            client.SendString("b " + jsonString);

        }

        private void RestoreDeck() {
            if (gs.DeckOfCards.UnusedCards.Count < 7) {
                foreach (string card in gs.DeckOfCards.UsedCards.OrderBy(item => rnd.Next()).ToList())
                {
                    gs.DeckOfCards.UnusedCards.Add(card);
                }
               
            }
        }

        private void GameLoop()
        {

            while (GameHasWinner() == "None")
            {
                Console.Clear();
                formater.RenderAll(gs, ID);

                if (ID == gs.CurrentPlayerID)
                {
                    RestoreDeck();
                    PlayMove();
                }

                SaveGameStateJson();



            }


            formater.RenderResults(gs, GameHasWinner());
            
        }

        private bool HasCard(GameState gs, int player, string card)
        {
            return gs.Players[player].CardsOnTable.Exists(s => s.Contains(card));
        }

        private void PlayMove()
        {
            GameEvent move = new GameEvent();
            if (gs.LastEvent.Event == "Bang" && gs.LastEvent.PlayedOn == ID)
            {
                move.SetLastEvent(gs.LastEvent.PlayedOn, "Missed", gs.LastEvent.PlayedBy);
                moveExecutor.ExecuteMove(gs, move);
            }
            else if (gs.LastEvent.Event == "Duel" && gs.LastEvent.PlayedOn == ID)
            {
                move.SetLastEvent(gs.LastEvent.PlayedOn, "DuelResponse", gs.LastEvent.PlayedBy);
                moveExecutor.ExecuteMove(gs, move);
            }
            else if (gs.LastEvent.Event == "GatlingResponse" && gs.LastEvent.PlayedOn == ID)
            {
                move.SetLastEvent(gs.LastEvent.PlayedBy, "GatlingResponse", (gs.CurrentPlayerID + 1) % gs.Players.Count);
                moveExecutor.ExecuteMove(gs, move);
            }
            else if (gs.LastEvent.Event == "IndiansResponse" && gs.LastEvent.PlayedOn == ID)
            {
                move.SetLastEvent(gs.LastEvent.PlayedBy, "IndiansResponse", (gs.CurrentPlayerID + 1) % gs.Players.Count);
                moveExecutor.ExecuteMove(gs, move);
            }
            else
            {
                if (!gs.FirstPartDone)
                {
                    // Evaluete Dinamite card
                    if (HasCard(gs, gs.CurrentPlayerID, "Dynamite"))
                    {
                        blueCardExecutor.ApplyDinamiteEffect(gs);
                    }

                    // Evaluete Prison card and continue or skip player's turn
                    if (HasCard(gs, gs.CurrentPlayerID, "Jail") && blueCardExecutor.ApplyJailEffect(gs))
                    {
                        // Send changed GameState to the server 
                        client.SendString("move " + GetGameStateJson());
                        return;
                    }

                    move.SetLastEvent(gs.CurrentPlayerID, "TakeTwoCards");
                    moveExecutor.ExecuteMove(gs, move);
                    gs.FirstPartDone = true;
                }
                else
                {
                    Console.Write("Insert your move: ");
                    move = IdentifyMove(inputValidator.InsertMove(gs));

                    // Vlaidate move
                    while (!validator.ValidateMove(gs, move))
                    {
                        Console.Write("Insert your move: ");
                        move = IdentifyMove(inputValidator.InsertMove(gs));



                    }
                    // Execute move
                    moveExecutor.ExecuteMove(gs, move);

                }

            }



            // Send changed GameState to the server 
            client.SendString("move " + GetGameStateJson());


        }

        private GameEvent IdentifyMove(string moveString)
        {
            List<string> moveParts = moveString.Split(' ').ToList();
            GameEvent move = new GameEvent();
            move.SetLastEvent(Int32.Parse(moveParts[0]), moveParts[1], Int32.Parse(moveParts[2]));
            return move;

        }
    }
}
