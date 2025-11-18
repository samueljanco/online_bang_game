using System;
namespace BangOnline
{
    public class BeerCard : Card
    {
        public BeerCard()
        {
        }

        public override void Action(GameState gs, int playedBy)
        {
            int maxLives = material.Characters[gs.Players[playedBy].ChosenCharacter].Lives + ((gs.Players[playedBy].Role == "Sheriff") ? 1 : 0);
            if (gs.Players[playedBy].Lives < maxLives)
            {
                gs.Players[playedBy].Lives++;
            }
            gs.LastEvent.SetLastEvent(playedBy, "Beer");
        }

        public override bool Validate(GameState gs, GameEvent move)
        {
            return ValidatePossesion(gs, move);
        }
    }
}
