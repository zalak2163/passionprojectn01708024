using System.ComponentModel.DataAnnotations;

namespace passionprojectn01708024.Models
{
	public class EventAttendee
	{
		public int EventId { get; set; }
		public Event Event { get; set; }

		public int AttendeeId { get; set; }
		public Attendee Attendee { get; set; }
	}



	public class EventAttendeeDto
		{
			public int EventId { get; set; }
			public int AttendeeId { get; set; }
			
		}
	}


