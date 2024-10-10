using Microsoft.AspNetCore.Mvc;
using passionprojectn01708024.Interfaces;
using passionprojectn01708024.Models;

namespace passionprojectn01708024.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AttendeeController : ControllerBase
	{
		private readonly IAttendeeService _attendeeService;

		public AttendeeController(IAttendeeService attendeeService)
		{
			_attendeeService = attendeeService;
		}

		/// <summary>
		/// Retrieves a list of all attendees.
		/// </summary>
		/// <returns>A collection of AttendeeDto objects.</returns>
		[HttpGet("List")]
		public async Task<IEnumerable<AttendeeDto>> GetAttendeesAsync()
		{
			return await _attendeeService.GetAttendeesAsync();
		}

		/// <summary>
		/// Retrieves details of a specific attendee by their ID.
		/// </summary>
		/// <param name="id">The ID of the attendee to retrieve.</param>
		/// <returns>An AttendeeDto object containing the attendee's details.</returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<AttendeeDto>> GetAttendee(int id)
		{
			var attendee = await _attendeeService.GetAttendeeAsync(id);
			if (attendee == null)
			{
				return NotFound();
			}

			return Ok(attendee);
		}

		/// <summary>
		/// Updates the details of an existing attendee.
		/// </summary>
		/// <param name="attendee">The updated attendee details.</param>
		/// <returns>The updated attendee object.</returns>
		[HttpPut("Update")]
		public async Task<ActionResult<AttendeeDto>> UpdateAttendee(Attendee attendee)
		{
			var serviceResponse = await _attendeeService.AddOrUpdateAttendeeAsync(attendee);
			return Ok(serviceResponse);
		}

		/// <summary>
		/// Deletes an attendee by their ID.
		/// </summary>
		/// <param name="id">The ID of the attendee to delete.</param>
		/// <returns>No content if deletion is successful; otherwise, not found.</returns>
		[HttpDelete("Delete/{id}")]
		public async Task<IActionResult> DeleteAttendee(int id)
		{
			var serviceResponse = await _attendeeService.DeleteAttendeeAsync(id);
			if (serviceResponse.Status == ServiceResponse.ServiceStatus.NotFound)
			{
				return NotFound();
			}

			return NoContent();
		}

		/// <summary>
		/// Registers an attendee for a specific event.
		/// </summary>
		/// <param name="id">The ID of the attendee.</param>
		/// <param name="eventId">The ID of the event to register for.</param>
		/// <returns>No content if registration is successful; otherwise, not found.</returns>
		[HttpPost("{id}/events/{eventId}/register")]
		public async Task<IActionResult> RegisterForEvent(int id, int eventId)
		{
			var serviceResponse = await _attendeeService.RegisterAttendee(id, eventId);
			return serviceResponse.Status == ServiceResponse.ServiceStatus.Created ? NoContent() : NotFound();
		}

		/// <summary>
		/// Unregisters an attendee from a specific event.
		/// </summary>
		/// <param name="attendeeId">The ID of the attendee.</param>
		/// <param name="eventId">The ID of the event to unregister from.</param>
		/// <returns>No content if unregistration is successful; otherwise, not found.</returns>
		[HttpPost("{attendeeId}/events/{eventId}/unregister")]
		public async Task<IActionResult> UnregisterAttendee(int attendeeId, int eventId)
		{
			var serviceResponse = await _attendeeService.UnregisterAttendee(attendeeId, eventId);
			return serviceResponse.Status == ServiceResponse.ServiceStatus.Deleted ? NoContent() : NotFound();
		}
	}
}
