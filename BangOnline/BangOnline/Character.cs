using System;
namespace BangOnline
{
    public class Character
    {
        
        public string Name { get; set; }
        public string Ability { get; set; }
        public int Lives { get; set; }

        public Character(string name, string ability, int lives)
        {
            Name = name;
            Ability = ability;
            Lives = lives;
        }

        
        public virtual bool ApplyAbility(GameState gs, int playedBy) {
            return false;
        }

        public virtual bool ApplyAbility(GameState gs, int playedBy, int playedOn)
        {
            return false;
        }

        public virtual bool ApplyAbility(GameState gs, int playedBy, string situation)
        {
            return false;
        }



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

        protected bool HasCard(GameState gs, int player, string card)
        { 
            return gs.Players[player].CardsInHand.Exists(s => s.Contains(card));
        }



    }
}
