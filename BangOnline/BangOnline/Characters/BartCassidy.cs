using System;
namespace BangOnline
{
    public class BartCassidy : Character
    {
        public BartCassidy(string name,string ability,int lifes) : base(name, ability, lifes)
        {
        }

        public override bool ApplyAbility(GameState gs, int playedBy)
        {
            if (gs.Players[playedBy].ChosenCharacter == Name)
            {
                gs.Players[playedBy].CardsInHand.Add(gs.DeckOfCards.UnusedCards[0]);
                gs.DeckOfCards.UnusedCards.RemoveAt(0);
                return true;
            }
            return false;
        }
    }
}
