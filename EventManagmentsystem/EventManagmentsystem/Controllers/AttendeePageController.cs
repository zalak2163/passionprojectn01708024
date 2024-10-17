using EventManagmentsystem.Interface;
using EventManagmentsystem.Models;
using EventManagmentsystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventManagmentsystem.Controllers
{
    public class AttendeePageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly IAttendeeService _attendeeService;

        public AttendeePageController(IAttendeeService attendeeService)
        {
            _attendeeService = attendeeService;
        }

        /// <summary>
        /// Retrieves a list of all attendees.
        /// </summary>
        /// <returns>A collection of <see cref="AttendeeDto"/>.</returns>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            return View(await _attendeeService.ListAttendees());
        }

        /// <summary>
        /// Retrieves a specific attendee by ID.
        /// </summary>
        /// <param name="id">The ID of the attendee.</param>
        /// <returns>The <see cref="AttendeeDto"/> if found; otherwise, null.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            return View(await _attendeeService.Getattendee(id));
        }

        /// <summary>
        /// Creates a new attendee.
        /// </summary>
        /// <param name="attendeeDto">The attendee details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the creation.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(AttendeeDto attendeeDto)
        {
            return View(await _attendeeService.CreateAttendee(attendeeDto));
        }

        /// <summary>
        /// Updates the details of an existing attendee.
        /// </summary>
        /// <param name="id">The ID of the attendee to update.</param>
        /// <param name="attendeeDto">The updated attendee details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the update.</returns>
        [HttpPut]
        public async Task<IActionResult> Update(int id, AttendeeDto attendeeDto)
        {
            return View(await _attendeeService.UpdateAttendeeDetails(id, attendeeDto));
        }

        /// <summary>
        /// Deletes an attendee by ID.
        /// </summary>
        /// <param name="id">The ID of the attendee to delete.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the deletion.</returns>
        [HttpDelete]
        public async Task<IActionResult> ConfirmDelete(int id)
        {

            AttendeeDto? attendeeDto = await _attendeeService.Getattendee(id);
            if (attendeeDto == null)
            {
                return View("Error");
            }
            else
            {
                return View(attendeeDto);
            }
        }

        //POST CategoryPage/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse response = await _attendeeService.DeleteAttendee(id);

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
