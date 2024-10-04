using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using passionprojectn01708024.Data;
using passionprojectn01708024.Interfaces;
using passionprojectn01708024.Models;

namespace passionprojectn01708024.Services
{
	public class LocationService : ILocationService
	{
		private readonly ApplicationDbContext _context;

		public LocationService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Location>> GetLocationsAsync()
		{
			return await _context.Locations.ToListAsync();
		}

		public async Task<Location?> GetLocationAsync(int id)
		{
			return await _context.Locations.FindAsync(id);
		}

		public async Task<ServiceResponse> AddOrUpdateLocationAsync(Location location)
		{
			var serviceResponse = new ServiceResponse();

			if (location.LocationId == 0)
			{
				_context.Locations.Add(location);
				serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
			}
			else
			{
				_context.Entry(location).State = EntityState.Modified;
				serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
			}

			await _context.SaveChangesAsync();
			serviceResponse.CreatedId = location.LocationId;
			return serviceResponse;
		}

		public async Task<ServiceResponse> DeleteLocationAsync(int id)
		{
			var serviceResponse = new ServiceResponse();
			var location = await _context.Locations.FindAsync(id);

			if (location == null)
			{
				serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
				return serviceResponse;
			}

			_context.Locations.Remove(location);
			await _context.SaveChangesAsync();
			serviceResponse.Status = ServiceResponse.ServiceStatus.Deleted;
			return serviceResponse;
		}
	}
}
