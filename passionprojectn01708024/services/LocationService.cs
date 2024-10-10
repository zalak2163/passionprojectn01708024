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

		public async Task<IEnumerable<LocationDto>> GetLocationsAsync()
		{
			var locations = await _context.Locations.ToListAsync();
			return locations.Select(location => new LocationDto
			{
				LocationId = location.LocationId,
				LocationName = location.LocationName,
				Address = location.Address,
				Capacity = location.Capacity
			}).ToList();
		}

		public async Task<LocationDto?> GetLocationAsync(int id)
		{
			var location = await _context.Locations.FindAsync(id);
			if (location == null) return null;

			return new LocationDto
			{
				LocationId = location.LocationId,
				LocationName = location.LocationName,
				Address = location.Address,
				Capacity = location.Capacity
			};
		}

		public async Task<ServiceResponse> AddOrUpdateLocationAsync(LocationDto locationDto)
		{
			var serviceResponse = new ServiceResponse();
			var location = new Location
			{
				LocationId = locationDto.LocationId,
				LocationName = locationDto.LocationName,
				Address = locationDto.Address,
				Capacity = locationDto.Capacity
			};

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
