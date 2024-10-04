using Microsoft.AspNetCore.Mvc;
using passionprojectn01708024.Interfaces;
using passionprojectn01708024.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace passionprojectn01708024.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EventController : ControllerBase
	{
		private readonly IEventService _eventService;

		public EventController(IEventService eventService)
		{
			_eventService = eventService;
		}

		// GET: api/event
		[HttpGet]
		public async Task<ActionResult<IEnumerable<EventDto>>> GetEvents()
		{
			var events = await _eventService.ListEvents();
			return Ok(events);
		}

		// GET: api/event/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<EventDto>> GetEvent(int id)
		{
			var eventDto = await _eventService.FindEvent(id);
			if (eventDto == null)
			{
				return NotFound();
			}

			return Ok(eventDto);
		}

		// PUT: api/event
		[HttpPut]
		public async Task<ActionResult> UpdateEvent([FromBody] EventDto eventDto)
		{
			var serviceResponse = await _eventService.UpdateEvent(eventDto);
			if (serviceResponse.Status == ServiceResponse.ServiceStatus.Error)
			{
				return BadRequest(serviceResponse.Messages);
			}
			return NoContent();
		}

		// DELETE: api/event/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteEvent(int id)
		{
			var serviceResponse = await _eventService.DeleteEvent(id);
			if (serviceResponse.Status == ServiceResponse.ServiceStatus.NotFound)
			{
				return NotFound();
			}

			return NoContent();
		}

		// POST: api/event/{eventId}/attendees/{attendeeId}/register
		[HttpPost("{eventId}/attendees/{attendeeId}/register")]
		public async Task<IActionResult> RegisterAttendee(int eventId, int attendeeId)
		{
			var serviceResponse = await _eventService.RegisterAttendeeToEvent(eventId, attendeeId);
			if (serviceResponse.Status == ServiceResponse.ServiceStatus.Created)
			{
				return NoContent();
			}
			return NotFound();
		}

		// POST: api/event/{eventId}/attendees/{attendeeId}/unregister
		[HttpPost("{eventId}/attendees/{attendeeId}/unregister")]
		public async Task<IActionResult> UnregisterAttendee(int eventId, int attendeeId)
		{
			var serviceResponse = await _eventService.UnregisterAttendeeFromEvent(eventId, attendeeId);
			if (serviceResponse.Status == ServiceResponse.ServiceStatus.Deleted)
			{
				return NoContent();
			}
			return NotFound();
		}
	}
}
