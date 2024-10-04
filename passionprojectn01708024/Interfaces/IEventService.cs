using passionprojectn01708024.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace passionprojectn01708024.Interfaces
{
	public interface IEventService
	{
		// Base CRUD
		Task<IEnumerable<EventDto>> ListEvents();
		Task<EventDto?> FindEvent(int id);
		Task<ServiceResponse> UpdateEvent(EventDto eventDto);
		Task<ServiceResponse> AddEvent(EventDto eventDto);
		Task<ServiceResponse> DeleteEvent(int id);

		// Related methods
		Task<IEnumerable<EventDto>> ListEventsForAttendee(int attendeeId);
		Task<ServiceResponse> RegisterAttendeeToEvent(int eventId, int attendeeId);
		Task<ServiceResponse> UnregisterAttendeeFromEvent(int eventId, int attendeeId);
	}
}
