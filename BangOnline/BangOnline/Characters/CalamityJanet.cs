using System;
namespace BangOnline
{
    public class CalamityJanet : Character
    {
        public CalamityJanet(string name, string ability, int lifes) : base(name, ability, lifes)
        {
        }

        public override bool ApplyAbility(GameState gs, int playedBy)
        {
            if (gs.Players[playedBy].ChosenCharacter == Name)
            { 
                return HasCard(gs, playedBy, "Bang") || HasCard(gs, playedBy, "Missed");
            }
            return false;
        }
    }
}
