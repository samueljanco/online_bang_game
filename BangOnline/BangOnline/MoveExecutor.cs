using System;
using System.Collections.Generic;
using System.Linq;

namespace BangOnline
{
    public class MoveExecutor
    {
        private CardList cardList = new CardList();
        private GameMaterial material = new GameMaterial();
        public MoveExecutor()
        {
        }

        public void ExecuteMove(GameState gs, GameEvent move) {

            
            string cardCodeName = TranslateCardName(gs, move.PlayedBy, move.Event, "hand");
            switch (move.Event)
            {
                case "Beer":
                    cardList.Cards["Beer"].Action(gs, move.PlayedBy);
                    ThrowAwayUsedCard(gs, move.PlayedBy, cardCodeName);
                    break;
                case "CatBalou":
                    cardList.Cards["Cat_Balou"].Action(gs, move.PlayedBy, move.PlayedOn);
                    ThrowAwayUsedCard(gs, move.PlayedBy, cardCodeName);
                    break;
                case "Bang":
                    cardList.Cards["Bang"].Action(gs, move.PlayedBy, move.PlayedOn);
                    ThrowAwayUsedCard(gs, move.PlayedBy, cardCodeName);
                    break;
                case "Stagecoach":
                    cardList.Cards["Stagecoach"].Action(gs, move.PlayedBy);
                    ThrowAwayUsedCard(gs, move.PlayedBy, cardCodeName);
                    break;
                case "WellsFargo":
                    cardList.Cards["Wells_Fargo"].Action(gs, move.PlayedBy);
                    ThrowAwayUsedCard(gs, move.PlayedBy, cardCodeName);
                    break;
                case "Duel":
                    cardList.Cards["Duel"].Action(gs, move.PlayedBy, move.PlayedOn);
                    ThrowAwayUsedCard(gs, move.PlayedBy, cardCodeName);
                    break;
                case "DuelResponse":
                    cardList.Cards["Duel"].Response(gs, move.PlayedBy, move.PlayedOn);
                    break;
                case "Emporio":
                    cardList.Cards["Emporio"].Action(gs, move.PlayedBy);
                    ThrowAwayUsedCard(gs, move.PlayedBy, cardCodeName);
                    break;
                case "Salon":
                    cardList.Cards["Salon"].Action(gs, move.PlayedBy);
                    ThrowAwayUsedCard(gs, move.PlayedBy, cardCodeName);
                    break;
                case "Indians":
                    cardList.Cards["Indians"].Action(gs, move.PlayedBy);
                    ThrowAwayUsedCard(gs, move.PlayedBy, cardCodeName);
                    break;
                case "IndiansResponse":
                    cardList.Cards["Indians"].Response(gs, move.PlayedBy);
                    break;
                case "Gatling":
                    cardList.Cards["Gatlimng"].Action(gs, move.PlayedBy);
                    ThrowAwayUsedCard(gs, move.PlayedBy, cardCodeName);
                    break;
                case "GatlingResponse":
                    cardList.Cards["Gatling"].Response(gs, move.PlayedBy);
                    break;
                case "Missed":
                    cardList.Cards["Missed"].Action(gs, move.PlayedBy, move.PlayedOn);
                    if (material.Characters["Slab the Killer"].ApplyAbility(gs, move.PlayedBy))
                    {
                        cardList.Cards["Missed"].Action(gs, move.PlayedBy, move.PlayedOn);
                    }
                    break;
                case "Panic":
                    cardList.Cards["Panic"].Action(gs, move.PlayedBy, move.PlayedOn);
                    ThrowAwayUsedCard(gs, move.PlayedBy, cardCodeName);
                    break;
                case "Jail":
                    cardList.Cards["Jail"].Action(gs, move.PlayedBy, move.PlayedOn);
                    break;
                case "ThrowAway":
                    ThrowAwayCardAction(gs, move.PlayedBy);
                    break;
                case "Mustang":
                    cardList.Cards["Mustang"].Action(gs, move.PlayedBy);
                    break;
                case "Scope":
                    cardList.Cards["Scope"].Action(gs, move.PlayedBy);
                    break;
                case "Barrel":
                    cardList.Cards["Barrel"].Action(gs, move.PlayedBy);
                    break;
                case "Dynamite":
                    cardList.Cards["Dynamite"].Action(gs, move.PlayedBy);
                    break;
                case "Volcanic":
                    cardList.Cards["Volcanic"].Action(gs, move.PlayedBy);
                    break;
                case "Schofield":
                    cardList.Cards["Schofield"].Action(gs, move.PlayedBy);
                    break;
                case "Remington":
                    cardList.Cards["Remington"].Action(gs, move.PlayedBy);
                    break;
                case "RevCarabine":
                    cardList.Cards["Rev_Carabine"].Action(gs, move.PlayedBy);
                    break;
                case "Winchester":
                    cardList.Cards["Winchester"].Action(gs, move.PlayedBy);
                    break;
                case "EndMove":
                    EndMoveAction(gs, move.PlayedBy);
                    
                    break;
                case "TakeTwoCards":
                    TakeTwoCards(gs, move.PlayedBy);
                    break;
                default:
                    break;
            }

            
            

        }
       
