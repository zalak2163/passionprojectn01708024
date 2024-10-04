using passionprojectn01708024.Models;

namespace passionprojectn01708024.Interfaces
{
	public interface IEventAttendeeService
	{
		Task<IEnumerable<EventAttendeeDto>> ListAttendeesForEvent(int eventId);
		Task<EventAttendeeDto?> GetAttendeeRegistration(int eventId, int attendeeId);
		Task<ServiceResponse> RegisterAttendee(int eventId, int attendeeId);
		Task<ServiceResponse> UnregisterAttendee(int eventId, int attendeeId);
	}
}
