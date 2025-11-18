using System;
namespace BangOnline
{
    public class BangCard : Card
    {
        public BangCard()
        {
        }

        public override void Action(GameState gs, int playedBy, int playedOn)
        {
            gs.CurrentPlayerID = playedOn;
            gs.PlayedBang = blueCardsExecutor.ApplyVolcanicEffect(gs) &&
                !material.Characters["Willy the Kid"].ApplyAbility(gs, playedBy);

            gs.LastEvent.SetLastEvent(playedBy, "Bang", playedOn);
        }

        public override bool Validate(GameState gs, GameEvent move)
        {
            int range = blueCardsExecutor.ApplyGunEffect(gs) + blueCardsExecutor.ApplyScopeEffect(gs)
                + blueCardsExecutor.ApplyMustangEffect(gs, move.PlayedOn) - Convert.ToInt32(material.Characters["Paul Regret"].ApplyAbility(gs, move.PlayedOn))
                + Convert.ToInt32(material.Characters["Rose Doolan"].ApplyAbility(gs, move.PlayedBy));

            if (!HasCard(gs, move.PlayedBy, move.Event, "hand"))
            {
                Console.WriteLine("No such card in hand!");
                return false;
            }
            else if (gs.PlayedBang)
            {
                Console.WriteLine("You can play only one Bang during your move.");
                return false;
            }
            else if (move.PlayedOn == -1)
            {
                Console.WriteLine("Choose target of the card.");
                return false;
            }
            else if (!gs.Players[move.PlayedOn].IsAlive)
            {
                Console.WriteLine("The player is already dead.");
                return false;
            }
            else if (!IsInRange(move.PlayedBy, move.PlayedOn, range, gs.Players.Count))
            {
                Console.WriteLine("Player is out of your range.");
                return false;
            }

            return true;
        }
    }
}
// calamity translate and has card 