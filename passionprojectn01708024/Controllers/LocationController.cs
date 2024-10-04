using Microsoft.AspNetCore.Mvc;
using passionprojectn01708024.Interfaces;
using passionprojectn01708024.Models;

namespace passionprojectn01708024.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LocationController : ControllerBase
	{
		private readonly ILocationService _locationService;

		public LocationController(ILocationService locationService)
		{
			_locationService = locationService;
		}

		// GET: api/location
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
		{
			var locations = await _locationService.GetLocationsAsync();
			return Ok(locations);
		}

		// GET: api/location/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<Location>> GetLocation(int id)
		{
			var location = await _locationService.GetLocationAsync(id);
			if (location == null)
			{
				return NotFound();
			}

			return Ok(location);
		}

		// POST: api/location
		[HttpPost]
		public async Task<ActionResult<Location>> PostLocation(Location location)
		{
			var serviceResponse = await _locationService.AddOrUpdateLocationAsync(location);
			return CreatedAtAction(nameof(GetLocation), new { id = serviceResponse.CreatedId }, location);
		}

		// DELETE: api/location/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteLocation(int id)
		{
			var serviceResponse = await _locationService.DeleteLocationAsync(id);
			if (serviceResponse.Status == ServiceResponse.ServiceStatus.NotFound)
			{
				return NotFound();
			}

			return NoContent();
		}
	}
}
