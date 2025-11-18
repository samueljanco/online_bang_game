using System;
namespace BangOnline
{
    public class RevCarabineCard : Card
    {
        public RevCarabineCard()
        {
        }

        public override void Action(GameState gs, int playedBy)
        {
            UseBlueCardAction(gs, playedBy, "Rev_Carabine");
        }

        public override bool Validate(GameState gs, GameEvent move)
        {
            return ValidatePossesion(gs, move);
        }
    }
}
