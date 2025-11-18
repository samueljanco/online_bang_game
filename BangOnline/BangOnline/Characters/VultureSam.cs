using System;
namespace BangOnline
{
    public class VultureSam : Character
    {
        public VultureSam(string name, string ability, int lifes) : base(name, ability, lifes)
        {
        }


        public override bool ApplyAbility(GameState gs, int playedBy, int playedOn)
        {
            if (gs.Players[playedBy].ChosenCharacter == Name)
            {
                foreach (string card in gs.Players[playedOn].CardsInHand)
                {
                    gs.Players[playedBy].CardsInHand.Add(card);
                }
                gs.Players[playedOn].CardsInHand.Clear();
                foreach (string card in gs.Players[playedOn].CardsOnTable)
                {
                    gs.Players[playedBy].CardsInHand.Add(card);
                }
                gs.Players[playedOn].CardsOnTable.Clear();
                return true;
            }
            return false;
        }
    }
}
