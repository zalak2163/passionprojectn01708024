using System.ComponentModel.DataAnnotations;

namespace passionprojectn01708024.Models
{
	public class Location
	{
		[Key]
		public int LocationId { get; set; }
		public string LocationName { get; set; }
		public string Address { get; set; }
		public int Capacity { get; set; }

		// Navigation property for events held at the location
		public virtual ICollection<Event> Events { get; set; }
	}

	public class LocationDto
	{
		public int LocationId { get; set; }
		public string LocationName { get; set; }
		public string Address { get; set; }
		public int Capacity { get; set; }
	}
}
