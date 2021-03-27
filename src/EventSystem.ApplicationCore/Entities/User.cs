using System;
using System.Collections.Generic;
using System.Text;

namespace EventSystem.ApplicationCore.Entities
{
    public class User
    {
        public string IdentityUserId { get; set; }
        public List<Event> OrganizedEvents { get; set; }
        public List<EventParticipant> ConnectedEvents{ get; set; }
    }
}
