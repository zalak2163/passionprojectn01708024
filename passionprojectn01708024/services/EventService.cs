using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using passionprojectn01708024.Data;
using passionprojectn01708024.Models;
using passionprojectn01708024.Services;


namespace EventManagementSystem.Services
{
	public class EventService
	{
		private readonly ApplicationDbContext _context;

		public EventService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
		{
			return await _context.Events
				.Include(e => e.Location)
				.Include(e => e.Attendees)
				.Select(e => new EventDto
				{
					EventId = e.EventId,
					EventName = e.EventName,
					Date = e.Date,
					Description = e.Description,
					LocationId = e.LocationId,
					AttendeeIds = e.Attendees.Select(a => a.AttendeeId).ToList()
				}).ToListAsync();
		}

		public async Task<EventDto> GetEventByIdAsync(int id)
		{
			var eventEntity = await _context.Events
				.Include(e => e.Location)
				.Include(e => e.Attendees)
				.ThenInclude(a => a.Attendee)
				.FirstOrDefaultAsync(e => e.EventId == id);

			if (eventEntity == null) return null;

			return new EventDto
			{
				EventId = eventEntity.EventId,
				EventName = eventEntity.EventName,
				Date = eventEntity.Date,
				Description = eventEntity.Description,
				LocationId = eventEntity.LocationId,
				AttendeeIds = eventEntity.Attendees.Select(a => a.AttendeeId).ToList()
			};
		}

		public async Task CreateEventAsync(EventDto newEventDto)
		{
			var newEvent = new Event
			{
				EventName = newEventDto.EventName,
				Date = newEventDto.Date,
				Description = newEventDto.Description,
				LocationId = newEventDto.LocationId,
				Attendees = new List<EventAttendee>()
			};

			foreach (var attendeeId in newEventDto.AttendeeIds)
			{
				newEvent.Attendees.Add(new EventAttendee { AttendeeId = attendeeId });
			}

			_context.Events.Add(newEvent);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateEventAsync(EventDto updatedEventDto)
		{
			var eventEntity = await _context.Events
				.Include(e => e.Attendees)
				.FirstOrDefaultAsync(e => e.EventId == updatedEventDto.EventId);

			if (eventEntity == null) return;

			eventEntity.EventName = updatedEventDto.EventName;
			eventEntity.Date = updatedEventDto.Date;
			eventEntity.Description = updatedEventDto.Description;
			eventEntity.LocationId = updatedEventDto.LocationId;

			// Update attendees
			eventEntity.Attendees.Clear();
			foreach (var attendeeId in updatedEventDto.AttendeeIds)
			{
				eventEntity.Attendees.Add(new EventAttendee { AttendeeId = attendeeId });
			}

			await _context.SaveChangesAsync();
		}

		public async Task DeleteEventAsync(int id)
		{
			var eventToDelete = await _context.Events.FindAsync(id);
			if (eventToDelete != null)
			{
				_context.Events.Remove(eventToDelete);
				await _context.SaveChangesAsync();
			}
		}
	}
}
