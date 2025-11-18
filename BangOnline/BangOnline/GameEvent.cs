using System;
namespace BangOnline
{
    public class GameEvent
    {
        public int PlayedBy { get; set; }
        public string Event { get; set; } = "";
        public int PlayedOn { get; set; }
       

        public GameEvent()
        {
            
        }

        public void SetLastEvent(int PlayedBy, string Event, int PlayedOn = -1) {
            this.PlayedBy = PlayedBy;
            this.Event = Event;
            this.PlayedOn = PlayedOn;
        }
    }
}
