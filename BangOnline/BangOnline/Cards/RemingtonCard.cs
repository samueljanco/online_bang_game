using System;
namespace BangOnline
{
    public class RemingtonCard : Card
    {
        public RemingtonCard()
        {
        }

        public override void Action(GameState gs, int playedBy)
        {
            UseBlueCardAction(gs, playedBy, "Remington");
        }

        public override bool Validate(GameState gs, GameEvent move)
        {
            return ValidatePossesion(gs, move);
        }
    }
}
