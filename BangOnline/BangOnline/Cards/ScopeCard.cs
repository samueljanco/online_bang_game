using System;
namespace BangOnline
{
    public class ScopeCard : Card
    {
        public ScopeCard()
        {
        }

        public override void Action(GameState gs, int playedBy)
        {
            UseBlueCardAction(gs, playedBy, "Scope");
        }

        public override bool Validate(GameState gs, GameEvent move)
        {
            return ValidatePossesion(gs, move);
        }
    }
}
