using Microsoft.AspNetCore.Mvc;
using passionprojectn01708024.Interfaces;
using passionprojectn01708024.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagementSystem.Services;


namespace EventManagementSystem.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EventController : ControllerBase
	{
		private readonly EventService _eventService;

		public EventController(EventService eventService)
		{
			_eventService = eventService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllEvents()
		{
			var events = await _eventService.GetAllEventsAsync();
			return Ok(events);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetEventById(int id)
		{
			var eventEntity = await _eventService.GetEventByIdAsync(id);
			if (eventEntity == null) return NotFound();

			return Ok(eventEntity);
		}

		[HttpPost]
		public async Task<IActionResult> CreateEvent([FromBody] EventDto newEvent)
		{
			await _eventService.CreateEventAsync(newEvent);
			return CreatedAtAction(nameof(GetEventById), new { id = newEvent.EventId }, newEvent);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventDto updatedEvent)
		{
			if (id != updatedEvent.EventId) return BadRequest();

			await _eventService.UpdateEventAsync(updatedEvent);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteEvent(int id)
		{
			await _eventService.DeleteEventAsync(id);
			return NoContent();
		}
	}
}

