using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using passionprojectn01708024.Data;
using passionprojectn01708024.Interfaces;
using passionprojectn01708024.Models;

namespace passionprojectn01708024.Services
{
	public class AttendeeService : IAttendeeService
	{
		private readonly ApplicationDbContext _context;

		public AttendeeService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<AttendeeDto>> GetAttendeesAsync()
		{
			return await _context.Attendees
				.Select(a => new AttendeeDto
				{
					AttendeeId = a.AttendeeId,
					Name = a.Name,
					Email = a.Email
				}).ToListAsync();
		}

		public async Task<Attendee?> GetAttendeeAsync(int id)
		{
			return await _context.Attendees.FindAsync(id);
		}

		public async Task<ServiceResponse> AddOrUpdateAttendeeAsync(Attendee attendee)
		{
			var serviceResponse = new ServiceResponse();

			if (attendee.AttendeeId == 0)
			{
				_context.Attendees.Add(attendee);
				serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
			}
			else
			{
				_context.Entry(attendee).State = EntityState.Modified;
				serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
			}

			await _context.SaveChangesAsync();
			serviceResponse.CreatedId = attendee.AttendeeId;
			return serviceResponse;
		}

		public async Task<ServiceResponse> DeleteAttendeeAsync(int id)
		{
			var serviceResponse = new ServiceResponse();
			var attendee = await _context.Attendees.FindAsync(id);

			if (attendee == null)
			{
				serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
				return serviceResponse;
			}

			_context.Attendees.Remove(attendee);
			await _context.SaveChangesAsync();
			serviceResponse.Status = ServiceResponse.ServiceStatus.Deleted;
			return serviceResponse;
		}

		public async Task<ServiceResponse> RegisterAttendee(int attendeeId, int eventId)
		{
			var serviceResponse = new ServiceResponse();
			var attendee = await _context.Attendees.FindAsync(attendeeId);
			var eventEntity = await _context.Events.FindAsync(eventId);

			if (attendee == null || eventEntity == null)
			{
				serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
				return serviceResponse;
			}

			var eventAttendee = new EventAttendee { EventId = eventId, AttendeeId = attendeeId };
			_context.EventAttendees.Add(eventAttendee);
			await _context.SaveChangesAsync();
			serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
			return serviceResponse;
		}

		public async Task<ServiceResponse> UnregisterAttendee(int attendeeId, int eventId)
		{
			var serviceResponse = new ServiceResponse();
			var eventAttendee = await _context.EventAttendees
				.FirstOrDefaultAsync(ea => ea.AttendeeId == attendeeId && ea.EventId == eventId);

			if (eventAttendee == null)
			{
				serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
				return serviceResponse;
			}

			_context.EventAttendees.Remove(eventAttendee);
			await _context.SaveChangesAsync();
			serviceResponse.Status = ServiceResponse.ServiceStatus.Deleted;
			return serviceResponse;
		}
	}
}
