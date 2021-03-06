using System;
using System.Collections.Generic;
using System.Text;

namespace EventSystem.ApplicationCore.Dtos
{
    public class UserViewModel : UserShortViewModel
    {
        public List<EventShortViewModel> OrganizedEvents { get; set; }
        public List<EventShortViewModel> ConnectedEvents { get; set; }
    }
}
