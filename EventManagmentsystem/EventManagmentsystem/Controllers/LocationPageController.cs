using EventManagmentsystem.Interface;
using EventManagmentsystem.Models;
using EventManagmentsystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventManagmentsystem.Controllers
{
    public class LocationPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly ILocationService _locationService;

        public LocationPageController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        /// <summary>
        /// Retrieves a list of all locations.
        /// </summary>
        /// <returns>A collection of LocationDto objects.</returns>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            return View(await _locationService.ListLocations());
        }

        /// <summary>
        /// Retrieves a specific location by ID.
        /// </summary>
        /// <param name="id">The ID of the location to retrieve.</param>
        /// <returns>The requested LocationDto, or null if not found.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            return View(await _locationService.GetLocation(id));
        }

        /// <summary>
        /// Creates a new location.
        /// </summary>
        /// <param name="locationDto">The location data transfer object.</param>
        /// <returns>A ServiceResponse indicating the result of the creation operation.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(LocationDto locationDto)
        {
            return View(await _locationService.CreateLocation(locationDto));
        }

        /// <summary>
        /// Updates the details of an existing location.
        /// </summary>
        /// <param name="id">The ID of the location to update.</param>
        /// <param name="locationDto">The updated location data transfer object.</param>
        /// <returns>A ServiceResponse indicating the result of the update operation.</returns>
        [HttpPut]
        public async Task<IActionResult> Update(int id, LocationDto locationDto)
        {
            return View(await _locationService.UpdateLocationDetails(id, locationDto));
        }

        /// <summary>
        /// Deletes a location by its ID.
        /// </summary>
        /// <param name="id">The ID of the location to delete.</param>
        /// <returns>A ServiceResponse indicating the result of the deletion operation.</returns>
        [HttpDelete]
        public async Task<IActionResult> ConfirmDelete(int id)
        {

            LocationDto? locationDto = await _locationService.GetLocation(id);
            if (locationDto == null)
            {
                return View("Error");
            }
            else
            {
                return View(locationDto);
            }
        }

        //POST CategoryPage/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse response = await _locationService.DeleteLocation(id);

            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("List", "AttendeePage");
            }
            else
            {
                return View("Error");
            }
        }
    }
}
