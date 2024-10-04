using System.ComponentModel.DataAnnotations;

namespace passionprojectn01708024.Models
{
	public class EventAttendee
	{
		[Key]
		public int EventId { get; set; }
		public virtual Event Event { get; set; }

		public int AttendeeId { get; set; }
		public virtual Attendee Attendee { get; set; }
	}

	public class EventAttendeeDto
	{
		public int EventId { get; set; }
		public int AttendeeId { get; set; }
	}
}
