using System;
namespace BangOnline
{
    public class JailCard : Card
    {
        public JailCard()
        {
        }

        public override void Action(GameState gs, int playedBy, int playedOn)
        {
            string card = TranslateCardName(gs, playedBy, "Jail", "hand");
            gs.Players[playedBy].CardsInHand.Remove(card);
            gs.Players[playedOn].CardsOnTable.Add(card);
            gs.LastEvent.SetLastEvent(playedBy, "Jail", playedOn);
        }

        public override bool Validate(GameState gs, GameEvent move)
        {
            return ValidatePossesionAndTarget(gs, move);
        }
    }
}
