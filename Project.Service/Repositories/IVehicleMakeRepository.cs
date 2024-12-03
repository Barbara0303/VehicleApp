using Project.Service.Parameters;
using Project.Service.Models;

namespace Project.Service.Repositories
{
    public interface IVehicleMakeRepository
    {
        Task<IEnumerable<VehicleMake>> GetAllAsync(
            FilteringParameters filteringParams,
            SortingParameters sortingParams,
            PagingParameters pagingParams);
        Task<VehicleMake?> GetByIdAsync(int id);
        Task<VehicleMake> AddAsync(VehicleMake vehicleMake);
        Task<VehicleMake?> UpdateAsync(VehicleMake vehicleMake);
        Task<VehicleMake?> DeleteAsync(int id);
        Task<int> CountAsync(FilteringParameters filteringParams);
        Task<IEnumerable<VehicleMake>> GetAllForDropdownAsync();
    }
}
