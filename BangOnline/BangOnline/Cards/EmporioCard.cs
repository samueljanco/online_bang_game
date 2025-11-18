using System;
namespace BangOnline
{
    public class EmporioCard : Card
    {
        public EmporioCard()
        {
        }

        public override void Action(GameState gs, int playedBy)
        {
            foreach (PlayerInfo player in gs.Players.Values)
            {
                if (player.IsAlive)
                {
                    player.CardsInHand.Add(gs.DeckOfCards.UnusedCards[0]);
                    gs.DeckOfCards.UnusedCards.RemoveAt(0);
                }

            }
            gs.LastEvent.SetLastEvent(playedBy, "Emporio");
        }

        public override bool Validate(GameState gs, GameEvent move)
        {
            return ValidatePossesion(gs, move);
        }
    }
}
