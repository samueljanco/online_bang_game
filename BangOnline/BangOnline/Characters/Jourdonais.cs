using System;
namespace BangOnline
{
    public class Jourdonais : Character
    {
        public Jourdonais(string name, string ability, int lifes) : base(name, ability, lifes)
        {
        }

        public override bool ApplyAbility(GameState gs, int playedBy)
        {
            if (gs.Players[playedBy].ChosenCharacter == Name)
            {
                string card = gs.DeckOfCards.UnusedCards[0];
                gs.DeckOfCards.UsedCards.Add(card);
                gs.DeckOfCards.UnusedCards.Remove(card);
                string value = GetCardValue(card).Item2;
                if (value == "H") {
                    return true;
                }
                
            }
            return false;
        }
    }
}
