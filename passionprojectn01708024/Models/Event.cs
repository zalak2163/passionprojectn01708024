using System.ComponentModel.DataAnnotations;

namespace passionprojectn01708024.Models
{
	public class Event
	{
		[Key]
		public int EventId { get; set; }

		public string EventName { get; set; } = string.Empty; // Default to empty string
		public DateTime Date { get; set; }
		public string Description { get; set; } = string.Empty; // Default to empty string

		// Navigation property for events
		public ICollection<EventAttendee>? EventAttendees { get; set; } = new List<EventAttendee>(); // Initialize to an empty list

		public int LocationId { get; set; }
		public virtual Location Location { get; set; } = new Location(); // Initialize to a new Location instance
	}

	public class EventDto
	{
		public int EventId { get; set; }
		public string EventName { get; set; } = string.Empty; // Default to empty string
		public DateTime Date { get; set; }
		public string Description { get; set; } = string.Empty; // Default to empty string
		public int LocationId { get; set; }
	}
}
