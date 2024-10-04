using passionprojectn01708024.Models;

namespace passionprojectn01708024.Interfaces
{
	public interface IAttendeeService
	{
		Task<IEnumerable<Attendee>> GetAttendeesAsync();
		Task<Attendee?> GetAttendeeAsync(int id);
		Task<ServiceResponse> AddOrUpdateAttendeeAsync(Attendee attendee);

		Task<ServiceResponse> DeleteAttendeeAsync(int id);
		Task<ServiceResponse> RegisterForEventAsync(int attendeeId, int eventId);
		Task<ServiceResponse> UnregisterFromEventAsync(int attendeeId, int eventId);

	}
}
