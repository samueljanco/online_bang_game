using System;
namespace BangOnline
{
    public class VolcanicCard : Card
    {
        public VolcanicCard()
        {
        }

        public override void Action(GameState gs, int playedBy)
        {
            UseBlueCardAction(gs, playedBy, "Volcanic");
        }
        public override bool Validate(GameState gs, GameEvent move)
        {
            return ValidatePossesion(gs, move);
        }
    }
}
