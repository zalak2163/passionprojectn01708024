using System.ComponentModel.DataAnnotations;

namespace passionprojectn01708024.Models
{
	public class Attendee
	{
		[Key]
		public int AttendeeId { get; set; }
		public string AttendeeName { get; set; }
		public string AttendeeEmail { get; set; }

		// Navigation property for events
		public ICollection<EventAttendee>? EventAttendees { get; set; }
	}

	public class AttendeeDto
	{
		public int AttendeeId { get; set; }
		public string AttendeeName { get; set; }
		public string AttendeeEmail { get; set; }
	}
}
