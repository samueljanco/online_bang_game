using System;
using System.Collections.Generic;
using System.Linq;
namespace BangOnline
{
    public class Card
    {
        protected GameMaterial material = new GameMaterial();
        protected BlueCardEffectExecutor blueCardsExecutor = new BlueCardEffectExecutor();

        public Card()
        {
           
        }

        public virtual void Action(GameState gs, int playedBy, int playedOn) { }

        public virtual void Action(GameState gs, int playedBy) { }

        public virtual void Response(GameState gs, int playedBy, int playedOn) { }

        public virtual void Response(GameState gs, int playedBy) { }

        public virtual bool Validate(GameState gs, GameEvent move) { return false; }

        protected (string, string) GetCardValue(string cardCodeName)
        {
            string value = cardCodeName.Split('-')[0];
            if (value.Length == 3)
            {
                return (value.Substring(0, 2), value.Substring(2));
            }
            else
            {
                return (value.Substring(0, 1), value.Substring(1));
            }
        }

        protected void ChangePlayer(GameState gs)
        {

            gs.CurrentPlayerID = (gs.CurrentPlayerID + 1) % gs.Players.Count;
            while (!gs.Players[gs.CurrentPlayerID].IsAlive)
            {
                gs.CurrentPlayerID = (gs.CurrentPlayerID + 1) % gs.Players.Count;
            }
        }

        protected bool HasCard(GameState gs, int player, string card, string handOrTable)
        {
            if (handOrTable == "hand")
            {
                if (card == "Missed" || card == "Bang") {
                    return material.Characters["Calamity Janet"].ApplyAbility(gs, player) || gs.Players[player].CardsInHand.Exists(s => s.Contains(card));
                }
                return gs.Players[player].CardsInHand.Exists(s => s.Contains(card));
            }
            else
            {
                return gs.Players[player].CardsOnTable.Exists(s => s.Contains(card));
            }
        }

        protected string TranslateCardName(GameState gs, int playedBy, string cardNiceName, string handOrTable)
        {
            if (handOrTable == "hand")
            {

                if (material.Characters["Calamity Janet"].ApplyAbility(gs, playedBy))
                {

                    if (cardNiceName == "Missed")
                    {
                        if (gs.Players[playedBy].CardsInHand.Exists(s => s.Contains(cardNiceName)))
                        {
                            return gs.Players[playedBy].CardsInHand.FirstOrDefault(s => s.Contains("Missed"));
                        }
                        else
                        {
                            return gs.Players[playedBy].CardsInHand.FirstOrDefault(s => s.Contains("Bang"));
                        }
                    }
                    else
                    {
                        if (gs.Players[playedBy].CardsInHand.Exists(s => s.Contains(cardNiceName)))
                        {
                            return gs.Players[playedBy].CardsInHand.FirstOrDefault(s => s.Contains("Bang"));
                        }
                        else
                        {
                            return gs.Players[playedBy].CardsInHand.FirstOrDefault(s => s.Contains("Missed"));
                        }
                    }
                }
                else {
                    return gs.Players[playedBy].CardsInHand.FirstOrDefault(s => s.Contains(cardNiceName));

                }
            }
            else
            {
                return gs.Players[playedBy].CardsOnTable.FirstOrDefault(s => s.Contains(cardNiceName));
            }

        }

        protected void Die(GameState gs, int died, int killedBy)
        {

            if (!material.Characters["Vulture Sam"].ApplyAbility(gs, gs.Players.FirstOrDefault(p => p.Value.ChosenCharacter == "Vulture Sam").Key, died)) {
                foreach (string card in gs.Players[died].CardsInHand)
                {
                    gs.DeckOfCards.UsedCards.Add(card);
                }
                gs.Players[died].CardsInHand.Clear();
                foreach (string card in gs.Players[died].CardsOnTable)
                {
                    gs.DeckOfCards.UsedCards.Add(card);
                }
                gs.Players[died].CardsOnTable.Clear();
            }

            
            gs.Players[died].IsAlive = false;
            gs.LastEvent.SetLastEvent(died, "was killed by", killedBy);

            if (gs.Players[died].Role == "Bandit")
            {
                for (int i = 0; i < 3; i++)
                {
                    gs.Players[killedBy].CardsInHand.Add(gs.DeckOfCards.UnusedCards[0]);
                    gs.DeckOfCards.UnusedCards.RemoveAt(0);
                }
            }
            else if (gs.Players[died].Role == "Vice" && gs.Players[killedBy].Role == "Sheriff")
            {
                foreach (string card in gs.Players[killedBy].CardsInHand)
                {
                    gs.DeckOfCards.UsedCards.Add(card);
                }
                gs.Players[killedBy].CardsInHand.Clear();
                foreach (string card in gs.Players[killedBy].CardsOnTable)
                {
                    gs.DeckOfCards.UsedCards.Add(card);
                }
                gs.Players[killedBy].CardsOnTable.Clear();
            }
            
        }

        protected void ThrowAwayUsedCard(GameState gs, int playedBy, string cardName)
        {
            int index = gs.Players[playedBy].CardsInHand.IndexOf(cardName);
            gs.DeckOfCards.UsedCards.Add(gs.Players[playedBy].CardsInHand[index]);
            gs.Players[playedBy].CardsInHand.RemoveAt(index);

        }

        protected void UseBlueCardAction(GameState gs, int playedBy, string card)
        {
            string cardToUse = TranslateCardName(gs, playedBy, card, "hand");
            gs.Players[playedBy].CardsOnTable.Add(cardToUse);
            gs.Players[playedBy].CardsInHand.Remove(cardToUse);
            gs.LastEvent.SetLastEvent(playedBy, $"put {card} on the table");

        }


        protected bool IsInRange(int playedBy, int playedOn, int range, int playersCount)
        {

            for (int i = 1; i <= range; i++)
            {
                int x = (playedBy + i) % playersCount;
                int y = ((playedBy - i) % playersCount + playersCount) % playersCount;
                if (x == playedOn || y == playedOn)
                {
                    return true;
                }

            }
            return false;

        }

        protected bool ValidatePossesion(GameState gs, GameEvent move) {
            if (HasCard(gs, move.PlayedBy, move.Event, "hand"))
            {
                return true;
            }
            else
            {
                Console.WriteLine("No such card in hand!");
                return false;
            }
        }

        protected bool ValidatePossesionAndTarget(GameState gs, GameEvent move)
        {
            if(move.PlayedOn == -1)
                    {
                Console.WriteLine("Choose target of the card.");
                return false;
            }
                    else if (!gs.Players[move.PlayedOn].IsAlive)
            {
                Console.WriteLine("The player is dead.");
                return false;
            }


            if (HasCard(gs, move.PlayedBy, move.Event,"hand"))
            {
                return true;
            }
            else
            {
                Console.WriteLine("No such card in hand!");
                return false;
            }
        }

    }
}
