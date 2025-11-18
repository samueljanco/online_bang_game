using System;
using System.Collections.Generic;
using System.Linq;
namespace BangOnline
{
    public class CardList
    {
        public Dictionary<string, Card> Cards = new Dictionary<string, Card>
        {
            {"Beer", new BeerCard() },
            {"Cat_Balou", new CatBalouCard()},
            {"Bang", new BangCard() },
            {"Missed", new MissedCard()},
            {"Barrel", new BarrelCard()},
            {"Duel", new DuelCard() },
            {"Dynamite", new DynamiteCard()},
            {"Emporio", new EmporioCard() },
            {"Gatling",new GatlingCard()},
            {"Indians", new IndiansCard()},
            {"Jail", new JailCard() },
            {"Mustang", new MustangCard() },
            {"Panic", new PanicCard()},
            {"Remington", new RemingtonCard() },
            {"Rev_Carabine", new RevCarabineCard() },
            {"Salon", new SalonCard() },
            {"Schofield", new SchofieldCard() },
            {"Scope", new ScopeCard() },
            {"Stagecoach", new StagecoachCard()},
            {"Volcanic", new VolcanicCard() },
            {"Wells_Fargo", new WellsFargoCard() },
            {"Winchester", new WinchesterCard() }


        };
    }
}