using System;
namespace BangOnline
{
    public class WinchesterCard : Card
    {
        public WinchesterCard()
        {
        }

        public override void Action(GameState gs, int playedBy)
        {
            UseBlueCardAction(gs, playedBy, "Winchester"); ;
        }

        public override bool Validate(GameState gs, GameEvent move)
        {
            return ValidatePossesion(gs, move);
        }
    }
}
