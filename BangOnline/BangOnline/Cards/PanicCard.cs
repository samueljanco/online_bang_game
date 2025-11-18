using System;
namespace BangOnline
{
    public class PanicCard : Card
    {
        public PanicCard()
        {
        }

        public override void Action(GameState gs, int playedBy, int playedOn)
        {
            //validation
            Console.WriteLine("Chose card to get from the player (number of the card):");

            int cardToTake = Int32.Parse(Console.ReadLine()) - 1;
            while (cardToTake < 0 && cardToTake >= gs.Players[playedOn].CardsInHand.Count)
            {
                Console.WriteLine("Number not in valid range.");
                cardToTake = Int32.Parse(Console.ReadLine()) - 1;
            }
            gs.Players[playedBy].CardsInHand.Add(gs.Players[playedOn].CardsInHand[cardToTake]);
            gs.Players[playedOn].CardsInHand.RemoveAt(cardToTake);

            material.Characters["Suzy Lafayette"].ApplyAbility(gs, playedOn);

            gs.LastEvent.SetLastEvent(playedBy, "Panic", playedOn);
        }

        public override bool Validate(GameState gs, GameEvent move)
        {
            if (!HasCard(gs, move.PlayedBy, move.Event, "hand"))
            {
                Console.WriteLine("No such card in hand!");
                return false;
            }
            else if (move.PlayedOn == -1)
            {
                Console.WriteLine("Choose target of the card.");
                return false;
            }
            else if (!gs.Players[move.PlayedOn].IsAlive)
            {
                Console.WriteLine("The player is dead.");
                return false;
            }
            else if ((move.PlayedBy + 1) % gs.Players.Count != move.PlayedOn && (move.PlayedBy - 1) % gs.Players.Count != move.PlayedOn)
            {
                Console.WriteLine("Player is out of your range.");
                return false;
            }
            return true;
        }
    }
}
