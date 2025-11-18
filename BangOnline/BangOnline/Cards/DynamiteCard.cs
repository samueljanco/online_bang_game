using System;
namespace BangOnline
{
    public class DynamiteCard : Card
    {
        public DynamiteCard()
        {
        }

        public override void Action(GameState gs, int playedBy)
        {
            UseBlueCardAction(gs, playedBy, "Dynamite");
        }

        public override bool Validate(GameState gs, GameEvent move)
        {
            return ValidatePossesion(gs, move);
        }
    }
}
