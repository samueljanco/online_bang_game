using System;
using System.Linq;

namespace BangOnline
{
    public class JesseJones : Character
    {
        public JesseJones(string name, string ability, int lifes) : base(name, ability, lifes)
        {
        }

        public override bool ApplyAbility(GameState gs, int playedBy)
        {
            if (gs.Players[playedBy].ChosenCharacter == Name)
            {
                Console.Write("Draw first card from other player or from deck? (player name cardNumber/ deck): ");
                string playerOrDeck = Console.ReadLine();
                if (playerOrDeck == "deck")
                {
                    return false;
                }
                else {
                    string[] input = playerOrDeck.Split(' ');
                    int playedOn = gs.Players.FirstOrDefault(x => x.Value.Name == input[1]).Key;
                    string card = gs.Players[playedOn].CardsInHand[Int32.Parse(input[2])];
                    gs.Players[playedBy].CardsInHand.Add(card);
                    gs.Players[playedOn].CardsInHand.Remove(card);
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
