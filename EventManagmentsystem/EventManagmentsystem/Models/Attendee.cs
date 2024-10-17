using System.ComponentModel.DataAnnotations;

namespace EventManagmentsystem.Models
{
	/// <summary>
	/// Represents an attendee in the event management system.
	/// </summary>
	public class Attendee
	{
		[Key]
		public int AttendeeId { get; set; }

		/// <summary>
		/// Gets or sets the name of the attendee.
		/// </summary>
		public string AttendeeName { get; set; }

		/// <summary>
		/// Gets or sets the email of the attendee.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the collection of events associated with the attendee.
		/// </summary>
		public virtual ICollection<Event> Events { get; set; } 
	}

	/// <summary>
	/// Data Transfer Object for attendee information.
	/// </summary>
	public class AttendeeDto
	{
		public int AttendeeId { get; set; }

		/// <summary>
		/// Gets or sets the name of the attendee.
		/// </summary>
		public string AttendeeName { get; set; }

		/// <summary>
		/// Gets or sets the email of the attendee.
		/// </summary>
		public string Email { get; set; }
	}
}
