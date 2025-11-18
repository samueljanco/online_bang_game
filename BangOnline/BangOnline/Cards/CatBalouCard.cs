using System;
namespace BangOnline
{
    public class CatBalouCard : Card
    {
        public CatBalouCard()
        {
        }

        public override void Action(GameState gs, int playedBy, int playedOn)
        {
            Console.WriteLine("Chose card to destroy (hand number / table cardname):");

            string cardToDestroy = Console.ReadLine();
            if (cardToDestroy[0] == 'h')
            {
                int cardNumber = Int32.Parse(cardToDestroy.Substring(5)) - 1;
                while (cardNumber < 0 && cardNumber >= gs.Players[playedOn].CardsInHand.Count)
                {
                    Console.WriteLine("Invalid input. Reenter the card number.");
                    cardNumber = Int32.Parse(Console.ReadLine()) - 1;
                }
                gs.DeckOfCards.UsedCards.Add(gs.Players[playedOn].CardsInHand[cardNumber]);
                gs.Players[playedOn].CardsInHand.RemoveAt(cardNumber);

                material.Characters["Suzy Lafayette"].ApplyAbility(gs, playedOn);
            }
            else
            {
                string cardName = cardToDestroy.Substring(6);
                while (!HasCard(gs, playedBy, cardName, "table"))
                {
                    Console.WriteLine("Invalid input. Reenter the card name.");
                    cardName = Console.ReadLine();
                }
                gs.DeckOfCards.UsedCards.Add(cardName);
                gs.Players[playedOn].CardsOnTable.Remove(cardName);

            }
            gs.LastEvent.SetLastEvent(playedBy, "Cat Balou", playedOn);
        }

        public override bool Validate(GameState gs, GameEvent move)
        {
            return ValidatePossesionAndTarget(gs, move);
        }
    }
}
