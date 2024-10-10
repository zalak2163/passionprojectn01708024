using Microsoft.AspNetCore.Mvc;
using passionprojectn01708024.Interfaces;
using passionprojectn01708024.Models;
using System.Threading.Tasks;

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

		/// <summary>
		/// Registers an attendee for a specific event.
		/// </summary>
		[HttpPost("{attendeeId}/register")]
		public async Task<ActionResult<EventAttendeeDto>> RegisterAttendee(int eventId, int attendeeId)
		{
			var attendeeDto = new EventAttendeeDto
			{
				EventId = eventId,
				AttendeeId = attendeeId
			};

			var serviceResponse = await _eventAttendeeService.RegisterAttendee(attendeeDto);
			if (serviceResponse.Status == ServiceResponse.ServiceStatus.Error)
			{
				return Conflict(serviceResponse.Messages); 
			}

			return CreatedAtAction(nameof(RegisterAttendee), new { eventId, attendeeId }, attendeeDto);
		}

		/// <summary>
		/// Unregisters an attendee from a specific event.
		/// </summary>
		[HttpPost("{attendeeId}/unregister")]
		public async Task<IActionResult> UnregisterAttendee(int eventId, int attendeeId)
		{
			var serviceResponse = await _eventAttendeeService.UnregisterAttendee(eventId, attendeeId);
			if (serviceResponse.Status == ServiceResponse.ServiceStatus.NotFound)
			{
				return NotFound(serviceResponse.Messages);
			}

			return NoContent();
		}
	}
}
