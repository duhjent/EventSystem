using EventSystem.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSystem.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public User DomainUser { get; set; }
    }
}
