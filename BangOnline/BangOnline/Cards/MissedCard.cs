using System;
namespace BangOnline
{
    public class MissedCard : Card
    {
        public MissedCard()
        {
        }

        public override void Action(GameState gs, int playedBy, int playedOn)
        {
            if (HasCard(gs, playedBy, "Barrel", "table") && blueCardsExecutor.ApplyBarrelEffect(gs))
            {

                gs.LastEvent.SetLastEvent(playedBy, "Missed(successful)", playedOn);
            }
            else if (material.Characters["Jourdonnais"].ApplyAbility(gs, playedBy))
            {
                gs.LastEvent.SetLastEvent(playedBy, "Missed(successful)", playedOn);
            }
            else
            {
                Console.WriteLine("Use Missed card or lose one life point.(Missed/LoseLife)");
                string move = Console.ReadLine();
                while ((move != "Missed" && move != "LoseLife") ||
                    (move == "Missed" && !gs.Players[playedBy].CardsInHand.Contains(TranslateCardName(gs, playedBy, "Missed", "hand")))

                )
                {
                    Console.WriteLine("Invalid input or no Missed card in hand.");
                    move = Console.ReadLine();
                }
                if (move == "Missed")
                {
                    string card = TranslateCardName(gs, playedBy, "Missed", "hand");

                    ThrowAwayUsedCard(gs, playedBy, card);
                    gs.LastEvent.SetLastEvent(playedBy, "Missed(successful)", playedOn);

                }
                else
                {
                    gs.Players[playedBy].Lives--;
                    material.Characters["Bart Cassidy"].ApplyAbility(gs, playedBy);
                    material.Characters["El Gringo"].ApplyAbility(gs, playedBy, playedOn);
                    gs.LastEvent.SetLastEvent(playedBy, "Missed(unsuccessful)", playedOn);
                    if (gs.Players[playedBy].Lives <= 0)
                    {
                        Die(gs, playedBy, playedOn);
                    }
                }
            }


            

            gs.CurrentPlayerID = playedOn;
        }

        public override bool Validate(GameState gs, GameEvent move)
        {
            return ValidatePossesion(gs, move);
        }
    }
}
