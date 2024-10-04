using passionprojectn01708024.Interfaces;
using passionprojectn01708024.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace passionprojectn01708024.Services
{
	public class EventAttendeeService : IEventAttendeeService
	{
		private readonly List<EventAttendeeDto> _attendees = new();

		public async Task<IEnumerable<EventAttendeeDto>> ListAttendeesForEvent(int eventId)
		{
			return await Task.FromResult(_attendees.Where(a => a.EventId == eventId).ToList());
		}

		public async Task<EventAttendeeDto?> GetAttendeeRegistration(int eventId, int attendeeId)
		{
			return await Task.FromResult(_attendees.FirstOrDefault(a => a.EventId == eventId && a.AttendeeId == attendeeId));
		}

		public async Task<ServiceResponse> RegisterAttendee(int eventId, int attendeeId)
		{
			var response = new ServiceResponse();

			// Example registration logic
			if (_attendees.Any(a => a.EventId == eventId && a.AttendeeId == attendeeId))
			{
				response.Status = ServiceResponse.ServiceStatus.Error;
				response.Messages.Add("Attendee is already registered.");
			}
			else
			{
				_attendees.Add(new EventAttendeeDto { EventId = eventId, AttendeeId = attendeeId });
				response.Status = ServiceResponse.ServiceStatus.Created;
				response.CreatedId = attendeeId; // Assuming attendeeId represents the created attendee
				response.Messages.Add("Attendee registered successfully.");
			}

			return await Task.FromResult(response);
		}

		public async Task<ServiceResponse> UnregisterAttendee(int eventId, int attendeeId)
		{
			var response = new ServiceResponse();

			// Example unregistration logic
			var attendee = _attendees.FirstOrDefault(a => a.EventId == eventId && a.AttendeeId == attendeeId);
			if (attendee != null)
			{
				_attendees.Remove(attendee);
				response.Status = ServiceResponse.ServiceStatus.Deleted;
				response.Messages.Add("Attendee unregistered successfully.");
			}
			else
			{
				response.Status = ServiceResponse.ServiceStatus.NotFound;
				response.Messages.Add("Attendee not found.");
			}

			return await Task.FromResult(response);
		}
	}
}
