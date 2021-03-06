using System;
using System.Collections.Generic;

namespace EventSystem.ApplicationCore.Dtos
{
    public class EventViewModel : EventShortViewModel
    {
        public UserShortViewModel Organizer { get; set; }
        public List<UserShortViewModel> Participants { get; set; }
    }
}
