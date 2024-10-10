using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace passionprojectn01708024.Models
{
	public class Event
	{
		public int EventId { get; set; }
		public string EventName { get; set; } = string.Empty; 
		public DateTime Date { get; set; }
		public string Description { get; set; } = string.Empty;
		public int LocationId { get; set; }
		public virtual Location Location { get; set; } = null!; 
		public virtual ICollection<EventAttendee> Attendees { get; set; } = new List<EventAttendee>();

	}


	public class EventDto
	{
		public int EventId { get; set; }
		public string EventName { get; set; }
		public DateTime Date { get; set; }
		public string Description { get; set; }
		public int LocationId { get; set; }
		public List<int> AttendeeIds { get; set; }
	}
}
