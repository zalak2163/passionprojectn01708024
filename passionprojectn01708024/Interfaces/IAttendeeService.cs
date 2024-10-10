using System.Collections.Generic;
using System.Threading.Tasks;
using passionprojectn01708024.Models;

namespace passionprojectn01708024.Interfaces
{
	public interface IAttendeeService
	{
		Task<IEnumerable<AttendeeDto>> GetAttendeesAsync();
		Task<Attendee?> GetAttendeeAsync(int id);
		Task<ServiceResponse> AddOrUpdateAttendeeAsync(Attendee attendee);
		Task<ServiceResponse> DeleteAttendeeAsync(int id);
		Task<ServiceResponse> RegisterAttendee(int attendeeId, int eventId);
		Task<ServiceResponse> UnregisterAttendee(int attendeeId, int eventId);
	}
}
