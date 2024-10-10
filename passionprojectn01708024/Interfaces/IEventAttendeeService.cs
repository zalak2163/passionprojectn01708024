using System.Collections.Generic;
using System.Threading.Tasks;
using passionprojectn01708024.Models;

namespace passionprojectn01708024.Interfaces
{
	public interface IEventAttendeeService
	{
		Task<IEnumerable<EventAttendeeDto>> ListAttendeesForEvent(int eventId);
		Task<ServiceResponse> RegisterAttendee(EventAttendeeDto attendeeDto);
		Task<ServiceResponse> UnregisterAttendee(int eventId, int attendeeId);
	}
}
