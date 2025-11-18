using System;
namespace BangOnline
{
    public class ElGringo : Character
    {
        public ElGringo(string name, string ability, int lifes) : base(name, ability, lifes)
        {
        }

        public override bool ApplyAbility(GameState gs, int playedBy, int playedOn)
        {
            if (gs.Players[playedBy].ChosenCharacter == Name)
            {
                Console.Write("Choose number of card to get: ");
                int cardNumber = Int32.Parse(Console.ReadLine()) - 1;
                while (cardNumber < 0 || cardNumber >= gs.Players[playedOn].CardsInHand.Count) {
                    Console.WriteLine("Invalid input");
                    Console.Write("Choose number of card to get: ");
                    cardNumber = Int32.Parse(Console.ReadLine()) - 1;
                }
                string card = gs.Players[playedOn].CardsInHand[cardNumber];
                gs.Players[playedBy].CardsInHand.Add(card);
                gs.Players[playedOn].CardsInHand.Remove(card);
                return true;
            }
            return false;
        }
    }
}
