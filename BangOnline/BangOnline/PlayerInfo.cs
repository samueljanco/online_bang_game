using System;
using System.Collections.Generic;
namespace BangOnline
{
    public class PlayerInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string ChosenCharacter { get; set; }
        public List<string> Characters { get; set; }
        public int Lives { get; set; }
        public List<string> CardsOnTable { get; set; } = new List<string>();
        public List<string> CardsInHand { get; set; } = new List<string>();
        public bool IsAlive { get; set; }
    }
}
