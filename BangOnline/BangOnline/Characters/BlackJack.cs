using System;
namespace BangOnline
{
    public class BlackJack : Character
    {
        public BlackJack(string name, string ability, int lifes) : base(name, ability, lifes)
        {
        }

        public override bool ApplyAbility(GameState gs, int playedBy)
        {
            if (gs.Players[playedBy].ChosenCharacter == Name)
            {
                string value = GetCardValue(gs.DeckOfCards.UnusedCards[1]).Item2;
                if (value == "H" || value == "D")
                {
                    gs.Players[playedBy].CardsInHand.Add(gs.DeckOfCards.UnusedCards[0]);
                    gs.DeckOfCards.UnusedCards.RemoveAt(0);
                    return true;
                }
            }
            return false;
        }

    }
}
