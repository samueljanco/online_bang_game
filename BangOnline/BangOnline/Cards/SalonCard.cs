using System;
namespace BangOnline
{
    public class SalonCard : Card
    {
        public SalonCard()
        {
        }

        public override void Action(GameState gs, int playedBy)
        {
            foreach (PlayerInfo player in gs.Players.Values)
            {
                if (player.IsAlive)
                {
                    int maxLives = material.Characters[player.ChosenCharacter].Lives + ((player.Role == "Sheriff") ? 1 : 0);
                    if (player.Lives < maxLives)
                    {
                        player.Lives++;
                    }
                }
            }
            gs.LastEvent.SetLastEvent(playedBy, "Salon");
        }

        public override bool Validate(GameState gs, GameEvent move)
        {
            return ValidatePossesion(gs, move);
        }
    }
}
