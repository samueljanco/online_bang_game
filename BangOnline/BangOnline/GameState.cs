using System;
using System.Collections.Generic;

namespace BangOnline
{
    public class GameState
    {


        public Dictionary<int, PlayerInfo> Players { get; set; } = new Dictionary<int, PlayerInfo>();
        public int CurrentPlayerID { get; set; } = 0;
        public bool FirstPartDone { get; set; } = false;
        public bool PlayedBang { get; set; } = false;
        public GameEvent LastEvent { get; set; } = new GameEvent();
        public DeckOfCards DeckOfCards { get; set; } = new DeckOfCards();

    }
}
