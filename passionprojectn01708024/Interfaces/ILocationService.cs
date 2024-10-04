using passionprojectn01708024.Models;

namespace passionprojectn01708024.Interfaces
{
	public interface ILocationService
	{
		Task<IEnumerable<Location>> GetLocationsAsync();
		Task<Location?> GetLocationAsync(int id);
		Task<ServiceResponse> AddOrUpdateLocationAsync(Location location);
		Task<ServiceResponse> DeleteLocationAsync(int id);
	}
}
