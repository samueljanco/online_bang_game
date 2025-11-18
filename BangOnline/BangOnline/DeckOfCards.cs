using System;
using System.Collections.Generic;
namespace BangOnline
{
    public class DeckOfCards
    {

        public List<string> UsedCards { get; set; } = new List<string>();
        public List<string> UnusedCards { get; set; } = new List<string>();
        public DeckOfCards()
        {
        }
    }
}
