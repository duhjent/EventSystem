using EventSystem.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSystem.Infrastructure.Identity
{
    class ApplicationUser : IdentityUser
    {
        public int DomainUserId { get; set; }
        public User DomainUser { get; set; }
    }
}
