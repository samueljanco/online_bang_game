using System;
using System.Collections.Generic;
using System.Linq;
namespace BangOnline
{
    public class MoveValidator
    {
       
        private CardList cardList = new CardList();
        public MoveValidator()
        {
        }

        public bool ValidateMove(GameState gs, GameEvent move) {

            switch (move.Event)
            {
                
                case "Bang":
                    return cardList.Cards["Bang"].Validate(gs,move);
                case "Panic":
                    return cardList.Cards["Panic"].Validate(gs, move);
                case "Beer":
                    return cardList.Cards["Beer"].Validate(gs, move);
                case "Stagecoach":
                    return cardList.Cards["Stagecoach"].Validate(gs, move);
                case "WellsFargo":
                    return cardList.Cards["Wells_Fargo"].Validate(gs, move);
                case "Emporio":
                    return cardList.Cards["Emporio"].Validate(gs, move);
                case "Salon":
                    return cardList.Cards["Salon"].Validate(gs, move);
                case "Indians":
                    return cardList.Cards["Indians"].Validate(gs, move);
                case "Gatling":
                    return cardList.Cards["Gatling"].Validate(gs, move);
                case "Missed":
                    return cardList.Cards["Missed"].Validate(gs, move);
                case "Mustang":
                    return cardList.Cards["Mustang"].Validate(gs, move);
                case "Scope":
                    return cardList.Cards["Scope"].Validate(gs, move);
                case "Barrel":
                    return cardList.Cards["Barrel"].Validate(gs, move);
                case "Dynamite":
                    return cardList.Cards["Dynamite"].Validate(gs, move);
                case "Volcanic":
                    return cardList.Cards["Volcanic"].Validate(gs, move);
                case "Schofield":
                    return cardList.Cards["Schofield"].Validate(gs, move);
                case "Remington":
                    return cardList.Cards["Remington"].Validate(gs, move);
                case "RevCarabine":
                    return cardList.Cards["Rev_Carabine"].Validate(gs, move);
                case "Winchester":
                    return cardList.Cards["Winchester"].Validate(gs, move);
                case "CatBalou":
                    return cardList.Cards["Cat_Balou"].Validate(gs, move);
                case "Duel":
                    return cardList.Cards["Duel"].Validate(gs, move);
                case "Jail":
                    return cardList.Cards["Jail"].Validate(gs, move);
                case "ThrowAway":
                    return true;
                case "TakeTwoCards":
                    return true;
                case "EndMove":
                    if (gs.Players[move.PlayedBy].CardsInHand.Count <= gs.Players[move.PlayedBy].Lives)
                    {
                        return true;
                    }
                    else{
                        Console.WriteLine("You can have at maximum as many card as you have lives.");
                        return false;
                    }
                    
                default:
                    Console.WriteLine("Unknown action!");
                    break;
            }

            return false;
        }

      
        

    }
}
