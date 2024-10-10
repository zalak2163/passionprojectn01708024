using System.Collections.Generic;
using System.Threading.Tasks;
using passionprojectn01708024.Models;

namespace passionprojectn01708024.Interfaces
{
	public interface ILocationService
	{
		Task<IEnumerable<LocationDto>> GetLocationsAsync();
		Task<LocationDto?> GetLocationAsync(int id);
		Task<ServiceResponse> AddOrUpdateLocationAsync(LocationDto locationDto);
		Task<ServiceResponse> DeleteLocationAsync(int id);
	}
}
