using Microsoft.EntityFrameworkCore;
using passionprojectn01708024.Data;
using passionprojectn01708024.Interfaces;
using passionprojectn01708024.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace passionprojectn01708024.Services
{
	public class EventService : IEventService
	{
		private readonly ApplicationDbContext _context;

		public EventService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<EventDto>> ListEvents()
		{
			var events = await _context.Events
				.Include(e => e.Location)
				.Include(e => e.EventAttendees)
				.ToListAsync();

			return events.Select(e => new EventDto
			{
				EventId = e.EventId,
				EventName = e.EventName,
				Date = e.Date,
				Description = e.Description,
				LocationId = e.LocationId,
				// Add other necessary properties from Location and EventAttendees as needed
			}).ToList();
		}

		public async Task<EventDto?> FindEvent(int id)
		{
			var eventEntity = await _context.Events
				.Include(e => e.Location)
				.Include(e => e.EventAttendees)
				.FirstOrDefaultAsync(e => e.EventId == id);

			if (eventEntity == null)
			{
				return null;
			}

			return new EventDto
			{
				EventId = eventEntity.EventId,
				EventName = eventEntity.EventName,
				Date = eventEntity.Date,
				Description = eventEntity.Description,
				LocationId = eventEntity.LocationId,
				// Add other necessary properties as needed
			};
		}

		public async Task<ServiceResponse> UpdateEvent(EventDto eventDto)
		{
			var serviceResponse = new ServiceResponse();

			var eventEntity = new Event
			{
				EventId = eventDto.EventId,
				EventName = eventDto.EventName,
				Date = eventDto.Date,
				Description = eventDto.Description,
				LocationId = eventDto.LocationId,
				// Map other necessary properties as needed
			};

			_context.Entry(eventEntity).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
				serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
			}
			catch (DbUpdateConcurrencyException)
			{
				serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
				serviceResponse.Messages.Add("An error occurred updating the record.");
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse> AddEvent(EventDto eventDto)
		{
			var serviceResponse = new ServiceResponse();

			var eventEntity = new Event
			{
				EventName = eventDto.EventName,
				Date = eventDto.Date,
				Description = eventDto.Description,
				LocationId = eventDto.LocationId,
				// Map other necessary properties as needed
			};

			try
			{
				_context.Events.Add(eventEntity);
				await _context.SaveChangesAsync();
				serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
				serviceResponse.CreatedId = eventEntity.EventId;
			}
			catch (Exception ex)
			{
				serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
				serviceResponse.Messages.Add("There was an error adding the event.");
				serviceResponse.Messages.Add(ex.Message);
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse> DeleteEvent(int id)
		{
			var serviceResponse = new ServiceResponse();
			var eventEntity = await _context.Events.FindAsync(id);

			if (eventEntity == null)
			{
				serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
				serviceResponse.Messages.Add("Event cannot be deleted because it does not exist.");
				return serviceResponse;
			}

			try
			{
				_context.Events.Remove(eventEntity);
				await _context.SaveChangesAsync();
				serviceResponse.Status = ServiceResponse.ServiceStatus.Deleted;
			}
			catch (Exception ex)
			{
				serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
				serviceResponse.Messages.Add("Error encountered while deleting the event.");
				serviceResponse.Messages.Add(ex.Message);
			}

			return serviceResponse;
		}

		public async Task<IEnumerable<EventDto>> ListEventsForAttendee(int attendeeId)
		{
			var events = await _context.Events
				.Where(e => e.EventAttendees.Any(a => a.AttendeeId == attendeeId))
				.ToListAsync();

			return events.Select(e => new EventDto
			{
				EventId = e.EventId,
				EventName = e.EventName,
				Date = e.Date,
				Description = e.Description,
				LocationId = e.LocationId,
				// Add other necessary properties as needed
			}).ToList();
		}

		public async Task<ServiceResponse> RegisterAttendeeToEvent(int eventId, int attendeeId)
		{
			var serviceResponse = new ServiceResponse();

			var eventEntity = await _context.Events
				.Include(e => e.EventAttendees)
				.FirstOrDefaultAsync(e => e.EventId == eventId);
			var attendee = await _context.Attendees.FindAsync(attendeeId);

			if (eventEntity == null || attendee == null)
			{
				serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
				if (eventEntity == null) serviceResponse.Messages.Add("Event not found.");
				if (attendee == null) serviceResponse.Messages.Add("Attendee not found.");
				return serviceResponse;
			}

			try
			{
				eventEntity.EventAttendees.Add(new EventAttendee { EventId = eventId, AttendeeId = attendeeId });
				await _context.SaveChangesAsync();
				serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
			}
			catch (Exception ex)
			{
				serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
				serviceResponse.Messages.Add("There was an issue registering the attendee to the event.");
				serviceResponse.Messages.Add(ex.Message);
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse> UnregisterAttendeeFromEvent(int eventId, int attendeeId)
		{
			var serviceResponse = new ServiceResponse();

			var eventEntity = await _context.Events
				.Include(e => e.EventAttendees)
				.FirstOrDefaultAsync(e => e.EventId == eventId);
			var attendee = await _context.Attendees.FindAsync(attendeeId);

			if (eventEntity == null || attendee == null)
			{
				serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
				if (eventEntity == null) serviceResponse.Messages.Add("Event not found.");
				if (attendee == null) serviceResponse.Messages.Add("Attendee not found.");
				return serviceResponse;
			}

			try
			{
				var eventAttendee = eventEntity.EventAttendees.FirstOrDefault(a => a.AttendeeId == attendeeId);
				if (eventAttendee != null)
				{
					eventEntity.EventAttendees.Remove(eventAttendee);
					await _context.SaveChangesAsync();
					serviceResponse.Status = ServiceResponse.ServiceStatus.Deleted;
				}
			}
			catch (Exception ex)
			{
				serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
				serviceResponse.Messages.Add("There was an issue unregistering the attendee from the event.");
				serviceResponse.Messages.Add(ex.Message);
			}

			return serviceResponse;
		}
	}
}
