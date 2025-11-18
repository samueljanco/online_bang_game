using System;
namespace BangOnline
{
    public class SidKetchum : Character
    {
        public SidKetchum(string name, string ability, int lifes) : base(name, ability, lifes)
        {
        }
        private int thrownCards = 1;


        public override bool ApplyAbility(GameState gs, int playedBy)
        {
            if (gs.Players[playedBy].ChosenCharacter == Name && thrownCards != 2)
            {
                Console.Write("Throw away another card to gain one life point back. (yes / no): ");
                string input = Console.ReadLine();
                while (input != "yes" && input != "no")
                {
                    Console.WriteLine("Invalid input");
                    Console.Write("Reenter your move: ");
                    input = Console.ReadLine();
                }

                if (input == "yes")
                {
                    thrownCards++;
                    return true;
                }



            }
            thrownCards--;
            return false;
        }
    }
}
