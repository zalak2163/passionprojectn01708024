using Microsoft.AspNetCore.Mvc;
using passionprojectn01708024.Interfaces;
using passionprojectn01708024.Models;

namespace passionprojectn01708024.Controllers
{
	[Route("api/events/{eventId}/attendees")]
	[ApiController]
	public class EventAttendeeController : ControllerBase
	{
		private readonly IEventAttendeeService _eventAttendeeService;

		public EventAttendeeController(IEventAttendeeService eventAttendeeService)
		{
			_eventAttendeeService = eventAttendeeService;
		}

		// POST: api/events/{eventId}/attendees/{attendeeId}/register
		[HttpPost("{attendeeId}/register")]
		public async Task<ActionResult> RegisterAttendee(int eventId, int attendeeId)
		{
			var serviceResponse = await _eventAttendeeService.RegisterAttendee(eventId, attendeeId);
			if (serviceResponse.Status == ServiceResponse.ServiceStatus.NotFound)
			{
				return NotFound();
			}

			return CreatedAtAction(nameof(RegisterAttendee), new { eventId, attendeeId });
		}

		// POST: api/events/{eventId}/attendees/{attendeeId}/unregister
		[HttpPost("{attendeeId}/unregister")]
		public async Task<IActionResult> UnregisterAttendee(int eventId, int attendeeId)
		{
			var serviceResponse = await _eventAttendeeService.UnregisterAttendee(eventId, attendeeId);
			if (serviceResponse.Status == ServiceResponse.ServiceStatus.NotFound)
			{
				return NotFound();
			}

			return NoContent();
		}
	}
}
