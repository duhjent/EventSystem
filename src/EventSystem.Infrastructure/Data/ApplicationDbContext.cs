using EventSystem.ApplicationCore.Entities;
using EventSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventSystem.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<User> DomainUsers { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasOne(au => au.DomainUser)
                .WithOne()
                .HasForeignKey<User>(du => du.IdentityUserId);

            builder.Entity<EventParticipant>()
                .HasKey(ep => new { ep.EventId, ep.UserId });

            builder.Entity<Event>()
                .HasMany(e => e.Participants)
                .WithOne(ep => ep.Event)
                .HasForeignKey(ep => ep.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasKey(u => u.IdentityUserId);

            builder.Entity<User>()
                .HasMany(du => du.ConnectedEvents)
                .WithOne(ep => ep.User)
                .HasForeignKey(ep => ep.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Event>()
                .HasOne(e => e.Organizer)
                .WithMany(du => du.OrganizedEvents)
                .HasForeignKey(e => e.OrganizerId);
        }
    }
}