        private void ThrowAwayCardAction(GameState gs, int playedBy)
        {
            Console.WriteLine("Chose card to throw away:");
            string cardToThrowAway = Console.ReadLine();

            while (!HasCard(gs, playedBy, cardToThrowAway, "hand"))
            {
                Console.WriteLine("No such card in hand!");
                cardToThrowAway = Console.ReadLine();
            }

            cardToThrowAway = TranslateCardName(gs, playedBy, cardToThrowAway, "hand");
            int index = gs.Players[playedBy].CardsInHand.IndexOf(cardToThrowAway);
            gs.DeckOfCards.UsedCards.Add(gs.Players[playedBy].CardsInHand[index]);
            gs.Players[playedBy].CardsInHand.RemoveAt(index);


            gs.LastEvent.SetLastEvent(playedBy, "throw away");

            material.Characters["Suzy Lafayette"].ApplyAbility(gs, playedBy);

            if (material.Characters["Sid Ketchum"].ApplyAbility(gs, playedBy))
            {
                ThrowAwayCardAction(gs, playedBy);
            }
        }
     
        private void EndMoveAction(GameState gs, int playedBy)
        {
            ChangePlayer(gs);
            gs.FirstPartDone = false;
            gs.PlayedBang = false;
            gs.LastEvent.SetLastEvent(playedBy, "EndMove");

        }

        private void ThrowAwayUsedCard(GameState gs,int playedBy, string cardName) {
            int index = gs.Players[playedBy].CardsInHand.IndexOf(cardName);
            gs.DeckOfCards.UsedCards.Add(gs.Players[playedBy].CardsInHand[index]);
            gs.Players[playedBy].CardsInHand.RemoveAt(index);

            material.Characters["Suzy Lafayette"].ApplyAbility(gs, playedBy);

        }

        private void TakeTwoCards(GameState gs, int playedBy)
        {

            material.Characters["Black Jack"].ApplyAbility(gs, playedBy);
            material.Characters["Kit Carlson"].ApplyAbility(gs, playedBy);
            if (!material.Characters["Jesse Jones"].ApplyAbility(gs, playedBy) &&
                !material.Characters["Pedro Ramirez"].ApplyAbility(gs, playedBy)) { 
                for (int i = 0; i < 2; i++)
                {
                    gs.Players[playedBy].CardsInHand.Add(gs.DeckOfCards.UnusedCards[0]);
                    gs.DeckOfCards.UnusedCards.RemoveAt(0);
                }
            }

        }
       
        private string TranslateCardName(GameState gs, int playedBy, string cardNiceName, string handOrTable) {
            if (handOrTable == "hand")
            {
                return gs.Players[playedBy].CardsInHand.FirstOrDefault(s => s.Contains(cardNiceName));
            }
            else {
                return gs.Players[playedBy].CardsOnTable.FirstOrDefault(s => s.Contains(cardNiceName));
            }
           
        }

        private bool HasCard(GameState gs, int player, string card, string handOrTable)
        {
            if (handOrTable == "hand")
            {
                return gs.Players[player].CardsInHand.Exists(s => s.Contains(card));
            }
            else
            {
                return gs.Players[player].CardsOnTable.Exists(s => s.Contains(card));
            }
        }
        
        private void ChangePlayer(GameState gs) {

            gs.CurrentPlayerID = (gs.CurrentPlayerID + 1) % gs.Players.Count;
            while (!gs.Players[gs.CurrentPlayerID].IsAlive)
            {
                gs.CurrentPlayerID = (gs.CurrentPlayerID + 1) % gs.Players.Count;
            }
        }


        
    }
}
