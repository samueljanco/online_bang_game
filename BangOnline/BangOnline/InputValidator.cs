using System;
using System.Linq;

namespace BangOnline
{
    public class InputValidator
    {
        public InputValidator()
        {
        }

        public int ChooseCharacter() {
            string characterNumber = Console.ReadLine();
            while (characterNumber != "1" && characterNumber != "2")
            {
                Console.Write("Invalid input character: ");
                characterNumber = Console.ReadLine();
            }

            return Int32.Parse(characterNumber) - 1;
        }

        public string ChooseNick() {
            string nick = Console.ReadLine();
            while (nick.Length < 0 || nick.Length > 15)
            {
                Console.Write("Nick too short or too long. Enter new one please: ");
                nick = Console.ReadLine();
            }
            return nick;
        }

        public string InsertMove(GameState gs) {
            string[] move = Console.ReadLine().Split();
            if (move.Length == 1)
            {
                return $"{gs.CurrentPlayerID} {move[0]} -1";
            }
            else if (move.Length == 2 && gs.Players.ContainsKey(gs.Players.FirstOrDefault(x => x.Value.Name == move[1]).Key))
            {
                return $"{gs.CurrentPlayerID} {move[0]} {gs.Players.FirstOrDefault(x => x.Value.Name == move[1]).Key}";
            }
            else {

                return InsertMove(gs);
            }
            

        }
    }
}
