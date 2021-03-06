using System;
using System.Collections.Generic;

namespace EventSystem.ApplicationCore.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public User Organizer { get; set; }
        public List<EventParticipant> Participants { get; set; }
    }
}
