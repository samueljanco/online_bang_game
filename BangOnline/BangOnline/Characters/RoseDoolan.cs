using System;
namespace BangOnline
{
    public class RoseDoolan : Character
    {
        public RoseDoolan(string name, string ability, int lifes) : base(name, ability, lifes)
        {
        }

        public override bool ApplyAbility(GameState gs, int playedBy)
        {
            if (gs.Players[playedBy].ChosenCharacter == Name)
            {
                return true;

            }
            return false;
        }
    }
}
