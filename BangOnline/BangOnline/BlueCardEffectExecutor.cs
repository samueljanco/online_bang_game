using System;
using System.Linq;

namespace BangOnline
{
    public class BlueCardEffectExecutor
    {
        private GameMaterial material = new GameMaterial();
        public BlueCardEffectExecutor()
        {
        }

        public int ApplyGunEffect(GameState gs) {
            foreach (string gun in material.GunsRanges.Keys)
            {
                if (HasCard(gs,gs.CurrentPlayerID,gun)) {
                    return material.GunsRanges[gun];
                }
            }

            return 1;
        }

        public int ApplyScopeEffect(GameState gs)
        {
            
            if (HasCard(gs, gs.CurrentPlayerID, "Scope"))
            {
                return 1;
            }
            

            return 0;
        }

        public int ApplyMustangEffect(GameState gs, int playedOn)
        {

            if (HasCard(gs, playedOn, "Mustang"))
            {
                return -1;
            }


            return 0;
        }

        public void ApplyDinamiteEffect(GameState gs) {
            Console.WriteLine("Press Enter to draw on Dinamite.");
            Console.ReadLine();

            string card = gs.DeckOfCards.UnusedCards[0];
            gs.DeckOfCards.UnusedCards.Remove(card);
            gs.DeckOfCards.UsedCards.Add(card);
            (string, string) cardValue = GetCardValue(card);

            if (Int32.TryParse(cardValue.Item1, out int numValue) && (numValue >= 2 && numValue <= 9 && cardValue.Item2 == "S") && !material.Characters["Lucky Duke"].ApplyAbility(gs, gs.CurrentPlayerID))
            {
                
                gs.Players[gs.CurrentPlayerID].Lives -= 3;
                string dinamite = TranslateCardName(gs, gs.CurrentPlayerID, "Dynamite", "table");
                gs.Players[gs.CurrentPlayerID].CardsOnTable.Remove(dinamite);
                gs.DeckOfCards.UsedCards.Add(dinamite);
                gs.LastEvent.SetLastEvent(gs.CurrentPlayerID, "blown Dynamite");
                if (gs.Players[gs.CurrentPlayerID].Lives <= 0)
                {
                    Die(gs, gs.CurrentPlayerID);
                }
            }
            else {
                string dinamite = TranslateCardName(gs, gs.CurrentPlayerID, "Dynamite", "table");
                gs.Players[gs.CurrentPlayerID].CardsOnTable.Remove(dinamite);
                
                gs.Players[PassDynamiteTo(gs, gs.CurrentPlayerID)].CardsOnTable.Add(dinamite);
                
                
            }
        }

        public bool ApplyJailEffect(GameState gs)
        {
            Console.WriteLine("Press Enter to draw on Jail.");
            Console.ReadLine();

            string card = gs.DeckOfCards.UnusedCards[0];
            gs.DeckOfCards.UnusedCards.Remove(card);
            gs.DeckOfCards.UsedCards.Add(card);

            string jail = TranslateCardName(gs, gs.CurrentPlayerID, "Jail", "table");
            gs.Players[gs.CurrentPlayerID].CardsOnTable.Remove(jail);
            gs.DeckOfCards.UsedCards.Add(jail);

            (string, string) cardValue = GetCardValue(card);
            if (cardValue.Item2 != "H" && !material.Characters["Lucky Duke"].ApplyAbility(gs, gs.CurrentPlayerID))
            {
                gs.LastEvent.SetLastEvent(gs.CurrentPlayerID, "Jail(unsuccessful)");
                ChangePlayer(gs);
                
                return false;
            }
            gs.LastEvent.SetLastEvent(gs.CurrentPlayerID, "Jail(successful)");
            return true;
            
        }

        public bool ApplyBarrelEffect(GameState gs)
        {
            Console.WriteLine("Press Enter to use your Barrel.");
            Console.ReadLine();
            string card = gs.DeckOfCards.UnusedCards[0];
            gs.DeckOfCards.UnusedCards.Remove(card);
            gs.DeckOfCards.UsedCards.Add(card);


            return GetCardValue(card).Item2 == "H" || material.Characters["Lucky Duke"].ApplyAbility(gs, gs.CurrentPlayerID);
        }

        public bool ApplyVolcanicEffect(GameState gs) {
            return !HasCard(gs, gs.CurrentPlayerID, "Volcanic"); 
        }

        private bool HasCard(GameState gs, int player, string card)
        {

            return gs.Players[player].CardsOnTable.Exists(s => s.Contains(card));
        }

        private (string, string) GetCardValue(string cardCodeName)
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

        private string TranslateCardName(GameState gs, int playedBy, string cardNiceName, string handOrTable)
        {
            if (handOrTable == "hand")
            {
                return gs.Players[playedBy].CardsInHand.FirstOrDefault(s => s.Contains(cardNiceName));
            }
            else
            {
                return gs.Players[playedBy].CardsOnTable.FirstOrDefault(s => s.Contains(cardNiceName));
            }

        }

        private void Die(GameState gs, int died)
        {
            if (!material.Characters["Vulture Sam"].ApplyAbility(gs, gs.Players.FirstOrDefault(p => p.Value.ChosenCharacter == "Vulture Sam").Key, died))
            {
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
                gs.Players[died].IsAlive = false;
            }

            
            
        }

        private int PassDynamiteTo(GameState gs, int from) {
            from = ((from  - 1) % gs.Players.Count + gs.Players.Count) % gs.Players.Count;
            while (!gs.Players[from].IsAlive) {
                from = ((from - 1) % gs.Players.Count + gs.Players.Count) % gs.Players.Count;
            }
            return from;
        }

        protected void ChangePlayer(GameState gs)
        {

            gs.CurrentPlayerID = (gs.CurrentPlayerID + 1) % gs.Players.Count;
            while (!gs.Players[gs.CurrentPlayerID].IsAlive)
            {
                gs.CurrentPlayerID = (gs.CurrentPlayerID + 1) % gs.Players.Count;
            }
        }
    }
}
