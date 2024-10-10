using passionprojectn01708024.Data;
using passionprojectn01708024.Interfaces;
using passionprojectn01708024.Models;
using Microsoft.EntityFrameworkCore;

public class EventAttendeeService : IEventAttendeeService
{
	private readonly ApplicationDbContext _context;

	public EventAttendeeService(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<EventAttendeeDto>> ListAttendeesForEvent(int eventId)
	{
		var attendees = await _context.EventAttendees
			.Where(ea => ea.EventId == eventId)
			.Include(ea => ea.Attendee)
			.Select(ea => new EventAttendeeDto
			{
				EventId = ea.EventId,
				AttendeeId = ea.AttendeeId
			})
			.ToListAsync();

		return attendees;
	}

	public async Task<ServiceResponse> RegisterAttendee(EventAttendeeDto attendeeDto)
	{
		var response = new ServiceResponse();

		if (_context.EventAttendees.Any(a => a.EventId == attendeeDto.EventId && a.AttendeeId == attendeeDto.AttendeeId))
		{
			response.Status = ServiceResponse.ServiceStatus.Error;
			response.Messages.Add("Attendee is already registered.");
		}
		else
		{
			var eventAttendee = new EventAttendee
			{
				EventId = attendeeDto.EventId,
				AttendeeId = attendeeDto.AttendeeId
			};
			_context.EventAttendees.Add(eventAttendee);
			await _context.SaveChangesAsync();
			response.Status = ServiceResponse.ServiceStatus.Created;
			response.CreatedId = eventAttendee.EventId; // Or use attendeeId if needed
			response.Messages.Add("Attendee registered successfully.");
		}

		return response;
	}

	public async Task<ServiceResponse> UnregisterAttendee(int eventId, int attendeeId)
	{
		var response = new ServiceResponse();

		var eventAttendee = await _context.EventAttendees
			.FirstOrDefaultAsync(ea => ea.EventId == eventId && ea.AttendeeId == attendeeId);
		if (eventAttendee != null)
		{
			_context.EventAttendees.Remove(eventAttendee);
			await _context.SaveChangesAsync();
			response.Status = ServiceResponse.ServiceStatus.Deleted;
			response.Messages.Add("Attendee unregistered successfully.");
		}
		else
		{
			response.Status = ServiceResponse.ServiceStatus.NotFound;
			response.Messages.Add("Attendee not found.");
		}

		return response;
	}
}
