using System;
namespace BangOnline
{
    public class SchofieldCard : Card
    {
        public SchofieldCard()
        {
        }

        public override void Action(GameState gs, int playedBy)
        {
            UseBlueCardAction(gs, playedBy, "Schofield");
        }

        public override bool Validate(GameState gs, GameEvent move)
        {
            return ValidatePossesion(gs, move);
        }
    }
}
