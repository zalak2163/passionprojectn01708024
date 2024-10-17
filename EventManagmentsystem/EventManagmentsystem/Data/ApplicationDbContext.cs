using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EventManagmentsystem.Models;

namespace EventManagmentsystem.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<Event> Events { get; set; }
		public DbSet<Location> Locations { get; set; }
		public DbSet<Attendee> Attendees { get; set; }
		public object EventAttendees { get; internal set; }
	}

}
