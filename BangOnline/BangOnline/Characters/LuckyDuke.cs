using System;
namespace BangOnline
{
    public class LuckyDuke : Character
    {
        public LuckyDuke(string name, string ability, int lifes) : base(name, ability, lifes)
        {
        }



        public override bool ApplyAbility(GameState gs, int playedBy, string situation)
        {
            if (gs.Players[playedBy].ChosenCharacter == Name)
            {
                string card = gs.DeckOfCards.UnusedCards[0];
                gs.Players[playedBy].CardsInHand.Add(card);
                gs.DeckOfCards.UnusedCards.Remove(card);
                (string ,string) cardValue = GetCardValue(card);


                switch (situation)
                {
                    case "Dynamite":
     
                        return Int32.TryParse(cardValue.Item1, out int numValue) && (numValue < 2 || numValue > 9 || cardValue.Item2 != "S");
                    case "Jail":
                    case "Barrel":
                        return cardValue.Item2 == "H";
                    default:
                        break;
                }
            }
            return false;
        }
    }
}
