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

        // GET: api/attendee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendee>>> GetAttendees()
        {
            var attendees = await _attendeeService.GetAttendeesAsync();
            return Ok(attendees);
        }

		// GET: api/attendee/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<Attendee>> GetAttendee(int id)
		{
			var attendee = await _attendeeService.GetAttendeeAsync(id);
			if (attendee == null)
			{
				return NotFound();
			}

			return Ok(attendee);
		}

		// PUT: api/attendee
		[HttpPut]
        public async Task<ActionResult<Attendee>> UpdateAttendee(Attendee attendee)
        {
            var serviceResponse = await _attendeeService.AddOrUpdateAttendeeAsync(attendee);
            return Ok(serviceResponse); // Assuming serviceResponse contains updated attendee
        }

        // DELETE: api/attendee/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendee(int id)
        {
            var serviceResponse = await _attendeeService.DeleteAttendeeAsync(id);
            if (serviceResponse.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/attendee/{id}/events/{eventId}/register
        [HttpPost("{id}/events/{eventId}/register")]
        public async Task<IActionResult> RegisterForEvent(int id, int eventId)
        {
            var serviceResponse = await _attendeeService.RegisterForEventAsync(id, eventId);
            return serviceResponse.Status == ServiceResponse.ServiceStatus.Created ? NoContent() : NotFound();
        }

        // POST: api/attendee/{id}/events/{eventId}/unregister
        [HttpPost("{id}/events/{eventId}/unregister")]
        public async Task<IActionResult> UnregisterFromEvent(int id, int eventId)
        {
            var serviceResponse = await _attendeeService.UnregisterFromEventAsync(id, eventId);
            return serviceResponse.Status == ServiceResponse.ServiceStatus.Deleted ? NoContent() : NotFound();
        }
    }
}
