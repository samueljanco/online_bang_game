using System;
namespace BangOnline
{
    public class BarrelCard : Card
    {
        public BarrelCard()
        {
        }

        public override void Action(GameState gs, int playedBy)
        {
            UseBlueCardAction(gs, playedBy, "Barrel");
        }


        public override bool Validate(GameState gs, GameEvent move)
        {
            return ValidatePossesion(gs, move);
        }
    }
}
