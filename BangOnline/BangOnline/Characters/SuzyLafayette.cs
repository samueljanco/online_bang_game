using System;
namespace BangOnline
{
    public class SuzyLafayette : Character
    {
        public SuzyLafayette(string name, string ability, int lifes) : base(name, ability, lifes)
        {
        }

        public override bool ApplyAbility(GameState gs, int playedBy)
        {
            if (gs.Players[playedBy].ChosenCharacter == Name)
            {
                if (gs.Players[playedBy].CardsInHand.Count == 0) {
                    string card = gs.DeckOfCards.UnusedCards[0];
                    gs.Players[playedBy].CardsInHand.Add(card);
                    gs.DeckOfCards.UnusedCards.Remove(card);
                    return true;
                }
            }
            return false;
        }


    }
}
