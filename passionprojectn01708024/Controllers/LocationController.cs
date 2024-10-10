using Microsoft.AspNetCore.Mvc;
using passionprojectn01708024.Interfaces;
using passionprojectn01708024.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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

		/// <summary>
		/// Retrieves a list of all locations.
		/// </summary>
		/// <returns>A collection of Location objects.</returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<LocationDto>>> GetLocations()
		{
			var locations = await _locationService.GetLocationsAsync();
			return Ok(locations);
		}

		/// <summary>
		/// Retrieves details of a specific location by its ID.
		/// </summary>
		/// <param name="id">The ID of the location to retrieve.</param>
		/// <returns>A Location object containing the location details.</returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<LocationDto>> GetLocation(int id)
		{
			var location = await _locationService.GetLocationAsync(id);
			if (location == null)
			{
				return NotFound();
			}

			return Ok(location);
		}

		/// <summary>
		/// Creates a new location or updates an existing one.
		/// </summary>
		/// <param name="location">The location details to create or update.</param>
		/// <returns>The created Location object with a 201 Created status.</returns>
		[HttpPost]
		public async Task<ActionResult<LocationDto>> PostLocation(LocationDto location)
		{
			var serviceResponse = await _locationService.AddOrUpdateLocationAsync(location);
			return CreatedAtAction(nameof(GetLocation), new { id = serviceResponse.CreatedId }, location);
		}

		/// <summary>
		/// Deletes a location by its ID.
		/// </summary>
		/// <param name="id">The ID of the location to delete.</param>
		/// <returns>No content if the deletion is successful; otherwise, 404 Not Found.</returns>
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
