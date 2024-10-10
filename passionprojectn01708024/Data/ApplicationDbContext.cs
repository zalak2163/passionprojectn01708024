using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using passionprojectn01708024.Models;

namespace passionprojectn01708024.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public DbSet<Event> Events { get; set; }
		public DbSet<Attendee> Attendees { get; set; }
		public DbSet<Location> Locations { get; set; }
		public DbSet<EventAttendee> EventAttendees { get; set; }
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<EventAttendee>()
				.HasKey(ea => new { ea.EventId, ea.AttendeeId });

			modelBuilder.Entity<EventAttendee>()
				.HasOne(ea => ea.Event)
				.WithMany(e => e.EventAttendee)
				.HasForeignKey(ea => ea.EventId);

			modelBuilder.Entity<EventAttendee>()
				.HasOne(ea => ea.Attendee)
				.WithMany(a => a.EventAttendees)
				.HasForeignKey(ea => ea.AttendeeId);
		}
	}
}
