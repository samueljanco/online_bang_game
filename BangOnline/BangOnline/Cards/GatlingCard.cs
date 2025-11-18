using System;
namespace BangOnline
{
    public class GatlingCard : Card
    {
        public GatlingCard()
        {
        }

        public override void Action(GameState gs, int playedBy)
        {
            ChangePlayer(gs);
            gs.LastEvent.SetLastEvent(playedBy, "GatlingResponse", gs.CurrentPlayerID);
        }

        public override void Response(GameState gs, int playedBy) {
            Console.WriteLine("Gatling! Play Missed card or lose one life point (Missed/LoseLife).");
            Console.Write("Insert yout move:");
            string move = Console.ReadLine();
            while ((move != "Missed" && move != "LoseLife") ||
                (move == "Missed" && !HasCard(gs,playedBy,"Missed", "hand"))
            )
            {
                Console.WriteLine("Invalid input or no Missed card in hand.");
                move = Console.ReadLine();
            }
            if (move == "LoseLife")
            {
                gs.Players[gs.CurrentPlayerID].Lives--;
                if (gs.Players[gs.CurrentPlayerID].Lives <= 0)
                {
                    Die(gs, gs.CurrentPlayerID, playedBy);
                }
            }
            else
            {

                string card = TranslateCardName(gs, gs.CurrentPlayerID, "Missed", "hand");
                gs.DeckOfCards.UsedCards.Add(card);
                gs.Players[playedBy].CardsInHand.Remove(card);

            }

            gs.CurrentPlayerID = (gs.CurrentPlayerID + 1) % gs.Players.Count;

            if (playedBy != gs.CurrentPlayerID)
            {
                gs.LastEvent.SetLastEvent(playedBy, "GatlingResponse", gs.CurrentPlayerID);
            }
            else
            {
                gs.LastEvent.SetLastEvent(playedBy, "Gatling Ended");
            }
        }

        public override bool Validate(GameState gs, GameEvent move)
        {
            return ValidatePossesion(gs, move);
        }
    }
}
