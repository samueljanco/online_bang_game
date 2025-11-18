using System;
namespace BangOnline
{
    public class SlabTheKiller : Character
    {
        public SlabTheKiller(string name, string ability, int lifes) : base(name, ability, lifes)
        {
        }
        

        public override bool ApplyAbility(GameState gs, int playedBy)
        {
            return gs.Players[playedBy].ChosenCharacter == Name;
            
        }
    }
}
