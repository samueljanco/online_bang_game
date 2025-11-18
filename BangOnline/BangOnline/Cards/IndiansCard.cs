using System;
namespace BangOnline
{
    public class IndiansCard : Card
    {
        public IndiansCard()
        {
        }

        public override void Action(GameState gs, int playedBy)
        {
            ChangePlayer(gs);
            gs.LastEvent.SetLastEvent(playedBy, "IndiansResponse", gs.CurrentPlayerID);
        }

        public override void Response(GameState gs, int playedBy) {
            Console.WriteLine("Indians! Play Bang card or lose one life point (Bang/LoseLife).");
            Console.Write("Insert yout move:");
            string move = Console.ReadLine();
            while ((move != "Bang" && move != "LoseLife") || (move == "Bang" && !HasCard(gs, playedBy, "Bang", "hand")))
            {
                Console.WriteLine("Invalid input or no Bang card in hand.");
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
                string card = TranslateCardName(gs, gs.CurrentPlayerID, "Bang", "hand");
                gs.DeckOfCards.UsedCards.Add(card);
                gs.Players[playedBy].CardsInHand.Remove(card);

            }

            gs.CurrentPlayerID = (gs.CurrentPlayerID + 1) % gs.Players.Count;


            if (playedBy != gs.CurrentPlayerID)
            {
                gs.LastEvent.SetLastEvent(playedBy, "IndiansResponse", gs.CurrentPlayerID);
            }
            else
            {
                gs.LastEvent.SetLastEvent(playedBy, "Indians Ended");
            }
        }

        public override bool Validate(GameState gs, GameEvent move)
        {
            return ValidatePossesion(gs, move);
        }
    }
}
