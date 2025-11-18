using System;
using System.Linq;

namespace BangOnline
{
    public class PedroRamirez : Character
    {
        public PedroRamirez(string name, string ability, int lifes) : base(name, ability, lifes)
        {
        }

        public override bool ApplyAbility(GameState gs, int playedBy)
        {
            if (gs.Players[playedBy].ChosenCharacter == Name)
            {
                Console.Write("Draw first card from discard pile or from deck? (discard pile / deck): ");
                string playerOrDeck = Console.ReadLine();
                if (playerOrDeck == "deck")
                {
                    return false;
                }
                else
                {
                    
                    string card = gs.DeckOfCards.UsedCards[gs.DeckOfCards.UsedCards.Count-1];
                    gs.Players[playedBy].CardsInHand.Add(card);
                    gs.DeckOfCards.UsedCards.Remove(card);
                    card = gs.DeckOfCards.UnusedCards[0];
                    gs.Players[playedBy].CardsInHand.Add(card);
                    gs.DeckOfCards.UnusedCards.Remove(card);
                    return true;
                }

            }
            return false;
        }
    }
}
