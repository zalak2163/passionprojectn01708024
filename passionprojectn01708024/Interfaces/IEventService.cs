using System.Collections.Generic;
using System.Threading.Tasks;
using passionprojectn01708024.Models;

namespace passionprojectn01708024.Interfaces
{
	public interface IEventService
	{
		Task<IEnumerable<EventDto>> ListEvents();
		Task<EventDto?> FindEvent(int id);
		Task<ServiceResponse> UpdateEvent(EventDto eventDto);
		Task<ServiceResponse> AddEvent(EventDto eventDto);
		Task<ServiceResponse> DeleteEvent(int id);
		Task<IEnumerable<EventDto>> ListEventsForAttendee(int attendeeId);
		Task<ServiceResponse> RegisterAttendeeToEvent(int eventId, int attendeeId);
		Task<ServiceResponse> UnregisterAttendeeFromEvent(int eventId, int attendeeId);
	}
}
