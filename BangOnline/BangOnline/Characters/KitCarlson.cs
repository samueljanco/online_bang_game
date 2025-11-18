using System;
using System.Collections.Generic;
using System.Linq;
namespace BangOnline
{
    public class KitCarlson : Character
    {
        public KitCarlson(string name, string ability, int lifes) : base(name, ability, lifes)
        {
        }

        public override bool ApplyAbility(GameState gs, int playedBy)
        {
            if (gs.Players[playedBy].ChosenCharacter == Name)
            {
                
                
                Console.Write("Choose card to return to deck");
                for (int i = 0; i < 3; i++)
                {
                    Console.Write($"({i + 1}) {gs.DeckOfCards.UnusedCards[i]} ");
                }
                int cardNumber = Int32.Parse(Console.ReadLine())-1;
                string card = gs.DeckOfCards.UnusedCards[cardNumber];
                gs.DeckOfCards.UnusedCards[cardNumber] = gs.DeckOfCards.UnusedCards[2];
                gs.DeckOfCards.UnusedCards[2] = card;
                return true;
            }
            return false;
        }
    }
}
