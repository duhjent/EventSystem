using System;
using System.Collections.Generic;

namespace EventSystem.ApplicationCore.Entities
{
    public class Event : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string OrganizerId { get; set; }
        public User Organizer { get; set; }
        public List<EventParticipant> Participants { get; set; }
    }
}
