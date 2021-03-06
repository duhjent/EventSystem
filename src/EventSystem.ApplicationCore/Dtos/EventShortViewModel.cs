﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EventSystem.ApplicationCore.Dtos
{
    public class EventShortViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
    }
}
