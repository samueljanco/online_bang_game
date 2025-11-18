using System;
namespace BangOnline
{
    public class WellsFargoCard : Card
    {
        public WellsFargoCard()
        {
        }

        public override void Action(GameState gs, int playedBy)
        {
            for(int i = 0; i < 3; i++)
            {
                gs.Players[playedBy].CardsInHand.Add(gs.DeckOfCards.UnusedCards[0]);
                gs.DeckOfCards.UnusedCards.RemoveAt(0);

            }
            gs.LastEvent.SetLastEvent(playedBy, "Wells Fargo");
        }

        public override bool Validate(GameState gs, GameEvent move)
        {
            return ValidatePossesion(gs, move);
        }
    }
}
    