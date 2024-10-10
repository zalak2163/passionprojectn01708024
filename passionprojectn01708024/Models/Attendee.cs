using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace passionprojectn01708024.Models
{
	public class Attendee
	{

		[Key]
		public int AttendeeId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }

		// Navigation property for many-to-many relationship
		public virtual ICollection<EventAttendee> EventAttendees { get; set; }
	}

	public class AttendeeDto
	{
		public int AttendeeId { get; set; }
		public string Name { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;
	}

}
