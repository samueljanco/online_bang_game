using System;
using System.Linq;
namespace BangOnline
{
    public class DuelCard : Card
    {
        public DuelCard()
        {
        }

        public override void Action(GameState gs, int playedBy, int playedOn)
        {
            gs.LastEvent.SetLastEvent(playedBy, "Duel", playedOn);
            gs.CurrentPlayerID = playedOn;
        }
        public override void Response(GameState gs, int playedBy, int playedOn) {
            Console.WriteLine("You are in a duel. Play Bang or lose a life point (Bang/LoseLife).");
            string move = Console.ReadLine();
            while ((move != "Bang" && move != "LoseLife") ||
                (move == "Bang" && !gs.Players[playedBy].CardsInHand.Contains(TranslateCardName(gs, playedBy, "Bang", "hand")))
            )
            {
                Console.WriteLine("Invalid input or no Bang card in hand.");
                move = Console.ReadLine();
            }
            if (move == "Bang")
            {
                ThrowAwayUsedCard(gs, playedBy, TranslateCardName(gs, playedBy, "Bang", "hand"));
                Action(gs, playedBy, playedOn);

            }
            else
            {
                gs.Players[playedBy].Lives--;
                material.Characters["Bart Cassidy"].ApplyAbility(gs, playedBy);
                material.Characters["El Gringo"].ApplyAbility(gs, playedBy, playedOn);
                string cardCodeName = gs.DeckOfCards.UsedCards.LastOrDefault(s => s.Contains("Duel"));
                int amountOfBangs = gs.DeckOfCards.UsedCards.Count - gs.DeckOfCards.UsedCards.LastIndexOf(cardCodeName) - 1;
                gs.CurrentPlayerID = (amountOfBangs % 2 == 0) ? playedOn : playedBy;
                gs.LastEvent.SetLastEvent(playedOn, "won the duel over", playedBy);
                if (gs.Players[playedBy].Lives <= 0)
                {
                    Die(gs, playedBy, playedOn);
                }

            }
        }

        public override bool Validate(GameState gs, GameEvent move)
        {
            return ValidatePossesionAndTarget(gs, move);
        }
    }
}
